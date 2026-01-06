using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Febucci.UI.Effects;

namespace Febucci.UI.Core.Parsing
{
	// Token: 0x0200004C RID: 76
	public class AnimationParser<T> : TagParserBase where T : AnimationScriptableBase
	{
		// Token: 0x0600018A RID: 394 RVA: 0x00007498 File Offset: 0x00005698
		public AnimationParser(char startSymbol, char closingSymbol, char endSymbol, VisibilityMode visibilityMode, Database<T> database)
			: base(startSymbol, closingSymbol, endSymbol)
		{
			this.visibilityMode = visibilityMode;
			this.database = database;
			this.middleSymbol = '\n';
		}

		// Token: 0x0600018B RID: 395 RVA: 0x000074BB File Offset: 0x000056BB
		public AnimationParser(char startSymbol, char closingSymbol, char middleSymbol, char endSymbol, VisibilityMode visibilityMode, Database<T> database)
			: base(startSymbol, closingSymbol, endSymbol)
		{
			this.visibilityMode = visibilityMode;
			this.database = database;
			this.middleSymbol = middleSymbol;
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600018C RID: 396 RVA: 0x000074DE File Offset: 0x000056DE
		public AnimationRegion[] results
		{
			get
			{
				return this._results.Values.ToArray<AnimationRegion>();
			}
		}

		// Token: 0x0600018D RID: 397 RVA: 0x000074F0 File Offset: 0x000056F0
		protected override void OnInitialize()
		{
			base.OnInitialize();
			this._results = new Dictionary<string, AnimationRegion>();
			if (this.database)
			{
				this.database.BuildOnce();
			}
		}

		// Token: 0x0600018E RID: 398 RVA: 0x0000751C File Offset: 0x0000571C
		public override bool TryProcessingTag(string textInsideBrackets, int tagLength, ref int realTextIndex, StringBuilder finalTextBuilder, int internalOrder)
		{
			if (!this.database)
			{
				return false;
			}
			textInsideBrackets = textInsideBrackets.ToLower();
			this.database.BuildOnce();
			bool flag = textInsideBrackets[0] == this.closingSymbol;
			if (flag && tagLength == 1)
			{
				foreach (AnimationRegion animationRegion in this._results.Values)
				{
					animationRegion.CloseAllOpenedRanges(realTextIndex);
				}
				return true;
			}
			int num = (flag ? 1 : 0);
			string[] array = textInsideBrackets.Substring(num).Split(Array.Empty<char>());
			string text = array[0];
			if (flag && array.Length > 1)
			{
				return false;
			}
			if (this.middleSymbol != '\n')
			{
				if (text[0] != this.middleSymbol)
				{
					return false;
				}
				text = text.Substring(1);
			}
			if (!this.database.ContainsKey(text))
			{
				return false;
			}
			if (flag)
			{
				if (this._results.ContainsKey(text))
				{
					this._results[text].TryClosingRange(realTextIndex);
				}
			}
			else
			{
				if (!this._results.ContainsKey(text))
				{
					this._results.Add(text, new AnimationRegion(text, this.visibilityMode, this.database[text]));
				}
				this._results[text].OpenNewRange(realTextIndex, array);
			}
			return true;
		}

		// Token: 0x04000111 RID: 273
		public Database<T> database;

		// Token: 0x04000112 RID: 274
		private VisibilityMode visibilityMode;

		// Token: 0x04000113 RID: 275
		private char middleSymbol;

		// Token: 0x04000114 RID: 276
		private const char middleSymbolDefault = '\n';

		// Token: 0x04000115 RID: 277
		private Dictionary<string, AnimationRegion> _results;
	}
}
