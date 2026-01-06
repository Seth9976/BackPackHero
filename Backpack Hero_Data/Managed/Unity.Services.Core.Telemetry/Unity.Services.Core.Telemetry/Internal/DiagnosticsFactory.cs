using System;
using System.Collections.Generic;
using Unity.Services.Core.Configuration.Internal;
using Unity.Services.Core.Internal;

namespace Unity.Services.Core.Telemetry.Internal
{
	// Token: 0x02000007 RID: 7
	internal class DiagnosticsFactory : IDiagnosticsFactory, IServiceComponent
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000017 RID: 23 RVA: 0x000023A6 File Offset: 0x000005A6
		public IReadOnlyDictionary<string, string> CommonTags { get; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000018 RID: 24 RVA: 0x000023AE File Offset: 0x000005AE
		internal DiagnosticsHandler Handler { get; }

		// Token: 0x06000019 RID: 25 RVA: 0x000023B8 File Offset: 0x000005B8
		public DiagnosticsFactory(DiagnosticsHandler handler, IProjectConfiguration projectConfig)
		{
			this.Handler = handler;
			this.m_ProjectConfig = projectConfig;
			this.CommonTags = new Dictionary<string, string>(handler.Cache.Payload.CommonTags).MergeAllowOverride(handler.Cache.Payload.DiagnosticsCommonTags);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x0000240C File Offset: 0x0000060C
		public IDiagnostics Create(string packageName)
		{
			if (string.IsNullOrEmpty(packageName))
			{
				throw new ArgumentNullException("packageName");
			}
			IDictionary<string, string> dictionary = FactoryUtils.CreatePackageTags(this.m_ProjectConfig, packageName);
			return new Diagnostics(this.Handler, dictionary);
		}

		// Token: 0x0400000C RID: 12
		private readonly IProjectConfiguration m_ProjectConfig;
	}
}
