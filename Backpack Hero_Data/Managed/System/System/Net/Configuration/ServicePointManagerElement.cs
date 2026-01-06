using System;
using System.Configuration;
using System.Net.Security;
using Unity;

namespace System.Net.Configuration
{
	/// <summary>Represents the default settings used to create connections to a remote computer. This class cannot be inherited.</summary>
	// Token: 0x02000584 RID: 1412
	public sealed class ServicePointManagerElement : ConfigurationElement
	{
		// Token: 0x06002C92 RID: 11410 RVA: 0x0009EF30 File Offset: 0x0009D130
		static ServicePointManagerElement()
		{
			ServicePointManagerElement.properties.Add(ServicePointManagerElement.checkCertificateNameProp);
			ServicePointManagerElement.properties.Add(ServicePointManagerElement.checkCertificateRevocationListProp);
			ServicePointManagerElement.properties.Add(ServicePointManagerElement.dnsRefreshTimeoutProp);
			ServicePointManagerElement.properties.Add(ServicePointManagerElement.enableDnsRoundRobinProp);
			ServicePointManagerElement.properties.Add(ServicePointManagerElement.expect100ContinueProp);
			ServicePointManagerElement.properties.Add(ServicePointManagerElement.useNagleAlgorithmProp);
		}

		/// <summary>Gets or sets a Boolean value that controls checking host name information in an X509 certificate.</summary>
		/// <returns>true to specify host name checking; otherwise, false. </returns>
		// Token: 0x17000A77 RID: 2679
		// (get) Token: 0x06002C94 RID: 11412 RVA: 0x0009F05F File Offset: 0x0009D25F
		// (set) Token: 0x06002C95 RID: 11413 RVA: 0x0009F071 File Offset: 0x0009D271
		[ConfigurationProperty("checkCertificateName", DefaultValue = "True")]
		public bool CheckCertificateName
		{
			get
			{
				return (bool)base[ServicePointManagerElement.checkCertificateNameProp];
			}
			set
			{
				base[ServicePointManagerElement.checkCertificateNameProp] = value;
			}
		}

		/// <summary>Gets or sets a Boolean value that indicates whether the certificate is checked against the certificate authority revocation list.</summary>
		/// <returns>true if the certificate revocation list is checked; otherwise, false.The default value is false.</returns>
		// Token: 0x17000A78 RID: 2680
		// (get) Token: 0x06002C96 RID: 11414 RVA: 0x0009F084 File Offset: 0x0009D284
		// (set) Token: 0x06002C97 RID: 11415 RVA: 0x0009F096 File Offset: 0x0009D296
		[ConfigurationProperty("checkCertificateRevocationList", DefaultValue = "False")]
		public bool CheckCertificateRevocationList
		{
			get
			{
				return (bool)base[ServicePointManagerElement.checkCertificateRevocationListProp];
			}
			set
			{
				base[ServicePointManagerElement.checkCertificateRevocationListProp] = value;
			}
		}

		/// <summary>Gets or sets the amount of time after which address information is refreshed.</summary>
		/// <returns>A <see cref="T:System.TimeSpan" /> that specifies when addresses are resolved using DNS.</returns>
		// Token: 0x17000A79 RID: 2681
		// (get) Token: 0x06002C98 RID: 11416 RVA: 0x0009F0A9 File Offset: 0x0009D2A9
		// (set) Token: 0x06002C99 RID: 11417 RVA: 0x0009F0BB File Offset: 0x0009D2BB
		[ConfigurationProperty("dnsRefreshTimeout", DefaultValue = "120000")]
		public int DnsRefreshTimeout
		{
			get
			{
				return (int)base[ServicePointManagerElement.dnsRefreshTimeoutProp];
			}
			set
			{
				base[ServicePointManagerElement.dnsRefreshTimeoutProp] = value;
			}
		}

		/// <summary>Gets or sets a Boolean value that controls using different IP addresses on connections to the same server.</summary>
		/// <returns>true to enable DNS round-robin behavior; otherwise, false.</returns>
		// Token: 0x17000A7A RID: 2682
		// (get) Token: 0x06002C9A RID: 11418 RVA: 0x0009F0CE File Offset: 0x0009D2CE
		// (set) Token: 0x06002C9B RID: 11419 RVA: 0x0009F0E0 File Offset: 0x0009D2E0
		[ConfigurationProperty("enableDnsRoundRobin", DefaultValue = "False")]
		public bool EnableDnsRoundRobin
		{
			get
			{
				return (bool)base[ServicePointManagerElement.enableDnsRoundRobinProp];
			}
			set
			{
				base[ServicePointManagerElement.enableDnsRoundRobinProp] = value;
			}
		}

		/// <summary>Gets or sets a Boolean value that determines whether 100-Continue behavior is used.</summary>
		/// <returns>true to expect 100-Continue responses for POST requests; otherwise, false. The default value is true.</returns>
		// Token: 0x17000A7B RID: 2683
		// (get) Token: 0x06002C9C RID: 11420 RVA: 0x0009F0F3 File Offset: 0x0009D2F3
		// (set) Token: 0x06002C9D RID: 11421 RVA: 0x0009F105 File Offset: 0x0009D305
		[ConfigurationProperty("expect100Continue", DefaultValue = "True")]
		public bool Expect100Continue
		{
			get
			{
				return (bool)base[ServicePointManagerElement.expect100ContinueProp];
			}
			set
			{
				base[ServicePointManagerElement.expect100ContinueProp] = value;
			}
		}

		/// <summary>Gets or sets a Boolean value that determines whether the Nagle algorithm is used.</summary>
		/// <returns>true to use the Nagle algorithm; otherwise, false. The default value is true.</returns>
		// Token: 0x17000A7C RID: 2684
		// (get) Token: 0x06002C9E RID: 11422 RVA: 0x0009F118 File Offset: 0x0009D318
		// (set) Token: 0x06002C9F RID: 11423 RVA: 0x0009F12A File Offset: 0x0009D32A
		[ConfigurationProperty("useNagleAlgorithm", DefaultValue = "True")]
		public bool UseNagleAlgorithm
		{
			get
			{
				return (bool)base[ServicePointManagerElement.useNagleAlgorithmProp];
			}
			set
			{
				base[ServicePointManagerElement.useNagleAlgorithmProp] = value;
			}
		}

		// Token: 0x17000A7D RID: 2685
		// (get) Token: 0x06002CA0 RID: 11424 RVA: 0x0009F13D File Offset: 0x0009D33D
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return ServicePointManagerElement.properties;
			}
		}

		// Token: 0x06002CA1 RID: 11425 RVA: 0x00003917 File Offset: 0x00001B17
		[MonoTODO]
		protected override void PostDeserialize()
		{
		}

		/// <summary>Gets or sets the <see cref="T:System.Net.Security.EncryptionPolicy" /> to use.</summary>
		/// <returns>The encryption policy to use for a <see cref="T:System.Net.ServicePointManager" /> instance.</returns>
		// Token: 0x17000A7E RID: 2686
		// (get) Token: 0x06002CA2 RID: 11426 RVA: 0x0009F144 File Offset: 0x0009D344
		// (set) Token: 0x06002CA3 RID: 11427 RVA: 0x00013B26 File Offset: 0x00011D26
		public EncryptionPolicy EncryptionPolicy
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return EncryptionPolicy.RequireEncryption;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		// Token: 0x04001A7B RID: 6779
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04001A7C RID: 6780
		private static ConfigurationProperty checkCertificateNameProp = new ConfigurationProperty("checkCertificateName", typeof(bool), true);

		// Token: 0x04001A7D RID: 6781
		private static ConfigurationProperty checkCertificateRevocationListProp = new ConfigurationProperty("checkCertificateRevocationList", typeof(bool), false);

		// Token: 0x04001A7E RID: 6782
		private static ConfigurationProperty dnsRefreshTimeoutProp = new ConfigurationProperty("dnsRefreshTimeout", typeof(int), 120000);

		// Token: 0x04001A7F RID: 6783
		private static ConfigurationProperty enableDnsRoundRobinProp = new ConfigurationProperty("enableDnsRoundRobin", typeof(bool), false);

		// Token: 0x04001A80 RID: 6784
		private static ConfigurationProperty expect100ContinueProp = new ConfigurationProperty("expect100Continue", typeof(bool), true);

		// Token: 0x04001A81 RID: 6785
		private static ConfigurationProperty useNagleAlgorithmProp = new ConfigurationProperty("useNagleAlgorithm", typeof(bool), true);
	}
}
