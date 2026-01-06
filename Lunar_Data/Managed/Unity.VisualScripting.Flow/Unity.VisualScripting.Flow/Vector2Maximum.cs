using System;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x020000F7 RID: 247
	[UnitCategory("Math/Vector 2")]
	[UnitTitle("Maximum")]
	public sealed class Vector2Maximum : Maximum<Vector2>
	{
		// Token: 0x06000716 RID: 1814 RVA: 0x0000D871 File Offset: 0x0000BA71
		public override Vector2 Operation(Vector2 a, Vector2 b)
		{
			return Vector2.Max(a, b);
		}

		// Token: 0x06000717 RID: 1815 RVA: 0x0000D87C File Offset: 0x0000BA7C
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
					vector = Vector2.Max(vector, vector2);
				}
			}
			return vector;
		}
	}
}
