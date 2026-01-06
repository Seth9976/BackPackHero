using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x020000F0 RID: 240
	[UnitCategory("Math/Vector 2")]
	[UnitTitle("Absolute")]
	public sealed class Vector2Absolute : Absolute<Vector2>
	{
		// Token: 0x06000703 RID: 1795 RVA: 0x0000D746 File Offset: 0x0000B946
		protected override Vector2 Operation(Vector2 input)
		{
			return new Vector2(Mathf.Abs(input.x), Mathf.Abs(input.y));
		}
	}
}
