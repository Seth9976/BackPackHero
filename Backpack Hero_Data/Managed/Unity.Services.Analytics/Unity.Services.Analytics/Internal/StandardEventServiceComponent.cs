using System;
using System.Collections.Generic;
using Unity.Services.Core.Analytics.Internal;
using Unity.Services.Core.Configuration.Internal;
using Unity.Services.Core.Internal;

namespace Unity.Services.Analytics.Internal
{
	// Token: 0x0200003D RID: 61
	internal class StandardEventServiceComponent : IAnalyticsStandardEventComponent, IServiceComponent
	{
		// Token: 0x0600014D RID: 333 RVA: 0x00004FD1 File Offset: 0x000031D1
		public StandardEventServiceComponent(IProjectConfiguration configuration, IUnstructuredEventRecorder analyticsService)
		{
			this.m_Configuration = configuration;
			this.m_AnalyticsService = analyticsService;
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00004FE8 File Offset: 0x000031E8
		public void Record(string eventName, IDictionary<string, object> eventParameters, int eventVersion, string packageName)
		{
			string @string = this.m_Configuration.GetString(packageName + ".version", null);
			string text = packageName + "@" + @string;
			this.m_AnalyticsService.CustomData(eventName, eventParameters, new int?(eventVersion), true, true, text);
		}

		// Token: 0x040000DB RID: 219
		private readonly IProjectConfiguration m_Configuration;

		// Token: 0x040000DC RID: 220
		private readonly IUnstructuredEventRecorder m_AnalyticsService;
	}
}
