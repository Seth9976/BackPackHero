using System;

namespace UnityEngine
{
	// Token: 0x020001DD RID: 477
	[AttributeUsage(256, Inherited = true, AllowMultiple = false)]
	public sealed class TextAreaAttribute : PropertyAttribute
	{
		// Token: 0x060015D5 RID: 5589 RVA: 0x00022F6E File Offset: 0x0002116E
		public TextAreaAttribute()
		{
			this.minLines = 3;
			this.maxLines = 3;
		}

		// Token: 0x060015D6 RID: 5590 RVA: 0x00022F86 File Offset: 0x00021186
		public TextAreaAttribute(int minLines, int maxLines)
		{
			this.minLines = minLines;
			this.maxLines = maxLines;
		}

		// Token: 0x040007B2 RID: 1970
		public readonly int minLines;

		// Token: 0x040007B3 RID: 1971
		public readonly int maxLines;
	}
}
