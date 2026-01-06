using System;
using System.Security.Permissions;

namespace System.Configuration
{
	// Token: 0x02000196 RID: 406
	[ConfigurationPermission(SecurityAction.Assert, Unrestricted = true)]
	internal static class PrivilegedConfigurationManager
	{
		// Token: 0x170001AD RID: 429
		// (get) Token: 0x06000AA5 RID: 2725 RVA: 0x0002DB87 File Offset: 0x0002BD87
		internal static ConnectionStringSettingsCollection ConnectionStrings
		{
			get
			{
				return ConfigurationManager.ConnectionStrings;
			}
		}

		// Token: 0x06000AA6 RID: 2726 RVA: 0x0002DB8E File Offset: 0x0002BD8E
		internal static object GetSection(string sectionName)
		{
			return ConfigurationManager.GetSection(sectionName);
		}
	}
}
