using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x020000F6 RID: 246
	[UnitCategory("Math/Vector 2")]
	[UnitTitle("Lerp")]
	public sealed class Vector2Lerp : Lerp<Vector2>
	{
		// Token: 0x1700028C RID: 652
		// (get) Token: 0x06000712 RID: 1810 RVA: 0x0000D851 File Offset: 0x0000BA51
		protected override Vector2 defaultA
		{
			get
			{
				return Vector2.zero;
			}
		}

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x06000713 RID: 1811 RVA: 0x0000D858 File Offset: 0x0000BA58
		protected override Vector2 defaultB
		{
			get
			{
				return Vector2.one;
			}
		}

		// Token: 0x06000714 RID: 1812 RVA: 0x0000D85F File Offset: 0x0000BA5F
		public override Vector2 Operation(Vector2 a, Vector2 b, float t)
		{
			return Vector2.Lerp(a, b, t);
		}
	}
}
