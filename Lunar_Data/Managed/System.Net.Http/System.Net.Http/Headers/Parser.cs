using System;
using System.Globalization;
using System.Net.Mail;

namespace System.Net.Http.Headers
{
	// Token: 0x02000053 RID: 83
	internal static class Parser
	{
		// Token: 0x02000054 RID: 84
		public static class Token
		{
			// Token: 0x06000338 RID: 824 RVA: 0x0000B79A File Offset: 0x0000999A
			public static bool TryParse(string input, out string result)
			{
				if (input != null && Lexer.IsValidToken(input))
				{
					result = input;
					return true;
				}
				result = null;
				return false;
			}

			// Token: 0x06000339 RID: 825 RVA: 0x0000B7B0 File Offset: 0x000099B0
			public static void Check(string s)
			{
				if (s == null)
				{
					throw new ArgumentNullException();
				}
				if (Lexer.IsValidToken(s))
				{
					return;
				}
				if (s.Length == 0)
				{
					throw new ArgumentException();
				}
				throw new FormatException(s);
			}

			// Token: 0x0600033A RID: 826 RVA: 0x0000B7D8 File Offset: 0x000099D8
			public static bool TryCheck(string s)
			{
				return s != null && Lexer.IsValidToken(s);
			}

			// Token: 0x0600033B RID: 827 RVA: 0x0000B7E8 File Offset: 0x000099E8
			public static void CheckQuotedString(string s)
			{
				if (s == null)
				{
					throw new ArgumentNullException();
				}
				Lexer lexer = new Lexer(s);
				if (lexer.Scan(false) == global::System.Net.Http.Headers.Token.Type.QuotedString && lexer.Scan(false) == global::System.Net.Http.Headers.Token.Type.End)
				{
					return;
				}
				if (s.Length == 0)
				{
					throw new ArgumentException();
				}
				throw new FormatException(s);
			}

			// Token: 0x0600033C RID: 828 RVA: 0x0000B838 File Offset: 0x00009A38
			public static void CheckComment(string s)
			{
				if (s == null)
				{
					throw new ArgumentNullException();
				}
				string text;
				if (new Lexer(s).ScanCommentOptional(out text))
				{
					return;
				}
				if (s.Length == 0)
				{
					throw new ArgumentException();
				}
				throw new FormatException(s);
			}
		}

		// Token: 0x02000055 RID: 85
		public static class DateTime
		{
			// Token: 0x0600033D RID: 829 RVA: 0x0000B872 File Offset: 0x00009A72
			public static bool TryParse(string input, out DateTimeOffset result)
			{
				return Lexer.TryGetDateValue(input, out result);
			}

			// Token: 0x0400013A RID: 314
			public new static readonly Func<object, string> ToString = (object l) => ((DateTimeOffset)l).ToString("r", CultureInfo.InvariantCulture);
		}

		// Token: 0x02000057 RID: 87
		public static class EmailAddress
		{
			// Token: 0x06000342 RID: 834 RVA: 0x0000B8C8 File Offset: 0x00009AC8
			public static bool TryParse(string input, out string result)
			{
				bool flag;
				try
				{
					new MailAddress(input);
					result = input;
					flag = true;
				}
				catch
				{
					result = null;
					flag = false;
				}
				return flag;
			}
		}

		// Token: 0x02000058 RID: 88
		public static class Host
		{
			// Token: 0x06000343 RID: 835 RVA: 0x0000B8FC File Offset: 0x00009AFC
			public static bool TryParse(string input, out string result)
			{
				result = input;
				global::System.Uri uri;
				return global::System.Uri.TryCreate("http://u@" + input + "/", UriKind.Absolute, out uri);
			}
		}

		// Token: 0x02000059 RID: 89
		public static class Int
		{
			// Token: 0x06000344 RID: 836 RVA: 0x0000B924 File Offset: 0x00009B24
			public static bool TryParse(string input, out int result)
			{
				return int.TryParse(input, NumberStyles.None, CultureInfo.InvariantCulture, out result);
			}
		}

		// Token: 0x0200005A RID: 90
		public static class Long
		{
			// Token: 0x06000345 RID: 837 RVA: 0x0000B933 File Offset: 0x00009B33
			public static bool TryParse(string input, out long result)
			{
				return long.TryParse(input, NumberStyles.None, CultureInfo.InvariantCulture, out result);
			}
		}

		// Token: 0x0200005B RID: 91
		public static class MD5
		{
			// Token: 0x06000346 RID: 838 RVA: 0x0000B944 File Offset: 0x00009B44
			public static bool TryParse(string input, out byte[] result)
			{
				bool flag;
				try
				{
					result = Convert.FromBase64String(input);
					flag = true;
				}
				catch
				{
					result = null;
					flag = false;
				}
				return flag;
			}

			// Token: 0x0400013C RID: 316
			public new static readonly Func<object, string> ToString = (object l) => Convert.ToBase64String((byte[])l);
		}

		// Token: 0x0200005D RID: 93
		public static class TimeSpanSeconds
		{
			// Token: 0x0600034B RID: 843 RVA: 0x0000B9A8 File Offset: 0x00009BA8
			public static bool TryParse(string input, out TimeSpan result)
			{
				int num;
				if (Parser.Int.TryParse(input, out num))
				{
					result = TimeSpan.FromSeconds((double)num);
					return true;
				}
				result = TimeSpan.Zero;
				return false;
			}
		}

		// Token: 0x0200005E RID: 94
		public static class Uri
		{
			// Token: 0x0600034C RID: 844 RVA: 0x0000B9DA File Offset: 0x00009BDA
			public static bool TryParse(string input, out global::System.Uri result)
			{
				return global::System.Uri.TryCreate(input, UriKind.RelativeOrAbsolute, out result);
			}

			// Token: 0x0600034D RID: 845 RVA: 0x0000B9E4 File Offset: 0x00009BE4
			public static void Check(string s)
			{
				if (s == null)
				{
					throw new ArgumentNullException();
				}
				global::System.Uri uri;
				if (Parser.Uri.TryParse(s, out uri))
				{
					return;
				}
				if (s.Length == 0)
				{
					throw new ArgumentException();
				}
				throw new FormatException(s);
			}
		}
	}
}
