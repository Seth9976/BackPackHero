using System;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x0200010C RID: 268
	[UnitCategory("Math/Vector 3")]
	[UnitTitle("Minimum")]
	public sealed class Vector3Minimum : Minimum<Vector3>
	{
		// Token: 0x06000754 RID: 1876 RVA: 0x0000DCD8 File Offset: 0x0000BED8
		public override Vector3 Operation(Vector3 a, Vector3 b)
		{
			return Vector3.Min(a, b);
		}

		// Token: 0x06000755 RID: 1877 RVA: 0x0000DCE4 File Offset: 0x0000BEE4
		public override Vector3 Operation(IEnumerable<Vector3> values)
		{
			bool flag = false;
			Vector3 vector = Vector3.zero;
			foreach (Vector3 vector2 in values)
			{
				if (!flag)
				{
					vector = vector2;
					flag = true;
				}
				else
				{
					vector = Vector3.Min(vector, vector2);
				}
			}
			return vector;
		}
	}
}
