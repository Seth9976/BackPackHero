using System;
using System.Runtime.InteropServices;

namespace Microsoft.Win32.SafeHandles
{
	/// <summary>Provides a safe handle that represents a key (NCRYPT_KEY_HANDLE).</summary>
	// Token: 0x02000017 RID: 23
	public sealed class SafeNCryptKeyHandle : SafeNCryptHandle
	{
		/// <summary>Initializes a new instance of the <see cref="T:Microsoft.Win32.SafeHandles.SafeNCryptKeyHandle" /> class.</summary>
		// Token: 0x06000048 RID: 72 RVA: 0x000023D4 File Offset: 0x000005D4
		public SafeNCryptKeyHandle()
		{
		}

		// Token: 0x06000049 RID: 73 RVA: 0x000023DC File Offset: 0x000005DC
		public SafeNCryptKeyHandle(IntPtr handle, SafeHandle parentHandle)
			: base(handle, parentHandle)
		{
		}

		// Token: 0x0600004A RID: 74 RVA: 0x000023D1 File Offset: 0x000005D1
		protected override bool ReleaseNativeHandle()
		{
			return false;
		}
	}
}
