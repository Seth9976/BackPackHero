using System;
using System.Configuration;

namespace System.CodeDom.Compiler
{
	// Token: 0x02000357 RID: 855
	internal sealed class CodeDomConfigurationHandler : ConfigurationSection
	{
		// Token: 0x06001C49 RID: 7241 RVA: 0x00066C68 File Offset: 0x00064E68
		static CodeDomConfigurationHandler()
		{
			CodeDomConfigurationHandler.properties.Add(CodeDomConfigurationHandler.compilersProp);
		}

		// Token: 0x06001C4B RID: 7243 RVA: 0x00066CB6 File Offset: 0x00064EB6
		protected override void InitializeDefault()
		{
			CodeDomConfigurationHandler.compilersProp = new ConfigurationProperty("compilers", typeof(CompilerCollection), CodeDomConfigurationHandler.default_compilers);
		}

		// Token: 0x06001C4C RID: 7244 RVA: 0x00066CD6 File Offset: 0x00064ED6
		[MonoTODO]
		protected override void PostDeserialize()
		{
			base.PostDeserialize();
		}

		// Token: 0x06001C4D RID: 7245 RVA: 0x00007575 File Offset: 0x00005775
		protected override object GetRuntimeObject()
		{
			return this;
		}

		// Token: 0x170005AA RID: 1450
		// (get) Token: 0x06001C4E RID: 7246 RVA: 0x00066CDE File Offset: 0x00064EDE
		[ConfigurationProperty("compilers")]
		public CompilerCollection Compilers
		{
			get
			{
				return (CompilerCollection)base[CodeDomConfigurationHandler.compilersProp];
			}
		}

		// Token: 0x170005AB RID: 1451
		// (get) Token: 0x06001C4F RID: 7247 RVA: 0x00066CF0 File Offset: 0x00064EF0
		public CompilerInfo[] CompilerInfos
		{
			get
			{
				CompilerCollection compilerCollection = (CompilerCollection)base[CodeDomConfigurationHandler.compilersProp];
				if (compilerCollection == null)
				{
					return null;
				}
				return compilerCollection.CompilerInfos;
			}
		}

		// Token: 0x170005AC RID: 1452
		// (get) Token: 0x06001C50 RID: 7248 RVA: 0x00066D0D File Offset: 0x00064F0D
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return CodeDomConfigurationHandler.properties;
			}
		}

		// Token: 0x04000E74 RID: 3700
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04000E75 RID: 3701
		private static ConfigurationProperty compilersProp = new ConfigurationProperty("compilers", typeof(CompilerCollection), CodeDomConfigurationHandler.default_compilers);

		// Token: 0x04000E76 RID: 3702
		private static CompilerCollection default_compilers = new CompilerCollection();
	}
}
