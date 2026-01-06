using System;

namespace TwitchLib.Client.Models
{
	// Token: 0x02000015 RID: 21
	public class OutgoingMessage
	{
		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x00005413 File Offset: 0x00003613
		// (set) Token: 0x060000D4 RID: 212 RVA: 0x0000541B File Offset: 0x0000361B
		public string Channel { get; set; }

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060000D5 RID: 213 RVA: 0x00005424 File Offset: 0x00003624
		// (set) Token: 0x060000D6 RID: 214 RVA: 0x0000542C File Offset: 0x0000362C
		public string Message { get; set; }

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x00005435 File Offset: 0x00003635
		// (set) Token: 0x060000D8 RID: 216 RVA: 0x0000543D File Offset: 0x0000363D
		public int Nonce { get; set; }

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x00005446 File Offset: 0x00003646
		// (set) Token: 0x060000DA RID: 218 RVA: 0x0000544E File Offset: 0x0000364E
		public string Sender { get; set; }

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060000DB RID: 219 RVA: 0x00005457 File Offset: 0x00003657
		// (set) Token: 0x060000DC RID: 220 RVA: 0x0000545F File Offset: 0x0000365F
		public MessageState State { get; set; }
	}
}
