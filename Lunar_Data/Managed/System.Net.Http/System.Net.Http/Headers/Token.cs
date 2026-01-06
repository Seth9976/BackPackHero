using System;

namespace System.Net.Http.Headers
{
	// Token: 0x0200004B RID: 75
	internal struct Token
	{
		// Token: 0x060002DF RID: 735 RVA: 0x0000A773 File Offset: 0x00008973
		public Token(Token.Type type, int startPosition, int endPosition)
		{
			this = default(Token);
			this.type = type;
			this.StartPosition = startPosition;
			this.EndPosition = endPosition;
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060002E0 RID: 736 RVA: 0x0000A791 File Offset: 0x00008991
		// (set) Token: 0x060002E1 RID: 737 RVA: 0x0000A799 File Offset: 0x00008999
		public int StartPosition { readonly get; private set; }

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060002E2 RID: 738 RVA: 0x0000A7A2 File Offset: 0x000089A2
		// (set) Token: 0x060002E3 RID: 739 RVA: 0x0000A7AA File Offset: 0x000089AA
		public int EndPosition { readonly get; private set; }

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060002E4 RID: 740 RVA: 0x0000A7B3 File Offset: 0x000089B3
		public Token.Type Kind
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x0000A7B3 File Offset: 0x000089B3
		public static implicit operator Token.Type(Token token)
		{
			return token.type;
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x0000A7BB File Offset: 0x000089BB
		public override string ToString()
		{
			return this.type.ToString();
		}

		// Token: 0x0400011F RID: 287
		public static readonly Token Empty = new Token(Token.Type.Token, 0, 0);

		// Token: 0x04000120 RID: 288
		private readonly Token.Type type;

		// Token: 0x0200004C RID: 76
		public enum Type
		{
			// Token: 0x04000124 RID: 292
			Error,
			// Token: 0x04000125 RID: 293
			End,
			// Token: 0x04000126 RID: 294
			Token,
			// Token: 0x04000127 RID: 295
			QuotedString,
			// Token: 0x04000128 RID: 296
			SeparatorEqual,
			// Token: 0x04000129 RID: 297
			SeparatorSemicolon,
			// Token: 0x0400012A RID: 298
			SeparatorSlash,
			// Token: 0x0400012B RID: 299
			SeparatorDash,
			// Token: 0x0400012C RID: 300
			SeparatorComma,
			// Token: 0x0400012D RID: 301
			OpenParens
		}
	}
}
