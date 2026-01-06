using System;
using System.Collections.Generic;

namespace System.Net.Http.Headers
{
	// Token: 0x02000037 RID: 55
	internal static class CollectionParser
	{
		// Token: 0x060001CB RID: 459 RVA: 0x000079E4 File Offset: 0x00005BE4
		public static bool TryParse<T>(string input, int minimalCount, ElementTryParser<T> parser, out List<T> result) where T : class
		{
			Lexer lexer = new Lexer(input);
			result = new List<T>();
			T t;
			Token token;
			while (parser(lexer, out t, out token))
			{
				if (t != null)
				{
					result.Add(t);
				}
				if (token != Token.Type.SeparatorComma)
				{
					if (token != Token.Type.End)
					{
						result = null;
						return false;
					}
					if (minimalCount > result.Count)
					{
						result = null;
						return false;
					}
					return true;
				}
			}
			return false;
		}

		// Token: 0x060001CC RID: 460 RVA: 0x00007A45 File Offset: 0x00005C45
		public static bool TryParse(string input, int minimalCount, out List<string> result)
		{
			return CollectionParser.TryParse<string>(input, minimalCount, new ElementTryParser<string>(CollectionParser.TryParseStringElement), out result);
		}

		// Token: 0x060001CD RID: 461 RVA: 0x00007A5B File Offset: 0x00005C5B
		public static bool TryParseRepetition(string input, int minimalCount, out List<string> result)
		{
			return CollectionParser.TryParseRepetition<string>(input, minimalCount, new ElementTryParser<string>(CollectionParser.TryParseStringElement), out result);
		}

		// Token: 0x060001CE RID: 462 RVA: 0x00007A74 File Offset: 0x00005C74
		private static bool TryParseStringElement(Lexer lexer, out string parsedValue, out Token t)
		{
			t = lexer.Scan(false);
			if (t == Token.Type.Token)
			{
				parsedValue = lexer.GetStringValue(t);
				if (parsedValue.Length == 0)
				{
					parsedValue = null;
				}
				t = lexer.Scan(false);
			}
			else
			{
				parsedValue = null;
			}
			return true;
		}

		// Token: 0x060001CF RID: 463 RVA: 0x00007ACC File Offset: 0x00005CCC
		public static bool TryParseRepetition<T>(string input, int minimalCount, ElementTryParser<T> parser, out List<T> result) where T : class
		{
			Lexer lexer = new Lexer(input);
			result = new List<T>();
			T t;
			Token token;
			while (parser(lexer, out t, out token))
			{
				if (t != null)
				{
					result.Add(t);
				}
				if (token == Token.Type.End)
				{
					return minimalCount <= result.Count;
				}
			}
			return false;
		}
	}
}
