using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x0200010D RID: 269
	internal abstract class CollectionVirtualizationController
	{
		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x06000880 RID: 2176
		public abstract int firstVisibleIndex { get; }

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x06000881 RID: 2177
		public abstract int lastVisibleIndex { get; }

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x06000882 RID: 2178
		public abstract int visibleItemCount { get; }

		// Token: 0x06000883 RID: 2179 RVA: 0x0001F82C File Offset: 0x0001DA2C
		protected CollectionVirtualizationController(ScrollView scrollView)
		{
			this.m_ScrollView = scrollView;
		}

		// Token: 0x06000884 RID: 2180
		public abstract void Refresh(bool rebuild);

		// Token: 0x06000885 RID: 2181
		public abstract void ScrollToItem(int id);

		// Token: 0x06000886 RID: 2182
		public abstract void Resize(Vector2 size, int layoutPass);

		// Token: 0x06000887 RID: 2183
		public abstract void OnScroll(Vector2 offset);

		// Token: 0x06000888 RID: 2184
		public abstract int GetIndexFromPosition(Vector2 position);

		// Token: 0x06000889 RID: 2185
		public abstract float GetItemHeight(int index);

		// Token: 0x0600088A RID: 2186
		public abstract void OnFocus(VisualElement leafTarget);

		// Token: 0x0600088B RID: 2187
		public abstract void OnBlur(VisualElement willFocus);

		// Token: 0x0600088C RID: 2188
		public abstract void UpdateBackground();

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x0600088D RID: 2189
		public abstract IEnumerable<ReusableCollectionItem> activeItems { get; }

		// Token: 0x0600088E RID: 2190
		public abstract void ReplaceActiveItem(int index);

		// Token: 0x0400037D RID: 893
		protected readonly ScrollView m_ScrollView;
	}
}
