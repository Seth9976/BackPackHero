using System;
using System.Runtime.InteropServices;

namespace System.IO
{
	// Token: 0x02000832 RID: 2098
	internal struct kevent : IDisposable
	{
		// Token: 0x060042D5 RID: 17109 RVA: 0x000E8524 File Offset: 0x000E6724
		public void Dispose()
		{
			if (this.udata != IntPtr.Zero)
			{
				Marshal.FreeHGlobal(this.udata);
			}
		}

		// Token: 0x0400282A RID: 10282
		public UIntPtr ident;

		// Token: 0x0400282B RID: 10283
		public EventFilter filter;

		// Token: 0x0400282C RID: 10284
		public EventFlags flags;

		// Token: 0x0400282D RID: 10285
		public FilterFlags fflags;

		// Token: 0x0400282E RID: 10286
		public IntPtr data;

		// Token: 0x0400282F RID: 10287
		public IntPtr udata;
	}
}
