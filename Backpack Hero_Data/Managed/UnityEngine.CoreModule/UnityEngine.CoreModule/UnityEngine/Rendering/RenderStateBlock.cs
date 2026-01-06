using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000407 RID: 1031
	public struct RenderStateBlock : IEquatable<RenderStateBlock>
	{
		// Token: 0x0600233B RID: 9019 RVA: 0x0003B5BB File Offset: 0x000397BB
		public RenderStateBlock(RenderStateMask mask)
		{
			this.m_BlendState = BlendState.defaultValue;
			this.m_RasterState = RasterState.defaultValue;
			this.m_DepthState = DepthState.defaultValue;
			this.m_StencilState = StencilState.defaultValue;
			this.m_StencilReference = 0;
			this.m_Mask = mask;
		}

		// Token: 0x1700068E RID: 1678
		// (get) Token: 0x0600233C RID: 9020 RVA: 0x0003B5F8 File Offset: 0x000397F8
		// (set) Token: 0x0600233D RID: 9021 RVA: 0x0003B610 File Offset: 0x00039810
		public BlendState blendState
		{
			get
			{
				return this.m_BlendState;
			}
			set
			{
				this.m_BlendState = value;
			}
		}

		// Token: 0x1700068F RID: 1679
		// (get) Token: 0x0600233E RID: 9022 RVA: 0x0003B61C File Offset: 0x0003981C
		// (set) Token: 0x0600233F RID: 9023 RVA: 0x0003B634 File Offset: 0x00039834
		public RasterState rasterState
		{
			get
			{
				return this.m_RasterState;
			}
			set
			{
				this.m_RasterState = value;
			}
		}

		// Token: 0x17000690 RID: 1680
		// (get) Token: 0x06002340 RID: 9024 RVA: 0x0003B640 File Offset: 0x00039840
		// (set) Token: 0x06002341 RID: 9025 RVA: 0x0003B658 File Offset: 0x00039858
		public DepthState depthState
		{
			get
			{
				return this.m_DepthState;
			}
			set
			{
				this.m_DepthState = value;
			}
		}

		// Token: 0x17000691 RID: 1681
		// (get) Token: 0x06002342 RID: 9026 RVA: 0x0003B664 File Offset: 0x00039864
		// (set) Token: 0x06002343 RID: 9027 RVA: 0x0003B67C File Offset: 0x0003987C
		public StencilState stencilState
		{
			get
			{
				return this.m_StencilState;
			}
			set
			{
				this.m_StencilState = value;
			}
		}

		// Token: 0x17000692 RID: 1682
		// (get) Token: 0x06002344 RID: 9028 RVA: 0x0003B688 File Offset: 0x00039888
		// (set) Token: 0x06002345 RID: 9029 RVA: 0x0003B6A0 File Offset: 0x000398A0
		public int stencilReference
		{
			get
			{
				return this.m_StencilReference;
			}
			set
			{
				this.m_StencilReference = value;
			}
		}

		// Token: 0x17000693 RID: 1683
		// (get) Token: 0x06002346 RID: 9030 RVA: 0x0003B6AC File Offset: 0x000398AC
		// (set) Token: 0x06002347 RID: 9031 RVA: 0x0003B6C4 File Offset: 0x000398C4
		public RenderStateMask mask
		{
			get
			{
				return this.m_Mask;
			}
			set
			{
				this.m_Mask = value;
			}
		}

		// Token: 0x06002348 RID: 9032 RVA: 0x0003B6D0 File Offset: 0x000398D0
		public bool Equals(RenderStateBlock other)
		{
			return this.m_BlendState.Equals(other.m_BlendState) && this.m_RasterState.Equals(other.m_RasterState) && this.m_DepthState.Equals(other.m_DepthState) && this.m_StencilState.Equals(other.m_StencilState) && this.m_StencilReference == other.m_StencilReference && this.m_Mask == other.m_Mask;
		}

		// Token: 0x06002349 RID: 9033 RVA: 0x0003B750 File Offset: 0x00039950
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is RenderStateBlock && this.Equals((RenderStateBlock)obj);
		}

		// Token: 0x0600234A RID: 9034 RVA: 0x0003B788 File Offset: 0x00039988
		public override int GetHashCode()
		{
			int num = this.m_BlendState.GetHashCode();
			num = (num * 397) ^ this.m_RasterState.GetHashCode();
			num = (num * 397) ^ this.m_DepthState.GetHashCode();
			num = (num * 397) ^ this.m_StencilState.GetHashCode();
			num = (num * 397) ^ this.m_StencilReference;
			return (num * 397) ^ (int)this.m_Mask;
		}

		// Token: 0x0600234B RID: 9035 RVA: 0x0003B81C File Offset: 0x00039A1C
		public static bool operator ==(RenderStateBlock left, RenderStateBlock right)
		{
			return left.Equals(right);
		}

		// Token: 0x0600234C RID: 9036 RVA: 0x0003B838 File Offset: 0x00039A38
		public static bool operator !=(RenderStateBlock left, RenderStateBlock right)
		{
			return !left.Equals(right);
		}

		// Token: 0x04000D17 RID: 3351
		private BlendState m_BlendState;

		// Token: 0x04000D18 RID: 3352
		private RasterState m_RasterState;

		// Token: 0x04000D19 RID: 3353
		private DepthState m_DepthState;

		// Token: 0x04000D1A RID: 3354
		private StencilState m_StencilState;

		// Token: 0x04000D1B RID: 3355
		private int m_StencilReference;

		// Token: 0x04000D1C RID: 3356
		private RenderStateMask m_Mask;
	}
}
