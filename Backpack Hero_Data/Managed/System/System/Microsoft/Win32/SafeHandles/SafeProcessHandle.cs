using System;
using System.Security;
using System.Security.Permissions;

namespace Microsoft.Win32.SafeHandles
{
	// Token: 0x02000133 RID: 307
	[SuppressUnmanagedCodeSecurity]
	public sealed class SafeProcessHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06000727 RID: 1831 RVA: 0x00013AC8 File Offset: 0x00011CC8
		internal SafeProcessHandle()
			: base(true)
		{
		}

		// Token: 0x06000728 RID: 1832 RVA: 0x00013AD1 File Offset: 0x00011CD1
		internal SafeProcessHandle(IntPtr handle)
			: base(true)
		{
			base.SetHandle(handle);
		}

		// Token: 0x06000729 RID: 1833 RVA: 0x00013AE1 File Offset: 0x00011CE1
		[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
		public SafeProcessHandle(IntPtr existingHandle, bool ownsHandle)
			: base(ownsHandle)
		{
			base.SetHandle(existingHandle);
		}

		// Token: 0x0600072A RID: 1834 RVA: 0x00013AF1 File Offset: 0x00011CF1
		internal void InitialSetHandle(IntPtr h)
		{
			this.handle = h;
		}

		// Token: 0x0600072B RID: 1835 RVA: 0x00013AFA File Offset: 0x00011CFA
		protected override bool ReleaseHandle()
		{
			return NativeMethods.CloseProcess(this.handle);
		}

		// Token: 0x04000516 RID: 1302
		internal static SafeProcessHandle InvalidHandle = new SafeProcessHandle(IntPtr.Zero);
	}
}
