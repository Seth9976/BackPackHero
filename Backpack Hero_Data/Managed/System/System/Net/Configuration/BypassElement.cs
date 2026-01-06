using System;
using System.Configuration;

namespace System.Net.Configuration
{
	/// <summary>Represents the address information for resources that are not retrieved using a proxy server. This class cannot be inherited.</summary>
	// Token: 0x0200056B RID: 1387
	public sealed class BypassElement : ConfigurationElement
	{
		// Token: 0x06002BEE RID: 11246 RVA: 0x0009D7C7 File Offset: 0x0009B9C7
		static BypassElement()
		{
			BypassElement.properties.Add(BypassElement.addressProp);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Configuration.BypassElement" /> class. </summary>
		// Token: 0x06002BEF RID: 11247 RVA: 0x00031194 File Offset: 0x0002F394
		public BypassElement()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Configuration.BypassElement" /> class with the specified type information.</summary>
		/// <param name="address">A string that identifies the address of a resource.</param>
		// Token: 0x06002BF0 RID: 11248 RVA: 0x0009D7FD File Offset: 0x0009B9FD
		public BypassElement(string address)
		{
			this.Address = address;
		}

		/// <summary>Gets or sets the addresses of resources that bypass the proxy server.</summary>
		/// <returns>A string that identifies a resource.</returns>
		// Token: 0x17000A3D RID: 2621
		// (get) Token: 0x06002BF1 RID: 11249 RVA: 0x0009D80C File Offset: 0x0009BA0C
		// (set) Token: 0x06002BF2 RID: 11250 RVA: 0x0009D81E File Offset: 0x0009BA1E
		[ConfigurationProperty("address", Options = ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey)]
		public string Address
		{
			get
			{
				return (string)base[BypassElement.addressProp];
			}
			set
			{
				base[BypassElement.addressProp] = value;
			}
		}

		// Token: 0x17000A3E RID: 2622
		// (get) Token: 0x06002BF3 RID: 11251 RVA: 0x0009D82C File Offset: 0x0009BA2C
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return BypassElement.properties;
			}
		}

		// Token: 0x04001A41 RID: 6721
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04001A42 RID: 6722
		private static ConfigurationProperty addressProp = new ConfigurationProperty("address", typeof(string), null, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);
	}
}
