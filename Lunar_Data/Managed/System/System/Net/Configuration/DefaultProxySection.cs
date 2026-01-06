using System;
using System.Configuration;

namespace System.Net.Configuration
{
	/// <summary>Represents the configuration section for Web proxy server usage. This class cannot be inherited.</summary>
	// Token: 0x02000574 RID: 1396
	public sealed class DefaultProxySection : ConfigurationSection
	{
		// Token: 0x06002C2B RID: 11307 RVA: 0x0009E180 File Offset: 0x0009C380
		static DefaultProxySection()
		{
			DefaultProxySection.properties.Add(DefaultProxySection.bypassListProp);
			DefaultProxySection.properties.Add(DefaultProxySection.enabledProp);
			DefaultProxySection.properties.Add(DefaultProxySection.moduleProp);
			DefaultProxySection.properties.Add(DefaultProxySection.proxyProp);
			DefaultProxySection.properties.Add(DefaultProxySection.useDefaultCredentialsProp);
		}

		/// <summary>Gets the collection of resources that are not obtained using the Web proxy server.</summary>
		/// <returns>A <see cref="T:System.Net.Configuration.BypassElementCollection" /> that contains the addresses of resources that bypass the Web proxy server. </returns>
		// Token: 0x17000A4A RID: 2634
		// (get) Token: 0x06002C2D RID: 11309 RVA: 0x0009E26E File Offset: 0x0009C46E
		[ConfigurationProperty("bypasslist")]
		public BypassElementCollection BypassList
		{
			get
			{
				return (BypassElementCollection)base[DefaultProxySection.bypassListProp];
			}
		}

		/// <summary>Gets or sets whether a Web proxy is used.</summary>
		/// <returns>true if a Web proxy will be used; otherwise, false.</returns>
		// Token: 0x17000A4B RID: 2635
		// (get) Token: 0x06002C2E RID: 11310 RVA: 0x0009E280 File Offset: 0x0009C480
		// (set) Token: 0x06002C2F RID: 11311 RVA: 0x0009E292 File Offset: 0x0009C492
		[ConfigurationProperty("enabled", DefaultValue = "True")]
		public bool Enabled
		{
			get
			{
				return (bool)base[DefaultProxySection.enabledProp];
			}
			set
			{
				base[DefaultProxySection.enabledProp] = value;
			}
		}

		/// <summary>Gets the type information for a custom Web proxy implementation.</summary>
		/// <returns>A <see cref="T:System.Net.Configuration.ModuleElement" />. The type information for a custom Web proxy implementation.</returns>
		// Token: 0x17000A4C RID: 2636
		// (get) Token: 0x06002C30 RID: 11312 RVA: 0x0009E2A5 File Offset: 0x0009C4A5
		[ConfigurationProperty("module")]
		public ModuleElement Module
		{
			get
			{
				return (ModuleElement)base[DefaultProxySection.moduleProp];
			}
		}

		/// <summary>Gets the URI that identifies the Web proxy server to use.</summary>
		/// <returns>A <see cref="T:System.Net.Configuration.ProxyElement" />. The URI that identifies the Web proxy server.</returns>
		// Token: 0x17000A4D RID: 2637
		// (get) Token: 0x06002C31 RID: 11313 RVA: 0x0009E2B7 File Offset: 0x0009C4B7
		[ConfigurationProperty("proxy")]
		public ProxyElement Proxy
		{
			get
			{
				return (ProxyElement)base[DefaultProxySection.proxyProp];
			}
		}

		/// <summary>Gets or sets whether default credentials are to be used to access a Web proxy server.</summary>
		/// <returns>true if default credentials are to be used; otherwise, false.</returns>
		// Token: 0x17000A4E RID: 2638
		// (get) Token: 0x06002C32 RID: 11314 RVA: 0x0009E2C9 File Offset: 0x0009C4C9
		// (set) Token: 0x06002C33 RID: 11315 RVA: 0x0009E2DB File Offset: 0x0009C4DB
		[ConfigurationProperty("useDefaultCredentials", DefaultValue = "False")]
		public bool UseDefaultCredentials
		{
			get
			{
				return (bool)base[DefaultProxySection.useDefaultCredentialsProp];
			}
			set
			{
				base[DefaultProxySection.useDefaultCredentialsProp] = value;
			}
		}

		// Token: 0x17000A4F RID: 2639
		// (get) Token: 0x06002C34 RID: 11316 RVA: 0x0009E2EE File Offset: 0x0009C4EE
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return DefaultProxySection.properties;
			}
		}

		// Token: 0x06002C35 RID: 11317 RVA: 0x00003917 File Offset: 0x00001B17
		[MonoTODO]
		protected override void PostDeserialize()
		{
		}

		// Token: 0x06002C36 RID: 11318 RVA: 0x00003917 File Offset: 0x00001B17
		[MonoTODO]
		protected override void Reset(ConfigurationElement parentElement)
		{
		}

		// Token: 0x04001A4A RID: 6730
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04001A4B RID: 6731
		private static ConfigurationProperty bypassListProp = new ConfigurationProperty("bypasslist", typeof(BypassElementCollection), null);

		// Token: 0x04001A4C RID: 6732
		private static ConfigurationProperty enabledProp = new ConfigurationProperty("enabled", typeof(bool), true);

		// Token: 0x04001A4D RID: 6733
		private static ConfigurationProperty moduleProp = new ConfigurationProperty("module", typeof(ModuleElement), null);

		// Token: 0x04001A4E RID: 6734
		private static ConfigurationProperty proxyProp = new ConfigurationProperty("proxy", typeof(ProxyElement), null);

		// Token: 0x04001A4F RID: 6735
		private static ConfigurationProperty useDefaultCredentialsProp = new ConfigurationProperty("useDefaultCredentials", typeof(bool), false);
	}
}
