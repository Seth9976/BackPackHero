using System;

namespace TwitchLib.Client.Models
{
	// Token: 0x02000014 RID: 20
	public class OutboundWhisperMessage
	{
		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060000CB RID: 203 RVA: 0x00005369 File Offset: 0x00003569
		// (set) Token: 0x060000CC RID: 204 RVA: 0x00005371 File Offset: 0x00003571
		public string Username { get; set; }

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060000CD RID: 205 RVA: 0x0000537A File Offset: 0x0000357A
		// (set) Token: 0x060000CE RID: 206 RVA: 0x00005382 File Offset: 0x00003582
		public string Receiver { get; set; }

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060000CF RID: 207 RVA: 0x0000538B File Offset: 0x0000358B
		// (set) Token: 0x060000D0 RID: 208 RVA: 0x00005393 File Offset: 0x00003593
		public string Message { get; set; }

		// Token: 0x060000D1 RID: 209 RVA: 0x0000539C File Offset: 0x0000359C
		public override string ToString()
		{
			return string.Concat(new string[] { ":", this.Username, "~", this.Username, "@", this.Username, ".tmi.twitch.tv PRIVMSG #jtv :/w ", this.Receiver, " ", this.Message });
		}
	}
}
