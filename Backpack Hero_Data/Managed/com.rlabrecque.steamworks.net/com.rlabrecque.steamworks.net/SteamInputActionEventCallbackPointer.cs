using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200019B RID: 411
	// (Invoke) Token: 0x060009E0 RID: 2528
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate void SteamInputActionEventCallbackPointer(IntPtr SteamInputActionEvent);
}
