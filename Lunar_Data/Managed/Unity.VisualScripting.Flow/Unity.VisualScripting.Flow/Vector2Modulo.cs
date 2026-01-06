using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x020000F9 RID: 249
	[UnitCategory("Math/Vector 2")]
	[UnitTitle("Modulo")]
	public sealed class Vector2Modulo : Modulo<Vector2>
	{
		// Token: 0x1700028E RID: 654
		// (get) Token: 0x0600071C RID: 1820 RVA: 0x0000D950 File Offset: 0x0000BB50
		protected override Vector2 defaultDividend
		{
			get
			{
				return Vector2.zero;
			}
		}

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x0600071D RID: 1821 RVA: 0x0000D957 File Offset: 0x0000BB57
		protected override Vector2 defaultDivisor
		{
			get
			{
				return Vector2.zero;
			}
		}

		// Token: 0x0600071E RID: 1822 RVA: 0x0000D95E File Offset: 0x0000BB5E
		public override Vector2 Operation(Vector2 a, Vector2 b)
		{
			return new Vector2(a.x % b.x, a.y % b.y);
		}
	}
}
