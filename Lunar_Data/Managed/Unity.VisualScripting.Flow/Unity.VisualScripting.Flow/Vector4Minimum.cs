using System;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x0200011E RID: 286
	[UnitCategory("Math/Vector 4")]
	[UnitTitle("Minimum")]
	public sealed class Vector4Minimum : Minimum<Vector4>
	{
		// Token: 0x0600078B RID: 1931 RVA: 0x0000E0F8 File Offset: 0x0000C2F8
		public override Vector4 Operation(Vector4 a, Vector4 b)
		{
			return Vector4.Min(a, b);
		}

		// Token: 0x0600078C RID: 1932 RVA: 0x0000E104 File Offset: 0x0000C304
		public override Vector4 Operation(IEnumerable<Vector4> values)
		{
			bool flag = false;
			Vector4 vector = Vector4.zero;
			foreach (Vector4 vector2 in values)
			{
				if (!flag)
				{
					vector = vector2;
					flag = true;
				}
				else
				{
					vector = Vector4.Min(vector, vector2);
				}
			}
			return vector;
		}
	}
}
