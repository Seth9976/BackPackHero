using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x0200010E RID: 270
	[UnitCategory("Math/Vector 3")]
	[UnitTitle("Move Towards")]
	public sealed class Vector3MoveTowards : MoveTowards<Vector3>
	{
		// Token: 0x1700029D RID: 669
		// (get) Token: 0x0600075B RID: 1883 RVA: 0x0000DD8C File Offset: 0x0000BF8C
		protected override Vector3 defaultCurrent
		{
			get
			{
				return Vector3.zero;
			}
		}

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x0600075C RID: 1884 RVA: 0x0000DD93 File Offset: 0x0000BF93
		protected override Vector3 defaultTarget
		{
			get
			{
				return Vector3.one;
			}
		}

		// Token: 0x0600075D RID: 1885 RVA: 0x0000DD9A File Offset: 0x0000BF9A
		public override Vector3 Operation(Vector3 current, Vector3 target, float maxDelta)
		{
			return Vector3.MoveTowards(current, target, maxDelta);
		}
	}
}
