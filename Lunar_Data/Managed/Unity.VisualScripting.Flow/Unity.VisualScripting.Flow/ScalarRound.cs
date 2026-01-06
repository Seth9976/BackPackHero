using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x020000EA RID: 234
	[UnitCategory("Math/Scalar")]
	[UnitTitle("Round")]
	public sealed class ScalarRound : Round<float, int>
	{
		// Token: 0x060006E1 RID: 1761 RVA: 0x0000D46F File Offset: 0x0000B66F
		protected override int Floor(float input)
		{
			return Mathf.FloorToInt(input);
		}

		// Token: 0x060006E2 RID: 1762 RVA: 0x0000D477 File Offset: 0x0000B677
		protected override int AwayFromZero(float input)
		{
			return Mathf.RoundToInt(input);
		}

		// Token: 0x060006E3 RID: 1763 RVA: 0x0000D47F File Offset: 0x0000B67F
		protected override int Ceiling(float input)
		{
			return Mathf.CeilToInt(input);
		}
	}
}
