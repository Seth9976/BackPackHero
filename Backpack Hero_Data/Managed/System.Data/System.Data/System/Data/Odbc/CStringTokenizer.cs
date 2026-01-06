using System;
using System.Text;

namespace System.Data.Odbc
{
	// Token: 0x020002AE RID: 686
	internal sealed class CStringTokenizer
	{
		// Token: 0x06001E12 RID: 7698 RVA: 0x00092CF0 File Offset: 0x00090EF0
		internal CStringTokenizer(string text, char quote, char escape)
		{
			this._token = new StringBuilder();
			this._quote = quote;
			this._escape = escape;
			this._sqlstatement = text;
			if (text != null)
			{
				int num = text.IndexOf('\0');
				this._len = ((0 > num) ? text.Length : num);
				return;
			}
			this._len = 0;
		}

		// Token: 0x17000571 RID: 1393
		// (get) Token: 0x06001E13 RID: 7699 RVA: 0x00092D49 File Offset: 0x00090F49
		internal int CurrentPosition
		{
			get
			{
				return this._idx;
			}
		}

		// Token: 0x06001E14 RID: 7700 RVA: 0x00092D54 File Offset: 0x00090F54
		internal string NextToken()
		{
			if (this._token.Length != 0)
			{
				this._idx += this._token.Length;
				this._token.Remove(0, this._token.Length);
			}
			while (this._idx < this._len && char.IsWhiteSpace(this._sqlstatement[this._idx]))
			{
				this._idx++;
			}
			if (this._idx == this._len)
			{
				return string.Empty;
			}
			int i = this._idx;
			bool flag = false;
			while (!flag && i < this._len)
			{
				if (this.IsValidNameChar(this._sqlstatement[i]))
				{
					while (i < this._len)
					{
						if (!this.IsValidNameChar(this._sqlstatement[i]))
						{
							break;
						}
						this._token.Append(this._sqlstatement[i]);
						i++;
					}
				}
				else
				{
					char c = this._sqlstatement[i];
					if (c == '[')
					{
						i = this.GetTokenFromBracket(i);
					}
					else
					{
						if (' ' == this._quote || c != this._quote)
						{
							if (!char.IsWhiteSpace(c))
							{
								if (c == ',')
								{
									if (i == this._idx)
									{
										this._token.Append(c);
									}
								}
								else
								{
									this._token.Append(c);
								}
							}
							break;
						}
						i = this.GetTokenFromQuote(i);
					}
				}
			}
			if (this._token.Length <= 0)
			{
				return string.Empty;
			}
			return this._token.ToString();
		}

		// Token: 0x06001E15 RID: 7701 RVA: 0x00092EE7 File Offset: 0x000910E7
		private int GetTokenFromBracket(int curidx)
		{
			while (curidx < this._len)
			{
				this._token.Append(this._sqlstatement[curidx]);
				curidx++;
				if (this._sqlstatement[curidx - 1] == ']')
				{
					break;
				}
			}
			return curidx;
		}

		// Token: 0x06001E16 RID: 7702 RVA: 0x00092F24 File Offset: 0x00091124
		private int GetTokenFromQuote(int curidx)
		{
			int i;
			for (i = curidx; i < this._len; i++)
			{
				this._token.Append(this._sqlstatement[i]);
				if (this._sqlstatement[i] == this._quote && i > curidx && this._sqlstatement[i - 1] != this._escape && i + 1 < this._len && this._sqlstatement[i + 1] != this._quote)
				{
					return i + 1;
				}
			}
			return i;
		}

		// Token: 0x06001E17 RID: 7703 RVA: 0x00092FB0 File Offset: 0x000911B0
		private bool IsValidNameChar(char ch)
		{
			return char.IsLetterOrDigit(ch) || ch == '_' || ch == '-' || ch == '.' || ch == '$' || ch == '#' || ch == '@' || ch == '~' || ch == '`' || ch == '%' || ch == '^' || ch == '&' || ch == '|';
		}

		// Token: 0x06001E18 RID: 7704 RVA: 0x00093004 File Offset: 0x00091204
		internal int FindTokenIndex(string tokenString)
		{
			string text;
			do
			{
				text = this.NextToken();
				if (this._idx == this._len || string.IsNullOrEmpty(text))
				{
					return -1;
				}
			}
			while (string.Compare(tokenString, text, StringComparison.OrdinalIgnoreCase) != 0);
			return this._idx;
		}

		// Token: 0x06001E19 RID: 7705 RVA: 0x00093040 File Offset: 0x00091240
		internal bool StartsWith(string tokenString)
		{
			int num = 0;
			while (num < this._len && char.IsWhiteSpace(this._sqlstatement[num]))
			{
				num++;
			}
			if (this._len - num < tokenString.Length)
			{
				return false;
			}
			if (string.Compare(this._sqlstatement, num, tokenString, 0, tokenString.Length, StringComparison.OrdinalIgnoreCase) == 0)
			{
				this._idx = 0;
				this.NextToken();
				return true;
			}
			return false;
		}

		// Token: 0x0400160B RID: 5643
		private readonly StringBuilder _token;

		// Token: 0x0400160C RID: 5644
		private readonly string _sqlstatement;

		// Token: 0x0400160D RID: 5645
		private readonly char _quote;

		// Token: 0x0400160E RID: 5646
		private readonly char _escape;

		// Token: 0x0400160F RID: 5647
		private int _len;

		// Token: 0x04001610 RID: 5648
		private int _idx;
	}
}
