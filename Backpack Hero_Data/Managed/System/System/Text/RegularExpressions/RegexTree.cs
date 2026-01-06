using System;
using System.Collections;

namespace System.Text.RegularExpressions
{
	// Token: 0x0200020E RID: 526
	internal sealed class RegexTree
	{
		// Token: 0x06000F2F RID: 3887 RVA: 0x00043E9E File Offset: 0x0004209E
		internal RegexTree(RegexNode root, Hashtable caps, int[] capNumList, int capTop, Hashtable capNames, string[] capsList, RegexOptions options)
		{
			this.Root = root;
			this.Caps = caps;
			this.CapNumList = capNumList;
			this.CapTop = capTop;
			this.CapNames = capNames;
			this.CapsList = capsList;
			this.Options = options;
		}

		// Token: 0x0400097A RID: 2426
		public readonly RegexNode Root;

		// Token: 0x0400097B RID: 2427
		public readonly Hashtable Caps;

		// Token: 0x0400097C RID: 2428
		public readonly int[] CapNumList;

		// Token: 0x0400097D RID: 2429
		public readonly int CapTop;

		// Token: 0x0400097E RID: 2430
		public readonly Hashtable CapNames;

		// Token: 0x0400097F RID: 2431
		public readonly string[] CapsList;

		// Token: 0x04000980 RID: 2432
		public readonly RegexOptions Options;
	}
}
