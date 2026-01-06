using System;

namespace Unity.VisualScripting.Antlr3.Runtime
{
	// Token: 0x02000012 RID: 18
	[Serializable]
	public class CommonToken : IToken
	{
		// Token: 0x060000E4 RID: 228 RVA: 0x00003451 File Offset: 0x00002451
		public CommonToken(int type)
		{
			this.type = type;
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x0000346E File Offset: 0x0000246E
		public CommonToken(ICharStream input, int type, int channel, int start, int stop)
		{
			this.input = input;
			this.type = type;
			this.channel = channel;
			this.start = start;
			this.stop = stop;
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x000034A9 File Offset: 0x000024A9
		public CommonToken(int type, string text)
		{
			this.type = type;
			this.channel = 0;
			this.text = text;
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x000034D4 File Offset: 0x000024D4
		public CommonToken(IToken oldToken)
		{
			this.text = oldToken.Text;
			this.type = oldToken.Type;
			this.line = oldToken.Line;
			this.index = oldToken.TokenIndex;
			this.charPositionInLine = oldToken.CharPositionInLine;
			this.channel = oldToken.Channel;
			if (oldToken is CommonToken)
			{
				this.start = ((CommonToken)oldToken).start;
				this.stop = ((CommonToken)oldToken).stop;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x00003567 File Offset: 0x00002567
		// (set) Token: 0x060000E9 RID: 233 RVA: 0x0000356F File Offset: 0x0000256F
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

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000EA RID: 234 RVA: 0x00003578 File Offset: 0x00002578
		// (set) Token: 0x060000EB RID: 235 RVA: 0x00003580 File Offset: 0x00002580
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

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000EC RID: 236 RVA: 0x00003589 File Offset: 0x00002589
		// (set) Token: 0x060000ED RID: 237 RVA: 0x00003591 File Offset: 0x00002591
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

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000EE RID: 238 RVA: 0x0000359A File Offset: 0x0000259A
		// (set) Token: 0x060000EF RID: 239 RVA: 0x000035A2 File Offset: 0x000025A2
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

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x000035AB File Offset: 0x000025AB
		// (set) Token: 0x060000F1 RID: 241 RVA: 0x000035B3 File Offset: 0x000025B3
		public virtual int StartIndex
		{
			get
			{
				return this.start;
			}
			set
			{
				this.start = value;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x000035BC File Offset: 0x000025BC
		// (set) Token: 0x060000F3 RID: 243 RVA: 0x000035C4 File Offset: 0x000025C4
		public virtual int StopIndex
		{
			get
			{
				return this.stop;
			}
			set
			{
				this.stop = value;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000F4 RID: 244 RVA: 0x000035CD File Offset: 0x000025CD
		// (set) Token: 0x060000F5 RID: 245 RVA: 0x000035D5 File Offset: 0x000025D5
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

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x000035DE File Offset: 0x000025DE
		// (set) Token: 0x060000F7 RID: 247 RVA: 0x000035E6 File Offset: 0x000025E6
		public virtual ICharStream InputStream
		{
			get
			{
				return this.input;
			}
			set
			{
				this.input = value;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x000035EF File Offset: 0x000025EF
		// (set) Token: 0x060000F9 RID: 249 RVA: 0x0000362D File Offset: 0x0000262D
		public virtual string Text
		{
			get
			{
				if (this.text != null)
				{
					return this.text;
				}
				if (this.input == null)
				{
					return null;
				}
				this.text = this.input.Substring(this.start, this.stop);
				return this.text;
			}
			set
			{
				this.text = value;
			}
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00003638 File Offset: 0x00002638
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
				"[@", this.TokenIndex, ",", this.start, ":", this.stop, "='", text2, "',<", this.type,
				">", text, ",", this.line, ":", this.CharPositionInLine, "]"
			});
		}

		// Token: 0x04000024 RID: 36
		protected internal int type;

		// Token: 0x04000025 RID: 37
		protected internal int line;

		// Token: 0x04000026 RID: 38
		protected internal int charPositionInLine = -1;

		// Token: 0x04000027 RID: 39
		protected internal int channel;

		// Token: 0x04000028 RID: 40
		[NonSerialized]
		protected internal ICharStream input;

		// Token: 0x04000029 RID: 41
		protected internal string text;

		// Token: 0x0400002A RID: 42
		protected internal int index = -1;

		// Token: 0x0400002B RID: 43
		protected internal int start;

		// Token: 0x0400002C RID: 44
		protected internal int stop;
	}
}
