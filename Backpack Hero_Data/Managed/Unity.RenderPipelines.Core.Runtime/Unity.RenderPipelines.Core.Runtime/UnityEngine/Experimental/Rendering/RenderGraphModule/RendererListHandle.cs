using System;
using System.Diagnostics;
using UnityEngine.Rendering.RendererUtils;

namespace UnityEngine.Experimental.Rendering.RenderGraphModule
{
	// Token: 0x02000033 RID: 51
	[DebuggerDisplay("RendererList ({handle})")]
	public struct RendererListHandle
	{
		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000206 RID: 518 RVA: 0x0000BAB0 File Offset: 0x00009CB0
		// (set) Token: 0x06000207 RID: 519 RVA: 0x0000BAB8 File Offset: 0x00009CB8
		internal int handle { readonly get; private set; }

		// Token: 0x06000208 RID: 520 RVA: 0x0000BAC1 File Offset: 0x00009CC1
		internal RendererListHandle(int handle)
		{
			this.handle = handle;
			this.m_IsValid = true;
		}

		// Token: 0x06000209 RID: 521 RVA: 0x0000BAD1 File Offset: 0x00009CD1
		public static implicit operator int(RendererListHandle handle)
		{
			return handle.handle;
		}

		// Token: 0x0600020A RID: 522 RVA: 0x0000BADA File Offset: 0x00009CDA
		public static implicit operator RendererList(RendererListHandle rendererList)
		{
			if (!rendererList.IsValid())
			{
				return RendererList.nullRendererList;
			}
			return RenderGraphResourceRegistry.current.GetRendererList(in rendererList);
		}

		// Token: 0x0600020B RID: 523 RVA: 0x0000BAF7 File Offset: 0x00009CF7
		public bool IsValid()
		{
			return this.m_IsValid;
		}

		// Token: 0x04000145 RID: 325
		private bool m_IsValid;
	}
}
