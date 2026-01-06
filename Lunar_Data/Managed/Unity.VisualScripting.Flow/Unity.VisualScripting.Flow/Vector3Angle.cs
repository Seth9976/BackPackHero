using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x02000104 RID: 260
	[UnitCategory("Math/Vector 3")]
	[UnitTitle("Angle")]
	public sealed class Vector3Angle : Angle<Vector3>
	{
		// Token: 0x0600073E RID: 1854 RVA: 0x0000DB48 File Offset: 0x0000BD48
		public override float Operation(Vector3 a, Vector3 b)
		{
			return Vector3.Angle(a, b);
		}
	}
}
