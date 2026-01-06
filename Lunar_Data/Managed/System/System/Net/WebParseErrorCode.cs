using System;

namespace System.Net
{
	// Token: 0x02000435 RID: 1077
	internal enum WebParseErrorCode
	{
		// Token: 0x040013BE RID: 5054
		Generic,
		// Token: 0x040013BF RID: 5055
		InvalidHeaderName,
		// Token: 0x040013C0 RID: 5056
		InvalidContentLength,
		// Token: 0x040013C1 RID: 5057
		IncompleteHeaderLine,
		// Token: 0x040013C2 RID: 5058
		CrLfError,
		// Token: 0x040013C3 RID: 5059
		InvalidChunkFormat,
		// Token: 0x040013C4 RID: 5060
		UnexpectedServerResponse
	}
}
