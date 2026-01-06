using System;

namespace System.Net
{
	// Token: 0x02000469 RID: 1129
	internal class FileWebRequestCreator : IWebRequestCreate
	{
		// Token: 0x060023B6 RID: 9142 RVA: 0x0000219B File Offset: 0x0000039B
		internal FileWebRequestCreator()
		{
		}

		// Token: 0x060023B7 RID: 9143 RVA: 0x000842D0 File Offset: 0x000824D0
		public WebRequest Create(Uri uri)
		{
			return new FileWebRequest(uri);
		}
	}
}
