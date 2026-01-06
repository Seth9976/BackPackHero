using System;

namespace UnityEngine.Rendering
{
	// Token: 0x020003C2 RID: 962
	public struct RenderTargetBinding
	{
		// Token: 0x17000607 RID: 1543
		// (get) Token: 0x06001F7D RID: 8061 RVA: 0x000337A4 File Offset: 0x000319A4
		// (set) Token: 0x06001F7E RID: 8062 RVA: 0x000337BC File Offset: 0x000319BC
		public RenderTargetIdentifier[] colorRenderTargets
		{
			get
			{
				return this.m_ColorRenderTargets;
			}
			set
			{
				this.m_ColorRenderTargets = value;
			}
		}

		// Token: 0x17000608 RID: 1544
		// (get) Token: 0x06001F7F RID: 8063 RVA: 0x000337C8 File Offset: 0x000319C8
		// (set) Token: 0x06001F80 RID: 8064 RVA: 0x000337E0 File Offset: 0x000319E0
		public RenderTargetIdentifier depthRenderTarget
		{
			get
			{
				return this.m_DepthRenderTarget;
			}
			set
			{
				this.m_DepthRenderTarget = value;
			}
		}

		// Token: 0x17000609 RID: 1545
		// (get) Token: 0x06001F81 RID: 8065 RVA: 0x000337EC File Offset: 0x000319EC
		// (set) Token: 0x06001F82 RID: 8066 RVA: 0x00033804 File Offset: 0x00031A04
		public RenderBufferLoadAction[] colorLoadActions
		{
			get
			{
				return this.m_ColorLoadActions;
			}
			set
			{
				this.m_ColorLoadActions = value;
			}
		}

		// Token: 0x1700060A RID: 1546
		// (get) Token: 0x06001F83 RID: 8067 RVA: 0x00033810 File Offset: 0x00031A10
		// (set) Token: 0x06001F84 RID: 8068 RVA: 0x00033828 File Offset: 0x00031A28
		public RenderBufferStoreAction[] colorStoreActions
		{
			get
			{
				return this.m_ColorStoreActions;
			}
			set
			{
				this.m_ColorStoreActions = value;
			}
		}

		// Token: 0x1700060B RID: 1547
		// (get) Token: 0x06001F85 RID: 8069 RVA: 0x00033834 File Offset: 0x00031A34
		// (set) Token: 0x06001F86 RID: 8070 RVA: 0x0003384C File Offset: 0x00031A4C
		public RenderBufferLoadAction depthLoadAction
		{
			get
			{
				return this.m_DepthLoadAction;
			}
			set
			{
				this.m_DepthLoadAction = value;
			}
		}

		// Token: 0x1700060C RID: 1548
		// (get) Token: 0x06001F87 RID: 8071 RVA: 0x00033858 File Offset: 0x00031A58
		// (set) Token: 0x06001F88 RID: 8072 RVA: 0x00033870 File Offset: 0x00031A70
		public RenderBufferStoreAction depthStoreAction
		{
			get
			{
				return this.m_DepthStoreAction;
			}
			set
			{
				this.m_DepthStoreAction = value;
			}
		}

		// Token: 0x1700060D RID: 1549
		// (get) Token: 0x06001F89 RID: 8073 RVA: 0x0003387C File Offset: 0x00031A7C
		// (set) Token: 0x06001F8A RID: 8074 RVA: 0x00033894 File Offset: 0x00031A94
		public RenderTargetFlags flags
		{
			get
			{
				return this.m_Flags;
			}
			set
			{
				this.m_Flags = value;
			}
		}

		// Token: 0x06001F8B RID: 8075 RVA: 0x0003389E File Offset: 0x00031A9E
		public RenderTargetBinding(RenderTargetIdentifier[] colorRenderTargets, RenderBufferLoadAction[] colorLoadActions, RenderBufferStoreAction[] colorStoreActions, RenderTargetIdentifier depthRenderTarget, RenderBufferLoadAction depthLoadAction, RenderBufferStoreAction depthStoreAction)
		{
			this.m_ColorRenderTargets = colorRenderTargets;
			this.m_DepthRenderTarget = depthRenderTarget;
			this.m_ColorLoadActions = colorLoadActions;
			this.m_ColorStoreActions = colorStoreActions;
			this.m_DepthLoadAction = depthLoadAction;
			this.m_DepthStoreAction = depthStoreAction;
			this.m_Flags = RenderTargetFlags.None;
		}

		// Token: 0x06001F8C RID: 8076 RVA: 0x000338D5 File Offset: 0x00031AD5
		public RenderTargetBinding(RenderTargetIdentifier colorRenderTarget, RenderBufferLoadAction colorLoadAction, RenderBufferStoreAction colorStoreAction, RenderTargetIdentifier depthRenderTarget, RenderBufferLoadAction depthLoadAction, RenderBufferStoreAction depthStoreAction)
		{
			this = new RenderTargetBinding(new RenderTargetIdentifier[] { colorRenderTarget }, new RenderBufferLoadAction[] { colorLoadAction }, new RenderBufferStoreAction[] { colorStoreAction }, depthRenderTarget, depthLoadAction, depthStoreAction);
		}

		// Token: 0x06001F8D RID: 8077 RVA: 0x00033908 File Offset: 0x00031B08
		public RenderTargetBinding(RenderTargetSetup setup)
		{
			this.m_ColorRenderTargets = new RenderTargetIdentifier[setup.color.Length];
			for (int i = 0; i < this.m_ColorRenderTargets.Length; i++)
			{
				this.m_ColorRenderTargets[i] = new RenderTargetIdentifier(setup.color[i], setup.mipLevel, setup.cubemapFace, setup.depthSlice);
			}
			this.m_DepthRenderTarget = setup.depth;
			this.m_ColorLoadActions = (RenderBufferLoadAction[])setup.colorLoad.Clone();
			this.m_ColorStoreActions = (RenderBufferStoreAction[])setup.colorStore.Clone();
			this.m_DepthLoadAction = setup.depthLoad;
			this.m_DepthStoreAction = setup.depthStore;
			this.m_Flags = RenderTargetFlags.None;
		}

		// Token: 0x04000B7E RID: 2942
		private RenderTargetIdentifier[] m_ColorRenderTargets;

		// Token: 0x04000B7F RID: 2943
		private RenderTargetIdentifier m_DepthRenderTarget;

		// Token: 0x04000B80 RID: 2944
		private RenderBufferLoadAction[] m_ColorLoadActions;

		// Token: 0x04000B81 RID: 2945
		private RenderBufferStoreAction[] m_ColorStoreActions;

		// Token: 0x04000B82 RID: 2946
		private RenderBufferLoadAction m_DepthLoadAction;

		// Token: 0x04000B83 RID: 2947
		private RenderBufferStoreAction m_DepthStoreAction;

		// Token: 0x04000B84 RID: 2948
		private RenderTargetFlags m_Flags;
	}
}
