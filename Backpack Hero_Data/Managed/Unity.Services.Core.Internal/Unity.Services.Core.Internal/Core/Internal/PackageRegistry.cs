using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Unity.Services.Core.Internal
{
	// Token: 0x02000045 RID: 69
	internal class PackageRegistry : IPackageRegistry
	{
		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000128 RID: 296 RVA: 0x00003662 File Offset: 0x00001862
		// (set) Token: 0x06000129 RID: 297 RVA: 0x0000366A File Offset: 0x0000186A
		public DependencyTree Tree { get; set; }

		// Token: 0x0600012A RID: 298 RVA: 0x00003673 File Offset: 0x00001873
		public PackageRegistry([CanBeNull] DependencyTree tree)
		{
			this.Tree = tree;
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00003684 File Offset: 0x00001884
		public CoreRegistration RegisterPackage<TPackage>(TPackage package) where TPackage : IInitializablePackage
		{
			int hashCode = typeof(TPackage).GetHashCode();
			this.Tree.PackageTypeHashToInstance[hashCode] = package;
			this.Tree.PackageTypeHashToComponentTypeHashDependencies[hashCode] = new List<int>();
			return new CoreRegistration(this, hashCode);
		}

		// Token: 0x0600012C RID: 300 RVA: 0x000036D8 File Offset: 0x000018D8
		public void RegisterDependency<TComponent>(int packageTypeHash) where TComponent : IServiceComponent
		{
			Type typeFromHandle = typeof(TComponent);
			int hashCode = typeFromHandle.GetHashCode();
			this.Tree.ComponentTypeHashToInstance[hashCode] = new MissingComponent(typeFromHandle);
			this.AddComponentDependencyToPackage(hashCode, packageTypeHash);
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00003718 File Offset: 0x00001918
		public void RegisterOptionalDependency<TComponent>(int packageTypeHash) where TComponent : IServiceComponent
		{
			int hashCode = typeof(TComponent).GetHashCode();
			if (!this.Tree.ComponentTypeHashToInstance.ContainsKey(hashCode))
			{
				this.Tree.ComponentTypeHashToInstance[hashCode] = null;
			}
			this.AddComponentDependencyToPackage(hashCode, packageTypeHash);
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00003764 File Offset: 0x00001964
		public void RegisterProvision<TComponent>(int packageTypeHash) where TComponent : IServiceComponent
		{
			int hashCode = typeof(TComponent).GetHashCode();
			this.Tree.ComponentTypeHashToPackageTypeHash[hashCode] = packageTypeHash;
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00003794 File Offset: 0x00001994
		private void AddComponentDependencyToPackage(int componentTypeHash, int packageTypeHash)
		{
			List<int> list = this.Tree.PackageTypeHashToComponentTypeHashDependencies[packageTypeHash];
			if (!list.Contains(componentTypeHash))
			{
				list.Add(componentTypeHash);
			}
		}
	}
}
