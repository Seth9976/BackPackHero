using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x02000106 RID: 262
	[UnitCategory("Math/Vector 3")]
	[UnitTitle("Cross Product")]
	public sealed class Vector3CrossProduct : CrossProduct<Vector3>
	{
		// Token: 0x06000743 RID: 1859 RVA: 0x0000DBD4 File Offset: 0x0000BDD4
		public override Vector3 Operation(Vector3 a, Vector3 b)
		{
			return Vector3.Cross(a, b);
		}
	}
}
