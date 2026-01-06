using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x020000F3 RID: 243
	[UnitCategory("Math/Vector 2")]
	[UnitTitle("Distance")]
	public sealed class Vector2Distance : Distance<Vector2>
	{
		// Token: 0x0600070A RID: 1802 RVA: 0x0000D7F8 File Offset: 0x0000B9F8
		public override float Operation(Vector2 a, Vector2 b)
		{
			return Vector2.Distance(a, b);
		}
	}
}
