using System;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x020000F8 RID: 248
	[UnitCategory("Math/Vector 2")]
	[UnitTitle("Minimum")]
	public sealed class Vector2Minimum : Minimum<Vector2>
	{
		// Token: 0x06000719 RID: 1817 RVA: 0x0000D8E0 File Offset: 0x0000BAE0
		public override Vector2 Operation(Vector2 a, Vector2 b)
		{
			return Vector2.Min(a, b);
		}

		// Token: 0x0600071A RID: 1818 RVA: 0x0000D8EC File Offset: 0x0000BAEC
		public override Vector2 Operation(IEnumerable<Vector2> values)
		{
			bool flag = false;
			Vector2 vector = Vector2.zero;
			foreach (Vector2 vector2 in values)
			{
				if (!flag)
				{
					vector = vector2;
					flag = true;
				}
				else
				{
					vector = Vector2.Min(vector, vector2);
				}
			}
			return vector;
		}
	}
}
