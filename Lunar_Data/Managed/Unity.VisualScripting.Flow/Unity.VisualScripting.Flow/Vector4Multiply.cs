using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x02000121 RID: 289
	[UnitCategory("Math/Vector 4")]
	[UnitTitle("Multiply")]
	public sealed class Vector4Multiply : Multiply<Vector4>
	{
		// Token: 0x170002AC RID: 684
		// (get) Token: 0x06000796 RID: 1942 RVA: 0x0000E1D9 File Offset: 0x0000C3D9
		protected override Vector4 defaultB
		{
			get
			{
				return Vector4.zero;
			}
		}

		// Token: 0x06000797 RID: 1943 RVA: 0x0000E1E0 File Offset: 0x0000C3E0
		public override Vector4 Operation(Vector4 a, Vector4 b)
		{
			return new Vector4(a.x * b.x, a.y * b.y, a.z * b.z, a.w * b.w);
		}
	}
}
