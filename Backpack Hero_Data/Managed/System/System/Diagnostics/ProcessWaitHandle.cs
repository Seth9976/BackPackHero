using System;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;

namespace System.Diagnostics
{
	// Token: 0x02000247 RID: 583
	internal class ProcessWaitHandle : WaitHandle
	{
		// Token: 0x0600120D RID: 4621 RVA: 0x0004E18C File Offset: 0x0004C38C
		internal ProcessWaitHandle(SafeProcessHandle processHandle)
		{
			SafeWaitHandle safeWaitHandle = null;
			if (!Microsoft.Win32.NativeMethods.DuplicateHandle(new HandleRef(this, Microsoft.Win32.NativeMethods.GetCurrentProcess()), processHandle, new HandleRef(this, Microsoft.Win32.NativeMethods.GetCurrentProcess()), out safeWaitHandle, 0, false, 2))
			{
				throw new SystemException("Unknown error in DuplicateHandle");
			}
			base.SafeWaitHandle = safeWaitHandle;
		}
	}
}
