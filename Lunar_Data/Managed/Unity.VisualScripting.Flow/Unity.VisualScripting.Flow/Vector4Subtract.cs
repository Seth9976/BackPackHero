using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x02000125 RID: 293
	[UnitCategory("Math/Vector 4")]
	[UnitTitle("Subtract")]
	public sealed class Vector4Subtract : Subtract<Vector4>
	{
		// Token: 0x170002AD RID: 685
		// (get) Token: 0x060007A1 RID: 1953 RVA: 0x0000E2E9 File Offset: 0x0000C4E9
		protected override Vector4 defaultMinuend
		{
			get
			{
				return Vector4.zero;
			}
		}

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x060007A2 RID: 1954 RVA: 0x0000E2F0 File Offset: 0x0000C4F0
		protected override Vector4 defaultSubtrahend
		{
			get
			{
				return Vector4.zero;
			}
		}

		// Token: 0x060007A3 RID: 1955 RVA: 0x0000E2F7 File Offset: 0x0000C4F7
		public override Vector4 Operation(Vector4 a, Vector4 b)
		{
			return a - b;
		}
	}
}
