using System;

namespace Unity.VisualScripting.Antlr3.Runtime.Tree
{
	// Token: 0x0200003C RID: 60
	public class TreePatternParser
	{
		// Token: 0x0600023A RID: 570 RVA: 0x00006D98 File Offset: 0x00005D98
		public TreePatternParser(TreePatternLexer tokenizer, TreeWizard wizard, ITreeAdaptor adaptor)
		{
			this.tokenizer = tokenizer;
			this.wizard = wizard;
			this.adaptor = adaptor;
			this.ttype = tokenizer.NextToken();
		}

		// Token: 0x0600023B RID: 571 RVA: 0x00006DC4 File Offset: 0x00005DC4
		public object Pattern()
		{
			if (this.ttype == 1)
			{
				return this.ParseTree();
			}
			if (this.ttype != 3)
			{
				return null;
			}
			object obj = this.ParseNode();
			if (this.ttype == -1)
			{
				return obj;
			}
			return null;
		}

		// Token: 0x0600023C RID: 572 RVA: 0x00006E00 File Offset: 0x00005E00
		public object ParseTree()
		{
			if (this.ttype != 1)
			{
				Console.Out.WriteLine("no BEGIN");
				return null;
			}
			this.ttype = this.tokenizer.NextToken();
			object obj = this.ParseNode();
			if (obj == null)
			{
				return null;
			}
			while (this.ttype == 1 || this.ttype == 3 || this.ttype == 5 || this.ttype == 7)
			{
				if (this.ttype == 1)
				{
					object obj2 = this.ParseTree();
					this.adaptor.AddChild(obj, obj2);
				}
				else
				{
					object obj3 = this.ParseNode();
					if (obj3 == null)
					{
						return null;
					}
					this.adaptor.AddChild(obj, obj3);
				}
			}
			if (this.ttype != 2)
			{
				Console.Out.WriteLine("no END");
				return null;
			}
			this.ttype = this.tokenizer.NextToken();
			return obj;
		}

		// Token: 0x0600023D RID: 573 RVA: 0x00006ECC File Offset: 0x00005ECC
		public object ParseNode()
		{
			string text = null;
			if (this.ttype == 5)
			{
				this.ttype = this.tokenizer.NextToken();
				if (this.ttype != 3)
				{
					return null;
				}
				text = this.tokenizer.sval.ToString();
				this.ttype = this.tokenizer.NextToken();
				if (this.ttype != 6)
				{
					return null;
				}
				this.ttype = this.tokenizer.NextToken();
			}
			if (this.ttype == 7)
			{
				this.ttype = this.tokenizer.NextToken();
				IToken token = new CommonToken(0, ".");
				TreeWizard.TreePattern treePattern = new TreeWizard.WildcardTreePattern(token);
				if (text != null)
				{
					treePattern.label = text;
				}
				return treePattern;
			}
			if (this.ttype != 3)
			{
				return null;
			}
			string text2 = this.tokenizer.sval.ToString();
			this.ttype = this.tokenizer.NextToken();
			if (text2.Equals("nil"))
			{
				return this.adaptor.GetNilNode();
			}
			string text3 = text2;
			string text4 = null;
			if (this.ttype == 4)
			{
				text4 = this.tokenizer.sval.ToString();
				text3 = text4;
				this.ttype = this.tokenizer.NextToken();
			}
			int tokenType = this.wizard.GetTokenType(text2);
			if (tokenType == 0)
			{
				return null;
			}
			object obj = this.adaptor.Create(tokenType, text3);
			if (text != null && obj.GetType() == typeof(TreeWizard.TreePattern))
			{
				((TreeWizard.TreePattern)obj).label = text;
			}
			if (text4 != null && obj.GetType() == typeof(TreeWizard.TreePattern))
			{
				((TreeWizard.TreePattern)obj).hasTextArg = true;
			}
			return obj;
		}

		// Token: 0x0400009C RID: 156
		protected TreePatternLexer tokenizer;

		// Token: 0x0400009D RID: 157
		protected int ttype;

		// Token: 0x0400009E RID: 158
		protected TreeWizard wizard;

		// Token: 0x0400009F RID: 159
		protected ITreeAdaptor adaptor;
	}
}
