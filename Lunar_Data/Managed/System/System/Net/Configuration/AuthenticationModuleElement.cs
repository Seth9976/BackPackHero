using System;
using System.Configuration;

namespace System.Net.Configuration
{
	/// <summary>Represents the type information for an authentication module. This class cannot be inherited.</summary>
	// Token: 0x02000568 RID: 1384
	public sealed class AuthenticationModuleElement : ConfigurationElement
	{
		// Token: 0x06002BD5 RID: 11221 RVA: 0x0009D6B2 File Offset: 0x0009B8B2
		static AuthenticationModuleElement()
		{
			AuthenticationModuleElement.properties.Add(AuthenticationModuleElement.typeProp);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Configuration.AuthenticationModuleElement" /> class. </summary>
		// Token: 0x06002BD6 RID: 11222 RVA: 0x00031194 File Offset: 0x0002F394
		public AuthenticationModuleElement()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Configuration.AuthenticationModuleElement" /> class with the specified type information.</summary>
		/// <param name="typeName">A string that identifies the type and the assembly that contains it.</param>
		// Token: 0x06002BD7 RID: 11223 RVA: 0x0009D6E8 File Offset: 0x0009B8E8
		public AuthenticationModuleElement(string typeName)
		{
			this.Type = typeName;
		}

		// Token: 0x17000A37 RID: 2615
		// (get) Token: 0x06002BD8 RID: 11224 RVA: 0x0009D6F7 File Offset: 0x0009B8F7
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return AuthenticationModuleElement.properties;
			}
		}

		/// <summary>Gets or sets the type and assembly information for the current instance.</summary>
		/// <returns>A string that identifies a type that implements an authentication module or null if no value has been specified.</returns>
		// Token: 0x17000A38 RID: 2616
		// (get) Token: 0x06002BD9 RID: 11225 RVA: 0x0009D6FE File Offset: 0x0009B8FE
		// (set) Token: 0x06002BDA RID: 11226 RVA: 0x0009D710 File Offset: 0x0009B910
		[ConfigurationProperty("type", Options = ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey)]
		public string Type
		{
			get
			{
				return (string)base[AuthenticationModuleElement.typeProp];
			}
			set
			{
				base[AuthenticationModuleElement.typeProp] = value;
			}
		}

		// Token: 0x04001A3D RID: 6717
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04001A3E RID: 6718
		private static ConfigurationProperty typeProp = new ConfigurationProperty("type", typeof(string), null, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);
	}
}
