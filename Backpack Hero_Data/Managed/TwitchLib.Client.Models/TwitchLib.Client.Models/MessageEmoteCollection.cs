using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace TwitchLib.Client.Models
{
	// Token: 0x02000012 RID: 18
	public class MessageEmoteCollection
	{
		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x00004F72 File Offset: 0x00003172
		// (set) Token: 0x060000B1 RID: 177 RVA: 0x00004F7A File Offset: 0x0000317A
		private string CurrentPattern
		{
			get
			{
				return this._currentPattern;
			}
			set
			{
				if (this._currentPattern != null && this._currentPattern.Equals(value))
				{
					return;
				}
				this._currentPattern = value;
				this.PatternChanged = true;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x00004FA4 File Offset: 0x000031A4
		private Regex CurrentRegex
		{
			get
			{
				if (this.PatternChanged)
				{
					if (this.CurrentPattern != null)
					{
						this._regex = new Regex(string.Format(this.CurrentPattern, ""));
						this.PatternChanged = false;
					}
					else
					{
						this._regex = null;
					}
				}
				return this._regex;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x00004FF2 File Offset: 0x000031F2
		// (set) Token: 0x060000B4 RID: 180 RVA: 0x00004FFA File Offset: 0x000031FA
		private bool PatternChanged { get; set; }

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x00005003 File Offset: 0x00003203
		// (set) Token: 0x060000B6 RID: 182 RVA: 0x0000500B File Offset: 0x0000320B
		private MessageEmoteCollection.EmoteFilterDelegate CurrentEmoteFilter { get; set; } = new MessageEmoteCollection.EmoteFilterDelegate(MessageEmoteCollection.AllInclusiveEmoteFilter);

		// Token: 0x060000B7 RID: 183 RVA: 0x00005014 File Offset: 0x00003214
		public MessageEmoteCollection()
		{
			this._emoteList = new SortedList<string, MessageEmote>();
			this._preferredFilter = new MessageEmoteCollection.EmoteFilterDelegate(MessageEmoteCollection.AllInclusiveEmoteFilter);
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x0000504B File Offset: 0x0000324B
		public MessageEmoteCollection(MessageEmoteCollection.EmoteFilterDelegate preferredFilter)
			: this()
		{
			this._preferredFilter = preferredFilter;
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x0000505C File Offset: 0x0000325C
		public void Add(MessageEmote emote)
		{
			MessageEmote messageEmote;
			if (!this._emoteList.TryGetValue(emote.Text, ref messageEmote))
			{
				this._emoteList.Add(emote.Text, emote);
			}
			if (this.CurrentPattern == null)
			{
				this.CurrentPattern = string.Format("(\\b{0}\\b)", emote.EscapedText);
				return;
			}
			this.CurrentPattern = this.CurrentPattern + "|" + string.Format("(\\b{0}\\b)", emote.EscapedText);
		}

		// Token: 0x060000BA RID: 186 RVA: 0x000050D8 File Offset: 0x000032D8
		public void Merge(IEnumerable<MessageEmote> emotes)
		{
			IEnumerator<MessageEmote> enumerator = emotes.GetEnumerator();
			while (enumerator.MoveNext())
			{
				MessageEmote messageEmote = enumerator.Current;
				this.Add(messageEmote);
			}
			enumerator.Dispose();
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00005108 File Offset: 0x00003308
		public void Remove(MessageEmote emote)
		{
			if (!this._emoteList.ContainsKey(emote.Text))
			{
				return;
			}
			this._emoteList.Remove(emote.Text);
			string text = "(^\\(\\\\b" + emote.EscapedText + "\\\\b\\)\\|?)";
			string text2 = "(\\|\\(\\\\b" + emote.EscapedText + "\\\\b\\))";
			string text3 = Regex.Replace(this.CurrentPattern, text + "|" + text2, "");
			this.CurrentPattern = (text3.Equals("") ? null : text3);
		}

		// Token: 0x060000BC RID: 188 RVA: 0x0000519B File Offset: 0x0000339B
		public void RemoveAll()
		{
			this._emoteList.Clear();
			this.CurrentPattern = null;
		}

		// Token: 0x060000BD RID: 189 RVA: 0x000051B0 File Offset: 0x000033B0
		public string ReplaceEmotes(string originalMessage, MessageEmoteCollection.EmoteFilterDelegate del = null)
		{
			if (this.CurrentRegex == null)
			{
				return originalMessage;
			}
			if (del != null && del != this.CurrentEmoteFilter)
			{
				this.CurrentEmoteFilter = del;
			}
			string text = this.CurrentRegex.Replace(originalMessage, new MatchEvaluator(this.GetReplacementString));
			this.CurrentEmoteFilter = this._preferredFilter;
			return text;
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00005203 File Offset: 0x00003403
		public static bool AllInclusiveEmoteFilter(MessageEmote emote)
		{
			return true;
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00005206 File Offset: 0x00003406
		public static bool TwitchOnlyEmoteFilter(MessageEmote emote)
		{
			return emote.Source == MessageEmote.EmoteSource.Twitch;
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00005214 File Offset: 0x00003414
		private string GetReplacementString(Match m)
		{
			if (!this._emoteList.ContainsKey(m.Value))
			{
				return m.Value;
			}
			MessageEmote messageEmote = this._emoteList[m.Value];
			if (!this.CurrentEmoteFilter(messageEmote))
			{
				return m.Value;
			}
			return messageEmote.ReplacementString;
		}

		// Token: 0x040000A6 RID: 166
		private readonly SortedList<string, MessageEmote> _emoteList;

		// Token: 0x040000A7 RID: 167
		private const string BasePattern = "(\\b{0}\\b)";

		// Token: 0x040000A8 RID: 168
		private string _currentPattern;

		// Token: 0x040000A9 RID: 169
		private Regex _regex;

		// Token: 0x040000AA RID: 170
		private readonly MessageEmoteCollection.EmoteFilterDelegate _preferredFilter;

		// Token: 0x0200004E RID: 78
		// (Invoke) Token: 0x060002D4 RID: 724
		public delegate bool EmoteFilterDelegate(MessageEmote emote);
	}
}
