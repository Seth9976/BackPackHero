using System;

namespace Mono.Net.Security
{
	// Token: 0x02000093 RID: 147
	internal class AsyncShutdownRequest : AsyncProtocolRequest
	{
		// Token: 0x06000242 RID: 578 RVA: 0x00006BC9 File Offset: 0x00004DC9
		public AsyncShutdownRequest(MobileAuthenticatedStream parent)
			: base(parent, false)
		{
		}

		// Token: 0x06000243 RID: 579 RVA: 0x00006BD3 File Offset: 0x00004DD3
		protected override AsyncOperationStatus Run(AsyncOperationStatus status)
		{
			return base.Parent.ProcessShutdown(status);
		}
	}
}
