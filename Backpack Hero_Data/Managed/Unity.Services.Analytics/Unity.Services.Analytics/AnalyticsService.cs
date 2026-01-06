using System;
using Unity.Services.Core;

namespace Unity.Services.Analytics
{
	// Token: 0x02000003 RID: 3
	public static class AnalyticsService
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000004 RID: 4 RVA: 0x000020F3 File Offset: 0x000002F3
		public static IAnalyticsService Instance
		{
			get
			{
				if (AnalyticsService.internalInstance == null)
				{
					throw new ServicesInitializationException("The Analytics service has not been initialized. Please initialize Unity Services.");
				}
				return AnalyticsService.internalInstance;
			}
		}

		// Token: 0x04000001 RID: 1
		internal static AnalyticsServiceInstance internalInstance;
	}
}
