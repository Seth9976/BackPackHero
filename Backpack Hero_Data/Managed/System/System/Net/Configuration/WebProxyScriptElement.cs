using System;
using System.Configuration;
using Unity;

namespace System.Net.Configuration
{
	/// <summary>Represents information used to configure Web proxy scripts. This class cannot be inherited.</summary>
	// Token: 0x0200058A RID: 1418
	public sealed class WebProxyScriptElement : ConfigurationElement
	{
		// Token: 0x06002CDB RID: 11483 RVA: 0x0009F59C File Offset: 0x0009D79C
		static WebProxyScriptElement()
		{
			WebProxyScriptElement.properties.Add(WebProxyScriptElement.downloadTimeoutProp);
		}

		// Token: 0x06002CDC RID: 11484 RVA: 0x00003917 File Offset: 0x00001B17
		protected override void PostDeserialize()
		{
		}

		/// <summary>Gets or sets the Web proxy script download timeout using the format hours:minutes:seconds.</summary>
		/// <returns>A <see cref="T:System.TimeSpan" /> object that contains the timeout value. The default download timeout is one minute.</returns>
		// Token: 0x17000A9E RID: 2718
		// (get) Token: 0x06002CDD RID: 11485 RVA: 0x0009F5E9 File Offset: 0x0009D7E9
		// (set) Token: 0x06002CDE RID: 11486 RVA: 0x0009F5FB File Offset: 0x0009D7FB
		[ConfigurationProperty("downloadTimeout", DefaultValue = "00:02:00")]
		public TimeSpan DownloadTimeout
		{
			get
			{
				return (TimeSpan)base[WebProxyScriptElement.downloadTimeoutProp];
			}
			set
			{
				base[WebProxyScriptElement.downloadTimeoutProp] = value;
			}
		}

		// Token: 0x17000A9F RID: 2719
		// (get) Token: 0x06002CDF RID: 11487 RVA: 0x0009F60E File Offset: 0x0009D80E
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return WebProxyScriptElement.properties;
			}
		}

		// Token: 0x17000AA0 RID: 2720
		// (get) Token: 0x06002CE1 RID: 11489 RVA: 0x0009F618 File Offset: 0x0009D818
		// (set) Token: 0x06002CE2 RID: 11490 RVA: 0x00013B26 File Offset: 0x00011D26
		public int AutoConfigUrlRetryInterval
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		// Token: 0x04001A8E RID: 6798
		private static ConfigurationProperty downloadTimeoutProp = new ConfigurationProperty("downloadTimeout", typeof(TimeSpan), new TimeSpan(0, 0, 2, 0));

		// Token: 0x04001A8F RID: 6799
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
