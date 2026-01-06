using System;

namespace TwitchLib.Client.Models
{
	// Token: 0x02000013 RID: 19
	public class OutboundChatMessage
	{
		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x00005268 File Offset: 0x00003468
		// (set) Token: 0x060000C2 RID: 194 RVA: 0x00005270 File Offset: 0x00003470
		public string Channel { get; set; }

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x00005279 File Offset: 0x00003479
		// (set) Token: 0x060000C4 RID: 196 RVA: 0x00005281 File Offset: 0x00003481
		public string Message { get; set; }

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x0000528A File Offset: 0x0000348A
		// (set) Token: 0x060000C6 RID: 198 RVA: 0x00005292 File Offset: 0x00003492
		public string Username { get; set; }

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x0000529B File Offset: 0x0000349B
		// (set) Token: 0x060000C8 RID: 200 RVA: 0x000052A3 File Offset: 0x000034A3
		public string ReplyToId { get; set; }

		// Token: 0x060000C9 RID: 201 RVA: 0x000052AC File Offset: 0x000034AC
		public override string ToString()
		{
			string text = this.Username.ToLower();
			string text2 = this.Channel.ToLower();
			if (this.ReplyToId == null)
			{
				return string.Concat(new string[] { ":", text, "!", text, "@", text, ".tmi.twitch.tv PRIVMSG #", text2, " :", this.Message });
			}
			return string.Concat(new string[] { "@reply-parent-msg-id=", this.ReplyToId, " PRIVMSG #", text2, " :", this.Message });
		}
	}
}
