using System;
using System.Collections.Generic;
using System.Globalization;

namespace System.Net.Http.Headers
{
	/// <summary>Represents a warning value used by the Warning header.</summary>
	// Token: 0x0200006B RID: 107
	public class WarningHeaderValue : ICloneable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.Headers.WarningHeaderValue" /> class.</summary>
		/// <param name="code">The specific warning code.</param>
		/// <param name="agent">The host that attached the warning.</param>
		/// <param name="text">A quoted-string containing the warning text.</param>
		// Token: 0x060003D9 RID: 985 RVA: 0x0000D3C3 File Offset: 0x0000B5C3
		public WarningHeaderValue(int code, string agent, string text)
		{
			if (!WarningHeaderValue.IsCodeValid(code))
			{
				throw new ArgumentOutOfRangeException("code");
			}
			Parser.Uri.Check(agent);
			Parser.Token.CheckQuotedString(text);
			this.Code = code;
			this.Agent = agent;
			this.Text = text;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.Headers.WarningHeaderValue" /> class.</summary>
		/// <param name="code">The specific warning code.</param>
		/// <param name="agent">The host that attached the warning.</param>
		/// <param name="text">A quoted-string containing the warning text.</param>
		/// <param name="date">The date/time stamp of the warning.</param>
		// Token: 0x060003DA RID: 986 RVA: 0x0000D3FF File Offset: 0x0000B5FF
		public WarningHeaderValue(int code, string agent, string text, DateTimeOffset date)
			: this(code, agent, text)
		{
			this.Date = new DateTimeOffset?(date);
		}

		// Token: 0x060003DB RID: 987 RVA: 0x00002300 File Offset: 0x00000500
		private WarningHeaderValue()
		{
		}

		/// <summary>Gets the host that attached the warning.</summary>
		/// <returns>Returns <see cref="T:System.String" />.The host that attached the warning.</returns>
		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x060003DC RID: 988 RVA: 0x0000D417 File Offset: 0x0000B617
		// (set) Token: 0x060003DD RID: 989 RVA: 0x0000D41F File Offset: 0x0000B61F
		public string Agent { get; private set; }

		/// <summary>Gets the specific warning code.</summary>
		/// <returns>Returns <see cref="T:System.Int32" />.The specific warning code.</returns>
		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x060003DE RID: 990 RVA: 0x0000D428 File Offset: 0x0000B628
		// (set) Token: 0x060003DF RID: 991 RVA: 0x0000D430 File Offset: 0x0000B630
		public int Code { get; private set; }

		/// <summary>Gets the date/time stamp of the warning.</summary>
		/// <returns>Returns <see cref="T:System.DateTimeOffset" />.The date/time stamp of the warning.</returns>
		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x060003E0 RID: 992 RVA: 0x0000D439 File Offset: 0x0000B639
		// (set) Token: 0x060003E1 RID: 993 RVA: 0x0000D441 File Offset: 0x0000B641
		public DateTimeOffset? Date { get; private set; }

		/// <summary>Gets a quoted-string containing the warning text.</summary>
		/// <returns>Returns <see cref="T:System.String" />.A quoted-string containing the warning text.</returns>
		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x060003E2 RID: 994 RVA: 0x0000D44A File Offset: 0x0000B64A
		// (set) Token: 0x060003E3 RID: 995 RVA: 0x0000D452 File Offset: 0x0000B652
		public string Text { get; private set; }

		// Token: 0x060003E4 RID: 996 RVA: 0x0000D45B File Offset: 0x0000B65B
		private static bool IsCodeValid(int code)
		{
			return code >= 0 && code < 1000;
		}

		/// <summary>Creates a new object that is a copy of the current <see cref="T:System.Net.Http.Headers.WarningHeaderValue" /> instance.</summary>
		/// <returns>Returns <see cref="T:System.Object" />.Returns a copy of the current instance.</returns>
		// Token: 0x060003E5 RID: 997 RVA: 0x000069EA File Offset: 0x00004BEA
		object ICloneable.Clone()
		{
			return base.MemberwiseClone();
		}

		/// <summary>Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Net.Http.Headers.WarningHeaderValue" /> object.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.true if the specified <see cref="T:System.Object" /> is equal to the current object; otherwise, false.</returns>
		/// <param name="obj">The object to compare with the current object.</param>
		// Token: 0x060003E6 RID: 998 RVA: 0x0000D46C File Offset: 0x0000B66C
		public override bool Equals(object obj)
		{
			WarningHeaderValue warningHeaderValue = obj as WarningHeaderValue;
			return warningHeaderValue != null && (this.Code == warningHeaderValue.Code && string.Equals(warningHeaderValue.Agent, this.Agent, StringComparison.OrdinalIgnoreCase) && this.Text == warningHeaderValue.Text) && this.Date == warningHeaderValue.Date;
		}

		/// <summary>Serves as a hash function for an <see cref="T:System.Net.Http.Headers.WarningHeaderValue" /> object.</summary>
		/// <returns>Returns <see cref="T:System.Int32" />.A hash code for the current object.</returns>
		// Token: 0x060003E7 RID: 999 RVA: 0x0000D4FC File Offset: 0x0000B6FC
		public override int GetHashCode()
		{
			return this.Code.GetHashCode() ^ this.Agent.ToLowerInvariant().GetHashCode() ^ this.Text.GetHashCode() ^ this.Date.GetHashCode();
		}

		/// <summary>Converts a string to an <see cref="T:System.Net.Http.Headers.WarningHeaderValue" /> instance.</summary>
		/// <returns>Returns an <see cref="T:System.Net.Http.Headers.WarningHeaderValue" /> instance.</returns>
		/// <param name="input">A string that represents authentication header value information.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is a null reference.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="input" /> is not valid authentication header value information.</exception>
		// Token: 0x060003E8 RID: 1000 RVA: 0x0000D54C File Offset: 0x0000B74C
		public static WarningHeaderValue Parse(string input)
		{
			WarningHeaderValue warningHeaderValue;
			if (WarningHeaderValue.TryParse(input, out warningHeaderValue))
			{
				return warningHeaderValue;
			}
			throw new FormatException(input);
		}

		/// <summary>Determines whether a string is valid <see cref="T:System.Net.Http.Headers.WarningHeaderValue" /> information.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.true if <paramref name="input" /> is valid <see cref="T:System.Net.Http.Headers.WarningHeaderValue" /> information; otherwise, false.</returns>
		/// <param name="input">The string to validate.</param>
		/// <param name="parsedValue">The <see cref="T:System.Net.Http.Headers.WarningHeaderValue" /> version of the string.</param>
		// Token: 0x060003E9 RID: 1001 RVA: 0x0000D56C File Offset: 0x0000B76C
		public static bool TryParse(string input, out WarningHeaderValue parsedValue)
		{
			Token token;
			if (WarningHeaderValue.TryParseElement(new Lexer(input), out parsedValue, out token) && token == Token.Type.End)
			{
				return true;
			}
			parsedValue = null;
			return false;
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x0000D598 File Offset: 0x0000B798
		internal static bool TryParse(string input, int minimalCount, out List<WarningHeaderValue> result)
		{
			return CollectionParser.TryParse<WarningHeaderValue>(input, minimalCount, new ElementTryParser<WarningHeaderValue>(WarningHeaderValue.TryParseElement), out result);
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x0000D5B0 File Offset: 0x0000B7B0
		private static bool TryParseElement(Lexer lexer, out WarningHeaderValue parsedValue, out Token t)
		{
			parsedValue = null;
			t = lexer.Scan(false);
			if (t != Token.Type.Token)
			{
				return false;
			}
			int num;
			if (!lexer.TryGetNumericValue(t, out num) || !WarningHeaderValue.IsCodeValid(num))
			{
				return false;
			}
			t = lexer.Scan(false);
			if (t != Token.Type.Token)
			{
				return false;
			}
			Token token = t;
			if (lexer.PeekChar() == 58)
			{
				lexer.EatChar();
				token = lexer.Scan(false);
				if (token != Token.Type.Token)
				{
					return false;
				}
			}
			WarningHeaderValue warningHeaderValue = new WarningHeaderValue();
			warningHeaderValue.Code = num;
			warningHeaderValue.Agent = lexer.GetStringValue(t, token);
			t = lexer.Scan(false);
			if (t != Token.Type.QuotedString)
			{
				return false;
			}
			warningHeaderValue.Text = lexer.GetStringValue(t);
			t = lexer.Scan(false);
			if (t == Token.Type.QuotedString)
			{
				DateTimeOffset dateTimeOffset;
				if (!lexer.TryGetDateValue(t, out dateTimeOffset))
				{
					return false;
				}
				warningHeaderValue.Date = new DateTimeOffset?(dateTimeOffset);
				t = lexer.Scan(false);
			}
			parsedValue = warningHeaderValue;
			return true;
		}

		/// <summary>Returns a string that represents the current <see cref="T:System.Net.Http.Headers.WarningHeaderValue" /> object.</summary>
		/// <returns>Returns <see cref="T:System.String" />.A string that represents the current object.</returns>
		// Token: 0x060003EC RID: 1004 RVA: 0x0000D6D8 File Offset: 0x0000B8D8
		public override string ToString()
		{
			string text = string.Concat(new string[]
			{
				this.Code.ToString("000"),
				" ",
				this.Agent,
				" ",
				this.Text
			});
			if (this.Date != null)
			{
				text = text + " \"" + this.Date.Value.ToString("r", CultureInfo.InvariantCulture) + "\"";
			}
			return text;
		}
	}
}
