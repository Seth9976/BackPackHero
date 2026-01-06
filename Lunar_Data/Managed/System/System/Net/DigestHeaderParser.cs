using System;

namespace System.Net
{
	// Token: 0x02000485 RID: 1157
	internal class DigestHeaderParser
	{
		// Token: 0x06002467 RID: 9319 RVA: 0x000865CC File Offset: 0x000847CC
		public DigestHeaderParser(string header)
		{
			this.header = header.Trim();
		}

		// Token: 0x17000745 RID: 1861
		// (get) Token: 0x06002468 RID: 9320 RVA: 0x000865F2 File Offset: 0x000847F2
		public string Realm
		{
			get
			{
				return this.values[0];
			}
		}

		// Token: 0x17000746 RID: 1862
		// (get) Token: 0x06002469 RID: 9321 RVA: 0x000865FC File Offset: 0x000847FC
		public string Opaque
		{
			get
			{
				return this.values[1];
			}
		}

		// Token: 0x17000747 RID: 1863
		// (get) Token: 0x0600246A RID: 9322 RVA: 0x00086606 File Offset: 0x00084806
		public string Nonce
		{
			get
			{
				return this.values[2];
			}
		}

		// Token: 0x17000748 RID: 1864
		// (get) Token: 0x0600246B RID: 9323 RVA: 0x00086610 File Offset: 0x00084810
		public string Algorithm
		{
			get
			{
				return this.values[3];
			}
		}

		// Token: 0x17000749 RID: 1865
		// (get) Token: 0x0600246C RID: 9324 RVA: 0x0008661A File Offset: 0x0008481A
		public string QOP
		{
			get
			{
				return this.values[4];
			}
		}

		// Token: 0x0600246D RID: 9325 RVA: 0x00086624 File Offset: 0x00084824
		public bool Parse()
		{
			if (!this.header.ToLower().StartsWith("digest "))
			{
				return false;
			}
			this.pos = 6;
			this.length = this.header.Length;
			while (this.pos < this.length)
			{
				string text;
				string text2;
				if (!this.GetKeywordAndValue(out text, out text2))
				{
					return false;
				}
				this.SkipWhitespace();
				if (this.pos < this.length && this.header[this.pos] == ',')
				{
					this.pos++;
				}
				int num = Array.IndexOf<string>(DigestHeaderParser.keywords, text);
				if (num != -1)
				{
					if (this.values[num] != null)
					{
						return false;
					}
					this.values[num] = text2;
				}
			}
			return this.Realm != null && this.Nonce != null;
		}

		// Token: 0x0600246E RID: 9326 RVA: 0x000866F0 File Offset: 0x000848F0
		private void SkipWhitespace()
		{
			char c = ' ';
			while (this.pos < this.length && (c == ' ' || c == '\t' || c == '\r' || c == '\n'))
			{
				string text = this.header;
				int num = this.pos;
				this.pos = num + 1;
				c = text[num];
			}
			this.pos--;
		}

		// Token: 0x0600246F RID: 9327 RVA: 0x00086750 File Offset: 0x00084950
		private string GetKey()
		{
			this.SkipWhitespace();
			int num = this.pos;
			while (this.pos < this.length && this.header[this.pos] != '=')
			{
				this.pos++;
			}
			return this.header.Substring(num, this.pos - num).Trim().ToLower();
		}

		// Token: 0x06002470 RID: 9328 RVA: 0x000867BC File Offset: 0x000849BC
		private bool GetKeywordAndValue(out string key, out string value)
		{
			key = null;
			value = null;
			key = this.GetKey();
			if (this.pos >= this.length)
			{
				return false;
			}
			this.SkipWhitespace();
			if (this.pos + 1 < this.length)
			{
				string text = this.header;
				int num = this.pos;
				this.pos = num + 1;
				if (text[num] == '=')
				{
					this.SkipWhitespace();
					if (this.pos + 1 >= this.length)
					{
						return false;
					}
					bool flag = false;
					if (this.header[this.pos] == '"')
					{
						this.pos++;
						flag = true;
					}
					int num2 = this.pos;
					if (flag)
					{
						this.pos = this.header.IndexOf('"', this.pos);
						if (this.pos == -1)
						{
							return false;
						}
					}
					else
					{
						do
						{
							char c = this.header[this.pos];
							if (c == ',' || c == ' ' || c == '\t' || c == '\r' || c == '\n')
							{
								break;
							}
							num = this.pos + 1;
							this.pos = num;
						}
						while (num < this.length);
						if (this.pos >= this.length && num2 == this.pos)
						{
							return false;
						}
					}
					value = this.header.Substring(num2, this.pos - num2);
					this.pos += (flag ? 2 : 1);
					return true;
				}
			}
			return false;
		}

		// Token: 0x04001546 RID: 5446
		private string header;

		// Token: 0x04001547 RID: 5447
		private int length;

		// Token: 0x04001548 RID: 5448
		private int pos;

		// Token: 0x04001549 RID: 5449
		private static string[] keywords = new string[] { "realm", "opaque", "nonce", "algorithm", "qop" };

		// Token: 0x0400154A RID: 5450
		private string[] values = new string[DigestHeaderParser.keywords.Length];
	}
}
