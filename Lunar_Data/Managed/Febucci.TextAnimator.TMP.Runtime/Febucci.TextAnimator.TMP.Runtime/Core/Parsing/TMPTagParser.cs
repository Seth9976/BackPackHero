using System;
using System.Globalization;
using System.Text;

namespace Febucci.UI.Core.Parsing
{
	// Token: 0x02000003 RID: 3
	public class TMPTagParser : TagParserBase
	{
		// Token: 0x06000013 RID: 19 RVA: 0x000025CF File Offset: 0x000007CF
		public TMPTagParser(bool richTagsEnabled, char openingBracket, char closingBracket, char closingTagSymbol)
			: base(openingBracket, closingBracket, closingTagSymbol)
		{
			this.richTagsEnabled = richTagsEnabled;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000025E4 File Offset: 0x000007E4
		public override bool TryProcessingTag(string textInsideBrackets, int tagLength, ref int realTextIndex, StringBuilder finalTextBuilder, int internalOrder)
		{
			if (!this.richTagsEnabled)
			{
				return false;
			}
			string text = this.startSymbol.ToString() + textInsideBrackets + this.endSymbol.ToString();
			foreach (TMPTagParser.TMPTagInfo tmptagInfo in TMPTagParser.lookups)
			{
				if (text.StartsWith(tmptagInfo.tagOpening, true, CultureInfo.InvariantCulture))
				{
					finalTextBuilder.Append(text);
					if (tmptagInfo.increasesTextLength)
					{
						realTextIndex++;
					}
					return true;
				}
			}
			return false;
		}

		// Token: 0x0400000B RID: 11
		private readonly bool richTagsEnabled;

		// Token: 0x0400000C RID: 12
		private static readonly TMPTagParser.TMPTagInfo[] lookups = new TMPTagParser.TMPTagInfo[]
		{
			new TMPTagParser.TMPTagInfo("<align=", false),
			new TMPTagParser.TMPTagInfo("</align>", false),
			new TMPTagParser.TMPTagInfo("<allcaps>", false),
			new TMPTagParser.TMPTagInfo("</allcaps>", false),
			new TMPTagParser.TMPTagInfo("<alpha=", false),
			new TMPTagParser.TMPTagInfo("</alpha>", false),
			new TMPTagParser.TMPTagInfo("<b>", false),
			new TMPTagParser.TMPTagInfo("</b>", false),
			new TMPTagParser.TMPTagInfo("<color=", false),
			new TMPTagParser.TMPTagInfo("</color>", false),
			new TMPTagParser.TMPTagInfo("<cspace=", false),
			new TMPTagParser.TMPTagInfo("</cspace>", false),
			new TMPTagParser.TMPTagInfo("<font=", false),
			new TMPTagParser.TMPTagInfo("</font>", false),
			new TMPTagParser.TMPTagInfo("<font-weight=", false),
			new TMPTagParser.TMPTagInfo("</font-weight>", false),
			new TMPTagParser.TMPTagInfo("<gradient=", false),
			new TMPTagParser.TMPTagInfo("</gradient>", false),
			new TMPTagParser.TMPTagInfo("<i>", false),
			new TMPTagParser.TMPTagInfo("</i>", false),
			new TMPTagParser.TMPTagInfo("<indent=", false),
			new TMPTagParser.TMPTagInfo("</indent>", false),
			new TMPTagParser.TMPTagInfo("<line-height=", false),
			new TMPTagParser.TMPTagInfo("</line-height>", false),
			new TMPTagParser.TMPTagInfo("<line-indent=", false),
			new TMPTagParser.TMPTagInfo("</line-indent>", false),
			new TMPTagParser.TMPTagInfo("<link=", false),
			new TMPTagParser.TMPTagInfo("</link>", false),
			new TMPTagParser.TMPTagInfo("<link>", false),
			new TMPTagParser.TMPTagInfo("</link>", false),
			new TMPTagParser.TMPTagInfo("<lowercase>", false),
			new TMPTagParser.TMPTagInfo("</lowercase>", false),
			new TMPTagParser.TMPTagInfo("<margin=", false),
			new TMPTagParser.TMPTagInfo("</margin>", false),
			new TMPTagParser.TMPTagInfo("<margin-left>", false),
			new TMPTagParser.TMPTagInfo("<margin-right>", false),
			new TMPTagParser.TMPTagInfo("<mark=", false),
			new TMPTagParser.TMPTagInfo("</mark>", false),
			new TMPTagParser.TMPTagInfo("<mspace=", false),
			new TMPTagParser.TMPTagInfo("</mspace>", false),
			new TMPTagParser.TMPTagInfo("<nobr>", false),
			new TMPTagParser.TMPTagInfo("</nobr>", false),
			new TMPTagParser.TMPTagInfo("<page>", false),
			new TMPTagParser.TMPTagInfo("<pos=", false),
			new TMPTagParser.TMPTagInfo("<rotate=", false),
			new TMPTagParser.TMPTagInfo("</rotate>", false),
			new TMPTagParser.TMPTagInfo("<s>", false),
			new TMPTagParser.TMPTagInfo("</s>", false),
			new TMPTagParser.TMPTagInfo("<size=", false),
			new TMPTagParser.TMPTagInfo("</size>", false),
			new TMPTagParser.TMPTagInfo("<smallcaps>", false),
			new TMPTagParser.TMPTagInfo("</smallcaps>", false),
			new TMPTagParser.TMPTagInfo("<space=", false),
			new TMPTagParser.TMPTagInfo("<sprite", true),
			new TMPTagParser.TMPTagInfo("<sprite ", true),
			new TMPTagParser.TMPTagInfo("<style=", false),
			new TMPTagParser.TMPTagInfo("</style>", false),
			new TMPTagParser.TMPTagInfo("<sub>", false),
			new TMPTagParser.TMPTagInfo("</sub>", false),
			new TMPTagParser.TMPTagInfo("<sup>", false),
			new TMPTagParser.TMPTagInfo("</sup>", false),
			new TMPTagParser.TMPTagInfo("<u>", false),
			new TMPTagParser.TMPTagInfo("</u>", false),
			new TMPTagParser.TMPTagInfo("<uppercase>", false),
			new TMPTagParser.TMPTagInfo("</uppercase>", false),
			new TMPTagParser.TMPTagInfo("<voffset=", false),
			new TMPTagParser.TMPTagInfo("</voffset>", false),
			new TMPTagParser.TMPTagInfo("<width=", false),
			new TMPTagParser.TMPTagInfo("</width>", false)
		};

		// Token: 0x02000004 RID: 4
		private struct TMPTagInfo
		{
			// Token: 0x06000016 RID: 22 RVA: 0x00002B93 File Offset: 0x00000D93
			public TMPTagInfo(string tagOpening, bool increasesTextLength = false)
			{
				this.tagOpening = tagOpening;
				this.increasesTextLength = increasesTextLength;
			}

			// Token: 0x0400000D RID: 13
			public readonly string tagOpening;

			// Token: 0x0400000E RID: 14
			public readonly bool increasesTextLength;
		}
	}
}
