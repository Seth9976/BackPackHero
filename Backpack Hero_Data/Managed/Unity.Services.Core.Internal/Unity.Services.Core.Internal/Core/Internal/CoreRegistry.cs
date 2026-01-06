using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Unity.Services.Core.Internal
{
	// Token: 0x02000036 RID: 54
	public sealed class CoreRegistry
	{
		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x00002C93 File Offset: 0x00000E93
		// (set) Token: 0x060000E9 RID: 233 RVA: 0x00002C9A File Offset: 0x00000E9A
		public static CoreRegistry Instance { get; internal set; }

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000EA RID: 234 RVA: 0x00002CA2 File Offset: 0x00000EA2
		// (set) Token: 0x060000EB RID: 235 RVA: 0x00002CAA File Offset: 0x00000EAA
		[NotNull]
		internal IPackageRegistry PackageRegistry { get; private set; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000EC RID: 236 RVA: 0x00002CB3 File Offset: 0x00000EB3
		// (set) Token: 0x060000ED RID: 237 RVA: 0x00002CBB File Offset: 0x00000EBB
		[NotNull]
		internal IComponentRegistry ComponentRegistry { get; private set; }

		// Token: 0x060000EE RID: 238 RVA: 0x00002CC4 File Offset: 0x00000EC4
		internal CoreRegistry()
		{
			DependencyTree dependencyTree = new DependencyTree();
			this.PackageRegistry = new PackageRegistry(dependencyTree);
			Dictionary<int, IServiceComponent> dictionary = new Dictionary<int, IServiceComponent>(dependencyTree.ComponentTypeHashToInstance.Count);
			this.ComponentRegistry = new ComponentRegistry(dictionary);
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00002D06 File Offset: 0x00000F06
		internal CoreRegistry([NotNull] IPackageRegistry packageRegistry, [NotNull] IComponentRegistry componentRegistry)
		{
			this.PackageRegistry = packageRegistry;
			this.ComponentRegistry = componentRegistry;
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00002D1C File Offset: 0x00000F1C
		public CoreRegistration RegisterPackage<TPackage>([NotNull] TPackage package) where TPackage : IInitializablePackage
		{
			return this.PackageRegistry.RegisterPackage<TPackage>(package);
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00002D2A File Offset: 0x00000F2A
		public void RegisterServiceComponent<TComponent>([NotNull] TComponent component) where TComponent : IServiceComponent
		{
			this.ComponentRegistry.RegisterServiceComponent<TComponent>(component);
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00002D38 File Offset: 0x00000F38
		public TComponent GetServiceComponent<TComponent>() where TComponent : IServiceComponent
		{
			return this.ComponentRegistry.GetServiceComponent<TComponent>();
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00002D45 File Offset: 0x00000F45
		internal void LockPackageRegistration()
		{
			if (this.PackageRegistry is LockedPackageRegistry)
			{
				return;
			}
			this.PackageRegistry = new LockedPackageRegistry(this.PackageRegistry);
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00002D66 File Offset: 0x00000F66
		internal void LockComponentRegistration()
		{
			if (this.ComponentRegistry is LockedComponentRegistry)
			{
				return;
			}
			this.ComponentRegistry = new LockedComponentRegistry(this.ComponentRegistry);
		}
	}
}
