using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;

namespace System.Net.Sockets
{
	// Token: 0x020005CF RID: 1487
	[StructLayout(LayoutKind.Sequential)]
	internal sealed class SocketAsyncResult : IOAsyncResult
	{
		// Token: 0x17000B3D RID: 2877
		// (get) Token: 0x0600300A RID: 12298 RVA: 0x000AA373 File Offset: 0x000A8573
		public IntPtr Handle
		{
			get
			{
				if (this.socket == null)
				{
					return IntPtr.Zero;
				}
				return this.socket.Handle;
			}
		}

		// Token: 0x0600300B RID: 12299 RVA: 0x000AA38E File Offset: 0x000A858E
		public SocketAsyncResult()
		{
		}

		// Token: 0x0600300C RID: 12300 RVA: 0x000AA398 File Offset: 0x000A8598
		public void Init(Socket socket, AsyncCallback callback, object state, SocketOperation operation)
		{
			base.Init(callback, state);
			this.socket = socket;
			this.operation = operation;
			this.DelayedException = null;
			this.EndPoint = null;
			this.Buffer = null;
			this.Offset = 0;
			this.Size = 0;
			this.SockFlags = SocketFlags.None;
			this.AcceptSocket = null;
			this.Addresses = null;
			this.Port = 0;
			this.Buffers = null;
			this.ReuseSocket = false;
			this.CurrentAddress = 0;
			this.AcceptedSocket = null;
			this.Total = 0;
			this.error = 0;
			this.EndCalled = 0;
		}

		// Token: 0x0600300D RID: 12301 RVA: 0x000AA431 File Offset: 0x000A8631
		public SocketAsyncResult(Socket socket, AsyncCallback callback, object state, SocketOperation operation)
			: base(callback, state)
		{
			this.socket = socket;
			this.operation = operation;
		}

		// Token: 0x17000B3E RID: 2878
		// (get) Token: 0x0600300E RID: 12302 RVA: 0x000AA44C File Offset: 0x000A864C
		public SocketError ErrorCode
		{
			get
			{
				SocketException ex = this.DelayedException as SocketException;
				if (ex != null)
				{
					return ex.SocketErrorCode;
				}
				if (this.error != 0)
				{
					return (SocketError)this.error;
				}
				return SocketError.Success;
			}
		}

		// Token: 0x0600300F RID: 12303 RVA: 0x000AA47F File Offset: 0x000A867F
		public void CheckIfThrowDelayedException()
		{
			if (this.DelayedException != null)
			{
				this.socket.is_connected = false;
				throw this.DelayedException;
			}
			if (this.error != 0)
			{
				this.socket.is_connected = false;
				throw new SocketException(this.error);
			}
		}

		// Token: 0x06003010 RID: 12304 RVA: 0x000AA4BC File Offset: 0x000A86BC
		internal override void CompleteDisposed()
		{
			this.Complete();
		}

		// Token: 0x06003011 RID: 12305 RVA: 0x000AA4C4 File Offset: 0x000A86C4
		public void Complete()
		{
			if (this.operation != SocketOperation.Receive && this.socket.CleanedUp)
			{
				this.DelayedException = new ObjectDisposedException(this.socket.GetType().ToString());
			}
			base.IsCompleted = true;
			Socket socket = this.socket;
			SocketOperation socketOperation = this.operation;
			if (!base.CompletedSynchronously && base.AsyncCallback != null)
			{
				ThreadPool.UnsafeQueueUserWorkItem(delegate(object state)
				{
					((SocketAsyncResult)state).AsyncCallback((SocketAsyncResult)state);
				}, this);
			}
			switch (socketOperation)
			{
			case SocketOperation.Accept:
			case SocketOperation.Receive:
			case SocketOperation.ReceiveFrom:
			case SocketOperation.ReceiveGeneric:
				socket.ReadSem.Release();
				return;
			case SocketOperation.Connect:
			case SocketOperation.RecvJustCallback:
			case SocketOperation.SendJustCallback:
			case SocketOperation.Disconnect:
			case SocketOperation.AcceptReceive:
				break;
			case SocketOperation.Send:
			case SocketOperation.SendTo:
			case SocketOperation.SendGeneric:
				socket.WriteSem.Release();
				break;
			default:
				return;
			}
		}

		// Token: 0x06003012 RID: 12306 RVA: 0x000AA59D File Offset: 0x000A879D
		public void Complete(bool synch)
		{
			base.CompletedSynchronously = synch;
			this.Complete();
		}

		// Token: 0x06003013 RID: 12307 RVA: 0x000AA5AC File Offset: 0x000A87AC
		public void Complete(int total)
		{
			this.Total = total;
			this.Complete();
		}

		// Token: 0x06003014 RID: 12308 RVA: 0x000AA5BB File Offset: 0x000A87BB
		public void Complete(Exception e, bool synch)
		{
			this.DelayedException = e;
			base.CompletedSynchronously = synch;
			this.Complete();
		}

		// Token: 0x06003015 RID: 12309 RVA: 0x000AA5D1 File Offset: 0x000A87D1
		public void Complete(Exception e)
		{
			this.DelayedException = e;
			this.Complete();
		}

		// Token: 0x06003016 RID: 12310 RVA: 0x000AA5E0 File Offset: 0x000A87E0
		public void Complete(Socket s)
		{
			this.AcceptedSocket = s;
			this.Complete();
		}

		// Token: 0x06003017 RID: 12311 RVA: 0x000AA5EF File Offset: 0x000A87EF
		public void Complete(Socket s, int total)
		{
			this.AcceptedSocket = s;
			this.Total = total;
			this.Complete();
		}

		// Token: 0x04001CB5 RID: 7349
		public Socket socket;

		// Token: 0x04001CB6 RID: 7350
		public SocketOperation operation;

		// Token: 0x04001CB7 RID: 7351
		private Exception DelayedException;

		// Token: 0x04001CB8 RID: 7352
		public EndPoint EndPoint;

		// Token: 0x04001CB9 RID: 7353
		public Memory<byte> Buffer;

		// Token: 0x04001CBA RID: 7354
		public int Offset;

		// Token: 0x04001CBB RID: 7355
		public int Size;

		// Token: 0x04001CBC RID: 7356
		public SocketFlags SockFlags;

		// Token: 0x04001CBD RID: 7357
		public Socket AcceptSocket;

		// Token: 0x04001CBE RID: 7358
		public IPAddress[] Addresses;

		// Token: 0x04001CBF RID: 7359
		public int Port;

		// Token: 0x04001CC0 RID: 7360
		public IList<ArraySegment<byte>> Buffers;

		// Token: 0x04001CC1 RID: 7361
		public bool ReuseSocket;

		// Token: 0x04001CC2 RID: 7362
		public int CurrentAddress;

		// Token: 0x04001CC3 RID: 7363
		public Socket AcceptedSocket;

		// Token: 0x04001CC4 RID: 7364
		public int Total;

		// Token: 0x04001CC5 RID: 7365
		internal int error;

		// Token: 0x04001CC6 RID: 7366
		public int EndCalled;
	}
}
