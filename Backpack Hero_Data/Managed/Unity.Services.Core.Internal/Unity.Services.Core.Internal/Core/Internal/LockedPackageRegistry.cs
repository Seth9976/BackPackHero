using System;
using JetBrains.Annotations;

namespace Unity.Services.Core.Internal
{
	// Token: 0x02000044 RID: 68
	internal class LockedPackageRegistry : IPackageRegistry
	{
		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000120 RID: 288 RVA: 0x00003600 File Offset: 0x00001800
		[NotNull]
		internal IPackageRegistry Registry { get; }

		// Token: 0x06000121 RID: 289 RVA: 0x00003608 File Offset: 0x00001808
		public LockedPackageRegistry([NotNull] IPackageRegistry registryToLock)
		{
			this.Registry = registryToLock;
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000122 RID: 290 RVA: 0x00003617 File Offset: 0x00001817
		// (set) Token: 0x06000123 RID: 291 RVA: 0x00003624 File Offset: 0x00001824
		public DependencyTree Tree
		{
			get
			{
				return this.Registry.Tree;
			}
			set
			{
				this.Registry.Tree = value;
			}
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00003632 File Offset: 0x00001832
		public CoreRegistration RegisterPackage<TPackage>(TPackage package) where TPackage : IInitializablePackage
		{
			throw new InvalidOperationException("Package registration has been locked. Make sure to register service packages in[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)].");
		}

		// Token: 0x06000125 RID: 293 RVA: 0x0000363E File Offset: 0x0000183E
		public void RegisterDependency<TComponent>(int packageTypeHash) where TComponent : IServiceComponent
		{
			throw new InvalidOperationException("Package registration has been locked. Make sure to register service packages in[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)].");
		}

		// Token: 0x06000126 RID: 294 RVA: 0x0000364A File Offset: 0x0000184A
		public void RegisterOptionalDependency<TComponent>(int packageTypeHash) where TComponent : IServiceComponent
		{
			throw new InvalidOperationException("Package registration has been locked. Make sure to register service packages in[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)].");
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00003656 File Offset: 0x00001856
		public void RegisterProvision<TComponent>(int packageTypeHash) where TComponent : IServiceComponent
		{
			throw new InvalidOperationException("Package registration has been locked. Make sure to register service packages in[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)].");
		}

		// Token: 0x0400004A RID: 74
		private const string k_ErrorMessage = "Package registration has been locked. Make sure to register service packages in[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)].";
	}
}
