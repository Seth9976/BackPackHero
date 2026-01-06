using System;

namespace TwitchLib.Client.Models
{
	// Token: 0x0200000C RID: 12
	public class Emote
	{
		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000076 RID: 118 RVA: 0x00004540 File Offset: 0x00002740
		public string Id { get; }

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000077 RID: 119 RVA: 0x00004548 File Offset: 0x00002748
		public string Name { get; }

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000078 RID: 120 RVA: 0x00004550 File Offset: 0x00002750
		public int StartIndex { get; }

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000079 RID: 121 RVA: 0x00004558 File Offset: 0x00002758
		public int EndIndex { get; }

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x0600007A RID: 122 RVA: 0x00004560 File Offset: 0x00002760
		public string ImageUrl { get; }

		// Token: 0x0600007B RID: 123 RVA: 0x00004568 File Offset: 0x00002768
		public Emote(string emoteId, string name, int emoteStartIndex, int emoteEndIndex)
		{
			this.Id = emoteId;
			this.Name = name;
			this.StartIndex = emoteStartIndex;
			this.EndIndex = emoteEndIndex;
			this.ImageUrl = "https://static-cdn.jtvnw.net/emoticons/v1/" + emoteId + "/1.0";
		}
	}
}
