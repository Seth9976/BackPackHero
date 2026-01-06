using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x020000E3 RID: 227
	[UnitCategory("Math/Scalar")]
	[UnitTitle("Minimum")]
	public sealed class ScalarMinimum : Minimum<float>
	{
		// Token: 0x060006C6 RID: 1734 RVA: 0x0000D2DA File Offset: 0x0000B4DA
		public override float Operation(float a, float b)
		{
			return Mathf.Min(a, b);
		}

		// Token: 0x060006C7 RID: 1735 RVA: 0x0000D2E3 File Offset: 0x0000B4E3
		public override float Operation(IEnumerable<float> values)
		{
			return values.Min();
		}
	}
}
