using System;
using System.Configuration.Provider;
using System.Xml;
using Unity;

namespace System.Configuration
{
	// Token: 0x0200008A RID: 138
	public abstract class ConfigurationBuilder : ProviderBase
	{
		// Token: 0x0600048D RID: 1165 RVA: 0x00003518 File Offset: 0x00001718
		protected ConfigurationBuilder()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x00003527 File Offset: 0x00001727
		public virtual ConfigurationSection ProcessConfigurationSection(ConfigurationSection configSection)
		{
			ThrowStub.ThrowNotSupportedException();
			return null;
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x00003527 File Offset: 0x00001727
		public virtual XmlNode ProcessRawXml(XmlNode rawXml)
		{
			ThrowStub.ThrowNotSupportedException();
			return null;
		}
	}
}
