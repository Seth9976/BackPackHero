using System;

namespace Febucci.UI.Core
{
	// Token: 0x02000040 RID: 64
	public struct WordInfo
	{
		// Token: 0x0600016A RID: 362 RVA: 0x00006E8C File Offset: 0x0000508C
		public WordInfo(int firstCharacterIndex, int lastCharacterIndex, string text)
		{
			this.firstCharacterIndex = firstCharacterIndex;
			this.lastCharacterIndex = lastCharacterIndex;
			this.text = text;
		}

		// Token: 0x040000F9 RID: 249
		public readonly int firstCharacterIndex;

		// Token: 0x040000FA RID: 250
		public readonly int lastCharacterIndex;

		// Token: 0x040000FB RID: 251
		public readonly string text;
	}
}
