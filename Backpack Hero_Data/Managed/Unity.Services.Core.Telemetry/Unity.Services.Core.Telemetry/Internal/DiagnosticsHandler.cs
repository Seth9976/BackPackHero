using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using Unity.Services.Core.Configuration.Internal;
using Unity.Services.Core.Environments.Internal;
using Unity.Services.Core.Scheduler.Internal;
using UnityEngine;

namespace Unity.Services.Core.Telemetry.Internal
{
	// Token: 0x0200000A RID: 10
	internal class DiagnosticsHandler : TelemetryHandler<DiagnosticsPayload, Diagnostic>
	{
		// Token: 0x06000020 RID: 32 RVA: 0x00002471 File Offset: 0x00000671
		public DiagnosticsHandler(TelemetryConfig config, CachedPayload<DiagnosticsPayload> cache, IActionScheduler scheduler, ICachePersister<DiagnosticsPayload> cachePersister, TelemetrySender sender)
			: base(config, cache, scheduler, cachePersister, sender)
		{
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002480 File Offset: 0x00000680
		internal override void SendPersistedCache(CachedPayload<DiagnosticsPayload> persistedCache)
		{
			Task task = this.m_Sender.SendAsync<DiagnosticsPayload>(persistedCache.Payload);
			this.m_CachePersister.Delete();
			DiagnosticsHandler.SendState sendState = new DiagnosticsHandler.SendState
			{
				Self = this,
				Payload = new CachedPayload<DiagnosticsPayload>
				{
					TimeOfOccurenceTicks = persistedCache.TimeOfOccurenceTicks,
					Payload = persistedCache.Payload
				}
			};
			task.ContinueWith(new Action<Task, object>(DiagnosticsHandler.OnSendAsyncCompleted), sendState, TaskContinuationOptions.ExecuteSynchronously);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000024F4 File Offset: 0x000006F4
		private static void OnSendAsyncCompleted(Task sendOperation, object state)
		{
			try
			{
				DiagnosticsHandler.SendState sendState = state as DiagnosticsHandler.SendState;
				if (sendState == null)
				{
					throw new ArgumentException("The given state is invalid.");
				}
				TaskStatus status = sendOperation.Status;
				if (status != TaskStatus.RanToCompletion)
				{
					if (status - TaskStatus.Canceled > 1)
					{
						throw new ArgumentOutOfRangeException("Status", "Can't continue without the send operation being completed.");
					}
					sendState.Self.ThreadSafeCache(sendState.Payload);
				}
			}
			catch (Exception ex) when (TelemetryUtils.LogTelemetryException(ex, false))
			{
			}
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002578 File Offset: 0x00000778
		private void ThreadSafeCache(CachedPayload<DiagnosticsPayload> payload)
		{
			object @lock = base.Lock;
			lock (@lock)
			{
				base.Cache.AddRangeFrom(payload);
			}
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000025C0 File Offset: 0x000007C0
		internal override void FetchSpecificCommonTags(ICloudProjectId cloudProjectId, IEnvironments environments)
		{
			Dictionary<string, string> diagnosticsCommonTags = base.Cache.Payload.DiagnosticsCommonTags;
			diagnosticsCommonTags.Clear();
			diagnosticsCommonTags["application_version"] = Application.version;
			diagnosticsCommonTags["product_name"] = Application.productName;
			diagnosticsCommonTags["cloud_project_id"] = cloudProjectId.GetCloudProjectId();
			diagnosticsCommonTags["environment_name"] = environments.Current;
			diagnosticsCommonTags["application_genuine"] = (Application.genuineCheckAvailable ? Application.genuine.ToString(CultureInfo.InvariantCulture) : "unavailable");
			diagnosticsCommonTags["internet_reachability"] = Application.internetReachability.ToString();
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002670 File Offset: 0x00000870
		internal override void SendCachedPayload()
		{
			if (base.Cache.IsEmpty<DiagnosticsPayload>())
			{
				return;
			}
			Task task = this.m_Sender.SendAsync<DiagnosticsPayload>(base.Cache.Payload);
			DiagnosticsHandler.SendState sendState = new DiagnosticsHandler.SendState
			{
				Self = this,
				Payload = new CachedPayload<DiagnosticsPayload>
				{
					TimeOfOccurenceTicks = base.Cache.TimeOfOccurenceTicks,
					Payload = new DiagnosticsPayload
					{
						Diagnostics = new List<Diagnostic>(base.Cache.Payload.Diagnostics),
						CommonTags = new Dictionary<string, string>(base.Cache.Payload.CommonTags),
						DiagnosticsCommonTags = new Dictionary<string, string>(base.Cache.Payload.DiagnosticsCommonTags)
					}
				}
			};
			base.Cache.TimeOfOccurenceTicks = 0L;
			base.Cache.Payload.Diagnostics.Clear();
			if (this.m_CachePersister.CanPersist)
			{
				this.m_CachePersister.Delete();
			}
			task.ContinueWith(new Action<Task, object>(DiagnosticsHandler.OnSendAsyncCompleted), sendState, TaskContinuationOptions.ExecuteSynchronously);
		}

		// Token: 0x02000026 RID: 38
		private class SendState
		{
			// Token: 0x0400005D RID: 93
			public DiagnosticsHandler Self;

			// Token: 0x0400005E RID: 94
			public CachedPayload<DiagnosticsPayload> Payload;
		}
	}
}
