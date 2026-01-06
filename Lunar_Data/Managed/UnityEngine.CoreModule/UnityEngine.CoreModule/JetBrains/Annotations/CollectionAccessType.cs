using System;

namespace JetBrains.Annotations
{
	// Token: 0x020000CE RID: 206
	[Flags]
	public enum CollectionAccessType
	{
		// Token: 0x0400025E RID: 606
		None = 0,
		// Token: 0x0400025F RID: 607
		Read = 1,
		// Token: 0x04000260 RID: 608
		ModifyExistingContent = 2,
		// Token: 0x04000261 RID: 609
		UpdatedContent = 6
	}
}
