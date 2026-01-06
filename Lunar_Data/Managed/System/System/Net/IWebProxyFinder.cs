using System;
using System.Collections.Generic;

namespace System.Net
{
	// Token: 0x020003EA RID: 1002
	internal interface IWebProxyFinder : IDisposable
	{
		// Token: 0x060020A5 RID: 8357
		bool GetProxies(Uri destination, out IList<string> proxyList);

		// Token: 0x060020A6 RID: 8358
		void Abort();

		// Token: 0x060020A7 RID: 8359
		void Reset();

		// Token: 0x1700066D RID: 1645
		// (get) Token: 0x060020A8 RID: 8360
		bool IsValid { get; }
	}
}
