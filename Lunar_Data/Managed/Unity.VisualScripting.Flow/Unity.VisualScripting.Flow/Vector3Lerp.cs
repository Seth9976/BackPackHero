using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x0200010A RID: 266
	[UnitCategory("Math/Vector 3")]
	[UnitTitle("Lerp")]
	public sealed class Vector3Lerp : Lerp<Vector3>
	{
		// Token: 0x17000299 RID: 665
		// (get) Token: 0x0600074D RID: 1869 RVA: 0x0000DC4B File Offset: 0x0000BE4B
		protected override Vector3 defaultA
		{
			get
			{
				return Vector3.zero;
			}
		}

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x0600074E RID: 1870 RVA: 0x0000DC52 File Offset: 0x0000BE52
		protected override Vector3 defaultB
		{
			get
			{
				return Vector3.one;
			}
		}

		// Token: 0x0600074F RID: 1871 RVA: 0x0000DC59 File Offset: 0x0000BE59
		public override Vector3 Operation(Vector3 a, Vector3 b, float t)
		{
			return Vector3.Lerp(a, b, t);
		}
	}
}
