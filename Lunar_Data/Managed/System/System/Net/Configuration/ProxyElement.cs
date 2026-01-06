using System;
using System.Configuration;

namespace System.Net.Configuration
{
	/// <summary>Identifies the configuration settings for Web proxy server. This class cannot be inherited.</summary>
	// Token: 0x0200057F RID: 1407
	public sealed class ProxyElement : ConfigurationElement
	{
		// Token: 0x06002C76 RID: 11382 RVA: 0x0009EB94 File Offset: 0x0009CD94
		static ProxyElement()
		{
			ProxyElement.properties.Add(ProxyElement.autoDetectProp);
			ProxyElement.properties.Add(ProxyElement.bypassOnLocalProp);
			ProxyElement.properties.Add(ProxyElement.proxyAddressProp);
			ProxyElement.properties.Add(ProxyElement.scriptLocationProp);
			ProxyElement.properties.Add(ProxyElement.useSystemDefaultProp);
		}

		/// <summary>Gets or sets an <see cref="T:System.Net.Configuration.ProxyElement.AutoDetectValues" /> value that controls whether the Web proxy is automatically detected.</summary>
		/// <returns>
		///   <see cref="F:System.Net.Configuration.ProxyElement.AutoDetectValues.True" /> if the <see cref="T:System.Net.WebProxy" /> is automatically detected; <see cref="F:System.Net.Configuration.ProxyElement.AutoDetectValues.False" /> if the <see cref="T:System.Net.WebProxy" /> is not automatically detected; or <see cref="F:System.Net.Configuration.ProxyElement.AutoDetectValues.Unspecified" />.</returns>
		// Token: 0x17000A6A RID: 2666
		// (get) Token: 0x06002C78 RID: 11384 RVA: 0x0009EC87 File Offset: 0x0009CE87
		// (set) Token: 0x06002C79 RID: 11385 RVA: 0x0009EC99 File Offset: 0x0009CE99
		[ConfigurationProperty("autoDetect", DefaultValue = "Unspecified")]
		public ProxyElement.AutoDetectValues AutoDetect
		{
			get
			{
				return (ProxyElement.AutoDetectValues)base[ProxyElement.autoDetectProp];
			}
			set
			{
				base[ProxyElement.autoDetectProp] = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether local resources are retrieved by using a Web proxy server.</summary>
		/// <returns>A <see cref="T:System.Net.Configuration.ProxyElement.BypassOnLocalValues" />.Avalue that indicates whether local resources are retrieved by using a Web proxy server.</returns>
		// Token: 0x17000A6B RID: 2667
		// (get) Token: 0x06002C7A RID: 11386 RVA: 0x0009ECAC File Offset: 0x0009CEAC
		// (set) Token: 0x06002C7B RID: 11387 RVA: 0x0009ECBE File Offset: 0x0009CEBE
		[ConfigurationProperty("bypassonlocal", DefaultValue = "Unspecified")]
		public ProxyElement.BypassOnLocalValues BypassOnLocal
		{
			get
			{
				return (ProxyElement.BypassOnLocalValues)base[ProxyElement.bypassOnLocalProp];
			}
			set
			{
				base[ProxyElement.bypassOnLocalProp] = value;
			}
		}

		/// <summary>Gets or sets the URI that identifies the Web proxy server to use.</summary>
		/// <returns>A <see cref="T:System.String" /> containing a URI.</returns>
		// Token: 0x17000A6C RID: 2668
		// (get) Token: 0x06002C7C RID: 11388 RVA: 0x0009ECD1 File Offset: 0x0009CED1
		// (set) Token: 0x06002C7D RID: 11389 RVA: 0x0009ECE3 File Offset: 0x0009CEE3
		[ConfigurationProperty("proxyaddress")]
		public Uri ProxyAddress
		{
			get
			{
				return (Uri)base[ProxyElement.proxyAddressProp];
			}
			set
			{
				base[ProxyElement.proxyAddressProp] = value;
			}
		}

		/// <summary>Gets or sets an <see cref="T:System.Uri" /> value that specifies the location of the automatic proxy detection script.</summary>
		/// <returns>A <see cref="T:System.Uri" /> specifying the location of the automatic proxy detection script.</returns>
		// Token: 0x17000A6D RID: 2669
		// (get) Token: 0x06002C7E RID: 11390 RVA: 0x0009ECF1 File Offset: 0x0009CEF1
		// (set) Token: 0x06002C7F RID: 11391 RVA: 0x0009ED03 File Offset: 0x0009CF03
		[ConfigurationProperty("scriptLocation")]
		public Uri ScriptLocation
		{
			get
			{
				return (Uri)base[ProxyElement.scriptLocationProp];
			}
			set
			{
				base[ProxyElement.scriptLocationProp] = value;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value that controls whether the Internet Explorer Web proxy settings are used.</summary>
		/// <returns>true if the Internet Explorer LAN settings are used to detect and configure the default <see cref="T:System.Net.WebProxy" /> used for requests; otherwise, false.</returns>
		// Token: 0x17000A6E RID: 2670
		// (get) Token: 0x06002C80 RID: 11392 RVA: 0x0009ED11 File Offset: 0x0009CF11
		// (set) Token: 0x06002C81 RID: 11393 RVA: 0x0009ED23 File Offset: 0x0009CF23
		[ConfigurationProperty("usesystemdefault", DefaultValue = "Unspecified")]
		public ProxyElement.UseSystemDefaultValues UseSystemDefault
		{
			get
			{
				return (ProxyElement.UseSystemDefaultValues)base[ProxyElement.useSystemDefaultProp];
			}
			set
			{
				base[ProxyElement.useSystemDefaultProp] = value;
			}
		}

		// Token: 0x17000A6F RID: 2671
		// (get) Token: 0x06002C82 RID: 11394 RVA: 0x0009ED36 File Offset: 0x0009CF36
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return ProxyElement.properties;
			}
		}

		// Token: 0x04001A62 RID: 6754
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04001A63 RID: 6755
		private static ConfigurationProperty autoDetectProp = new ConfigurationProperty("autoDetect", typeof(ProxyElement.AutoDetectValues), ProxyElement.AutoDetectValues.Unspecified);

		// Token: 0x04001A64 RID: 6756
		private static ConfigurationProperty bypassOnLocalProp = new ConfigurationProperty("bypassonlocal", typeof(ProxyElement.BypassOnLocalValues), ProxyElement.BypassOnLocalValues.Unspecified);

		// Token: 0x04001A65 RID: 6757
		private static ConfigurationProperty proxyAddressProp = new ConfigurationProperty("proxyaddress", typeof(Uri), null);

		// Token: 0x04001A66 RID: 6758
		private static ConfigurationProperty scriptLocationProp = new ConfigurationProperty("scriptLocation", typeof(Uri), null);

		// Token: 0x04001A67 RID: 6759
		private static ConfigurationProperty useSystemDefaultProp = new ConfigurationProperty("usesystemdefault", typeof(ProxyElement.UseSystemDefaultValues), ProxyElement.UseSystemDefaultValues.Unspecified);

		/// <summary>Specifies whether the proxy is bypassed for local resources.</summary>
		// Token: 0x02000580 RID: 1408
		public enum BypassOnLocalValues
		{
			/// <summary>Unspecified.</summary>
			// Token: 0x04001A69 RID: 6761
			Unspecified = -1,
			/// <summary>Access local resources directly.</summary>
			// Token: 0x04001A6A RID: 6762
			True = 1,
			/// <summary>All requests for local resources should go through the proxy</summary>
			// Token: 0x04001A6B RID: 6763
			False = 0
		}

		/// <summary>Specifies whether to use the local system proxy settings to determine whether the proxy is bypassed for local resources.</summary>
		// Token: 0x02000581 RID: 1409
		public enum UseSystemDefaultValues
		{
			/// <summary>The system default proxy setting is unspecified.</summary>
			// Token: 0x04001A6D RID: 6765
			Unspecified = -1,
			/// <summary>Use system default proxy setting values.</summary>
			// Token: 0x04001A6E RID: 6766
			True = 1,
			/// <summary>Do not use system default proxy setting values</summary>
			// Token: 0x04001A6F RID: 6767
			False = 0
		}

		/// <summary>Specifies whether the proxy is automatically detected.</summary>
		// Token: 0x02000582 RID: 1410
		public enum AutoDetectValues
		{
			/// <summary>Unspecified.</summary>
			// Token: 0x04001A71 RID: 6769
			Unspecified = -1,
			/// <summary>The proxy is automatically detected.</summary>
			// Token: 0x04001A72 RID: 6770
			True = 1,
			/// <summary>The proxy is not automatically detected.</summary>
			// Token: 0x04001A73 RID: 6771
			False = 0
		}
	}
}
