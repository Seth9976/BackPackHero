using System;

namespace TwitchLib.Client.Models
{
	// Token: 0x02000006 RID: 6
	public class ChatReply
	{
		// Token: 0x1700003F RID: 63
		// (get) Token: 0x0600004A RID: 74 RVA: 0x0000392B File Offset: 0x00001B2B
		// (set) Token: 0x0600004B RID: 75 RVA: 0x00003933 File Offset: 0x00001B33
		public string ParentDisplayName { get; internal set; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x0600004C RID: 76 RVA: 0x0000393C File Offset: 0x00001B3C
		// (set) Token: 0x0600004D RID: 77 RVA: 0x00003944 File Offset: 0x00001B44
		public string ParentMsgBody { get; internal set; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x0600004E RID: 78 RVA: 0x0000394D File Offset: 0x00001B4D
		// (set) Token: 0x0600004F RID: 79 RVA: 0x00003955 File Offset: 0x00001B55
		public string ParentMsgId { get; internal set; }

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000050 RID: 80 RVA: 0x0000395E File Offset: 0x00001B5E
		// (set) Token: 0x06000051 RID: 81 RVA: 0x00003966 File Offset: 0x00001B66
		public string ParentUserId { get; internal set; }

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000052 RID: 82 RVA: 0x0000396F File Offset: 0x00001B6F
		// (set) Token: 0x06000053 RID: 83 RVA: 0x00003977 File Offset: 0x00001B77
		public string ParentUserLogin { get; internal set; }
	}
}
