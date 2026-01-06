using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x020009FA RID: 2554
	public class EventSourceCreatedEventArgs : EventArgs
	{
		// Token: 0x17000F99 RID: 3993
		// (get) Token: 0x06005B1C RID: 23324 RVA: 0x001347FD File Offset: 0x001329FD
		// (set) Token: 0x06005B1D RID: 23325 RVA: 0x00134805 File Offset: 0x00132A05
		public EventSource EventSource { get; internal set; }
	}
}
