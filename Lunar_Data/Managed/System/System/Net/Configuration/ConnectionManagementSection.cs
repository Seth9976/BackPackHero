using System;
using System.Configuration;

namespace System.Net.Configuration
{
	/// <summary>Represents the configuration section for connection management. This class cannot be inherited.</summary>
	// Token: 0x02000572 RID: 1394
	public sealed class ConnectionManagementSection : ConfigurationSection
	{
		// Token: 0x06002C24 RID: 11300 RVA: 0x0009DD06 File Offset: 0x0009BF06
		static ConnectionManagementSection()
		{
			ConnectionManagementSection.properties.Add(ConnectionManagementSection.connectionManagementProp);
		}

		/// <summary>Gets the collection of connection management objects in the section.</summary>
		/// <returns>A <see cref="T:System.Net.Configuration.ConnectionManagementElementCollection" /> that contains the connection management information for the local computer. </returns>
		// Token: 0x17000A48 RID: 2632
		// (get) Token: 0x06002C26 RID: 11302 RVA: 0x0009DD3C File Offset: 0x0009BF3C
		[ConfigurationProperty("", Options = ConfigurationPropertyOptions.IsDefaultCollection)]
		public ConnectionManagementElementCollection ConnectionManagement
		{
			get
			{
				return (ConnectionManagementElementCollection)base[ConnectionManagementSection.connectionManagementProp];
			}
		}

		// Token: 0x17000A49 RID: 2633
		// (get) Token: 0x06002C27 RID: 11303 RVA: 0x0009DD4E File Offset: 0x0009BF4E
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return ConnectionManagementSection.properties;
			}
		}

		// Token: 0x04001A48 RID: 6728
		private static ConfigurationProperty connectionManagementProp = new ConfigurationProperty("ConnectionManagement", typeof(ConnectionManagementElementCollection), null, ConfigurationPropertyOptions.IsDefaultCollection);

		// Token: 0x04001A49 RID: 6729
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
