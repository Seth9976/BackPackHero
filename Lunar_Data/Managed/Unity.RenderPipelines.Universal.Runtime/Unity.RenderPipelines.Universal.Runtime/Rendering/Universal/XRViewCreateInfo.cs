using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020000EC RID: 236
	internal struct XRViewCreateInfo
	{
		// Token: 0x04000673 RID: 1651
		public Matrix4x4 projMatrix;

		// Token: 0x04000674 RID: 1652
		public Matrix4x4 viewMatrix;

		// Token: 0x04000675 RID: 1653
		public Rect viewport;

		// Token: 0x04000676 RID: 1654
		public int textureArraySlice;
	}
}
