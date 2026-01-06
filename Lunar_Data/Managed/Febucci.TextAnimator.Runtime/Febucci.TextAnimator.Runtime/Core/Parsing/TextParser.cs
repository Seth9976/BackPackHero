using System;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine;

namespace Febucci.UI.Core.Parsing
{
	// Token: 0x02000052 RID: 82
	public static class TextParser
	{
		// Token: 0x0600019E RID: 414 RVA: 0x00007B40 File Offset: 0x00005D40
		public static string ParseText(string text, params TagParserBase[] rules)
		{
			if (rules == null || rules.Length == 0)
			{
				Debug.LogWarning("No rules were provided to parse the text. Skipping");
				return text;
			}
			for (int i = 0; i < rules.Length; i++)
			{
				rules[i].Initialize();
			}
			TextParser.<>c__DisplayClass0_0 CS$<>8__locals1;
			CS$<>8__locals1.result = new StringBuilder();
			char[] array = text.ToCharArray();
			int num = array.Length;
			bool flag = true;
			TextParser.<>c__DisplayClass0_1 CS$<>8__locals2;
			CS$<>8__locals2.textIndex = 0;
			int num2 = 0;
			while (CS$<>8__locals2.textIndex < num)
			{
				CS$<>8__locals1.foundTag = false;
				if (array[CS$<>8__locals2.textIndex] == '<')
				{
					TextParser.<>c__DisplayClass0_2 CS$<>8__locals3;
					CS$<>8__locals3.closeIndex = text.IndexOf('>', CS$<>8__locals2.textIndex + 1);
					if (CS$<>8__locals3.closeIndex > 0)
					{
						int num3 = CS$<>8__locals3.closeIndex - CS$<>8__locals2.textIndex + 1;
						CS$<>8__locals1.fullTag = text.Substring(CS$<>8__locals2.textIndex, num3);
						string text2 = CS$<>8__locals1.fullTag.ToLower();
						if (!(text2 == "<noparse>"))
						{
							if (text2 == "</noparse>")
							{
								flag = true;
								TextParser.<ParseText>g__PasteTagToText|0_0(ref CS$<>8__locals1, ref CS$<>8__locals2, ref CS$<>8__locals3);
							}
						}
						else
						{
							flag = false;
							TextParser.<ParseText>g__PasteTagToText|0_0(ref CS$<>8__locals1, ref CS$<>8__locals2, ref CS$<>8__locals3);
						}
					}
				}
				if (flag && !CS$<>8__locals1.foundTag)
				{
					foreach (TagParserBase tagParserBase in rules)
					{
						if (array[CS$<>8__locals2.textIndex] == tagParserBase.startSymbol)
						{
							int num4 = CS$<>8__locals2.textIndex + 1;
							while (num4 < num && !CS$<>8__locals1.foundTag && array[num4] != tagParserBase.startSymbol)
							{
								if (array[num4] == tagParserBase.endSymbol)
								{
									int num5 = num4 - CS$<>8__locals2.textIndex - 1;
									if (num5 == 0)
									{
										break;
									}
									if (tagParserBase.TryProcessingTag(text.Substring(CS$<>8__locals2.textIndex + 1, num5), num5, ref num2, CS$<>8__locals1.result, CS$<>8__locals2.textIndex))
									{
										CS$<>8__locals1.foundTag = true;
										CS$<>8__locals2.textIndex = num4;
										break;
									}
								}
								num4++;
							}
						}
					}
				}
				if (!CS$<>8__locals1.foundTag)
				{
					CS$<>8__locals1.result.Append(array[CS$<>8__locals2.textIndex]);
					num2++;
				}
				int i = CS$<>8__locals2.textIndex;
				CS$<>8__locals2.textIndex = i + 1;
			}
			return CS$<>8__locals1.result.ToString();
		}

		// Token: 0x0600019F RID: 415 RVA: 0x00007D75 File Offset: 0x00005F75
		[CompilerGenerated]
		internal static void <ParseText>g__PasteTagToText|0_0(ref TextParser.<>c__DisplayClass0_0 A_0, ref TextParser.<>c__DisplayClass0_1 A_1, ref TextParser.<>c__DisplayClass0_2 A_2)
		{
			A_0.foundTag = true;
			A_0.result.Append(A_0.fullTag);
			A_1.textIndex = A_2.closeIndex;
		}
	}
}
