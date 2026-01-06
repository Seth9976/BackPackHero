using System;
using System.Text.RegularExpressions;

namespace UnityEngine.UIElements.StyleSheets
{
	// Token: 0x02000356 RID: 854
	internal static class CSSSpec
	{
		// Token: 0x06001B78 RID: 7032 RVA: 0x0007E3E4 File Offset: 0x0007C5E4
		public static int GetSelectorSpecificity(string selector)
		{
			int num = 0;
			StyleSelectorPart[] array;
			bool flag = CSSSpec.ParseSelector(selector, out array);
			if (flag)
			{
				num = CSSSpec.GetSelectorSpecificity(array);
			}
			return num;
		}

		// Token: 0x06001B79 RID: 7033 RVA: 0x0007E410 File Offset: 0x0007C610
		public static int GetSelectorSpecificity(StyleSelectorPart[] parts)
		{
			int num = 1;
			for (int i = 0; i < parts.Length; i++)
			{
				switch (parts[i].type)
				{
				case StyleSelectorType.Type:
					num++;
					break;
				case StyleSelectorType.Class:
				case StyleSelectorType.PseudoClass:
					num += 10;
					break;
				case StyleSelectorType.RecursivePseudoClass:
					throw new ArgumentException("Recursive pseudo classes are not supported");
				case StyleSelectorType.ID:
					num += 100;
					break;
				}
			}
			return num;
		}

		// Token: 0x06001B7A RID: 7034 RVA: 0x0007E48C File Offset: 0x0007C68C
		public static bool ParseSelector(string selector, out StyleSelectorPart[] parts)
		{
			MatchCollection matchCollection = CSSSpec.rgx.Matches(selector);
			int count = matchCollection.Count;
			bool flag = count < 1;
			bool flag2;
			if (flag)
			{
				parts = null;
				flag2 = false;
			}
			else
			{
				parts = new StyleSelectorPart[count];
				for (int i = 0; i < count; i++)
				{
					Match match = matchCollection[i];
					StyleSelectorType styleSelectorType = StyleSelectorType.Unknown;
					string text = string.Empty;
					bool flag3 = !string.IsNullOrEmpty(match.Groups["wildcard"].Value);
					if (flag3)
					{
						text = "*";
						styleSelectorType = StyleSelectorType.Wildcard;
					}
					else
					{
						bool flag4 = !string.IsNullOrEmpty(match.Groups["id"].Value);
						if (flag4)
						{
							text = match.Groups["id"].Value.Substring(1);
							styleSelectorType = StyleSelectorType.ID;
						}
						else
						{
							bool flag5 = !string.IsNullOrEmpty(match.Groups["class"].Value);
							if (flag5)
							{
								text = match.Groups["class"].Value.Substring(1);
								styleSelectorType = StyleSelectorType.Class;
							}
							else
							{
								bool flag6 = !string.IsNullOrEmpty(match.Groups["pseudoclass"].Value);
								if (flag6)
								{
									string value = match.Groups["param"].Value;
									bool flag7 = !string.IsNullOrEmpty(value);
									if (flag7)
									{
										text = value;
										styleSelectorType = StyleSelectorType.RecursivePseudoClass;
									}
									else
									{
										text = match.Groups["pseudoclass"].Value.Substring(1);
										styleSelectorType = StyleSelectorType.PseudoClass;
									}
								}
								else
								{
									bool flag8 = !string.IsNullOrEmpty(match.Groups["type"].Value);
									if (flag8)
									{
										text = match.Groups["type"].Value;
										styleSelectorType = StyleSelectorType.Type;
									}
								}
							}
						}
					}
					parts[i] = new StyleSelectorPart
					{
						type = styleSelectorType,
						value = text
					};
				}
				flag2 = true;
			}
			return flag2;
		}

		// Token: 0x04000D97 RID: 3479
		private static readonly Regex rgx = new Regex("(?<id>#[-]?\\w[\\w-]*)|(?<class>\\.[\\w-]+)|(?<pseudoclass>:[\\w-]+(\\((?<param>.+)\\))?)|(?<type>[^\\-]\\w+)|(?<wildcard>\\*)|\\s+", 9);

		// Token: 0x04000D98 RID: 3480
		private const int typeSelectorWeight = 1;

		// Token: 0x04000D99 RID: 3481
		private const int classSelectorWeight = 10;

		// Token: 0x04000D9A RID: 3482
		private const int idSelectorWeight = 100;
	}
}
