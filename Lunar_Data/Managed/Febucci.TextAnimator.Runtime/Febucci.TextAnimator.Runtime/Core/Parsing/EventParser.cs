using System;
using System.Text;

namespace Febucci.UI.Core.Parsing
{
	// Token: 0x0200004A RID: 74
	public class EventParser : TagParserBase
	{
		// Token: 0x06000183 RID: 387 RVA: 0x0000730C File Offset: 0x0000550C
		public EventParser(char openingBracket, char closingBracket, char closingTagSymbol)
			: base(openingBracket, closingBracket, closingTagSymbol)
		{
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000184 RID: 388 RVA: 0x00007317 File Offset: 0x00005517
		public EventMarker[] results
		{
			get
			{
				return this._results;
			}
		}

		// Token: 0x06000185 RID: 389 RVA: 0x0000731F File Offset: 0x0000551F
		protected override void OnInitialize()
		{
			base.OnInitialize();
			this._results = new EventMarker[0];
		}

		// Token: 0x06000186 RID: 390 RVA: 0x00007334 File Offset: 0x00005534
		public override bool TryProcessingTag(string textInsideBrackets, int tagLength, ref int realTextIndex, StringBuilder finalTextBuilder, int internalOrder)
		{
			if (textInsideBrackets[0] != '?')
			{
				return false;
			}
			int num = textInsideBrackets.IndexOf('=');
			EventMarker eventMarker;
			if (num != -1)
			{
				string text = textInsideBrackets.Substring(1, num - 1);
				string text2 = textInsideBrackets.Substring(num + 1);
				eventMarker = new EventMarker(text, realTextIndex, internalOrder, text2.Replace(" ", "").Split(',', StringSplitOptions.None));
			}
			else
			{
				eventMarker = new EventMarker(textInsideBrackets.Substring(1), realTextIndex, internalOrder, new string[0]);
			}
			Array.Resize<EventMarker>(ref this._results, this._results.Length + 1);
			this._results[this._results.Length - 1] = eventMarker;
			return true;
		}

		// Token: 0x0400010B RID: 267
		private const char eventSymbol = '?';

		// Token: 0x0400010C RID: 268
		private EventMarker[] _results;
	}
}
