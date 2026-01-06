using System;

namespace TwitchLib.Client.Exceptions
{
	// Token: 0x02000020 RID: 32
	public class FailureToReceiveJoinConfirmationException
	{
		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000201 RID: 513 RVA: 0x00007E70 File Offset: 0x00006070
		// (set) Token: 0x06000202 RID: 514 RVA: 0x00007E78 File Offset: 0x00006078
		public string Channel { get; protected set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000203 RID: 515 RVA: 0x00007E81 File Offset: 0x00006081
		// (set) Token: 0x06000204 RID: 516 RVA: 0x00007E89 File Offset: 0x00006089
		public string Details { get; protected set; }

		// Token: 0x06000205 RID: 517 RVA: 0x00007E92 File Offset: 0x00006092
		public FailureToReceiveJoinConfirmationException(string channel, string details = null)
		{
			this.Channel = channel;
			this.Details = details;
		}
	}
}
