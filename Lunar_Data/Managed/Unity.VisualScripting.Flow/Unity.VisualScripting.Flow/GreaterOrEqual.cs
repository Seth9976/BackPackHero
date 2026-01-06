using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x020000BC RID: 188
	[UnitCategory("Logic")]
	[UnitOrder(12)]
	public sealed class GreaterOrEqual : BinaryComparisonUnit
	{
		// Token: 0x17000222 RID: 546
		// (get) Token: 0x060005A7 RID: 1447 RVA: 0x0000BA9B File Offset: 0x00009C9B
		[PortLabel("A ≥ B")]
		public override ValueOutput comparison
		{
			get
			{
				return base.comparison;
			}
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x0000BAA3 File Offset: 0x00009CA3
		protected override bool NumericComparison(float a, float b)
		{
			return a > b || Mathf.Approximately(a, b);
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x0000BAB2 File Offset: 0x00009CB2
		protected override bool GenericComparison(object a, object b)
		{
			return OperatorUtility.GreaterThanOrEqual(a, b);
		}
	}
}
