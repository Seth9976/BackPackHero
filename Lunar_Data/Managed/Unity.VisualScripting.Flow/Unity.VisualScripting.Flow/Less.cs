using System;

namespace Unity.VisualScripting
{
	// Token: 0x020000BD RID: 189
	[UnitCategory("Logic")]
	[UnitOrder(9)]
	public sealed class Less : BinaryComparisonUnit
	{
		// Token: 0x17000223 RID: 547
		// (get) Token: 0x060005AB RID: 1451 RVA: 0x0000BAC3 File Offset: 0x00009CC3
		[PortLabel("A < B")]
		public override ValueOutput comparison
		{
			get
			{
				return base.comparison;
			}
		}

		// Token: 0x060005AC RID: 1452 RVA: 0x0000BACB File Offset: 0x00009CCB
		protected override bool NumericComparison(float a, float b)
		{
			return a < b;
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x0000BAD1 File Offset: 0x00009CD1
		protected override bool GenericComparison(object a, object b)
		{
			return OperatorUtility.LessThan(a, b);
		}
	}
}
