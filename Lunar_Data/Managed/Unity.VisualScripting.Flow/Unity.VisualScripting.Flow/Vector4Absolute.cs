using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x02000117 RID: 279
	[UnitCategory("Math/Vector 4")]
	[UnitTitle("Absolute")]
	public sealed class Vector4Absolute : Absolute<Vector4>
	{
		// Token: 0x06000777 RID: 1911 RVA: 0x0000DF40 File Offset: 0x0000C140
		protected override Vector4 Operation(Vector4 input)
		{
			return new Vector4(Mathf.Abs(input.x), Mathf.Abs(input.y), Mathf.Abs(input.z), Mathf.Abs(input.w));
		}
	}
}
