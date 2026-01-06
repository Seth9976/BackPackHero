using System;
using System.Configuration;

namespace System.Net.Configuration
{
	/// <summary>Determines whether Internet Protocol version 6 is enabled on the local computer. This class cannot be inherited.</summary>
	// Token: 0x02000578 RID: 1400
	public sealed class Ipv6Element : ConfigurationElement
	{
		// Token: 0x06002C57 RID: 11351 RVA: 0x0009E640 File Offset: 0x0009C840
		static Ipv6Element()
		{
			Ipv6Element.properties.Add(Ipv6Element.enabledProp);
		}

		/// <summary>Gets or sets a Boolean value that indicates whether Internet Protocol version 6 is enabled on the local computer.</summary>
		/// <returns>true if IPv6 is enabled; otherwise, false.</returns>
		// Token: 0x17000A5C RID: 2652
		// (get) Token: 0x06002C59 RID: 11353 RVA: 0x0009E67A File Offset: 0x0009C87A
		// (set) Token: 0x06002C5A RID: 11354 RVA: 0x0009E68C File Offset: 0x0009C88C
		[ConfigurationProperty("enabled", DefaultValue = "False")]
		public bool Enabled
		{
			get
			{
				return (bool)base[Ipv6Element.enabledProp];
			}
			set
			{
				base[Ipv6Element.enabledProp] = value;
			}
		}

		// Token: 0x17000A5D RID: 2653
		// (get) Token: 0x06002C5B RID: 11355 RVA: 0x0009E69F File Offset: 0x0009C89F
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return Ipv6Element.properties;
			}
		}

		// Token: 0x04001A5C RID: 6748
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04001A5D RID: 6749
		private static ConfigurationProperty enabledProp = new ConfigurationProperty("enabled", typeof(bool), false);
	}
}
