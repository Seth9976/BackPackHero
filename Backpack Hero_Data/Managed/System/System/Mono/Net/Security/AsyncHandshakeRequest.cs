using System;

namespace Mono.Net.Security
{
	// Token: 0x0200008F RID: 143
	internal class AsyncHandshakeRequest : AsyncProtocolRequest
	{
		// Token: 0x06000237 RID: 567 RVA: 0x00006A32 File Offset: 0x00004C32
		public AsyncHandshakeRequest(MobileAuthenticatedStream parent, bool sync)
			: base(parent, sync)
		{
		}

		// Token: 0x06000238 RID: 568 RVA: 0x00006A3C File Offset: 0x00004C3C
		protected override AsyncOperationStatus Run(AsyncOperationStatus status)
		{
			return base.Parent.ProcessHandshake(status, false);
		}
	}
}
