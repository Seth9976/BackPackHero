using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200013D RID: 317
	[NativeHeader("Runtime/Graphics/GraphicsScriptBindings.h")]
	public static class RendererExtensions
	{
		// Token: 0x06000A6B RID: 2667 RVA: 0x0000F252 File Offset: 0x0000D452
		public static void UpdateGIMaterials(this Renderer renderer)
		{
			RendererExtensions.UpdateGIMaterialsForRenderer(renderer);
		}

		// Token: 0x06000A6C RID: 2668
		[FreeFunction("RendererScripting::UpdateGIMaterialsForRenderer")]
		[MethodImpl(4096)]
		internal static extern void UpdateGIMaterialsForRenderer(Renderer renderer);
	}
}
