using System;
using System.Collections.Generic;

namespace Unity.Services.Core.Telemetry.Internal
{
	// Token: 0x02000012 RID: 18
	public interface IMetrics
	{
		// Token: 0x06000025 RID: 37
		void SendGaugeMetric(string name, double value = 0.0, IDictionary<string, string> tags = null);

		// Token: 0x06000026 RID: 38
		void SendHistogramMetric(string name, double time, IDictionary<string, string> tags = null);

		// Token: 0x06000027 RID: 39
		void SendSumMetric(string name, double value = 1.0, IDictionary<string, string> tags = null);
	}
}
