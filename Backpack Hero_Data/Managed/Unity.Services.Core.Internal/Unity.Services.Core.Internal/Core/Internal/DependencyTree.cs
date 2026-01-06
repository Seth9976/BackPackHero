using System;
using System.Collections.Generic;

namespace Unity.Services.Core.Internal
{
	// Token: 0x02000038 RID: 56
	internal class DependencyTree
	{
		// Token: 0x060000F7 RID: 247 RVA: 0x00002DE3 File Offset: 0x00000FE3
		internal DependencyTree()
			: this(new Dictionary<int, IInitializablePackage>(), new Dictionary<int, int>(), new Dictionary<int, List<int>>(), new Dictionary<int, IServiceComponent>())
		{
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00002DFF File Offset: 0x00000FFF
		internal DependencyTree(Dictionary<int, IInitializablePackage> packageToInstance, Dictionary<int, int> componentToPackage, Dictionary<int, List<int>> packageToComponentDependencies, Dictionary<int, IServiceComponent> componentToInstance)
		{
			this.PackageTypeHashToInstance = packageToInstance;
			this.ComponentTypeHashToPackageTypeHash = componentToPackage;
			this.PackageTypeHashToComponentTypeHashDependencies = packageToComponentDependencies;
			this.ComponentTypeHashToInstance = componentToInstance;
		}

		// Token: 0x0400003F RID: 63
		public readonly Dictionary<int, IInitializablePackage> PackageTypeHashToInstance;

		// Token: 0x04000040 RID: 64
		public readonly Dictionary<int, int> ComponentTypeHashToPackageTypeHash;

		// Token: 0x04000041 RID: 65
		public readonly Dictionary<int, List<int>> PackageTypeHashToComponentTypeHashDependencies;

		// Token: 0x04000042 RID: 66
		public readonly Dictionary<int, IServiceComponent> ComponentTypeHashToInstance;
	}
}
