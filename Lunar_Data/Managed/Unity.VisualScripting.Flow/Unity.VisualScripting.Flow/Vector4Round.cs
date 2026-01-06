using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x02000124 RID: 292
	[UnitCategory("Math/Vector 4")]
	[UnitTitle("Round")]
	public sealed class Vector4Round : Round<Vector4, Vector4>
	{
		// Token: 0x0600079D RID: 1949 RVA: 0x0000E248 File Offset: 0x0000C448
		protected override Vector4 Floor(Vector4 input)
		{
			return new Vector4(Mathf.Floor(input.x), Mathf.Floor(input.y), Mathf.Floor(input.z), Mathf.Floor(input.w));
		}

		// Token: 0x0600079E RID: 1950 RVA: 0x0000E27B File Offset: 0x0000C47B
		protected override Vector4 AwayFromZero(Vector4 input)
		{
			return new Vector4(Mathf.Round(input.x), Mathf.Round(input.y), Mathf.Round(input.z), Mathf.Round(input.w));
		}

		// Token: 0x0600079F RID: 1951 RVA: 0x0000E2AE File Offset: 0x0000C4AE
		protected override Vector4 Ceiling(Vector4 input)
		{
			return new Vector4(Mathf.Ceil(input.x), Mathf.Ceil(input.y), Mathf.Ceil(input.z), Mathf.Ceil(input.w));
		}
	}
}
