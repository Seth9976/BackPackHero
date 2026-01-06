using System;

namespace System.Net.Security
{
	// Token: 0x02000657 RID: 1623
	internal sealed class SafeFreeContextBufferChannelBinding_SECURITY : SafeFreeContextBufferChannelBinding
	{
		// Token: 0x060033F6 RID: 13302 RVA: 0x000BC894 File Offset: 0x000BAA94
		protected override bool ReleaseHandle()
		{
			return global::Interop.SspiCli.FreeContextBuffer(this.handle) == 0;
		}
	}
}
