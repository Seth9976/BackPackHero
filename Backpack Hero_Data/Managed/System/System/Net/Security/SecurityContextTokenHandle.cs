using System;
using System.Threading;
using Microsoft.Win32.SafeHandles;

namespace System.Net.Security
{
	// Token: 0x0200065D RID: 1629
	internal sealed class SecurityContextTokenHandle : CriticalHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x0600340C RID: 13324 RVA: 0x000BD248 File Offset: 0x000BB448
		private SecurityContextTokenHandle()
		{
		}

		// Token: 0x0600340D RID: 13325 RVA: 0x000BD250 File Offset: 0x000BB450
		internal IntPtr DangerousGetHandle()
		{
			return this.handle;
		}

		// Token: 0x0600340E RID: 13326 RVA: 0x000BD258 File Offset: 0x000BB458
		protected override bool ReleaseHandle()
		{
			return this.IsInvalid || Interlocked.Increment(ref this._disposed) != 1 || global::Interop.Kernel32.CloseHandle(this.handle);
		}

		// Token: 0x04001FAA RID: 8106
		private int _disposed;
	}
}
