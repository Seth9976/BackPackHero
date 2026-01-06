using System;
using Unity.VisualScripting.Antlr3.Runtime.Tree;

namespace Unity.VisualScripting.Antlr3.Runtime
{
	// Token: 0x0200000E RID: 14
	[Serializable]
	public class CommonErrorNode : CommonTree
	{
		// Token: 0x0600009F RID: 159 RVA: 0x00002DF0 File Offset: 0x00001DF0
		public CommonErrorNode(ITokenStream input, IToken start, IToken stop, RecognitionException e)
		{
			if (stop == null || (stop.TokenIndex < start.TokenIndex && stop.Type != Unity.VisualScripting.Antlr3.Runtime.Token.EOF))
			{
				stop = start;
			}
			this.input = input;
			this.start = start;
			this.stop = stop;
			this.trappedException = e;
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x00002E41 File Offset: 0x00001E41
		public override bool IsNil
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x00002E44 File Offset: 0x00001E44
		public override int Type
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x00002E48 File Offset: 0x00001E48
		public override string Text
		{
			get
			{
				string text;
				if (this.start != null)
				{
					int tokenIndex = this.start.TokenIndex;
					int num = this.stop.TokenIndex;
					if (this.stop.Type == Unity.VisualScripting.Antlr3.Runtime.Token.EOF)
					{
						num = ((ITokenStream)this.input).Count;
					}
					text = ((ITokenStream)this.input).ToString(tokenIndex, num);
				}
				else if (this.start is ITree)
				{
					text = ((ITreeNodeStream)this.input).ToString(this.start, this.stop);
				}
				else
				{
					text = "<unknown>";
				}
				return text;
			}
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00002EE4 File Offset: 0x00001EE4
		public override string ToString()
		{
			if (this.trappedException is MissingTokenException)
			{
				return "<missing type: " + ((MissingTokenException)this.trappedException).MissingType + ">";
			}
			if (this.trappedException is UnwantedTokenException)
			{
				return string.Concat(new object[]
				{
					"<extraneous: ",
					((UnwantedTokenException)this.trappedException).UnexpectedToken,
					", resync=",
					this.Text,
					">"
				});
			}
			if (this.trappedException is MismatchedTokenException)
			{
				return string.Concat(new object[]
				{
					"<mismatched token: ",
					this.trappedException.Token,
					", resync=",
					this.Text,
					">"
				});
			}
			if (this.trappedException is NoViableAltException)
			{
				return string.Concat(new object[]
				{
					"<unexpected: ",
					this.trappedException.Token,
					", resync=",
					this.Text,
					">"
				});
			}
			return "<error: " + this.Text + ">";
		}

		// Token: 0x0400001B RID: 27
		public IIntStream input;

		// Token: 0x0400001C RID: 28
		public IToken start;

		// Token: 0x0400001D RID: 29
		public IToken stop;

		// Token: 0x0400001E RID: 30
		[NonSerialized]
		public RecognitionException trappedException;
	}
}
