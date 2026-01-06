using System;
using UnityEngine.XR;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020000ED RID: 237
	internal struct XRView
	{
		// Token: 0x06000694 RID: 1684 RVA: 0x00025A3D File Offset: 0x00023C3D
		internal XRView(Matrix4x4 proj, Matrix4x4 view, Rect vp, int dstSlice)
		{
			this.projMatrix = proj;
			this.viewMatrix = view;
			this.viewport = vp;
			this.occlusionMesh = null;
			this.textureArraySlice = dstSlice;
		}

		// Token: 0x06000695 RID: 1685 RVA: 0x00025A64 File Offset: 0x00023C64
		internal XRView(XRDisplaySubsystem.XRRenderPass renderPass, XRDisplaySubsystem.XRRenderParameter renderParameter)
		{
			this.projMatrix = renderParameter.projection;
			this.viewMatrix = renderParameter.view;
			this.viewport = renderParameter.viewport;
			this.occlusionMesh = renderParameter.occlusionMesh;
			this.textureArraySlice = renderParameter.textureArraySlice;
			this.viewport.x = this.viewport.x * (float)renderPass.renderTargetDesc.width;
			this.viewport.width = this.viewport.width * (float)renderPass.renderTargetDesc.width;
			this.viewport.y = this.viewport.y * (float)renderPass.renderTargetDesc.height;
			this.viewport.height = this.viewport.height * (float)renderPass.renderTargetDesc.height;
		}

		// Token: 0x04000677 RID: 1655
		internal readonly Matrix4x4 projMatrix;

		// Token: 0x04000678 RID: 1656
		internal readonly Matrix4x4 viewMatrix;

		// Token: 0x04000679 RID: 1657
		internal readonly Rect viewport;

		// Token: 0x0400067A RID: 1658
		internal readonly Mesh occlusionMesh;

		// Token: 0x0400067B RID: 1659
		internal readonly int textureArraySlice;
	}
}
