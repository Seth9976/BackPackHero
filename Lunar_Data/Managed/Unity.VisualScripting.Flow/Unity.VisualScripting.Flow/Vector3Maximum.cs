using System;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x0200010B RID: 267
	[UnitCategory("Math/Vector 3")]
	[UnitTitle("Maximum")]
	public sealed class Vector3Maximum : Maximum<Vector3>
	{
		// Token: 0x06000751 RID: 1873 RVA: 0x0000DC6B File Offset: 0x0000BE6B
		public override Vector3 Operation(Vector3 a, Vector3 b)
		{
			return Vector3.Max(a, b);
		}

		// Token: 0x06000752 RID: 1874 RVA: 0x0000DC74 File Offset: 0x0000BE74
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
					vector = Vector3.Max(vector, vector2);
				}
			}
			return vector;
		}
	}
}
