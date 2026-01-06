using System;
using System.Collections.Generic;
using System.Linq;

namespace UnityEngine.UIElements
{
	// Token: 0x02000191 RID: 401
	internal readonly struct TreeItem
	{
		// Token: 0x1700029D RID: 669
		// (get) Token: 0x06000CF2 RID: 3314 RVA: 0x00035170 File Offset: 0x00033370
		public int id { get; }

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x06000CF3 RID: 3315 RVA: 0x00035178 File Offset: 0x00033378
		public int parentId { get; }

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x06000CF4 RID: 3316 RVA: 0x00035180 File Offset: 0x00033380
		public IEnumerable<int> childrenIds { get; }

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x06000CF5 RID: 3317 RVA: 0x00035188 File Offset: 0x00033388
		public bool hasChildren
		{
			get
			{
				return this.childrenIds != null && Enumerable.Any<int>(this.childrenIds);
			}
		}

		// Token: 0x06000CF6 RID: 3318 RVA: 0x000351A0 File Offset: 0x000333A0
		public TreeItem(int id, int parentId = -1, IEnumerable<int> childrenIds = null)
		{
			this.id = id;
			this.parentId = parentId;
			this.childrenIds = childrenIds;
		}

		// Token: 0x040005EF RID: 1519
		public const int invalidId = -1;
	}
}
