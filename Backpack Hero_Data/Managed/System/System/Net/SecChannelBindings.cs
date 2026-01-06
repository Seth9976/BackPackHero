using System;
using System.Runtime.InteropServices;

namespace System.Net
{
	// Token: 0x020003FF RID: 1023
	[StructLayout(LayoutKind.Sequential)]
	internal class SecChannelBindings
	{
		// Token: 0x0400127D RID: 4733
		internal int dwInitiatorAddrType;

		// Token: 0x0400127E RID: 4734
		internal int cbInitiatorLength;

		// Token: 0x0400127F RID: 4735
		internal int dwInitiatorOffset;

		// Token: 0x04001280 RID: 4736
		internal int dwAcceptorAddrType;

		// Token: 0x04001281 RID: 4737
		internal int cbAcceptorLength;

		// Token: 0x04001282 RID: 4738
		internal int dwAcceptorOffset;

		// Token: 0x04001283 RID: 4739
		internal int cbApplicationDataLength;

		// Token: 0x04001284 RID: 4740
		internal int dwApplicationDataOffset;
	}
}
