using System;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000326 RID: 806
	internal class GradientRemap : LinkedPoolItem<GradientRemap>
	{
		// Token: 0x060019FC RID: 6652 RVA: 0x0006E630 File Offset: 0x0006C830
		public void Reset()
		{
			this.origIndex = 0;
			this.destIndex = 0;
			this.location = default(RectInt);
			this.atlas = TextureId.invalid;
		}

		// Token: 0x04000BDB RID: 3035
		public int origIndex;

		// Token: 0x04000BDC RID: 3036
		public int destIndex;

		// Token: 0x04000BDD RID: 3037
		public RectInt location;

		// Token: 0x04000BDE RID: 3038
		public GradientRemap next;

		// Token: 0x04000BDF RID: 3039
		public TextureId atlas;
	}
}
