using System;
using System.Runtime.InteropServices;

namespace System.Data
{
	// Token: 0x020000FF RID: 255
	internal static class SafeNativeMethods
	{
		// Token: 0x06000E07 RID: 3591 RVA: 0x000496A9 File Offset: 0x000478A9
		internal static IntPtr LocalAlloc(IntPtr initialSize)
		{
			IntPtr intPtr = Marshal.AllocHGlobal(initialSize);
			SafeNativeMethods.ZeroMemory(intPtr, (int)initialSize);
			return intPtr;
		}

		// Token: 0x06000E08 RID: 3592 RVA: 0x000496BD File Offset: 0x000478BD
		internal static void LocalFree(IntPtr ptr)
		{
			Marshal.FreeHGlobal(ptr);
		}

		// Token: 0x06000E09 RID: 3593 RVA: 0x000496C5 File Offset: 0x000478C5
		internal static void ZeroMemory(IntPtr ptr, int length)
		{
			Marshal.Copy(new byte[length], 0, ptr, length);
		}

		// Token: 0x06000E0A RID: 3594
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Ansi, SetLastError = true, ThrowOnUnmappableChar = true)]
		internal static extern IntPtr GetProcAddress(IntPtr HModule, [MarshalAs(UnmanagedType.LPStr)] [In] string funcName);
	}
}
