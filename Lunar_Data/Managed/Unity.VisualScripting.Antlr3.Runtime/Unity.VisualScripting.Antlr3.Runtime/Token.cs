using System;

namespace Unity.VisualScripting.Antlr3.Runtime
{
	// Token: 0x02000017 RID: 23
	public static class Token
	{
		// Token: 0x0400003F RID: 63
		public const int EOR_TOKEN_TYPE = 1;

		// Token: 0x04000040 RID: 64
		public const int DOWN = 2;

		// Token: 0x04000041 RID: 65
		public const int UP = 3;

		// Token: 0x04000042 RID: 66
		public const int INVALID_TOKEN_TYPE = 0;

		// Token: 0x04000043 RID: 67
		public const int DEFAULT_CHANNEL = 0;

		// Token: 0x04000044 RID: 68
		public const int HIDDEN_CHANNEL = 99;

		// Token: 0x04000045 RID: 69
		public static readonly int MIN_TOKEN_TYPE = 4;

		// Token: 0x04000046 RID: 70
		public static readonly int EOF = -1;

		// Token: 0x04000047 RID: 71
		public static readonly IToken EOF_TOKEN = new CommonToken(Token.EOF);

		// Token: 0x04000048 RID: 72
		public static readonly IToken INVALID_TOKEN = new CommonToken(0);

		// Token: 0x04000049 RID: 73
		public static readonly IToken SKIP_TOKEN = new CommonToken(0);
	}
}
