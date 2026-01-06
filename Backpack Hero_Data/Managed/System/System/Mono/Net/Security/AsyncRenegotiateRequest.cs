using System;

namespace Mono.Net.Security
{
	// Token: 0x02000094 RID: 148
	internal class AsyncRenegotiateRequest : AsyncProtocolRequest
	{
		// Token: 0x06000244 RID: 580 RVA: 0x00006BC9 File Offset: 0x00004DC9
		public AsyncRenegotiateRequest(MobileAuthenticatedStream parent)
			: base(parent, false)
		{
		}

		// Token: 0x06000245 RID: 581 RVA: 0x00006BE1 File Offset: 0x00004DE1
		protected override AsyncOperationStatus Run(AsyncOperationStatus status)
		{
			return base.Parent.ProcessHandshake(status, true);
		}
	}
}
