using System;
using System.Collections.Generic;
using System.Linq;

namespace TwitchLib.Client.Models.Common
{
	// Token: 0x0200002A RID: 42
	public static class Helpers
	{
		// Token: 0x06000178 RID: 376 RVA: 0x00007A78 File Offset: 0x00005C78
		public static List<string> ParseQuotesAndNonQuotes(string message)
		{
			List<string> list = new List<string>();
			if (message == "")
			{
				return new List<string>();
			}
			bool flag = message.get_Chars(0) != '"';
			foreach (string text in message.Split(new char[] { '"' }))
			{
				if (!string.IsNullOrEmpty(text))
				{
					if (!flag)
					{
						list.Add(text);
						flag = true;
					}
					else if (text.Contains(" "))
					{
						foreach (string text2 in text.Split(new char[] { ' ' }))
						{
							if (!string.IsNullOrWhiteSpace(text2))
							{
								list.Add(text2);
								flag = false;
							}
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00007B3C File Offset: 0x00005D3C
		public static List<KeyValuePair<string, string>> ParseBadges(string badgesStr)
		{
			List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
			if (Enumerable.Contains<char>(badgesStr, '/'))
			{
				if (!badgesStr.Contains(","))
				{
					list.Add(new KeyValuePair<string, string>(badgesStr.Split(new char[] { '/' })[0], badgesStr.Split(new char[] { '/' })[1]));
				}
				else
				{
					foreach (string text in badgesStr.Split(new char[] { ',' }))
					{
						list.Add(new KeyValuePair<string, string>(text.Split(new char[] { '/' })[0], text.Split(new char[] { '/' })[1]));
					}
				}
			}
			return list;
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00007BF4 File Offset: 0x00005DF4
		public static string ParseToken(string token, string message)
		{
			string text = string.Empty;
			for (int i = message.IndexOf(token, 3); i > -1; i = message.IndexOf(token, i + token.Length, 3))
			{
				text = Enumerable.LastOrDefault<string>(new string(Enumerable.ToArray<char>(Enumerable.TakeWhile<char>(message.Substring(i), (char x) => x != ';' && x != ' '))).Split(new char[] { '=' }));
			}
			return text;
		}

		// Token: 0x0600017B RID: 379 RVA: 0x00007C73 File Offset: 0x00005E73
		public static bool ConvertToBool(string data)
		{
			return data == "1";
		}
	}
}
