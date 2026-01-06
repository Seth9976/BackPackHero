using System;

namespace System.Net
{
	// Token: 0x020004A3 RID: 1187
	internal class HttpRequestCreator : IWebRequestCreate
	{
		// Token: 0x060025DA RID: 9690 RVA: 0x0000219B File Offset: 0x0000039B
		internal HttpRequestCreator()
		{
		}

		// Token: 0x060025DB RID: 9691 RVA: 0x0008BFD6 File Offset: 0x0008A1D6
		public WebRequest Create(Uri uri)
		{
			return new HttpWebRequest(uri);
		}
	}
}
