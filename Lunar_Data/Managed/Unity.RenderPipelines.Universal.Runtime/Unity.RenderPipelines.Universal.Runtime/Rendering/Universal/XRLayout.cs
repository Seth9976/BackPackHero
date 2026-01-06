using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020000EA RID: 234
	internal struct XRLayout
	{
		// Token: 0x06000692 RID: 1682 RVA: 0x000259FC File Offset: 0x00023BFC
		internal XRPass CreatePass(XRPassCreateInfo passCreateInfo)
		{
			XRPass xrpass = XRPass.Create(passCreateInfo);
			this.xrSystem.AddPassToFrame(xrpass);
			return xrpass;
		}

		// Token: 0x06000693 RID: 1683 RVA: 0x00025A1D File Offset: 0x00023C1D
		internal void AddViewToPass(XRViewCreateInfo viewCreateInfo, XRPass pass)
		{
			pass.AddView(viewCreateInfo.projMatrix, viewCreateInfo.viewMatrix, viewCreateInfo.viewport, viewCreateInfo.textureArraySlice);
		}

		// Token: 0x0400066A RID: 1642
		internal Camera camera;

		// Token: 0x0400066B RID: 1643
		internal XRSystem xrSystem;
	}
}
