using System;

namespace Unity.Services.Analytics
{
	// Token: 0x02000006 RID: 6
	internal class AnalyticsServiceSystemCalls : IAnalyticsServiceSystemCalls
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600002C RID: 44 RVA: 0x00002EED File Offset: 0x000010ED
		public DateTime UtcNow
		{
			get
			{
				return DateTime.UtcNow;
			}
		}
	}
}
