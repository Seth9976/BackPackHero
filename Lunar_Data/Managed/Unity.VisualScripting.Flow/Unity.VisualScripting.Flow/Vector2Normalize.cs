using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x020000FC RID: 252
	[UnitCategory("Math/Vector 2")]
	[UnitTitle("Normalize")]
	public sealed class Vector2Normalize : Normalize<Vector2>
	{
		// Token: 0x06000727 RID: 1831 RVA: 0x0000D9D7 File Offset: 0x0000BBD7
		public override Vector2 Operation(Vector2 input)
		{
			return input.normalized;
		}
	}
}
