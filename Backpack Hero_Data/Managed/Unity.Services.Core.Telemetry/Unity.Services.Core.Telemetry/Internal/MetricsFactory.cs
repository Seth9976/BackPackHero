using System;
using System.Collections.Generic;
using Unity.Services.Core.Configuration.Internal;
using Unity.Services.Core.Internal;

namespace Unity.Services.Core.Telemetry.Internal
{
	// Token: 0x02000011 RID: 17
	internal class MetricsFactory : IMetricsFactory, IServiceComponent
	{
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600004D RID: 77 RVA: 0x00002E24 File Offset: 0x00001024
		public IReadOnlyDictionary<string, string> CommonTags { get; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600004E RID: 78 RVA: 0x00002E2C File Offset: 0x0000102C
		internal MetricsHandler Handler { get; }

		// Token: 0x0600004F RID: 79 RVA: 0x00002E34 File Offset: 0x00001034
		public MetricsFactory(MetricsHandler handler, IProjectConfiguration projectConfig)
		{
			this.Handler = handler;
			this.m_ProjectConfig = projectConfig;
			this.CommonTags = new Dictionary<string, string>(handler.Cache.Payload.CommonTags).MergeAllowOverride(handler.Cache.Payload.MetricsCommonTags);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002E88 File Offset: 0x00001088
		public IMetrics Create(string packageName)
		{
			if (string.IsNullOrEmpty(packageName))
			{
				throw new ArgumentNullException("packageName");
			}
			IDictionary<string, string> dictionary = FactoryUtils.CreatePackageTags(this.m_ProjectConfig, packageName);
			return new Metrics(this.Handler, dictionary);
		}

		// Token: 0x0400001B RID: 27
		private readonly IProjectConfiguration m_ProjectConfig;
	}
}
