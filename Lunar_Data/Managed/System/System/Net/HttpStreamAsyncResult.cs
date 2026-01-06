using System;
using System.Threading;

namespace System.Net
{
	// Token: 0x020004A4 RID: 1188
	internal class HttpStreamAsyncResult : IAsyncResult
	{
		// Token: 0x060025DC RID: 9692 RVA: 0x0008BFDE File Offset: 0x0008A1DE
		public void Complete(Exception e)
		{
			this.Error = e;
			this.Complete();
		}

		// Token: 0x060025DD RID: 9693 RVA: 0x0008BFF0 File Offset: 0x0008A1F0
		public void Complete()
		{
			object obj = this.locker;
			lock (obj)
			{
				if (!this.completed)
				{
					this.completed = true;
					if (this.handle != null)
					{
						this.handle.Set();
					}
					if (this.Callback != null)
					{
						this.Callback.BeginInvoke(this, null, null);
					}
				}
			}
		}

		// Token: 0x170007B0 RID: 1968
		// (get) Token: 0x060025DE RID: 9694 RVA: 0x0008C068 File Offset: 0x0008A268
		public object AsyncState
		{
			get
			{
				return this.State;
			}
		}

		// Token: 0x170007B1 RID: 1969
		// (get) Token: 0x060025DF RID: 9695 RVA: 0x0008C070 File Offset: 0x0008A270
		public WaitHandle AsyncWaitHandle
		{
			get
			{
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

		// Token: 0x170007B2 RID: 1970
		// (get) Token: 0x060025E0 RID: 9696 RVA: 0x0008C0CC File Offset: 0x0008A2CC
		public bool CompletedSynchronously
		{
			get
			{
				return this.SynchRead == this.Count;
			}
		}

		// Token: 0x170007B3 RID: 1971
		// (get) Token: 0x060025E1 RID: 9697 RVA: 0x0008C0DC File Offset: 0x0008A2DC
		public bool IsCompleted
		{
			get
			{
				object obj = this.locker;
				bool flag2;
				lock (obj)
				{
					flag2 = this.completed;
				}
				return flag2;
			}
		}

		// Token: 0x040015E0 RID: 5600
		private object locker = new object();

		// Token: 0x040015E1 RID: 5601
		private ManualResetEvent handle;

		// Token: 0x040015E2 RID: 5602
		private bool completed;

		// Token: 0x040015E3 RID: 5603
		internal byte[] Buffer;

		// Token: 0x040015E4 RID: 5604
		internal int Offset;

		// Token: 0x040015E5 RID: 5605
		internal int Count;

		// Token: 0x040015E6 RID: 5606
		internal AsyncCallback Callback;

		// Token: 0x040015E7 RID: 5607
		internal object State;

		// Token: 0x040015E8 RID: 5608
		internal int SynchRead;

		// Token: 0x040015E9 RID: 5609
		internal Exception Error;
	}
}
