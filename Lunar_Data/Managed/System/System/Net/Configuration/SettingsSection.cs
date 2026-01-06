using System;
using System.Configuration;
using Unity;

namespace System.Net.Configuration
{
	/// <summary>Represents the configuration section for sockets, IPv6, response headers, and service points. This class cannot be inherited.</summary>
	// Token: 0x02000585 RID: 1413
	public sealed class SettingsSection : ConfigurationSection
	{
		// Token: 0x06002CA4 RID: 11428 RVA: 0x0009F160 File Offset: 0x0009D360
		static SettingsSection()
		{
			SettingsSection.properties.Add(SettingsSection.httpWebRequestProp);
			SettingsSection.properties.Add(SettingsSection.ipv6Prop);
			SettingsSection.properties.Add(SettingsSection.performanceCountersProp);
			SettingsSection.properties.Add(SettingsSection.servicePointManagerProp);
			SettingsSection.properties.Add(SettingsSection.socketProp);
			SettingsSection.properties.Add(SettingsSection.webProxyScriptProp);
		}

		/// <summary>Gets the configuration element that controls the settings used by an <see cref="T:System.Net.HttpWebRequest" /> object.</summary>
		/// <returns>A <see cref="T:System.Net.Configuration.HttpWebRequestElement" /> object.The configuration element that controls the maximum response header length and other settings used by an <see cref="T:System.Net.HttpWebRequest" /> object.</returns>
		// Token: 0x17000A7F RID: 2687
		// (get) Token: 0x06002CA6 RID: 11430 RVA: 0x0009F267 File Offset: 0x0009D467
		[ConfigurationProperty("httpWebRequest")]
		public HttpWebRequestElement HttpWebRequest
		{
			get
			{
				return (HttpWebRequestElement)base[SettingsSection.httpWebRequestProp];
			}
		}

		/// <summary>Gets the configuration element that enables Internet Protocol version 6 (IPv6).</summary>
		/// <returns>A <see cref="T:System.Net.Configuration.Ipv6Element" />.The configuration element that controls setting used by IPv6.</returns>
		// Token: 0x17000A80 RID: 2688
		// (get) Token: 0x06002CA7 RID: 11431 RVA: 0x0009F279 File Offset: 0x0009D479
		[ConfigurationProperty("ipv6")]
		public Ipv6Element Ipv6
		{
			get
			{
				return (Ipv6Element)base[SettingsSection.ipv6Prop];
			}
		}

		/// <summary>Gets the configuration element that controls whether network performance counters are enabled.</summary>
		/// <returns>A <see cref="T:System.Net.Configuration.PerformanceCountersElement" />.The configuration element that controls setting used network performance counters.</returns>
		// Token: 0x17000A81 RID: 2689
		// (get) Token: 0x06002CA8 RID: 11432 RVA: 0x0009F28B File Offset: 0x0009D48B
		[ConfigurationProperty("performanceCounters")]
		public PerformanceCountersElement PerformanceCounters
		{
			get
			{
				return (PerformanceCountersElement)base[SettingsSection.performanceCountersProp];
			}
		}

		/// <summary>Gets the configuration element that controls settings for connections to remote host computers.</summary>
		/// <returns>A <see cref="T:System.Net.Configuration.ServicePointManagerElement" /> object.The configuration element that that controls setting used network performance counters for connections to remote host computers.</returns>
		// Token: 0x17000A82 RID: 2690
		// (get) Token: 0x06002CA9 RID: 11433 RVA: 0x0009F29D File Offset: 0x0009D49D
		[ConfigurationProperty("servicePointManager")]
		public ServicePointManagerElement ServicePointManager
		{
			get
			{
				return (ServicePointManagerElement)base[SettingsSection.servicePointManagerProp];
			}
		}

		/// <summary>Gets the configuration element that controls settings for sockets.</summary>
		/// <returns>A <see cref="T:System.Net.Configuration.SocketElement" /> object.The configuration element that controls settings for sockets.</returns>
		// Token: 0x17000A83 RID: 2691
		// (get) Token: 0x06002CAA RID: 11434 RVA: 0x0009F2AF File Offset: 0x0009D4AF
		[ConfigurationProperty("socket")]
		public SocketElement Socket
		{
			get
			{
				return (SocketElement)base[SettingsSection.socketProp];
			}
		}

		/// <summary>Gets the configuration element that controls the execution timeout and download timeout of Web proxy scripts.</summary>
		/// <returns>A <see cref="T:System.Net.Configuration.WebProxyScriptElement" /> object.The configuration element that controls settings for the execution timeout and download timeout used by the Web proxy scripts.</returns>
		// Token: 0x17000A84 RID: 2692
		// (get) Token: 0x06002CAB RID: 11435 RVA: 0x0009F2C1 File Offset: 0x0009D4C1
		[ConfigurationProperty("webProxyScript")]
		public WebProxyScriptElement WebProxyScript
		{
			get
			{
				return (WebProxyScriptElement)base[SettingsSection.webProxyScriptProp];
			}
		}

		// Token: 0x17000A85 RID: 2693
		// (get) Token: 0x06002CAC RID: 11436 RVA: 0x0009F2D3 File Offset: 0x0009D4D3
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return SettingsSection.properties;
			}
		}

		/// <summary>Gets the configuration element that controls the settings used by an <see cref="T:System.Net.HttpListener" /> object.</summary>
		/// <returns>An <see cref="T:System.Net.Configuration.HttpListenerElement" /> object.The configuration element that controls the settings used by an <see cref="T:System.Net.HttpListener" /> object.</returns>
		// Token: 0x17000A86 RID: 2694
		// (get) Token: 0x06002CAD RID: 11437 RVA: 0x000327E0 File Offset: 0x000309E0
		public HttpListenerElement HttpListener
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the configuration element that controls the settings used by an <see cref="T:System.Net.WebUtility" /> object.</summary>
		/// <returns>Returns <see cref="T:System.Net.Configuration.WebUtilityElement" />.The configuration element that controls the settings used by an <see cref="T:System.Net.WebUtility" /> object.</returns>
		// Token: 0x17000A87 RID: 2695
		// (get) Token: 0x06002CAE RID: 11438 RVA: 0x000327E0 File Offset: 0x000309E0
		public WebUtilityElement WebUtility
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		// Token: 0x17000A88 RID: 2696
		// (get) Token: 0x06002CAF RID: 11439 RVA: 0x000327E0 File Offset: 0x000309E0
		public WindowsAuthenticationElement WindowsAuthentication
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		// Token: 0x04001A82 RID: 6786
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04001A83 RID: 6787
		private static ConfigurationProperty httpWebRequestProp = new ConfigurationProperty("httpWebRequest", typeof(HttpWebRequestElement));

		// Token: 0x04001A84 RID: 6788
		private static ConfigurationProperty ipv6Prop = new ConfigurationProperty("ipv6", typeof(Ipv6Element));

		// Token: 0x04001A85 RID: 6789
		private static ConfigurationProperty performanceCountersProp = new ConfigurationProperty("performanceCounters", typeof(PerformanceCountersElement));

		// Token: 0x04001A86 RID: 6790
		private static ConfigurationProperty servicePointManagerProp = new ConfigurationProperty("servicePointManager", typeof(ServicePointManagerElement));

		// Token: 0x04001A87 RID: 6791
		private static ConfigurationProperty webProxyScriptProp = new ConfigurationProperty("webProxyScript", typeof(WebProxyScriptElement));

		// Token: 0x04001A88 RID: 6792
		private static ConfigurationProperty socketProp = new ConfigurationProperty("socket", typeof(SocketElement));
	}
}
