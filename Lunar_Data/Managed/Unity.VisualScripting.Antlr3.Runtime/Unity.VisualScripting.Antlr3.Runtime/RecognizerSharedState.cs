using System;
using System.Collections;

namespace Unity.VisualScripting.Antlr3.Runtime
{
	// Token: 0x02000019 RID: 25
	public class RecognizerSharedState
	{
		// Token: 0x0400004D RID: 77
		public BitSet[] following = new BitSet[100];

		// Token: 0x0400004E RID: 78
		public int followingStackPointer = -1;

		// Token: 0x0400004F RID: 79
		public bool errorRecovery;

		// Token: 0x04000050 RID: 80
		public int lastErrorIndex = -1;

		// Token: 0x04000051 RID: 81
		public bool failed;

		// Token: 0x04000052 RID: 82
		public int syntaxErrors;

		// Token: 0x04000053 RID: 83
		public int backtracking;

		// Token: 0x04000054 RID: 84
		public IDictionary[] ruleMemo;

		// Token: 0x04000055 RID: 85
		public IToken token;

		// Token: 0x04000056 RID: 86
		public int tokenStartCharIndex = -1;

		// Token: 0x04000057 RID: 87
		public int tokenStartLine;

		// Token: 0x04000058 RID: 88
		public int tokenStartCharPositionInLine;

		// Token: 0x04000059 RID: 89
		public int channel;

		// Token: 0x0400005A RID: 90
		public int type;

		// Token: 0x0400005B RID: 91
		public string text;
	}
}
