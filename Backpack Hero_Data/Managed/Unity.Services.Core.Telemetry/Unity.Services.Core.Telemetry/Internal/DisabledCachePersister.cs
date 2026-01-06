using System;

namespace Unity.Services.Core.Telemetry.Internal
{
	// Token: 0x02000002 RID: 2
	internal class DisabledCachePersister<TPayload> : ICachePersister<TPayload> where TPayload : ITelemetryPayload
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public bool CanPersist
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002053 File Offset: 0x00000253
		public void Persist(CachedPayload<TPayload> cache)
		{
			throw new NotSupportedException("Cache persistence isn't supported on the current platform.");
		}

		// Token: 0x06000003 RID: 3 RVA: 0x0000205F File Offset: 0x0000025F
		public bool TryFetch(out CachedPayload<TPayload> persistedCache)
		{
			throw new NotSupportedException("Cache persistence isn't supported on the current platform.");
		}

		// Token: 0x06000004 RID: 4 RVA: 0x0000206B File Offset: 0x0000026B
		public void Delete()
		{
			throw new NotSupportedException("Cache persistence isn't supported on the current platform.");
		}

		// Token: 0x04000001 RID: 1
		private const string k_ErrorMessage = "Cache persistence isn't supported on the current platform.";
	}
}
