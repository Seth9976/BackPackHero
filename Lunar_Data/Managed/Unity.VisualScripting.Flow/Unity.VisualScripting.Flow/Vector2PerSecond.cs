using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x020000FD RID: 253
	[UnitCategory("Math/Vector 2")]
	[UnitTitle("Per Second")]
	public sealed class Vector2PerSecond : PerSecond<Vector2>
	{
		// Token: 0x06000729 RID: 1833 RVA: 0x0000D9E8 File Offset: 0x0000BBE8
		public override Vector2 Operation(Vector2 input)
		{
			return input * Time.deltaTime;
		}
	}
}
