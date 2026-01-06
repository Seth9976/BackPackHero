using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements.StyleSheets.Syntax
{
	// Token: 0x0200037E RID: 894
	internal class StyleSyntaxTokenizer
	{
		// Token: 0x170006C4 RID: 1732
		// (get) Token: 0x06001C56 RID: 7254 RVA: 0x000847E0 File Offset: 0x000829E0
		public StyleSyntaxToken current
		{
			get
			{
				bool flag = this.m_CurrentTokenIndex < 0 || this.m_CurrentTokenIndex >= this.m_Tokens.Count;
				StyleSyntaxToken styleSyntaxToken;
				if (flag)
				{
					styleSyntaxToken = new StyleSyntaxToken(StyleSyntaxTokenType.Unknown);
				}
				else
				{
					styleSyntaxToken = this.m_Tokens[this.m_CurrentTokenIndex];
				}
				return styleSyntaxToken;
			}
		}

		// Token: 0x06001C57 RID: 7255 RVA: 0x00084834 File Offset: 0x00082A34
		public StyleSyntaxToken MoveNext()
		{
			StyleSyntaxToken styleSyntaxToken = this.current;
			bool flag = styleSyntaxToken.type == StyleSyntaxTokenType.Unknown;
			StyleSyntaxToken styleSyntaxToken2;
			if (flag)
			{
				styleSyntaxToken2 = styleSyntaxToken;
			}
			else
			{
				this.m_CurrentTokenIndex++;
				styleSyntaxToken = this.current;
				bool flag2 = this.m_CurrentTokenIndex == this.m_Tokens.Count;
				if (flag2)
				{
					this.m_CurrentTokenIndex = -1;
				}
				styleSyntaxToken2 = styleSyntaxToken;
			}
			return styleSyntaxToken2;
		}

		// Token: 0x06001C58 RID: 7256 RVA: 0x00084894 File Offset: 0x00082A94
		public StyleSyntaxToken PeekNext()
		{
			int num = this.m_CurrentTokenIndex + 1;
			bool flag = this.m_CurrentTokenIndex < 0 || num >= this.m_Tokens.Count;
			StyleSyntaxToken styleSyntaxToken;
			if (flag)
			{
				styleSyntaxToken = new StyleSyntaxToken(StyleSyntaxTokenType.Unknown);
			}
			else
			{
				styleSyntaxToken = this.m_Tokens[num];
			}
			return styleSyntaxToken;
		}

		// Token: 0x06001C59 RID: 7257 RVA: 0x000848E8 File Offset: 0x00082AE8
		public void Tokenize(string syntax)
		{
			this.m_Tokens.Clear();
			this.m_CurrentTokenIndex = 0;
			syntax = syntax.Trim(new char[] { ' ' }).ToLower();
			int i = 0;
			while (i < syntax.Length)
			{
				char c = syntax.get_Chars(i);
				char c2 = c;
				char c3 = c2;
				if (c3 <= '?')
				{
					switch (c3)
					{
					case ' ':
						i = StyleSyntaxTokenizer.GlobCharacter(syntax, i, ' ');
						this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.Space));
						break;
					case '!':
						this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.ExclamationPoint));
						break;
					case '"':
					case '$':
					case '%':
					case '(':
					case ')':
						goto IL_02EA;
					case '#':
						this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.HashMark));
						break;
					case '&':
					{
						bool flag = !StyleSyntaxTokenizer.IsNextCharacter(syntax, i, '&');
						if (flag)
						{
							string text = ((i + 1 < syntax.Length) ? syntax.get_Chars(i + 1).ToString() : "EOF");
							Debug.LogAssertionFormat("Expected '&' got '{0}'", new object[] { text });
							this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.Unknown));
						}
						else
						{
							this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.DoubleAmpersand));
							i++;
						}
						break;
					}
					case '\'':
						this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.SingleQuote));
						break;
					case '*':
						this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.Asterisk));
						break;
					case '+':
						this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.Plus));
						break;
					case ',':
						this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.Comma));
						break;
					default:
						switch (c3)
						{
						case '<':
							this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.LessThan));
							break;
						case '=':
							goto IL_02EA;
						case '>':
							this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.GreaterThan));
							break;
						case '?':
							this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.QuestionMark));
							break;
						default:
							goto IL_02EA;
						}
						break;
					}
				}
				else if (c3 != '[')
				{
					if (c3 != ']')
					{
						switch (c3)
						{
						case '{':
							this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.OpenBrace));
							break;
						case '|':
						{
							bool flag2 = StyleSyntaxTokenizer.IsNextCharacter(syntax, i, '|');
							if (flag2)
							{
								this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.DoubleBar));
								i++;
							}
							else
							{
								this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.SingleBar));
							}
							break;
						}
						case '}':
							this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.CloseBrace));
							break;
						default:
							goto IL_02EA;
						}
					}
					else
					{
						this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.CloseBracket));
					}
				}
				else
				{
					this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.OpenBracket));
				}
				IL_03C5:
				i++;
				continue;
				IL_02EA:
				bool flag3 = char.IsNumber(c);
				if (flag3)
				{
					int num = i;
					int num2 = 1;
					while (StyleSyntaxTokenizer.IsNextNumber(syntax, i))
					{
						i++;
						num2++;
					}
					string text2 = syntax.Substring(num, num2);
					int num3 = int.Parse(text2);
					this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.Number, num3));
				}
				else
				{
					bool flag4 = char.IsLetter(c);
					if (flag4)
					{
						int num4 = i;
						int num5 = 1;
						while (StyleSyntaxTokenizer.IsNextLetterOrDash(syntax, i))
						{
							i++;
							num5++;
						}
						string text3 = syntax.Substring(num4, num5);
						this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.String, text3));
					}
					else
					{
						Debug.LogAssertionFormat("Expected letter or number got '{0}'", new object[] { c });
						this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.Unknown));
					}
				}
				goto IL_03C5;
			}
			this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.End));
		}

		// Token: 0x06001C5A RID: 7258 RVA: 0x00084CE4 File Offset: 0x00082EE4
		private static bool IsNextCharacter(string s, int index, char c)
		{
			return index + 1 < s.Length && s.get_Chars(index + 1) == c;
		}

		// Token: 0x06001C5B RID: 7259 RVA: 0x00084D10 File Offset: 0x00082F10
		private static bool IsNextLetterOrDash(string s, int index)
		{
			return index + 1 < s.Length && (char.IsLetter(s.get_Chars(index + 1)) || s.get_Chars(index + 1) == '-');
		}

		// Token: 0x06001C5C RID: 7260 RVA: 0x00084D50 File Offset: 0x00082F50
		private static bool IsNextNumber(string s, int index)
		{
			return index + 1 < s.Length && char.IsNumber(s.get_Chars(index + 1));
		}

		// Token: 0x06001C5D RID: 7261 RVA: 0x00084D80 File Offset: 0x00082F80
		private static int GlobCharacter(string s, int index, char c)
		{
			while (StyleSyntaxTokenizer.IsNextCharacter(s, index, c))
			{
				index++;
			}
			return index;
		}

		// Token: 0x04000E57 RID: 3671
		private List<StyleSyntaxToken> m_Tokens = new List<StyleSyntaxToken>();

		// Token: 0x04000E58 RID: 3672
		private int m_CurrentTokenIndex = -1;
	}
}
