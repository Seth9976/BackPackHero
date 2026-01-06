using System;

namespace TMPro
{
	// Token: 0x0200006F RID: 111
	public struct CaretInfo
	{
		// Token: 0x06000599 RID: 1433 RVA: 0x000363FC File Offset: 0x000345FC
		public CaretInfo(int index, CaretPosition position)
		{
			this.index = index;
			this.position = position;
		}

		// Token: 0x04000555 RID: 1365
		public int index;

		// Token: 0x04000556 RID: 1366
		public CaretPosition position;
	}
}
