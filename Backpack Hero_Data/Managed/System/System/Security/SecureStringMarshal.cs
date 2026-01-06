using System;
using System.Runtime.InteropServices;

namespace System.Security
{
	// Token: 0x0200028D RID: 653
	public static class SecureStringMarshal
	{
		// Token: 0x060014A9 RID: 5289 RVA: 0x0005430A File Offset: 0x0005250A
		public static IntPtr SecureStringToCoTaskMemAnsi(SecureString s)
		{
			return Marshal.SecureStringToCoTaskMemAnsi(s);
		}

		// Token: 0x060014AA RID: 5290 RVA: 0x00054312 File Offset: 0x00052512
		public static IntPtr SecureStringToGlobalAllocAnsi(SecureString s)
		{
			return Marshal.SecureStringToGlobalAllocAnsi(s);
		}

		// Token: 0x060014AB RID: 5291 RVA: 0x0005431A File Offset: 0x0005251A
		public static IntPtr SecureStringToCoTaskMemUnicode(SecureString s)
		{
			return Marshal.SecureStringToCoTaskMemUnicode(s);
		}

		// Token: 0x060014AC RID: 5292 RVA: 0x00054322 File Offset: 0x00052522
		public static IntPtr SecureStringToGlobalAllocUnicode(SecureString s)
		{
			return Marshal.SecureStringToGlobalAllocUnicode(s);
		}
	}
}
