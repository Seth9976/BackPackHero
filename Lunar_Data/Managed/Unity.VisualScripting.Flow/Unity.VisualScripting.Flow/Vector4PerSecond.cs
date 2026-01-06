using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x02000123 RID: 291
	[UnitCategory("Math/Vector 4")]
	[UnitTitle("Per Second")]
	public sealed class Vector4PerSecond : PerSecond<Vector4>
	{
		// Token: 0x0600079B RID: 1947 RVA: 0x0000E233 File Offset: 0x0000C433
		public override Vector4 Operation(Vector4 input)
		{
			return input * Time.deltaTime;
		}
	}
}
