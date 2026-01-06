using System;

namespace Unity.Services.Core.Internal
{
	// Token: 0x02000035 RID: 53
	public readonly struct CoreRegistration
	{
		// Token: 0x060000E4 RID: 228 RVA: 0x00002C38 File Offset: 0x00000E38
		internal CoreRegistration(IPackageRegistry registry, int packageHash)
		{
			this.m_Registry = registry;
			this.m_PackageHash = packageHash;
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00002C48 File Offset: 0x00000E48
		public CoreRegistration DependsOn<T>() where T : IServiceComponent
		{
			this.m_Registry.RegisterDependency<T>(this.m_PackageHash);
			return this;
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00002C61 File Offset: 0x00000E61
		public CoreRegistration OptionallyDependsOn<T>() where T : IServiceComponent
		{
			this.m_Registry.RegisterOptionalDependency<T>(this.m_PackageHash);
			return this;
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00002C7A File Offset: 0x00000E7A
		public CoreRegistration ProvidesComponent<T>() where T : IServiceComponent
		{
			this.m_Registry.RegisterProvision<T>(this.m_PackageHash);
			return this;
		}

		// Token: 0x04000038 RID: 56
		private readonly IPackageRegistry m_Registry;

		// Token: 0x04000039 RID: 57
		private readonly int m_PackageHash;
	}
}
