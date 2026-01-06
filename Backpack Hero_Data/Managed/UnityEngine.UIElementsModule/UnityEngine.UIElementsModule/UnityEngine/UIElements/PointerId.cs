using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000213 RID: 531
	public static class PointerId
	{
		// Token: 0x04000705 RID: 1797
		public static readonly int maxPointers = 32;

		// Token: 0x04000706 RID: 1798
		public static readonly int invalidPointerId = -1;

		// Token: 0x04000707 RID: 1799
		public static readonly int mousePointerId = 0;

		// Token: 0x04000708 RID: 1800
		public static readonly int touchPointerIdBase = 1;

		// Token: 0x04000709 RID: 1801
		public static readonly int touchPointerCount = 20;

		// Token: 0x0400070A RID: 1802
		public static readonly int penPointerIdBase = PointerId.touchPointerIdBase + PointerId.touchPointerCount;

		// Token: 0x0400070B RID: 1803
		public static readonly int penPointerCount = 2;

		// Token: 0x0400070C RID: 1804
		internal static readonly int[] hoveringPointers = new int[] { PointerId.mousePointerId };
	}
}
