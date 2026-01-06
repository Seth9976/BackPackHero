using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Unity;

namespace System.Net.Sockets
{
	/// <summary>Represents an asynchronous socket operation.</summary>
	// Token: 0x020005CE RID: 1486
	public class SocketAsyncEventArgs : EventArgs, IDisposable
	{
		/// <summary>Gets the exception in the case of a connection failure when a <see cref="T:System.Net.DnsEndPoint" /> was used.</summary>
		/// <returns>An <see cref="T:System.Exception" /> that indicates the cause of the connection error when a <see cref="T:System.Net.DnsEndPoint" /> was specified for the <see cref="P:System.Net.Sockets.SocketAsyncEventArgs.RemoteEndPoint" /> property.</returns>
		// Token: 0x17000B27 RID: 2855
		// (get) Token: 0x06002FCD RID: 12237 RVA: 0x000A9D8F File Offset: 0x000A7F8F
		// (set) Token: 0x06002FCE RID: 12238 RVA: 0x000A9D97 File Offset: 0x000A7F97
		public Exception ConnectByNameError { get; private set; }

		/// <summary>Gets or sets the socket to use or the socket created for accepting a connection with an asynchronous socket method.</summary>
		/// <returns>The <see cref="T:System.Net.Sockets.Socket" /> to use or the socket created for accepting a connection with an asynchronous socket method.</returns>
		// Token: 0x17000B28 RID: 2856
		// (get) Token: 0x06002FCF RID: 12239 RVA: 0x000A9DA0 File Offset: 0x000A7FA0
		// (set) Token: 0x06002FD0 RID: 12240 RVA: 0x000A9DA8 File Offset: 0x000A7FA8
		public Socket AcceptSocket { get; set; }

		/// <summary>Gets the number of bytes transferred in the socket operation.</summary>
		/// <returns>An <see cref="T:System.Int32" /> that contains the number of bytes transferred in the socket operation.</returns>
		// Token: 0x17000B29 RID: 2857
		// (get) Token: 0x06002FD1 RID: 12241 RVA: 0x000A9DB1 File Offset: 0x000A7FB1
		// (set) Token: 0x06002FD2 RID: 12242 RVA: 0x000A9DB9 File Offset: 0x000A7FB9
		public int BytesTransferred { get; private set; }

		/// <summary>Gets or sets a value that specifies if socket can be reused after a disconnect operation.</summary>
		/// <returns>A <see cref="T:System.Boolean" /> that specifies if socket can be reused after a disconnect operation.</returns>
		// Token: 0x17000B2A RID: 2858
		// (get) Token: 0x06002FD3 RID: 12243 RVA: 0x000A9DC2 File Offset: 0x000A7FC2
		// (set) Token: 0x06002FD4 RID: 12244 RVA: 0x000A9DCA File Offset: 0x000A7FCA
		public bool DisconnectReuseSocket { get; set; }

		/// <summary>Gets the type of socket operation most recently performed with this context object.</summary>
		/// <returns>A <see cref="T:System.Net.Sockets.SocketAsyncOperation" /> instance that indicates the type of socket operation most recently performed with this context object.</returns>
		// Token: 0x17000B2B RID: 2859
		// (get) Token: 0x06002FD5 RID: 12245 RVA: 0x000A9DD3 File Offset: 0x000A7FD3
		// (set) Token: 0x06002FD6 RID: 12246 RVA: 0x000A9DDB File Offset: 0x000A7FDB
		public SocketAsyncOperation LastOperation { get; private set; }

		/// <summary>Gets or sets the remote IP endpoint for an asynchronous operation.</summary>
		/// <returns>An <see cref="T:System.Net.EndPoint" /> that represents the remote IP endpoint for an asynchronous operation.</returns>
		// Token: 0x17000B2C RID: 2860
		// (get) Token: 0x06002FD7 RID: 12247 RVA: 0x000A9DE4 File Offset: 0x000A7FE4
		// (set) Token: 0x06002FD8 RID: 12248 RVA: 0x000A9DEC File Offset: 0x000A7FEC
		public EndPoint RemoteEndPoint
		{
			get
			{
				return this.remote_ep;
			}
			set
			{
				this.remote_ep = value;
			}
		}

		/// <summary>Gets the IP address and interface of a received packet.</summary>
		/// <returns>An <see cref="T:System.Net.Sockets.IPPacketInformation" /> instance that contains the destination IP address and interface of a received packet.</returns>
		// Token: 0x17000B2D RID: 2861
		// (get) Token: 0x06002FD9 RID: 12249 RVA: 0x000A9DF5 File Offset: 0x000A7FF5
		// (set) Token: 0x06002FDA RID: 12250 RVA: 0x000A9DFD File Offset: 0x000A7FFD
		public IPPacketInformation ReceiveMessageFromPacketInfo { get; private set; }

		/// <summary>Gets or sets an array of buffers to be sent for an asynchronous operation used by the <see cref="M:System.Net.Sockets.Socket.SendPacketsAsync(System.Net.Sockets.SocketAsyncEventArgs)" /> method.</summary>
		/// <returns>An array of <see cref="T:System.Net.Sockets.SendPacketsElement" /> objects that represent an array of buffers to be sent.</returns>
		// Token: 0x17000B2E RID: 2862
		// (get) Token: 0x06002FDB RID: 12251 RVA: 0x000A9E06 File Offset: 0x000A8006
		// (set) Token: 0x06002FDC RID: 12252 RVA: 0x000A9E0E File Offset: 0x000A800E
		public SendPacketsElement[] SendPacketsElements { get; set; }

		/// <summary>Gets or sets a bitwise combination of <see cref="T:System.Net.Sockets.TransmitFileOptions" /> values for an asynchronous operation used by the <see cref="M:System.Net.Sockets.Socket.SendPacketsAsync(System.Net.Sockets.SocketAsyncEventArgs)" /> method.</summary>
		/// <returns>A <see cref="T:System.Net.Sockets.TransmitFileOptions" /> that contains a bitwise combination of values that are used with an asynchronous operation.</returns>
		// Token: 0x17000B2F RID: 2863
		// (get) Token: 0x06002FDD RID: 12253 RVA: 0x000A9E17 File Offset: 0x000A8017
		// (set) Token: 0x06002FDE RID: 12254 RVA: 0x000A9E1F File Offset: 0x000A801F
		public TransmitFileOptions SendPacketsFlags { get; set; }

		/// <summary>Gets or sets the size, in bytes, of the data block used in the send operation.</summary>
		/// <returns>An <see cref="T:System.Int32" /> that contains the size, in bytes, of the data block used in the send operation.</returns>
		// Token: 0x17000B30 RID: 2864
		// (get) Token: 0x06002FDF RID: 12255 RVA: 0x000A9E28 File Offset: 0x000A8028
		// (set) Token: 0x06002FE0 RID: 12256 RVA: 0x000A9E30 File Offset: 0x000A8030
		[MonoTODO("unused property")]
		public int SendPacketsSendSize { get; set; }

		/// <summary>Gets or sets the result of the asynchronous socket operation.</summary>
		/// <returns>A <see cref="T:System.Net.Sockets.SocketError" /> that represents the result of the asynchronous socket operation.</returns>
		// Token: 0x17000B31 RID: 2865
		// (get) Token: 0x06002FE1 RID: 12257 RVA: 0x000A9E39 File Offset: 0x000A8039
		// (set) Token: 0x06002FE2 RID: 12258 RVA: 0x000A9E41 File Offset: 0x000A8041
		public SocketError SocketError { get; set; }

		/// <summary>Gets the results of an asynchronous socket operation or sets the behavior of an asynchronous operation.</summary>
		/// <returns>A <see cref="T:System.Net.Sockets.SocketFlags" /> that represents the results of an asynchronous socket operation.</returns>
		// Token: 0x17000B32 RID: 2866
		// (get) Token: 0x06002FE3 RID: 12259 RVA: 0x000A9E4A File Offset: 0x000A804A
		// (set) Token: 0x06002FE4 RID: 12260 RVA: 0x000A9E52 File Offset: 0x000A8052
		public SocketFlags SocketFlags { get; set; }

		/// <summary>Gets or sets a user or application object associated with this asynchronous socket operation.</summary>
		/// <returns>An object that represents the user or application object associated with this asynchronous socket operation.</returns>
		// Token: 0x17000B33 RID: 2867
		// (get) Token: 0x06002FE5 RID: 12261 RVA: 0x000A9E5B File Offset: 0x000A805B
		// (set) Token: 0x06002FE6 RID: 12262 RVA: 0x000A9E63 File Offset: 0x000A8063
		public object UserToken { get; set; }

		/// <summary>The created and connected <see cref="T:System.Net.Sockets.Socket" /> object after successful completion of the <see cref="Overload:System.Net.Sockets.Socket.ConnectAsync" /> method.</summary>
		/// <returns>The connected <see cref="T:System.Net.Sockets.Socket" /> object.</returns>
		// Token: 0x17000B34 RID: 2868
		// (get) Token: 0x06002FE7 RID: 12263 RVA: 0x000A9E6C File Offset: 0x000A806C
		public Socket ConnectSocket
		{
			get
			{
				if (this.SocketError == SocketError.AccessDenied)
				{
					return null;
				}
				return this.current_socket;
			}
		}

		/// <summary>The event used to complete an asynchronous operation.</summary>
		// Token: 0x1400003C RID: 60
		// (add) Token: 0x06002FE8 RID: 12264 RVA: 0x000A9E84 File Offset: 0x000A8084
		// (remove) Token: 0x06002FE9 RID: 12265 RVA: 0x000A9EBC File Offset: 0x000A80BC
		public event EventHandler<SocketAsyncEventArgs> Completed;

		/// <summary>Creates an empty <see cref="T:System.Net.Sockets.SocketAsyncEventArgs" /> instance.</summary>
		/// <exception cref="T:System.NotSupportedException">The platform is not supported. </exception>
		// Token: 0x06002FEA RID: 12266 RVA: 0x000A9EF1 File Offset: 0x000A80F1
		public SocketAsyncEventArgs()
		{
			this.SendPacketsSendSize = -1;
		}

		// Token: 0x06002FEB RID: 12267 RVA: 0x000A9F0B File Offset: 0x000A810B
		internal SocketAsyncEventArgs(bool flowExecutionContext)
		{
		}

		// Token: 0x06002FEC RID: 12268 RVA: 0x000A9F20 File Offset: 0x000A8120
		~SocketAsyncEventArgs()
		{
			this.Dispose(false);
		}

		// Token: 0x06002FED RID: 12269 RVA: 0x000A9F50 File Offset: 0x000A8150
		private void Dispose(bool disposing)
		{
			this.disposed = true;
			if (disposing)
			{
				int num = this.in_progress;
				return;
			}
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Net.Sockets.SocketAsyncEventArgs" /> instance and optionally disposes of the managed resources.</summary>
		// Token: 0x06002FEE RID: 12270 RVA: 0x000A9F66 File Offset: 0x000A8166
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06002FEF RID: 12271 RVA: 0x000A9F75 File Offset: 0x000A8175
		internal void SetConnectByNameError(Exception error)
		{
			this.ConnectByNameError = error;
		}

		// Token: 0x06002FF0 RID: 12272 RVA: 0x000A9F7E File Offset: 0x000A817E
		internal void SetBytesTransferred(int value)
		{
			this.BytesTransferred = value;
		}

		// Token: 0x17000B35 RID: 2869
		// (get) Token: 0x06002FF1 RID: 12273 RVA: 0x000A9F87 File Offset: 0x000A8187
		internal Socket CurrentSocket
		{
			get
			{
				return this.current_socket;
			}
		}

		// Token: 0x06002FF2 RID: 12274 RVA: 0x000A9F8F File Offset: 0x000A818F
		internal void SetCurrentSocket(Socket socket)
		{
			this.current_socket = socket;
		}

		// Token: 0x06002FF3 RID: 12275 RVA: 0x000A9F98 File Offset: 0x000A8198
		internal void SetLastOperation(SocketAsyncOperation op)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException("System.Net.Sockets.SocketAsyncEventArgs");
			}
			if (Interlocked.Exchange(ref this.in_progress, 1) != 0)
			{
				throw new InvalidOperationException("Operation already in progress");
			}
			this.LastOperation = op;
		}

		// Token: 0x06002FF4 RID: 12276 RVA: 0x000A9FCD File Offset: 0x000A81CD
		internal void Complete_internal()
		{
			this.in_progress = 0;
			this.OnCompleted(this);
		}

		/// <summary>Represents a method that is called when an asynchronous operation completes.</summary>
		/// <param name="e">The event that is signaled.</param>
		// Token: 0x06002FF5 RID: 12277 RVA: 0x000A9FE0 File Offset: 0x000A81E0
		protected virtual void OnCompleted(SocketAsyncEventArgs e)
		{
			if (e == null)
			{
				return;
			}
			EventHandler<SocketAsyncEventArgs> completed = e.Completed;
			if (completed != null)
			{
				completed(e.current_socket, e);
			}
		}

		// Token: 0x06002FF6 RID: 12278 RVA: 0x000A9F8F File Offset: 0x000A818F
		internal void StartOperationCommon(Socket socket)
		{
			this.current_socket = socket;
		}

		// Token: 0x06002FF7 RID: 12279 RVA: 0x000AA008 File Offset: 0x000A8208
		internal void StartOperationWrapperConnect(MultipleConnectAsync args)
		{
			this.SetLastOperation(SocketAsyncOperation.Connect);
		}

		// Token: 0x06002FF8 RID: 12280 RVA: 0x000AA011 File Offset: 0x000A8211
		internal void FinishConnectByNameSyncFailure(Exception exception, int bytesTransferred, SocketFlags flags)
		{
			this.SetResults(exception, bytesTransferred, flags);
			if (this.current_socket != null)
			{
				this.current_socket.is_connected = false;
			}
			this.Complete_internal();
		}

		// Token: 0x06002FF9 RID: 12281 RVA: 0x000AA011 File Offset: 0x000A8211
		internal void FinishOperationAsyncFailure(Exception exception, int bytesTransferred, SocketFlags flags)
		{
			this.SetResults(exception, bytesTransferred, flags);
			if (this.current_socket != null)
			{
				this.current_socket.is_connected = false;
			}
			this.Complete_internal();
		}

		// Token: 0x06002FFA RID: 12282 RVA: 0x000AA036 File Offset: 0x000A8236
		internal void FinishWrapperConnectSuccess(Socket connectSocket, int bytesTransferred, SocketFlags flags)
		{
			this.SetResults(SocketError.Success, bytesTransferred, flags);
			this.current_socket = connectSocket;
			this.Complete_internal();
		}

		// Token: 0x06002FFB RID: 12283 RVA: 0x000AA04E File Offset: 0x000A824E
		internal void SetResults(SocketError socketError, int bytesTransferred, SocketFlags flags)
		{
			this.SocketError = socketError;
			this.ConnectByNameError = null;
			this.BytesTransferred = bytesTransferred;
			this.SocketFlags = flags;
		}

		// Token: 0x06002FFC RID: 12284 RVA: 0x000AA06C File Offset: 0x000A826C
		internal void SetResults(Exception exception, int bytesTransferred, SocketFlags flags)
		{
			this.ConnectByNameError = exception;
			this.BytesTransferred = bytesTransferred;
			this.SocketFlags = flags;
			if (exception == null)
			{
				this.SocketError = SocketError.Success;
				return;
			}
			SocketException ex = exception as SocketException;
			if (ex != null)
			{
				this.SocketError = ex.SocketErrorCode;
				return;
			}
			this.SocketError = SocketError.SocketError;
		}

		/// <summary>Gets the data buffer to use with an asynchronous socket method.</summary>
		/// <returns>A <see cref="T:System.Byte" /> array that represents the data buffer to use with an asynchronous socket method.</returns>
		// Token: 0x17000B36 RID: 2870
		// (get) Token: 0x06002FFD RID: 12285 RVA: 0x000AA0B8 File Offset: 0x000A82B8
		public byte[] Buffer
		{
			get
			{
				if (this._bufferIsExplicitArray)
				{
					ArraySegment<byte> arraySegment;
					MemoryMarshal.TryGetArray<byte>(this._buffer, out arraySegment);
					return arraySegment.Array;
				}
				return null;
			}
		}

		// Token: 0x17000B37 RID: 2871
		// (get) Token: 0x06002FFE RID: 12286 RVA: 0x000AA0E9 File Offset: 0x000A82E9
		public Memory<byte> MemoryBuffer
		{
			get
			{
				return this._buffer;
			}
		}

		/// <summary>Gets the offset, in bytes, into the data buffer referenced by the <see cref="P:System.Net.Sockets.SocketAsyncEventArgs.Buffer" /> property.</summary>
		/// <returns>An <see cref="T:System.Int32" /> that contains the offset, in bytes, into the data buffer referenced by the <see cref="P:System.Net.Sockets.SocketAsyncEventArgs.Buffer" /> property.</returns>
		// Token: 0x17000B38 RID: 2872
		// (get) Token: 0x06002FFF RID: 12287 RVA: 0x000AA0F1 File Offset: 0x000A82F1
		public int Offset
		{
			get
			{
				return this._offset;
			}
		}

		/// <summary>Gets the maximum amount of data, in bytes, to send or receive in an asynchronous operation.</summary>
		/// <returns>An <see cref="T:System.Int32" /> that contains the maximum amount of data, in bytes, to send or receive.</returns>
		// Token: 0x17000B39 RID: 2873
		// (get) Token: 0x06003000 RID: 12288 RVA: 0x000AA0F9 File Offset: 0x000A82F9
		public int Count
		{
			get
			{
				return this._count;
			}
		}

		/// <summary>Gets or sets an array of data buffers to use with an asynchronous socket method.</summary>
		/// <returns>An <see cref="T:System.Collections.IList" /> that represents an array of data buffers to use with an asynchronous socket method.</returns>
		/// <exception cref="T:System.ArgumentException">There are ambiguous buffers specified on a set operation. This exception occurs if the <see cref="P:System.Net.Sockets.SocketAsyncEventArgs.Buffer" /> property has been set to a non-null value and an attempt was made to set the <see cref="P:System.Net.Sockets.SocketAsyncEventArgs.BufferList" /> property to a non-null value.</exception>
		// Token: 0x17000B3A RID: 2874
		// (get) Token: 0x06003001 RID: 12289 RVA: 0x000AA101 File Offset: 0x000A8301
		// (set) Token: 0x06003002 RID: 12290 RVA: 0x000AA10C File Offset: 0x000A830C
		public IList<ArraySegment<byte>> BufferList
		{
			get
			{
				return this._bufferList;
			}
			set
			{
				if (value != null)
				{
					if (!this._buffer.Equals(default(Memory<byte>)))
					{
						throw new ArgumentException(SR.Format("Buffer and BufferList properties cannot both be non-null.", "Buffer"));
					}
					int count = value.Count;
					if (this._bufferListInternal == null)
					{
						this._bufferListInternal = new List<ArraySegment<byte>>(count);
					}
					else
					{
						this._bufferListInternal.Clear();
					}
					for (int i = 0; i < count; i++)
					{
						ArraySegment<byte> arraySegment = value[i];
						RangeValidationHelpers.ValidateSegment(arraySegment);
						this._bufferListInternal.Add(arraySegment);
					}
				}
				else
				{
					List<ArraySegment<byte>> bufferListInternal = this._bufferListInternal;
					if (bufferListInternal != null)
					{
						bufferListInternal.Clear();
					}
				}
				this._bufferList = value;
			}
		}

		/// <summary>Sets the data buffer to use with an asynchronous socket method.</summary>
		/// <param name="offset">The offset, in bytes, in the data buffer where the operation starts.</param>
		/// <param name="count">The maximum amount of data, in bytes, to send or receive in the buffer.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">An argument was out of range. This exception occurs if the <paramref name="offset" /> parameter is less than zero or greater than the length of the array in the <see cref="P:System.Net.Sockets.SocketAsyncEventArgs.Buffer" /> property. This exception also occurs if the <paramref name="count" /> parameter is less than zero or greater than the length of the array in the <see cref="P:System.Net.Sockets.SocketAsyncEventArgs.Buffer" /> property minus the <paramref name="offset" /> parameter.</exception>
		// Token: 0x06003003 RID: 12291 RVA: 0x000AA1B0 File Offset: 0x000A83B0
		public void SetBuffer(int offset, int count)
		{
			if (!this._buffer.Equals(default(Memory<byte>)))
			{
				if ((ulong)offset > (ulong)((long)this._buffer.Length))
				{
					throw new ArgumentOutOfRangeException("offset");
				}
				if ((ulong)count > (ulong)((long)(this._buffer.Length - offset)))
				{
					throw new ArgumentOutOfRangeException("count");
				}
				if (!this._bufferIsExplicitArray)
				{
					throw new InvalidOperationException("This operation may only be performed when the buffer was set using the SetBuffer overload that accepts an array.");
				}
				this._offset = offset;
				this._count = count;
			}
		}

		// Token: 0x06003004 RID: 12292 RVA: 0x000AA22C File Offset: 0x000A842C
		internal void CopyBufferFrom(SocketAsyncEventArgs source)
		{
			this._buffer = source._buffer;
			this._offset = source._offset;
			this._count = source._count;
			this._bufferIsExplicitArray = source._bufferIsExplicitArray;
		}

		/// <summary>Sets the data buffer to use with an asynchronous socket method.</summary>
		/// <param name="buffer">The data buffer to use with an asynchronous socket method.</param>
		/// <param name="offset">The offset, in bytes, in the data buffer where the operation starts.</param>
		/// <param name="count">The maximum amount of data, in bytes, to send or receive in the buffer.</param>
		/// <exception cref="T:System.ArgumentException">There are ambiguous buffers specified. This exception occurs if the <see cref="P:System.Net.Sockets.SocketAsyncEventArgs.Buffer" /> property is also not null and the <see cref="P:System.Net.Sockets.SocketAsyncEventArgs.BufferList" /> property is also not null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">An argument was out of range. This exception occurs if the <paramref name="offset" /> parameter is less than zero or greater than the length of the array in the <see cref="P:System.Net.Sockets.SocketAsyncEventArgs.Buffer" /> property. This exception also occurs if the <paramref name="count" /> parameter is less than zero or greater than the length of the array in the <see cref="P:System.Net.Sockets.SocketAsyncEventArgs.Buffer" /> property minus the <paramref name="offset" /> parameter.</exception>
		// Token: 0x06003005 RID: 12293 RVA: 0x000AA260 File Offset: 0x000A8460
		public void SetBuffer(byte[] buffer, int offset, int count)
		{
			if (buffer == null)
			{
				this._buffer = default(Memory<byte>);
				this._offset = 0;
				this._count = 0;
				this._bufferIsExplicitArray = false;
				return;
			}
			if (this._bufferList != null)
			{
				throw new ArgumentException(SR.Format("Buffer and BufferList properties cannot both be non-null.", "BufferList"));
			}
			if ((ulong)offset > (ulong)((long)buffer.Length))
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if ((ulong)count > (ulong)((long)(buffer.Length - offset)))
			{
				throw new ArgumentOutOfRangeException("count");
			}
			this._buffer = buffer;
			this._offset = offset;
			this._count = count;
			this._bufferIsExplicitArray = true;
		}

		// Token: 0x06003006 RID: 12294 RVA: 0x000AA2F8 File Offset: 0x000A84F8
		public void SetBuffer(Memory<byte> buffer)
		{
			if (buffer.Length != 0 && this._bufferList != null)
			{
				throw new ArgumentException(SR.Format("Buffer and BufferList properties cannot both be non-null.", "BufferList"));
			}
			this._buffer = buffer;
			this._offset = 0;
			this._count = buffer.Length;
			this._bufferIsExplicitArray = false;
		}

		// Token: 0x17000B3B RID: 2875
		// (get) Token: 0x06003007 RID: 12295 RVA: 0x000AA34D File Offset: 0x000A854D
		internal bool HasMultipleBuffers
		{
			get
			{
				return this._bufferList != null;
			}
		}

		/// <summary>Gets or sets the protocol to use to download the socket client access policy file. </summary>
		/// <returns>Returns <see cref="T:System.Net.Sockets.SocketClientAccessPolicyProtocol" />.The protocol to use to download the socket client access policy file.</returns>
		// Token: 0x17000B3C RID: 2876
		// (get) Token: 0x06003008 RID: 12296 RVA: 0x000AA358 File Offset: 0x000A8558
		// (set) Token: 0x06003009 RID: 12297 RVA: 0x00013B26 File Offset: 0x00011D26
		public SocketClientAccessPolicyProtocol SocketClientAccessPolicyProtocol
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return SocketClientAccessPolicyProtocol.Tcp;
			}
			[CompilerGenerated]
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		// Token: 0x04001C9D RID: 7325
		private bool disposed;

		// Token: 0x04001C9E RID: 7326
		internal volatile int in_progress;

		// Token: 0x04001C9F RID: 7327
		private EndPoint remote_ep;

		// Token: 0x04001CA0 RID: 7328
		private Socket current_socket;

		// Token: 0x04001CA1 RID: 7329
		internal SocketAsyncResult socket_async_result = new SocketAsyncResult();

		// Token: 0x04001CAF RID: 7343
		private Memory<byte> _buffer;

		// Token: 0x04001CB0 RID: 7344
		private int _offset;

		// Token: 0x04001CB1 RID: 7345
		private int _count;

		// Token: 0x04001CB2 RID: 7346
		private bool _bufferIsExplicitArray;

		// Token: 0x04001CB3 RID: 7347
		private IList<ArraySegment<byte>> _bufferList;

		// Token: 0x04001CB4 RID: 7348
		private List<ArraySegment<byte>> _bufferListInternal;
	}
}
