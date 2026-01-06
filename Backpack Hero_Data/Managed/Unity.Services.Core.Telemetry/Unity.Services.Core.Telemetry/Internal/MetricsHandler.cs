using System;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Utilities;
using Unity.Services.Core.Configuration.Internal;
using Unity.Services.Core.Environments.Internal;
using Unity.Services.Core.Scheduler.Internal;

namespace Unity.Services.Core.Telemetry.Internal
{
	// Token: 0x0200000B RID: 11
	internal class MetricsHandler : TelemetryHandler<MetricsPayload, Metric>
	{
		// Token: 0x06000026 RID: 38 RVA: 0x0000277F File Offset: 0x0000097F
		public MetricsHandler(TelemetryConfig config, CachedPayload<MetricsPayload> cache, IActionScheduler scheduler, ICachePersister<MetricsPayload> cachePersister, TelemetrySender sender)
			: base(config, cache, scheduler, cachePersister, sender)
		{
			AotHelper.EnsureType<StringEnumConverter>();
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002794 File Offset: 0x00000994
		internal override void SendPersistedCache(CachedPayload<MetricsPayload> persistedCache)
		{
			MetricsHandler.<>c__DisplayClass1_0 CS$<>8__locals1;
			CS$<>8__locals1.persistedCache = persistedCache;
			CS$<>8__locals1.<>4__this = this;
			if (!this.<SendPersistedCache>g__AreMetricsOutdated|1_0(ref CS$<>8__locals1))
			{
				this.m_Sender.SendAsync<MetricsPayload>(CS$<>8__locals1.persistedCache.Payload);
			}
			this.m_CachePersister.Delete();
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000027DD File Offset: 0x000009DD
		internal override void FetchSpecificCommonTags(ICloudProjectId cloudProjectId, IEnvironments environments)
		{
			base.Cache.Payload.MetricsCommonTags.Clear();
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000027F4 File Offset: 0x000009F4
		internal override void SendCachedPayload()
		{
			if (base.Cache.Payload.Metrics.Count <= 0)
			{
				return;
			}
			this.m_Sender.SendAsync<MetricsPayload>(base.Cache.Payload);
			base.Cache.Payload.Metrics.Clear();
			base.Cache.TimeOfOccurenceTicks = 0L;
			if (this.m_CachePersister.CanPersist)
			{
				this.m_CachePersister.Delete();
			}
		}

		// Token: 0x0600002A RID: 42 RVA: 0x0000286C File Offset: 0x00000A6C
		[CompilerGenerated]
		private bool <SendPersistedCache>g__AreMetricsOutdated|1_0(ref MetricsHandler.<>c__DisplayClass1_0 A_1)
		{
			return (DateTime.UtcNow - new DateTime(A_1.persistedCache.TimeOfOccurenceTicks)).TotalSeconds > base.Config.PayloadExpirationSeconds;
		}
	}
}
