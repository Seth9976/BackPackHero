using System;

namespace TwitchLib.PubSub.Events
{
	// Token: 0x02000032 RID: 50
	public class OnBitsReceivedV2Args
	{
		// Token: 0x170000AC RID: 172
		// (get) Token: 0x0600021B RID: 539 RVA: 0x0000732D File Offset: 0x0000552D
		// (set) Token: 0x0600021C RID: 540 RVA: 0x00007335 File Offset: 0x00005535
		public string UserName { get; internal set; }

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x0600021D RID: 541 RVA: 0x0000733E File Offset: 0x0000553E
		// (set) Token: 0x0600021E RID: 542 RVA: 0x00007346 File Offset: 0x00005546
		public string ChannelName { get; internal set; }

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x0600021F RID: 543 RVA: 0x0000734F File Offset: 0x0000554F
		// (set) Token: 0x06000220 RID: 544 RVA: 0x00007357 File Offset: 0x00005557
		public string UserId { get; internal set; }

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000221 RID: 545 RVA: 0x00007360 File Offset: 0x00005560
		// (set) Token: 0x06000222 RID: 546 RVA: 0x00007368 File Offset: 0x00005568
		public string ChannelId { get; internal set; }

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000223 RID: 547 RVA: 0x00007371 File Offset: 0x00005571
		// (set) Token: 0x06000224 RID: 548 RVA: 0x00007379 File Offset: 0x00005579
		public DateTime Time { get; internal set; }

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000225 RID: 549 RVA: 0x00007382 File Offset: 0x00005582
		// (set) Token: 0x06000226 RID: 550 RVA: 0x0000738A File Offset: 0x0000558A
		public string ChatMessage { get; internal set; }

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000227 RID: 551 RVA: 0x00007393 File Offset: 0x00005593
		// (set) Token: 0x06000228 RID: 552 RVA: 0x0000739B File Offset: 0x0000559B
		public int BitsUsed { get; internal set; }

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000229 RID: 553 RVA: 0x000073A4 File Offset: 0x000055A4
		// (set) Token: 0x0600022A RID: 554 RVA: 0x000073AC File Offset: 0x000055AC
		public int TotalBitsUsed { get; internal set; }

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x0600022B RID: 555 RVA: 0x000073B5 File Offset: 0x000055B5
		// (set) Token: 0x0600022C RID: 556 RVA: 0x000073BD File Offset: 0x000055BD
		public bool IsAnonymous { get; internal set; }

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x0600022D RID: 557 RVA: 0x000073C6 File Offset: 0x000055C6
		// (set) Token: 0x0600022E RID: 558 RVA: 0x000073CE File Offset: 0x000055CE
		public string Context { get; internal set; }
	}
}
