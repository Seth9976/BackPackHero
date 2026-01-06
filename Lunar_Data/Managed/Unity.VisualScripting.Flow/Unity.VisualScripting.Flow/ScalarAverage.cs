using System;
using System.Collections.Generic;
using System.Linq;

namespace Unity.VisualScripting
{
	// Token: 0x020000DE RID: 222
	[UnitCategory("Math/Scalar")]
	[UnitTitle("Average")]
	public sealed class ScalarAverage : Average<float>
	{
		// Token: 0x060006AF RID: 1711 RVA: 0x0000D197 File Offset: 0x0000B397
		public override float Operation(float a, float b)
		{
			return (a + b) / 2f;
		}

		// Token: 0x060006B0 RID: 1712 RVA: 0x0000D1A2 File Offset: 0x0000B3A2
		public override float Operation(IEnumerable<float> values)
		{
			return values.Average();
		}
	}
}
