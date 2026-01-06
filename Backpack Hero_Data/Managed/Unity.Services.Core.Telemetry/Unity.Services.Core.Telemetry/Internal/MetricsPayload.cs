using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Unity.Services.Core.Telemetry.Internal
{
	// Token: 0x02000018 RID: 24
	[Serializable]
	internal struct MetricsPayload : ITelemetryPayload
	{
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000058 RID: 88 RVA: 0x00002F2D File Offset: 0x0000112D
		Dictionary<string, string> ITelemetryPayload.CommonTags
		{
			get
			{
				return this.CommonTags;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000059 RID: 89 RVA: 0x00002F35 File Offset: 0x00001135
		int ITelemetryPayload.Count
		{
			get
			{
				List<Metric> metrics = this.Metrics;
				if (metrics == null)
				{
					return 0;
				}
				return metrics.Count;
			}
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002F48 File Offset: 0x00001148
		void ITelemetryPayload.Add(ITelemetryEvent telemetryEvent)
		{
			if (telemetryEvent is Metric)
			{
				Metric metric = (Metric)telemetryEvent;
				if (this.Metrics == null)
				{
					this.Metrics = new List<Metric>(1);
				}
				this.Metrics.Add(metric);
				return;
			}
			throw new ArgumentException("This payload accepts only Metric events.");
		}

		// Token: 0x04000028 RID: 40
		[JsonProperty("metrics")]
		public List<Metric> Metrics;

		// Token: 0x04000029 RID: 41
		[JsonProperty("commonTags")]
		public Dictionary<string, string> CommonTags;

		// Token: 0x0400002A RID: 42
		[JsonProperty("metricsCommonTags")]
		public Dictionary<string, string> MetricsCommonTags;
	}
}
