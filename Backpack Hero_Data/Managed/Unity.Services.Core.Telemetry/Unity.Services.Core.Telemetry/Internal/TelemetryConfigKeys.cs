using System;

namespace Unity.Services.Core.Telemetry.Internal
{
	// Token: 0x02000024 RID: 36
	internal static class TelemetryConfigKeys
	{
		// Token: 0x04000055 RID: 85
		private const string k_BaseKey = "com.unity.services.core.telemetry-";

		// Token: 0x04000056 RID: 86
		public const string TargetUrl = "com.unity.services.core.telemetry-target-url";

		// Token: 0x04000057 RID: 87
		public const string ServicePath = "com.unity.services.core.telemetry-service-path";

		// Token: 0x04000058 RID: 88
		public const string PayloadExpirationSeconds = "com.unity.services.core.telemetry-payload-expiration-seconds";

		// Token: 0x04000059 RID: 89
		public const string PayloadSendingMaxIntervalSeconds = "com.unity.services.core.telemetry-payload-sending-max-interval-seconds";

		// Token: 0x0400005A RID: 90
		public const string SafetyPersistenceIntervalSeconds = "com.unity.services.core.telemetry-safety-persistence-interval-seconds";

		// Token: 0x0400005B RID: 91
		public const string MaxMetricCountPerPayload = "com.unity.services.core.telemetry-max-metric-count-per-payload";
	}
}
