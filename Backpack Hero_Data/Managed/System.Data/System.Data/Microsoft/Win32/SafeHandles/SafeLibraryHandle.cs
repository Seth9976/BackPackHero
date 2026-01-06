using System;

namespace Microsoft.Win32.SafeHandles
{
	// Token: 0x020003D1 RID: 977
	internal sealed class SafeLibraryHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06002F46 RID: 12102 RVA: 0x000CB240 File Offset: 0x000C9440
		internal SafeLibraryHandle()
			: base(true)
		{
		}

		// Token: 0x06002F47 RID: 12103 RVA: 0x000CB249 File Offset: 0x000C9449
		internal SafeLibraryHandle(bool ownsHandle)
			: base(ownsHandle)
		{
		}

		// Token: 0x06002F48 RID: 12104 RVA: 0x000CB252 File Offset: 0x000C9452
		protected override bool ReleaseHandle()
		{
			return global::Interop.Kernel32.FreeLibrary(this.handle);
		}
	}
}
