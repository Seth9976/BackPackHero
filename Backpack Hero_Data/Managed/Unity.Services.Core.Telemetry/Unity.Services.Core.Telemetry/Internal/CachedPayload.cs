using System;

namespace Unity.Services.Core.Telemetry.Internal
{
	// Token: 0x02000012 RID: 18
	[Serializable]
	internal class CachedPayload<TPayload> where TPayload : ITelemetryPayload
	{
		// Token: 0x0400001E RID: 30
		public long TimeOfOccurenceTicks;

		// Token: 0x0400001F RID: 31
		public TPayload Payload;
	}
}
