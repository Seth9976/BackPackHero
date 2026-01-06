using System;
using System.Runtime.InteropServices;

namespace Mono.Btls
{
	// Token: 0x020000E3 RID: 227
	// (Invoke) Token: 0x060004CF RID: 1231
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate int MonoBtlsSelectCallback(string[] acceptableIssuers);
}
