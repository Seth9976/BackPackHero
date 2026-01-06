using System;

namespace System.Data.Common
{
	// Token: 0x02000386 RID: 902
	internal class DbProviderFactoryConfigSection
	{
		// Token: 0x06002B58 RID: 11096 RVA: 0x000BF81C File Offset: 0x000BDA1C
		public DbProviderFactoryConfigSection(Type FactoryType, string FactoryName, string FactoryDescription)
		{
			try
			{
				this.factType = FactoryType;
				this.name = FactoryName;
				this.invariantName = this.factType.Namespace.ToString();
				this.description = FactoryDescription;
				this.assemblyQualifiedName = this.factType.AssemblyQualifiedName.ToString();
			}
			catch
			{
				this.factType = null;
				this.name = string.Empty;
				this.invariantName = string.Empty;
				this.description = string.Empty;
				this.assemblyQualifiedName = string.Empty;
			}
		}

		// Token: 0x06002B59 RID: 11097 RVA: 0x000BF8B8 File Offset: 0x000BDAB8
		public DbProviderFactoryConfigSection(string FactoryName, string FactoryInvariantName, string FactoryDescription, string FactoryAssemblyQualifiedName)
		{
			this.factType = null;
			this.name = FactoryName;
			this.invariantName = FactoryInvariantName;
			this.description = FactoryDescription;
			this.assemblyQualifiedName = FactoryAssemblyQualifiedName;
		}

		// Token: 0x06002B5A RID: 11098 RVA: 0x000BF8E4 File Offset: 0x000BDAE4
		public bool IsNull()
		{
			return this.factType == null && this.invariantName == string.Empty;
		}

		// Token: 0x17000732 RID: 1842
		// (get) Token: 0x06002B5B RID: 11099 RVA: 0x000BF909 File Offset: 0x000BDB09
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000733 RID: 1843
		// (get) Token: 0x06002B5C RID: 11100 RVA: 0x000BF911 File Offset: 0x000BDB11
		public string InvariantName
		{
			get
			{
				return this.invariantName;
			}
		}

		// Token: 0x17000734 RID: 1844
		// (get) Token: 0x06002B5D RID: 11101 RVA: 0x000BF919 File Offset: 0x000BDB19
		public string Description
		{
			get
			{
				return this.description;
			}
		}

		// Token: 0x17000735 RID: 1845
		// (get) Token: 0x06002B5E RID: 11102 RVA: 0x000BF921 File Offset: 0x000BDB21
		public string AssemblyQualifiedName
		{
			get
			{
				return this.assemblyQualifiedName;
			}
		}

		// Token: 0x04001A75 RID: 6773
		private Type factType;

		// Token: 0x04001A76 RID: 6774
		private string name;

		// Token: 0x04001A77 RID: 6775
		private string invariantName;

		// Token: 0x04001A78 RID: 6776
		private string description;

		// Token: 0x04001A79 RID: 6777
		private string assemblyQualifiedName;
	}
}
