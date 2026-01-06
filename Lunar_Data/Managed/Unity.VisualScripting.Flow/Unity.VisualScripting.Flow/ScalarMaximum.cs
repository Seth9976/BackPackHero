using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x020000E2 RID: 226
	[UnitCategory("Math/Scalar")]
	[UnitTitle("Maximum")]
	public sealed class ScalarMaximum : Maximum<float>
	{
		// Token: 0x060006C3 RID: 1731 RVA: 0x0000D2C1 File Offset: 0x0000B4C1
		public override float Operation(float a, float b)
		{
			return Mathf.Max(a, b);
		}

		// Token: 0x060006C4 RID: 1732 RVA: 0x0000D2CA File Offset: 0x0000B4CA
		public override float Operation(IEnumerable<float> values)
		{
			return values.Max();
		}
	}
}
