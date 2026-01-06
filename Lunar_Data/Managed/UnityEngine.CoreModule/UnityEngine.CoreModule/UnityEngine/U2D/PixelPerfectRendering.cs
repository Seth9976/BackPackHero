using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.U2D
{
	// Token: 0x0200026F RID: 623
	[NativeHeader("Runtime/2D/Common/PixelSnapping.h")]
	[MovedFrom("UnityEngine.Experimental.U2D")]
	public static class PixelPerfectRendering
	{
		// Token: 0x17000572 RID: 1394
		// (get) Token: 0x06001B11 RID: 6929
		// (set) Token: 0x06001B12 RID: 6930
		public static extern float pixelSnapSpacing
		{
			[FreeFunction("GetPixelSnapSpacing")]
			[MethodImpl(4096)]
			get;
			[FreeFunction("SetPixelSnapSpacing")]
			[MethodImpl(4096)]
			set;
		}
	}
}
