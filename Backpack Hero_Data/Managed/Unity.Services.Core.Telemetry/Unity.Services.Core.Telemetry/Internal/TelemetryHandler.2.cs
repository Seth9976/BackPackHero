using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Services.Core.Configuration.Internal;
using Unity.Services.Core.Environments.Internal;
using Unity.Services.Core.Scheduler.Internal;
using UnityEngine;

namespace Unity.Services.Core.Telemetry.Internal
{
	// Token: 0x0200000D RID: 13
	internal abstract class TelemetryHandler<TPayload, TEvent> : TelemetryHandler where TPayload : ITelemetryPayload where TEvent : ITelemetryEvent
	{
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600002D RID: 45 RVA: 0x000028B3 File Offset: 0x00000AB3
		public TelemetryConfig Config { get; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600002E RID: 46 RVA: 0x000028BB File Offset: 0x00000ABB
		public CachedPayload<TPayload> Cache { get; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600002F RID: 47 RVA: 0x000028C3 File Offset: 0x00000AC3
		protected object Lock { get; } = new object();

		// Token: 0x06000030 RID: 48 RVA: 0x000028CB File Offset: 0x00000ACB
		protected TelemetryHandler(TelemetryConfig config, CachedPayload<TPayload> cache, IActionScheduler scheduler, ICachePersister<TPayload> cachePersister, TelemetrySender sender)
		{
			this.Config = config;
			this.Cache = cache;
			this.m_Scheduler = scheduler;
			this.m_CachePersister = cachePersister;
			this.m_Sender = sender;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002903 File Offset: 0x00000B03
		public void Initialize(ICloudProjectId cloudProjectId, IEnvironments environments)
		{
			this.HandlePersistedCache();
			this.FetchAllCommonTags(cloudProjectId, environments);
			this.ScheduleSendingLoop();
			if (this.m_CachePersister.CanPersist)
			{
				this.SchedulePersistenceLoop();
			}
		}

		// Token: 0x06000032 RID: 50 RVA: 0x0000292C File Offset: 0x00000B2C
		internal void HandlePersistedCache()
		{
			object @lock = this.Lock;
			lock (@lock)
			{
				CachedPayload<TPayload> cachedPayload;
				if (this.m_CachePersister.CanPersist && this.m_CachePersister.TryFetch(out cachedPayload))
				{
					if (cachedPayload.IsEmpty<TPayload>())
					{
						this.m_CachePersister.Delete();
					}
					else
					{
						this.SendPersistedCache(cachedPayload);
					}
				}
			}
		}

		// Token: 0x06000033 RID: 51
		internal abstract void SendPersistedCache(CachedPayload<TPayload> persistedCache);

		// Token: 0x06000034 RID: 52 RVA: 0x000029A0 File Offset: 0x00000BA0
		private void FetchAllCommonTags(ICloudProjectId cloudProjectId, IEnvironments environments)
		{
			this.FetchTelemetryCommonTags();
			this.FetchSpecificCommonTags(cloudProjectId, environments);
		}

		// Token: 0x06000035 RID: 53
		internal abstract void FetchSpecificCommonTags(ICloudProjectId cloudProjectId, IEnvironments environments);

		// Token: 0x06000036 RID: 54 RVA: 0x000029B0 File Offset: 0x00000BB0
		internal void FetchTelemetryCommonTags()
		{
			Dictionary<string, string> commonTags = this.Cache.Payload.CommonTags;
			commonTags.Clear();
			commonTags["application_install_mode"] = Application.installMode.ToString();
			commonTags["operating_system"] = TelemetryHandler.FormatOperatingSystemInfo(SystemInfo.operatingSystem);
			commonTags["platform"] = Application.platform.ToString();
			commonTags["engine"] = "Unity";
			commonTags["unity_version"] = Application.unityVersion;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002A4C File Offset: 0x00000C4C
		internal void ScheduleSendingLoop()
		{
			try
			{
				this.SendingLoopScheduleId = this.m_Scheduler.ScheduleAction(new Action(this.<ScheduleSendingLoop>g__SendingLoop|21_0), this.Config.PayloadSendingMaxIntervalSeconds);
			}
			catch (Exception ex) when (TelemetryUtils.LogTelemetryException(ex, false))
			{
			}
		}

		// Token: 0x06000038 RID: 56
		internal abstract void SendCachedPayload();

		// Token: 0x06000039 RID: 57 RVA: 0x00002AB0 File Offset: 0x00000CB0
		internal void SchedulePersistenceLoop()
		{
			try
			{
				this.PersistenceLoopScheduleId = this.m_Scheduler.ScheduleAction(new Action(this.<SchedulePersistenceLoop>g__PersistenceLoop|23_0), this.Config.SafetyPersistenceIntervalSeconds);
			}
			catch (Exception ex) when (TelemetryUtils.LogTelemetryException(ex, false))
			{
			}
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002B14 File Offset: 0x00000D14
		internal void PersistCache()
		{
			object @lock = this.Lock;
			lock (@lock)
			{
				if (this.m_CachePersister.CanPersist && this.Cache.TimeOfOccurenceTicks > 0L && this.Cache.Payload.Count > 0)
				{
					this.m_CachePersister.Persist(this.Cache);
				}
			}
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002B98 File Offset: 0x00000D98
		public void Register(TEvent telemetryEvent)
		{
			try
			{
				object @lock = this.Lock;
				lock (@lock)
				{
					this.Cache.Add(telemetryEvent);
					if (!this.<Register>g__IsCacheFull|25_0())
					{
						return;
					}
					this.SendCachedPayload();
				}
				this.m_Scheduler.CancelAction(this.SendingLoopScheduleId);
				this.ScheduleSendingLoop();
			}
			catch (Exception ex) when (TelemetryUtils.LogTelemetryException(ex, false))
			{
			}
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002C34 File Offset: 0x00000E34
		[CompilerGenerated]
		private void <ScheduleSendingLoop>g__SendingLoop|21_0()
		{
			this.ScheduleSendingLoop();
			object @lock = this.Lock;
			lock (@lock)
			{
				try
				{
					this.SendCachedPayload();
				}
				catch (Exception ex) when (TelemetryUtils.LogTelemetryException(ex, false))
				{
				}
			}
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002CA0 File Offset: 0x00000EA0
		[CompilerGenerated]
		private void <SchedulePersistenceLoop>g__PersistenceLoop|23_0()
		{
			this.SchedulePersistenceLoop();
			try
			{
				this.PersistCache();
			}
			catch (Exception ex) when (TelemetryUtils.LogTelemetryException(ex, false))
			{
			}
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002CE8 File Offset: 0x00000EE8
		[CompilerGenerated]
		private bool <Register>g__IsCacheFull|25_0()
		{
			return this.Cache.Payload.Count >= this.Config.MaxMetricCountPerPayload;
		}

		// Token: 0x04000010 RID: 16
		private readonly IActionScheduler m_Scheduler;

		// Token: 0x04000011 RID: 17
		protected readonly ICachePersister<TPayload> m_CachePersister;

		// Token: 0x04000012 RID: 18
		protected readonly TelemetrySender m_Sender;

		// Token: 0x04000013 RID: 19
		internal long SendingLoopScheduleId;

		// Token: 0x04000014 RID: 20
		internal long PersistenceLoopScheduleId;
	}
}
