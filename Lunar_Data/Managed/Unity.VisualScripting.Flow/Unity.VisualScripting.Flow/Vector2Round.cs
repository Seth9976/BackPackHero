using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x020000FF RID: 255
	[UnitCategory("Math/Vector 2")]
	[UnitTitle("Round")]
	public sealed class Vector2Round : Round<Vector2, Vector2>
	{
		// Token: 0x0600072D RID: 1837 RVA: 0x0000DA1A File Offset: 0x0000BC1A
		protected override Vector2 Floor(Vector2 input)
		{
			return new Vector2(Mathf.Floor(input.x), Mathf.Floor(input.y));
		}

		// Token: 0x0600072E RID: 1838 RVA: 0x0000DA37 File Offset: 0x0000BC37
		protected override Vector2 AwayFromZero(Vector2 input)
		{
			return new Vector2(Mathf.Round(input.x), Mathf.Round(input.y));
		}

		// Token: 0x0600072F RID: 1839 RVA: 0x0000DA54 File Offset: 0x0000BC54
		protected override Vector2 Ceiling(Vector2 input)
		{
			return new Vector2(Mathf.Ceil(input.x), Mathf.Ceil(input.y));
		}
	}
}
