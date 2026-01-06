using System;

namespace System.Net
{
	// Token: 0x020003EF RID: 1007
	internal static class NclConstants
	{
		// Token: 0x040011D1 RID: 4561
		internal static readonly object Sentinel = new object();

		// Token: 0x040011D2 RID: 4562
		internal static readonly object[] EmptyObjectArray = new object[0];

		// Token: 0x040011D3 RID: 4563
		internal static readonly Uri[] EmptyUriArray = new Uri[0];

		// Token: 0x040011D4 RID: 4564
		internal static readonly byte[] CRLF = new byte[] { 13, 10 };

		// Token: 0x040011D5 RID: 4565
		internal static readonly byte[] ChunkTerminator = new byte[] { 48, 13, 10, 13, 10 };
	}
}
