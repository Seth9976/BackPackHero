using System;

namespace TwitchLib.Client.Models
{
	// Token: 0x02000010 RID: 16
	public class JoinedChannel
	{
		// Token: 0x17000080 RID: 128
		// (get) Token: 0x0600009E RID: 158 RVA: 0x00004D9C File Offset: 0x00002F9C
		public string Channel { get; }

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x0600009F RID: 159 RVA: 0x00004DA4 File Offset: 0x00002FA4
		// (set) Token: 0x060000A0 RID: 160 RVA: 0x00004DAC File Offset: 0x00002FAC
		public ChannelState ChannelState { get; protected set; }

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x00004DB5 File Offset: 0x00002FB5
		// (set) Token: 0x060000A2 RID: 162 RVA: 0x00004DBD File Offset: 0x00002FBD
		public ChatMessage PreviousMessage { get; protected set; }

		// Token: 0x060000A3 RID: 163 RVA: 0x00004DC6 File Offset: 0x00002FC6
		public JoinedChannel(string channel)
		{
			this.Channel = channel;
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00004DD5 File Offset: 0x00002FD5
		public void HandleMessage(ChatMessage message)
		{
			this.PreviousMessage = message;
		}
	}
}
