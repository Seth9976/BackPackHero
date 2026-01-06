using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace System
{
	// Token: 0x02000170 RID: 368
	[StructLayout(LayoutKind.Sequential)]
	internal abstract class IOAsyncResult : IAsyncResult
	{
		// Token: 0x060009C8 RID: 2504 RVA: 0x0000219B File Offset: 0x0000039B
		protected IOAsyncResult()
		{
		}

		// Token: 0x060009C9 RID: 2505 RVA: 0x0002B4C3 File Offset: 0x000296C3
		protected void Init(AsyncCallback async_callback, object async_state)
		{
			this.async_callback = async_callback;
			this.async_state = async_state;
			this.completed = false;
			this.completed_synchronously = false;
			if (this.wait_handle != null)
			{
				this.wait_handle.Reset();
			}
		}

		// Token: 0x060009CA RID: 2506 RVA: 0x0002B4F5 File Offset: 0x000296F5
		protected IOAsyncResult(AsyncCallback async_callback, object async_state)
		{
			this.async_callback = async_callback;
			this.async_state = async_state;
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x060009CB RID: 2507 RVA: 0x0002B50B File Offset: 0x0002970B
		public AsyncCallback AsyncCallback
		{
			get
			{
				return this.async_callback;
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x060009CC RID: 2508 RVA: 0x0002B513 File Offset: 0x00029713
		public object AsyncState
		{
			get
			{
				return this.async_state;
			}
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x060009CD RID: 2509 RVA: 0x0002B51C File Offset: 0x0002971C
		public WaitHandle AsyncWaitHandle
		{
			get
			{
				WaitHandle waitHandle;
				lock (this)
				{
					if (this.wait_handle == null)
					{
						this.wait_handle = new ManualResetEvent(this.completed);
					}
					waitHandle = this.wait_handle;
				}
				return waitHandle;
			}
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x060009CE RID: 2510 RVA: 0x0002B574 File Offset: 0x00029774
		// (set) Token: 0x060009CF RID: 2511 RVA: 0x0002B57C File Offset: 0x0002977C
		public bool CompletedSynchronously
		{
			get
			{
				return this.completed_synchronously;
			}
			protected set
			{
				this.completed_synchronously = value;
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x060009D0 RID: 2512 RVA: 0x0002B585 File Offset: 0x00029785
		// (set) Token: 0x060009D1 RID: 2513 RVA: 0x0002B590 File Offset: 0x00029790
		public bool IsCompleted
		{
			get
			{
				return this.completed;
			}
			protected set
			{
				this.completed = value;
				lock (this)
				{
					if (value && this.wait_handle != null)
					{
						this.wait_handle.Set();
					}
				}
			}
		}

		// Token: 0x060009D2 RID: 2514
		internal abstract void CompleteDisposed();

		// Token: 0x040006A3 RID: 1699
		private AsyncCallback async_callback;

		// Token: 0x040006A4 RID: 1700
		private object async_state;

		// Token: 0x040006A5 RID: 1701
		private ManualResetEvent wait_handle;

		// Token: 0x040006A6 RID: 1702
		private bool completed_synchronously;

		// Token: 0x040006A7 RID: 1703
		private bool completed;
	}
}
