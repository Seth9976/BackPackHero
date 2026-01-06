using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x0200011F RID: 287
	[UnitCategory("Math/Vector 4")]
	[UnitTitle("Modulo")]
	public sealed class Vector4Modulo : Modulo<Vector4>
	{
		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x0600078E RID: 1934 RVA: 0x0000E168 File Offset: 0x0000C368
		protected override Vector4 defaultDividend
		{
			get
			{
				return Vector4.zero;
			}
		}

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x0600078F RID: 1935 RVA: 0x0000E16F File Offset: 0x0000C36F
		protected override Vector4 defaultDivisor
		{
			get
			{
				return Vector4.zero;
			}
		}

		// Token: 0x06000790 RID: 1936 RVA: 0x0000E176 File Offset: 0x0000C376
		public override Vector4 Operation(Vector4 a, Vector4 b)
		{
			return new Vector4(a.x % b.x, a.y % b.y, a.z % b.z, a.w % b.w);
		}
	}
}
