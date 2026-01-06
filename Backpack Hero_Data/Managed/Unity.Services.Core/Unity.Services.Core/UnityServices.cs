using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;

namespace Unity.Services.Core
{
	// Token: 0x0200000B RID: 11
	public static class UnityServices
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000023 RID: 35 RVA: 0x00002254 File Offset: 0x00000454
		// (set) Token: 0x06000024 RID: 36 RVA: 0x0000225B File Offset: 0x0000045B
		internal static IUnityServices Instance { get; set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000025 RID: 37 RVA: 0x00002263 File Offset: 0x00000463
		// (set) Token: 0x06000026 RID: 38 RVA: 0x0000226A File Offset: 0x0000046A
		internal static TaskCompletionSource<object> InstantiationCompletion { get; set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000027 RID: 39 RVA: 0x00002274 File Offset: 0x00000474
		public static ServicesInitializationState State
		{
			get
			{
				if (!UnityThreadUtils.IsRunningOnUnityThread)
				{
					throw new ServicesInitializationException("You are attempting to access UnityServices.State from a non-Unity Thread. UnityServices.State can only be accessed from Unity Thread");
				}
				if (UnityServices.Instance != null)
				{
					return UnityServices.Instance.State;
				}
				TaskCompletionSource<object> instantiationCompletion = UnityServices.InstantiationCompletion;
				if (instantiationCompletion != null && instantiationCompletion.Task.Status == TaskStatus.WaitingForActivation)
				{
					return ServicesInitializationState.Initializing;
				}
				return ServicesInitializationState.Uninitialized;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000028 RID: 40 RVA: 0x000022C3 File Offset: 0x000004C3
		// (set) Token: 0x06000029 RID: 41 RVA: 0x000022CF File Offset: 0x000004CF
		public static string ExternalUserId
		{
			get
			{
				return UnityServices.ExternalUserIdProperty.UserId;
			}
			set
			{
				UnityServices.ExternalUserIdProperty.UserId = value;
			}
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000022DC File Offset: 0x000004DC
		public static Task InitializeAsync()
		{
			return UnityServices.InitializeAsync(new InitializationOptions());
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000022E8 File Offset: 0x000004E8
		[PreserveDependency("Register()", "Unity.Services.Core.Registration.CorePackageInitializer", "Unity.Services.Core.Registration")]
		[PreserveDependency("CreateStaticInstance()", "Unity.Services.Core.Internal.UnityServicesInitializer", "Unity.Services.Core.Internal")]
		[PreserveDependency("EnableServicesInitializationAsync()", "Unity.Services.Core.Internal.UnityServicesInitializer", "Unity.Services.Core.Internal")]
		[PreserveDependency("CaptureUnityThreadInfo()", "Unity.Services.Core.UnityThreadUtils", "Unity.Services.Core")]
		public static async Task InitializeAsync(InitializationOptions options)
		{
			if (!UnityThreadUtils.IsRunningOnUnityThread)
			{
				throw new ServicesInitializationException("You are attempting to initialize Unity Services from a non-Unity Thread. Unity Services can only be initialized from Unity Thread");
			}
			if (!Application.isPlaying)
			{
				throw new ServicesInitializationException("You are attempting to initialize Unity Services in Edit Mode. Unity Services can only be initialized in Play Mode");
			}
			if (UnityServices.Instance == null)
			{
				if (UnityServices.InstantiationCompletion == null)
				{
					UnityServices.InstantiationCompletion = new TaskCompletionSource<object>();
				}
				await UnityServices.InstantiationCompletion.Task;
			}
			if (options == null)
			{
				options = new InitializationOptions();
			}
			await UnityServices.Instance.InitializeAsync(options);
		}

		// Token: 0x04000018 RID: 24
		internal static ExternalUserIdProperty ExternalUserIdProperty = new ExternalUserIdProperty();
	}
}
