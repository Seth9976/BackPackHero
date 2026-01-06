using System;

namespace Unity.VisualScripting
{
	// Token: 0x020000BB RID: 187
	[UnitCategory("Logic")]
	[UnitOrder(11)]
	public sealed class Greater : BinaryComparisonUnit
	{
		// Token: 0x17000221 RID: 545
		// (get) Token: 0x060005A3 RID: 1443 RVA: 0x0000BA7C File Offset: 0x00009C7C
		[PortLabel("A > B")]
		public override ValueOutput comparison
		{
			get
			{
				return base.comparison;
			}
		}

		// Token: 0x060005A4 RID: 1444 RVA: 0x0000BA84 File Offset: 0x00009C84
		protected override bool NumericComparison(float a, float b)
		{
			return a > b;
		}

		// Token: 0x060005A5 RID: 1445 RVA: 0x0000BA8A File Offset: 0x00009C8A
		protected override bool GenericComparison(object a, object b)
		{
			return OperatorUtility.GreaterThan(a, b);
		}
	}
}
