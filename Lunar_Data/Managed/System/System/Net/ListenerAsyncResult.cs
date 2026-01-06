using System;
using System.Threading;

namespace System.Net
{
	// Token: 0x020004B1 RID: 1201
	internal class ListenerAsyncResult : IAsyncResult
	{
		// Token: 0x060026BE RID: 9918 RVA: 0x0008F880 File Offset: 0x0008DA80
		public ListenerAsyncResult(AsyncCallback cb, object state)
		{
			this.cb = cb;
			this.state = state;
		}

		// Token: 0x060026BF RID: 9919 RVA: 0x0008F8A4 File Offset: 0x0008DAA4
		internal void Complete(Exception exc)
		{
			if (this.forward != null)
			{
				this.forward.Complete(exc);
				return;
			}
			this.exception = exc;
			if (this.InGet && exc is ObjectDisposedException)
			{
				this.exception = new HttpListenerException(500, "Listener closed");
			}
			object obj = this.locker;
			lock (obj)
			{
				this.completed = true;
				if (this.handle != null)
				{
					this.handle.Set();
				}
				if (this.cb != null)
				{
					ThreadPool.UnsafeQueueUserWorkItem(ListenerAsyncResult.InvokeCB, this);
				}
			}
		}

		// Token: 0x060026C0 RID: 9920 RVA: 0x0008F950 File Offset: 0x0008DB50
		private static void InvokeCallback(object o)
		{
			ListenerAsyncResult listenerAsyncResult = (ListenerAsyncResult)o;
			if (listenerAsyncResult.forward != null)
			{
				ListenerAsyncResult.InvokeCallback(listenerAsyncResult.forward);
				return;
			}
			try
			{
				listenerAsyncResult.cb(listenerAsyncResult);
			}
			catch
			{
			}
		}

		// Token: 0x060026C1 RID: 9921 RVA: 0x0008F99C File Offset: 0x0008DB9C
		internal void Complete(HttpListenerContext context)
		{
			this.Complete(context, false);
		}

		// Token: 0x060026C2 RID: 9922 RVA: 0x0008F9A8 File Offset: 0x0008DBA8
		internal void Complete(HttpListenerContext context, bool synch)
		{
			if (this.forward != null)
			{
				this.forward.Complete(context, synch);
				return;
			}
			this.synch = synch;
			this.context = context;
			object obj = this.locker;
			lock (obj)
			{
				AuthenticationSchemes authenticationSchemes = context.Listener.SelectAuthenticationScheme(context);
				if ((authenticationSchemes == AuthenticationSchemes.Basic || context.Listener.AuthenticationSchemes == AuthenticationSchemes.Negotiate) && context.Request.Headers["Authorization"] == null)
				{
					context.Response.StatusCode = 401;
					context.Response.Headers["WWW-Authenticate"] = authenticationSchemes.ToString() + " realm=\"" + context.Listener.Realm + "\"";
					context.Response.OutputStream.Close();
					IAsyncResult asyncResult = context.Listener.BeginGetContext(this.cb, this.state);
					this.forward = (ListenerAsyncResult)asyncResult;
					object obj2 = this.forward.locker;
					lock (obj2)
					{
						if (this.handle != null)
						{
							this.forward.handle = this.handle;
						}
					}
					ListenerAsyncResult listenerAsyncResult = this.forward;
					int num = 0;
					while (listenerAsyncResult.forward != null)
					{
						if (num > 20)
						{
							this.Complete(new HttpListenerException(400, "Too many authentication errors"));
						}
						listenerAsyncResult = listenerAsyncResult.forward;
						num++;
					}
				}
				else
				{
					this.completed = true;
					this.synch = false;
					if (this.handle != null)
					{
						this.handle.Set();
					}
					if (this.cb != null)
					{
						ThreadPool.UnsafeQueueUserWorkItem(ListenerAsyncResult.InvokeCB, this);
					}
				}
			}
		}

		// Token: 0x060026C3 RID: 9923 RVA: 0x0008FBA0 File Offset: 0x0008DDA0
		internal HttpListenerContext GetContext()
		{
			if (this.forward != null)
			{
				return this.forward.GetContext();
			}
			if (this.exception != null)
			{
				throw this.exception;
			}
			return this.context;
		}

		// Token: 0x17000802 RID: 2050
		// (get) Token: 0x060026C4 RID: 9924 RVA: 0x0008FBCB File Offset: 0x0008DDCB
		public object AsyncState
		{
			get
			{
				if (this.forward != null)
				{
					return this.forward.AsyncState;
				}
				return this.state;
			}
		}

		// Token: 0x17000803 RID: 2051
		// (get) Token: 0x060026C5 RID: 9925 RVA: 0x0008FBE8 File Offset: 0x0008DDE8
		public WaitHandle AsyncWaitHandle
		{
			get
			{
				if (this.forward != null)
				{
					return this.forward.AsyncWaitHandle;
				}
				object obj = this.locker;
				lock (obj)
				{
					if (this.handle == null)
					{
						this.handle = new ManualResetEvent(this.completed);
					}
				}
				return this.handle;
			}
		}

		// Token: 0x17000804 RID: 2052
		// (get) Token: 0x060026C6 RID: 9926 RVA: 0x0008FC58 File Offset: 0x0008DE58
		public bool CompletedSynchronously
		{
			get
			{
				if (this.forward != null)
				{
					return this.forward.CompletedSynchronously;
				}
				return this.synch;
			}
		}

		// Token: 0x17000805 RID: 2053
		// (get) Token: 0x060026C7 RID: 9927 RVA: 0x0008FC74 File Offset: 0x0008DE74
		public bool IsCompleted
		{
			get
			{
				if (this.forward != null)
				{
					return this.forward.IsCompleted;
				}
				object obj = this.locker;
				bool flag2;
				lock (obj)
				{
					flag2 = this.completed;
				}
				return flag2;
			}
		}

		// Token: 0x04001668 RID: 5736
		private ManualResetEvent handle;

		// Token: 0x04001669 RID: 5737
		private bool synch;

		// Token: 0x0400166A RID: 5738
		private bool completed;

		// Token: 0x0400166B RID: 5739
		private AsyncCallback cb;

		// Token: 0x0400166C RID: 5740
		private object state;

		// Token: 0x0400166D RID: 5741
		private Exception exception;

		// Token: 0x0400166E RID: 5742
		private HttpListenerContext context;

		// Token: 0x0400166F RID: 5743
		private object locker = new object();

		// Token: 0x04001670 RID: 5744
		private ListenerAsyncResult forward;

		// Token: 0x04001671 RID: 5745
		internal bool EndCalled;

		// Token: 0x04001672 RID: 5746
		internal bool InGet;

		// Token: 0x04001673 RID: 5747
		private static WaitCallback InvokeCB = new WaitCallback(ListenerAsyncResult.InvokeCallback);
	}
}
