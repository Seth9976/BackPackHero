using System;

namespace System.Net
{
	// Token: 0x02000443 RID: 1091
	internal interface IAutoWebProxy : IWebProxy
	{
		// Token: 0x06002294 RID: 8852
		ProxyChain GetProxies(Uri destination);
	}
}
