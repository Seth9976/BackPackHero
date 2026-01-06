using System;
using System.Collections.Generic;
using Unity.Services.Core.Internal;

namespace Unity.Services.Core.Telemetry.Internal
{
	// Token: 0x0200000F RID: 15
	internal class DisabledMetricsFactory : IMetricsFactory, IServiceComponent
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000043 RID: 67 RVA: 0x00002D1E File Offset: 0x00000F1E
		IReadOnlyDictionary<string, string> IMetricsFactory.CommonTags { get; } = new Dictionary<string, string>();

		// Token: 0x06000044 RID: 68 RVA: 0x00002D26 File Offset: 0x00000F26
		IMetrics IMetricsFactory.Create(string packageName)
		{
			return new DisabledMetrics();
		}
	}
}
