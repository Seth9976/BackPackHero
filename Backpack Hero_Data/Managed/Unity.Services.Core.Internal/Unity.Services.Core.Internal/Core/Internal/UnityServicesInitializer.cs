using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Unity.Services.Core.Internal
{
	// Token: 0x02000048 RID: 72
	internal static class UnityServicesInitializer
	{
		// Token: 0x0600014A RID: 330 RVA: 0x00003AD4 File Offset: 0x00001CD4
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
		private static void CreateStaticInstance()
		{
			CoreRegistry.Instance = new CoreRegistry();
			CoreMetrics.Instance = new CoreMetrics();
			CoreDiagnostics.Instance = new CoreDiagnostics();
			UnityServices.Instance = new UnityServicesInternal(CoreRegistry.Instance, CoreMetrics.Instance, CoreDiagnostics.Instance);
			TaskCompletionSource<object> instantiationCompletion = UnityServices.InstantiationCompletion;
			if (instantiationCompletion == null)
			{
				return;
			}
			instantiationCompletion.TrySetResult(null);
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00003B2C File Offset: 0x00001D2C
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
		private static async void EnableServicesInitializationAsync()
		{
			await ((UnityServicesInternal)UnityServices.Instance).EnableInitializationAsync();
		}
	}
}
