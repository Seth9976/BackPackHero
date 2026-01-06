using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x020000E1 RID: 225
	[UnitCategory("Math/Scalar")]
	[UnitTitle("Lerp")]
	public sealed class ScalarLerp : Lerp<float>
	{
		// Token: 0x17000276 RID: 630
		// (get) Token: 0x060006BF RID: 1727 RVA: 0x0000D2A1 File Offset: 0x0000B4A1
		protected override float defaultA
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x060006C0 RID: 1728 RVA: 0x0000D2A8 File Offset: 0x0000B4A8
		protected override float defaultB
		{
			get
			{
				return 1f;
			}
		}

		// Token: 0x060006C1 RID: 1729 RVA: 0x0000D2AF File Offset: 0x0000B4AF
		public override float Operation(float a, float b, float t)
		{
			return Mathf.Lerp(a, b, t);
		}
	}
}
