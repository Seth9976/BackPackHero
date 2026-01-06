using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x02000109 RID: 265
	[UnitCategory("Math/Vector 3")]
	[UnitTitle("Dot Product")]
	public sealed class Vector3DotProduct : DotProduct<Vector3>
	{
		// Token: 0x0600074B RID: 1867 RVA: 0x0000DC3A File Offset: 0x0000BE3A
		public override float Operation(Vector3 a, Vector3 b)
		{
			return Vector3.Dot(a, b);
		}
	}
}
