using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200018D RID: 397
	// (Invoke) Token: 0x0600093B RID: 2363
	[UnmanagedFunctionPointer(CallingConvention.StdCall)]
	public delegate void SteamAPI_CheckCallbackRegistered_t(int iCallbackNum);
}
