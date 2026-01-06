using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x020000C1 RID: 193
	[UnitCategory("Logic")]
	[UnitOrder(6)]
	public sealed class NotEqual : BinaryComparisonUnit
	{
		// Token: 0x060005C3 RID: 1475 RVA: 0x0000BC70 File Offset: 0x00009E70
		public NotEqual()
		{
			base.numeric = false;
		}

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x060005C4 RID: 1476 RVA: 0x0000BC7F File Offset: 0x00009E7F
		protected override string outputKey
		{
			get
			{
				return "notEqual";
			}
		}

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x060005C5 RID: 1477 RVA: 0x0000BC86 File Offset: 0x00009E86
		[DoNotSerialize]
		[PortLabel("A ≠ B")]
		[PortKey("notEqual")]
		public override ValueOutput comparison
		{
			get
			{
				return base.comparison;
			}
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x0000BC8E File Offset: 0x00009E8E
		protected override bool NumericComparison(float a, float b)
		{
			return !Mathf.Approximately(a, b);
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x0000BC9A File Offset: 0x00009E9A
		protected override bool GenericComparison(object a, object b)
		{
			return OperatorUtility.NotEqual(a, b);
		}
	}
}
