using System;
using Unity;

namespace System.Configuration
{
	// Token: 0x0200008F RID: 143
	public sealed class ConfigurationBuildersSection : ConfigurationSection
	{
		// Token: 0x06000495 RID: 1173 RVA: 0x00003518 File Offset: 0x00001718
		public ConfigurationBuildersSection()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x06000496 RID: 1174 RVA: 0x00003527 File Offset: 0x00001727
		public ProviderSettingsCollection Builders
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x00003527 File Offset: 0x00001727
		public ConfigurationBuilder GetBuilderFromName(string builderName)
		{
			ThrowStub.ThrowNotSupportedException();
			return null;
		}
	}
}
