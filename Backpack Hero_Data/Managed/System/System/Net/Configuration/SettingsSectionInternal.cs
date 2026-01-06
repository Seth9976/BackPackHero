using System;
using System.Configuration;
using System.Net.Security;
using System.Net.Sockets;

namespace System.Net.Configuration
{
	// Token: 0x02000567 RID: 1383
	internal sealed class SettingsSectionInternal
	{
		// Token: 0x17000A2E RID: 2606
		// (get) Token: 0x06002BC3 RID: 11203 RVA: 0x0009D5C8 File Offset: 0x0009B7C8
		internal static SettingsSectionInternal Section
		{
			get
			{
				return SettingsSectionInternal.instance;
			}
		}

		// Token: 0x17000A2F RID: 2607
		// (get) Token: 0x06002BC4 RID: 11204 RVA: 0x0009D5CF File Offset: 0x0009B7CF
		// (set) Token: 0x06002BC5 RID: 11205 RVA: 0x0009D5D7 File Offset: 0x0009B7D7
		internal bool UseNagleAlgorithm { get; set; }

		// Token: 0x17000A30 RID: 2608
		// (get) Token: 0x06002BC6 RID: 11206 RVA: 0x0009D5E0 File Offset: 0x0009B7E0
		// (set) Token: 0x06002BC7 RID: 11207 RVA: 0x0009D5E8 File Offset: 0x0009B7E8
		internal bool Expect100Continue { get; set; }

		// Token: 0x17000A31 RID: 2609
		// (get) Token: 0x06002BC8 RID: 11208 RVA: 0x0009D5F1 File Offset: 0x0009B7F1
		// (set) Token: 0x06002BC9 RID: 11209 RVA: 0x0009D5F9 File Offset: 0x0009B7F9
		internal bool CheckCertificateName { get; private set; }

		// Token: 0x17000A32 RID: 2610
		// (get) Token: 0x06002BCA RID: 11210 RVA: 0x0009D602 File Offset: 0x0009B802
		// (set) Token: 0x06002BCB RID: 11211 RVA: 0x0009D60A File Offset: 0x0009B80A
		internal int DnsRefreshTimeout { get; set; }

		// Token: 0x17000A33 RID: 2611
		// (get) Token: 0x06002BCC RID: 11212 RVA: 0x0009D613 File Offset: 0x0009B813
		// (set) Token: 0x06002BCD RID: 11213 RVA: 0x0009D61B File Offset: 0x0009B81B
		internal bool EnableDnsRoundRobin { get; set; }

		// Token: 0x17000A34 RID: 2612
		// (get) Token: 0x06002BCE RID: 11214 RVA: 0x0009D624 File Offset: 0x0009B824
		// (set) Token: 0x06002BCF RID: 11215 RVA: 0x0009D62C File Offset: 0x0009B82C
		internal bool CheckCertificateRevocationList { get; set; }

		// Token: 0x17000A35 RID: 2613
		// (get) Token: 0x06002BD0 RID: 11216 RVA: 0x0009D635 File Offset: 0x0009B835
		// (set) Token: 0x06002BD1 RID: 11217 RVA: 0x0009D63D File Offset: 0x0009B83D
		internal EncryptionPolicy EncryptionPolicy { get; private set; }

		// Token: 0x17000A36 RID: 2614
		// (get) Token: 0x06002BD2 RID: 11218 RVA: 0x0009D648 File Offset: 0x0009B848
		internal bool Ipv6Enabled
		{
			get
			{
				try
				{
					SettingsSection settingsSection = (SettingsSection)ConfigurationManager.GetSection("system.net/settings");
					if (settingsSection != null)
					{
						return settingsSection.Ipv6.Enabled;
					}
				}
				catch
				{
				}
				return true;
			}
		}

		// Token: 0x04001A31 RID: 6705
		private static readonly SettingsSectionInternal instance = new SettingsSectionInternal();

		// Token: 0x04001A32 RID: 6706
		internal UnicodeEncodingConformance WebUtilityUnicodeEncodingConformance;

		// Token: 0x04001A33 RID: 6707
		internal UnicodeDecodingConformance WebUtilityUnicodeDecodingConformance;

		// Token: 0x04001A34 RID: 6708
		internal readonly bool HttpListenerUnescapeRequestUrl = true;

		// Token: 0x04001A35 RID: 6709
		internal readonly IPProtectionLevel IPProtectionLevel = IPProtectionLevel.Unspecified;
	}
}
