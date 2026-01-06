using System;

namespace Unity.VisualScripting
{
	// Token: 0x020000E6 RID: 230
	[UnitCategory("Math/Scalar")]
	[UnitTitle("Multiply")]
	public sealed class ScalarMultiply : Multiply<float>
	{
		// Token: 0x1700027C RID: 636
		// (get) Token: 0x060006D1 RID: 1745 RVA: 0x0000D32E File Offset: 0x0000B52E
		protected override float defaultB
		{
			get
			{
				return 1f;
			}
		}

		// Token: 0x060006D2 RID: 1746 RVA: 0x0000D335 File Offset: 0x0000B535
		public override float Operation(float a, float b)
		{
			return a * b;
		}
	}
}
