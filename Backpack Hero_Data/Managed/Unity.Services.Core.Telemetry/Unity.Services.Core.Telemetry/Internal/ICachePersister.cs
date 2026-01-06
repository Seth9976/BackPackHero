using System;

namespace Unity.Services.Core.Telemetry.Internal
{
	// Token: 0x02000005 RID: 5
	internal interface ICachePersister<TPayload> where TPayload : ITelemetryPayload
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000F RID: 15
		bool CanPersist { get; }

		// Token: 0x06000010 RID: 16
		void Persist(CachedPayload<TPayload> cache);

		// Token: 0x06000011 RID: 17
		bool TryFetch(out CachedPayload<TPayload> persistedCache);

		// Token: 0x06000012 RID: 18
		void Delete();
	}
}
