using System;
using System.Runtime.InteropServices;

namespace Mono.Btls
{
	// Token: 0x020000E4 RID: 228
	// (Invoke) Token: 0x060004D3 RID: 1235
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate int MonoBtlsServerNameCallback();
}
