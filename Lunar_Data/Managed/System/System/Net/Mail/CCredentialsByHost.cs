using System;

namespace System.Net.Mail
{
	// Token: 0x02000643 RID: 1603
	internal class CCredentialsByHost : ICredentialsByHost
	{
		// Token: 0x0600337A RID: 13178 RVA: 0x000BB0C5 File Offset: 0x000B92C5
		public CCredentialsByHost(string userName, string password)
		{
			this.userName = userName;
			this.password = password;
		}

		// Token: 0x0600337B RID: 13179 RVA: 0x000BB0DB File Offset: 0x000B92DB
		public NetworkCredential GetCredential(string host, int port, string authenticationType)
		{
			return new NetworkCredential(this.userName, this.password);
		}

		// Token: 0x04001F5C RID: 8028
		private string userName;

		// Token: 0x04001F5D RID: 8029
		private string password;
	}
}
