using System;

namespace System.Net.Security
{
	// Token: 0x02000650 RID: 1616
	internal sealed class SafeFreeContextBuffer_SECURITY : SafeFreeContextBuffer
	{
		// Token: 0x060033DD RID: 13277 RVA: 0x000BC88C File Offset: 0x000BAA8C
		internal SafeFreeContextBuffer_SECURITY()
		{
		}

		// Token: 0x060033DE RID: 13278 RVA: 0x000BC894 File Offset: 0x000BAA94
		protected override bool ReleaseHandle()
		{
			return global::Interop.SspiCli.FreeContextBuffer(this.handle) == 0;
		}
	}
}
