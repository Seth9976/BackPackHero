using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x02000119 RID: 281
	[UnitCategory("Math/Vector 4")]
	[UnitTitle("Distance")]
	public sealed class Vector4Distance : Distance<Vector4>
	{
		// Token: 0x0600077C RID: 1916 RVA: 0x0000DFF8 File Offset: 0x0000C1F8
		public override float Operation(Vector4 a, Vector4 b)
		{
			return Vector4.Distance(a, b);
		}
	}
}
