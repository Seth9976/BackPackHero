using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Unity.Services.Core.Configuration;
using Unity.Services.Core.Configuration.Internal;
using Unity.Services.Core.Device;
using Unity.Services.Core.Device.Internal;
using Unity.Services.Core.Environments.Internal;
using Unity.Services.Core.Internal;
using Unity.Services.Core.Scheduler.Internal;
using Unity.Services.Core.Telemetry.Internal;
using Unity.Services.Core.Threading.Internal;
using UnityEngine;

namespace Unity.Services.Core.Registration
{
	// Token: 0x02000002 RID: 2
	internal class CorePackageInitializer : IInitializablePackage, IDiagnosticsComponentProvider
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		// (set) Token: 0x06000002 RID: 2 RVA: 0x00002058 File Offset: 0x00000258
		internal ActionScheduler ActionScheduler { get; private set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000003 RID: 3 RVA: 0x00002061 File Offset: 0x00000261
		// (set) Token: 0x06000004 RID: 4 RVA: 0x00002069 File Offset: 0x00000269
		internal InstallationId InstallationId { get; private set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000005 RID: 5 RVA: 0x00002072 File Offset: 0x00000272
		// (set) Token: 0x06000006 RID: 6 RVA: 0x0000207A File Offset: 0x0000027A
		internal ProjectConfiguration ProjectConfig { get; private set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000007 RID: 7 RVA: 0x00002083 File Offset: 0x00000283
		// (set) Token: 0x06000008 RID: 8 RVA: 0x0000208B File Offset: 0x0000028B
		internal Environments Environments { get; private set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000009 RID: 9 RVA: 0x00002094 File Offset: 0x00000294
		// (set) Token: 0x0600000A RID: 10 RVA: 0x0000209C File Offset: 0x0000029C
		internal ExternalUserId ExternalUserId { get; private set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000B RID: 11 RVA: 0x000020A5 File Offset: 0x000002A5
		// (set) Token: 0x0600000C RID: 12 RVA: 0x000020AD File Offset: 0x000002AD
		internal ICloudProjectId CloudProjectId { get; private set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000D RID: 13 RVA: 0x000020B6 File Offset: 0x000002B6
		// (set) Token: 0x0600000E RID: 14 RVA: 0x000020BE File Offset: 0x000002BE
		internal IDiagnosticsFactory DiagnosticsFactory { get; private set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600000F RID: 15 RVA: 0x000020C7 File Offset: 0x000002C7
		// (set) Token: 0x06000010 RID: 16 RVA: 0x000020CF File Offset: 0x000002CF
		internal IMetricsFactory MetricsFactory { get; private set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000011 RID: 17 RVA: 0x000020D8 File Offset: 0x000002D8
		// (set) Token: 0x06000012 RID: 18 RVA: 0x000020E0 File Offset: 0x000002E0
		internal UnityThreadUtilsInternal UnityThreadUtils { get; private set; }

		// Token: 0x06000013 RID: 19 RVA: 0x000020EC File Offset: 0x000002EC
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void Register()
		{
			CorePackageInitializer corePackageInitializer = new CorePackageInitializer();
			CoreDiagnostics.Instance.DiagnosticsComponentProvider = corePackageInitializer;
			CoreRegistry.Instance.RegisterPackage<CorePackageInitializer>(corePackageInitializer).ProvidesComponent<IInstallationId>().ProvidesComponent<ICloudProjectId>()
				.ProvidesComponent<IActionScheduler>()
				.ProvidesComponent<IEnvironments>()
				.ProvidesComponent<IProjectConfiguration>()
				.ProvidesComponent<IMetricsFactory>()
				.ProvidesComponent<IDiagnosticsFactory>()
				.ProvidesComponent<IUnityThreadUtils>()
				.ProvidesComponent<IExternalUserId>();
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002160 File Offset: 0x00000360
		public Task Initialize(CoreRegistry registry)
		{
			CorePackageInitializer.<Initialize>d__40 <Initialize>d__;
			<Initialize>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<Initialize>d__.<>4__this = this;
			<Initialize>d__.registry = registry;
			<Initialize>d__.<>1__state = -1;
			<Initialize>d__.<>t__builder.Start<CorePackageInitializer.<Initialize>d__40>(ref <Initialize>d__);
			return <Initialize>d__.<>t__builder.Task;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000021AB File Offset: 0x000003AB
		private bool HaveInitOptionsChanged()
		{
			return this.m_CurrentInitializationOptions != null && !this.m_CurrentInitializationOptions.Values.ValueEquals(UnityServices.Instance.Options.Values);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000021D9 File Offset: 0x000003D9
		private void FreeOptionsDependantComponents()
		{
			this.ProjectConfig = null;
			this.Environments = null;
			this.DiagnosticsFactory = null;
			this.MetricsFactory = null;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000021F8 File Offset: 0x000003F8
		internal void InitializeInstallationId()
		{
			if (this.InstallationId != null)
			{
				return;
			}
			InstallationId installationId = new InstallationId();
			installationId.CreateIdentifier();
			this.InstallationId = installationId;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002224 File Offset: 0x00000424
		internal void InitializeActionScheduler()
		{
			if (this.ActionScheduler != null)
			{
				return;
			}
			ActionScheduler actionScheduler = new ActionScheduler();
			actionScheduler.JoinPlayerLoopSystem();
			this.ActionScheduler = actionScheduler;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002250 File Offset: 0x00000450
		internal async Task InitializeProjectConfigAsync([NotNull] InitializationOptions options)
		{
			if (this.ProjectConfig == null)
			{
				ProjectConfiguration projectConfiguration = await CorePackageInitializer.GenerateProjectConfigurationAsync(options);
				this.ProjectConfig = projectConfiguration;
				this.m_CurrentInitializationOptions = new InitializationOptions(options);
			}
		}

		// Token: 0x0600001A RID: 26 RVA: 0x0000229C File Offset: 0x0000049C
		internal static async Task<ProjectConfiguration> GenerateProjectConfigurationAsync([NotNull] InitializationOptions options)
		{
			SerializableProjectConfiguration serializableProjectConfiguration = await CorePackageInitializer.GetSerializedConfigOrEmptyAsync();
			if (serializableProjectConfiguration.Keys == null || serializableProjectConfiguration.Values == null)
			{
				serializableProjectConfiguration = SerializableProjectConfiguration.Empty;
			}
			Dictionary<string, ConfigurationEntry> dictionary = new Dictionary<string, ConfigurationEntry>(serializableProjectConfiguration.Keys.Length);
			dictionary.FillWith(serializableProjectConfiguration);
			dictionary.FillWith(options);
			return new ProjectConfiguration(dictionary);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000022E0 File Offset: 0x000004E0
		internal static async Task<SerializableProjectConfiguration> GetSerializedConfigOrEmptyAsync()
		{
			SerializableProjectConfiguration serializableProjectConfiguration;
			try
			{
				serializableProjectConfiguration = await ConfigurationUtils.ConfigurationLoader.GetConfigAsync();
			}
			catch (Exception ex)
			{
				CoreLogger.LogError("An error occured while trying to get the project configuration for services.\n" + ex.Message + "\n" + ex.StackTrace);
				serializableProjectConfiguration = SerializableProjectConfiguration.Empty;
			}
			return serializableProjectConfiguration;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x0000231C File Offset: 0x0000051C
		internal void InitializeExternalUserId(IProjectConfiguration projectConfiguration)
		{
			if (UnityServices.ExternalUserId == null)
			{
				string @string = projectConfiguration.GetString("com.unity.services.core.analytics-user-id", null);
				if (!string.IsNullOrEmpty(@string))
				{
					UnityServices.ExternalUserId = @string;
				}
			}
			if (this.ExternalUserId != null)
			{
				return;
			}
			this.ExternalUserId = new ExternalUserId();
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002360 File Offset: 0x00000560
		internal void InitializeEnvironments(IProjectConfiguration projectConfiguration)
		{
			if (this.Environments != null)
			{
				return;
			}
			string @string = projectConfiguration.GetString("com.unity.services.core.environment-name", "production");
			this.Environments = new Environments
			{
				Current = @string
			};
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002399 File Offset: 0x00000599
		internal void InitializeCloudProjectId(ICloudProjectId cloudProjectId = null)
		{
			if (this.CloudProjectId != null)
			{
				return;
			}
			this.CloudProjectId = cloudProjectId ?? new CloudProjectId();
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000023B4 File Offset: 0x000005B4
		internal void InitializeDiagnostics(IActionScheduler scheduler, IProjectConfiguration projectConfiguration, ICloudProjectId cloudProjectId, IEnvironments environments)
		{
			if (this.DiagnosticsFactory != null)
			{
				return;
			}
			this.DiagnosticsFactory = TelemetryUtils.CreateDiagnosticsFactory(scheduler, projectConfiguration, cloudProjectId, environments);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000023CF File Offset: 0x000005CF
		internal void InitializeMetrics(IActionScheduler scheduler, IProjectConfiguration projectConfiguration, ICloudProjectId cloudProjectId, IEnvironments environments)
		{
			if (this.MetricsFactory != null)
			{
				return;
			}
			this.MetricsFactory = TelemetryUtils.CreateMetricsFactory(scheduler, projectConfiguration, cloudProjectId, environments);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000023EA File Offset: 0x000005EA
		internal void InitializeUnityThreadUtils()
		{
			if (this.UnityThreadUtils != null)
			{
				return;
			}
			this.UnityThreadUtils = new UnityThreadUtilsInternal();
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002400 File Offset: 0x00000600
		public async Task<IDiagnosticsFactory> CreateDiagnosticsComponents()
		{
			if (this.HaveInitOptionsChanged())
			{
				this.FreeOptionsDependantComponents();
			}
			this.InitializeActionScheduler();
			await this.InitializeProjectConfigAsync(UnityServices.Instance.Options);
			this.InitializeEnvironments(this.ProjectConfig);
			this.InitializeCloudProjectId(null);
			this.InitializeDiagnostics(this.ActionScheduler, this.ProjectConfig, this.CloudProjectId, this.Environments);
			return this.DiagnosticsFactory;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002444 File Offset: 0x00000644
		[Conditional("ENABLE_UNITY_SERVICES_CORE_VERBOSE_LOGGING")]
		private void LogInitializationInfoJson()
		{
			JObject jobject = new JObject();
			JObject jobject2 = JObject.Parse(JsonConvert.SerializeObject(this.DiagnosticsFactory.CommonTags));
			JObject jobject3 = JObject.Parse(this.ProjectConfig.ToJson());
			JObject jobject4 = JObject.Parse("{\"installation_id\": \"" + this.InstallationId.Identifier + "\"}");
			jobject2.Merge(jobject4);
			jobject.Add("CommonSettings", jobject2);
			jobject.Add("ServicesRuntimeSettings", jobject3);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000024BC File Offset: 0x000006BC
		public async Task<string> GetSerializedProjectConfigurationAsync()
		{
			await this.InitializeProjectConfigAsync(UnityServices.Instance.Options);
			return this.ProjectConfig.ToJson();
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002508 File Offset: 0x00000708
		[CompilerGenerated]
		private void <Initialize>g__RegisterProvidedComponents|40_0(ref CorePackageInitializer.<>c__DisplayClass40_0 A_1)
		{
			A_1.registry.RegisterServiceComponent<IInstallationId>(this.InstallationId);
			A_1.registry.RegisterServiceComponent<IActionScheduler>(this.ActionScheduler);
			A_1.registry.RegisterServiceComponent<IProjectConfiguration>(this.ProjectConfig);
			A_1.registry.RegisterServiceComponent<IEnvironments>(this.Environments);
			A_1.registry.RegisterServiceComponent<ICloudProjectId>(this.CloudProjectId);
			A_1.registry.RegisterServiceComponent<IDiagnosticsFactory>(this.DiagnosticsFactory);
			A_1.registry.RegisterServiceComponent<IMetricsFactory>(this.MetricsFactory);
			A_1.registry.RegisterServiceComponent<IUnityThreadUtils>(this.UnityThreadUtils);
			A_1.registry.RegisterServiceComponent<IExternalUserId>(this.ExternalUserId);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000025AE File Offset: 0x000007AE
		[CompilerGenerated]
		internal static bool <Initialize>g__SendFailedInitDiagnostic|40_1(Exception reason)
		{
			CoreDiagnostics.Instance.SendCorePackageInitDiagnostics(reason);
			return false;
		}

		// Token: 0x04000001 RID: 1
		internal const string CorePackageName = "com.unity.services.core";

		// Token: 0x04000002 RID: 2
		internal const string ProjectUnlinkMessage = "To use Unity's dashboard services, you need to link your Unity project to a project ID. To do this, go to Project Settings to select your organization, select your project and then link a project ID. You also need to make sure your organization has access to the required products. Visit https://dashboard.unity3d.com to sign up.";

		// Token: 0x0400000C RID: 12
		private InitializationOptions m_CurrentInitializationOptions;
	}
}
