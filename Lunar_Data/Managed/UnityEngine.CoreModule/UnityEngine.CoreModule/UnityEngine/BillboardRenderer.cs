using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000121 RID: 289
	[NativeHeader("Runtime/Graphics/Billboard/BillboardRenderer.h")]
	public sealed class BillboardRenderer : Renderer
	{
		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x060007F7 RID: 2039
		// (set) Token: 0x060007F8 RID: 2040
		public extern BillboardAsset billboard
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}
	}
}
