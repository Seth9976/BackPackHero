using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x0200011B RID: 283
	[UnitCategory("Math/Vector 4")]
	[UnitTitle("Dot Product")]
	public sealed class Vector4DotProduct : DotProduct<Vector4>
	{
		// Token: 0x06000782 RID: 1922 RVA: 0x0000E05A File Offset: 0x0000C25A
		public override float Operation(Vector4 a, Vector4 b)
		{
			return Vector4.Dot(a, b);
		}
	}
}
