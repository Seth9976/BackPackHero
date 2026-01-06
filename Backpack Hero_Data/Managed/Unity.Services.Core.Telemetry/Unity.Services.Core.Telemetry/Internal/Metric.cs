using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Unity.Services.Core.Telemetry.Internal
{
	// Token: 0x02000017 RID: 23
	[Serializable]
	internal struct Metric : ITelemetryEvent
	{
		// Token: 0x04000024 RID: 36
		[JsonProperty("name")]
		public string Name;

		// Token: 0x04000025 RID: 37
		[JsonProperty("value")]
		public double Value;

		// Token: 0x04000026 RID: 38
		[JsonProperty("type")]
		[JsonConverter(typeof(StringEnumConverter))]
		public MetricType Type;

		// Token: 0x04000027 RID: 39
		[JsonProperty("tags")]
		public IDictionary<string, string> Tags;
	}
}
