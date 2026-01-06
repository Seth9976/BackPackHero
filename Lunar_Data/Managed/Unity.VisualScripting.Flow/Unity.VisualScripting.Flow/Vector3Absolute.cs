using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x02000103 RID: 259
	[UnitCategory("Math/Vector 3")]
	[UnitTitle("Absolute")]
	public sealed class Vector3Absolute : Absolute<Vector3>
	{
		// Token: 0x0600073C RID: 1852 RVA: 0x0000DB18 File Offset: 0x0000BD18
		protected override Vector3 Operation(Vector3 input)
		{
			return new Vector3(Mathf.Abs(input.x), Mathf.Abs(input.y), Mathf.Abs(input.z));
		}
	}
}
