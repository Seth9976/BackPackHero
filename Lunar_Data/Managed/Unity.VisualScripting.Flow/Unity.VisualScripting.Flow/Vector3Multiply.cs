using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x0200010F RID: 271
	[UnitCategory("Math/Vector 3")]
	[UnitTitle("Multiply")]
	public sealed class Vector3Multiply : Multiply<Vector3>
	{
		// Token: 0x1700029F RID: 671
		// (get) Token: 0x0600075F RID: 1887 RVA: 0x0000DDAC File Offset: 0x0000BFAC
		protected override Vector3 defaultB
		{
			get
			{
				return Vector3.zero;
			}
		}

		// Token: 0x06000760 RID: 1888 RVA: 0x0000DDB3 File Offset: 0x0000BFB3
		public override Vector3 Operation(Vector3 a, Vector3 b)
		{
			return new Vector3(a.x * b.x, a.y * b.y, a.z * b.z);
		}
	}
}
