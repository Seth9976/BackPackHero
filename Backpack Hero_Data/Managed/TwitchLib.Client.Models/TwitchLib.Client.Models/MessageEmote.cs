using System;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;

namespace TwitchLib.Client.Models
{
	// Token: 0x02000011 RID: 17
	public class MessageEmote
	{
		// Token: 0x060000A5 RID: 165 RVA: 0x00004DE0 File Offset: 0x00002FE0
		public static string SourceMatchingReplacementText(MessageEmote caller)
		{
			int size = (int)caller.Size;
			switch (caller.Source)
			{
			case MessageEmote.EmoteSource.Twitch:
				return string.Format(MessageEmote.TwitchEmoteUrls[size], caller.Id);
			case MessageEmote.EmoteSource.FrankerFaceZ:
				return string.Format(MessageEmote.FrankerFaceZEmoteUrls[size], caller.Id);
			case MessageEmote.EmoteSource.BetterTwitchTv:
				return string.Format(MessageEmote.BetterTwitchTvEmoteUrls[size], caller.Id);
			default:
				return caller.Text;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x00004E5A File Offset: 0x0000305A
		public string Id
		{
			get
			{
				return this._id;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x00004E62 File Offset: 0x00003062
		public string Text
		{
			get
			{
				return this._text;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x00004E6A File Offset: 0x0000306A
		public MessageEmote.EmoteSource Source
		{
			get
			{
				return this._source;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x00004E72 File Offset: 0x00003072
		public MessageEmote.EmoteSize Size
		{
			get
			{
				return this._size;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060000AA RID: 170 RVA: 0x00004E7A File Offset: 0x0000307A
		public string ReplacementString
		{
			get
			{
				return MessageEmote.ReplacementDelegate(this);
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060000AB RID: 171 RVA: 0x00004E87 File Offset: 0x00003087
		// (set) Token: 0x060000AC RID: 172 RVA: 0x00004E8E File Offset: 0x0000308E
		public static MessageEmote.ReplaceEmoteDelegate ReplacementDelegate { get; set; } = new MessageEmote.ReplaceEmoteDelegate(MessageEmote.SourceMatchingReplacementText);

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060000AD RID: 173 RVA: 0x00004E96 File Offset: 0x00003096
		public string EscapedText
		{
			get
			{
				return this._escapedText;
			}
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00004E9E File Offset: 0x0000309E
		public MessageEmote(string id, string text, MessageEmote.EmoteSource source = MessageEmote.EmoteSource.Twitch, MessageEmote.EmoteSize size = MessageEmote.EmoteSize.Small, MessageEmote.ReplaceEmoteDelegate replacementDelegate = null)
		{
			this._id = id;
			this._text = text;
			this._escapedText = Regex.Escape(text);
			this._source = source;
			this._size = size;
			if (replacementDelegate != null)
			{
				MessageEmote.ReplacementDelegate = replacementDelegate;
			}
		}

		// Token: 0x0400009D RID: 157
		public static readonly ReadOnlyCollection<string> TwitchEmoteUrls = new ReadOnlyCollection<string>(new string[] { "https://static-cdn.jtvnw.net/emoticons/v1/{0}/1.0", "https://static-cdn.jtvnw.net/emoticons/v1/{0}/2.0", "https://static-cdn.jtvnw.net/emoticons/v1/{0}/3.0" });

		// Token: 0x0400009E RID: 158
		public static readonly ReadOnlyCollection<string> FrankerFaceZEmoteUrls = new ReadOnlyCollection<string>(new string[] { "//cdn.frankerfacez.com/emoticon/{0}/1", "//cdn.frankerfacez.com/emoticon/{0}/2", "//cdn.frankerfacez.com/emoticon/{0}/4" });

		// Token: 0x0400009F RID: 159
		public static readonly ReadOnlyCollection<string> BetterTwitchTvEmoteUrls = new ReadOnlyCollection<string>(new string[] { "//cdn.betterttv.net/emote/{0}/1x", "//cdn.betterttv.net/emote/{0}/2x", "//cdn.betterttv.net/emote/{0}/3x" });

		// Token: 0x040000A0 RID: 160
		private readonly string _id;

		// Token: 0x040000A1 RID: 161
		private readonly string _text;

		// Token: 0x040000A2 RID: 162
		private readonly string _escapedText;

		// Token: 0x040000A3 RID: 163
		private readonly MessageEmote.EmoteSource _source;

		// Token: 0x040000A4 RID: 164
		private readonly MessageEmote.EmoteSize _size;

		// Token: 0x0200004B RID: 75
		// (Invoke) Token: 0x060002D0 RID: 720
		public delegate string ReplaceEmoteDelegate(MessageEmote caller);

		// Token: 0x0200004C RID: 76
		public enum EmoteSource
		{
			// Token: 0x04000259 RID: 601
			Twitch,
			// Token: 0x0400025A RID: 602
			FrankerFaceZ,
			// Token: 0x0400025B RID: 603
			BetterTwitchTv
		}

		// Token: 0x0200004D RID: 77
		public enum EmoteSize
		{
			// Token: 0x0400025D RID: 605
			Small,
			// Token: 0x0400025E RID: 606
			Medium,
			// Token: 0x0400025F RID: 607
			Large
		}
	}
}
