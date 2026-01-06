using System;
using System.Configuration;

namespace System.Net.Configuration
{
	/// <summary>Represents an SMTP pickup directory configuration element.</summary>
	// Token: 0x02000588 RID: 1416
	public sealed class SmtpSpecifiedPickupDirectoryElement : ConfigurationElement
	{
		// Token: 0x06002CCD RID: 11469 RVA: 0x0009F457 File Offset: 0x0009D657
		static SmtpSpecifiedPickupDirectoryElement()
		{
			SmtpSpecifiedPickupDirectoryElement.properties.Add(SmtpSpecifiedPickupDirectoryElement.pickupDirectoryLocationProp);
		}

		/// <summary>Gets or sets the folder where applications save mail messages to be processed by the SMTP server.</summary>
		/// <returns>A string that specifies the pickup directory for e-mail messages.</returns>
		// Token: 0x17000A98 RID: 2712
		// (get) Token: 0x06002CCE RID: 11470 RVA: 0x0009F48B File Offset: 0x0009D68B
		// (set) Token: 0x06002CCF RID: 11471 RVA: 0x0009F49D File Offset: 0x0009D69D
		[ConfigurationProperty("pickupDirectoryLocation")]
		public string PickupDirectoryLocation
		{
			get
			{
				return (string)base[SmtpSpecifiedPickupDirectoryElement.pickupDirectoryLocationProp];
			}
			set
			{
				base[SmtpSpecifiedPickupDirectoryElement.pickupDirectoryLocationProp] = value;
			}
		}

		// Token: 0x17000A99 RID: 2713
		// (get) Token: 0x06002CD0 RID: 11472 RVA: 0x0009F4AB File Offset: 0x0009D6AB
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return SmtpSpecifiedPickupDirectoryElement.properties;
			}
		}

		// Token: 0x04001A89 RID: 6793
		private static ConfigurationProperty pickupDirectoryLocationProp = new ConfigurationProperty("pickupDirectoryLocation", typeof(string));

		// Token: 0x04001A8A RID: 6794
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
