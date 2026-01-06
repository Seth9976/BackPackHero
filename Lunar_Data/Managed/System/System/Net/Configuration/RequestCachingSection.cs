using System;
using System.Configuration;
using System.Net.Cache;
using System.Xml;

namespace System.Net.Configuration
{
	/// <summary>Represents the configuration section for cache behavior. This class cannot be inherited.</summary>
	// Token: 0x02000583 RID: 1411
	public sealed class RequestCachingSection : ConfigurationSection
	{
		// Token: 0x06002C83 RID: 11395 RVA: 0x0009ED40 File Offset: 0x0009CF40
		static RequestCachingSection()
		{
			RequestCachingSection.properties.Add(RequestCachingSection.defaultFtpCachePolicyProp);
			RequestCachingSection.properties.Add(RequestCachingSection.defaultHttpCachePolicyProp);
			RequestCachingSection.properties.Add(RequestCachingSection.defaultPolicyLevelProp);
			RequestCachingSection.properties.Add(RequestCachingSection.disableAllCachingProp);
			RequestCachingSection.properties.Add(RequestCachingSection.isPrivateCacheProp);
			RequestCachingSection.properties.Add(RequestCachingSection.unspecifiedMaximumAgeProp);
		}

		/// <summary>Gets the default FTP caching behavior for the local computer.</summary>
		/// <returns>A <see cref="T:System.Net.Configuration.FtpCachePolicyElement" /> that defines the default cache policy.</returns>
		// Token: 0x17000A70 RID: 2672
		// (get) Token: 0x06002C85 RID: 11397 RVA: 0x0009EE67 File Offset: 0x0009D067
		[ConfigurationProperty("defaultFtpCachePolicy")]
		public FtpCachePolicyElement DefaultFtpCachePolicy
		{
			get
			{
				return (FtpCachePolicyElement)base[RequestCachingSection.defaultFtpCachePolicyProp];
			}
		}

		/// <summary>Gets the default caching behavior for the local computer.</summary>
		/// <returns>A <see cref="T:System.Net.Configuration.HttpCachePolicyElement" /> that defines the default cache policy.</returns>
		// Token: 0x17000A71 RID: 2673
		// (get) Token: 0x06002C86 RID: 11398 RVA: 0x0009EE79 File Offset: 0x0009D079
		[ConfigurationProperty("defaultHttpCachePolicy")]
		public HttpCachePolicyElement DefaultHttpCachePolicy
		{
			get
			{
				return (HttpCachePolicyElement)base[RequestCachingSection.defaultHttpCachePolicyProp];
			}
		}

		/// <summary>Gets or sets the default cache policy level.</summary>
		/// <returns>A <see cref="T:System.Net.Cache.RequestCacheLevel" /> enumeration value.</returns>
		// Token: 0x17000A72 RID: 2674
		// (get) Token: 0x06002C87 RID: 11399 RVA: 0x0009EE8B File Offset: 0x0009D08B
		// (set) Token: 0x06002C88 RID: 11400 RVA: 0x0009EE9D File Offset: 0x0009D09D
		[ConfigurationProperty("defaultPolicyLevel", DefaultValue = "BypassCache")]
		public RequestCacheLevel DefaultPolicyLevel
		{
			get
			{
				return (RequestCacheLevel)base[RequestCachingSection.defaultPolicyLevelProp];
			}
			set
			{
				base[RequestCachingSection.defaultPolicyLevelProp] = value;
			}
		}

		/// <summary>Gets or sets a Boolean value that enables caching on the local computer.</summary>
		/// <returns>true if caching is disabled on the local computer; otherwise, false.</returns>
		// Token: 0x17000A73 RID: 2675
		// (get) Token: 0x06002C89 RID: 11401 RVA: 0x0009EEB0 File Offset: 0x0009D0B0
		// (set) Token: 0x06002C8A RID: 11402 RVA: 0x0009EEC2 File Offset: 0x0009D0C2
		[ConfigurationProperty("disableAllCaching", DefaultValue = "False")]
		public bool DisableAllCaching
		{
			get
			{
				return (bool)base[RequestCachingSection.disableAllCachingProp];
			}
			set
			{
				base[RequestCachingSection.disableAllCachingProp] = value;
			}
		}

		/// <summary>Gets or sets a Boolean value that indicates whether the local computer cache is private.</summary>
		/// <returns>true if the cache provides user isolation; otherwise, false.</returns>
		// Token: 0x17000A74 RID: 2676
		// (get) Token: 0x06002C8B RID: 11403 RVA: 0x0009EED5 File Offset: 0x0009D0D5
		// (set) Token: 0x06002C8C RID: 11404 RVA: 0x0009EEE7 File Offset: 0x0009D0E7
		[ConfigurationProperty("isPrivateCache", DefaultValue = "True")]
		public bool IsPrivateCache
		{
			get
			{
				return (bool)base[RequestCachingSection.isPrivateCacheProp];
			}
			set
			{
				base[RequestCachingSection.isPrivateCacheProp] = value;
			}
		}

		/// <summary>Gets or sets a value used as the maximum age for cached resources that do not have expiration information.</summary>
		/// <returns>A <see cref="T:System.TimeSpan" /> that provides a default maximum age for cached resources.</returns>
		// Token: 0x17000A75 RID: 2677
		// (get) Token: 0x06002C8D RID: 11405 RVA: 0x0009EEFA File Offset: 0x0009D0FA
		// (set) Token: 0x06002C8E RID: 11406 RVA: 0x0009EF0C File Offset: 0x0009D10C
		[ConfigurationProperty("unspecifiedMaximumAge", DefaultValue = "1.00:00:00")]
		public TimeSpan UnspecifiedMaximumAge
		{
			get
			{
				return (TimeSpan)base[RequestCachingSection.unspecifiedMaximumAgeProp];
			}
			set
			{
				base[RequestCachingSection.unspecifiedMaximumAgeProp] = value;
			}
		}

		// Token: 0x17000A76 RID: 2678
		// (get) Token: 0x06002C8F RID: 11407 RVA: 0x0009EF1F File Offset: 0x0009D11F
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return RequestCachingSection.properties;
			}
		}

		// Token: 0x06002C90 RID: 11408 RVA: 0x00066CD6 File Offset: 0x00064ED6
		[MonoTODO]
		protected override void PostDeserialize()
		{
			base.PostDeserialize();
		}

		// Token: 0x06002C91 RID: 11409 RVA: 0x0009EF26 File Offset: 0x0009D126
		[MonoTODO]
		protected override void DeserializeElement(XmlReader reader, bool serializeCollectionKey)
		{
			base.DeserializeElement(reader, serializeCollectionKey);
		}

		// Token: 0x04001A74 RID: 6772
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04001A75 RID: 6773
		private static ConfigurationProperty defaultFtpCachePolicyProp = new ConfigurationProperty("defaultFtpCachePolicy", typeof(FtpCachePolicyElement));

		// Token: 0x04001A76 RID: 6774
		private static ConfigurationProperty defaultHttpCachePolicyProp = new ConfigurationProperty("defaultHttpCachePolicy", typeof(HttpCachePolicyElement));

		// Token: 0x04001A77 RID: 6775
		private static ConfigurationProperty defaultPolicyLevelProp = new ConfigurationProperty("defaultPolicyLevel", typeof(RequestCacheLevel), RequestCacheLevel.BypassCache);

		// Token: 0x04001A78 RID: 6776
		private static ConfigurationProperty disableAllCachingProp = new ConfigurationProperty("disableAllCaching", typeof(bool), false);

		// Token: 0x04001A79 RID: 6777
		private static ConfigurationProperty isPrivateCacheProp = new ConfigurationProperty("isPrivateCache", typeof(bool), true);

		// Token: 0x04001A7A RID: 6778
		private static ConfigurationProperty unspecifiedMaximumAgeProp = new ConfigurationProperty("unspecifiedMaximumAge", typeof(TimeSpan), new TimeSpan(1, 0, 0, 0));
	}
}
