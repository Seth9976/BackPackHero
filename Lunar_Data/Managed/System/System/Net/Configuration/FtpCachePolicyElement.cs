using System;
using System.Configuration;
using System.Net.Cache;
using System.Xml;

namespace System.Net.Configuration
{
	/// <summary>Represents the default FTP cache policy for network resources. This class cannot be inherited.</summary>
	// Token: 0x02000575 RID: 1397
	public sealed class FtpCachePolicyElement : ConfigurationElement
	{
		// Token: 0x06002C37 RID: 11319 RVA: 0x0009E2F5 File Offset: 0x0009C4F5
		static FtpCachePolicyElement()
		{
			FtpCachePolicyElement.properties.Add(FtpCachePolicyElement.policyLevelProp);
		}

		/// <summary>Gets or sets FTP caching behavior for the local machine.</summary>
		/// <returns>A <see cref="T:System.Net.Cache.RequestCacheLevel" /> value that specifies the cache behavior.</returns>
		// Token: 0x17000A50 RID: 2640
		// (get) Token: 0x06002C39 RID: 11321 RVA: 0x0009E32F File Offset: 0x0009C52F
		// (set) Token: 0x06002C3A RID: 11322 RVA: 0x0009E341 File Offset: 0x0009C541
		[ConfigurationProperty("policyLevel", DefaultValue = "Default")]
		public RequestCacheLevel PolicyLevel
		{
			get
			{
				return (RequestCacheLevel)base[FtpCachePolicyElement.policyLevelProp];
			}
			set
			{
				base[FtpCachePolicyElement.policyLevelProp] = value;
			}
		}

		// Token: 0x17000A51 RID: 2641
		// (get) Token: 0x06002C3B RID: 11323 RVA: 0x0009E354 File Offset: 0x0009C554
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return FtpCachePolicyElement.properties;
			}
		}

		// Token: 0x06002C3C RID: 11324 RVA: 0x0000822E File Offset: 0x0000642E
		[MonoTODO]
		protected override void DeserializeElement(XmlReader reader, bool serializeCollectionKey)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06002C3D RID: 11325 RVA: 0x0000822E File Offset: 0x0000642E
		[MonoTODO]
		protected override void Reset(ConfigurationElement parentElement)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04001A50 RID: 6736
		private static ConfigurationProperty policyLevelProp = new ConfigurationProperty("policyLevel", typeof(RequestCacheLevel), RequestCacheLevel.Default);

		// Token: 0x04001A51 RID: 6737
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
