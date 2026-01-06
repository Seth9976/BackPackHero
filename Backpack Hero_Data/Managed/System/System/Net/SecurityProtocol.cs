using System;
using System.Security.Authentication;

namespace System.Net
{
	// Token: 0x0200037B RID: 891
	internal static class SecurityProtocol
	{
		// Token: 0x04000F0D RID: 3853
		public const SslProtocols DefaultSecurityProtocols = SslProtocols.Tls | SslProtocols.Tls11 | SslProtocols.Tls12;

		// Token: 0x04000F0E RID: 3854
		public const SslProtocols SystemDefaultSecurityProtocols = SslProtocols.None;
	}
}
