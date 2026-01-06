using System;

namespace Unity.VisualScripting
{
	// Token: 0x020000DF RID: 223
	[UnitCategory("Math/Scalar")]
	[UnitTitle("Divide")]
	public sealed class ScalarDivide : Divide<float>
	{
		// Token: 0x17000271 RID: 625
		// (get) Token: 0x060006B2 RID: 1714 RVA: 0x0000D1B2 File Offset: 0x0000B3B2
		protected override float defaultDividend
		{
			get
			{
				return 1f;
			}
		}

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x060006B3 RID: 1715 RVA: 0x0000D1B9 File Offset: 0x0000B3B9
		protected override float defaultDivisor
		{
			get
			{
				return 1f;
			}
		}

		// Token: 0x060006B4 RID: 1716 RVA: 0x0000D1C0 File Offset: 0x0000B3C0
		public override float Operation(float a, float b)
		{
			return a / b;
		}
	}
}
