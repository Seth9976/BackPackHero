using System;

namespace System.Net
{
	/// <summary>Provides a container class for Internet host address information.</summary>
	// Token: 0x020003E9 RID: 1001
	public class IPHostEntry
	{
		/// <summary>Gets or sets the DNS name of the host.</summary>
		/// <returns>A string that contains the primary host name for the server.</returns>
		// Token: 0x1700066A RID: 1642
		// (get) Token: 0x0600209E RID: 8350 RVA: 0x00077862 File Offset: 0x00075A62
		// (set) Token: 0x0600209F RID: 8351 RVA: 0x0007786A File Offset: 0x00075A6A
		public string HostName
		{
			get
			{
				return this.hostName;
			}
			set
			{
				this.hostName = value;
			}
		}

		/// <summary>Gets or sets a list of aliases that are associated with a host.</summary>
		/// <returns>An array of strings that contain DNS names that resolve to the IP addresses in the <see cref="P:System.Net.IPHostEntry.AddressList" /> property.</returns>
		// Token: 0x1700066B RID: 1643
		// (get) Token: 0x060020A0 RID: 8352 RVA: 0x00077873 File Offset: 0x00075A73
		// (set) Token: 0x060020A1 RID: 8353 RVA: 0x0007787B File Offset: 0x00075A7B
		public string[] Aliases
		{
			get
			{
				return this.aliases;
			}
			set
			{
				this.aliases = value;
			}
		}

		/// <summary>Gets or sets a list of IP addresses that are associated with a host.</summary>
		/// <returns>An array of type <see cref="T:System.Net.IPAddress" /> that contains IP addresses that resolve to the host names that are contained in the <see cref="P:System.Net.IPHostEntry.Aliases" /> property.</returns>
		// Token: 0x1700066C RID: 1644
		// (get) Token: 0x060020A2 RID: 8354 RVA: 0x00077884 File Offset: 0x00075A84
		// (set) Token: 0x060020A3 RID: 8355 RVA: 0x0007788C File Offset: 0x00075A8C
		public IPAddress[] AddressList
		{
			get
			{
				return this.addressList;
			}
			set
			{
				this.addressList = value;
			}
		}

		// Token: 0x040011C8 RID: 4552
		private string hostName;

		// Token: 0x040011C9 RID: 4553
		private string[] aliases;

		// Token: 0x040011CA RID: 4554
		private IPAddress[] addressList;

		// Token: 0x040011CB RID: 4555
		internal bool isTrustedHost = true;
	}
}
