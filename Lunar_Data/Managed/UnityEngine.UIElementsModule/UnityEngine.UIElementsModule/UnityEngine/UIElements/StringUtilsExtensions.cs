using System;
using System.Globalization;
using System.Linq;
using System.Text;

namespace UnityEngine.UIElements
{
	// Token: 0x0200006D RID: 109
	internal static class StringUtilsExtensions
	{
		// Token: 0x0600030E RID: 782 RVA: 0x0000B4BC File Offset: 0x000096BC
		public static string ToPascalCase(this string text)
		{
			return StringUtilsExtensions.ConvertCase(text, StringUtilsExtensions.NoDelimiter, new Func<char, char>(char.ToUpperInvariant), new Func<char, char>(char.ToUpperInvariant));
		}

		// Token: 0x0600030F RID: 783 RVA: 0x0000B4F4 File Offset: 0x000096F4
		public static string ToCamelCase(this string text)
		{
			return StringUtilsExtensions.ConvertCase(text, StringUtilsExtensions.NoDelimiter, new Func<char, char>(char.ToLowerInvariant), new Func<char, char>(char.ToUpperInvariant));
		}

		// Token: 0x06000310 RID: 784 RVA: 0x0000B52C File Offset: 0x0000972C
		public static string ToKebabCase(this string text)
		{
			return StringUtilsExtensions.ConvertCase(text, '-', new Func<char, char>(char.ToLowerInvariant), new Func<char, char>(char.ToLowerInvariant));
		}

		// Token: 0x06000311 RID: 785 RVA: 0x0000B560 File Offset: 0x00009760
		public static string ToTrainCase(this string text)
		{
			return StringUtilsExtensions.ConvertCase(text, '-', new Func<char, char>(char.ToUpperInvariant), new Func<char, char>(char.ToUpperInvariant));
		}

		// Token: 0x06000312 RID: 786 RVA: 0x0000B594 File Offset: 0x00009794
		public static string ToSnakeCase(this string text)
		{
			return StringUtilsExtensions.ConvertCase(text, '_', new Func<char, char>(char.ToLowerInvariant), new Func<char, char>(char.ToLowerInvariant));
		}

		// Token: 0x06000313 RID: 787 RVA: 0x0000B5C8 File Offset: 0x000097C8
		private static string ConvertCase(string text, char outputWordDelimiter, Func<char, char> startOfStringCaseHandler, Func<char, char> middleStringCaseHandler)
		{
			bool flag = text == null;
			if (flag)
			{
				throw new ArgumentNullException("text");
			}
			StringBuilder stringBuilder = new StringBuilder();
			bool flag2 = true;
			bool flag3 = true;
			bool flag4 = true;
			for (int i = 0; i < text.Length; i++)
			{
				char c = text.get_Chars(i);
				bool flag5 = Enumerable.Contains<char>(StringUtilsExtensions.WordDelimiters, c);
				if (flag5)
				{
					bool flag6 = c == outputWordDelimiter;
					if (flag6)
					{
						stringBuilder.Append(outputWordDelimiter);
						flag4 = false;
					}
					flag3 = true;
				}
				else
				{
					bool flag7 = !char.IsLetterOrDigit(c);
					if (flag7)
					{
						flag2 = true;
						flag3 = true;
					}
					else
					{
						bool flag8 = flag3 || char.IsUpper(c);
						if (flag8)
						{
							bool flag9 = flag2;
							if (flag9)
							{
								stringBuilder.Append(startOfStringCaseHandler.Invoke(c));
							}
							else
							{
								bool flag10 = flag4 && outputWordDelimiter != StringUtilsExtensions.NoDelimiter;
								if (flag10)
								{
									stringBuilder.Append(outputWordDelimiter);
								}
								stringBuilder.Append(middleStringCaseHandler.Invoke(c));
								flag4 = true;
							}
							flag2 = false;
							flag3 = false;
						}
						else
						{
							stringBuilder.Append(c);
						}
					}
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000314 RID: 788 RVA: 0x0000B6F4 File Offset: 0x000098F4
		public static bool EndsWithIgnoreCaseFast(this string a, string b)
		{
			int num = a.Length - 1;
			int num2 = b.Length - 1;
			CultureInfo invariantCulture = CultureInfo.InvariantCulture;
			while (num >= 0 && num2 >= 0 && (a.get_Chars(num) == b.get_Chars(num2) || char.ToLower(a.get_Chars(num), invariantCulture) == char.ToLower(b.get_Chars(num2), invariantCulture)))
			{
				num--;
				num2--;
			}
			return num2 < 0;
		}

		// Token: 0x06000315 RID: 789 RVA: 0x0000B770 File Offset: 0x00009970
		public static bool StartsWithIgnoreCaseFast(this string a, string b)
		{
			int length = a.Length;
			int length2 = b.Length;
			int num = 0;
			int num2 = 0;
			CultureInfo invariantCulture = CultureInfo.InvariantCulture;
			while (num < length && num2 < length2 && (a.get_Chars(num) == b.get_Chars(num2) || char.ToLower(a.get_Chars(num), invariantCulture) == char.ToLower(b.get_Chars(num2), invariantCulture)))
			{
				num++;
				num2++;
			}
			return num2 == length2;
		}

		// Token: 0x04000161 RID: 353
		private static readonly char NoDelimiter = '\0';

		// Token: 0x04000162 RID: 354
		private static readonly char[] WordDelimiters = new char[] { ' ', '-', '_' };
	}
}
