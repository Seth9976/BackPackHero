using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200012F RID: 303
	[NativeHeader("Runtime/GfxDevice/ScalableBufferManager.h")]
	[StaticAccessor("ScalableBufferManager::GetInstance()", StaticAccessorType.Dot)]
	public static class ScalableBufferManager
	{
		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x0600098A RID: 2442
		public static extern float widthScaleFactor
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x0600098B RID: 2443
		public static extern float heightScaleFactor
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x0600098C RID: 2444
		[MethodImpl(4096)]
		public static extern void ResizeBuffers(float widthScale, float heightScale);
	}
}
