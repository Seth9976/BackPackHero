using System;

namespace Unity.VisualScripting.Antlr3.Runtime.Tree
{
	// Token: 0x02000010 RID: 16
	public class CommonTreeAdaptor : BaseTreeAdaptor
	{
		// Token: 0x060000BE RID: 190 RVA: 0x0000301B File Offset: 0x0000201B
		public override object DupNode(object t)
		{
			if (t == null)
			{
				return null;
			}
			return ((ITree)t).DupNode();
		}

		// Token: 0x060000BF RID: 191 RVA: 0x0000302D File Offset: 0x0000202D
		public override object Create(IToken payload)
		{
			return new CommonTree(payload);
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00003035 File Offset: 0x00002035
		public override IToken CreateToken(int tokenType, string text)
		{
			return new CommonToken(tokenType, text);
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x0000303E File Offset: 0x0000203E
		public override IToken CreateToken(IToken fromToken)
		{
			return new CommonToken(fromToken);
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00003048 File Offset: 0x00002048
		public override void SetTokenBoundaries(object t, IToken startToken, IToken stopToken)
		{
			if (t == null)
			{
				return;
			}
			int num = 0;
			int num2 = 0;
			if (startToken != null)
			{
				num = startToken.TokenIndex;
			}
			if (stopToken != null)
			{
				num2 = stopToken.TokenIndex;
			}
			((ITree)t).TokenStartIndex = num;
			((ITree)t).TokenStopIndex = num2;
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00003089 File Offset: 0x00002089
		public override int GetTokenStartIndex(object t)
		{
			if (t == null)
			{
				return -1;
			}
			return ((ITree)t).TokenStartIndex;
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x0000309B File Offset: 0x0000209B
		public override int GetTokenStopIndex(object t)
		{
			if (t == null)
			{
				return -1;
			}
			return ((ITree)t).TokenStopIndex;
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x000030AD File Offset: 0x000020AD
		public override string GetNodeText(object t)
		{
			if (t == null)
			{
				return null;
			}
			return ((ITree)t).Text;
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x000030BF File Offset: 0x000020BF
		public override int GetNodeType(object t)
		{
			if (t == null)
			{
				return 0;
			}
			return ((ITree)t).Type;
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x000030D1 File Offset: 0x000020D1
		public override IToken GetToken(object treeNode)
		{
			if (treeNode is CommonTree)
			{
				return ((CommonTree)treeNode).Token;
			}
			return null;
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x000030E8 File Offset: 0x000020E8
		public override object GetChild(object t, int i)
		{
			if (t == null)
			{
				return null;
			}
			return ((ITree)t).GetChild(i);
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x000030FB File Offset: 0x000020FB
		public override int GetChildCount(object t)
		{
			if (t == null)
			{
				return 0;
			}
			return ((ITree)t).ChildCount;
		}

		// Token: 0x060000CA RID: 202 RVA: 0x0000310D File Offset: 0x0000210D
		public override object GetParent(object t)
		{
			if (t == null)
			{
				return null;
			}
			return ((ITree)t).Parent;
		}

		// Token: 0x060000CB RID: 203 RVA: 0x0000311F File Offset: 0x0000211F
		public override void SetParent(object t, object parent)
		{
			if (t == null)
			{
				((ITree)t).Parent = (ITree)parent;
			}
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00003135 File Offset: 0x00002135
		public override int GetChildIndex(object t)
		{
			if (t == null)
			{
				return 0;
			}
			return ((ITree)t).ChildIndex;
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00003147 File Offset: 0x00002147
		public override void SetChildIndex(object t, int index)
		{
			if (t == null)
			{
				((ITree)t).ChildIndex = index;
			}
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00003158 File Offset: 0x00002158
		public override void ReplaceChildren(object parent, int startChildIndex, int stopChildIndex, object t)
		{
			if (parent != null)
			{
				((ITree)parent).ReplaceChildren(startChildIndex, stopChildIndex, t);
			}
		}
	}
}
