using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x0200011C RID: 284
	[UnitCategory("Math/Vector 4")]
	[UnitTitle("Lerp")]
	public sealed class Vector4Lerp : Lerp<Vector4>
	{
		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x06000784 RID: 1924 RVA: 0x0000E06B File Offset: 0x0000C26B
		protected override Vector4 defaultA
		{
			get
			{
				return Vector4.zero;
			}
		}

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x06000785 RID: 1925 RVA: 0x0000E072 File Offset: 0x0000C272
		protected override Vector4 defaultB
		{
			get
			{
				return Vector4.one;
			}
		}

		// Token: 0x06000786 RID: 1926 RVA: 0x0000E079 File Offset: 0x0000C279
		public override Vector4 Operation(Vector4 a, Vector4 b, float t)
		{
			return Vector4.Lerp(a, b, t);
		}
	}
}
