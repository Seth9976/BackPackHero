using System;

namespace System.Net
{
	// Token: 0x02000375 RID: 885
	internal static class HttpValidationHelpers
	{
		// Token: 0x06001D13 RID: 7443 RVA: 0x00069AC4 File Offset: 0x00067CC4
		internal static string CheckBadHeaderNameChars(string name)
		{
			if (HttpValidationHelpers.IsInvalidMethodOrHeaderString(name))
			{
				throw new ArgumentException("Specified value has invalid HTTP Header characters.", "name");
			}
			if (HttpValidationHelpers.ContainsNonAsciiChars(name))
			{
				throw new ArgumentException("Specified value has invalid HTTP Header characters.", "name");
			}
			return name;
		}

		// Token: 0x06001D14 RID: 7444 RVA: 0x00069AF8 File Offset: 0x00067CF8
		internal static bool ContainsNonAsciiChars(string token)
		{
			for (int i = 0; i < token.Length; i++)
			{
				if (token[i] < ' ' || token[i] > '~')
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001D15 RID: 7445 RVA: 0x00069B2F File Offset: 0x00067D2F
		internal static bool IsValidToken(string token)
		{
			return token.Length > 0 && !HttpValidationHelpers.IsInvalidMethodOrHeaderString(token) && !HttpValidationHelpers.ContainsNonAsciiChars(token);
		}

		// Token: 0x06001D16 RID: 7446 RVA: 0x00069B50 File Offset: 0x00067D50
		public static string CheckBadHeaderValueChars(string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				return string.Empty;
			}
			value = value.Trim(HttpValidationHelpers.s_httpTrimCharacters);
			int num = 0;
			for (int i = 0; i < value.Length; i++)
			{
				char c = 'ÿ' & value[i];
				switch (num)
				{
				case 0:
					if (c == '\r')
					{
						num = 1;
					}
					else if (c == '\n')
					{
						num = 2;
					}
					else if (c == '\u007f' || (c < ' ' && c != '\t'))
					{
						throw new ArgumentException("Specified value has invalid Control characters.", "value");
					}
					break;
				case 1:
					if (c != '\n')
					{
						throw new ArgumentException("Specified value has invalid CRLF characters.", "value");
					}
					num = 2;
					break;
				case 2:
					if (c != ' ' && c != '\t')
					{
						throw new ArgumentException("Specified value has invalid Control characters.", "value");
					}
					num = 0;
					break;
				}
			}
			if (num != 0)
			{
				throw new ArgumentException("Specified value has invalid CRLF characters.", "value");
			}
			return value;
		}

		// Token: 0x06001D17 RID: 7447 RVA: 0x00069C30 File Offset: 0x00067E30
		public static bool IsInvalidMethodOrHeaderString(string stringValue)
		{
			foreach (char c in stringValue)
			{
				if (c <= '/')
				{
					if (c <= ' ')
					{
						switch (c)
						{
						case '\t':
						case '\n':
						case '\r':
							return true;
						case '\v':
						case '\f':
							break;
						default:
							if (c == ' ')
							{
								return true;
							}
							break;
						}
					}
					else
					{
						if (c == '"')
						{
							return true;
						}
						switch (c)
						{
						case '\'':
						case '(':
						case ')':
						case ',':
						case '/':
							return true;
						}
					}
				}
				else if (c <= ']')
				{
					switch (c)
					{
					case ':':
					case ';':
					case '<':
					case '=':
					case '>':
					case '?':
					case '@':
						return true;
					default:
						switch (c)
						{
						case '[':
						case '\\':
						case ']':
							return true;
						}
						break;
					}
				}
				else if (c == '{' || c == '}')
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x04000EE2 RID: 3810
		private static readonly char[] s_httpTrimCharacters = new char[] { '\t', '\n', '\v', '\f', '\r', ' ' };
	}
}
