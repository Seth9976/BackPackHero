using System;
using System.Configuration;
using System.Net.Sockets;
using Unity;

namespace System.Net.Configuration
{
	/// <summary>Represents information used to configure <see cref="T:System.Net.Sockets.Socket" /> objects. This class cannot be inherited.</summary>
	// Token: 0x02000589 RID: 1417
	public sealed class SocketElement : ConfigurationElement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Configuration.SocketElement" /> class. </summary>
		// Token: 0x06002CD2 RID: 11474 RVA: 0x0009F4B4 File Offset: 0x0009D6B4
		public SocketElement()
		{
			SocketElement.alwaysUseCompletionPortsForAcceptProp = new ConfigurationProperty("alwaysUseCompletionPortsForAccept", typeof(bool), false);
			SocketElement.alwaysUseCompletionPortsForConnectProp = new ConfigurationProperty("alwaysUseCompletionPortsForConnect", typeof(bool), false);
			SocketElement.properties = new ConfigurationPropertyCollection();
			SocketElement.properties.Add(SocketElement.alwaysUseCompletionPortsForAcceptProp);
			SocketElement.properties.Add(SocketElement.alwaysUseCompletionPortsForConnectProp);
		}

		/// <summary>Gets or sets a Boolean value that specifies whether completion ports are used when accepting connections.</summary>
		/// <returns>true to use completion ports; otherwise, false.</returns>
		// Token: 0x17000A9A RID: 2714
		// (get) Token: 0x06002CD3 RID: 11475 RVA: 0x0009F52D File Offset: 0x0009D72D
		// (set) Token: 0x06002CD4 RID: 11476 RVA: 0x0009F53F File Offset: 0x0009D73F
		[ConfigurationProperty("alwaysUseCompletionPortsForAccept", DefaultValue = "False")]
		public bool AlwaysUseCompletionPortsForAccept
		{
			get
			{
				return (bool)base[SocketElement.alwaysUseCompletionPortsForAcceptProp];
			}
			set
			{
				base[SocketElement.alwaysUseCompletionPortsForAcceptProp] = value;
			}
		}

		/// <summary>Gets or sets a Boolean value that specifies whether completion ports are used when making connections.</summary>
		/// <returns>true to use completion ports; otherwise, false.</returns>
		// Token: 0x17000A9B RID: 2715
		// (get) Token: 0x06002CD5 RID: 11477 RVA: 0x0009F552 File Offset: 0x0009D752
		// (set) Token: 0x06002CD6 RID: 11478 RVA: 0x0009F564 File Offset: 0x0009D764
		[ConfigurationProperty("alwaysUseCompletionPortsForConnect", DefaultValue = "False")]
		public bool AlwaysUseCompletionPortsForConnect
		{
			get
			{
				return (bool)base[SocketElement.alwaysUseCompletionPortsForConnectProp];
			}
			set
			{
				base[SocketElement.alwaysUseCompletionPortsForConnectProp] = value;
			}
		}

		// Token: 0x17000A9C RID: 2716
		// (get) Token: 0x06002CD7 RID: 11479 RVA: 0x0009F577 File Offset: 0x0009D777
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return SocketElement.properties;
			}
		}

		// Token: 0x06002CD8 RID: 11480 RVA: 0x00003917 File Offset: 0x00001B17
		[MonoTODO]
		protected override void PostDeserialize()
		{
		}

		/// <summary>Gets or sets a value that specifies the default <see cref="T:System.Net.Sockets.IPProtectionLevel" /> to use for a socket.</summary>
		/// <returns>The value of the <see cref="T:System.Net.Sockets.IPProtectionLevel" /> for the current instance.</returns>
		// Token: 0x17000A9D RID: 2717
		// (get) Token: 0x06002CD9 RID: 11481 RVA: 0x0009F580 File Offset: 0x0009D780
		// (set) Token: 0x06002CDA RID: 11482 RVA: 0x00013B26 File Offset: 0x00011D26
		public IPProtectionLevel IPProtectionLevel
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return (IPProtectionLevel)0;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		// Token: 0x04001A8B RID: 6795
		private static ConfigurationPropertyCollection properties;

		// Token: 0x04001A8C RID: 6796
		private static ConfigurationProperty alwaysUseCompletionPortsForAcceptProp;

		// Token: 0x04001A8D RID: 6797
		private static ConfigurationProperty alwaysUseCompletionPortsForConnectProp;
	}
}
