using System;
using System.Runtime.InteropServices;

namespace Microsoft.Win32.SafeHandles
{
	/// <summary>Provides a safe handle that can be used by Cryptography Next Generation (CNG) objects.</summary>
	// Token: 0x02000016 RID: 22
	public abstract class SafeNCryptHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		/// <summary>Initializes a new instance of the <see cref="T:Microsoft.Win32.SafeHandles.SafeNCryptHandle" /> class.</summary>
		// Token: 0x06000043 RID: 67 RVA: 0x000023B3 File Offset: 0x000005B3
		protected SafeNCryptHandle()
			: base(true)
		{
		}

		// Token: 0x06000044 RID: 68 RVA: 0x000023BC File Offset: 0x000005BC
		protected SafeNCryptHandle(IntPtr handle, SafeHandle parentHandle)
			: base(false)
		{
			throw new NotImplementedException();
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000045 RID: 69 RVA: 0x000023CA File Offset: 0x000005CA
		public override bool IsInvalid
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Releases a handle used by a Cryptography Next Generation (CNG) object.</summary>
		/// <returns>true if the handle is released successfully; otherwise, false.</returns>
		// Token: 0x06000046 RID: 70 RVA: 0x000023D1 File Offset: 0x000005D1
		protected override bool ReleaseHandle()
		{
			return false;
		}

		/// <summary>Releases a native handle used by a Cryptography Next Generation (CNG) object.</summary>
		/// <returns>true if the handle is released successfully; otherwise, false.</returns>
		// Token: 0x06000047 RID: 71
		protected abstract bool ReleaseNativeHandle();
	}
}
