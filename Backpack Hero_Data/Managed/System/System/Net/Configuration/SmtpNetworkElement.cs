using System;
using System.Configuration;
using Unity;

namespace System.Net.Configuration
{
	/// <summary>Represents the network element in the SMTP configuration file. This class cannot be inherited.</summary>
	// Token: 0x02000586 RID: 1414
	public sealed class SmtpNetworkElement : ConfigurationElement
	{
		/// <summary>Determines whether or not default user credentials are used to access an SMTP server. The default value is false.</summary>
		/// <returns>true indicates that default user credentials will be used to access the SMTP server; otherwise, false.</returns>
		// Token: 0x17000A89 RID: 2697
		// (get) Token: 0x06002CB0 RID: 11440 RVA: 0x0009F2DA File Offset: 0x0009D4DA
		// (set) Token: 0x06002CB1 RID: 11441 RVA: 0x0009F2EC File Offset: 0x0009D4EC
		[ConfigurationProperty("defaultCredentials", DefaultValue = "False")]
		public bool DefaultCredentials
		{
			get
			{
				return (bool)base["defaultCredentials"];
			}
			set
			{
				base["defaultCredentials"] = value;
			}
		}

		/// <summary>Gets or sets the name of the SMTP server.</summary>
		/// <returns>A string that represents the name of the SMTP server to connect to.</returns>
		// Token: 0x17000A8A RID: 2698
		// (get) Token: 0x06002CB2 RID: 11442 RVA: 0x0009F2FF File Offset: 0x0009D4FF
		// (set) Token: 0x06002CB3 RID: 11443 RVA: 0x0009F311 File Offset: 0x0009D511
		[ConfigurationProperty("host")]
		public string Host
		{
			get
			{
				return (string)base["host"];
			}
			set
			{
				base["host"] = value;
			}
		}

		/// <summary>Gets or sets the user password to use to connect to an SMTP mail server.</summary>
		/// <returns>A string that represents the password to use to connect to an SMTP mail server.</returns>
		// Token: 0x17000A8B RID: 2699
		// (get) Token: 0x06002CB4 RID: 11444 RVA: 0x0009F31F File Offset: 0x0009D51F
		// (set) Token: 0x06002CB5 RID: 11445 RVA: 0x0009F331 File Offset: 0x0009D531
		[ConfigurationProperty("password")]
		public string Password
		{
			get
			{
				return (string)base["password"];
			}
			set
			{
				base["password"] = value;
			}
		}

		/// <summary>Gets or sets the port that SMTP clients use to connect to an SMTP mail server. The default value is 25.</summary>
		/// <returns>A string that represents the port to connect to an SMTP mail server.</returns>
		// Token: 0x17000A8C RID: 2700
		// (get) Token: 0x06002CB6 RID: 11446 RVA: 0x0009F33F File Offset: 0x0009D53F
		// (set) Token: 0x06002CB7 RID: 11447 RVA: 0x0009F351 File Offset: 0x0009D551
		[ConfigurationProperty("port", DefaultValue = "25")]
		public int Port
		{
			get
			{
				return (int)base["port"];
			}
			set
			{
				base["port"] = value;
			}
		}

		/// <summary>Gets or sets the user name to connect to an SMTP mail server.</summary>
		/// <returns>A string that represents the user name to connect to an SMTP mail server.</returns>
		// Token: 0x17000A8D RID: 2701
		// (get) Token: 0x06002CB8 RID: 11448 RVA: 0x0009F364 File Offset: 0x0009D564
		// (set) Token: 0x06002CB9 RID: 11449 RVA: 0x0009F376 File Offset: 0x0009D576
		[ConfigurationProperty("userName", DefaultValue = null)]
		public string UserName
		{
			get
			{
				return (string)base["userName"];
			}
			set
			{
				base["userName"] = value;
			}
		}

		/// <summary>Gets or sets the Service Provider Name (SPN) to use for authentication when using extended protection to connect to an SMTP mail server.</summary>
		/// <returns>A string that represents the SPN to use for authentication when using extended protection to connect to an SMTP mail server.</returns>
		// Token: 0x17000A8E RID: 2702
		// (get) Token: 0x06002CBA RID: 11450 RVA: 0x0009F384 File Offset: 0x0009D584
		// (set) Token: 0x06002CBB RID: 11451 RVA: 0x0009F396 File Offset: 0x0009D596
		[ConfigurationProperty("targetName", DefaultValue = null)]
		public string TargetName
		{
			get
			{
				return (string)base["targetName"];
			}
			set
			{
				base["targetName"] = value;
			}
		}

		/// <summary>Gets or sets whether SSL is used to access an SMTP mail server. The default value is false.</summary>
		/// <returns>true indicates that SSL will be used to access the SMTP mail server; otherwise, false.</returns>
		// Token: 0x17000A8F RID: 2703
		// (get) Token: 0x06002CBC RID: 11452 RVA: 0x0009F3A4 File Offset: 0x0009D5A4
		// (set) Token: 0x06002CBD RID: 11453 RVA: 0x0009F3B6 File Offset: 0x0009D5B6
		[ConfigurationProperty("enableSsl", DefaultValue = false)]
		public bool EnableSsl
		{
			get
			{
				return (bool)base["enableSsl"];
			}
			set
			{
				base["enableSsl"] = value;
			}
		}

		// Token: 0x17000A90 RID: 2704
		// (get) Token: 0x06002CBE RID: 11454 RVA: 0x000316E3 File Offset: 0x0002F8E3
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return base.Properties;
			}
		}

		// Token: 0x06002CBF RID: 11455 RVA: 0x00003917 File Offset: 0x00001B17
		protected override void PostDeserialize()
		{
		}

		/// <summary>Gets or sets the client domain name used in the initial SMTP protocol request to connect to an SMTP mail server.</summary>
		/// <returns>A string that represents the client domain name used in the initial SMTP protocol request to connect to an SMTP mail server.</returns>
		// Token: 0x17000A91 RID: 2705
		// (get) Token: 0x06002CC1 RID: 11457 RVA: 0x000327E0 File Offset: 0x000309E0
		// (set) Token: 0x06002CC2 RID: 11458 RVA: 0x00013B26 File Offset: 0x00011D26
		public string ClientDomain
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}
	}
}
