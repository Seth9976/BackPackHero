using System;

namespace System.Net
{
	// Token: 0x020003A3 RID: 931
	internal class FtpWebRequestCreator : IWebRequestCreate
	{
		// Token: 0x06001ECD RID: 7885 RVA: 0x0000219B File Offset: 0x0000039B
		internal FtpWebRequestCreator()
		{
		}

		// Token: 0x06001ECE RID: 7886 RVA: 0x00071D3D File Offset: 0x0006FF3D
		public WebRequest Create(Uri uri)
		{
			return new FtpWebRequest(uri);
		}
	}
}
