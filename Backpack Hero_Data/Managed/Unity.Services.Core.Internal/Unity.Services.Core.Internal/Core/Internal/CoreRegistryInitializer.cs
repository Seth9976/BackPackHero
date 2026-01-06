using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Unity.Services.Core.Internal
{
	// Token: 0x02000037 RID: 55
	internal class CoreRegistryInitializer
	{
		// Token: 0x060000F5 RID: 245 RVA: 0x00002D87 File Offset: 0x00000F87
		public CoreRegistryInitializer([NotNull] CoreRegistry registry, [NotNull] List<int> sortedPackageTypeHashes)
		{
			this.m_Registry = registry;
			this.m_SortedPackageTypeHashes = sortedPackageTypeHashes;
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00002DA0 File Offset: 0x00000FA0
		public async Task<List<PackageInitializationInfo>> InitializeRegistryAsync()
		{
			CoreRegistryInitializer.<>c__DisplayClass3_0 CS$<>8__locals1 = new CoreRegistryInitializer.<>c__DisplayClass3_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.packagesInitInfos = new List<PackageInitializationInfo>(this.m_SortedPackageTypeHashes.Count);
			List<PackageInitializationInfo> list;
			if (this.m_SortedPackageTypeHashes.Count <= 0)
			{
				list = CS$<>8__locals1.packagesInitInfos;
			}
			else
			{
				CS$<>8__locals1.dependencyTree = this.m_Registry.PackageRegistry.Tree;
				if (CS$<>8__locals1.dependencyTree == null)
				{
					NullReferenceException ex = new NullReferenceException("Registry requires a valid dependency tree to be initialized.");
					throw new ServicesInitializationException("Registry is in an invalid state (dependency tree is null) and can't be initialized.", ex);
				}
				this.m_Registry.ComponentRegistry.ResetProvidedComponents(CS$<>8__locals1.dependencyTree.ComponentTypeHashToInstance);
				CS$<>8__locals1.failureReasons = new List<Exception>(this.m_SortedPackageTypeHashes.Count);
				CS$<>8__locals1.stopwatch = new Stopwatch();
				for (int i = 0; i < this.m_SortedPackageTypeHashes.Count; i++)
				{
					IInitializablePackage initializablePackage = CS$<>8__locals1.<InitializeRegistryAsync>g__GetPackageAt|1(i);
					await CS$<>8__locals1.<InitializeRegistryAsync>g__TryInitializePackageAsync|0(initializablePackage);
				}
				if (CS$<>8__locals1.failureReasons.Count > 0)
				{
					CS$<>8__locals1.<InitializeRegistryAsync>g__Fail|2();
				}
				list = CS$<>8__locals1.packagesInitInfos;
			}
			return list;
		}

		// Token: 0x0400003D RID: 61
		[NotNull]
		private readonly CoreRegistry m_Registry;

		// Token: 0x0400003E RID: 62
		[NotNull]
		private readonly List<int> m_SortedPackageTypeHashes;
	}
}
