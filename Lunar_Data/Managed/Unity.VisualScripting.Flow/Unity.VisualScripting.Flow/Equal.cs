using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x020000B8 RID: 184
	[UnitCategory("Logic")]
	[UnitOrder(5)]
	public sealed class Equal : BinaryComparisonUnit
	{
		// Token: 0x06000589 RID: 1417 RVA: 0x0000B82E File Offset: 0x00009A2E
		public Equal()
		{
			base.numeric = false;
		}

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x0600058A RID: 1418 RVA: 0x0000B83D File Offset: 0x00009A3D
		protected override string outputKey
		{
			get
			{
				return "equal";
			}
		}

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x0600058B RID: 1419 RVA: 0x0000B844 File Offset: 0x00009A44
		[DoNotSerialize]
		[PortLabel("A = B")]
		[PortKey("equal")]
		public override ValueOutput comparison
		{
			get
			{
				return base.comparison;
			}
		}

		// Token: 0x0600058C RID: 1420 RVA: 0x0000B84C File Offset: 0x00009A4C
		protected override bool NumericComparison(float a, float b)
		{
			return Mathf.Approximately(a, b);
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x0000B855 File Offset: 0x00009A55
		protected override bool GenericComparison(object a, object b)
		{
			return OperatorUtility.Equal(a, b);
		}
	}
}
