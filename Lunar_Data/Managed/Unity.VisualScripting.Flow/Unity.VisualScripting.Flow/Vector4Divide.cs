using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x0200011A RID: 282
	[UnitCategory("Math/Vector 4")]
	[UnitTitle("Divide")]
	public sealed class Vector4Divide : Divide<Vector4>
	{
		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x0600077E RID: 1918 RVA: 0x0000E009 File Offset: 0x0000C209
		protected override Vector4 defaultDividend
		{
			get
			{
				return Vector4.zero;
			}
		}

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x0600077F RID: 1919 RVA: 0x0000E010 File Offset: 0x0000C210
		protected override Vector4 defaultDivisor
		{
			get
			{
				return Vector4.zero;
			}
		}

		// Token: 0x06000780 RID: 1920 RVA: 0x0000E017 File Offset: 0x0000C217
		public override Vector4 Operation(Vector4 a, Vector4 b)
		{
			return new Vector4(a.x / b.x, a.y / b.y, a.z / b.z, a.w / b.w);
		}
	}
}
