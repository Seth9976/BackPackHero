using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x020000E7 RID: 231
	[UnitCategory("Math/Scalar")]
	[UnitTitle("Normalize")]
	public sealed class ScalarNormalize : Normalize<float>
	{
		// Token: 0x060006D4 RID: 1748 RVA: 0x0000D342 File Offset: 0x0000B542
		public override float Operation(float input)
		{
			if (input == 0f)
			{
				return 0f;
			}
			return input / Mathf.Abs(input);
		}
	}
}
