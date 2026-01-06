using System;
using System.Text;
using Febucci.UI.Actions;

namespace Febucci.UI.Core.Parsing
{
	// Token: 0x02000048 RID: 72
	public sealed class ActionParser : TagParserBase
	{
		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600017E RID: 382 RVA: 0x000071F3 File Offset: 0x000053F3
		public ActionMarker[] results
		{
			get
			{
				return this._results;
			}
		}

		// Token: 0x0600017F RID: 383 RVA: 0x000071FB File Offset: 0x000053FB
		public ActionParser(char startSymbol, char closingSymbol, char endSymbol, ActionDatabase actionDatabase)
			: base(startSymbol, closingSymbol, endSymbol)
		{
			this.database = actionDatabase;
		}

		// Token: 0x06000180 RID: 384 RVA: 0x0000720E File Offset: 0x0000540E
		protected override void OnInitialize()
		{
			base.OnInitialize();
			this._results = new ActionMarker[0];
			if (this.database)
			{
				this.database.BuildOnce();
			}
		}

		// Token: 0x06000181 RID: 385 RVA: 0x0000723C File Offset: 0x0000543C
		public override bool TryProcessingTag(string textInsideBrackets, int tagLength, ref int realTextIndex, StringBuilder finalTextBuilder, int internalOrder)
		{
			if (!this.database)
			{
				return false;
			}
			this.database.BuildOnce();
			int num = textInsideBrackets.IndexOf('=');
			string text = ((num == -1) ? textInsideBrackets : textInsideBrackets.Substring(0, num));
			text = text.ToLower();
			if (!this.database.ContainsKey(text))
			{
				return false;
			}
			ActionMarker actionMarker;
			if (num != -1)
			{
				string text2 = textInsideBrackets.Substring(num + 1);
				actionMarker = new ActionMarker(text, realTextIndex, internalOrder, text2.Replace(" ", "").Split(',', StringSplitOptions.None));
			}
			else
			{
				actionMarker = new ActionMarker(text, realTextIndex, internalOrder, new string[0]);
			}
			Array.Resize<ActionMarker>(ref this._results, this._results.Length + 1);
			this._results[this._results.Length - 1] = actionMarker;
			return true;
		}

		// Token: 0x04000109 RID: 265
		public ActionDatabase database;

		// Token: 0x0400010A RID: 266
		private ActionMarker[] _results;
	}
}
