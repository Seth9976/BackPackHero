using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x020000E5 RID: 229
	[UnitCategory("Math/Scalar")]
	[UnitTitle("Move Towards")]
	public sealed class ScalarMoveTowards : MoveTowards<float>
	{
		// Token: 0x1700027A RID: 634
		// (get) Token: 0x060006CD RID: 1741 RVA: 0x0000D30E File Offset: 0x0000B50E
		protected override float defaultCurrent
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x060006CE RID: 1742 RVA: 0x0000D315 File Offset: 0x0000B515
		protected override float defaultTarget
		{
			get
			{
				return 1f;
			}
		}

		// Token: 0x060006CF RID: 1743 RVA: 0x0000D31C File Offset: 0x0000B51C
		public override float Operation(float current, float target, float maxDelta)
		{
			return Mathf.MoveTowards(current, target, maxDelta);
		}
	}
}
