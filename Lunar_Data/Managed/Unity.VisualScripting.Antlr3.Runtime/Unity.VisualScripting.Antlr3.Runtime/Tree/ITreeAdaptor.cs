using System;

namespace Unity.VisualScripting.Antlr3.Runtime.Tree
{
	// Token: 0x02000009 RID: 9
	public interface ITreeAdaptor
	{
		// Token: 0x06000036 RID: 54
		object Create(IToken payload);

		// Token: 0x06000037 RID: 55
		object DupNode(object treeNode);

		// Token: 0x06000038 RID: 56
		object DupTree(object tree);

		// Token: 0x06000039 RID: 57
		object GetNilNode();

		// Token: 0x0600003A RID: 58
		object ErrorNode(ITokenStream input, IToken start, IToken stop, RecognitionException e);

		// Token: 0x0600003B RID: 59
		bool IsNil(object tree);

		// Token: 0x0600003C RID: 60
		void AddChild(object t, object child);

		// Token: 0x0600003D RID: 61
		object BecomeRoot(object newRoot, object oldRoot);

		// Token: 0x0600003E RID: 62
		object RulePostProcessing(object root);

		// Token: 0x0600003F RID: 63
		int GetUniqueID(object node);

		// Token: 0x06000040 RID: 64
		object BecomeRoot(IToken newRoot, object oldRoot);

		// Token: 0x06000041 RID: 65
		object Create(int tokenType, IToken fromToken);

		// Token: 0x06000042 RID: 66
		object Create(int tokenType, IToken fromToken, string text);

		// Token: 0x06000043 RID: 67
		object Create(int tokenType, string text);

		// Token: 0x06000044 RID: 68
		int GetNodeType(object t);

		// Token: 0x06000045 RID: 69
		void SetNodeType(object t, int type);

		// Token: 0x06000046 RID: 70
		string GetNodeText(object t);

		// Token: 0x06000047 RID: 71
		void SetNodeText(object t, string text);

		// Token: 0x06000048 RID: 72
		IToken GetToken(object treeNode);

		// Token: 0x06000049 RID: 73
		void SetTokenBoundaries(object t, IToken startToken, IToken stopToken);

		// Token: 0x0600004A RID: 74
		int GetTokenStartIndex(object t);

		// Token: 0x0600004B RID: 75
		int GetTokenStopIndex(object t);

		// Token: 0x0600004C RID: 76
		object GetChild(object t, int i);

		// Token: 0x0600004D RID: 77
		void SetChild(object t, int i, object child);

		// Token: 0x0600004E RID: 78
		object DeleteChild(object t, int i);

		// Token: 0x0600004F RID: 79
		int GetChildCount(object t);

		// Token: 0x06000050 RID: 80
		object GetParent(object t);

		// Token: 0x06000051 RID: 81
		void SetParent(object t, object parent);

		// Token: 0x06000052 RID: 82
		int GetChildIndex(object t);

		// Token: 0x06000053 RID: 83
		void SetChildIndex(object t, int index);

		// Token: 0x06000054 RID: 84
		void ReplaceChildren(object parent, int startChildIndex, int stopChildIndex, object t);
	}
}
