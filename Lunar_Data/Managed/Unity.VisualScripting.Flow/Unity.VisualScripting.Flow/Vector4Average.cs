using System;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x02000118 RID: 280
	[UnitCategory("Math/Vector 4")]
	[UnitTitle("Average")]
	public sealed class Vector4Average : Average<Vector4>
	{
		// Token: 0x06000779 RID: 1913 RVA: 0x0000DF7B File Offset: 0x0000C17B
		public override Vector4 Operation(Vector4 a, Vector4 b)
		{
			return (a + b) / 2f;
		}

		// Token: 0x0600077A RID: 1914 RVA: 0x0000DF90 File Offset: 0x0000C190
		public override Vector4 Operation(IEnumerable<Vector4> values)
		{
			Vector4 vector = Vector4.zero;
			int num = 0;
			foreach (Vector4 vector2 in values)
			{
				vector += vector2;
				num++;
			}
			vector /= (float)num;
			return vector;
		}
	}
}
