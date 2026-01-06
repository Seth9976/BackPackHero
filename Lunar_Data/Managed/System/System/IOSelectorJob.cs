using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace System
{
	// Token: 0x02000171 RID: 369
	[StructLayout(LayoutKind.Sequential)]
	internal class IOSelectorJob : IThreadPoolWorkItem
	{
		// Token: 0x060009D3 RID: 2515 RVA: 0x0002B5E4 File Offset: 0x000297E4
		public IOSelectorJob(IOOperation operation, IOAsyncCallback callback, IOAsyncResult state)
		{
			this.operation = operation;
			this.callback = callback;
			this.state = state;
		}

		// Token: 0x060009D4 RID: 2516 RVA: 0x0002B601 File Offset: 0x00029801
		void IThreadPoolWorkItem.ExecuteWorkItem()
		{
			this.callback(this.state);
		}

		// Token: 0x060009D5 RID: 2517 RVA: 0x00003917 File Offset: 0x00001B17
		void IThreadPoolWorkItem.MarkAborted(ThreadAbortException tae)
		{
		}

		// Token: 0x060009D6 RID: 2518 RVA: 0x0002B614 File Offset: 0x00029814
		public void MarkDisposed()
		{
			this.state.CompleteDisposed();
		}

		// Token: 0x040006A8 RID: 1704
		private IOOperation operation;

		// Token: 0x040006A9 RID: 1705
		private IOAsyncCallback callback;

		// Token: 0x040006AA RID: 1706
		private IOAsyncResult state;
	}
}
