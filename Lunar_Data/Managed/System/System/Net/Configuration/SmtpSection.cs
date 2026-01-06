using System;
using System.Configuration;
using System.Net.Mail;

namespace System.Net.Configuration
{
	/// <summary>Represents the SMTP section in the System.Net configuration file.</summary>
	// Token: 0x02000587 RID: 1415
	public sealed class SmtpSection : ConfigurationSection
	{
		/// <summary>Gets or sets the Simple Mail Transport Protocol (SMTP) delivery method. The default delivery method is <see cref="F:System.Net.Mail.SmtpDeliveryMethod.Network" />.</summary>
		/// <returns>A string that represents the SMTP delivery method.</returns>
		// Token: 0x17000A92 RID: 2706
		// (get) Token: 0x06002CC3 RID: 11459 RVA: 0x0009F3C9 File Offset: 0x0009D5C9
		// (set) Token: 0x06002CC4 RID: 11460 RVA: 0x0009F3DB File Offset: 0x0009D5DB
		[ConfigurationProperty("deliveryMethod", DefaultValue = "Network")]
		public SmtpDeliveryMethod DeliveryMethod
		{
			get
			{
				return (SmtpDeliveryMethod)base["deliveryMethod"];
			}
			set
			{
				base["deliveryMethod"] = value;
			}
		}

		/// <summary>Gets or sets the delivery format to use for sending outgoing e-mail using the Simple Mail Transport Protocol (SMTP).</summary>
		/// <returns>Returns <see cref="T:System.Net.Mail.SmtpDeliveryFormat" />.The delivery format to use for sending outgoing e-mail using SMTP.</returns>
		// Token: 0x17000A93 RID: 2707
		// (get) Token: 0x06002CC5 RID: 11461 RVA: 0x0009F3EE File Offset: 0x0009D5EE
		// (set) Token: 0x06002CC6 RID: 11462 RVA: 0x0009F400 File Offset: 0x0009D600
		[ConfigurationProperty("deliveryFormat", DefaultValue = SmtpDeliveryFormat.SevenBit)]
		public SmtpDeliveryFormat DeliveryFormat
		{
			get
			{
				return (SmtpDeliveryFormat)base["deliveryFormat"];
			}
			set
			{
				base["deliveryFormat"] = value;
			}
		}

		/// <summary>Gets or sets the default value that indicates who the email message is from.</summary>
		/// <returns>A string that represents the default value indicating who a mail message is from.</returns>
		// Token: 0x17000A94 RID: 2708
		// (get) Token: 0x06002CC7 RID: 11463 RVA: 0x0009F413 File Offset: 0x0009D613
		// (set) Token: 0x06002CC8 RID: 11464 RVA: 0x0009F425 File Offset: 0x0009D625
		[ConfigurationProperty("from")]
		public string From
		{
			get
			{
				return (string)base["from"];
			}
			set
			{
				base["from"] = value;
			}
		}

		/// <summary>Gets the configuration element that controls the network settings used by the Simple Mail Transport Protocol (SMTP). file.<see cref="T:System.Net.Configuration.SmtpNetworkElement" />.</summary>
		/// <returns>A <see cref="T:System.Net.Configuration.SmtpNetworkElement" /> object.The configuration element that controls the network settings used by SMTP.</returns>
		// Token: 0x17000A95 RID: 2709
		// (get) Token: 0x06002CC9 RID: 11465 RVA: 0x0009F433 File Offset: 0x0009D633
		[ConfigurationProperty("network")]
		public SmtpNetworkElement Network
		{
			get
			{
				return (SmtpNetworkElement)base["network"];
			}
		}

		/// <summary>Gets the pickup directory that will be used by the SMPT client.</summary>
		/// <returns>A <see cref="T:System.Net.Configuration.SmtpSpecifiedPickupDirectoryElement" /> object that specifies the pickup directory folder.</returns>
		// Token: 0x17000A96 RID: 2710
		// (get) Token: 0x06002CCA RID: 11466 RVA: 0x0009F445 File Offset: 0x0009D645
		[ConfigurationProperty("specifiedPickupDirectory")]
		public SmtpSpecifiedPickupDirectoryElement SpecifiedPickupDirectory
		{
			get
			{
				return (SmtpSpecifiedPickupDirectoryElement)base["specifiedPickupDirectory"];
			}
		}

		// Token: 0x17000A97 RID: 2711
		// (get) Token: 0x06002CCB RID: 11467 RVA: 0x000316E3 File Offset: 0x0002F8E3
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return base.Properties;
			}
		}
	}
}
