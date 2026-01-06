using System;
using System.Collections;

namespace System.Net
{
	// Token: 0x0200046D RID: 1133
	internal class WebProxyData
	{
		// Token: 0x040014E9 RID: 5353
		internal bool bypassOnLocal;

		// Token: 0x040014EA RID: 5354
		internal bool automaticallyDetectSettings;

		// Token: 0x040014EB RID: 5355
		internal Uri proxyAddress;

		// Token: 0x040014EC RID: 5356
		internal Hashtable proxyHostAddresses;

		// Token: 0x040014ED RID: 5357
		internal Uri scriptLocation;

		// Token: 0x040014EE RID: 5358
		internal ArrayList bypassList;
	}
}
