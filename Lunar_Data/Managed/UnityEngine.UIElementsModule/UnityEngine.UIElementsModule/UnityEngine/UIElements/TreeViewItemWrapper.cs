using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x02000192 RID: 402
	internal readonly struct TreeViewItemWrapper
	{
		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x06000CF7 RID: 3319 RVA: 0x000351B8 File Offset: 0x000333B8
		public int id
		{
			get
			{
				return this.item.id;
			}
		}

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x06000CF8 RID: 3320 RVA: 0x000351C5 File Offset: 0x000333C5
		public int parentId
		{
			get
			{
				return this.item.parentId;
			}
		}

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x06000CF9 RID: 3321 RVA: 0x000351D2 File Offset: 0x000333D2
		public IEnumerable<int> childrenIds
		{
			get
			{
				return this.item.childrenIds;
			}
		}

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06000CFA RID: 3322 RVA: 0x000351DF File Offset: 0x000333DF
		public bool hasChildren
		{
			get
			{
				return this.item.hasChildren;
			}
		}

		// Token: 0x06000CFB RID: 3323 RVA: 0x000351EC File Offset: 0x000333EC
		public TreeViewItemWrapper(TreeItem item, int depth)
		{
			this.item = item;
			this.depth = depth;
		}

		// Token: 0x040005F3 RID: 1523
		public readonly TreeItem item;

		// Token: 0x040005F4 RID: 1524
		public readonly int depth;
	}
}
