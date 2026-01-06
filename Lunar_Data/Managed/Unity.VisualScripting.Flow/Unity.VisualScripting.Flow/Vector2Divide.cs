using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x020000F4 RID: 244
	[UnitCategory("Math/Vector 2")]
	[UnitTitle("Divide")]
	public sealed class Vector2Divide : Divide<Vector2>
	{
		// Token: 0x1700028A RID: 650
		// (get) Token: 0x0600070C RID: 1804 RVA: 0x0000D809 File Offset: 0x0000BA09
		protected override Vector2 defaultDividend
		{
			get
			{
				return Vector2.zero;
			}
		}

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x0600070D RID: 1805 RVA: 0x0000D810 File Offset: 0x0000BA10
		protected override Vector2 defaultDivisor
		{
			get
			{
				return Vector2.zero;
			}
		}

		// Token: 0x0600070E RID: 1806 RVA: 0x0000D817 File Offset: 0x0000BA17
		public override Vector2 Operation(Vector2 a, Vector2 b)
		{
			return new Vector2(a.x / b.x, a.y / b.y);
		}
	}
}
