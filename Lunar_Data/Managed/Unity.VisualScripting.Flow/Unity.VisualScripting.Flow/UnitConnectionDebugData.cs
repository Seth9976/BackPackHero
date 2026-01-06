using System;

namespace Unity.VisualScripting
{
	// Token: 0x02000008 RID: 8
	public class UnitConnectionDebugData : IUnitConnectionDebugData, IGraphElementDebugData
	{
		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000037 RID: 55 RVA: 0x000025D5 File Offset: 0x000007D5
		// (set) Token: 0x06000038 RID: 56 RVA: 0x000025DD File Offset: 0x000007DD
		public int lastInvokeFrame { get; set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000039 RID: 57 RVA: 0x000025E6 File Offset: 0x000007E6
		// (set) Token: 0x0600003A RID: 58 RVA: 0x000025EE File Offset: 0x000007EE
		public float lastInvokeTime { get; set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600003B RID: 59 RVA: 0x000025F7 File Offset: 0x000007F7
		// (set) Token: 0x0600003C RID: 60 RVA: 0x000025FF File Offset: 0x000007FF
		public Exception runtimeException { get; set; }
	}
}
