using System;

namespace Unity.Services.Core.Telemetry.Internal
{
	// Token: 0x02000020 RID: 32
	[Serializable]
	internal class TelemetryConfig
	{
		// Token: 0x0400003E RID: 62
		public const int MaxMetricCountPerPayloadLimit = 295;

		// Token: 0x0400003F RID: 63
		public string TargetUrl;

		// Token: 0x04000040 RID: 64
		public string ServicePath;

		// Token: 0x04000041 RID: 65
		public double PayloadExpirationSeconds;

		// Token: 0x04000042 RID: 66
		public double PayloadSendingMaxIntervalSeconds;

		// Token: 0x04000043 RID: 67
		public double SafetyPersistenceIntervalSeconds;

		// Token: 0x04000044 RID: 68
		public int MaxMetricCountPerPayload;
	}
}
