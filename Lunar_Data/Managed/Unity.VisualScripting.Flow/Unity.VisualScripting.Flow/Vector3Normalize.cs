using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x02000110 RID: 272
	[UnitCategory("Math/Vector 3")]
	[UnitTitle("Normalize")]
	public sealed class Vector3Normalize : Normalize<Vector3>
	{
		// Token: 0x06000762 RID: 1890 RVA: 0x0000DDE9 File Offset: 0x0000BFE9
		public override Vector3 Operation(Vector3 input)
		{
			return Vector3.Normalize(input);
		}
	}
}
