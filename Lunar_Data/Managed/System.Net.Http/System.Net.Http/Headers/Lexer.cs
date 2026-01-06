using System;
using System.Globalization;

namespace System.Net.Http.Headers
{
	// Token: 0x0200004D RID: 77
	internal class Lexer
	{
		// Token: 0x060002E8 RID: 744 RVA: 0x0000A7DD File Offset: 0x000089DD
		public Lexer(string stream)
		{
			this.s = stream;
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060002E9 RID: 745 RVA: 0x0000A7EC File Offset: 0x000089EC
		// (set) Token: 0x060002EA RID: 746 RVA: 0x0000A7F4 File Offset: 0x000089F4
		public int Position
		{
			get
			{
				return this.pos;
			}
			set
			{
				this.pos = value;
			}
		}

		// Token: 0x060002EB RID: 747 RVA: 0x0000A7FD File Offset: 0x000089FD
		public string GetStringValue(Token token)
		{
			return this.s.Substring(token.StartPosition, token.EndPosition - token.StartPosition);
		}

		// Token: 0x060002EC RID: 748 RVA: 0x0000A820 File Offset: 0x00008A20
		public string GetStringValue(Token start, Token end)
		{
			return this.s.Substring(start.StartPosition, end.EndPosition - start.StartPosition);
		}

		// Token: 0x060002ED RID: 749 RVA: 0x0000A843 File Offset: 0x00008A43
		public string GetQuotedStringValue(Token start)
		{
			return this.s.Substring(start.StartPosition + 1, start.EndPosition - start.StartPosition - 2);
		}

		// Token: 0x060002EE RID: 750 RVA: 0x0000A86A File Offset: 0x00008A6A
		public string GetRemainingStringValue(int position)
		{
			if (position <= this.s.Length)
			{
				return this.s.Substring(position);
			}
			return null;
		}

		// Token: 0x060002EF RID: 751 RVA: 0x0000A888 File Offset: 0x00008A88
		public bool IsStarStringValue(Token token)
		{
			return token.EndPosition - token.StartPosition == 1 && this.s[token.StartPosition] == '*';
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x0000A8B4 File Offset: 0x00008AB4
		public bool TryGetNumericValue(Token token, out int value)
		{
			return int.TryParse(this.GetStringValue(token), NumberStyles.None, CultureInfo.InvariantCulture, out value);
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x0000A8C9 File Offset: 0x00008AC9
		public bool TryGetNumericValue(Token token, out long value)
		{
			return long.TryParse(this.GetStringValue(token), NumberStyles.None, CultureInfo.InvariantCulture, out value);
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x0000A8E0 File Offset: 0x00008AE0
		public TimeSpan? TryGetTimeSpanValue(Token token)
		{
			int num;
			if (this.TryGetNumericValue(token, out num))
			{
				return new TimeSpan?(TimeSpan.FromSeconds((double)num));
			}
			return null;
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x0000A90E File Offset: 0x00008B0E
		public bool TryGetDateValue(Token token, out DateTimeOffset value)
		{
			return Lexer.TryGetDateValue((token == Token.Type.QuotedString) ? this.s.Substring(token.StartPosition + 1, token.EndPosition - token.StartPosition - 2) : this.GetStringValue(token), out value);
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x0000A94D File Offset: 0x00008B4D
		public static bool TryGetDateValue(string text, out DateTimeOffset value)
		{
			return DateTimeOffset.TryParseExact(text, Lexer.dt_formats, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.AllowLeadingWhite | DateTimeStyles.AllowTrailingWhite | DateTimeStyles.AllowInnerWhite | DateTimeStyles.AssumeUniversal, out value);
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x0000A962 File Offset: 0x00008B62
		public bool TryGetDoubleValue(Token token, out double value)
		{
			return double.TryParse(this.GetStringValue(token), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out value);
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x0000A978 File Offset: 0x00008B78
		public static bool IsValidToken(string input)
		{
			int i;
			for (i = 0; i < input.Length; i++)
			{
				if (!Lexer.IsValidCharacter(input[i]))
				{
					return false;
				}
			}
			return i > 0;
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x0000A9AA File Offset: 0x00008BAA
		public static bool IsValidCharacter(char input)
		{
			return (int)input < Lexer.last_token_char && Lexer.token_chars[(int)input];
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x0000A9BD File Offset: 0x00008BBD
		public void EatChar()
		{
			this.pos++;
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x0000A9CD File Offset: 0x00008BCD
		public int PeekChar()
		{
			if (this.pos >= this.s.Length)
			{
				return -1;
			}
			return (int)this.s[this.pos];
		}

		// Token: 0x060002FA RID: 762 RVA: 0x0000A9F8 File Offset: 0x00008BF8
		public bool ScanCommentOptional(out string value)
		{
			Token token;
			return this.ScanCommentOptional(out value, out token) || token == Token.Type.End;
		}

		// Token: 0x060002FB RID: 763 RVA: 0x0000AA1C File Offset: 0x00008C1C
		public bool ScanCommentOptional(out string value, out Token readToken)
		{
			readToken = this.Scan(false);
			if (readToken != Token.Type.OpenParens)
			{
				value = null;
				return false;
			}
			int num = 1;
			while (this.pos < this.s.Length)
			{
				char c = this.s[this.pos];
				if (c == '(')
				{
					num++;
					this.pos++;
				}
				else if (c == ')')
				{
					this.pos++;
					if (--num <= 0)
					{
						int startPosition = readToken.StartPosition;
						value = this.s.Substring(startPosition, this.pos - startPosition);
						return true;
					}
				}
				else
				{
					if (c < ' ' || c > '~')
					{
						break;
					}
					this.pos++;
				}
			}
			value = null;
			return false;
		}

		// Token: 0x060002FC RID: 764 RVA: 0x0000AAE8 File Offset: 0x00008CE8
		public Token Scan(bool recognizeDash = false)
		{
			int num = this.pos;
			if (this.s == null)
			{
				return new Token(Token.Type.Error, 0, 0);
			}
			Token.Type type;
			if (this.pos >= this.s.Length)
			{
				type = Token.Type.End;
			}
			else
			{
				type = Token.Type.Error;
				char c;
				for (;;)
				{
					string text = this.s;
					int num2 = this.pos;
					this.pos = num2 + 1;
					c = text[num2];
					if (c > '"')
					{
						goto IL_006D;
					}
					if (c != '\t' && c != ' ')
					{
						break;
					}
					if (this.pos == this.s.Length)
					{
						goto Block_12;
					}
				}
				if (c != '"')
				{
					goto IL_0171;
				}
				num = this.pos - 1;
				while (this.pos < this.s.Length)
				{
					string text2 = this.s;
					int num2 = this.pos;
					this.pos = num2 + 1;
					c = text2[num2];
					if (c == '\\')
					{
						if (this.pos + 1 >= this.s.Length)
						{
							break;
						}
						this.pos++;
					}
					else if (c == '"')
					{
						type = Token.Type.QuotedString;
						break;
					}
				}
				goto IL_01D3;
				IL_006D:
				if (c <= '/')
				{
					if (c == '(')
					{
						num = this.pos - 1;
						type = Token.Type.OpenParens;
						goto IL_01D3;
					}
					switch (c)
					{
					case ',':
						type = Token.Type.SeparatorComma;
						goto IL_01D3;
					case '-':
						if (recognizeDash)
						{
							type = Token.Type.SeparatorDash;
							goto IL_01D3;
						}
						goto IL_0171;
					case '.':
						goto IL_0171;
					case '/':
						type = Token.Type.SeparatorSlash;
						goto IL_01D3;
					default:
						goto IL_0171;
					}
				}
				else
				{
					if (c == ';')
					{
						type = Token.Type.SeparatorSemicolon;
						goto IL_01D3;
					}
					if (c != '=')
					{
						goto IL_0171;
					}
					type = Token.Type.SeparatorEqual;
					goto IL_01D3;
				}
				Block_12:
				type = Token.Type.End;
				goto IL_01D3;
				IL_0171:
				if ((int)c < Lexer.last_token_char && Lexer.token_chars[(int)c])
				{
					num = this.pos - 1;
					type = Token.Type.Token;
					while (this.pos < this.s.Length)
					{
						c = this.s[this.pos];
						if ((int)c >= Lexer.last_token_char || !Lexer.token_chars[(int)c])
						{
							break;
						}
						this.pos++;
					}
				}
			}
			IL_01D3:
			return new Token(type, num, this.pos);
		}

		// Token: 0x0400012E RID: 302
		private static readonly bool[] token_chars = new bool[]
		{
			false, false, false, false, false, false, false, false, false, false,
			false, false, false, false, false, false, false, false, false, false,
			false, false, false, false, false, false, false, false, false, false,
			false, false, false, true, false, true, true, true, true, true,
			false, false, true, true, false, true, true, false, true, true,
			true, true, true, true, true, true, true, true, false, false,
			false, false, false, false, false, true, true, true, true, true,
			true, true, true, true, true, true, true, true, true, true,
			true, true, true, true, true, true, true, true, true, true,
			true, false, false, false, true, true, true, true, true, true,
			true, true, true, true, true, true, true, true, true, true,
			true, true, true, true, true, true, true, true, true, true,
			true, true, true, false, true, false, true
		};

		// Token: 0x0400012F RID: 303
		private static readonly int last_token_char = Lexer.token_chars.Length;

		// Token: 0x04000130 RID: 304
		private static readonly string[] dt_formats = new string[] { "r", "dddd, dd'-'MMM'-'yy HH:mm:ss 'GMT'", "ddd MMM d HH:mm:ss yyyy", "d MMM yy H:m:s", "ddd, d MMM yyyy H:m:s zzz" };

		// Token: 0x04000131 RID: 305
		private readonly string s;

		// Token: 0x04000132 RID: 306
		private int pos;
	}
}
