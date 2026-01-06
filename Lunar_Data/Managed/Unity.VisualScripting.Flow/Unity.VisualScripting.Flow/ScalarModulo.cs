using System;

namespace Unity.VisualScripting
{
	// Token: 0x020000E4 RID: 228
	[UnitCategory("Math/Scalar")]
	[UnitTitle("Modulo")]
	public sealed class ScalarModulo : Modulo<float>
	{
		// Token: 0x17000278 RID: 632
		// (get) Token: 0x060006C9 RID: 1737 RVA: 0x0000D2F3 File Offset: 0x0000B4F3
		protected override float defaultDividend
		{
			get
			{
				return 1f;
			}
		}

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x060006CA RID: 1738 RVA: 0x0000D2FA File Offset: 0x0000B4FA
		protected override float defaultDivisor
		{
			get
			{
				return 1f;
			}
		}

		// Token: 0x060006CB RID: 1739 RVA: 0x0000D301 File Offset: 0x0000B501
		public override float Operation(float a, float b)
		{
			return a % b;
		}
	}
}
