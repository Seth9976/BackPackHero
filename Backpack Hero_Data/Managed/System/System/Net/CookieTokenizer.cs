using System;

namespace System.Net
{
	// Token: 0x0200045C RID: 1116
	internal class CookieTokenizer
	{
		// Token: 0x06002330 RID: 9008 RVA: 0x00081348 File Offset: 0x0007F548
		internal CookieTokenizer(string tokenStream)
		{
			this.m_length = tokenStream.Length;
			this.m_tokenStream = tokenStream;
		}

		// Token: 0x17000702 RID: 1794
		// (get) Token: 0x06002331 RID: 9009 RVA: 0x00081363 File Offset: 0x0007F563
		// (set) Token: 0x06002332 RID: 9010 RVA: 0x0008136B File Offset: 0x0007F56B
		internal bool EndOfCookie
		{
			get
			{
				return this.m_eofCookie;
			}
			set
			{
				this.m_eofCookie = value;
			}
		}

		// Token: 0x17000703 RID: 1795
		// (get) Token: 0x06002333 RID: 9011 RVA: 0x00081374 File Offset: 0x0007F574
		internal bool Eof
		{
			get
			{
				return this.m_index >= this.m_length;
			}
		}

		// Token: 0x17000704 RID: 1796
		// (get) Token: 0x06002334 RID: 9012 RVA: 0x00081387 File Offset: 0x0007F587
		// (set) Token: 0x06002335 RID: 9013 RVA: 0x0008138F File Offset: 0x0007F58F
		internal string Name
		{
			get
			{
				return this.m_name;
			}
			set
			{
				this.m_name = value;
			}
		}

		// Token: 0x17000705 RID: 1797
		// (get) Token: 0x06002336 RID: 9014 RVA: 0x00081398 File Offset: 0x0007F598
		// (set) Token: 0x06002337 RID: 9015 RVA: 0x000813A0 File Offset: 0x0007F5A0
		internal bool Quoted
		{
			get
			{
				return this.m_quoted;
			}
			set
			{
				this.m_quoted = value;
			}
		}

		// Token: 0x17000706 RID: 1798
		// (get) Token: 0x06002338 RID: 9016 RVA: 0x000813A9 File Offset: 0x0007F5A9
		// (set) Token: 0x06002339 RID: 9017 RVA: 0x000813B1 File Offset: 0x0007F5B1
		internal CookieToken Token
		{
			get
			{
				return this.m_token;
			}
			set
			{
				this.m_token = value;
			}
		}

		// Token: 0x17000707 RID: 1799
		// (get) Token: 0x0600233A RID: 9018 RVA: 0x000813BA File Offset: 0x0007F5BA
		// (set) Token: 0x0600233B RID: 9019 RVA: 0x000813C2 File Offset: 0x0007F5C2
		internal string Value
		{
			get
			{
				return this.m_value;
			}
			set
			{
				this.m_value = value;
			}
		}

		// Token: 0x0600233C RID: 9020 RVA: 0x000813CC File Offset: 0x0007F5CC
		internal string Extract()
		{
			string text = string.Empty;
			if (this.m_tokenLength != 0)
			{
				text = this.m_tokenStream.Substring(this.m_start, this.m_tokenLength);
				if (!this.Quoted)
				{
					text = text.Trim();
				}
			}
			return text;
		}

		// Token: 0x0600233D RID: 9021 RVA: 0x00081410 File Offset: 0x0007F610
		internal CookieToken FindNext(bool ignoreComma, bool ignoreEquals)
		{
			this.m_tokenLength = 0;
			this.m_start = this.m_index;
			while (this.m_index < this.m_length && char.IsWhiteSpace(this.m_tokenStream[this.m_index]))
			{
				this.m_index++;
				this.m_start++;
			}
			CookieToken cookieToken = CookieToken.End;
			int num = 1;
			if (!this.Eof)
			{
				if (this.m_tokenStream[this.m_index] == '"')
				{
					this.Quoted = true;
					this.m_index++;
					bool flag = false;
					while (this.m_index < this.m_length)
					{
						char c = this.m_tokenStream[this.m_index];
						if (!flag && c == '"')
						{
							break;
						}
						if (flag)
						{
							flag = false;
						}
						else if (c == '\\')
						{
							flag = true;
						}
						this.m_index++;
					}
					if (this.m_index < this.m_length)
					{
						this.m_index++;
					}
					this.m_tokenLength = this.m_index - this.m_start;
					num = 0;
					ignoreComma = false;
				}
				while (this.m_index < this.m_length && this.m_tokenStream[this.m_index] != ';' && (ignoreEquals || this.m_tokenStream[this.m_index] != '=') && (ignoreComma || this.m_tokenStream[this.m_index] != ','))
				{
					if (this.m_tokenStream[this.m_index] == ',')
					{
						this.m_start = this.m_index + 1;
						this.m_tokenLength = -1;
						ignoreComma = false;
					}
					this.m_index++;
					this.m_tokenLength += num;
				}
				if (!this.Eof)
				{
					char c2 = this.m_tokenStream[this.m_index];
					if (c2 != ';')
					{
						if (c2 != '=')
						{
							cookieToken = CookieToken.EndCookie;
						}
						else
						{
							cookieToken = CookieToken.Equals;
						}
					}
					else
					{
						cookieToken = CookieToken.EndToken;
					}
					this.m_index++;
				}
			}
			return cookieToken;
		}

		// Token: 0x0600233E RID: 9022 RVA: 0x00081614 File Offset: 0x0007F814
		internal CookieToken Next(bool first, bool parseResponseCookies)
		{
			this.Reset();
			CookieToken cookieToken = this.FindNext(false, false);
			if (cookieToken == CookieToken.EndCookie)
			{
				this.EndOfCookie = true;
			}
			if (cookieToken == CookieToken.End || cookieToken == CookieToken.EndCookie)
			{
				if ((this.Name = this.Extract()).Length != 0)
				{
					this.Token = this.TokenFromName(parseResponseCookies);
					return CookieToken.Attribute;
				}
				return cookieToken;
			}
			else
			{
				this.Name = this.Extract();
				if (first)
				{
					this.Token = CookieToken.CookieName;
				}
				else
				{
					this.Token = this.TokenFromName(parseResponseCookies);
				}
				if (cookieToken == CookieToken.Equals)
				{
					cookieToken = this.FindNext(!first && this.Token == CookieToken.Expires, true);
					if (cookieToken == CookieToken.EndCookie)
					{
						this.EndOfCookie = true;
					}
					this.Value = this.Extract();
					return CookieToken.NameValuePair;
				}
				return CookieToken.Attribute;
			}
		}

		// Token: 0x0600233F RID: 9023 RVA: 0x000816C6 File Offset: 0x0007F8C6
		internal void Reset()
		{
			this.m_eofCookie = false;
			this.m_name = string.Empty;
			this.m_quoted = false;
			this.m_start = this.m_index;
			this.m_token = CookieToken.Nothing;
			this.m_tokenLength = 0;
			this.m_value = string.Empty;
		}

		// Token: 0x06002340 RID: 9024 RVA: 0x00081708 File Offset: 0x0007F908
		internal CookieToken TokenFromName(bool parseResponseCookies)
		{
			if (!parseResponseCookies)
			{
				for (int i = 0; i < CookieTokenizer.RecognizedServerAttributes.Length; i++)
				{
					if (CookieTokenizer.RecognizedServerAttributes[i].IsEqualTo(this.Name))
					{
						return CookieTokenizer.RecognizedServerAttributes[i].Token;
					}
				}
			}
			else
			{
				for (int j = 0; j < CookieTokenizer.RecognizedAttributes.Length; j++)
				{
					if (CookieTokenizer.RecognizedAttributes[j].IsEqualTo(this.Name))
					{
						return CookieTokenizer.RecognizedAttributes[j].Token;
					}
				}
			}
			return CookieToken.Unknown;
		}

		// Token: 0x0400149E RID: 5278
		private bool m_eofCookie;

		// Token: 0x0400149F RID: 5279
		private int m_index;

		// Token: 0x040014A0 RID: 5280
		private int m_length;

		// Token: 0x040014A1 RID: 5281
		private string m_name;

		// Token: 0x040014A2 RID: 5282
		private bool m_quoted;

		// Token: 0x040014A3 RID: 5283
		private int m_start;

		// Token: 0x040014A4 RID: 5284
		private CookieToken m_token;

		// Token: 0x040014A5 RID: 5285
		private int m_tokenLength;

		// Token: 0x040014A6 RID: 5286
		private string m_tokenStream;

		// Token: 0x040014A7 RID: 5287
		private string m_value;

		// Token: 0x040014A8 RID: 5288
		private static CookieTokenizer.RecognizedAttribute[] RecognizedAttributes = new CookieTokenizer.RecognizedAttribute[]
		{
			new CookieTokenizer.RecognizedAttribute("Path", CookieToken.Path),
			new CookieTokenizer.RecognizedAttribute("Max-Age", CookieToken.MaxAge),
			new CookieTokenizer.RecognizedAttribute("Expires", CookieToken.Expires),
			new CookieTokenizer.RecognizedAttribute("Version", CookieToken.Version),
			new CookieTokenizer.RecognizedAttribute("Domain", CookieToken.Domain),
			new CookieTokenizer.RecognizedAttribute("Secure", CookieToken.Secure),
			new CookieTokenizer.RecognizedAttribute("Discard", CookieToken.Discard),
			new CookieTokenizer.RecognizedAttribute("Port", CookieToken.Port),
			new CookieTokenizer.RecognizedAttribute("Comment", CookieToken.Comment),
			new CookieTokenizer.RecognizedAttribute("CommentURL", CookieToken.CommentUrl),
			new CookieTokenizer.RecognizedAttribute("HttpOnly", CookieToken.HttpOnly)
		};

		// Token: 0x040014A9 RID: 5289
		private static CookieTokenizer.RecognizedAttribute[] RecognizedServerAttributes = new CookieTokenizer.RecognizedAttribute[]
		{
			new CookieTokenizer.RecognizedAttribute("$Path", CookieToken.Path),
			new CookieTokenizer.RecognizedAttribute("$Version", CookieToken.Version),
			new CookieTokenizer.RecognizedAttribute("$Domain", CookieToken.Domain),
			new CookieTokenizer.RecognizedAttribute("$Port", CookieToken.Port),
			new CookieTokenizer.RecognizedAttribute("$HttpOnly", CookieToken.HttpOnly)
		};

		// Token: 0x0200045D RID: 1117
		private struct RecognizedAttribute
		{
			// Token: 0x06002342 RID: 9026 RVA: 0x000818E8 File Offset: 0x0007FAE8
			internal RecognizedAttribute(string name, CookieToken token)
			{
				this.m_name = name;
				this.m_token = token;
			}

			// Token: 0x17000708 RID: 1800
			// (get) Token: 0x06002343 RID: 9027 RVA: 0x000818F8 File Offset: 0x0007FAF8
			internal CookieToken Token
			{
				get
				{
					return this.m_token;
				}
			}

			// Token: 0x06002344 RID: 9028 RVA: 0x00081900 File Offset: 0x0007FB00
			internal bool IsEqualTo(string value)
			{
				return string.Compare(this.m_name, value, StringComparison.OrdinalIgnoreCase) == 0;
			}

			// Token: 0x040014AA RID: 5290
			private string m_name;

			// Token: 0x040014AB RID: 5291
			private CookieToken m_token;
		}
	}
}
