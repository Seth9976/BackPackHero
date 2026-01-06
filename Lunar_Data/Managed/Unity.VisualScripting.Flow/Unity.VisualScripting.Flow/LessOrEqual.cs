using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x020000BE RID: 190
	[UnitCategory("Logic")]
	[UnitOrder(10)]
	public sealed class LessOrEqual : BinaryComparisonUnit
	{
		// Token: 0x17000224 RID: 548
		// (get) Token: 0x060005AF RID: 1455 RVA: 0x0000BAE2 File Offset: 0x00009CE2
		[PortLabel("A ≤ B")]
		public override ValueOutput comparison
		{
			get
			{
				return base.comparison;
			}
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x0000BAEA File Offset: 0x00009CEA
		protected override bool NumericComparison(float a, float b)
		{
			return a < b || Mathf.Approximately(a, b);
		}

		// Token: 0x060005B1 RID: 1457 RVA: 0x0000BAF9 File Offset: 0x00009CF9
		protected override bool GenericComparison(object a, object b)
		{
			return OperatorUtility.LessThanOrEqual(a, b);
		}
	}
}
