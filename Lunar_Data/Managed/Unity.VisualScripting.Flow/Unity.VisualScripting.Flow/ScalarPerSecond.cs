using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x020000E8 RID: 232
	[UnitCategory("Math/Scalar")]
	[UnitTitle("Per Second")]
	public sealed class ScalarPerSecond : PerSecond<float>
	{
		// Token: 0x060006D6 RID: 1750 RVA: 0x0000D362 File Offset: 0x0000B562
		public override float Operation(float input)
		{
			return input * Time.deltaTime;
		}
	}
}
