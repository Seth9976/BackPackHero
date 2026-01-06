using System;

namespace Unity.VisualScripting.Antlr3.Runtime
{
	// Token: 0x02000042 RID: 66
	[Serializable]
	public class ClassicToken : IToken
	{
		// Token: 0x06000259 RID: 601 RVA: 0x000072BB File Offset: 0x000062BB
		public ClassicToken(int type)
		{
			this.type = type;
		}

		// Token: 0x0600025A RID: 602 RVA: 0x000072CC File Offset: 0x000062CC
		public ClassicToken(IToken oldToken)
		{
			this.text = oldToken.Text;
			this.type = oldToken.Type;
			this.line = oldToken.Line;
			this.charPositionInLine = oldToken.CharPositionInLine;
			this.channel = oldToken.Channel;
		}

		// Token: 0x0600025B RID: 603 RVA: 0x0000731B File Offset: 0x0000631B
		public ClassicToken(int type, string text)
		{
			this.type = type;
			this.text = text;
		}

		// Token: 0x0600025C RID: 604 RVA: 0x00007331 File Offset: 0x00006331
		public ClassicToken(int type, string text, int channel)
		{
			this.type = type;
			this.text = text;
			this.channel = channel;
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x0600025D RID: 605 RVA: 0x0000734E File Offset: 0x0000634E
		// (set) Token: 0x0600025E RID: 606 RVA: 0x00007356 File Offset: 0x00006356
		public virtual int Type
		{
			get
			{
				return this.type;
			}
			set
			{
				this.type = value;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x0600025F RID: 607 RVA: 0x0000735F File Offset: 0x0000635F
		// (set) Token: 0x06000260 RID: 608 RVA: 0x00007367 File Offset: 0x00006367
		public virtual int Line
		{
			get
			{
				return this.line;
			}
			set
			{
				this.line = value;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000261 RID: 609 RVA: 0x00007370 File Offset: 0x00006370
		// (set) Token: 0x06000262 RID: 610 RVA: 0x00007378 File Offset: 0x00006378
		public virtual int CharPositionInLine
		{
			get
			{
				return this.charPositionInLine;
			}
			set
			{
				this.charPositionInLine = value;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000263 RID: 611 RVA: 0x00007381 File Offset: 0x00006381
		// (set) Token: 0x06000264 RID: 612 RVA: 0x00007389 File Offset: 0x00006389
		public virtual int Channel
		{
			get
			{
				return this.channel;
			}
			set
			{
				this.channel = value;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000265 RID: 613 RVA: 0x00007392 File Offset: 0x00006392
		// (set) Token: 0x06000266 RID: 614 RVA: 0x0000739A File Offset: 0x0000639A
		public virtual int TokenIndex
		{
			get
			{
				return this.index;
			}
			set
			{
				this.index = value;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000267 RID: 615 RVA: 0x000073A3 File Offset: 0x000063A3
		// (set) Token: 0x06000268 RID: 616 RVA: 0x000073AB File Offset: 0x000063AB
		public virtual string Text
		{
			get
			{
				return this.text;
			}
			set
			{
				this.text = value;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000269 RID: 617 RVA: 0x000073B4 File Offset: 0x000063B4
		// (set) Token: 0x0600026A RID: 618 RVA: 0x000073B7 File Offset: 0x000063B7
		public virtual ICharStream InputStream
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x0600026B RID: 619 RVA: 0x000073BC File Offset: 0x000063BC
		public override string ToString()
		{
			string text = "";
			if (this.channel > 0)
			{
				text = ",channel=" + this.channel;
			}
			string text2 = this.Text;
			if (text2 != null)
			{
				text2 = text2.Replace("\n", "\\\\n");
				text2 = text2.Replace("\r", "\\\\r");
				text2 = text2.Replace("\t", "\\\\t");
			}
			else
			{
				text2 = "<no text>";
			}
			return string.Concat(new object[]
			{
				"[@", this.TokenIndex, ",'", text2, "',<", this.type, ">", text, ",", this.line,
				":", this.CharPositionInLine, "]"
			});
		}

		// Token: 0x040000A7 RID: 167
		protected internal string text;

		// Token: 0x040000A8 RID: 168
		protected internal int type;

		// Token: 0x040000A9 RID: 169
		protected internal int line;

		// Token: 0x040000AA RID: 170
		protected internal int charPositionInLine;

		// Token: 0x040000AB RID: 171
		protected internal int channel;

		// Token: 0x040000AC RID: 172
		protected internal int index;
	}
}
