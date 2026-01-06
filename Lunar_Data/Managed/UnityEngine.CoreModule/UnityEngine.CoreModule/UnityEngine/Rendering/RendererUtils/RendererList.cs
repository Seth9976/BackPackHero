using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Rendering.RendererUtils
{
	// Token: 0x02000428 RID: 1064
	[NativeHeader("Runtime/Graphics/ScriptableRenderLoop/RendererList.h")]
	public struct RendererList
	{
		// Token: 0x170006FD RID: 1789
		// (get) Token: 0x06002519 RID: 9497 RVA: 0x0003E9E1 File Offset: 0x0003CBE1
		public bool isValid
		{
			get
			{
				return RendererList.get_isValid_Injected(ref this);
			}
		}

		// Token: 0x0600251A RID: 9498 RVA: 0x0003E9E9 File Offset: 0x0003CBE9
		internal RendererList(UIntPtr ctx, uint indx)
		{
			this.context = ctx;
			this.index = indx;
			this.frame = 0U;
		}

		// Token: 0x0600251C RID: 9500
		[MethodImpl(4096)]
		private static extern bool get_isValid_Injected(ref RendererList _unity_self);

		// Token: 0x04000DCC RID: 3532
		internal UIntPtr context;

		// Token: 0x04000DCD RID: 3533
		internal uint index;

		// Token: 0x04000DCE RID: 3534
		internal uint frame;

		// Token: 0x04000DCF RID: 3535
		public static readonly RendererList nullRendererList = new RendererList(UIntPtr.Zero, uint.MaxValue);
	}
}
