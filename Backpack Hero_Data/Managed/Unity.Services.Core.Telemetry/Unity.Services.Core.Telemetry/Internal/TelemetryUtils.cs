using System;
using System.Collections.Generic;
using Unity.Services.Core.Configuration.Internal;
using Unity.Services.Core.Environments.Internal;
using Unity.Services.Core.Internal;
using Unity.Services.Core.Scheduler.Internal;
using UnityEngine;

namespace Unity.Services.Core.Telemetry.Internal
{
	// Token: 0x02000025 RID: 37
	internal static class TelemetryUtils
	{
		// Token: 0x06000073 RID: 115 RVA: 0x000033A0 File Offset: 0x000015A0
		public static IMetricsFactory CreateMetricsFactory(IActionScheduler scheduler, IProjectConfiguration projectConfiguration, ICloudProjectId cloudProjectId, IEnvironments environments)
		{
			if (TelemetryUtils.IsTelemetryDisabled(projectConfiguration))
			{
				return new DisabledMetricsFactory();
			}
			TelemetryConfig telemetryConfig = TelemetryUtils.CreateTelemetryConfig(projectConfiguration);
			CachedPayload<MetricsPayload> cachedPayload = new CachedPayload<MetricsPayload>
			{
				Payload = new MetricsPayload
				{
					Metrics = new List<Metric>(),
					CommonTags = new Dictionary<string, string>(),
					MetricsCommonTags = new Dictionary<string, string>()
				}
			};
			ICachePersister<MetricsPayload> cachePersister = TelemetryUtils.CreateCachePersister<MetricsPayload>("UnityServicesCachedMetrics", Application.platform);
			ExponentialBackOffRetryPolicy exponentialBackOffRetryPolicy = new ExponentialBackOffRetryPolicy();
			UnityWebRequestSender unityWebRequestSender = new UnityWebRequestSender();
			TelemetrySender telemetrySender = new TelemetrySender(telemetryConfig.TargetUrl, telemetryConfig.ServicePath, scheduler, exponentialBackOffRetryPolicy, unityWebRequestSender);
			MetricsHandler metricsHandler = new MetricsHandler(telemetryConfig, cachedPayload, scheduler, cachePersister, telemetrySender);
			metricsHandler.Initialize(cloudProjectId, environments);
			return new MetricsFactory(metricsHandler, projectConfiguration);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x0000344C File Offset: 0x0000164C
		public static IDiagnosticsFactory CreateDiagnosticsFactory(IActionScheduler scheduler, IProjectConfiguration projectConfiguration, ICloudProjectId cloudProjectId, IEnvironments environments)
		{
			if (TelemetryUtils.IsTelemetryDisabled(projectConfiguration))
			{
				return new DisabledDiagnosticsFactory();
			}
			TelemetryConfig telemetryConfig = TelemetryUtils.CreateTelemetryConfig(projectConfiguration);
			CachedPayload<DiagnosticsPayload> cachedPayload = new CachedPayload<DiagnosticsPayload>
			{
				Payload = new DiagnosticsPayload
				{
					Diagnostics = new List<Diagnostic>(),
					CommonTags = new Dictionary<string, string>(),
					DiagnosticsCommonTags = new Dictionary<string, string>()
				}
			};
			ICachePersister<DiagnosticsPayload> cachePersister = TelemetryUtils.CreateCachePersister<DiagnosticsPayload>("UnityServicesCachedDiagnostics", Application.platform);
			ExponentialBackOffRetryPolicy exponentialBackOffRetryPolicy = new ExponentialBackOffRetryPolicy();
			UnityWebRequestSender unityWebRequestSender = new UnityWebRequestSender();
			TelemetrySender telemetrySender = new TelemetrySender(telemetryConfig.TargetUrl, telemetryConfig.ServicePath, scheduler, exponentialBackOffRetryPolicy, unityWebRequestSender);
			DiagnosticsHandler diagnosticsHandler = new DiagnosticsHandler(telemetryConfig, cachedPayload, scheduler, cachePersister, telemetrySender);
			diagnosticsHandler.Initialize(cloudProjectId, environments);
			return new DiagnosticsFactory(diagnosticsHandler, projectConfiguration);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x000034F5 File Offset: 0x000016F5
		private static bool IsTelemetryDisabled(IProjectConfiguration projectConfiguration)
		{
			return projectConfiguration.GetBool("com.unity.services.core.telemetry-disabled", false);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003503 File Offset: 0x00001703
		internal static ICachePersister<TPayload> CreateCachePersister<TPayload>(string fileName, RuntimePlatform platform) where TPayload : ITelemetryPayload
		{
			if (platform == RuntimePlatform.Switch)
			{
				return new DisabledCachePersister<TPayload>();
			}
			return new FileCachePersister<TPayload>(fileName, CoreDiagnostics.Instance);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x0000351C File Offset: 0x0000171C
		internal static TelemetryConfig CreateTelemetryConfig(IProjectConfiguration projectConfiguration)
		{
			return new TelemetryConfig
			{
				TargetUrl = projectConfiguration.GetString("com.unity.services.core.telemetry-target-url", "https://operate-sdk-telemetry.unity3d.com"),
				ServicePath = projectConfiguration.GetString("com.unity.services.core.telemetry-service-path", "v1/record"),
				PayloadExpirationSeconds = (double)projectConfiguration.GetInt("com.unity.services.core.telemetry-payload-expiration-seconds", 3600),
				PayloadSendingMaxIntervalSeconds = (double)projectConfiguration.GetInt("com.unity.services.core.telemetry-payload-sending-max-interval-seconds", 600),
				SafetyPersistenceIntervalSeconds = (double)projectConfiguration.GetInt("com.unity.services.core.telemetry-safety-persistence-interval-seconds", 300),
				MaxMetricCountPerPayload = Math.Min(295, projectConfiguration.GetInt("com.unity.services.core.telemetry-max-metric-count-per-payload", 295))
			};
		}

		// Token: 0x06000078 RID: 120 RVA: 0x000035BF File Offset: 0x000017BF
		public static bool LogTelemetryException(Exception e, bool predicateValue = false)
		{
			return predicateValue;
		}

		// Token: 0x0400005C RID: 92
		internal const string TelemetryDisabledKey = "com.unity.services.core.telemetry-disabled";
	}
}
