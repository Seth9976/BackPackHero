using System;
using System.Text;

namespace Unity.VisualScripting.Antlr3.Runtime.Tree
{
	// Token: 0x0200003B RID: 59
	public class TreePatternLexer
	{
		// Token: 0x06000237 RID: 567 RVA: 0x00006B36 File Offset: 0x00005B36
		public TreePatternLexer(string pattern)
		{
			this.pattern = pattern;
			this.n = pattern.Length;
			this.Consume();
		}

		// Token: 0x06000238 RID: 568 RVA: 0x00006B6C File Offset: 0x00005B6C
		public int NextToken()
		{
			this.sval.Length = 0;
			while (this.c != -1)
			{
				if (this.c == 32 || this.c == 10 || this.c == 13 || this.c == 9)
				{
					this.Consume();
				}
				else
				{
					if ((this.c >= 97 && this.c <= 122) || (this.c >= 65 && this.c <= 90) || this.c == 95)
					{
						this.sval.Append((char)this.c);
						this.Consume();
						while ((this.c >= 97 && this.c <= 122) || (this.c >= 65 && this.c <= 90) || (this.c >= 48 && this.c <= 57) || this.c == 95)
						{
							this.sval.Append((char)this.c);
							this.Consume();
						}
						return 3;
					}
					if (this.c == 40)
					{
						this.Consume();
						return 1;
					}
					if (this.c == 41)
					{
						this.Consume();
						return 2;
					}
					if (this.c == 37)
					{
						this.Consume();
						return 5;
					}
					if (this.c == 58)
					{
						this.Consume();
						return 6;
					}
					if (this.c == 46)
					{
						this.Consume();
						return 7;
					}
					if (this.c == 91)
					{
						this.Consume();
						while (this.c != 93)
						{
							if (this.c == 92)
							{
								this.Consume();
								if (this.c != 93)
								{
									this.sval.Append('\\');
								}
								this.sval.Append((char)this.c);
							}
							else
							{
								this.sval.Append((char)this.c);
							}
							this.Consume();
						}
						this.Consume();
						return 4;
					}
					this.Consume();
					this.error = true;
					return -1;
				}
			}
			return -1;
		}

		// Token: 0x06000239 RID: 569 RVA: 0x00006D5B File Offset: 0x00005D5B
		protected void Consume()
		{
			this.p++;
			if (this.p >= this.n)
			{
				this.c = -1;
				return;
			}
			this.c = (int)this.pattern.get_Chars(this.p);
		}

		// Token: 0x0400008E RID: 142
		public const int EOF = -1;

		// Token: 0x0400008F RID: 143
		public const int BEGIN = 1;

		// Token: 0x04000090 RID: 144
		public const int END = 2;

		// Token: 0x04000091 RID: 145
		public const int ID = 3;

		// Token: 0x04000092 RID: 146
		public const int ARG = 4;

		// Token: 0x04000093 RID: 147
		public const int PERCENT = 5;

		// Token: 0x04000094 RID: 148
		public const int COLON = 6;

		// Token: 0x04000095 RID: 149
		public const int DOT = 7;

		// Token: 0x04000096 RID: 150
		protected string pattern;

		// Token: 0x04000097 RID: 151
		protected int p = -1;

		// Token: 0x04000098 RID: 152
		protected int c;

		// Token: 0x04000099 RID: 153
		protected int n;

		// Token: 0x0400009A RID: 154
		public StringBuilder sval = new StringBuilder();

		// Token: 0x0400009B RID: 155
		public bool error;
	}
}
