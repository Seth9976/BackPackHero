using System;
using System.Threading.Tasks;
using Unity.Services.Analytics;
using Unity.Services.Analytics.Data;
using Unity.Services.Analytics.Internal;
using Unity.Services.Authentication.Internal;
using Unity.Services.Core.Analytics.Internal;
using Unity.Services.Core.Configuration.Internal;
using Unity.Services.Core.Device.Internal;
using Unity.Services.Core.Environments.Internal;
using Unity.Services.Core.Internal;
using UnityEngine;

// Token: 0x02000002 RID: 2
internal class Ua2CoreInitializeCallback : IInitializablePackage
{
	// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
	private static void Register()
	{
		CoreRegistry.Instance.RegisterPackage<Ua2CoreInitializeCallback>(new Ua2CoreInitializeCallback()).DependsOn<IInstallationId>().DependsOn<ICloudProjectId>()
			.DependsOn<IEnvironments>()
			.DependsOn<IExternalUserId>()
			.DependsOn<IProjectConfiguration>()
			.OptionallyDependsOn<IPlayerId>()
			.ProvidesComponent<IAnalyticsStandardEventComponent>();
	}

	// Token: 0x06000002 RID: 2 RVA: 0x000020A8 File Offset: 0x000002A8
	public async Task Initialize(CoreRegistry registry)
	{
		ICloudProjectId serviceComponent = registry.GetServiceComponent<ICloudProjectId>();
		IInstallationId serviceComponent2 = registry.GetServiceComponent<IInstallationId>();
		IPlayerId serviceComponent3 = registry.GetServiceComponent<IPlayerId>();
		IEnvironments serviceComponent4 = registry.GetServiceComponent<IEnvironments>();
		IExternalUserId serviceComponent5 = registry.GetServiceComponent<IExternalUserId>();
		CoreStatsHelper coreStatsHelper = new CoreStatsHelper();
		ConsentTracker consentTracker = new ConsentTracker(coreStatsHelper);
		BufferX bufferX = new BufferX(new BufferSystemCalls(), new DiskCache(new FileSystemCalls()));
		AnalyticsService.internalInstance = new AnalyticsServiceInstance(new DataGenerator(), bufferX, new BufferRevoked(), coreStatsHelper, consentTracker, new Dispatcher(new WebRequestHelper(), consentTracker), new AnalyticsForgetter(consentTracker), serviceComponent, serviceComponent2, serviceComponent3, serviceComponent4.Current, serviceComponent5, new AnalyticsServiceSystemCalls());
		StandardEventServiceComponent standardEventServiceComponent = new StandardEventServiceComponent(registry.GetServiceComponent<IProjectConfiguration>(), AnalyticsService.internalInstance);
		registry.RegisterServiceComponent<IAnalyticsStandardEventComponent>(standardEventServiceComponent);
		bufferX.LoadFromDisk();
		await AnalyticsService.internalInstance.Initialize();
		if (consentTracker.IsGeoIpChecked())
		{
			AnalyticsService.internalInstance.Flush();
		}
	}
}
