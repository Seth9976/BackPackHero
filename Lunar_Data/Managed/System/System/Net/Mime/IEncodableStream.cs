using System;
using System.IO;

namespace System.Net.Mime
{
	// Token: 0x0200060C RID: 1548
	internal interface IEncodableStream
	{
		// Token: 0x060031C6 RID: 12742
		int DecodeBytes(byte[] buffer, int offset, int count);

		// Token: 0x060031C7 RID: 12743
		int EncodeBytes(byte[] buffer, int offset, int count);

		// Token: 0x060031C8 RID: 12744
		string GetEncodedString();

		// Token: 0x060031C9 RID: 12745
		Stream GetStream();
	}
}
