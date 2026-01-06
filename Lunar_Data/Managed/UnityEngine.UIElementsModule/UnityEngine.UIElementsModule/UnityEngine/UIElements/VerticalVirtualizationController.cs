using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Pool;

namespace UnityEngine.UIElements
{
	// Token: 0x02000114 RID: 276
	internal abstract class VerticalVirtualizationController<T> : CollectionVirtualizationController where T : ReusableCollectionItem, new()
	{
		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x060008DA RID: 2266 RVA: 0x00021BB9 File Offset: 0x0001FDB9
		public override IEnumerable<ReusableCollectionItem> activeItems
		{
			get
			{
				return this.m_ActiveItems as IEnumerable<ReusableCollectionItem>;
			}
		}

		// Token: 0x060008DB RID: 2267 RVA: 0x00021BC8 File Offset: 0x0001FDC8
		protected virtual bool VisibleItemPredicate(T i)
		{
			bool flag = false;
			ListViewDraggerAnimated listViewDraggerAnimated = this.m_ListView.dragger as ListViewDraggerAnimated;
			bool flag2 = listViewDraggerAnimated != null;
			if (flag2)
			{
				flag = listViewDraggerAnimated.isDragging && i.index == listViewDraggerAnimated.draggedItem.index;
			}
			return i.rootElement.style.display == DisplayStyle.Flex && !flag;
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x060008DC RID: 2268 RVA: 0x00021C42 File Offset: 0x0001FE42
		internal T firstVisibleItem
		{
			get
			{
				return Enumerable.FirstOrDefault<T>(this.m_ActiveItems, this.m_VisibleItemPredicateDelegate);
			}
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x060008DD RID: 2269 RVA: 0x00021C55 File Offset: 0x0001FE55
		internal T lastVisibleItem
		{
			get
			{
				return Enumerable.LastOrDefault<T>(this.m_ActiveItems, this.m_VisibleItemPredicateDelegate);
			}
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x060008DE RID: 2270 RVA: 0x00021C68 File Offset: 0x0001FE68
		public override int visibleItemCount
		{
			get
			{
				return Enumerable.Count<T>(this.m_ActiveItems, this.m_VisibleItemPredicateDelegate);
			}
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x060008DF RID: 2271 RVA: 0x00021C7B File Offset: 0x0001FE7B
		public override int firstVisibleIndex
		{
			get
			{
				return this.m_FirstVisibleIndex;
			}
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x060008E0 RID: 2272 RVA: 0x00021C83 File Offset: 0x0001FE83
		public override int lastVisibleIndex
		{
			get
			{
				T t = this.lastVisibleItem;
				return (t != null) ? t.index : (-1);
			}
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x060008E1 RID: 2273 RVA: 0x00021C9C File Offset: 0x0001FE9C
		protected float lastHeight
		{
			get
			{
				return this.m_ListView.lastHeight;
			}
		}

		// Token: 0x060008E2 RID: 2274 RVA: 0x00021CAC File Offset: 0x0001FEAC
		protected VerticalVirtualizationController(BaseVerticalCollectionView collectionView)
			: base(collectionView.scrollView)
		{
			this.m_ListView = collectionView;
			this.m_ActiveItems = new List<T>();
			this.m_VisibleItemPredicateDelegate = new Func<T, bool>(this.VisibleItemPredicate);
			this.k_EmptyRows = new VisualElement();
			this.k_EmptyRows.AddToClassList(BaseVerticalCollectionView.backgroundFillUssClassName);
		}

		// Token: 0x060008E3 RID: 2275 RVA: 0x00021D78 File Offset: 0x0001FF78
		public override void Refresh(bool rebuild)
		{
			bool flag = this.m_ListView.HasValidDataAndBindings();
			for (int i = 0; i < this.m_ActiveItems.Count; i++)
			{
				int num = this.m_FirstVisibleIndex + i;
				T t = this.m_ActiveItems[i];
				bool flag2 = t.rootElement.style.display == DisplayStyle.Flex;
				if (rebuild)
				{
					bool flag3 = flag && flag2;
					if (flag3)
					{
						this.m_ListView.viewController.InvokeUnbindItem(t, t.index);
						this.m_ListView.viewController.InvokeDestroyItem(t);
					}
					this.m_Pool.Release(t);
				}
				else
				{
					bool flag4 = num >= 0 && num < this.m_ListView.itemsSource.Count;
					if (flag4)
					{
						bool flag5 = flag && flag2;
						if (flag5)
						{
							this.m_ListView.viewController.InvokeUnbindItem(t, t.index);
							t.index = -1;
							this.Setup(t, num);
						}
					}
					else
					{
						bool flag6 = flag2;
						if (flag6)
						{
							this.ReleaseItem(i--);
						}
					}
				}
			}
			if (rebuild)
			{
				this.m_Pool.Clear();
				this.m_ActiveItems.Clear();
				this.m_ScrollView.Clear();
			}
		}

		// Token: 0x060008E4 RID: 2276 RVA: 0x00021EF8 File Offset: 0x000200F8
		protected void Setup(T recycledItem, int newIndex)
		{
			ListViewDraggerAnimated listViewDraggerAnimated = this.m_ListView.dragger as ListViewDraggerAnimated;
			bool flag = listViewDraggerAnimated != null;
			if (flag)
			{
				bool flag2 = listViewDraggerAnimated.isDragging && (listViewDraggerAnimated.draggedItem.index == newIndex || listViewDraggerAnimated.draggedItem == recycledItem);
				if (flag2)
				{
					return;
				}
			}
			bool flag3 = newIndex >= this.m_ListView.itemsSource.Count;
			if (flag3)
			{
				recycledItem.rootElement.style.display = DisplayStyle.None;
				bool flag4 = recycledItem.index >= 0 && recycledItem.index < this.m_ListView.itemsSource.Count;
				if (flag4)
				{
					this.m_ListView.viewController.InvokeUnbindItem(recycledItem, recycledItem.index);
					recycledItem.index = -1;
				}
			}
			else
			{
				recycledItem.rootElement.style.display = DisplayStyle.Flex;
				bool flag5 = recycledItem.index == newIndex;
				if (!flag5)
				{
					bool flag6 = this.m_ListView.showAlternatingRowBackgrounds != AlternatingRowBackground.None && newIndex % 2 == 1;
					recycledItem.rootElement.EnableInClassList(BaseVerticalCollectionView.itemAlternativeBackgroundUssClassName, flag6);
					int index = recycledItem.index;
					int idForIndex = this.m_ListView.viewController.GetIdForIndex(newIndex);
					bool flag7 = recycledItem.index != -1;
					if (flag7)
					{
						this.m_ListView.viewController.InvokeUnbindItem(recycledItem, recycledItem.index);
					}
					recycledItem.index = newIndex;
					recycledItem.id = idForIndex;
					int num = newIndex - this.m_FirstVisibleIndex;
					bool flag8 = num >= this.m_ScrollView.contentContainer.childCount;
					if (flag8)
					{
						recycledItem.rootElement.BringToFront();
					}
					else
					{
						bool flag9 = num >= 0;
						if (flag9)
						{
							recycledItem.rootElement.PlaceBehind(this.m_ScrollView.contentContainer[num]);
						}
						else
						{
							recycledItem.rootElement.SendToBack();
						}
					}
					this.m_ListView.viewController.InvokeBindItem(recycledItem, newIndex);
					this.HandleFocus(recycledItem, index);
				}
			}
		}

		// Token: 0x060008E5 RID: 2277 RVA: 0x0002217C File Offset: 0x0002037C
		public override void OnFocus(VisualElement leafTarget)
		{
			bool flag = leafTarget == this.m_ScrollView.contentContainer;
			if (!flag)
			{
				this.m_LastFocusedElementTreeChildIndexes.Clear();
				bool flag2 = this.m_ScrollView.contentContainer.FindElementInTree(leafTarget, this.m_LastFocusedElementTreeChildIndexes);
				if (flag2)
				{
					VisualElement visualElement = this.m_ScrollView.contentContainer[this.m_LastFocusedElementTreeChildIndexes[0]];
					foreach (ReusableCollectionItem reusableCollectionItem in this.activeItems)
					{
						bool flag3 = reusableCollectionItem.rootElement == visualElement;
						if (flag3)
						{
							this.m_LastFocusedElementIndex = reusableCollectionItem.index;
							break;
						}
					}
					this.m_LastFocusedElementTreeChildIndexes.RemoveAt(0);
				}
				else
				{
					this.m_LastFocusedElementIndex = -1;
				}
			}
		}

		// Token: 0x060008E6 RID: 2278 RVA: 0x00022260 File Offset: 0x00020460
		public override void OnBlur(VisualElement willFocus)
		{
			bool flag = willFocus == null || willFocus != this.m_ScrollView.contentContainer;
			if (flag)
			{
				this.m_LastFocusedElementTreeChildIndexes.Clear();
				this.m_LastFocusedElementIndex = -1;
			}
		}

		// Token: 0x060008E7 RID: 2279 RVA: 0x000222A0 File Offset: 0x000204A0
		private void HandleFocus(ReusableCollectionItem recycledItem, int previousIndex)
		{
			bool flag = this.m_LastFocusedElementIndex == -1;
			if (!flag)
			{
				bool flag2 = this.m_LastFocusedElementIndex == recycledItem.index;
				if (flag2)
				{
					VisualElement visualElement = recycledItem.rootElement.ElementAtTreePath(this.m_LastFocusedElementTreeChildIndexes);
					if (visualElement != null)
					{
						visualElement.Focus();
					}
				}
				else
				{
					bool flag3 = this.m_LastFocusedElementIndex != previousIndex;
					if (flag3)
					{
						VisualElement visualElement2 = recycledItem.rootElement.ElementAtTreePath(this.m_LastFocusedElementTreeChildIndexes);
						if (visualElement2 != null)
						{
							visualElement2.Blur();
						}
					}
					else
					{
						this.m_ScrollView.contentContainer.Focus();
					}
				}
			}
		}

		// Token: 0x060008E8 RID: 2280 RVA: 0x00022330 File Offset: 0x00020530
		public override void UpdateBackground()
		{
			float num = (float.IsNaN(this.k_EmptyRows.layout.size.y) ? 0f : this.k_EmptyRows.layout.size.y);
			float num2 = this.m_ScrollView.contentViewport.layout.size.y - this.m_ScrollView.contentContainer.layout.size.y - num;
			bool flag = this.m_ListView.showAlternatingRowBackgrounds != AlternatingRowBackground.All || num2 <= 0f;
			if (flag)
			{
				this.k_EmptyRows.RemoveFromHierarchy();
			}
			else
			{
				bool flag2 = this.lastVisibleItem == null;
				if (!flag2)
				{
					bool flag3 = this.k_EmptyRows.parent == null;
					if (flag3)
					{
						this.m_ScrollView.contentViewport.Add(this.k_EmptyRows);
					}
					float itemHeight = this.GetItemHeight(-1);
					int num3 = Mathf.FloorToInt(num2 / itemHeight) + 1;
					bool flag4 = num3 > this.k_EmptyRows.childCount;
					if (flag4)
					{
						int num4 = num3 - this.k_EmptyRows.childCount;
						for (int i = 0; i < num4; i++)
						{
							VisualElement visualElement = new VisualElement();
							visualElement.style.flexShrink = 0f;
							this.k_EmptyRows.Add(visualElement);
						}
					}
					int num5 = this.lastVisibleIndex;
					int childCount = this.k_EmptyRows.hierarchy.childCount;
					for (int j = 0; j < childCount; j++)
					{
						VisualElement visualElement2 = this.k_EmptyRows.hierarchy[j];
						num5++;
						visualElement2.style.height = itemHeight;
						visualElement2.EnableInClassList(BaseVerticalCollectionView.itemAlternativeBackgroundUssClassName, num5 % 2 == 1);
					}
				}
			}
		}

		// Token: 0x060008E9 RID: 2281 RVA: 0x00022530 File Offset: 0x00020730
		public override void ReplaceActiveItem(int index)
		{
			int num = 0;
			foreach (T t in this.m_ActiveItems)
			{
				bool flag = t.index == index;
				if (flag)
				{
					T orMakeItem = this.GetOrMakeItem();
					t.DetachElement();
					this.m_ActiveItems.Remove(t);
					this.m_ListView.viewController.InvokeUnbindItem(t, index);
					this.m_ListView.viewController.InvokeDestroyItem(t);
					this.m_ActiveItems.Insert(num, orMakeItem);
					this.m_ScrollView.Add(orMakeItem.rootElement);
					this.Setup(orMakeItem, index);
					break;
				}
				num++;
			}
		}

		// Token: 0x060008EA RID: 2282 RVA: 0x00022628 File Offset: 0x00020828
		internal virtual T GetOrMakeItem()
		{
			T t = this.m_Pool.Get();
			bool flag = t.rootElement == null;
			if (flag)
			{
				this.m_ListView.viewController.InvokeMakeItem(t);
			}
			t.PreAttachElement();
			return t;
		}

		// Token: 0x060008EB RID: 2283 RVA: 0x00022680 File Offset: 0x00020880
		internal virtual void ReleaseItem(int activeItemsIndex)
		{
			T t = this.m_ActiveItems[activeItemsIndex];
			int index = t.index;
			bool flag = index >= 0 && index < this.m_ListView.itemsSource.Count;
			if (flag)
			{
				this.m_ListView.viewController.InvokeUnbindItem(t, index);
			}
			this.m_Pool.Release(t);
			this.m_ActiveItems.RemoveAt(activeItemsIndex);
		}

		// Token: 0x040003A0 RID: 928
		protected BaseVerticalCollectionView m_ListView;

		// Token: 0x040003A1 RID: 929
		protected const int k_ExtraVisibleItems = 2;

		// Token: 0x040003A2 RID: 930
		protected readonly ObjectPool<T> m_Pool = new ObjectPool<T>(() => new T(), null, delegate(T i)
		{
			i.DetachElement();
		}, null, true, 10, 10000);

		// Token: 0x040003A3 RID: 931
		protected List<T> m_ActiveItems;

		// Token: 0x040003A4 RID: 932
		private int m_LastFocusedElementIndex = -1;

		// Token: 0x040003A5 RID: 933
		private List<int> m_LastFocusedElementTreeChildIndexes = new List<int>();

		// Token: 0x040003A6 RID: 934
		protected int m_FirstVisibleIndex;

		// Token: 0x040003A7 RID: 935
		private Func<T, bool> m_VisibleItemPredicateDelegate;

		// Token: 0x040003A8 RID: 936
		protected List<T> m_ScrollInsertionList = new List<T>();

		// Token: 0x040003A9 RID: 937
		private readonly VisualElement k_EmptyRows;
	}
}
