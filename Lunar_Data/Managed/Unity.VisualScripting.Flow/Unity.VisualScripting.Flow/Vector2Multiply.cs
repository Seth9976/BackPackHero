using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x020000FB RID: 251
	[UnitCategory("Math/Vector 2")]
	[UnitTitle("Multiply")]
	public sealed class Vector2Multiply : Multiply<Vector2>
	{
		// Token: 0x17000292 RID: 658
		// (get) Token: 0x06000724 RID: 1828 RVA: 0x0000D9A7 File Offset: 0x0000BBA7
		protected override Vector2 defaultB
		{
			get
			{
				return Vector2.zero;
			}
		}

		// Token: 0x06000725 RID: 1829 RVA: 0x0000D9AE File Offset: 0x0000BBAE
		public override Vector2 Operation(Vector2 a, Vector2 b)
		{
			return new Vector2(a.x * b.x, a.y * b.y);
		}
	}
}
