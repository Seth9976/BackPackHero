using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020000E7 RID: 231
	public class VisualElementFocusChangeDirection : FocusChangeDirection
	{
		// Token: 0x1700017E RID: 382
		// (get) Token: 0x06000743 RID: 1859 RVA: 0x0001A5A3 File Offset: 0x000187A3
		public static FocusChangeDirection left
		{
			get
			{
				return VisualElementFocusChangeDirection.s_Left;
			}
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x06000744 RID: 1860 RVA: 0x0001A5AA File Offset: 0x000187AA
		public static FocusChangeDirection right
		{
			get
			{
				return VisualElementFocusChangeDirection.s_Right;
			}
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x06000745 RID: 1861 RVA: 0x0001A5B4 File Offset: 0x000187B4
		protected new static VisualElementFocusChangeDirection lastValue
		{
			get
			{
				return VisualElementFocusChangeDirection.s_Right;
			}
		}

		// Token: 0x06000746 RID: 1862 RVA: 0x0001A5CB File Offset: 0x000187CB
		protected VisualElementFocusChangeDirection(int value)
			: base(value)
		{
		}

		// Token: 0x040002F1 RID: 753
		private static readonly VisualElementFocusChangeDirection s_Left = new VisualElementFocusChangeDirection(FocusChangeDirection.lastValue + 1);

		// Token: 0x040002F2 RID: 754
		private static readonly VisualElementFocusChangeDirection s_Right = new VisualElementFocusChangeDirection(FocusChangeDirection.lastValue + 2);
	}
}
