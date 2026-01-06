using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x02000114 RID: 276
	[UnitCategory("Math/Vector 3")]
	[UnitTitle("Subtract")]
	public sealed class Vector3Subtract : Subtract<Vector3>
	{
		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x0600076C RID: 1900 RVA: 0x0000DE9F File Offset: 0x0000C09F
		protected override Vector3 defaultMinuend
		{
			get
			{
				return Vector3.zero;
			}
		}

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x0600076D RID: 1901 RVA: 0x0000DEA6 File Offset: 0x0000C0A6
		protected override Vector3 defaultSubtrahend
		{
			get
			{
				return Vector3.zero;
			}
		}

		// Token: 0x0600076E RID: 1902 RVA: 0x0000DEAD File Offset: 0x0000C0AD
		public override Vector3 Operation(Vector3 a, Vector3 b)
		{
			return a - b;
		}
	}
}
