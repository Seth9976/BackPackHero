using System;
using System.Collections.Generic;
using System.Linq;
using TwitchLib.Client.Models.Extractors;

namespace TwitchLib.Client.Models
{
	// Token: 0x0200000D RID: 13
	public class EmoteSet
	{
		// Token: 0x17000064 RID: 100
		// (get) Token: 0x0600007C RID: 124 RVA: 0x000045A3 File Offset: 0x000027A3
		public List<Emote> Emotes { get; }

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600007D RID: 125 RVA: 0x000045AB File Offset: 0x000027AB
		public string RawEmoteSetString { get; }

		// Token: 0x0600007E RID: 126 RVA: 0x000045B4 File Offset: 0x000027B4
		public EmoteSet(string rawEmoteSetString, string message)
		{
			this.RawEmoteSetString = rawEmoteSetString;
			EmoteExtractor emoteExtractor = new EmoteExtractor();
			this.Emotes = Enumerable.ToList<Emote>(emoteExtractor.Extract(rawEmoteSetString, message));
		}

		// Token: 0x0600007F RID: 127 RVA: 0x000045E7 File Offset: 0x000027E7
		public EmoteSet(IEnumerable<Emote> emotes, string emoteSetData)
		{
			this.RawEmoteSetString = emoteSetData;
			this.Emotes = Enumerable.ToList<Emote>(emotes);
		}
	}
}
