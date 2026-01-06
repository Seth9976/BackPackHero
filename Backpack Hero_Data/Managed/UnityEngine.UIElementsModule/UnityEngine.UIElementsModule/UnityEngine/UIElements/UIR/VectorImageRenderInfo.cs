using System;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000323 RID: 803
	internal class VectorImageRenderInfo : LinkedPoolItem<VectorImageRenderInfo>
	{
		// Token: 0x060019F5 RID: 6645 RVA: 0x0006E594 File Offset: 0x0006C794
		public void Reset()
		{
			this.useCount = 0;
			this.firstGradientRemap = null;
			this.gradientSettingsAlloc = default(Alloc);
		}

		// Token: 0x04000BD5 RID: 3029
		public int useCount;

		// Token: 0x04000BD6 RID: 3030
		public GradientRemap firstGradientRemap;

		// Token: 0x04000BD7 RID: 3031
		public Alloc gradientSettingsAlloc;
	}
}
