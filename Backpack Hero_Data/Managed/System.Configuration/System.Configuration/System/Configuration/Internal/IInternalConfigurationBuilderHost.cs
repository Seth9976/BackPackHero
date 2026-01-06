using System;
using System.Runtime.InteropServices;
using System.Xml;

namespace System.Configuration.Internal
{
	// Token: 0x0200008C RID: 140
	[ComVisible(false)]
	public interface IInternalConfigurationBuilderHost
	{
		// Token: 0x06000490 RID: 1168
		ConfigurationSection ProcessConfigurationSection(ConfigurationSection configSection, ConfigurationBuilder builder);

		// Token: 0x06000491 RID: 1169
		XmlNode ProcessRawXml(XmlNode rawXml, ConfigurationBuilder builder);
	}
}
