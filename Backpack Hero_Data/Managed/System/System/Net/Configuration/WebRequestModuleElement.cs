using System;
using System.ComponentModel;
using System.Configuration;

namespace System.Net.Configuration
{
	/// <summary>Represents a URI prefix and the associated class that handles creating Web requests for the prefix. This class cannot be inherited.</summary>
	// Token: 0x0200058B RID: 1419
	public sealed class WebRequestModuleElement : ConfigurationElement
	{
		// Token: 0x06002CE3 RID: 11491 RVA: 0x0009F634 File Offset: 0x0009D834
		static WebRequestModuleElement()
		{
			WebRequestModuleElement.properties.Add(WebRequestModuleElement.prefixProp);
			WebRequestModuleElement.properties.Add(WebRequestModuleElement.typeProp);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Configuration.WebRequestModuleElement" /> class. </summary>
		// Token: 0x06002CE4 RID: 11492 RVA: 0x00031194 File Offset: 0x0002F394
		public WebRequestModuleElement()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Configuration.WebRequestModuleElement" /> class using the specified URI prefix and type information. </summary>
		/// <param name="prefix">A string containing a URI prefix.</param>
		/// <param name="type">A string containing the type and assembly information for the class that handles creating requests for resources that use the <paramref name="prefix" /> URI prefix. For more information, see the Remarks section.</param>
		// Token: 0x06002CE5 RID: 11493 RVA: 0x0009F69D File Offset: 0x0009D89D
		public WebRequestModuleElement(string prefix, string type)
		{
			base[WebRequestModuleElement.typeProp] = type;
			this.Prefix = prefix;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Configuration.WebRequestModuleElement" /> class using the specified URI prefix and type identifier.</summary>
		/// <param name="prefix">A string containing a URI prefix.</param>
		/// <param name="type">A <see cref="T:System.Type" /> that identifies the class that handles creating requests for resources that use the <paramref name="prefix" /> URI prefix. </param>
		// Token: 0x06002CE6 RID: 11494 RVA: 0x0009F6B8 File Offset: 0x0009D8B8
		public WebRequestModuleElement(string prefix, Type type)
			: this(prefix, type.FullName)
		{
		}

		/// <summary>Gets or sets the URI prefix for the current Web request module.</summary>
		/// <returns>A string that contains a URI prefix.</returns>
		// Token: 0x17000AA1 RID: 2721
		// (get) Token: 0x06002CE7 RID: 11495 RVA: 0x0009F6C7 File Offset: 0x0009D8C7
		// (set) Token: 0x06002CE8 RID: 11496 RVA: 0x0009F6D9 File Offset: 0x0009D8D9
		[ConfigurationProperty("prefix", Options = ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey)]
		public string Prefix
		{
			get
			{
				return (string)base[WebRequestModuleElement.prefixProp];
			}
			set
			{
				base[WebRequestModuleElement.prefixProp] = value;
			}
		}

		/// <summary>Gets or sets a class that creates Web requests.</summary>
		/// <returns>A <see cref="T:System.Type" /> instance that identifies a Web request module.</returns>
		// Token: 0x17000AA2 RID: 2722
		// (get) Token: 0x06002CE9 RID: 11497 RVA: 0x0009F6E7 File Offset: 0x0009D8E7
		// (set) Token: 0x06002CEA RID: 11498 RVA: 0x0009F6FE File Offset: 0x0009D8FE
		[ConfigurationProperty("type")]
		[TypeConverter(typeof(TypeConverter))]
		public Type Type
		{
			get
			{
				return Type.GetType((string)base[WebRequestModuleElement.typeProp]);
			}
			set
			{
				base[WebRequestModuleElement.typeProp] = value.FullName;
			}
		}

		// Token: 0x17000AA3 RID: 2723
		// (get) Token: 0x06002CEB RID: 11499 RVA: 0x0009F711 File Offset: 0x0009D911
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return WebRequestModuleElement.properties;
			}
		}

		// Token: 0x04001A90 RID: 6800
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04001A91 RID: 6801
		private static ConfigurationProperty prefixProp = new ConfigurationProperty("prefix", typeof(string), null, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);

		// Token: 0x04001A92 RID: 6802
		private static ConfigurationProperty typeProp = new ConfigurationProperty("type", typeof(string));
	}
}
