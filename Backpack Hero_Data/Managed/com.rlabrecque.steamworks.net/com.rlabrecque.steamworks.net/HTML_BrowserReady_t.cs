using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000053 RID: 83
	[CallbackIdentity(4501)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct HTML_BrowserReady_t
	{
		// Token: 0x04000095 RID: 149
		public const int k_iCallback = 4501;

		// Token: 0x04000096 RID: 150
		public HHTMLBrowser unBrowserHandle;
	}
}
