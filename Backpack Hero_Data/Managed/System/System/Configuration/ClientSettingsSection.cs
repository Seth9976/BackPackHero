using System;

namespace System.Configuration
{
	/// <summary>Represents a group of user-scoped application settings in a configuration file.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200019B RID: 411
	public sealed class ClientSettingsSection : ConfigurationSection
	{
		// Token: 0x06000ACE RID: 2766 RVA: 0x0002E965 File Offset: 0x0002CB65
		static ClientSettingsSection()
		{
			ClientSettingsSection.properties.Add(ClientSettingsSection.settings_prop);
		}

		/// <summary>Gets the collection of client settings for the section.</summary>
		/// <returns>A <see cref="T:System.Configuration.SettingElementCollection" /> containing all the client settings found in the current configuration section.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x06000AD0 RID: 2768 RVA: 0x0002E9A3 File Offset: 0x0002CBA3
		[ConfigurationProperty("", Options = ConfigurationPropertyOptions.IsDefaultCollection)]
		public SettingElementCollection Settings
		{
			get
			{
				return (SettingElementCollection)base[ClientSettingsSection.settings_prop];
			}
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06000AD1 RID: 2769 RVA: 0x0002E9B5 File Offset: 0x0002CBB5
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return ClientSettingsSection.properties;
			}
		}

		// Token: 0x0400072C RID: 1836
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x0400072D RID: 1837
		private static ConfigurationProperty settings_prop = new ConfigurationProperty("", typeof(SettingElementCollection), null, ConfigurationPropertyOptions.IsDefaultCollection);
	}
}
