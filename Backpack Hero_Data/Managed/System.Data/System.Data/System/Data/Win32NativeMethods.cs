using System;
using System.Data.SqlClient;
using System.Runtime.InteropServices;

namespace System.Data
{
	// Token: 0x02000100 RID: 256
	internal static class Win32NativeMethods
	{
		// Token: 0x06000E0B RID: 3595 RVA: 0x000496D8 File Offset: 0x000478D8
		internal static bool IsTokenRestrictedWrapper(IntPtr token)
		{
			bool flag;
			uint num = SNINativeMethodWrapper.UnmanagedIsTokenRestricted(token, out flag);
			if (num != 0U)
			{
				Marshal.ThrowExceptionForHR((int)num);
			}
			return flag;
		}
	}
}
