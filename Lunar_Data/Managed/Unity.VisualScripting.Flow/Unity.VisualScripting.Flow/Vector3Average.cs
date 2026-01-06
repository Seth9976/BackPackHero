using System;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x02000105 RID: 261
	[UnitCategory("Math/Vector 3")]
	[UnitTitle("Average")]
	public sealed class Vector3Average : Average<Vector3>
	{
		// Token: 0x06000740 RID: 1856 RVA: 0x0000DB59 File Offset: 0x0000BD59
		public override Vector3 Operation(Vector3 a, Vector3 b)
		{
			return (a + b) / 2f;
		}

		// Token: 0x06000741 RID: 1857 RVA: 0x0000DB6C File Offset: 0x0000BD6C
		public override Vector3 Operation(IEnumerable<Vector3> values)
		{
			Vector3 vector = Vector3.zero;
			int num = 0;
			foreach (Vector3 vector2 in values)
			{
				vector += vector2;
				num++;
			}
			vector /= (float)num;
			return vector;
		}
	}
}
