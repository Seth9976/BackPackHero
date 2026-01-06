using System;
using System.Collections.Generic;
using System.Text;
using Febucci.UI.Styles;

namespace Febucci.UI.Core.Parsing
{
	// Token: 0x0200004F RID: 79
	public class StylesParser : TagParserBase
	{
		// Token: 0x06000197 RID: 407 RVA: 0x0000793F File Offset: 0x00005B3F
		public StylesParser(char startSymbol, char closingSymbol, char endSymbol, StyleSheetScriptable sheet)
			: base(startSymbol, closingSymbol, endSymbol)
		{
			this.sheet = sheet;
			this.openedTags = new List<string>();
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00007960 File Offset: 0x00005B60
		public override bool TryProcessingTag(string textInsideBrackets, int tagLength, ref int realTextIndex, StringBuilder finalTextBuilder, int internalOrder)
		{
			if (!this.sheet)
			{
				return false;
			}
			textInsideBrackets = textInsideBrackets.ToLower();
			this.sheet.BuildOnce();
			bool flag = textInsideBrackets[0] == this.closingSymbol;
			int num = (flag ? 1 : 0);
			string text = textInsideBrackets.Substring(num);
			Style style;
			if (this.sheet.TryGetStyle(text, out style))
			{
				if (flag)
				{
					if (this.openedTags.Contains(text))
					{
						finalTextBuilder.Append(style.closingTag);
						this.openedTags.Remove(text);
					}
				}
				else
				{
					finalTextBuilder.Append(style.openingTag);
					this.openedTags.Add(text);
				}
				return true;
			}
			return false;
		}

		// Token: 0x0400011C RID: 284
		private StyleSheetScriptable sheet;

		// Token: 0x0400011D RID: 285
		private List<string> openedTags;
	}
}
