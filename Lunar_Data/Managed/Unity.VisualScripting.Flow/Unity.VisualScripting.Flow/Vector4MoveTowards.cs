using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x02000120 RID: 288
	[UnitCategory("Math/Vector 4")]
	[UnitTitle("Move Towards")]
	public sealed class Vector4MoveTowards : MoveTowards<Vector4>
	{
		// Token: 0x170002AA RID: 682
		// (get) Token: 0x06000792 RID: 1938 RVA: 0x0000E1B9 File Offset: 0x0000C3B9
		protected override Vector4 defaultCurrent
		{
			get
			{
				return Vector4.zero;
			}
		}

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x06000793 RID: 1939 RVA: 0x0000E1C0 File Offset: 0x0000C3C0
		protected override Vector4 defaultTarget
		{
			get
			{
				return Vector4.one;
			}
		}

		// Token: 0x06000794 RID: 1940 RVA: 0x0000E1C7 File Offset: 0x0000C3C7
		public override Vector4 Operation(Vector4 current, Vector4 target, float maxDelta)
		{
			return Vector4.MoveTowards(current, target, maxDelta);
		}
	}
}
