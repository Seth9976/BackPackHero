using System;

namespace UnityEngine.UIElements.StyleSheets.Syntax
{
	// Token: 0x0200037D RID: 893
	internal struct StyleSyntaxToken
	{
		// Token: 0x06001C53 RID: 7251 RVA: 0x00084795 File Offset: 0x00082995
		public StyleSyntaxToken(StyleSyntaxTokenType t)
		{
			this.type = t;
			this.text = null;
			this.number = 0;
		}

		// Token: 0x06001C54 RID: 7252 RVA: 0x000847AD File Offset: 0x000829AD
		public StyleSyntaxToken(StyleSyntaxTokenType type, string text)
		{
			this.type = type;
			this.text = text;
			this.number = 0;
		}

		// Token: 0x06001C55 RID: 7253 RVA: 0x000847C5 File Offset: 0x000829C5
		public StyleSyntaxToken(StyleSyntaxTokenType type, int number)
		{
			this.type = type;
			this.text = null;
			this.number = number;
		}

		// Token: 0x04000E54 RID: 3668
		public StyleSyntaxTokenType type;

		// Token: 0x04000E55 RID: 3669
		public string text;

		// Token: 0x04000E56 RID: 3670
		public int number;
	}
}
