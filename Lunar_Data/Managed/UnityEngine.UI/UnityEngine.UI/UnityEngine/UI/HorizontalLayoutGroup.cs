using System;

namespace UnityEngine.UI
{
	// Token: 0x0200001D RID: 29
	[AddComponentMenu("Layout/Horizontal Layout Group", 150)]
	public class HorizontalLayoutGroup : HorizontalOrVerticalLayoutGroup
	{
		// Token: 0x06000250 RID: 592 RVA: 0x0000DD3B File Offset: 0x0000BF3B
		protected HorizontalLayoutGroup()
		{
		}

		// Token: 0x06000251 RID: 593 RVA: 0x0000DD43 File Offset: 0x0000BF43
		public override void CalculateLayoutInputHorizontal()
		{
			base.CalculateLayoutInputHorizontal();
			base.CalcAlongAxis(0, false);
		}

		// Token: 0x06000252 RID: 594 RVA: 0x0000DD53 File Offset: 0x0000BF53
		public override void CalculateLayoutInputVertical()
		{
			base.CalcAlongAxis(1, false);
		}

		// Token: 0x06000253 RID: 595 RVA: 0x0000DD5D File Offset: 0x0000BF5D
		public override void SetLayoutHorizontal()
		{
			base.SetChildrenAlongAxis(0, false);
		}

		// Token: 0x06000254 RID: 596 RVA: 0x0000DD67 File Offset: 0x0000BF67
		public override void SetLayoutVertical()
		{
			base.SetChildrenAlongAxis(1, false);
		}
	}
}
