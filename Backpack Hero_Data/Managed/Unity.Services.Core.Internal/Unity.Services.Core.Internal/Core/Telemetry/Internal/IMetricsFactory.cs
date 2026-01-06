using System;
using System.Collections.Generic;
using Unity.Services.Core.Internal;

namespace Unity.Services.Core.Telemetry.Internal
{
	// Token: 0x02000013 RID: 19
	public interface IMetricsFactory : IServiceComponent
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000028 RID: 40
		IReadOnlyDictionary<string, string> CommonTags { get; }

		// Token: 0x06000029 RID: 41
		IMetrics Create(string packageName);
	}
}
