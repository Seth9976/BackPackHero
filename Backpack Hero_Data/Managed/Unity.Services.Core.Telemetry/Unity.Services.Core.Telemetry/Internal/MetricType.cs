using System;
using System.Runtime.Serialization;

namespace Unity.Services.Core.Telemetry.Internal
{
	// Token: 0x02000019 RID: 25
	internal enum MetricType
	{
		// Token: 0x0400002C RID: 44
		[EnumMember(Value = "GAUGE")]
		Gauge,
		// Token: 0x0400002D RID: 45
		[EnumMember(Value = "SUM")]
		Sum,
		// Token: 0x0400002E RID: 46
		[EnumMember(Value = "HISTOGRAM")]
		Histogram
	}
}
