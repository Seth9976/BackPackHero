using System;
using System.Collections;
using System.Text;

namespace Unity.VisualScripting.Antlr3.Runtime.Tree
{
	// Token: 0x0200005A RID: 90
	public class ParseTree : BaseTree
	{
		// Token: 0x0600035B RID: 859 RVA: 0x00009FE2 File Offset: 0x00008FE2
		public ParseTree(object label)
		{
			this.payload = label;
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x0600035C RID: 860 RVA: 0x00009FF1 File Offset: 0x00008FF1
		public override int Type
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x0600035D RID: 861 RVA: 0x00009FF4 File Offset: 0x00008FF4
		public override string Text
		{
			get
			{
				return this.ToString();
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x0600035E RID: 862 RVA: 0x00009FFC File Offset: 0x00008FFC
		// (set) Token: 0x0600035F RID: 863 RVA: 0x00009FFF File Offset: 0x00008FFF
		public override int TokenStartIndex
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000360 RID: 864 RVA: 0x0000A001 File Offset: 0x00009001
		// (set) Token: 0x06000361 RID: 865 RVA: 0x0000A004 File Offset: 0x00009004
		public override int TokenStopIndex
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		// Token: 0x06000362 RID: 866 RVA: 0x0000A006 File Offset: 0x00009006
		public override ITree DupNode()
		{
			return null;
		}

		// Token: 0x06000363 RID: 867 RVA: 0x0000A00C File Offset: 0x0000900C
		public override string ToString()
		{
			if (!(this.payload is IToken))
			{
				return this.payload.ToString();
			}
			IToken token = (IToken)this.payload;
			if (token.Type == Token.EOF)
			{
				return "<EOF>";
			}
			return token.Text;
		}

		// Token: 0x06000364 RID: 868 RVA: 0x0000A058 File Offset: 0x00009058
		public string ToStringWithHiddenTokens()
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (this.hiddenTokens != null)
			{
				for (int i = 0; i < this.hiddenTokens.Count; i++)
				{
					IToken token = (IToken)this.hiddenTokens[i];
					stringBuilder.Append(token.Text);
				}
			}
			string text = this.ToString();
			if (text != "<EOF>")
			{
				stringBuilder.Append(text);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000365 RID: 869 RVA: 0x0000A0CC File Offset: 0x000090CC
		public string ToInputString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			this._ToStringLeaves(stringBuilder);
			return stringBuilder.ToString();
		}

		// Token: 0x06000366 RID: 870 RVA: 0x0000A0EC File Offset: 0x000090EC
		public void _ToStringLeaves(StringBuilder buf)
		{
			if (this.payload is IToken)
			{
				buf.Append(this.ToStringWithHiddenTokens());
				return;
			}
			int num = 0;
			while (this.children != null && num < this.children.Count)
			{
				ParseTree parseTree = (ParseTree)this.children[num];
				parseTree._ToStringLeaves(buf);
				num++;
			}
		}

		// Token: 0x040000F8 RID: 248
		public object payload;

		// Token: 0x040000F9 RID: 249
		public IList hiddenTokens;
	}
}
