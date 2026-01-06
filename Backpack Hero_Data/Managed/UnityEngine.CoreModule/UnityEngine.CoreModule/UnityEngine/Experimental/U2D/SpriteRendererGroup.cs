using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Experimental.U2D
{
	// Token: 0x02000452 RID: 1106
	[NativeHeader("Runtime/2D/Renderer/SpriteRendererGroup.h")]
	[RequiredByNativeCode]
	[StructLayout(0)]
	internal class SpriteRendererGroup
	{
		// Token: 0x060027A5 RID: 10149 RVA: 0x0004141C File Offset: 0x0003F61C
		public static void AddRenderers(NativeArray<SpriteIntermediateRendererInfo> renderers)
		{
			SpriteRendererGroup.AddRenderers(renderers.GetUnsafeReadOnlyPtr<SpriteIntermediateRendererInfo>(), renderers.Length);
		}

		// Token: 0x060027A6 RID: 10150
		[MethodImpl(4096)]
		private unsafe static extern void AddRenderers(void* renderers, int count);

		// Token: 0x060027A7 RID: 10151
		[MethodImpl(4096)]
		public static extern void Clear();
	}
}
