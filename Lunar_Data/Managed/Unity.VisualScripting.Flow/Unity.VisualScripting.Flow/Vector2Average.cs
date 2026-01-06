using System;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x020000F2 RID: 242
	[UnitCategory("Math/Vector 2")]
	[UnitTitle("Average")]
	public sealed class Vector2Average : Average<Vector2>
	{
		// Token: 0x06000707 RID: 1799 RVA: 0x0000D77C File Offset: 0x0000B97C
		public override Vector2 Operation(Vector2 a, Vector2 b)
		{
			return (a + b) / 2f;
		}

		// Token: 0x06000708 RID: 1800 RVA: 0x0000D790 File Offset: 0x0000B990
		public override Vector2 Operation(IEnumerable<Vector2> values)
		{
			Vector2 vector = Vector2.zero;
			int num = 0;
			foreach (Vector2 vector2 in values)
			{
				vector += vector2;
				num++;
			}
			vector /= (float)num;
			return vector;
		}
	}
}
