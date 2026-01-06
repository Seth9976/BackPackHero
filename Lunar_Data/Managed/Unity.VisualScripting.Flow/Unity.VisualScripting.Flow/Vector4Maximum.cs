using System;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x0200011D RID: 285
	[UnitCategory("Math/Vector 4")]
	[UnitTitle("Maximum")]
	public sealed class Vector4Maximum : Maximum<Vector4>
	{
		// Token: 0x06000788 RID: 1928 RVA: 0x0000E08B File Offset: 0x0000C28B
		public override Vector4 Operation(Vector4 a, Vector4 b)
		{
			return Vector4.Max(a, b);
		}

		// Token: 0x06000789 RID: 1929 RVA: 0x0000E094 File Offset: 0x0000C294
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
					vector = Vector4.Max(vector, vector2);
				}
			}
			return vector;
		}
	}
}
