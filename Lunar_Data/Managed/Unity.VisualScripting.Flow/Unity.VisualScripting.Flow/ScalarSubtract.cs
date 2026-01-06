using System;

namespace Unity.VisualScripting
{
	// Token: 0x020000EB RID: 235
	[UnitCategory("Math/Scalar")]
	[UnitTitle("Subtract")]
	public sealed class ScalarSubtract : Subtract<float>
	{
		// Token: 0x17000280 RID: 640
		// (get) Token: 0x060006E5 RID: 1765 RVA: 0x0000D48F File Offset: 0x0000B68F
		protected override float defaultMinuend
		{
			get
			{
				return 1f;
			}
		}

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x060006E6 RID: 1766 RVA: 0x0000D496 File Offset: 0x0000B696
		protected override float defaultSubtrahend
		{
			get
			{
				return 1f;
			}
		}

		// Token: 0x060006E7 RID: 1767 RVA: 0x0000D49D File Offset: 0x0000B69D
		public override float Operation(float a, float b)
		{
			return a - b;
		}
	}
}
