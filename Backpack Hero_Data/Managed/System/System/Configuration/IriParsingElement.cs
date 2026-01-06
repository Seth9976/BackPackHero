using System;

namespace System.Configuration
{
	/// <summary>Provides the configuration setting for International Resource Identifier (IRI) processing in the <see cref="T:System.Uri" /> class.</summary>
	// Token: 0x020001B7 RID: 439
	public sealed class IriParsingElement : ConfigurationElement
	{
		// Token: 0x06000B90 RID: 2960 RVA: 0x000311FA File Offset: 0x0002F3FA
		static IriParsingElement()
		{
			IriParsingElement.properties.Add(IriParsingElement.enabled_prop);
		}

		/// <summary>Gets or sets the value of the <see cref="T:System.Configuration.IriParsingElement" /> configuration setting.</summary>
		/// <returns>A Boolean that indicates if International Resource Identifier (IRI) processing is enabled. </returns>
		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x06000B92 RID: 2962 RVA: 0x00031235 File Offset: 0x0002F435
		// (set) Token: 0x06000B93 RID: 2963 RVA: 0x00031247 File Offset: 0x0002F447
		[ConfigurationProperty("enabled", DefaultValue = false, Options = ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey)]
		public bool Enabled
		{
			get
			{
				return (bool)base[IriParsingElement.enabled_prop];
			}
			set
			{
				base[IriParsingElement.enabled_prop] = value;
			}
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x06000B94 RID: 2964 RVA: 0x0003125A File Offset: 0x0002F45A
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return IriParsingElement.properties;
			}
		}

		// Token: 0x06000B95 RID: 2965 RVA: 0x00031264 File Offset: 0x0002F464
		public override bool Equals(object o)
		{
			IriParsingElement iriParsingElement = o as IriParsingElement;
			return iriParsingElement != null && iriParsingElement.Enabled == this.Enabled;
		}

		// Token: 0x06000B96 RID: 2966 RVA: 0x0003128B File Offset: 0x0002F48B
		public override int GetHashCode()
		{
			return Convert.ToInt32(this.Enabled) ^ 127;
		}

		// Token: 0x04000782 RID: 1922
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04000783 RID: 1923
		private static ConfigurationProperty enabled_prop = new ConfigurationProperty("enabled", typeof(bool), false, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);
	}
}
