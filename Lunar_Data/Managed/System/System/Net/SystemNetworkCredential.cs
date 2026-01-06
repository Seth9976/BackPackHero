using System;

namespace System.Net
{
	// Token: 0x020003D8 RID: 984
	internal class SystemNetworkCredential : NetworkCredential
	{
		// Token: 0x06002064 RID: 8292 RVA: 0x00076B9D File Offset: 0x00074D9D
		private SystemNetworkCredential()
			: base(string.Empty, string.Empty, string.Empty)
		{
		}

		// Token: 0x04001138 RID: 4408
		internal static readonly SystemNetworkCredential defaultCredential = new SystemNetworkCredential();
	}
}
