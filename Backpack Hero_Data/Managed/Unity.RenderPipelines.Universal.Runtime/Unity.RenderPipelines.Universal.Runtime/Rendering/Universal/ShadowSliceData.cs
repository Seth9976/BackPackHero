using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020000BD RID: 189
	public struct ShadowSliceData
	{
		// Token: 0x060005A5 RID: 1445 RVA: 0x0001F8E8 File Offset: 0x0001DAE8
		public void Clear()
		{
			this.viewMatrix = Matrix4x4.identity;
			this.projectionMatrix = Matrix4x4.identity;
			this.shadowTransform = Matrix4x4.identity;
			this.offsetX = (this.offsetY = 0);
			this.resolution = 1024;
		}

		// Token: 0x0400047B RID: 1147
		public Matrix4x4 viewMatrix;

		// Token: 0x0400047C RID: 1148
		public Matrix4x4 projectionMatrix;

		// Token: 0x0400047D RID: 1149
		public Matrix4x4 shadowTransform;

		// Token: 0x0400047E RID: 1150
		public int offsetX;

		// Token: 0x0400047F RID: 1151
		public int offsetY;

		// Token: 0x04000480 RID: 1152
		public int resolution;

		// Token: 0x04000481 RID: 1153
		public ShadowSplitData splitData;
	}
}
