using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x020000F1 RID: 241
	[UnitCategory("Math/Vector 2")]
	[UnitTitle("Angle")]
	public sealed class Vector2Angle : Angle<Vector2>
	{
		// Token: 0x06000705 RID: 1797 RVA: 0x0000D76B File Offset: 0x0000B96B
		public override float Operation(Vector2 a, Vector2 b)
		{
			return Vector2.Angle(a, b);
		}
	}
}
