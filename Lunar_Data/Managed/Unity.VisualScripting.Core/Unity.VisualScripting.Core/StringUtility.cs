using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Unity.VisualScripting
{
	// Token: 0x02000163 RID: 355
	public static class StringUtility
	{
		// Token: 0x06000962 RID: 2402 RVA: 0x000285B9 File Offset: 0x000267B9
		public static bool IsNullOrWhiteSpace(string s)
		{
			return s == null || s.Trim() == string.Empty;
		}

		// Token: 0x06000963 RID: 2403 RVA: 0x000285D0 File Offset: 0x000267D0
		public static string FallbackEmpty(string s, string fallback)
		{
			if (string.IsNullOrEmpty(s))
			{
				s = fallback;
			}
			return s;
		}

		// Token: 0x06000964 RID: 2404 RVA: 0x000285DE File Offset: 0x000267DE
		public static string FallbackWhitespace(string s, string fallback)
		{
			if (StringUtility.IsNullOrWhiteSpace(s))
			{
				s = fallback;
			}
			return s;
		}

		// Token: 0x06000965 RID: 2405 RVA: 0x000285EC File Offset: 0x000267EC
		public static void AppendLineFormat(this StringBuilder sb, string format, params object[] args)
		{
			sb.AppendFormat(format, args);
			sb.AppendLine();
		}

		// Token: 0x06000966 RID: 2406 RVA: 0x000285FE File Offset: 0x000267FE
		public static string ToSeparatedString(this IEnumerable enumerable, string separator)
		{
			return string.Join(separator, (from object o in enumerable
				select ((o != null) ? o.ToString() : null) ?? "(null)").ToArray<string>());
		}

		// Token: 0x06000967 RID: 2407 RVA: 0x00028635 File Offset: 0x00026835
		public static string ToCommaSeparatedString(this IEnumerable enumerable)
		{
			return enumerable.ToSeparatedString(", ");
		}

		// Token: 0x06000968 RID: 2408 RVA: 0x00028642 File Offset: 0x00026842
		public static string ToLineSeparatedString(this IEnumerable enumerable)
		{
			return enumerable.ToSeparatedString(Environment.NewLine);
		}

		// Token: 0x06000969 RID: 2409 RVA: 0x0002864F File Offset: 0x0002684F
		public static bool ContainsInsensitive(this string haystack, string needle)
		{
			return haystack.IndexOf(needle, StringComparison.OrdinalIgnoreCase) >= 0;
		}

		// Token: 0x0600096A RID: 2410 RVA: 0x0002865F File Offset: 0x0002685F
		public static IEnumerable<int> AllIndexesOf(this string haystack, string needle)
		{
			if (string.IsNullOrEmpty(needle))
			{
				yield break;
			}
			int index = 0;
			for (;;)
			{
				index = haystack.IndexOf(needle, index, StringComparison.OrdinalIgnoreCase);
				if (index == -1)
				{
					break;
				}
				yield return index;
				index += needle.Length;
			}
			yield break;
		}

		// Token: 0x0600096B RID: 2411 RVA: 0x00028678 File Offset: 0x00026878
		public static string Filter(this string s, bool letters = true, bool numbers = true, bool whitespace = true, bool symbols = true, bool punctuation = true)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (char c in s)
			{
				if ((letters || !char.IsLetter(c)) && (numbers || !char.IsNumber(c)) && (whitespace || !char.IsWhiteSpace(c)) && (symbols || !char.IsSymbol(c)) && (punctuation || !char.IsPunctuation(c)))
				{
					stringBuilder.Append(c);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600096C RID: 2412 RVA: 0x000286F0 File Offset: 0x000268F0
		public static string FilterReplace(this string s, char replacement, bool merge, bool letters = true, bool numbers = true, bool whitespace = true, bool symbols = true, bool punctuation = true)
		{
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = false;
			foreach (char c in s)
			{
				if ((!letters && char.IsLetter(c)) || (!numbers && char.IsNumber(c)) || (!whitespace && char.IsWhiteSpace(c)) || (!symbols && char.IsSymbol(c)) || (!punctuation && char.IsPunctuation(c)))
				{
					if (!merge || !flag)
					{
						stringBuilder.Append(replacement);
					}
					flag = true;
				}
				else
				{
					stringBuilder.Append(c);
					flag = false;
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600096D RID: 2413 RVA: 0x00028784 File Offset: 0x00026984
		public static string Prettify(this string s)
		{
			return s.FirstCharacterToUpper().SplitWords(' ');
		}

		// Token: 0x0600096E RID: 2414 RVA: 0x00028793 File Offset: 0x00026993
		public static bool IsWordDelimiter(char c)
		{
			return char.IsWhiteSpace(c) || char.IsSymbol(c) || char.IsPunctuation(c);
		}

		// Token: 0x0600096F RID: 2415 RVA: 0x000287B0 File Offset: 0x000269B0
		public static bool IsWordBeginning(char? previous, char current, char? next)
		{
			bool flag = previous == null;
			bool flag2 = next == null;
			bool flag3 = char.IsLetter(current);
			bool flag4 = previous != null && char.IsLetter(previous.Value);
			bool flag5 = char.IsNumber(current);
			bool flag6 = previous != null && char.IsNumber(previous.Value);
			bool flag7 = char.IsUpper(current);
			bool flag8 = previous != null && char.IsUpper(previous.Value);
			bool flag9 = StringUtility.IsWordDelimiter(current);
			bool flag10 = previous != null && StringUtility.IsWordDelimiter(previous.Value);
			bool flag11 = next != null && char.IsLower(next.Value);
			return (!flag9 && flag) || (!flag9 && flag10) || (flag3 && flag4 && flag7 && !flag8) || (flag3 && flag4 && flag7 && flag8 && !flag2 && flag11) || (flag5 && flag4) || (flag3 && flag6 && flag7 && flag11);
		}

		// Token: 0x06000970 RID: 2416 RVA: 0x000288BC File Offset: 0x00026ABC
		public static bool IsWordBeginning(string s, int index)
		{
			Ensure.That("index").IsGte<int>(index, 0);
			Ensure.That("index").IsLt<int>(index, s.Length);
			char? c = ((index > 0) ? new char?(s[index - 1]) : null);
			char c2 = s[index];
			char? c3 = ((index < s.Length - 1) ? new char?(s[index + 1]) : null);
			return StringUtility.IsWordBeginning(c, c2, c3);
		}

		// Token: 0x06000971 RID: 2417 RVA: 0x00028944 File Offset: 0x00026B44
		public static string SplitWords(this string s, char separator)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < s.Length; i++)
			{
				char c = s[i];
				if (i > 0 && StringUtility.IsWordBeginning(s, i))
				{
					stringBuilder.Append(separator);
				}
				stringBuilder.Append(c);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000972 RID: 2418 RVA: 0x00028994 File Offset: 0x00026B94
		public static string RemoveConsecutiveCharacters(this string s, char c)
		{
			StringBuilder stringBuilder = new StringBuilder();
			char c2 = '\0';
			foreach (char c3 in s)
			{
				if (c3 != c || c3 != c2)
				{
					stringBuilder.Append(c3);
					c2 = c3;
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000973 RID: 2419 RVA: 0x000289E4 File Offset: 0x00026BE4
		public static string ReplaceMultiple(this string s, HashSet<char> haystacks, char replacement)
		{
			Ensure.That("haystacks").IsNotNull<HashSet<char>>(haystacks);
			StringBuilder stringBuilder = new StringBuilder();
			foreach (char c in s)
			{
				if (haystacks.Contains(c))
				{
					stringBuilder.Append(replacement);
				}
				else
				{
					stringBuilder.Append(c);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000974 RID: 2420 RVA: 0x00028A43 File Offset: 0x00026C43
		public static string Truncate(this string value, int maxLength, string suffix = "...")
		{
			if (value.Length > maxLength)
			{
				return value.Substring(0, maxLength) + suffix;
			}
			return value;
		}

		// Token: 0x06000975 RID: 2421 RVA: 0x00028A5E File Offset: 0x00026C5E
		public static string TrimEnd(this string source, string value)
		{
			if (!source.EndsWith(value))
			{
				return source;
			}
			return source.Remove(source.LastIndexOf(value));
		}

		// Token: 0x06000976 RID: 2422 RVA: 0x00028A78 File Offset: 0x00026C78
		public static string TrimStart(this string source, string value)
		{
			if (!source.StartsWith(value))
			{
				return source;
			}
			return source.Substring(value.Length);
		}

		// Token: 0x06000977 RID: 2423 RVA: 0x00028A94 File Offset: 0x00026C94
		public static string FirstCharacterToLower(this string s)
		{
			if (string.IsNullOrEmpty(s) || char.IsLower(s, 0))
			{
				return s;
			}
			return char.ToLowerInvariant(s[0]).ToString() + s.Substring(1);
		}

		// Token: 0x06000978 RID: 2424 RVA: 0x00028AD4 File Offset: 0x00026CD4
		public static string FirstCharacterToUpper(this string s)
		{
			if (string.IsNullOrEmpty(s) || char.IsUpper(s, 0))
			{
				return s;
			}
			return char.ToUpperInvariant(s[0]).ToString() + s.Substring(1);
		}

		// Token: 0x06000979 RID: 2425 RVA: 0x00028B14 File Offset: 0x00026D14
		public static string PartBefore(this string s, char c)
		{
			Ensure.That("s").IsNotNull(s);
			int num = s.IndexOf(c);
			if (num > 0)
			{
				return s.Substring(0, num);
			}
			return s;
		}

		// Token: 0x0600097A RID: 2426 RVA: 0x00028B48 File Offset: 0x00026D48
		public static string PartAfter(this string s, char c)
		{
			Ensure.That("s").IsNotNull(s);
			int num = s.IndexOf(c);
			if (num > 0)
			{
				return s.Substring(num + 1);
			}
			return s;
		}

		// Token: 0x0600097B RID: 2427 RVA: 0x00028B7C File Offset: 0x00026D7C
		public static void PartsAround(this string s, char c, out string before, out string after)
		{
			Ensure.That("s").IsNotNull(s);
			int num = s.IndexOf(c);
			if (num > 0)
			{
				before = s.Substring(0, num);
				after = s.Substring(num + 1);
				return;
			}
			before = s;
			after = null;
		}

		// Token: 0x0600097C RID: 2428 RVA: 0x00028BC1 File Offset: 0x00026DC1
		public static bool EndsWith(this string s, char c)
		{
			Ensure.That("s").IsNotNull(s);
			return s[s.Length - 1] == c;
		}

		// Token: 0x0600097D RID: 2429 RVA: 0x00028BE4 File Offset: 0x00026DE4
		public static bool StartsWith(this string s, char c)
		{
			Ensure.That("s").IsNotNull(s);
			return s[0] == c;
		}

		// Token: 0x0600097E RID: 2430 RVA: 0x00028C00 File Offset: 0x00026E00
		public static bool Contains(this string s, char c)
		{
			Ensure.That("s").IsNotNull(s);
			for (int i = 0; i < s.Length; i++)
			{
				if (s[i] == c)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600097F RID: 2431 RVA: 0x00028C3B File Offset: 0x00026E3B
		public static string NullIfEmpty(this string s)
		{
			if (s == string.Empty)
			{
				return null;
			}
			return s;
		}

		// Token: 0x06000980 RID: 2432 RVA: 0x00028C4D File Offset: 0x00026E4D
		public static string ToBinaryString(this int value)
		{
			return Convert.ToString(value, 2).PadLeft(8, '0');
		}

		// Token: 0x06000981 RID: 2433 RVA: 0x00028C5E File Offset: 0x00026E5E
		public static string ToBinaryString(this long value)
		{
			return Convert.ToString(value, 2).PadLeft(16, '0');
		}

		// Token: 0x06000982 RID: 2434 RVA: 0x00028C70 File Offset: 0x00026E70
		public static string ToBinaryString(this Enum value)
		{
			return Convert.ToString(Convert.ToInt64(value), 2).PadLeft(16, '0');
		}

		// Token: 0x06000983 RID: 2435 RVA: 0x00028C88 File Offset: 0x00026E88
		public static int CountIndices(this string s, char c)
		{
			int num = 0;
			foreach (char c2 in s)
			{
				if (c == c2)
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x06000984 RID: 2436 RVA: 0x00028CBB File Offset: 0x00026EBB
		public static bool IsGuid(string value)
		{
			return StringUtility.guidRegex.IsMatch(value);
		}

		// Token: 0x06000985 RID: 2437 RVA: 0x00028CC8 File Offset: 0x00026EC8
		public static string PathEllipsis(string s, int maxLength)
		{
			string text = "...";
			if (s.Length < maxLength)
			{
				return s;
			}
			string fileName = Path.GetFileName(s);
			string directoryName = Path.GetDirectoryName(s);
			int num = maxLength - fileName.Length - text.Length;
			if (num > 0)
			{
				return directoryName.Substring(0, num) + text + Path.DirectorySeparatorChar.ToString() + fileName;
			}
			return text + Path.DirectorySeparatorChar.ToString() + fileName;
		}

		// Token: 0x06000986 RID: 2438 RVA: 0x00028D33 File Offset: 0x00026F33
		public static string ToHexString(this byte[] bytes)
		{
			return BitConverter.ToString(bytes).Replace("-", "");
		}

		// Token: 0x0400023E RID: 574
		private static readonly Regex guidRegex = new Regex("[a-fA-F0-9]{8}(\\-[a-fA-F0-9]{4}){3}\\-[a-fA-F0-9]{12}");
	}
}
