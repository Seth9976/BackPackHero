using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Rendering
{
	// Token: 0x0200039D RID: 925
	[NativeHeader("Runtime/Graphics/GraphicsScriptBindings.h")]
	public class PIX
	{
		// Token: 0x06001F44 RID: 8004
		[FreeFunction("PIX::BeginGPUCapture")]
		[MethodImpl(4096)]
		public static extern void BeginGPUCapture();

		// Token: 0x06001F45 RID: 8005
		[FreeFunction("PIX::EndGPUCapture")]
		[MethodImpl(4096)]
		public static extern void EndGPUCapture();

		// Token: 0x06001F46 RID: 8006
		[FreeFunction("PIX::IsAttached")]
		[MethodImpl(4096)]
		public static extern bool IsAttached();
	}
}
