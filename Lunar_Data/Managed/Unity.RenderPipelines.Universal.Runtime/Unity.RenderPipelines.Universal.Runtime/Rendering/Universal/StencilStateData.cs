using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x0200003B RID: 59
	[Serializable]
	public class StencilStateData
	{
		// Token: 0x0400018B RID: 395
		public bool overrideStencilState;

		// Token: 0x0400018C RID: 396
		public int stencilReference;

		// Token: 0x0400018D RID: 397
		public CompareFunction stencilCompareFunction = CompareFunction.Always;

		// Token: 0x0400018E RID: 398
		public StencilOp passOperation;

		// Token: 0x0400018F RID: 399
		public StencilOp failOperation;

		// Token: 0x04000190 RID: 400
		public StencilOp zFailOperation;
	}
}
