using System;
using System.Configuration;

namespace System.Security.Authentication.ExtendedProtection.Configuration
{
	/// <summary>The <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElement" /> class represents a configuration element for a service name used in a <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElementCollection" />.</summary>
	// Token: 0x020002AC RID: 684
	public sealed class ServiceNameElement : ConfigurationElement
	{
		// Token: 0x06001549 RID: 5449 RVA: 0x00055BCB File Offset: 0x00053DCB
		static ServiceNameElement()
		{
			ServiceNameElement.properties.Add(ServiceNameElement.name);
		}

		/// <summary>Gets or sets the Service Provider Name (SPN) for this <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElement" /> instance.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the representation of SPN for this <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElement" /> instance.</returns>
		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x0600154A RID: 5450 RVA: 0x00055BFF File Offset: 0x00053DFF
		// (set) Token: 0x0600154B RID: 5451 RVA: 0x00055C11 File Offset: 0x00053E11
		[ConfigurationProperty("name")]
		public string Name
		{
			get
			{
				return (string)base[ServiceNameElement.name];
			}
			set
			{
				base[ServiceNameElement.name] = value;
			}
		}

		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x0600154C RID: 5452 RVA: 0x00055C1F File Offset: 0x00053E1F
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return ServiceNameElement.properties;
			}
		}

		// Token: 0x04000C03 RID: 3075
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04000C04 RID: 3076
		private static ConfigurationProperty name = ConfigUtil.BuildProperty(typeof(ServiceNameElement), "Name");
	}
}
