using System;
using Unity;

namespace System.Configuration
{
	/// <summary>Represents the Uri section within a configuration file.</summary>
	// Token: 0x020001DC RID: 476
	public sealed class UriSection : ConfigurationSection
	{
		// Token: 0x06000C5B RID: 3163 RVA: 0x0003274C File Offset: 0x0003094C
		static UriSection()
		{
			UriSection.properties.Add(UriSection.idn_prop);
			UriSection.properties.Add(UriSection.iriParsing_prop);
		}

		/// <summary>Gets an <see cref="T:System.Configuration.IdnElement" /> object that contains the configuration setting for International Domain Name (IDN) processing in the <see cref="T:System.Uri" /> class.</summary>
		/// <returns>The configuration setting for International Domain Name (IDN) processing in the <see cref="T:System.Uri" /> class.</returns>
		// Token: 0x17000222 RID: 546
		// (get) Token: 0x06000C5D RID: 3165 RVA: 0x000327B5 File Offset: 0x000309B5
		[ConfigurationProperty("idn")]
		public IdnElement Idn
		{
			get
			{
				return (IdnElement)base[UriSection.idn_prop];
			}
		}

		/// <summary>Gets an <see cref="T:System.Configuration.IriParsingElement" /> object that contains the configuration setting for International Resource Identifiers (IRI) parsing in the <see cref="T:System.Uri" /> class.</summary>
		/// <returns>The configuration setting for International Resource Identifiers (IRI) parsing in the <see cref="T:System.Uri" /> class.</returns>
		// Token: 0x17000223 RID: 547
		// (get) Token: 0x06000C5E RID: 3166 RVA: 0x000327C7 File Offset: 0x000309C7
		[ConfigurationProperty("iriParsing")]
		public IriParsingElement IriParsing
		{
			get
			{
				return (IriParsingElement)base[UriSection.iriParsing_prop];
			}
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x06000C5F RID: 3167 RVA: 0x000327D9 File Offset: 0x000309D9
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return UriSection.properties;
			}
		}

		/// <summary>Gets a <see cref="T:System.Configuration.SchemeSettingElementCollection" /> object that contains the configuration settings for scheme parsing in the <see cref="T:System.Uri" /> class.</summary>
		/// <returns>The configuration settings for scheme parsing in the <see cref="T:System.Uri" /> class</returns>
		// Token: 0x17000225 RID: 549
		// (get) Token: 0x06000C60 RID: 3168 RVA: 0x000327E0 File Offset: 0x000309E0
		public SchemeSettingElementCollection SchemeSettings
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		// Token: 0x040007BC RID: 1980
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x040007BD RID: 1981
		private static ConfigurationProperty idn_prop = new ConfigurationProperty("idn", typeof(IdnElement), null);

		// Token: 0x040007BE RID: 1982
		private static ConfigurationProperty iriParsing_prop = new ConfigurationProperty("iriParsing", typeof(IriParsingElement), null);
	}
}
