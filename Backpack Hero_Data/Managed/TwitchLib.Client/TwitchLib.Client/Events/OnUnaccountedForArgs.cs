using System;

namespace TwitchLib.Client.Events
{
	// Token: 0x0200004F RID: 79
	public class OnUnaccountedForArgs : EventArgs
	{
		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000236 RID: 566 RVA: 0x00008032 File Offset: 0x00006232
		// (set) Token: 0x06000237 RID: 567 RVA: 0x0000803A File Offset: 0x0000623A
		public string RawIRC { get; set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000238 RID: 568 RVA: 0x00008043 File Offset: 0x00006243
		// (set) Token: 0x06000239 RID: 569 RVA: 0x0000804B File Offset: 0x0000624B
		public string Location { get; set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600023A RID: 570 RVA: 0x00008054 File Offset: 0x00006254
		// (set) Token: 0x0600023B RID: 571 RVA: 0x0000805C File Offset: 0x0000625C
		public string BotUsername { get; set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600023C RID: 572 RVA: 0x00008065 File Offset: 0x00006265
		// (set) Token: 0x0600023D RID: 573 RVA: 0x0000806D File Offset: 0x0000626D
		public string Channel { get; set; }
	}
}
