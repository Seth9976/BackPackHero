using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x020000DD RID: 221
	[UnitCategory("Math/Scalar")]
	[UnitTitle("Absolute")]
	public sealed class ScalarAbsolute : Absolute<float>
	{
		// Token: 0x060006AD RID: 1709 RVA: 0x0000D187 File Offset: 0x0000B387
		protected override float Operation(float input)
		{
			return Mathf.Abs(input);
		}
	}
}
