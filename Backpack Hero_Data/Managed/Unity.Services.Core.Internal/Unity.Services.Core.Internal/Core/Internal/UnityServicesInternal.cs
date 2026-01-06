using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Unity.Services.Core.Internal
{
	// Token: 0x02000049 RID: 73
	internal class UnityServicesInternal : IUnityServices
	{
		// Token: 0x1700004C RID: 76
		// (get) Token: 0x0600014C RID: 332 RVA: 0x00003B5B File Offset: 0x00001D5B
		// (set) Token: 0x0600014D RID: 333 RVA: 0x00003B63 File Offset: 0x00001D63
		public ServicesInitializationState State { get; private set; }

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600014E RID: 334 RVA: 0x00003B6C File Offset: 0x00001D6C
		// (set) Token: 0x0600014F RID: 335 RVA: 0x00003B74 File Offset: 0x00001D74
		public InitializationOptions Options { get; internal set; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000150 RID: 336 RVA: 0x00003B7D File Offset: 0x00001D7D
		[NotNull]
		private CoreRegistry Registry { get; }

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000151 RID: 337 RVA: 0x00003B85 File Offset: 0x00001D85
		[NotNull]
		private CoreMetrics Metrics { get; }

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000152 RID: 338 RVA: 0x00003B8D File Offset: 0x00001D8D
		[NotNull]
		private CoreDiagnostics Diagnostics { get; }

		// Token: 0x06000153 RID: 339 RVA: 0x00003B95 File Offset: 0x00001D95
		public UnityServicesInternal([NotNull] CoreRegistry registry, [NotNull] CoreMetrics metrics, [NotNull] CoreDiagnostics diagnostics)
		{
			this.Registry = registry;
			this.Metrics = metrics;
			this.Diagnostics = diagnostics;
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00003BB4 File Offset: 0x00001DB4
		public async Task InitializeAsync(InitializationOptions options)
		{
			if (!this.HasRequestedInitialization() || this.<InitializeAsync>g__HasInitializationFailed|20_0())
			{
				this.Options = options;
				this.m_Initialization = new TaskCompletionSource<object>();
			}
			if (!this.CanInitialize || this.State != ServicesInitializationState.Uninitialized)
			{
				await this.m_Initialization.Task;
			}
			else
			{
				await this.InitializeServicesAsync();
			}
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00003BFF File Offset: 0x00001DFF
		private bool HasRequestedInitialization()
		{
			return this.m_Initialization != null;
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00003C10 File Offset: 0x00001E10
		private async Task InitializeServicesAsync()
		{
			UnityServicesInternal.<>c__DisplayClass22_0 CS$<>8__locals1 = new UnityServicesInternal.<>c__DisplayClass22_0();
			CS$<>8__locals1.<>4__this = this;
			this.State = ServicesInitializationState.Initializing;
			CS$<>8__locals1.initStopwatch = new Stopwatch();
			CS$<>8__locals1.initStopwatch.Start();
			CS$<>8__locals1.dependencyTree = this.Registry.PackageRegistry.Tree;
			if (CS$<>8__locals1.dependencyTree == null)
			{
				NullReferenceException ex = new NullReferenceException("Services require a valid dependency tree to be initialized.");
				CS$<>8__locals1.<InitializeServicesAsync>g__FailServicesInitialization|2(ex);
				throw ex;
			}
			CS$<>8__locals1.sortedPackageTypeHashes = new List<int>(CS$<>8__locals1.dependencyTree.PackageTypeHashToInstance.Count);
			List<PackageInitializationInfo> list;
			try
			{
				CS$<>8__locals1.<InitializeServicesAsync>g__SortPackages|0();
				list = await CS$<>8__locals1.<InitializeServicesAsync>g__InitializePackagesAsync|1();
			}
			catch (Exception ex2)
			{
				CS$<>8__locals1.<InitializeServicesAsync>g__FailServicesInitialization|2(ex2);
				throw;
			}
			this.SendInitializationMetrics(list);
			CS$<>8__locals1.<InitializeServicesAsync>g__SucceedServicesInitialization|3();
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00003C54 File Offset: 0x00001E54
		internal void SendInitializationMetrics(List<PackageInitializationInfo> packageInitInfos)
		{
			foreach (PackageInitializationInfo packageInitializationInfo in packageInitInfos)
			{
				this.Metrics.SendInitTimeMetricForPackage(packageInitializationInfo.PackageType, packageInitializationInfo.InitializationTimeInSeconds);
			}
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00003CB4 File Offset: 0x00001EB4
		internal async Task EnableInitializationAsync()
		{
			this.CanInitialize = true;
			this.Registry.LockPackageRegistration();
			if (this.HasRequestedInitialization())
			{
				await this.InitializeServicesAsync();
			}
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00003CF7 File Offset: 0x00001EF7
		[CompilerGenerated]
		private bool <InitializeAsync>g__HasInitializationFailed|20_0()
		{
			return this.m_Initialization.Task.IsCompleted && this.m_Initialization.Task.Status != TaskStatus.RanToCompletion;
		}

		// Token: 0x04000062 RID: 98
		internal bool CanInitialize;

		// Token: 0x04000063 RID: 99
		private TaskCompletionSource<object> m_Initialization;
	}
}
