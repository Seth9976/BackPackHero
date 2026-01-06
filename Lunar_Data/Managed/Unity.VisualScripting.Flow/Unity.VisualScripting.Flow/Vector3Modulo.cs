using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x0200010D RID: 269
	[UnitCategory("Math/Vector 3")]
	[UnitTitle("Modulo")]
	public sealed class Vector3Modulo : Modulo<Vector3>
	{
		// Token: 0x1700029B RID: 667
		// (get) Token: 0x06000757 RID: 1879 RVA: 0x0000DD48 File Offset: 0x0000BF48
		protected override Vector3 defaultDividend
		{
			get
			{
				return Vector3.zero;
			}
		}

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06000758 RID: 1880 RVA: 0x0000DD4F File Offset: 0x0000BF4F
		protected override Vector3 defaultDivisor
		{
			get
			{
				return Vector3.zero;
			}
		}

		// Token: 0x06000759 RID: 1881 RVA: 0x0000DD56 File Offset: 0x0000BF56
		public override Vector3 Operation(Vector3 a, Vector3 b)
		{
			return new Vector3(a.x % b.x, a.y % b.y, a.z % b.z);
		}
	}
}
