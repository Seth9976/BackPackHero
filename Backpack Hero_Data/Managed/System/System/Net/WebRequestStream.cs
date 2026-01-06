using System;
using System.IO;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net
{
	// Token: 0x020004DE RID: 1246
	internal class WebRequestStream : WebConnectionStream
	{
		// Token: 0x0600287F RID: 10367 RVA: 0x00096848 File Offset: 0x00094A48
		public WebRequestStream(WebConnection connection, WebOperation operation, Stream stream, WebConnectionTunnel tunnel)
			: base(connection, operation)
		{
			this.InnerStream = stream;
			this.allowBuffering = operation.Request.InternalAllowBuffering;
			this.sendChunked = operation.Request.SendChunked && operation.WriteBuffer == null;
			if (!this.sendChunked && this.allowBuffering && operation.WriteBuffer == null)
			{
				this.writeBuffer = new MemoryStream();
			}
			this.KeepAlive = base.Request.KeepAlive;
			if (((tunnel != null) ? tunnel.ProxyVersion : null) != null && ((tunnel != null) ? tunnel.ProxyVersion : null) != HttpVersion.Version11)
			{
				this.KeepAlive = false;
			}
		}

		// Token: 0x17000889 RID: 2185
		// (get) Token: 0x06002880 RID: 10368 RVA: 0x000968FF File Offset: 0x00094AFF
		internal Stream InnerStream { get; }

		// Token: 0x1700088A RID: 2186
		// (get) Token: 0x06002881 RID: 10369 RVA: 0x00096907 File Offset: 0x00094B07
		public bool KeepAlive { get; }

		// Token: 0x1700088B RID: 2187
		// (get) Token: 0x06002882 RID: 10370 RVA: 0x00003062 File Offset: 0x00001262
		public override bool CanRead
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700088C RID: 2188
		// (get) Token: 0x06002883 RID: 10371 RVA: 0x0000390E File Offset: 0x00001B0E
		public override bool CanWrite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700088D RID: 2189
		// (get) Token: 0x06002884 RID: 10372 RVA: 0x0009690F File Offset: 0x00094B0F
		// (set) Token: 0x06002885 RID: 10373 RVA: 0x00096917 File Offset: 0x00094B17
		internal bool SendChunked
		{
			get
			{
				return this.sendChunked;
			}
			set
			{
				this.sendChunked = value;
			}
		}

		// Token: 0x1700088E RID: 2190
		// (get) Token: 0x06002886 RID: 10374 RVA: 0x00096920 File Offset: 0x00094B20
		internal bool HasWriteBuffer
		{
			get
			{
				return base.Operation.WriteBuffer != null || this.writeBuffer != null;
			}
		}

		// Token: 0x1700088F RID: 2191
		// (get) Token: 0x06002887 RID: 10375 RVA: 0x0009693A File Offset: 0x00094B3A
		internal int WriteBufferLength
		{
			get
			{
				if (base.Operation.WriteBuffer != null)
				{
					return base.Operation.WriteBuffer.Size;
				}
				if (this.writeBuffer != null)
				{
					return (int)this.writeBuffer.Length;
				}
				return -1;
			}
		}

		// Token: 0x06002888 RID: 10376 RVA: 0x00096970 File Offset: 0x00094B70
		internal BufferOffsetSize GetWriteBuffer()
		{
			if (base.Operation.WriteBuffer != null)
			{
				return base.Operation.WriteBuffer;
			}
			if (this.writeBuffer == null || this.writeBuffer.Length == 0L)
			{
				return null;
			}
			return new BufferOffsetSize(this.writeBuffer.GetBuffer(), 0, (int)this.writeBuffer.Length, false);
		}

		// Token: 0x06002889 RID: 10377 RVA: 0x000969CC File Offset: 0x00094BCC
		private async Task FinishWriting(CancellationToken cancellationToken)
		{
			if (Interlocked.CompareExchange(ref this.completeRequestWritten, 1, 0) == 0)
			{
				try
				{
					base.Operation.ThrowIfClosedOrDisposed(cancellationToken);
					if (this.sendChunked)
					{
						await this.WriteChunkTrailer_inner(cancellationToken).ConfigureAwait(false);
					}
				}
				catch (Exception ex)
				{
					base.Operation.CompleteRequestWritten(this, ex);
					throw;
				}
				finally
				{
				}
				base.Operation.CompleteRequestWritten(this, null);
			}
		}

		// Token: 0x0600288A RID: 10378 RVA: 0x00096A18 File Offset: 0x00094C18
		public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			int num = buffer.Length;
			if (offset < 0 || num < offset)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (count < 0 || num - offset < count)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCanceled(cancellationToken);
			}
			base.Operation.ThrowIfClosedOrDisposed(cancellationToken);
			if (base.Operation.WriteBuffer != null)
			{
				throw new InvalidOperationException();
			}
			WebCompletionSource webCompletionSource = new WebCompletionSource();
			if (Interlocked.CompareExchange<WebCompletionSource>(ref this.pendingWrite, webCompletionSource, null) != null)
			{
				throw new InvalidOperationException(SR.GetString("Cannot re-call BeginGetRequestStream/BeginGetResponse while a previous call is still in progress."));
			}
			return this.WriteAsyncInner(buffer, offset, count, webCompletionSource, cancellationToken);
		}

		// Token: 0x0600288B RID: 10379 RVA: 0x00096AC4 File Offset: 0x00094CC4
		private async Task WriteAsyncInner(byte[] buffer, int offset, int size, WebCompletionSource completion, CancellationToken cancellationToken)
		{
			try
			{
				await this.ProcessWrite(buffer, offset, size, cancellationToken).ConfigureAwait(false);
				if (base.Request.ContentLength > 0L && this.totalWritten == base.Request.ContentLength)
				{
					await this.FinishWriting(cancellationToken);
				}
				this.pendingWrite = null;
				completion.TrySetCompleted();
			}
			catch (Exception ex)
			{
				this.KillBuffer();
				this.closed = true;
				ExceptionDispatchInfo exceptionDispatchInfo = base.Operation.CheckDisposed(cancellationToken);
				if (exceptionDispatchInfo != null)
				{
					ex = exceptionDispatchInfo.SourceException;
				}
				else if (ex is SocketException)
				{
					ex = new IOException("Error writing request", ex);
				}
				base.Operation.CompleteRequestWritten(this, ex);
				this.pendingWrite = null;
				completion.TrySetException(ex);
				if (exceptionDispatchInfo != null)
				{
					exceptionDispatchInfo.Throw();
				}
				throw;
			}
		}

		// Token: 0x0600288C RID: 10380 RVA: 0x00096B34 File Offset: 0x00094D34
		private async Task ProcessWrite(byte[] buffer, int offset, int size, CancellationToken cancellationToken)
		{
			base.Operation.ThrowIfClosedOrDisposed(cancellationToken);
			if (this.sendChunked)
			{
				this.requestWritten = true;
				string text = string.Format("{0:X}\r\n", size);
				byte[] bytes = Encoding.ASCII.GetBytes(text);
				int num = 2 + size + bytes.Length;
				byte[] array = new byte[num];
				Buffer.BlockCopy(bytes, 0, array, 0, bytes.Length);
				Buffer.BlockCopy(buffer, offset, array, bytes.Length, size);
				Buffer.BlockCopy(WebRequestStream.crlf, 0, array, bytes.Length + size, WebRequestStream.crlf.Length);
				if (this.allowBuffering)
				{
					if (this.writeBuffer == null)
					{
						this.writeBuffer = new MemoryStream();
					}
					this.writeBuffer.Write(buffer, offset, size);
				}
				this.totalWritten += (long)size;
				buffer = array;
				offset = 0;
				size = num;
			}
			else
			{
				this.CheckWriteOverflow(base.Request.ContentLength, this.totalWritten, (long)size);
				if (this.allowBuffering)
				{
					if (this.writeBuffer == null)
					{
						this.writeBuffer = new MemoryStream();
					}
					this.writeBuffer.Write(buffer, offset, size);
					this.totalWritten += (long)size;
					if (base.Request.ContentLength <= 0L || this.totalWritten < base.Request.ContentLength)
					{
						return;
					}
					this.requestWritten = true;
					buffer = this.writeBuffer.GetBuffer();
					offset = 0;
					size = (int)this.totalWritten;
				}
				else
				{
					this.totalWritten += (long)size;
				}
			}
			await this.InnerStream.WriteAsync(buffer, offset, size, cancellationToken).ConfigureAwait(false);
		}

		// Token: 0x0600288D RID: 10381 RVA: 0x00096B98 File Offset: 0x00094D98
		private void CheckWriteOverflow(long contentLength, long totalWritten, long size)
		{
			if (contentLength == -1L)
			{
				return;
			}
			long num = contentLength - totalWritten;
			if (size > num)
			{
				this.KillBuffer();
				this.closed = true;
				ProtocolViolationException ex = new ProtocolViolationException("The number of bytes to be written is greater than the specified ContentLength.");
				base.Operation.CompleteRequestWritten(this, ex);
				throw ex;
			}
		}

		// Token: 0x0600288E RID: 10382 RVA: 0x00096BDC File Offset: 0x00094DDC
		internal async Task Initialize(CancellationToken cancellationToken)
		{
			base.Operation.ThrowIfClosedOrDisposed(cancellationToken);
			if (base.Operation.WriteBuffer != null)
			{
				if (base.Operation.IsNtlmChallenge)
				{
					base.Request.InternalContentLength = 0L;
				}
				else
				{
					base.Request.InternalContentLength = (long)base.Operation.WriteBuffer.Size;
				}
			}
			await this.SetHeadersAsync(false, cancellationToken).ConfigureAwait(false);
			base.Operation.ThrowIfClosedOrDisposed(cancellationToken);
			if (base.Operation.WriteBuffer != null && !base.Operation.IsNtlmChallenge)
			{
				await this.WriteRequestAsync(cancellationToken);
				this.Close();
			}
		}

		// Token: 0x0600288F RID: 10383 RVA: 0x00096C28 File Offset: 0x00094E28
		private async Task SetHeadersAsync(bool setInternalLength, CancellationToken cancellationToken)
		{
			base.Operation.ThrowIfClosedOrDisposed(cancellationToken);
			if (!this.headersSent)
			{
				string method = base.Request.Method;
				bool flag = method == "GET" || method == "CONNECT" || method == "HEAD" || method == "TRACE";
				bool flag2 = method == "PROPFIND" || method == "PROPPATCH" || method == "MKCOL" || method == "COPY" || method == "MOVE" || method == "LOCK" || method == "UNLOCK";
				if (base.Operation.IsNtlmChallenge)
				{
					flag = true;
				}
				if (setInternalLength && !flag && this.HasWriteBuffer)
				{
					base.Request.InternalContentLength = (long)this.WriteBufferLength;
				}
				bool flag3 = !flag && (!this.HasWriteBuffer || base.Request.ContentLength > -1L);
				if (this.sendChunked || flag3 || flag || flag2)
				{
					this.headersSent = true;
					this.headers = base.Request.GetRequestHeaders();
					try
					{
						await this.InnerStream.WriteAsync(this.headers, 0, this.headers.Length, cancellationToken).ConfigureAwait(false);
						long contentLength = base.Request.ContentLength;
						if (!this.sendChunked && contentLength == 0L)
						{
							this.requestWritten = true;
						}
					}
					catch (Exception ex)
					{
						if (ex is WebException || ex is OperationCanceledException)
						{
							throw;
						}
						throw new WebException("Error writing headers", WebExceptionStatus.SendFailure, WebExceptionInternalStatus.RequestFatal, ex);
					}
				}
			}
		}

		// Token: 0x06002890 RID: 10384 RVA: 0x00096C7C File Offset: 0x00094E7C
		internal async Task WriteRequestAsync(CancellationToken cancellationToken)
		{
			base.Operation.ThrowIfClosedOrDisposed(cancellationToken);
			if (!this.requestWritten)
			{
				this.requestWritten = true;
				if (!this.sendChunked && this.HasWriteBuffer)
				{
					BufferOffsetSize buffer = this.GetWriteBuffer();
					if (buffer != null && !base.Operation.IsNtlmChallenge && base.Request.ContentLength != -1L && base.Request.ContentLength < (long)buffer.Size)
					{
						this.closed = true;
						WebException ex = new WebException("Specified Content-Length is less than the number of bytes to write", null, WebExceptionStatus.ServerProtocolViolation, null);
						base.Operation.CompleteRequestWritten(this, ex);
						throw ex;
					}
					await this.SetHeadersAsync(true, cancellationToken).ConfigureAwait(false);
					base.Operation.ThrowIfClosedOrDisposed(cancellationToken);
					if (buffer != null && buffer.Size > 0)
					{
						await this.InnerStream.WriteAsync(buffer.Buffer, 0, buffer.Size, cancellationToken);
					}
					await this.FinishWriting(cancellationToken);
				}
			}
		}

		// Token: 0x06002891 RID: 10385 RVA: 0x00096CC8 File Offset: 0x00094EC8
		private async Task WriteChunkTrailer_inner(CancellationToken cancellationToken)
		{
			if (Interlocked.CompareExchange(ref this.chunkTrailerWritten, 1, 0) == 0)
			{
				base.Operation.ThrowIfClosedOrDisposed(cancellationToken);
				byte[] bytes = Encoding.ASCII.GetBytes("0\r\n\r\n");
				await this.InnerStream.WriteAsync(bytes, 0, bytes.Length, cancellationToken).ConfigureAwait(false);
			}
		}

		// Token: 0x06002892 RID: 10386 RVA: 0x00096D14 File Offset: 0x00094F14
		private async Task WriteChunkTrailer()
		{
			CancellationTokenSource cts = new CancellationTokenSource();
			try
			{
				cts.CancelAfter(this.WriteTimeout);
				Task timeoutTask = Task.Delay(this.WriteTimeout, cts.Token);
				ConfiguredTaskAwaitable<Task>.ConfiguredTaskAwaiter configuredTaskAwaiter;
				do
				{
					WebCompletionSource webCompletionSource = new WebCompletionSource();
					WebCompletionSource webCompletionSource2 = Interlocked.CompareExchange<WebCompletionSource>(ref this.pendingWrite, webCompletionSource, null);
					if (webCompletionSource2 == null)
					{
						goto IL_010D;
					}
					Task<object> task = webCompletionSource2.WaitForCompletion();
					configuredTaskAwaiter = Task.WhenAny(new Task[] { timeoutTask, task }).ConfigureAwait(false).GetAwaiter();
					if (!configuredTaskAwaiter.IsCompleted)
					{
						await configuredTaskAwaiter;
						ConfiguredTaskAwaitable<Task>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
						configuredTaskAwaiter = configuredTaskAwaiter2;
						configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<Task>.ConfiguredTaskAwaiter);
					}
				}
				while (configuredTaskAwaiter.GetResult() != timeoutTask);
				throw new WebException("The operation has timed out.", WebExceptionStatus.Timeout);
				IL_010D:
				await this.WriteChunkTrailer_inner(cts.Token).ConfigureAwait(false);
				timeoutTask = null;
			}
			catch
			{
			}
			finally
			{
				this.pendingWrite = null;
				cts.Cancel();
				cts.Dispose();
			}
		}

		// Token: 0x06002893 RID: 10387 RVA: 0x00096D57 File Offset: 0x00094F57
		internal void KillBuffer()
		{
			this.writeBuffer = null;
		}

		// Token: 0x06002894 RID: 10388 RVA: 0x00096D60 File Offset: 0x00094F60
		public override Task<int> ReadAsync(byte[] buffer, int offset, int size, CancellationToken cancellationToken)
		{
			return Task.FromException<int>(new NotSupportedException("The stream does not support reading."));
		}

		// Token: 0x06002895 RID: 10389 RVA: 0x00011E2B File Offset: 0x0001002B
		protected override bool TryReadFromBufferedContent(byte[] buffer, int offset, int count, out int result)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x06002896 RID: 10390 RVA: 0x00096D74 File Offset: 0x00094F74
		protected override void Close_internal(ref bool disposed)
		{
			if (disposed)
			{
				return;
			}
			disposed = true;
			if (this.sendChunked)
			{
				this.WriteChunkTrailer().Wait();
				return;
			}
			if (!this.allowBuffering || this.requestWritten)
			{
				base.Operation.CompleteRequestWritten(this, null);
				return;
			}
			long contentLength = base.Request.ContentLength;
			if (!this.sendChunked && !base.Operation.IsNtlmChallenge && contentLength != -1L && this.totalWritten != contentLength)
			{
				IOException ex = new IOException("Cannot close the stream until all bytes are written");
				this.closed = true;
				disposed = true;
				WebException ex2 = new WebException("Request was cancelled.", WebExceptionStatus.RequestCanceled, WebExceptionInternalStatus.RequestFatal, ex);
				base.Operation.CompleteRequestWritten(this, ex2);
				throw ex2;
			}
			disposed = true;
			base.Operation.CompleteRequestWritten(this, null);
		}

		// Token: 0x0400179D RID: 6045
		private static byte[] crlf = new byte[] { 13, 10 };

		// Token: 0x0400179E RID: 6046
		private MemoryStream writeBuffer;

		// Token: 0x0400179F RID: 6047
		private bool requestWritten;

		// Token: 0x040017A0 RID: 6048
		private bool allowBuffering;

		// Token: 0x040017A1 RID: 6049
		private bool sendChunked;

		// Token: 0x040017A2 RID: 6050
		private WebCompletionSource pendingWrite;

		// Token: 0x040017A3 RID: 6051
		private long totalWritten;

		// Token: 0x040017A4 RID: 6052
		private byte[] headers;

		// Token: 0x040017A5 RID: 6053
		private bool headersSent;

		// Token: 0x040017A6 RID: 6054
		private int completeRequestWritten;

		// Token: 0x040017A7 RID: 6055
		private int chunkTrailerWritten;

		// Token: 0x040017A8 RID: 6056
		internal readonly string ME;
	}
}
