using System;
using System.Runtime.ExceptionServices;

namespace Mono.Net.Security
{
	// Token: 0x0200008A RID: 138
	internal class AsyncProtocolResult
	{
		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600021E RID: 542 RVA: 0x00006354 File Offset: 0x00004554
		public int UserResult { get; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600021F RID: 543 RVA: 0x0000635C File Offset: 0x0000455C
		public ExceptionDispatchInfo Error { get; }

		// Token: 0x06000220 RID: 544 RVA: 0x00006364 File Offset: 0x00004564
		public AsyncProtocolResult(int result)
		{
			this.UserResult = result;
		}

		// Token: 0x06000221 RID: 545 RVA: 0x00006373 File Offset: 0x00004573
		public AsyncProtocolResult(ExceptionDispatchInfo error)
		{
			this.Error = error;
		}
	}
}
