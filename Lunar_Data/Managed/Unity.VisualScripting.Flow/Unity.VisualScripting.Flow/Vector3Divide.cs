using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x02000108 RID: 264
	[UnitCategory("Math/Vector 3")]
	[UnitTitle("Divide")]
	public sealed class Vector3Divide : Divide<Vector3>
	{
		// Token: 0x17000297 RID: 663
		// (get) Token: 0x06000747 RID: 1863 RVA: 0x0000DBF6 File Offset: 0x0000BDF6
		protected override Vector3 defaultDividend
		{
			get
			{
				return Vector3.zero;
			}
		}

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x06000748 RID: 1864 RVA: 0x0000DBFD File Offset: 0x0000BDFD
		protected override Vector3 defaultDivisor
		{
			get
			{
				return Vector3.zero;
			}
		}

		// Token: 0x06000749 RID: 1865 RVA: 0x0000DC04 File Offset: 0x0000BE04
		public override Vector3 Operation(Vector3 a, Vector3 b)
		{
			return new Vector3(a.x / b.x, a.y / b.y, a.z / b.z);
		}
	}
}
