using System;
using System.Collections.Generic;

namespace System.Net.Http.Headers
{
	/// <summary>Represents a name/value pair used in various headers as defined in RFC 2616.</summary>
	// Token: 0x02000051 RID: 81
	public class NameValueHeaderValue : ICloneable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.Headers.NameValueHeaderValue" /> class.</summary>
		/// <param name="name">The header name.</param>
		// Token: 0x06000319 RID: 793 RVA: 0x0000B1B3 File Offset: 0x000093B3
		public NameValueHeaderValue(string name)
			: this(name, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.Headers.NameValueHeaderValue" /> class.</summary>
		/// <param name="name">The header name.</param>
		/// <param name="value">The header value.</param>
		// Token: 0x0600031A RID: 794 RVA: 0x0000B1BD File Offset: 0x000093BD
		public NameValueHeaderValue(string name, string value)
		{
			Parser.Token.Check(name);
			this.Name = name;
			this.Value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.Headers.NameValueHeaderValue" /> class.</summary>
		/// <param name="source">A <see cref="T:System.Net.Http.Headers.NameValueHeaderValue" /> object used to initialize the new instance.</param>
		// Token: 0x0600031B RID: 795 RVA: 0x0000B1D9 File Offset: 0x000093D9
		protected internal NameValueHeaderValue(NameValueHeaderValue source)
		{
			this.Name = source.Name;
			this.value = source.value;
		}

		// Token: 0x0600031C RID: 796 RVA: 0x00002300 File Offset: 0x00000500
		internal NameValueHeaderValue()
		{
		}

		/// <summary>Gets the header name.</summary>
		/// <returns>Returns <see cref="T:System.String" />.The header name.</returns>
		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x0600031D RID: 797 RVA: 0x0000B1F9 File Offset: 0x000093F9
		// (set) Token: 0x0600031E RID: 798 RVA: 0x0000B201 File Offset: 0x00009401
		public string Name { get; internal set; }

		/// <summary>Gets the header value.</summary>
		/// <returns>Returns <see cref="T:System.String" />.The header value.</returns>
		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x0600031F RID: 799 RVA: 0x0000B20A File Offset: 0x0000940A
		// (set) Token: 0x06000320 RID: 800 RVA: 0x0000B214 File Offset: 0x00009414
		public string Value
		{
			get
			{
				return this.value;
			}
			set
			{
				if (!string.IsNullOrEmpty(value))
				{
					Lexer lexer = new Lexer(value);
					Token token = lexer.Scan(false);
					if (lexer.Scan(false) != Token.Type.End || (token != Token.Type.Token && token != Token.Type.QuotedString))
					{
						throw new FormatException();
					}
					value = lexer.GetStringValue(token);
				}
				this.value = value;
			}
		}

		// Token: 0x06000321 RID: 801 RVA: 0x0000B26D File Offset: 0x0000946D
		internal static NameValueHeaderValue Create(string name, string value)
		{
			return new NameValueHeaderValue
			{
				Name = name,
				value = value
			};
		}

		/// <summary>Creates a new object that is a copy of the current <see cref="T:System.Net.Http.Headers.NameValueHeaderValue" /> instance.</summary>
		/// <returns>Returns <see cref="T:System.Object" />.A copy of the current instance.</returns>
		// Token: 0x06000322 RID: 802 RVA: 0x0000B282 File Offset: 0x00009482
		object ICloneable.Clone()
		{
			return new NameValueHeaderValue(this);
		}

		/// <summary>Serves as a hash function for an <see cref="T:System.Net.Http.Headers.NameValueHeaderValue" /> object.</summary>
		/// <returns>Returns <see cref="T:System.Int32" />.A hash code for the current object.</returns>
		// Token: 0x06000323 RID: 803 RVA: 0x0000B28C File Offset: 0x0000948C
		public override int GetHashCode()
		{
			int num = this.Name.ToLowerInvariant().GetHashCode();
			if (!string.IsNullOrEmpty(this.value))
			{
				num ^= this.value.ToLowerInvariant().GetHashCode();
			}
			return num;
		}

		/// <summary>Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Net.Http.Headers.NameValueHeaderValue" /> object.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.true if the specified <see cref="T:System.Object" /> is equal to the current object; otherwise, false.</returns>
		/// <param name="obj">The object to compare with the current object.</param>
		// Token: 0x06000324 RID: 804 RVA: 0x0000B2CC File Offset: 0x000094CC
		public override bool Equals(object obj)
		{
			NameValueHeaderValue nameValueHeaderValue = obj as NameValueHeaderValue;
			if (nameValueHeaderValue == null || !string.Equals(nameValueHeaderValue.Name, this.Name, StringComparison.OrdinalIgnoreCase))
			{
				return false;
			}
			if (string.IsNullOrEmpty(this.value))
			{
				return string.IsNullOrEmpty(nameValueHeaderValue.value);
			}
			return string.Equals(nameValueHeaderValue.value, this.value, StringComparison.OrdinalIgnoreCase);
		}

		/// <summary>Converts a string to an <see cref="T:System.Net.Http.Headers.NameValueHeaderValue" /> instance.</summary>
		/// <returns>Returns <see cref="T:System.Net.Http.Headers.NameValueHeaderValue" />.An <see cref="T:System.Net.Http.Headers.NameValueHeaderValue" /> instance.</returns>
		/// <param name="input">A string that represents name value header value information.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is a null reference.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="input" /> is not valid name value header value information.</exception>
		// Token: 0x06000325 RID: 805 RVA: 0x0000B324 File Offset: 0x00009524
		public static NameValueHeaderValue Parse(string input)
		{
			NameValueHeaderValue nameValueHeaderValue;
			if (NameValueHeaderValue.TryParse(input, out nameValueHeaderValue))
			{
				return nameValueHeaderValue;
			}
			throw new FormatException(input);
		}

		// Token: 0x06000326 RID: 806 RVA: 0x0000B343 File Offset: 0x00009543
		internal static bool TryParsePragma(string input, int minimalCount, out List<NameValueHeaderValue> result)
		{
			return CollectionParser.TryParse<NameValueHeaderValue>(input, minimalCount, new ElementTryParser<NameValueHeaderValue>(NameValueHeaderValue.TryParseElement), out result);
		}

		// Token: 0x06000327 RID: 807 RVA: 0x0000B35C File Offset: 0x0000955C
		internal static bool TryParseParameters(Lexer lexer, out List<NameValueHeaderValue> result, out Token t)
		{
			List<NameValueHeaderValue> list = new List<NameValueHeaderValue>();
			result = null;
			for (;;)
			{
				Token token = lexer.Scan(false);
				if (token != Token.Type.Token)
				{
					break;
				}
				string text = null;
				t = lexer.Scan(false);
				if (t == Token.Type.SeparatorEqual)
				{
					t = lexer.Scan(false);
					if (t != Token.Type.Token && t != Token.Type.QuotedString)
					{
						return false;
					}
					text = lexer.GetStringValue(t);
					t = lexer.Scan(false);
				}
				list.Add(new NameValueHeaderValue
				{
					Name = lexer.GetStringValue(token),
					value = text
				});
				if (t != Token.Type.SeparatorSemicolon)
				{
					goto Block_5;
				}
			}
			t = Token.Empty;
			return false;
			Block_5:
			result = list;
			return true;
		}

		/// <summary>Returns a string that represents the current <see cref="T:System.Net.Http.Headers.NameValueHeaderValue" /> object.</summary>
		/// <returns>Returns <see cref="T:System.String" />.A string that represents the current object.</returns>
		// Token: 0x06000328 RID: 808 RVA: 0x0000B426 File Offset: 0x00009626
		public override string ToString()
		{
			if (string.IsNullOrEmpty(this.value))
			{
				return this.Name;
			}
			return this.Name + "=" + this.value;
		}

		/// <summary>Determines whether a string is valid <see cref="T:System.Net.Http.Headers.NameValueHeaderValue" /> information.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.true if <paramref name="input" /> is valid <see cref="T:System.Net.Http.Headers.NameValueHeaderValue" /> information; otherwise, false.</returns>
		/// <param name="input">The string to validate.</param>
		/// <param name="parsedValue">The <see cref="T:System.Net.Http.Headers.NameValueHeaderValue" /> version of the string.</param>
		// Token: 0x06000329 RID: 809 RVA: 0x0000B454 File Offset: 0x00009654
		public static bool TryParse(string input, out NameValueHeaderValue parsedValue)
		{
			Token token;
			if (NameValueHeaderValue.TryParseElement(new Lexer(input), out parsedValue, out token) && token == Token.Type.End)
			{
				return true;
			}
			parsedValue = null;
			return false;
		}

		// Token: 0x0600032A RID: 810 RVA: 0x0000B480 File Offset: 0x00009680
		private static bool TryParseElement(Lexer lexer, out NameValueHeaderValue parsedValue, out Token t)
		{
			parsedValue = null;
			t = lexer.Scan(false);
			if (t != Token.Type.Token)
			{
				return false;
			}
			parsedValue = new NameValueHeaderValue
			{
				Name = lexer.GetStringValue(t)
			};
			t = lexer.Scan(false);
			if (t == Token.Type.SeparatorEqual)
			{
				t = lexer.Scan(false);
				if (t != Token.Type.Token && t != Token.Type.QuotedString)
				{
					return false;
				}
				parsedValue.value = lexer.GetStringValue(t);
				t = lexer.Scan(false);
			}
			return true;
		}

		// Token: 0x04000137 RID: 311
		internal string value;
	}
}
