using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x020000F5 RID: 245
	[UnitCategory("Math/Vector 2")]
	[UnitTitle("Dot Product")]
	public sealed class Vector2DotProduct : DotProduct<Vector2>
	{
		// Token: 0x06000710 RID: 1808 RVA: 0x0000D840 File Offset: 0x0000BA40
		public override float Operation(Vector2 a, Vector2 b)
		{
			return Vector2.Dot(a, b);
		}
	}
}
