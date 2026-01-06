using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x020000FE RID: 254
	[UnitCategory("Math/Vector 2")]
	[UnitTitle("Project")]
	public sealed class Vector2Project : Project<Vector2>
	{
		// Token: 0x0600072B RID: 1835 RVA: 0x0000D9FD File Offset: 0x0000BBFD
		public override Vector2 Operation(Vector2 a, Vector2 b)
		{
			return Vector2.Dot(a, b) * b.normalized;
		}
	}
}
