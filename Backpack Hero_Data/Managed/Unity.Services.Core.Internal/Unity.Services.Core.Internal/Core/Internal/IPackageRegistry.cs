using System;
using JetBrains.Annotations;

namespace Unity.Services.Core.Internal
{
	// Token: 0x02000043 RID: 67
	internal interface IPackageRegistry
	{
		// Token: 0x17000041 RID: 65
		// (get) Token: 0x0600011A RID: 282
		// (set) Token: 0x0600011B RID: 283
		[CanBeNull]
		DependencyTree Tree { get; set; }

		// Token: 0x0600011C RID: 284
		CoreRegistration RegisterPackage<TPackage>([NotNull] TPackage package) where TPackage : IInitializablePackage;

		// Token: 0x0600011D RID: 285
		void RegisterDependency<TComponent>(int packageTypeHash) where TComponent : IServiceComponent;

		// Token: 0x0600011E RID: 286
		void RegisterOptionalDependency<TComponent>(int packageTypeHash) where TComponent : IServiceComponent;

		// Token: 0x0600011F RID: 287
		void RegisterProvision<TComponent>(int packageTypeHash) where TComponent : IServiceComponent;
	}
}
