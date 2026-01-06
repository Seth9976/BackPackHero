using System;
using System.Buffers;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net.WebSockets
{
	// Token: 0x020005D7 RID: 1495
	internal sealed class ManagedWebSocket : WebSocket
	{
		// Token: 0x06003048 RID: 12360 RVA: 0x000AAE40 File Offset: 0x000A9040
		public static ManagedWebSocket CreateFromConnectedStream(Stream stream, bool isServer, string subprotocol, TimeSpan keepAliveInterval)
		{
			return new ManagedWebSocket(stream, isServer, subprotocol, keepAliveInterval);
		}

		// Token: 0x17000B3F RID: 2879
		// (get) Token: 0x06003049 RID: 12361 RVA: 0x000AAE4B File Offset: 0x000A904B
		private object StateUpdateLock
		{
			get
			{
				return this._abortSource;
			}
		}

		// Token: 0x17000B40 RID: 2880
		// (get) Token: 0x0600304A RID: 12362 RVA: 0x000AAE53 File Offset: 0x000A9053
		private object ReceiveAsyncLock
		{
			get
			{
				return this._utf8TextState;
			}
		}

		// Token: 0x0600304B RID: 12363 RVA: 0x000AAE5C File Offset: 0x000A905C
		private ManagedWebSocket(Stream stream, bool isServer, string subprotocol, TimeSpan keepAliveInterval)
		{
			this._stream = stream;
			this._isServer = isServer;
			this._subprotocol = subprotocol;
			this._receiveBuffer = new byte[125];
			this._abortSource.Token.Register(delegate(object s)
			{
				ManagedWebSocket managedWebSocket = (ManagedWebSocket)s;
				object stateUpdateLock = managedWebSocket.StateUpdateLock;
				lock (stateUpdateLock)
				{
					WebSocketState state = managedWebSocket._state;
					if (state != WebSocketState.Closed && state != WebSocketState.Aborted)
					{
						managedWebSocket._state = ((state != WebSocketState.None && state != WebSocketState.Connecting) ? WebSocketState.Aborted : WebSocketState.Closed);
					}
				}
			}, this);
			if (keepAliveInterval > TimeSpan.Zero)
			{
				this._keepAliveTimer = new Timer(delegate(object s)
				{
					((ManagedWebSocket)s).SendKeepAliveFrameAsync();
				}, this, keepAliveInterval, keepAliveInterval);
			}
		}

		// Token: 0x0600304C RID: 12364 RVA: 0x000AAF5C File Offset: 0x000A915C
		public override void Dispose()
		{
			object stateUpdateLock = this.StateUpdateLock;
			lock (stateUpdateLock)
			{
				this.DisposeCore();
			}
		}

		// Token: 0x0600304D RID: 12365 RVA: 0x000AAF9C File Offset: 0x000A919C
		private void DisposeCore()
		{
			if (!this._disposed)
			{
				this._disposed = true;
				Timer keepAliveTimer = this._keepAliveTimer;
				if (keepAliveTimer != null)
				{
					keepAliveTimer.Dispose();
				}
				Stream stream = this._stream;
				if (stream != null)
				{
					stream.Dispose();
				}
				if (this._state < WebSocketState.Aborted)
				{
					this._state = WebSocketState.Closed;
				}
			}
		}

		// Token: 0x17000B41 RID: 2881
		// (get) Token: 0x0600304E RID: 12366 RVA: 0x000AAFEA File Offset: 0x000A91EA
		public override WebSocketCloseStatus? CloseStatus
		{
			get
			{
				return this._closeStatus;
			}
		}

		// Token: 0x17000B42 RID: 2882
		// (get) Token: 0x0600304F RID: 12367 RVA: 0x000AAFF2 File Offset: 0x000A91F2
		public override string CloseStatusDescription
		{
			get
			{
				return this._closeStatusDescription;
			}
		}

		// Token: 0x17000B43 RID: 2883
		// (get) Token: 0x06003050 RID: 12368 RVA: 0x000AAFFA File Offset: 0x000A91FA
		public override WebSocketState State
		{
			get
			{
				return this._state;
			}
		}

		// Token: 0x17000B44 RID: 2884
		// (get) Token: 0x06003051 RID: 12369 RVA: 0x000AB002 File Offset: 0x000A9202
		public override string SubProtocol
		{
			get
			{
				return this._subprotocol;
			}
		}

		// Token: 0x06003052 RID: 12370 RVA: 0x000AB00C File Offset: 0x000A920C
		public override Task SendAsync(ArraySegment<byte> buffer, WebSocketMessageType messageType, bool endOfMessage, CancellationToken cancellationToken)
		{
			if (messageType != WebSocketMessageType.Text && messageType != WebSocketMessageType.Binary)
			{
				throw new ArgumentException(SR.Format("The message type '{0}' is not allowed for the '{1}' operation. Valid message types are: '{2}, {3}'. To close the WebSocket, use the '{4}' operation instead. ", new object[] { "Close", "SendAsync", "Binary", "Text", "CloseOutputAsync" }), "messageType");
			}
			WebSocketValidate.ValidateArraySegment(buffer, "buffer");
			return this.SendPrivateAsync(buffer, messageType, endOfMessage, cancellationToken).AsTask();
		}

		// Token: 0x06003053 RID: 12371 RVA: 0x000AB088 File Offset: 0x000A9288
		private ValueTask SendPrivateAsync(ReadOnlyMemory<byte> buffer, WebSocketMessageType messageType, bool endOfMessage, CancellationToken cancellationToken)
		{
			if (messageType != WebSocketMessageType.Text && messageType != WebSocketMessageType.Binary)
			{
				throw new ArgumentException(SR.Format("The message type '{0}' is not allowed for the '{1}' operation. Valid message types are: '{2}, {3}'. To close the WebSocket, use the '{4}' operation instead. ", new object[] { "Close", "SendAsync", "Binary", "Text", "CloseOutputAsync" }), "messageType");
			}
			try
			{
				WebSocketValidate.ThrowIfInvalidState(this._state, this._disposed, ManagedWebSocket.s_validSendStates);
			}
			catch (Exception ex)
			{
				return new ValueTask(Task.FromException(ex));
			}
			ManagedWebSocket.MessageOpcode messageOpcode = (this._lastSendWasFragment ? ManagedWebSocket.MessageOpcode.Continuation : ((messageType == WebSocketMessageType.Binary) ? ManagedWebSocket.MessageOpcode.Binary : ManagedWebSocket.MessageOpcode.Text));
			ValueTask valueTask = this.SendFrameAsync(messageOpcode, endOfMessage, buffer, cancellationToken);
			this._lastSendWasFragment = !endOfMessage;
			return valueTask;
		}

		// Token: 0x06003054 RID: 12372 RVA: 0x000AB140 File Offset: 0x000A9340
		public override Task<WebSocketReceiveResult> ReceiveAsync(ArraySegment<byte> buffer, CancellationToken cancellationToken)
		{
			WebSocketValidate.ValidateArraySegment(buffer, "buffer");
			Task<WebSocketReceiveResult> task2;
			try
			{
				WebSocketValidate.ThrowIfInvalidState(this._state, this._disposed, ManagedWebSocket.s_validReceiveStates);
				object receiveAsyncLock = this.ReceiveAsyncLock;
				lock (receiveAsyncLock)
				{
					this.ThrowIfOperationInProgress(this._lastReceiveAsync.IsCompleted, "ReceiveAsync");
					Task<WebSocketReceiveResult> task = this.ReceiveAsyncPrivate<ManagedWebSocket.WebSocketReceiveResultGetter, WebSocketReceiveResult>(buffer, cancellationToken, default(ManagedWebSocket.WebSocketReceiveResultGetter)).AsTask();
					this._lastReceiveAsync = task;
					task2 = task;
				}
			}
			catch (Exception ex)
			{
				task2 = Task.FromException<WebSocketReceiveResult>(ex);
			}
			return task2;
		}

		// Token: 0x06003055 RID: 12373 RVA: 0x000AB1F4 File Offset: 0x000A93F4
		public override Task CloseAsync(WebSocketCloseStatus closeStatus, string statusDescription, CancellationToken cancellationToken)
		{
			WebSocketValidate.ValidateCloseStatus(closeStatus, statusDescription);
			try
			{
				WebSocketValidate.ThrowIfInvalidState(this._state, this._disposed, ManagedWebSocket.s_validCloseStates);
			}
			catch (Exception ex)
			{
				return Task.FromException(ex);
			}
			return this.CloseAsyncPrivate(closeStatus, statusDescription, cancellationToken);
		}

		// Token: 0x06003056 RID: 12374 RVA: 0x000AB244 File Offset: 0x000A9444
		public override Task CloseOutputAsync(WebSocketCloseStatus closeStatus, string statusDescription, CancellationToken cancellationToken)
		{
			WebSocketValidate.ValidateCloseStatus(closeStatus, statusDescription);
			try
			{
				WebSocketValidate.ThrowIfInvalidState(this._state, this._disposed, ManagedWebSocket.s_validCloseOutputStates);
			}
			catch (Exception ex)
			{
				return Task.FromException(ex);
			}
			return this.SendCloseFrameAsync(closeStatus, statusDescription, cancellationToken);
		}

		// Token: 0x06003057 RID: 12375 RVA: 0x000AB294 File Offset: 0x000A9494
		public override void Abort()
		{
			this._abortSource.Cancel();
			this.Dispose();
		}

		// Token: 0x06003058 RID: 12376 RVA: 0x000AB2A7 File Offset: 0x000A94A7
		private ValueTask SendFrameAsync(ManagedWebSocket.MessageOpcode opcode, bool endOfMessage, ReadOnlyMemory<byte> payloadBuffer, CancellationToken cancellationToken)
		{
			if (!cancellationToken.CanBeCanceled && this._sendFrameAsyncLock.Wait(0))
			{
				return this.SendFrameLockAcquiredNonCancelableAsync(opcode, endOfMessage, payloadBuffer);
			}
			return new ValueTask(this.SendFrameFallbackAsync(opcode, endOfMessage, payloadBuffer, cancellationToken));
		}

		// Token: 0x06003059 RID: 12377 RVA: 0x000AB2DC File Offset: 0x000A94DC
		private ValueTask SendFrameLockAcquiredNonCancelableAsync(ManagedWebSocket.MessageOpcode opcode, bool endOfMessage, ReadOnlyMemory<byte> payloadBuffer)
		{
			ValueTask valueTask = default(ValueTask);
			bool flag = true;
			try
			{
				int num = this.WriteFrameToSendBuffer(opcode, endOfMessage, payloadBuffer.Span);
				valueTask = this._stream.WriteAsync(new ReadOnlyMemory<byte>(this._sendBuffer, 0, num), default(CancellationToken));
				if (valueTask.IsCompleted)
				{
					return valueTask;
				}
				flag = false;
			}
			catch (Exception ex)
			{
				return new ValueTask(Task.FromException((ex is OperationCanceledException) ? ex : ((this._state == WebSocketState.Aborted) ? ManagedWebSocket.CreateOperationCanceledException(ex, default(CancellationToken)) : new WebSocketException(WebSocketError.ConnectionClosedPrematurely, ex))));
			}
			finally
			{
				if (flag)
				{
					this._sendFrameAsyncLock.Release();
					this.ReleaseSendBuffer();
				}
			}
			return new ValueTask(this.WaitForWriteTaskAsync(valueTask));
		}

		// Token: 0x0600305A RID: 12378 RVA: 0x000AB3B8 File Offset: 0x000A95B8
		private async Task WaitForWriteTaskAsync(ValueTask writeTask)
		{
			try
			{
				await writeTask.ConfigureAwait(false);
			}
			catch (Exception ex) when (!(ex is OperationCanceledException))
			{
				throw (this._state == WebSocketState.Aborted) ? ManagedWebSocket.CreateOperationCanceledException(ex, default(CancellationToken)) : new WebSocketException(WebSocketError.ConnectionClosedPrematurely, ex);
			}
			finally
			{
				this._sendFrameAsyncLock.Release();
				this.ReleaseSendBuffer();
			}
		}

		// Token: 0x0600305B RID: 12379 RVA: 0x000AB404 File Offset: 0x000A9604
		private async Task SendFrameFallbackAsync(ManagedWebSocket.MessageOpcode opcode, bool endOfMessage, ReadOnlyMemory<byte> payloadBuffer, CancellationToken cancellationToken)
		{
			await this._sendFrameAsyncLock.WaitAsync().ConfigureAwait(false);
			try
			{
				int num = this.WriteFrameToSendBuffer(opcode, endOfMessage, payloadBuffer.Span);
				using (cancellationToken.Register(delegate(object s)
				{
					((ManagedWebSocket)s).Abort();
				}, this))
				{
					await this._stream.WriteAsync(new ReadOnlyMemory<byte>(this._sendBuffer, 0, num), cancellationToken).ConfigureAwait(false);
				}
				CancellationTokenRegistration cancellationTokenRegistration = default(CancellationTokenRegistration);
			}
			catch (Exception ex) when (!(ex is OperationCanceledException))
			{
				throw (this._state == WebSocketState.Aborted) ? ManagedWebSocket.CreateOperationCanceledException(ex, cancellationToken) : new WebSocketException(WebSocketError.ConnectionClosedPrematurely, ex);
			}
			finally
			{
				this._sendFrameAsyncLock.Release();
				this.ReleaseSendBuffer();
			}
		}

		// Token: 0x0600305C RID: 12380 RVA: 0x000AB468 File Offset: 0x000A9668
		private int WriteFrameToSendBuffer(ManagedWebSocket.MessageOpcode opcode, bool endOfMessage, ReadOnlySpan<byte> payloadBuffer)
		{
			this.AllocateSendBuffer(payloadBuffer.Length + 14);
			int? num = null;
			int num2;
			if (this._isServer)
			{
				num2 = ManagedWebSocket.WriteHeader(opcode, this._sendBuffer, payloadBuffer, endOfMessage, false);
			}
			else
			{
				num = new int?(ManagedWebSocket.WriteHeader(opcode, this._sendBuffer, payloadBuffer, endOfMessage, true));
				num2 = num.GetValueOrDefault() + 4;
			}
			if (payloadBuffer.Length > 0)
			{
				payloadBuffer.CopyTo(new Span<byte>(this._sendBuffer, num2, payloadBuffer.Length));
				if (num != null)
				{
					ManagedWebSocket.ApplyMask(new Span<byte>(this._sendBuffer, num2, payloadBuffer.Length), this._sendBuffer, num.Value, 0);
				}
			}
			return num2 + payloadBuffer.Length;
		}

		// Token: 0x0600305D RID: 12381 RVA: 0x000AB524 File Offset: 0x000A9724
		private void SendKeepAliveFrameAsync()
		{
			if (this._sendFrameAsyncLock.Wait(0))
			{
				ValueTask valueTask = this.SendFrameLockAcquiredNonCancelableAsync(ManagedWebSocket.MessageOpcode.Ping, true, Memory<byte>.Empty);
				if (valueTask.IsCompletedSuccessfully)
				{
					valueTask.GetAwaiter().GetResult();
					return;
				}
				valueTask.AsTask().ContinueWith(delegate(Task p)
				{
					AggregateException exception = p.Exception;
				}, CancellationToken.None, TaskContinuationOptions.NotOnRanToCompletion | TaskContinuationOptions.NotOnCanceled | TaskContinuationOptions.ExecuteSynchronously, TaskScheduler.Default);
			}
		}

		// Token: 0x0600305E RID: 12382 RVA: 0x000AB5A8 File Offset: 0x000A97A8
		private static int WriteHeader(ManagedWebSocket.MessageOpcode opcode, byte[] sendBuffer, ReadOnlySpan<byte> payload, bool endOfMessage, bool useMask)
		{
			sendBuffer[0] = (byte)opcode;
			if (endOfMessage)
			{
				int num = 0;
				sendBuffer[num] |= 128;
			}
			int num2;
			if (payload.Length <= 125)
			{
				sendBuffer[1] = (byte)payload.Length;
				num2 = 2;
			}
			else if (payload.Length <= 65535)
			{
				sendBuffer[1] = 126;
				sendBuffer[2] = (byte)(payload.Length / 256);
				sendBuffer[3] = (byte)payload.Length;
				num2 = 4;
			}
			else
			{
				sendBuffer[1] = 127;
				int num3 = payload.Length;
				for (int i = 9; i >= 2; i--)
				{
					sendBuffer[i] = (byte)num3;
					num3 /= 256;
				}
				num2 = 10;
			}
			if (useMask)
			{
				int num4 = 1;
				sendBuffer[num4] |= 128;
				ManagedWebSocket.WriteRandomMask(sendBuffer, num2);
			}
			return num2;
		}

		// Token: 0x0600305F RID: 12383 RVA: 0x000AB661 File Offset: 0x000A9861
		private static void WriteRandomMask(byte[] buffer, int offset)
		{
			ManagedWebSocket.s_random.GetBytes(buffer, offset, 4);
		}

		// Token: 0x06003060 RID: 12384 RVA: 0x000AB670 File Offset: 0x000A9870
		private ValueTask<TWebSocketReceiveResult> ReceiveAsyncPrivate<TWebSocketReceiveResultGetter, TWebSocketReceiveResult>(Memory<byte> payloadBuffer, CancellationToken cancellationToken, TWebSocketReceiveResultGetter resultGetter = default(TWebSocketReceiveResultGetter)) where TWebSocketReceiveResultGetter : struct, ManagedWebSocket.IWebSocketReceiveResultGetter<TWebSocketReceiveResult>
		{
			ManagedWebSocket.<ReceiveAsyncPrivate>d__61<TWebSocketReceiveResultGetter, TWebSocketReceiveResult> <ReceiveAsyncPrivate>d__;
			<ReceiveAsyncPrivate>d__.<>4__this = this;
			<ReceiveAsyncPrivate>d__.payloadBuffer = payloadBuffer;
			<ReceiveAsyncPrivate>d__.cancellationToken = cancellationToken;
			<ReceiveAsyncPrivate>d__.resultGetter = resultGetter;
			<ReceiveAsyncPrivate>d__.<>t__builder = AsyncValueTaskMethodBuilder<TWebSocketReceiveResult>.Create();
			<ReceiveAsyncPrivate>d__.<>1__state = -1;
			<ReceiveAsyncPrivate>d__.<>t__builder.Start<ManagedWebSocket.<ReceiveAsyncPrivate>d__61<TWebSocketReceiveResultGetter, TWebSocketReceiveResult>>(ref <ReceiveAsyncPrivate>d__);
			return <ReceiveAsyncPrivate>d__.<>t__builder.Task;
		}

		// Token: 0x06003061 RID: 12385 RVA: 0x000AB6CC File Offset: 0x000A98CC
		private unsafe async Task HandleReceivedCloseAsync(ManagedWebSocket.MessageHeader header, CancellationToken cancellationToken)
		{
			object stateUpdateLock = this.StateUpdateLock;
			lock (stateUpdateLock)
			{
				this._receivedCloseFrame = true;
				if (this._state < WebSocketState.CloseReceived)
				{
					this._state = WebSocketState.CloseReceived;
				}
			}
			WebSocketCloseStatus closeStatus = WebSocketCloseStatus.NormalClosure;
			string closeStatusDescription = string.Empty;
			if (header.PayloadLength == 1L)
			{
				await this.CloseWithReceiveErrorAndThrowAsync(WebSocketCloseStatus.ProtocolError, WebSocketError.Faulted, null).ConfigureAwait(false);
			}
			else if (header.PayloadLength >= 2L)
			{
				if ((long)this._receiveBufferCount < header.PayloadLength)
				{
					await this.EnsureBufferContainsAsync((int)header.PayloadLength, cancellationToken, true).ConfigureAwait(false);
				}
				if (this._isServer)
				{
					ManagedWebSocket.ApplyMask(this._receiveBuffer.Span.Slice(this._receiveBufferOffset, (int)header.PayloadLength), header.Mask, 0);
				}
				closeStatus = (WebSocketCloseStatus)(((int)(*this._receiveBuffer.Span[this._receiveBufferOffset]) << 8) | (int)(*this._receiveBuffer.Span[this._receiveBufferOffset + 1]));
				if (!ManagedWebSocket.IsValidCloseStatus(closeStatus))
				{
					await this.CloseWithReceiveErrorAndThrowAsync(WebSocketCloseStatus.ProtocolError, WebSocketError.Faulted, null).ConfigureAwait(false);
				}
				if (header.PayloadLength > 2L)
				{
					int num = 0;
					try
					{
						closeStatusDescription = ManagedWebSocket.s_textEncoding.GetString(this._receiveBuffer.Span.Slice(this._receiveBufferOffset + 2, (int)header.PayloadLength - 2));
					}
					catch (DecoderFallbackException stateUpdateLock)
					{
						num = 1;
					}
					if (num == 1)
					{
						await this.CloseWithReceiveErrorAndThrowAsync(WebSocketCloseStatus.ProtocolError, WebSocketError.Faulted, (DecoderFallbackException)stateUpdateLock).ConfigureAwait(false);
					}
				}
				this.ConsumeFromBuffer((int)header.PayloadLength);
			}
			this._closeStatus = new WebSocketCloseStatus?(closeStatus);
			this._closeStatusDescription = closeStatusDescription;
			if (!this._isServer && this._sentCloseFrame)
			{
				await this.WaitForServerToCloseConnectionAsync(cancellationToken).ConfigureAwait(false);
			}
		}

		// Token: 0x06003062 RID: 12386 RVA: 0x000AB720 File Offset: 0x000A9920
		private async Task WaitForServerToCloseConnectionAsync(CancellationToken cancellationToken)
		{
			ValueTask<int> valueTask = this._stream.ReadAsync(this._receiveBuffer, cancellationToken);
			if (!valueTask.IsCompletedSuccessfully)
			{
				using (CancellationTokenSource finalCts = new CancellationTokenSource(1000))
				{
					using (finalCts.Token.Register(delegate(object s)
					{
						((ManagedWebSocket)s).Abort();
					}, this))
					{
						try
						{
							await valueTask.ConfigureAwait(false);
						}
						catch
						{
						}
					}
					CancellationTokenRegistration cancellationTokenRegistration = default(CancellationTokenRegistration);
				}
				CancellationTokenSource finalCts = null;
			}
		}

		// Token: 0x06003063 RID: 12387 RVA: 0x000AB76C File Offset: 0x000A996C
		private async Task HandleReceivedPingPongAsync(ManagedWebSocket.MessageHeader header, CancellationToken cancellationToken)
		{
			if (header.PayloadLength > 0L && (long)this._receiveBufferCount < header.PayloadLength)
			{
				await this.EnsureBufferContainsAsync((int)header.PayloadLength, cancellationToken, true).ConfigureAwait(false);
			}
			if (header.Opcode == ManagedWebSocket.MessageOpcode.Ping)
			{
				if (this._isServer)
				{
					ManagedWebSocket.ApplyMask(this._receiveBuffer.Span.Slice(this._receiveBufferOffset, (int)header.PayloadLength), header.Mask, 0);
				}
				await this.SendFrameAsync(ManagedWebSocket.MessageOpcode.Pong, true, this._receiveBuffer.Slice(this._receiveBufferOffset, (int)header.PayloadLength), default(CancellationToken)).ConfigureAwait(false);
			}
			if (header.PayloadLength > 0L)
			{
				this.ConsumeFromBuffer((int)header.PayloadLength);
			}
		}

		// Token: 0x06003064 RID: 12388 RVA: 0x000AB7BF File Offset: 0x000A99BF
		private static bool IsValidCloseStatus(WebSocketCloseStatus closeStatus)
		{
			return closeStatus >= WebSocketCloseStatus.NormalClosure && closeStatus < (WebSocketCloseStatus)5000 && (closeStatus >= (WebSocketCloseStatus)3000 || (closeStatus - WebSocketCloseStatus.NormalClosure <= 3 || closeStatus - WebSocketCloseStatus.InvalidPayloadData <= 4));
		}

		// Token: 0x06003065 RID: 12389 RVA: 0x000AB7F4 File Offset: 0x000A99F4
		private async Task CloseWithReceiveErrorAndThrowAsync(WebSocketCloseStatus closeStatus, WebSocketError error, Exception innerException = null)
		{
			if (!this._sentCloseFrame)
			{
				await this.CloseOutputAsync(closeStatus, string.Empty, default(CancellationToken)).ConfigureAwait(false);
			}
			this._receiveBufferCount = 0;
			throw new WebSocketException(error, innerException);
		}

		// Token: 0x06003066 RID: 12390 RVA: 0x000AB850 File Offset: 0x000A9A50
		private unsafe bool TryParseMessageHeaderFromReceiveBuffer(out ManagedWebSocket.MessageHeader resultHeader)
		{
			ManagedWebSocket.MessageHeader messageHeader = default(ManagedWebSocket.MessageHeader);
			Span<byte> span = this._receiveBuffer.Span;
			messageHeader.Fin = (*span[this._receiveBufferOffset] & 128) > 0;
			bool flag = (*span[this._receiveBufferOffset] & 112) > 0;
			messageHeader.Opcode = (ManagedWebSocket.MessageOpcode)(*span[this._receiveBufferOffset] & 15);
			bool flag2 = (*span[this._receiveBufferOffset + 1] & 128) > 0;
			messageHeader.PayloadLength = (long)(*span[this._receiveBufferOffset + 1] & 127);
			this.ConsumeFromBuffer(2);
			if (messageHeader.PayloadLength == 126L)
			{
				messageHeader.PayloadLength = (long)(((int)(*span[this._receiveBufferOffset]) << 8) | (int)(*span[this._receiveBufferOffset + 1]));
				this.ConsumeFromBuffer(2);
			}
			else if (messageHeader.PayloadLength == 127L)
			{
				messageHeader.PayloadLength = 0L;
				for (int i = 0; i < 8; i++)
				{
					messageHeader.PayloadLength = (messageHeader.PayloadLength << 8) | (long)((ulong)(*span[this._receiveBufferOffset + i]));
				}
				this.ConsumeFromBuffer(8);
			}
			bool flag3 = flag;
			if (flag2)
			{
				if (!this._isServer)
				{
					flag3 = true;
				}
				messageHeader.Mask = ManagedWebSocket.CombineMaskBytes(span, this._receiveBufferOffset);
				this.ConsumeFromBuffer(4);
			}
			switch (messageHeader.Opcode)
			{
			case ManagedWebSocket.MessageOpcode.Continuation:
				if (this._lastReceiveHeader.Fin)
				{
					flag3 = true;
					goto IL_01CD;
				}
				goto IL_01CD;
			case ManagedWebSocket.MessageOpcode.Text:
			case ManagedWebSocket.MessageOpcode.Binary:
				if (!this._lastReceiveHeader.Fin)
				{
					flag3 = true;
					goto IL_01CD;
				}
				goto IL_01CD;
			case ManagedWebSocket.MessageOpcode.Close:
			case ManagedWebSocket.MessageOpcode.Ping:
			case ManagedWebSocket.MessageOpcode.Pong:
				if (messageHeader.PayloadLength > 125L || !messageHeader.Fin)
				{
					flag3 = true;
					goto IL_01CD;
				}
				goto IL_01CD;
			}
			flag3 = true;
			IL_01CD:
			resultHeader = messageHeader;
			return !flag3;
		}

		// Token: 0x06003067 RID: 12391 RVA: 0x000ABA38 File Offset: 0x000A9C38
		private async Task CloseAsyncPrivate(WebSocketCloseStatus closeStatus, string statusDescription, CancellationToken cancellationToken)
		{
			if (!this._sentCloseFrame)
			{
				await this.SendCloseFrameAsync(closeStatus, statusDescription, cancellationToken).ConfigureAwait(false);
			}
			byte[] closeBuffer = ArrayPool<byte>.Shared.Rent(139);
			object obj;
			try
			{
				while (!this._receivedCloseFrame)
				{
					obj = this.ReceiveAsyncLock;
					Task task;
					lock (obj)
					{
						if (this._receivedCloseFrame)
						{
							break;
						}
						task = this._lastReceiveAsync;
						task = (this._lastReceiveAsync = this.ValidateAndReceiveAsync(task, closeBuffer, cancellationToken));
					}
					await task.ConfigureAwait(false);
				}
			}
			finally
			{
				ArrayPool<byte>.Shared.Return(closeBuffer, false);
			}
			obj = this.StateUpdateLock;
			lock (obj)
			{
				this.DisposeCore();
				if (this._state < WebSocketState.Closed)
				{
					this._state = WebSocketState.Closed;
				}
			}
		}

		// Token: 0x06003068 RID: 12392 RVA: 0x000ABA94 File Offset: 0x000A9C94
		private async Task SendCloseFrameAsync(WebSocketCloseStatus closeStatus, string closeStatusDescription, CancellationToken cancellationToken)
		{
			byte[] buffer = null;
			try
			{
				int num = 2;
				if (string.IsNullOrEmpty(closeStatusDescription))
				{
					buffer = ArrayPool<byte>.Shared.Rent(num);
				}
				else
				{
					num += ManagedWebSocket.s_textEncoding.GetByteCount(closeStatusDescription);
					buffer = ArrayPool<byte>.Shared.Rent(num);
					ManagedWebSocket.s_textEncoding.GetBytes(closeStatusDescription, 0, closeStatusDescription.Length, buffer, 2);
				}
				ushort num2 = (ushort)closeStatus;
				buffer[0] = (byte)(num2 >> 8);
				buffer[1] = (byte)(num2 & 255);
				await this.SendFrameAsync(ManagedWebSocket.MessageOpcode.Close, true, new Memory<byte>(buffer, 0, num), cancellationToken).ConfigureAwait(false);
			}
			finally
			{
				if (buffer != null)
				{
					ArrayPool<byte>.Shared.Return(buffer, false);
				}
			}
			object stateUpdateLock = this.StateUpdateLock;
			lock (stateUpdateLock)
			{
				this._sentCloseFrame = true;
				if (this._state <= WebSocketState.CloseReceived)
				{
					this._state = WebSocketState.CloseSent;
				}
			}
			if (!this._isServer && this._receivedCloseFrame)
			{
				await this.WaitForServerToCloseConnectionAsync(cancellationToken).ConfigureAwait(false);
			}
		}

		// Token: 0x06003069 RID: 12393 RVA: 0x000ABAEF File Offset: 0x000A9CEF
		private void ConsumeFromBuffer(int count)
		{
			this._receiveBufferCount -= count;
			this._receiveBufferOffset += count;
		}

		// Token: 0x0600306A RID: 12394 RVA: 0x000ABB10 File Offset: 0x000A9D10
		private async Task EnsureBufferContainsAsync(int minimumRequiredBytes, CancellationToken cancellationToken, bool throwOnPrematureClosure = true)
		{
			if (this._receiveBufferCount < minimumRequiredBytes)
			{
				if (this._receiveBufferCount > 0)
				{
					this._receiveBuffer.Span.Slice(this._receiveBufferOffset, this._receiveBufferCount).CopyTo(this._receiveBuffer.Span);
				}
				this._receiveBufferOffset = 0;
				while (this._receiveBufferCount < minimumRequiredBytes)
				{
					int num = await this._stream.ReadAsync(this._receiveBuffer.Slice(this._receiveBufferCount, this._receiveBuffer.Length - this._receiveBufferCount), cancellationToken).ConfigureAwait(false);
					if (num <= 0)
					{
						this.ThrowIfEOFUnexpected(throwOnPrematureClosure);
						break;
					}
					this._receiveBufferCount += num;
				}
			}
		}

		// Token: 0x0600306B RID: 12395 RVA: 0x000ABB6B File Offset: 0x000A9D6B
		private void ThrowIfEOFUnexpected(bool throwOnPrematureClosure)
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException("WebSocket");
			}
			if (throwOnPrematureClosure)
			{
				throw new WebSocketException(WebSocketError.ConnectionClosedPrematurely);
			}
		}

		// Token: 0x0600306C RID: 12396 RVA: 0x000ABB8A File Offset: 0x000A9D8A
		private void AllocateSendBuffer(int minLength)
		{
			this._sendBuffer = ArrayPool<byte>.Shared.Rent(minLength);
		}

		// Token: 0x0600306D RID: 12397 RVA: 0x000ABBA0 File Offset: 0x000A9DA0
		private void ReleaseSendBuffer()
		{
			byte[] sendBuffer = this._sendBuffer;
			if (sendBuffer != null)
			{
				this._sendBuffer = null;
				ArrayPool<byte>.Shared.Return(sendBuffer, false);
			}
		}

		// Token: 0x0600306E RID: 12398 RVA: 0x000ABBCA File Offset: 0x000A9DCA
		private static int CombineMaskBytes(Span<byte> buffer, int maskOffset)
		{
			return BitConverter.ToInt32(buffer.Slice(maskOffset));
		}

		// Token: 0x0600306F RID: 12399 RVA: 0x000ABBDE File Offset: 0x000A9DDE
		private static int ApplyMask(Span<byte> toMask, byte[] mask, int maskOffset, int maskOffsetIndex)
		{
			return ManagedWebSocket.ApplyMask(toMask, ManagedWebSocket.CombineMaskBytes(mask, maskOffset), maskOffsetIndex);
		}

		// Token: 0x06003070 RID: 12400 RVA: 0x000ABBF4 File Offset: 0x000A9DF4
		private unsafe static int ApplyMask(Span<byte> toMask, int mask, int maskIndex)
		{
			int num = maskIndex * 8;
			int num2 = (int)(((uint)mask >> num) | (uint)((uint)mask << 32 - num));
			int i = toMask.Length;
			if (i > 0)
			{
				fixed (byte* reference = MemoryMarshal.GetReference<byte>(toMask))
				{
					byte* ptr = reference;
					if (ptr % 4L == null)
					{
						while (i >= 4)
						{
							i -= 4;
							*(int*)ptr ^= num2;
							ptr += 4;
						}
					}
					if (i > 0)
					{
						byte* ptr2 = (byte*)(&mask);
						byte* ptr3 = ptr + i;
						while (ptr < ptr3)
						{
							byte* ptr4 = ptr++;
							*ptr4 ^= ptr2[maskIndex];
							maskIndex = (maskIndex + 1) & 3;
						}
					}
				}
			}
			return maskIndex;
		}

		// Token: 0x06003071 RID: 12401 RVA: 0x000ABC7F File Offset: 0x000A9E7F
		private void ThrowIfOperationInProgress(bool operationCompleted, [CallerMemberName] string methodName = null)
		{
			if (!operationCompleted)
			{
				this.Abort();
				this.ThrowOperationInProgress(methodName);
			}
		}

		// Token: 0x06003072 RID: 12402 RVA: 0x000ABC91 File Offset: 0x000A9E91
		private void ThrowOperationInProgress(string methodName)
		{
			throw new InvalidOperationException(SR.Format("There is already one outstanding '{0}' call for this WebSocket instance. ReceiveAsync and SendAsync can be called simultaneously, but at most one outstanding operation for each of them is allowed at the same time.", methodName));
		}

		// Token: 0x06003073 RID: 12403 RVA: 0x000ABCA3 File Offset: 0x000A9EA3
		private static Exception CreateOperationCanceledException(Exception innerException, CancellationToken cancellationToken = default(CancellationToken))
		{
			return new OperationCanceledException(new OperationCanceledException().Message, innerException, cancellationToken);
		}

		// Token: 0x06003074 RID: 12404 RVA: 0x000ABCB8 File Offset: 0x000A9EB8
		private unsafe static bool TryValidateUtf8(Span<byte> span, bool endOfMessage, ManagedWebSocket.Utf8MessageState state)
		{
			int i = 0;
			while (i < span.Length)
			{
				if (!state.SequenceInProgress)
				{
					state.SequenceInProgress = true;
					byte b = *span[i];
					i++;
					if ((b & 128) == 0)
					{
						state.AdditionalBytesExpected = 0;
						state.CurrentDecodeBits = (int)(b & 127);
						state.ExpectedValueMin = 0;
					}
					else
					{
						if ((b & 192) == 128)
						{
							return false;
						}
						if ((b & 224) == 192)
						{
							state.AdditionalBytesExpected = 1;
							state.CurrentDecodeBits = (int)(b & 31);
							state.ExpectedValueMin = 128;
						}
						else if ((b & 240) == 224)
						{
							state.AdditionalBytesExpected = 2;
							state.CurrentDecodeBits = (int)(b & 15);
							state.ExpectedValueMin = 2048;
						}
						else
						{
							if ((b & 248) != 240)
							{
								return false;
							}
							state.AdditionalBytesExpected = 3;
							state.CurrentDecodeBits = (int)(b & 7);
							state.ExpectedValueMin = 65536;
						}
					}
				}
				while (state.AdditionalBytesExpected > 0 && i < span.Length)
				{
					byte b2 = *span[i];
					if ((b2 & 192) != 128)
					{
						return false;
					}
					i++;
					state.AdditionalBytesExpected--;
					state.CurrentDecodeBits = (state.CurrentDecodeBits << 6) | (int)(b2 & 63);
					if (state.AdditionalBytesExpected == 1 && state.CurrentDecodeBits >= 864 && state.CurrentDecodeBits <= 895)
					{
						return false;
					}
					if (state.AdditionalBytesExpected == 2 && state.CurrentDecodeBits >= 272)
					{
						return false;
					}
				}
				if (state.AdditionalBytesExpected == 0)
				{
					state.SequenceInProgress = false;
					if (state.CurrentDecodeBits < state.ExpectedValueMin)
					{
						return false;
					}
				}
			}
			return !endOfMessage || !state.SequenceInProgress;
		}

		// Token: 0x06003075 RID: 12405 RVA: 0x000ABE7C File Offset: 0x000AA07C
		private Task ValidateAndReceiveAsync(Task receiveTask, byte[] buffer, CancellationToken cancellationToken)
		{
			if (receiveTask != null)
			{
				if (receiveTask.Status != TaskStatus.RanToCompletion)
				{
					return receiveTask;
				}
				Task<WebSocketReceiveResult> task = receiveTask as Task<WebSocketReceiveResult>;
				if (task != null && task.Result.MessageType == WebSocketMessageType.Close)
				{
					return receiveTask;
				}
			}
			receiveTask = this.ReceiveAsyncPrivate<ManagedWebSocket.WebSocketReceiveResultGetter, WebSocketReceiveResult>(new ArraySegment<byte>(buffer), cancellationToken, default(ManagedWebSocket.WebSocketReceiveResultGetter)).AsTask();
			return receiveTask;
		}

		// Token: 0x04001CF8 RID: 7416
		private static readonly RandomNumberGenerator s_random = RandomNumberGenerator.Create();

		// Token: 0x04001CF9 RID: 7417
		private static readonly UTF8Encoding s_textEncoding = new UTF8Encoding(false, true);

		// Token: 0x04001CFA RID: 7418
		private static readonly WebSocketState[] s_validSendStates = new WebSocketState[]
		{
			WebSocketState.Open,
			WebSocketState.CloseReceived
		};

		// Token: 0x04001CFB RID: 7419
		private static readonly WebSocketState[] s_validReceiveStates = new WebSocketState[]
		{
			WebSocketState.Open,
			WebSocketState.CloseSent
		};

		// Token: 0x04001CFC RID: 7420
		private static readonly WebSocketState[] s_validCloseOutputStates = new WebSocketState[]
		{
			WebSocketState.Open,
			WebSocketState.CloseReceived
		};

		// Token: 0x04001CFD RID: 7421
		private static readonly WebSocketState[] s_validCloseStates = new WebSocketState[]
		{
			WebSocketState.Open,
			WebSocketState.CloseReceived,
			WebSocketState.CloseSent
		};

		// Token: 0x04001CFE RID: 7422
		private static readonly Task<WebSocketReceiveResult> s_cachedCloseTask = Task.FromResult<WebSocketReceiveResult>(new WebSocketReceiveResult(0, WebSocketMessageType.Close, true));

		// Token: 0x04001CFF RID: 7423
		internal const int MaxMessageHeaderLength = 14;

		// Token: 0x04001D00 RID: 7424
		private const int MaxControlPayloadLength = 125;

		// Token: 0x04001D01 RID: 7425
		private const int MaskLength = 4;

		// Token: 0x04001D02 RID: 7426
		private readonly Stream _stream;

		// Token: 0x04001D03 RID: 7427
		private readonly bool _isServer;

		// Token: 0x04001D04 RID: 7428
		private readonly string _subprotocol;

		// Token: 0x04001D05 RID: 7429
		private readonly Timer _keepAliveTimer;

		// Token: 0x04001D06 RID: 7430
		private readonly CancellationTokenSource _abortSource = new CancellationTokenSource();

		// Token: 0x04001D07 RID: 7431
		private Memory<byte> _receiveBuffer;

		// Token: 0x04001D08 RID: 7432
		private readonly ManagedWebSocket.Utf8MessageState _utf8TextState = new ManagedWebSocket.Utf8MessageState();

		// Token: 0x04001D09 RID: 7433
		private readonly SemaphoreSlim _sendFrameAsyncLock = new SemaphoreSlim(1, 1);

		// Token: 0x04001D0A RID: 7434
		private WebSocketState _state = WebSocketState.Open;

		// Token: 0x04001D0B RID: 7435
		private bool _disposed;

		// Token: 0x04001D0C RID: 7436
		private bool _sentCloseFrame;

		// Token: 0x04001D0D RID: 7437
		private bool _receivedCloseFrame;

		// Token: 0x04001D0E RID: 7438
		private WebSocketCloseStatus? _closeStatus;

		// Token: 0x04001D0F RID: 7439
		private string _closeStatusDescription;

		// Token: 0x04001D10 RID: 7440
		private ManagedWebSocket.MessageHeader _lastReceiveHeader = new ManagedWebSocket.MessageHeader
		{
			Opcode = ManagedWebSocket.MessageOpcode.Text,
			Fin = true
		};

		// Token: 0x04001D11 RID: 7441
		private int _receiveBufferOffset;

		// Token: 0x04001D12 RID: 7442
		private int _receiveBufferCount;

		// Token: 0x04001D13 RID: 7443
		private int _receivedMaskOffsetOffset;

		// Token: 0x04001D14 RID: 7444
		private byte[] _sendBuffer;

		// Token: 0x04001D15 RID: 7445
		private bool _lastSendWasFragment;

		// Token: 0x04001D16 RID: 7446
		private Task _lastReceiveAsync = Task.CompletedTask;

		// Token: 0x020005D8 RID: 1496
		private sealed class Utf8MessageState
		{
			// Token: 0x04001D17 RID: 7447
			internal bool SequenceInProgress;

			// Token: 0x04001D18 RID: 7448
			internal int AdditionalBytesExpected;

			// Token: 0x04001D19 RID: 7449
			internal int ExpectedValueMin;

			// Token: 0x04001D1A RID: 7450
			internal int CurrentDecodeBits;
		}

		// Token: 0x020005D9 RID: 1497
		private enum MessageOpcode : byte
		{
			// Token: 0x04001D1C RID: 7452
			Continuation,
			// Token: 0x04001D1D RID: 7453
			Text,
			// Token: 0x04001D1E RID: 7454
			Binary,
			// Token: 0x04001D1F RID: 7455
			Close = 8,
			// Token: 0x04001D20 RID: 7456
			Ping,
			// Token: 0x04001D21 RID: 7457
			Pong
		}

		// Token: 0x020005DA RID: 1498
		[StructLayout(LayoutKind.Auto)]
		private struct MessageHeader
		{
			// Token: 0x04001D22 RID: 7458
			internal ManagedWebSocket.MessageOpcode Opcode;

			// Token: 0x04001D23 RID: 7459
			internal bool Fin;

			// Token: 0x04001D24 RID: 7460
			internal long PayloadLength;

			// Token: 0x04001D25 RID: 7461
			internal int Mask;
		}

		// Token: 0x020005DB RID: 1499
		private interface IWebSocketReceiveResultGetter<TResult>
		{
			// Token: 0x06003078 RID: 12408
			TResult GetResult(int count, WebSocketMessageType messageType, bool endOfMessage, WebSocketCloseStatus? closeStatus, string closeDescription);
		}

		// Token: 0x020005DC RID: 1500
		private readonly struct WebSocketReceiveResultGetter : ManagedWebSocket.IWebSocketReceiveResultGetter<WebSocketReceiveResult>
		{
			// Token: 0x06003079 RID: 12409 RVA: 0x000ABF58 File Offset: 0x000AA158
			public WebSocketReceiveResult GetResult(int count, WebSocketMessageType messageType, bool endOfMessage, WebSocketCloseStatus? closeStatus, string closeDescription)
			{
				return new WebSocketReceiveResult(count, messageType, endOfMessage, closeStatus, closeDescription);
			}
		}
	}
}
