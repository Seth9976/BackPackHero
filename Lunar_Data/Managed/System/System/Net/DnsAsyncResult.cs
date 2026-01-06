using System;
using System.Threading;

namespace System.Net
{
	// Token: 0x0200048E RID: 1166
	internal class DnsAsyncResult : IAsyncResult
	{
		// Token: 0x060024BD RID: 9405 RVA: 0x000877A6 File Offset: 0x000859A6
		public DnsAsyncResult(AsyncCallback cb, object state)
		{
			this.callback = cb;
			this.state = state;
		}

		// Token: 0x060024BE RID: 9406 RVA: 0x000877BC File Offset: 0x000859BC
		public void SetCompleted(bool synch, IPHostEntry entry, Exception e)
		{
			this.synch = synch;
			this.entry = entry;
			this.exc = e;
			lock (this)
			{
				if (this.is_completed)
				{
					return;
				}
				this.is_completed = true;
				if (this.handle != null)
				{
					this.handle.Set();
				}
			}
			if (this.callback != null)
			{
				ThreadPool.QueueUserWorkItem(DnsAsyncResult.internal_cb, this);
			}
		}

		// Token: 0x060024BF RID: 9407 RVA: 0x00087840 File Offset: 0x00085A40
		public void SetCompleted(bool synch, Exception e)
		{
			this.SetCompleted(synch, null, e);
		}

		// Token: 0x060024C0 RID: 9408 RVA: 0x0008784B File Offset: 0x00085A4B
		public void SetCompleted(bool synch, IPHostEntry entry)
		{
			this.SetCompleted(synch, entry, null);
		}

		// Token: 0x060024C1 RID: 9409 RVA: 0x00087858 File Offset: 0x00085A58
		private static void CB(object _this)
		{
			DnsAsyncResult dnsAsyncResult = (DnsAsyncResult)_this;
			dnsAsyncResult.callback(dnsAsyncResult);
		}

		// Token: 0x17000755 RID: 1877
		// (get) Token: 0x060024C2 RID: 9410 RVA: 0x00087878 File Offset: 0x00085A78
		public object AsyncState
		{
			get
			{
				return this.state;
			}
		}

		// Token: 0x17000756 RID: 1878
		// (get) Token: 0x060024C3 RID: 9411 RVA: 0x00087880 File Offset: 0x00085A80
		public WaitHandle AsyncWaitHandle
		{
			get
			{
				lock (this)
				{
					if (this.handle == null)
					{
						this.handle = new ManualResetEvent(this.is_completed);
					}
				}
				return this.handle;
			}
		}

		// Token: 0x17000757 RID: 1879
		// (get) Token: 0x060024C4 RID: 9412 RVA: 0x000878D4 File Offset: 0x00085AD4
		public Exception Exception
		{
			get
			{
				return this.exc;
			}
		}

		// Token: 0x17000758 RID: 1880
		// (get) Token: 0x060024C5 RID: 9413 RVA: 0x000878DC File Offset: 0x00085ADC
		public IPHostEntry HostEntry
		{
			get
			{
				return this.entry;
			}
		}

		// Token: 0x17000759 RID: 1881
		// (get) Token: 0x060024C6 RID: 9414 RVA: 0x000878E4 File Offset: 0x00085AE4
		public bool CompletedSynchronously
		{
			get
			{
				return this.synch;
			}
		}

		// Token: 0x1700075A RID: 1882
		// (get) Token: 0x060024C7 RID: 9415 RVA: 0x000878EC File Offset: 0x00085AEC
		public bool IsCompleted
		{
			get
			{
				bool flag2;
				lock (this)
				{
					flag2 = this.is_completed;
				}
				return flag2;
			}
		}

		// Token: 0x04001554 RID: 5460
		private static WaitCallback internal_cb = new WaitCallback(DnsAsyncResult.CB);

		// Token: 0x04001555 RID: 5461
		private ManualResetEvent handle;

		// Token: 0x04001556 RID: 5462
		private bool synch;

		// Token: 0x04001557 RID: 5463
		private bool is_completed;

		// Token: 0x04001558 RID: 5464
		private AsyncCallback callback;

		// Token: 0x04001559 RID: 5465
		private object state;

		// Token: 0x0400155A RID: 5466
		private IPHostEntry entry;

		// Token: 0x0400155B RID: 5467
		private Exception exc;
	}
}
