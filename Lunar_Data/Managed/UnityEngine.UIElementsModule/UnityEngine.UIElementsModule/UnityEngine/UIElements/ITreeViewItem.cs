using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x020001A0 RID: 416
	internal interface ITreeViewItem
	{
		// Token: 0x170002CB RID: 715
		// (get) Token: 0x06000D91 RID: 3473
		int id { get; }

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x06000D92 RID: 3474
		ITreeViewItem parent { get; }

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x06000D93 RID: 3475
		IEnumerable<ITreeViewItem> children { get; }

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x06000D94 RID: 3476
		bool hasChildren { get; }

		// Token: 0x06000D95 RID: 3477
		void AddChild(ITreeViewItem child);

		// Token: 0x06000D96 RID: 3478
		void AddChildren(IList<ITreeViewItem> children);

		// Token: 0x06000D97 RID: 3479
		void RemoveChild(ITreeViewItem child);
	}
}
