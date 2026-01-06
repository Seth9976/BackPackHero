using System;
using System.Configuration;

namespace System.Net.Configuration
{
	/// <summary>Represents the type information for a custom <see cref="T:System.Net.IWebProxy" /> module. This class cannot be inherited.</summary>
	// Token: 0x0200057A RID: 1402
	public sealed class ModuleElement : ConfigurationElement
	{
		// Token: 0x06002C5E RID: 11358 RVA: 0x0009E6BD File Offset: 0x0009C8BD
		static ModuleElement()
		{
			ModuleElement.properties.Add(ModuleElement.typeProp);
		}

		// Token: 0x17000A5F RID: 2655
		// (get) Token: 0x06002C60 RID: 11360 RVA: 0x0009E6F2 File Offset: 0x0009C8F2
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return ModuleElement.properties;
			}
		}

		/// <summary>Gets or sets the type and assembly information for the current instance.</summary>
		/// <returns>A string that identifies a type that implements the <see cref="T:System.Net.IWebProxy" /> interface or null if no value has been specified.</returns>
		// Token: 0x17000A60 RID: 2656
		// (get) Token: 0x06002C61 RID: 11361 RVA: 0x0009E6F9 File Offset: 0x0009C8F9
		// (set) Token: 0x06002C62 RID: 11362 RVA: 0x0009E70B File Offset: 0x0009C90B
		[ConfigurationProperty("type")]
		public string Type
		{
			get
			{
				return (string)base[ModuleElement.typeProp];
			}
			set
			{
				base[ModuleElement.typeProp] = value;
			}
		}

		// Token: 0x04001A5E RID: 6750
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04001A5F RID: 6751
		private static ConfigurationProperty typeProp = new ConfigurationProperty("type", typeof(string), null);
	}
}
