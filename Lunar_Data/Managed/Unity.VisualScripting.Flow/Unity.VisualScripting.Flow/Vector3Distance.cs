using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x02000107 RID: 263
	[UnitCategory("Math/Vector 3")]
	[UnitTitle("Distance")]
	public sealed class Vector3Distance : Distance<Vector3>
	{
		// Token: 0x06000745 RID: 1861 RVA: 0x0000DBE5 File Offset: 0x0000BDE5
		public override float Operation(Vector3 a, Vector3 b)
		{
			return Vector3.Distance(a, b);
		}
	}
}
