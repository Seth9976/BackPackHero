using System;
using System.Configuration;
using System.Net.Cache;
using System.Xml;

namespace System.Net.Configuration
{
	/// <summary>Represents the default HTTP cache policy for network resources. This class cannot be inherited.</summary>
	// Token: 0x02000576 RID: 1398
	public sealed class HttpCachePolicyElement : ConfigurationElement
	{
		// Token: 0x06002C3E RID: 11326 RVA: 0x0009E35C File Offset: 0x0009C55C
		static HttpCachePolicyElement()
		{
			HttpCachePolicyElement.properties.Add(HttpCachePolicyElement.maximumAgeProp);
			HttpCachePolicyElement.properties.Add(HttpCachePolicyElement.maximumStaleProp);
			HttpCachePolicyElement.properties.Add(HttpCachePolicyElement.minimumFreshProp);
			HttpCachePolicyElement.properties.Add(HttpCachePolicyElement.policyLevelProp);
		}

		/// <summary>Gets or sets the maximum age permitted for a resource returned from the cache.</summary>
		/// <returns>A <see cref="T:System.TimeSpan" /> value that specifies the maximum age for cached resources specified in the configuration file.</returns>
		// Token: 0x17000A52 RID: 2642
		// (get) Token: 0x06002C40 RID: 11328 RVA: 0x0009E438 File Offset: 0x0009C638
		// (set) Token: 0x06002C41 RID: 11329 RVA: 0x0009E44A File Offset: 0x0009C64A
		[ConfigurationProperty("maximumAge", DefaultValue = "10675199.02:48:05.4775807")]
		public TimeSpan MaximumAge
		{
			get
			{
				return (TimeSpan)base[HttpCachePolicyElement.maximumAgeProp];
			}
			set
			{
				base[HttpCachePolicyElement.maximumAgeProp] = value;
			}
		}

		/// <summary>Gets or sets the maximum staleness value permitted for a resource returned from the cache.</summary>
		/// <returns>A <see cref="T:System.TimeSpan" /> value that is set to the maximum staleness value specified in the configuration file.</returns>
		// Token: 0x17000A53 RID: 2643
		// (get) Token: 0x06002C42 RID: 11330 RVA: 0x0009E45D File Offset: 0x0009C65D
		// (set) Token: 0x06002C43 RID: 11331 RVA: 0x0009E46F File Offset: 0x0009C66F
		[ConfigurationProperty("maximumStale", DefaultValue = "-10675199.02:48:05.4775808")]
		public TimeSpan MaximumStale
		{
			get
			{
				return (TimeSpan)base[HttpCachePolicyElement.maximumStaleProp];
			}
			set
			{
				base[HttpCachePolicyElement.maximumStaleProp] = value;
			}
		}

		/// <summary>Gets or sets the minimum freshness permitted for a resource returned from the cache.</summary>
		/// <returns>A <see cref="T:System.TimeSpan" /> value that specifies the minimum freshness specified in the configuration file.</returns>
		// Token: 0x17000A54 RID: 2644
		// (get) Token: 0x06002C44 RID: 11332 RVA: 0x0009E482 File Offset: 0x0009C682
		// (set) Token: 0x06002C45 RID: 11333 RVA: 0x0009E494 File Offset: 0x0009C694
		[ConfigurationProperty("minimumFresh", DefaultValue = "-10675199.02:48:05.4775808")]
		public TimeSpan MinimumFresh
		{
			get
			{
				return (TimeSpan)base[HttpCachePolicyElement.minimumFreshProp];
			}
			set
			{
				base[HttpCachePolicyElement.minimumFreshProp] = value;
			}
		}

		/// <summary>Gets or sets HTTP caching behavior for the local machine.</summary>
		/// <returns>A <see cref="T:System.Net.Cache.HttpRequestCacheLevel" /> value that specifies the cache behavior.</returns>
		// Token: 0x17000A55 RID: 2645
		// (get) Token: 0x06002C46 RID: 11334 RVA: 0x0009E4A7 File Offset: 0x0009C6A7
		// (set) Token: 0x06002C47 RID: 11335 RVA: 0x0009E4B9 File Offset: 0x0009C6B9
		[ConfigurationProperty("policyLevel", DefaultValue = "Default", Options = ConfigurationPropertyOptions.IsRequired)]
		public HttpRequestCacheLevel PolicyLevel
		{
			get
			{
				return (HttpRequestCacheLevel)base[HttpCachePolicyElement.policyLevelProp];
			}
			set
			{
				base[HttpCachePolicyElement.policyLevelProp] = value;
			}
		}

		// Token: 0x17000A56 RID: 2646
		// (get) Token: 0x06002C48 RID: 11336 RVA: 0x0009E4CC File Offset: 0x0009C6CC
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return HttpCachePolicyElement.properties;
			}
		}

		// Token: 0x06002C49 RID: 11337 RVA: 0x0000822E File Offset: 0x0000642E
		[MonoTODO]
		protected override void DeserializeElement(XmlReader reader, bool serializeCollectionKey)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06002C4A RID: 11338 RVA: 0x0000822E File Offset: 0x0000642E
		[MonoTODO]
		protected override void Reset(ConfigurationElement parentElement)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04001A52 RID: 6738
		private static ConfigurationProperty maximumAgeProp = new ConfigurationProperty("maximumAge", typeof(TimeSpan), TimeSpan.MaxValue);

		// Token: 0x04001A53 RID: 6739
		private static ConfigurationProperty maximumStaleProp = new ConfigurationProperty("maximumStale", typeof(TimeSpan), TimeSpan.MinValue);

		// Token: 0x04001A54 RID: 6740
		private static ConfigurationProperty minimumFreshProp = new ConfigurationProperty("minimumFresh", typeof(TimeSpan), TimeSpan.MinValue);

		// Token: 0x04001A55 RID: 6741
		private static ConfigurationProperty policyLevelProp = new ConfigurationProperty("policyLevel", typeof(HttpRequestCacheLevel), HttpRequestCacheLevel.Default, ConfigurationPropertyOptions.IsRequired);

		// Token: 0x04001A56 RID: 6742
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
