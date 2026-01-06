using System;

namespace System.Diagnostics
{
	// Token: 0x02000210 RID: 528
	public static class StackFrameExtensions
	{
		// Token: 0x06000F3B RID: 3899 RVA: 0x00044963 File Offset: 0x00042B63
		public static bool HasNativeImage(this StackFrame stackFrame)
		{
			return stackFrame.GetNativeImageBase() != IntPtr.Zero;
		}

		// Token: 0x06000F3C RID: 3900 RVA: 0x00044975 File Offset: 0x00042B75
		public static bool HasMethod(this StackFrame stackFrame)
		{
			return stackFrame.GetMethod() != null;
		}

		// Token: 0x06000F3D RID: 3901 RVA: 0x00044983 File Offset: 0x00042B83
		public static bool HasILOffset(this StackFrame stackFrame)
		{
			return stackFrame.GetILOffset() != -1;
		}

		// Token: 0x06000F3E RID: 3902 RVA: 0x00044991 File Offset: 0x00042B91
		public static bool HasSource(this StackFrame stackFrame)
		{
			return stackFrame.GetFileName() != null;
		}

		// Token: 0x06000F3F RID: 3903 RVA: 0x00011D8D File Offset: 0x0000FF8D
		public static IntPtr GetNativeIP(this StackFrame stackFrame)
		{
			return IntPtr.Zero;
		}

		// Token: 0x06000F40 RID: 3904 RVA: 0x00011D8D File Offset: 0x0000FF8D
		public static IntPtr GetNativeImageBase(this StackFrame stackFrame)
		{
			return IntPtr.Zero;
		}
	}
}
