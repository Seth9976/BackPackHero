using System;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000324 RID: 804
	internal class GradientRemapPool : LinkedPool<GradientRemap>
	{
		// Token: 0x060019F7 RID: 6647 RVA: 0x0006E5BC File Offset: 0x0006C7BC
		public GradientRemapPool()
			: base(() => new GradientRemap(), delegate(GradientRemap gradientRemap)
			{
				gradientRemap.Reset();
			}, 10000)
		{
		}
	}
}
