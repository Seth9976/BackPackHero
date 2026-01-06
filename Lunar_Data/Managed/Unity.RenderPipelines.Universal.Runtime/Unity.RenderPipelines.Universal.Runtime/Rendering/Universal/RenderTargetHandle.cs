using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020000B6 RID: 182
	public struct RenderTargetHandle
	{
		// Token: 0x17000162 RID: 354
		// (get) Token: 0x06000572 RID: 1394 RVA: 0x0001F46A File Offset: 0x0001D66A
		// (set) Token: 0x06000571 RID: 1393 RVA: 0x0001F461 File Offset: 0x0001D661
		public int id { readonly get; set; }

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x06000574 RID: 1396 RVA: 0x0001F47B File Offset: 0x0001D67B
		// (set) Token: 0x06000573 RID: 1395 RVA: 0x0001F472 File Offset: 0x0001D672
		private RenderTargetIdentifier rtid { readonly get; set; }

		// Token: 0x06000575 RID: 1397 RVA: 0x0001F483 File Offset: 0x0001D683
		public RenderTargetHandle(RenderTargetIdentifier renderTargetIdentifier)
		{
			this.id = -2;
			this.rtid = renderTargetIdentifier;
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x0001F494 File Offset: 0x0001D694
		internal static RenderTargetHandle GetCameraTarget(XRPass xr)
		{
			if (xr.enabled)
			{
				return new RenderTargetHandle(xr.renderTarget);
			}
			return RenderTargetHandle.CameraTarget;
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x0001F4AF File Offset: 0x0001D6AF
		public void Init(string shaderProperty)
		{
			this.id = Shader.PropertyToID(shaderProperty);
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x0001F4BD File Offset: 0x0001D6BD
		public void Init(RenderTargetIdentifier renderTargetIdentifier)
		{
			this.id = -2;
			this.rtid = renderTargetIdentifier;
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x0001F4CE File Offset: 0x0001D6CE
		public RenderTargetIdentifier Identifier()
		{
			if (this.id == -1)
			{
				return BuiltinRenderTextureType.CameraTarget;
			}
			if (this.id == -2)
			{
				return this.rtid;
			}
			return new RenderTargetIdentifier(this.id, 0, CubemapFace.Unknown, -1);
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x0001F4FF File Offset: 0x0001D6FF
		public bool HasInternalRenderTargetId()
		{
			return this.id == -2;
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x0001F50B File Offset: 0x0001D70B
		public bool Equals(RenderTargetHandle other)
		{
			if (this.id == -2 || other.id == -2)
			{
				return this.Identifier() == other.Identifier();
			}
			return this.id == other.id;
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x0001F544 File Offset: 0x0001D744
		public override bool Equals(object obj)
		{
			return obj != null && obj is RenderTargetHandle && this.Equals((RenderTargetHandle)obj);
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x0001F561 File Offset: 0x0001D761
		public override int GetHashCode()
		{
			return this.id;
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x0001F569 File Offset: 0x0001D769
		public static bool operator ==(RenderTargetHandle c1, RenderTargetHandle c2)
		{
			return c1.Equals(c2);
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x0001F573 File Offset: 0x0001D773
		public static bool operator !=(RenderTargetHandle c1, RenderTargetHandle c2)
		{
			return !c1.Equals(c2);
		}

		// Token: 0x0400045E RID: 1118
		public static readonly RenderTargetHandle CameraTarget = new RenderTargetHandle
		{
			id = -1
		};
	}
}
