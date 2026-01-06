using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine.Assertions;

namespace UnityEngine.UIElements
{
	// Token: 0x0200019A RID: 410
	internal class InternalTreeView : VisualElement
	{
		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x06000D37 RID: 3383 RVA: 0x00036798 File Offset: 0x00034998
		// (set) Token: 0x06000D38 RID: 3384 RVA: 0x000367B0 File Offset: 0x000349B0
		public Func<VisualElement> makeItem
		{
			get
			{
				return this.m_MakeItem;
			}
			set
			{
				bool flag = this.m_MakeItem == value;
				if (!flag)
				{
					this.m_MakeItem = value;
					this.m_ListView.Rebuild();
				}
			}
		}

		// Token: 0x14000028 RID: 40
		// (add) Token: 0x06000D39 RID: 3385 RVA: 0x000367E4 File Offset: 0x000349E4
		// (remove) Token: 0x06000D3A RID: 3386 RVA: 0x0003681C File Offset: 0x00034A1C
		[field: DebuggerBrowsable(0)]
		public event Action<IEnumerable<ITreeViewItem>> onItemsChosen;

		// Token: 0x14000029 RID: 41
		// (add) Token: 0x06000D3B RID: 3387 RVA: 0x00036854 File Offset: 0x00034A54
		// (remove) Token: 0x06000D3C RID: 3388 RVA: 0x0003688C File Offset: 0x00034A8C
		[field: DebuggerBrowsable(0)]
		public event Action<IEnumerable<ITreeViewItem>> onSelectionChange;

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x06000D3D RID: 3389 RVA: 0x000368C1 File Offset: 0x00034AC1
		public ITreeViewItem selectedItem
		{
			get
			{
				return (this.m_SelectedItems.Count == 0) ? null : Enumerable.First<ITreeViewItem>(this.m_SelectedItems);
			}
		}

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x06000D3E RID: 3390 RVA: 0x000368E0 File Offset: 0x00034AE0
		public IEnumerable<ITreeViewItem> selectedItems
		{
			get
			{
				bool flag = this.m_SelectedItems != null;
				IEnumerable<ITreeViewItem> enumerable;
				if (flag)
				{
					enumerable = this.m_SelectedItems;
				}
				else
				{
					this.m_SelectedItems = new List<ITreeViewItem>();
					foreach (ITreeViewItem treeViewItem in this.items)
					{
						foreach (int num in this.m_ListView.currentSelectionIds)
						{
							bool flag2 = treeViewItem.id == num;
							if (flag2)
							{
								this.m_SelectedItems.Add(treeViewItem);
							}
						}
					}
					enumerable = this.m_SelectedItems;
				}
				return enumerable;
			}
		}

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x06000D3F RID: 3391 RVA: 0x000369BC File Offset: 0x00034BBC
		// (set) Token: 0x06000D40 RID: 3392 RVA: 0x000369D4 File Offset: 0x00034BD4
		public Action<VisualElement, ITreeViewItem> bindItem
		{
			get
			{
				return this.m_BindItem;
			}
			set
			{
				this.m_BindItem = value;
				this.ListViewRefresh();
			}
		}

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x06000D41 RID: 3393 RVA: 0x000369E5 File Offset: 0x00034BE5
		// (set) Token: 0x06000D42 RID: 3394 RVA: 0x000369ED File Offset: 0x00034BED
		public Action<VisualElement, ITreeViewItem> unbindItem { get; set; }

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x06000D43 RID: 3395 RVA: 0x000369F8 File Offset: 0x00034BF8
		// (set) Token: 0x06000D44 RID: 3396 RVA: 0x00036A10 File Offset: 0x00034C10
		public IList<ITreeViewItem> rootItems
		{
			get
			{
				return this.m_RootItems;
			}
			set
			{
				this.m_RootItems = value;
				this.Rebuild();
			}
		}

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x06000D45 RID: 3397 RVA: 0x00036A21 File Offset: 0x00034C21
		public IEnumerable<ITreeViewItem> items
		{
			get
			{
				return InternalTreeView.GetAllItems(this.m_RootItems);
			}
		}

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x06000D46 RID: 3398 RVA: 0x00036A2E File Offset: 0x00034C2E
		public float resolvedItemHeight
		{
			get
			{
				return this.m_ListView.ResolveItemHeight(-1f);
			}
		}

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x06000D47 RID: 3399 RVA: 0x00036A40 File Offset: 0x00034C40
		// (set) Token: 0x06000D48 RID: 3400 RVA: 0x00036A5E File Offset: 0x00034C5E
		public int itemHeight
		{
			get
			{
				return (int)this.m_ListView.fixedItemHeight;
			}
			set
			{
				this.m_ListView.fixedItemHeight = (float)value;
			}
		}

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x06000D49 RID: 3401 RVA: 0x00036A70 File Offset: 0x00034C70
		// (set) Token: 0x06000D4A RID: 3402 RVA: 0x00036A8D File Offset: 0x00034C8D
		public bool horizontalScrollingEnabled
		{
			get
			{
				return this.m_ListView.horizontalScrollingEnabled;
			}
			set
			{
				this.m_ListView.horizontalScrollingEnabled = value;
			}
		}

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x06000D4B RID: 3403 RVA: 0x00036AA0 File Offset: 0x00034CA0
		// (set) Token: 0x06000D4C RID: 3404 RVA: 0x00036ABD File Offset: 0x00034CBD
		public bool showBorder
		{
			get
			{
				return this.m_ListView.showBorder;
			}
			set
			{
				this.m_ListView.showBorder = value;
			}
		}

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x06000D4D RID: 3405 RVA: 0x00036AD0 File Offset: 0x00034CD0
		// (set) Token: 0x06000D4E RID: 3406 RVA: 0x00036AED File Offset: 0x00034CED
		public SelectionType selectionType
		{
			get
			{
				return this.m_ListView.selectionType;
			}
			set
			{
				this.m_ListView.selectionType = value;
			}
		}

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x06000D4F RID: 3407 RVA: 0x00036B00 File Offset: 0x00034D00
		// (set) Token: 0x06000D50 RID: 3408 RVA: 0x00036B1D File Offset: 0x00034D1D
		public AlternatingRowBackground showAlternatingRowBackgrounds
		{
			get
			{
				return this.m_ListView.showAlternatingRowBackgrounds;
			}
			set
			{
				this.m_ListView.showAlternatingRowBackgrounds = value;
			}
		}

		// Token: 0x06000D51 RID: 3409 RVA: 0x00036B30 File Offset: 0x00034D30
		public InternalTreeView()
		{
			this.m_SelectedItems = null;
			this.m_ExpandedItemIds = new List<int>();
			this.m_ItemWrappers = new List<InternalTreeView.TreeViewItemWrapper>();
			this.m_ListView = new ListView();
			this.m_ListView.name = InternalTreeView.s_ListViewName;
			this.m_ListView.itemsSource = this.m_ItemWrappers;
			this.m_ListView.viewDataKey = InternalTreeView.s_ListViewName;
			this.m_ListView.AddToClassList(InternalTreeView.s_ListViewName);
			base.hierarchy.Add(this.m_ListView);
			this.m_ListView.makeItem = new Func<VisualElement>(this.MakeTreeItem);
			this.m_ListView.bindItem = new Action<VisualElement, int>(this.BindTreeItem);
			this.m_ListView.unbindItem = new Action<VisualElement, int>(this.UnbindTreeItem);
			this.m_ListView.getItemId = new Func<int, int>(this.GetItemId);
			this.m_ListView.onItemsChosen += new Action<IEnumerable<object>>(this.OnItemsChosen);
			this.m_ListView.onSelectionChange += new Action<IEnumerable<object>>(this.OnSelectionChange);
			this.m_ScrollView = this.m_ListView.scrollView;
			this.m_ScrollView.contentContainer.RegisterCallback<KeyDownEvent>(new EventCallback<KeyDownEvent>(this.OnKeyDown), TrickleDown.NoTrickleDown);
			base.RegisterCallback<MouseUpEvent>(new EventCallback<MouseUpEvent>(this.OnTreeViewMouseUp), TrickleDown.TrickleDown);
			base.RegisterCallback<CustomStyleResolvedEvent>(new EventCallback<CustomStyleResolvedEvent>(this.OnCustomStyleResolved), TrickleDown.NoTrickleDown);
		}

		// Token: 0x06000D52 RID: 3410 RVA: 0x00036CAE File Offset: 0x00034EAE
		public InternalTreeView(IList<ITreeViewItem> items, int fixedItemHeight, Func<VisualElement> makeItem, Action<VisualElement, ITreeViewItem> bindItem)
			: this()
		{
			this.m_ListView.fixedItemHeight = (float)fixedItemHeight;
			this.m_MakeItem = makeItem;
			this.m_BindItem = bindItem;
			this.m_RootItems = items;
			this.Rebuild();
		}

		// Token: 0x06000D53 RID: 3411 RVA: 0x00036CE3 File Offset: 0x00034EE3
		public void RefreshItems()
		{
			this.RegenerateWrappers();
			this.ListViewRefresh();
		}

		// Token: 0x06000D54 RID: 3412 RVA: 0x00036CF4 File Offset: 0x00034EF4
		public void Rebuild()
		{
			this.RegenerateWrappers();
			this.m_ListView.Rebuild();
		}

		// Token: 0x06000D55 RID: 3413 RVA: 0x00036D0C File Offset: 0x00034F0C
		internal override void OnViewDataReady()
		{
			base.OnViewDataReady();
			string fullHierarchicalViewDataKey = base.GetFullHierarchicalViewDataKey();
			base.OverwriteFromViewData(this, fullHierarchicalViewDataKey);
			this.Rebuild();
		}

		// Token: 0x06000D56 RID: 3414 RVA: 0x00036D38 File Offset: 0x00034F38
		public static IEnumerable<ITreeViewItem> GetAllItems(IEnumerable<ITreeViewItem> rootItems)
		{
			bool flag = rootItems == null;
			if (flag)
			{
				yield break;
			}
			Stack<IEnumerator<ITreeViewItem>> iteratorStack = new Stack<IEnumerator<ITreeViewItem>>();
			IEnumerator<ITreeViewItem> currentIterator = rootItems.GetEnumerator();
			for (;;)
			{
				bool hasNext = currentIterator.MoveNext();
				bool flag2 = !hasNext;
				if (flag2)
				{
					bool flag3 = iteratorStack.Count > 0;
					if (!flag3)
					{
						break;
					}
					currentIterator = iteratorStack.Pop();
				}
				else
				{
					ITreeViewItem currentItem = currentIterator.Current;
					yield return currentItem;
					bool hasChildren = currentItem.hasChildren;
					if (hasChildren)
					{
						iteratorStack.Push(currentIterator);
						currentIterator = currentItem.children.GetEnumerator();
					}
					currentItem = null;
				}
			}
			yield break;
		}

		// Token: 0x06000D57 RID: 3415 RVA: 0x00036D48 File Offset: 0x00034F48
		public void OnKeyDown(KeyDownEvent evt)
		{
			int selectedIndex = this.m_ListView.selectedIndex;
			bool flag = true;
			KeyCode keyCode = evt.keyCode;
			KeyCode keyCode2 = keyCode;
			if (keyCode2 != KeyCode.RightArrow)
			{
				if (keyCode2 != KeyCode.LeftArrow)
				{
					flag = false;
				}
				else
				{
					bool flag2 = this.IsExpandedByIndex(selectedIndex);
					if (flag2)
					{
						this.CollapseItemByIndex(selectedIndex);
					}
				}
			}
			else
			{
				bool flag3 = !this.IsExpandedByIndex(selectedIndex);
				if (flag3)
				{
					this.ExpandItemByIndex(selectedIndex);
				}
			}
			bool flag4 = flag;
			if (flag4)
			{
				evt.StopPropagation();
			}
		}

		// Token: 0x06000D58 RID: 3416 RVA: 0x00036DC4 File Offset: 0x00034FC4
		public void SetSelection(int id)
		{
			this.SetSelection(new int[] { id });
		}

		// Token: 0x06000D59 RID: 3417 RVA: 0x00036DD8 File Offset: 0x00034FD8
		public void SetSelection(IEnumerable<int> ids)
		{
			this.SetSelectionInternal(ids, true);
		}

		// Token: 0x06000D5A RID: 3418 RVA: 0x00036DE4 File Offset: 0x00034FE4
		public void SetSelectionWithoutNotify(IEnumerable<int> ids)
		{
			this.SetSelectionInternal(ids, false);
		}

		// Token: 0x06000D5B RID: 3419 RVA: 0x00036DF0 File Offset: 0x00034FF0
		internal void SetSelectionInternal(IEnumerable<int> ids, bool sendNotification)
		{
			bool flag = ids == null;
			if (!flag)
			{
				List<int> list = Enumerable.ToList<int>(Enumerable.Select<int, int>(ids, (int id) => this.GetItemIndex(id, true)));
				this.ListViewRefresh();
				this.m_ListView.SetSelectionInternal(list, sendNotification);
			}
		}

		// Token: 0x06000D5C RID: 3420 RVA: 0x00036E38 File Offset: 0x00035038
		internal void SetSelectionByIndices(IEnumerable<int> indexes, bool sendNotification)
		{
			bool flag = indexes == null;
			if (!flag)
			{
				this.ListViewRefresh();
				this.m_ListView.SetSelectionInternal(indexes, sendNotification);
			}
		}

		// Token: 0x06000D5D RID: 3421 RVA: 0x00036E68 File Offset: 0x00035068
		public void AddToSelection(int id)
		{
			int itemIndex = this.GetItemIndex(id, true);
			this.ListViewRefresh();
			this.m_ListView.AddToSelection(itemIndex);
		}

		// Token: 0x06000D5E RID: 3422 RVA: 0x00036E94 File Offset: 0x00035094
		public void RemoveFromSelection(int id)
		{
			int itemIndex = this.GetItemIndex(id, false);
			this.m_ListView.RemoveFromSelection(itemIndex);
		}

		// Token: 0x06000D5F RID: 3423 RVA: 0x00036EB8 File Offset: 0x000350B8
		internal int GetItemIndex(int id, bool expand = false)
		{
			ITreeViewItem treeViewItem = this.FindItem(id);
			bool flag = treeViewItem == null;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("id", id, "InternalTreeView: Item id not found.");
			}
			if (expand)
			{
				bool flag2 = false;
				for (ITreeViewItem treeViewItem2 = treeViewItem.parent; treeViewItem2 != null; treeViewItem2 = treeViewItem2.parent)
				{
					bool flag3 = !this.m_ExpandedItemIds.Contains(treeViewItem2.id);
					if (flag3)
					{
						this.m_ExpandedItemIds.Add(treeViewItem2.id);
						flag2 = true;
					}
				}
				bool flag4 = flag2;
				if (flag4)
				{
					this.RegenerateWrappers();
				}
			}
			int i;
			for (i = 0; i < this.m_ItemWrappers.Count; i++)
			{
				bool flag5 = this.m_ItemWrappers[i].id == id;
				if (flag5)
				{
					break;
				}
			}
			return i;
		}

		// Token: 0x06000D60 RID: 3424 RVA: 0x00036F9B File Offset: 0x0003519B
		public void ClearSelection()
		{
			this.m_ListView.ClearSelection();
		}

		// Token: 0x06000D61 RID: 3425 RVA: 0x00036FAA File Offset: 0x000351AA
		public void ScrollTo(VisualElement visualElement)
		{
			this.m_ListView.ScrollTo(visualElement);
		}

		// Token: 0x06000D62 RID: 3426 RVA: 0x00036FBC File Offset: 0x000351BC
		public void ScrollToItem(int id)
		{
			int itemIndex = this.GetItemIndex(id, true);
			this.RefreshItems();
			this.m_ListView.ScrollToItem(itemIndex);
		}

		// Token: 0x06000D63 RID: 3427 RVA: 0x00036FE8 File Offset: 0x000351E8
		internal void CopyExpandedStates(ITreeViewItem source, ITreeViewItem target)
		{
			bool flag = this.IsExpanded(source.id);
			if (flag)
			{
				this.ExpandItem(target.id);
				bool flag2 = source.children != null && Enumerable.Count<ITreeViewItem>(source.children) > 0;
				if (flag2)
				{
					bool flag3 = target.children == null || Enumerable.Count<ITreeViewItem>(source.children) != Enumerable.Count<ITreeViewItem>(target.children);
					if (flag3)
					{
						Debug.LogWarning("Source and target hierarchies are not the same");
					}
					else
					{
						for (int i = 0; i < Enumerable.Count<ITreeViewItem>(source.children); i++)
						{
							ITreeViewItem treeViewItem = Enumerable.ElementAt<ITreeViewItem>(source.children, i);
							ITreeViewItem treeViewItem2 = Enumerable.ElementAt<ITreeViewItem>(target.children, i);
							this.CopyExpandedStates(treeViewItem, treeViewItem2);
						}
					}
				}
			}
			else
			{
				this.CollapseItem(target.id);
			}
		}

		// Token: 0x06000D64 RID: 3428 RVA: 0x000370C8 File Offset: 0x000352C8
		public bool IsExpanded(int id)
		{
			return this.m_ExpandedItemIds.Contains(id);
		}

		// Token: 0x06000D65 RID: 3429 RVA: 0x000370E8 File Offset: 0x000352E8
		public void CollapseItem(int id)
		{
			bool flag = this.FindItem(id) == null;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("id", id, "InternalTreeView: Item id not found.");
			}
			for (int i = 0; i < this.m_ItemWrappers.Count; i++)
			{
				bool flag2 = this.m_ItemWrappers[i].item.id == id;
				if (flag2)
				{
					bool flag3 = this.IsExpandedByIndex(i);
					if (flag3)
					{
						this.CollapseItemByIndex(i);
						return;
					}
				}
			}
			bool flag4 = !this.m_ExpandedItemIds.Contains(id);
			if (flag4)
			{
				return;
			}
			this.m_ExpandedItemIds.Remove(id);
			this.RefreshItems();
		}

		// Token: 0x06000D66 RID: 3430 RVA: 0x00037194 File Offset: 0x00035394
		public void ExpandItem(int id)
		{
			bool flag = this.FindItem(id) == null;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("id", id, "InternalTreeView: Item id not found.");
			}
			for (int i = 0; i < this.m_ItemWrappers.Count; i++)
			{
				bool flag2 = this.m_ItemWrappers[i].item.id == id;
				if (flag2)
				{
					bool flag3 = !this.IsExpandedByIndex(i);
					if (flag3)
					{
						this.ExpandItemByIndex(i);
						return;
					}
				}
			}
			bool flag4 = this.m_ExpandedItemIds.Contains(id);
			if (flag4)
			{
				return;
			}
			this.m_ExpandedItemIds.Add(id);
			this.RefreshItems();
		}

		// Token: 0x06000D67 RID: 3431 RVA: 0x00037240 File Offset: 0x00035440
		public ITreeViewItem FindItem(int id)
		{
			foreach (ITreeViewItem treeViewItem in this.items)
			{
				bool flag = treeViewItem.id == id;
				if (flag)
				{
					return treeViewItem;
				}
			}
			return null;
		}

		// Token: 0x06000D68 RID: 3432 RVA: 0x000372A0 File Offset: 0x000354A0
		private void ListViewRefresh()
		{
			this.m_ListView.RefreshItems();
		}

		// Token: 0x06000D69 RID: 3433 RVA: 0x000372B0 File Offset: 0x000354B0
		private void OnItemsChosen(IEnumerable<object> chosenItems)
		{
			bool flag = this.onItemsChosen == null;
			if (!flag)
			{
				List<ITreeViewItem> list = new List<ITreeViewItem>();
				foreach (object obj in chosenItems)
				{
					InternalTreeView.TreeViewItemWrapper treeViewItemWrapper = (InternalTreeView.TreeViewItemWrapper)obj;
					list.Add(treeViewItemWrapper.item);
				}
				this.onItemsChosen.Invoke(list);
			}
		}

		// Token: 0x06000D6A RID: 3434 RVA: 0x00037330 File Offset: 0x00035530
		private void OnSelectionChange(IEnumerable<object> selectedListItems)
		{
			bool flag = this.m_SelectedItems == null;
			if (flag)
			{
				this.m_SelectedItems = new List<ITreeViewItem>();
			}
			this.m_SelectedItems.Clear();
			foreach (object obj in selectedListItems)
			{
				this.m_SelectedItems.Add(((InternalTreeView.TreeViewItemWrapper)obj).item);
			}
			Action<IEnumerable<ITreeViewItem>> action = this.onSelectionChange;
			if (action != null)
			{
				action.Invoke(this.m_SelectedItems);
			}
		}

		// Token: 0x06000D6B RID: 3435 RVA: 0x000373C8 File Offset: 0x000355C8
		private void OnTreeViewMouseUp(MouseUpEvent evt)
		{
			this.m_ScrollView.contentContainer.Focus();
		}

		// Token: 0x06000D6C RID: 3436 RVA: 0x000373DC File Offset: 0x000355DC
		private void OnItemMouseUp(MouseUpEvent evt)
		{
			bool flag = (evt.modifiers & EventModifiers.Alt) == EventModifiers.None;
			if (!flag)
			{
				VisualElement visualElement = evt.currentTarget as VisualElement;
				Toggle toggle = visualElement.Q(InternalTreeView.s_ItemToggleName, null);
				int num = (int)toggle.userData;
				ITreeViewItem item = this.m_ItemWrappers[num].item;
				bool flag2 = this.IsExpandedByIndex(num);
				bool flag3 = !item.hasChildren;
				if (!flag3)
				{
					HashSet<int> hashSet = new HashSet<int>(this.m_ExpandedItemIds);
					bool flag4 = flag2;
					if (flag4)
					{
						hashSet.Remove(item.id);
					}
					else
					{
						hashSet.Add(item.id);
					}
					foreach (ITreeViewItem treeViewItem in InternalTreeView.GetAllItems(item.children))
					{
						bool hasChildren = treeViewItem.hasChildren;
						if (hasChildren)
						{
							bool flag5 = flag2;
							if (flag5)
							{
								hashSet.Remove(treeViewItem.id);
							}
							else
							{
								hashSet.Add(treeViewItem.id);
							}
						}
					}
					this.m_ExpandedItemIds = Enumerable.ToList<int>(hashSet);
					this.RefreshItems();
					evt.StopPropagation();
				}
			}
		}

		// Token: 0x06000D6D RID: 3437 RVA: 0x00037524 File Offset: 0x00035724
		private VisualElement MakeTreeItem()
		{
			VisualElement visualElement = new VisualElement
			{
				name = InternalTreeView.itemUssClassName,
				style = 
				{
					flexDirection = FlexDirection.Row
				}
			};
			visualElement.AddToClassList(InternalTreeView.itemUssClassName);
			visualElement.RegisterCallback<MouseUpEvent>(new EventCallback<MouseUpEvent>(this.OnItemMouseUp), TrickleDown.NoTrickleDown);
			VisualElement visualElement2 = new VisualElement
			{
				name = InternalTreeView.s_ItemIndentsContainerName,
				style = 
				{
					flexDirection = FlexDirection.Row
				}
			};
			visualElement2.AddToClassList(InternalTreeView.s_ItemIndentsContainerName);
			visualElement.hierarchy.Add(visualElement2);
			Toggle toggle = new Toggle
			{
				name = InternalTreeView.s_ItemToggleName
			};
			toggle.AddToClassList(Foldout.toggleUssClassName);
			toggle.RegisterValueChangedCallback(new EventCallback<ChangeEvent<bool>>(this.ToggleExpandedState));
			visualElement.hierarchy.Add(toggle);
			VisualElement visualElement3 = new VisualElement
			{
				name = InternalTreeView.s_ItemContentContainerName,
				style = 
				{
					flexGrow = 1f
				}
			};
			visualElement3.AddToClassList(InternalTreeView.s_ItemContentContainerName);
			visualElement.Add(visualElement3);
			bool flag = this.m_MakeItem != null;
			if (flag)
			{
				visualElement3.Add(this.m_MakeItem.Invoke());
			}
			return visualElement;
		}

		// Token: 0x06000D6E RID: 3438 RVA: 0x00037660 File Offset: 0x00035860
		private void UnbindTreeItem(VisualElement element, int index)
		{
			bool flag = this.unbindItem == null;
			if (!flag)
			{
				ITreeViewItem treeViewItem = ((this.m_ItemWrappers.Count > index) ? this.m_ItemWrappers[index].item : null);
				VisualElement visualElement = element.Q(InternalTreeView.s_ItemContentContainerName, null).ElementAt(0);
				this.unbindItem.Invoke(visualElement, treeViewItem);
			}
		}

		// Token: 0x06000D6F RID: 3439 RVA: 0x000376C4 File Offset: 0x000358C4
		private void BindTreeItem(VisualElement element, int index)
		{
			ITreeViewItem item = this.m_ItemWrappers[index].item;
			VisualElement visualElement = element.Q(InternalTreeView.s_ItemIndentsContainerName, null);
			visualElement.Clear();
			for (int i = 0; i < this.m_ItemWrappers[index].depth; i++)
			{
				VisualElement visualElement2 = new VisualElement();
				visualElement2.AddToClassList(InternalTreeView.s_ItemIndentName);
				visualElement.Add(visualElement2);
			}
			Toggle toggle = element.Q(InternalTreeView.s_ItemToggleName, null);
			toggle.SetValueWithoutNotify(this.IsExpandedByIndex(index));
			toggle.userData = index;
			bool hasChildren = item.hasChildren;
			if (hasChildren)
			{
				toggle.visible = true;
			}
			else
			{
				toggle.visible = false;
			}
			bool flag = this.m_BindItem == null;
			if (!flag)
			{
				VisualElement visualElement3 = element.Q(InternalTreeView.s_ItemContentContainerName, null).ElementAt(0);
				this.m_BindItem.Invoke(visualElement3, item);
			}
		}

		// Token: 0x06000D70 RID: 3440 RVA: 0x000377B8 File Offset: 0x000359B8
		internal int GetItemId(int index)
		{
			return this.m_ItemWrappers[index].id;
		}

		// Token: 0x06000D71 RID: 3441 RVA: 0x000377E0 File Offset: 0x000359E0
		private bool IsExpandedByIndex(int index)
		{
			return this.m_ExpandedItemIds.Contains(this.m_ItemWrappers[index].id);
		}

		// Token: 0x06000D72 RID: 3442 RVA: 0x00037814 File Offset: 0x00035A14
		private void CollapseItemByIndex(int index)
		{
			bool flag = !this.m_ItemWrappers[index].item.hasChildren;
			if (!flag)
			{
				this.m_ExpandedItemIds.Remove(this.m_ItemWrappers[index].item.id);
				int num = 0;
				int num2 = index + 1;
				int depth = this.m_ItemWrappers[index].depth;
				while (num2 < this.m_ItemWrappers.Count && this.m_ItemWrappers[num2].depth > depth)
				{
					num++;
					num2++;
				}
				this.m_ItemWrappers.RemoveRange(index + 1, num);
				this.ListViewRefresh();
				base.SaveViewData();
			}
		}

		// Token: 0x06000D73 RID: 3443 RVA: 0x000378D4 File Offset: 0x00035AD4
		private void ExpandItemByIndex(int index)
		{
			bool flag = !this.m_ItemWrappers[index].item.hasChildren;
			if (!flag)
			{
				List<InternalTreeView.TreeViewItemWrapper> list = new List<InternalTreeView.TreeViewItemWrapper>();
				this.CreateWrappers(this.m_ItemWrappers[index].item.children, this.m_ItemWrappers[index].depth + 1, ref list);
				this.m_ItemWrappers.InsertRange(index + 1, list);
				this.m_ExpandedItemIds.Add(this.m_ItemWrappers[index].item.id);
				this.ListViewRefresh();
				base.SaveViewData();
			}
		}

		// Token: 0x06000D74 RID: 3444 RVA: 0x0003797C File Offset: 0x00035B7C
		private void ToggleExpandedState(ChangeEvent<bool> evt)
		{
			Toggle toggle = evt.target as Toggle;
			int num = (int)toggle.userData;
			bool flag = this.IsExpandedByIndex(num);
			Assert.AreNotEqual<bool>(flag, evt.newValue);
			bool flag2 = flag;
			if (flag2)
			{
				this.CollapseItemByIndex(num);
			}
			else
			{
				this.ExpandItemByIndex(num);
			}
			this.m_ScrollView.contentContainer.Focus();
		}

		// Token: 0x06000D75 RID: 3445 RVA: 0x000379E0 File Offset: 0x00035BE0
		private void CreateWrappers(IEnumerable<ITreeViewItem> treeViewItems, int depth, ref List<InternalTreeView.TreeViewItemWrapper> wrappers)
		{
			foreach (ITreeViewItem treeViewItem in treeViewItems)
			{
				InternalTreeView.TreeViewItemWrapper treeViewItemWrapper = new InternalTreeView.TreeViewItemWrapper
				{
					depth = depth,
					item = treeViewItem
				};
				wrappers.Add(treeViewItemWrapper);
				bool flag = this.m_ExpandedItemIds.Contains(treeViewItem.id) && treeViewItem.hasChildren;
				if (flag)
				{
					this.CreateWrappers(treeViewItem.children, depth + 1, ref wrappers);
				}
			}
		}

		// Token: 0x06000D76 RID: 3446 RVA: 0x00037A7C File Offset: 0x00035C7C
		public void CollapseAll()
		{
			bool flag = this.m_ExpandedItemIds.Count == 0;
			if (!flag)
			{
				this.m_ExpandedItemIds.Clear();
				this.RegenerateWrappers();
				this.RefreshItems();
			}
		}

		// Token: 0x06000D77 RID: 3447 RVA: 0x00037AB8 File Offset: 0x00035CB8
		private void RegenerateWrappers()
		{
			this.m_ItemWrappers.Clear();
			bool flag = this.m_RootItems == null;
			if (!flag)
			{
				this.CreateWrappers(this.m_RootItems, 0, ref this.m_ItemWrappers);
			}
		}

		// Token: 0x06000D78 RID: 3448 RVA: 0x00037AF8 File Offset: 0x00035CF8
		private void OnCustomStyleResolved(CustomStyleResolvedEvent e)
		{
			float fixedItemHeight = this.m_ListView.fixedItemHeight;
			int num;
			bool flag = !this.m_ListView.m_ItemHeightIsInline && e.customStyle.TryGetValue(BaseVerticalCollectionView.s_ItemHeightProperty, out num);
			if (flag)
			{
				this.m_ListView.m_FixedItemHeight = (float)num;
			}
			bool flag2 = this.m_ListView.m_FixedItemHeight != fixedItemHeight;
			if (flag2)
			{
				this.m_ListView.RefreshItems();
			}
		}

		// Token: 0x0400061E RID: 1566
		private static readonly string s_ListViewName = "unity-tree-view__list-view";

		// Token: 0x0400061F RID: 1567
		private static readonly string s_ItemToggleName = "unity-tree-view__item-toggle";

		// Token: 0x04000620 RID: 1568
		private static readonly string s_ItemIndentsContainerName = "unity-tree-view__item-indents";

		// Token: 0x04000621 RID: 1569
		private static readonly string s_ItemIndentName = "unity-tree-view__item-indent";

		// Token: 0x04000622 RID: 1570
		private static readonly string s_ItemContentContainerName = "unity-tree-view__item-content";

		// Token: 0x04000623 RID: 1571
		public static readonly string itemUssClassName = "unity-tree-view__item";

		// Token: 0x04000624 RID: 1572
		private Func<VisualElement> m_MakeItem;

		// Token: 0x04000627 RID: 1575
		private List<ITreeViewItem> m_SelectedItems;

		// Token: 0x04000628 RID: 1576
		private Action<VisualElement, ITreeViewItem> m_BindItem;

		// Token: 0x0400062A RID: 1578
		private IList<ITreeViewItem> m_RootItems;

		// Token: 0x0400062B RID: 1579
		[SerializeField]
		private List<int> m_ExpandedItemIds;

		// Token: 0x0400062C RID: 1580
		private List<InternalTreeView.TreeViewItemWrapper> m_ItemWrappers;

		// Token: 0x0400062D RID: 1581
		private readonly ListView m_ListView;

		// Token: 0x0400062E RID: 1582
		internal readonly ScrollView m_ScrollView;

		// Token: 0x0200019B RID: 411
		public new class UxmlFactory : UxmlFactory<InternalTreeView, InternalTreeView.UxmlTraits>
		{
		}

		// Token: 0x0200019C RID: 412
		public new class UxmlTraits : VisualElement.UxmlTraits
		{
			// Token: 0x170002C4 RID: 708
			// (get) Token: 0x06000D7C RID: 3452 RVA: 0x00037BB8 File Offset: 0x00035DB8
			public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription
			{
				get
				{
					yield break;
				}
			}

			// Token: 0x06000D7D RID: 3453 RVA: 0x00037BD8 File Offset: 0x00035DD8
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				int num = 0;
				bool flag = this.m_ItemHeight.TryGetValueFromBag(bag, cc, ref num);
				if (flag)
				{
					((InternalTreeView)ve).itemHeight = num;
				}
				((InternalTreeView)ve).showBorder = this.m_ShowBorder.GetValueFromBag(bag, cc);
				((InternalTreeView)ve).selectionType = this.m_SelectionType.GetValueFromBag(bag, cc);
				((InternalTreeView)ve).showAlternatingRowBackgrounds = this.m_ShowAlternatingRowBackgrounds.GetValueFromBag(bag, cc);
			}

			// Token: 0x0400062F RID: 1583
			private readonly UxmlIntAttributeDescription m_ItemHeight = new UxmlIntAttributeDescription
			{
				name = "item-height",
				defaultValue = BaseVerticalCollectionView.s_DefaultItemHeight
			};

			// Token: 0x04000630 RID: 1584
			private readonly UxmlBoolAttributeDescription m_ShowBorder = new UxmlBoolAttributeDescription
			{
				name = "show-border",
				defaultValue = false
			};

			// Token: 0x04000631 RID: 1585
			private readonly UxmlEnumAttributeDescription<SelectionType> m_SelectionType = new UxmlEnumAttributeDescription<SelectionType>
			{
				name = "selection-type",
				defaultValue = SelectionType.Single
			};

			// Token: 0x04000632 RID: 1586
			private readonly UxmlEnumAttributeDescription<AlternatingRowBackground> m_ShowAlternatingRowBackgrounds = new UxmlEnumAttributeDescription<AlternatingRowBackground>
			{
				name = "show-alternating-row-backgrounds",
				defaultValue = AlternatingRowBackground.None
			};
		}

		// Token: 0x0200019E RID: 414
		private struct TreeViewItemWrapper
		{
			// Token: 0x170002C7 RID: 711
			// (get) Token: 0x06000D87 RID: 3463 RVA: 0x00037D94 File Offset: 0x00035F94
			public int id
			{
				get
				{
					return this.item.id;
				}
			}

			// Token: 0x170002C8 RID: 712
			// (get) Token: 0x06000D88 RID: 3464 RVA: 0x00037DA1 File Offset: 0x00035FA1
			public bool hasChildren
			{
				get
				{
					return this.item.hasChildren;
				}
			}

			// Token: 0x04000637 RID: 1591
			public int depth;

			// Token: 0x04000638 RID: 1592
			public ITreeViewItem item;
		}
	}
}
