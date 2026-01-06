using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x02000122 RID: 290
	[UnitCategory("Math/Vector 4")]
	[UnitTitle("Normalize")]
	public sealed class Vector4Normalize : Normalize<Vector4>
	{
		// Token: 0x06000799 RID: 1945 RVA: 0x0000E223 File Offset: 0x0000C423
		public override Vector4 Operation(Vector4 input)
		{
			return Vector4.Normalize(input);
		}
	}
}
