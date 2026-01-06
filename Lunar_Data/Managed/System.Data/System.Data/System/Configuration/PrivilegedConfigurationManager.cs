using System;
using System.Security.Permissions;

namespace System.Configuration
{
	// Token: 0x02000025 RID: 37
	[ConfigurationPermission(SecurityAction.Assert, Unrestricted = true)]
	internal static class PrivilegedConfigurationManager
	{
		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000E1 RID: 225 RVA: 0x00005752 File Offset: 0x00003952
		internal static ConnectionStringSettingsCollection ConnectionStrings
		{
			get
			{
				return ConfigurationManager.ConnectionStrings;
			}
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00005759 File Offset: 0x00003959
		internal static object GetSection(string sectionName)
		{
			return ConfigurationManager.GetSection(sectionName);
		}
	}
}
