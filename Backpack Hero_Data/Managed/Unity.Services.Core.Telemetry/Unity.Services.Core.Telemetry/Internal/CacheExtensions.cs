using System;
using System.Collections.Generic;

namespace Unity.Services.Core.Telemetry.Internal
{
	// Token: 0x02000021 RID: 33
	internal static class CacheExtensions
	{
		// Token: 0x0600006F RID: 111 RVA: 0x00003234 File Offset: 0x00001434
		public static bool IsEmpty<TPayload>(this CachedPayload<TPayload> self) where TPayload : ITelemetryPayload
		{
			int? num;
			if (self == null)
			{
				num = null;
			}
			else
			{
				ref TPayload ptr = ref self.Payload;
				TPayload tpayload = default(TPayload);
				if (tpayload == null)
				{
					tpayload = self.Payload;
					ptr = ref tpayload;
					if (tpayload == null)
					{
						num = null;
						goto IL_0050;
					}
				}
				num = new int?(ptr.Count);
			}
			IL_0050:
			int? num2 = num;
			return num2.GetValueOrDefault() <= 0;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x000032A0 File Offset: 0x000014A0
		public static void AddRangeFrom(this CachedPayload<DiagnosticsPayload> self, CachedPayload<DiagnosticsPayload> payload)
		{
			int? num;
			if (payload == null)
			{
				num = null;
			}
			else
			{
				List<Diagnostic> diagnostics = payload.Payload.Diagnostics;
				num = ((diagnostics != null) ? new int?(diagnostics.Count) : null);
			}
			int? num2 = num;
			if (num2.GetValueOrDefault() <= 0)
			{
				return;
			}
			self.Payload.Diagnostics.AddRange(payload.Payload.Diagnostics);
			if (self.TimeOfOccurenceTicks <= 0L)
			{
				self.TimeOfOccurenceTicks = payload.TimeOfOccurenceTicks;
			}
		}

		// Token: 0x06000071 RID: 113 RVA: 0x0000331C File Offset: 0x0000151C
		public static void Add<TPayload>(this CachedPayload<TPayload> self, ITelemetryEvent telemetryEvent) where TPayload : ITelemetryPayload
		{
			if (self.TimeOfOccurenceTicks == 0L)
			{
				self.TimeOfOccurenceTicks = DateTime.UtcNow.Ticks;
			}
			self.Payload.Add(telemetryEvent);
		}
	}
}
