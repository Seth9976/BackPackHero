using System;

namespace System.Configuration
{
	/// <summary>Provides the configuration setting for International Domain Name (IDN) processing in the <see cref="T:System.Uri" /> class.</summary>
	// Token: 0x020001B5 RID: 437
	public sealed class IdnElement : ConfigurationElement
	{
		// Token: 0x06000B87 RID: 2951 RVA: 0x00031159 File Offset: 0x0002F359
		static IdnElement()
		{
			IdnElement.properties.Add(IdnElement.enabled_prop);
		}

		/// <summary>Gets or sets the value of the <see cref="T:System.Configuration.IdnElement" /> configuration setting. </summary>
		/// <returns>A <see cref="T:System.UriIdnScope" /> that contains the current configuration setting for IDN processing.</returns>
		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x06000B89 RID: 2953 RVA: 0x0003119C File Offset: 0x0002F39C
		// (set) Token: 0x06000B8A RID: 2954 RVA: 0x000311AE File Offset: 0x0002F3AE
		[ConfigurationProperty("enabled", DefaultValue = UriIdnScope.None, Options = ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey)]
		public UriIdnScope Enabled
		{
			get
			{
				return (UriIdnScope)base[IdnElement.enabled_prop];
			}
			set
			{
				base[IdnElement.enabled_prop] = value;
			}
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x06000B8B RID: 2955 RVA: 0x000311C1 File Offset: 0x0002F3C1
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return IdnElement.properties;
			}
		}

		// Token: 0x06000B8C RID: 2956 RVA: 0x000311C8 File Offset: 0x0002F3C8
		public override bool Equals(object o)
		{
			IdnElement idnElement = o as IdnElement;
			return idnElement != null && idnElement.Enabled == this.Enabled;
		}

		// Token: 0x06000B8D RID: 2957 RVA: 0x000311EF File Offset: 0x0002F3EF
		public override int GetHashCode()
		{
			return (int)(this.Enabled ^ (UriIdnScope)127);
		}

		// Token: 0x0400077F RID: 1919
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04000780 RID: 1920
		private static ConfigurationProperty enabled_prop = new ConfigurationProperty("enabled", typeof(UriIdnScope), UriIdnScope.None, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);

		// Token: 0x04000781 RID: 1921
		internal const UriIdnScope EnabledDefaultValue = UriIdnScope.None;
	}
}
