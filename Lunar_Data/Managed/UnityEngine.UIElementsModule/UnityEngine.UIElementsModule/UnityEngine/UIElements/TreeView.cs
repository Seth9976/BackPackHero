using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine.Assertions;

namespace UnityEngine.UIElements
{
	// Token: 0x0200018A RID: 394
	internal class TreeView : VisualElement
	{
		// Token: 0x17000288 RID: 648
		// (get) Token: 0x06000C8D RID: 3213 RVA: 0x000334E4 File Offset: 0x000316E4
		// (set) Token: 0x06000C8E RID: 3214 RVA: 0x000334FC File Offset: 0x000316FC
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

		// Token: 0x14000026 RID: 38
		// (add) Token: 0x06000C8F RID: 3215 RVA: 0x00033530 File Offset: 0x00031730
		// (remove) Token: 0x06000C90 RID: 3216 RVA: 0x00033568 File Offset: 0x00031768
		[field: DebuggerBrowsable(0)]
		public event Action<IEnumerable<ITreeViewItem>> onItemsChosen;

		// Token: 0x14000027 RID: 39
		// (add) Token: 0x06000C91 RID: 3217 RVA: 0x000335A0 File Offset: 0x000317A0
		// (remove) Token: 0x06000C92 RID: 3218 RVA: 0x000335D8 File Offset: 0x000317D8
		[field: DebuggerBrowsable(0)]
		public event Action<IEnumerable<ITreeViewItem>> onSelectionChange;

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x06000C93 RID: 3219 RVA: 0x0003360D File Offset: 0x0003180D
		public ITreeViewItem selectedItem
		{
			get
			{
				return (this.m_SelectedItems.Count == 0) ? null : Enumerable.First<ITreeViewItem>(this.m_SelectedItems);
			}
		}

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x06000C94 RID: 3220 RVA: 0x0003362C File Offset: 0x0003182C
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

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x06000C95 RID: 3221 RVA: 0x00033708 File Offset: 0x00031908
		// (set) Token: 0x06000C96 RID: 3222 RVA: 0x00033720 File Offset: 0x00031920
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

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x06000C97 RID: 3223 RVA: 0x00033731 File Offset: 0x00031931
		// (set) Token: 0x06000C98 RID: 3224 RVA: 0x00033739 File Offset: 0x00031939
		public Action<VisualElement, ITreeViewItem> unbindItem { get; set; }

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x06000C99 RID: 3225 RVA: 0x00033744 File Offset: 0x00031944
		// (set) Token: 0x06000C9A RID: 3226 RVA: 0x0003375C File Offset: 0x0003195C
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

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x06000C9B RID: 3227 RVA: 0x0003376D File Offset: 0x0003196D
		public IEnumerable<ITreeViewItem> items
		{
			get
			{
				return TreeView.GetAllItems(this.m_RootItems);
			}
		}

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x06000C9C RID: 3228 RVA: 0x0003377A File Offset: 0x0003197A
		public float resolvedItemHeight
		{
			get
			{
				return this.m_ListView.ResolveItemHeight(-1f);
			}
		}

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x06000C9D RID: 3229 RVA: 0x0003378C File Offset: 0x0003198C
		// (set) Token: 0x06000C9E RID: 3230 RVA: 0x000337AA File Offset: 0x000319AA
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

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x06000C9F RID: 3231 RVA: 0x000337BC File Offset: 0x000319BC
		// (set) Token: 0x06000CA0 RID: 3232 RVA: 0x000337D9 File Offset: 0x000319D9
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

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x06000CA1 RID: 3233 RVA: 0x000337EC File Offset: 0x000319EC
		// (set) Token: 0x06000CA2 RID: 3234 RVA: 0x00033809 File Offset: 0x00031A09
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

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06000CA3 RID: 3235 RVA: 0x0003381C File Offset: 0x00031A1C
		// (set) Token: 0x06000CA4 RID: 3236 RVA: 0x00033839 File Offset: 0x00031A39
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

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06000CA5 RID: 3237 RVA: 0x0003384C File Offset: 0x00031A4C
		// (set) Token: 0x06000CA6 RID: 3238 RVA: 0x00033869 File Offset: 0x00031A69
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

		// Token: 0x06000CA7 RID: 3239 RVA: 0x0003387C File Offset: 0x00031A7C
		public TreeView()
		{
			this.m_SelectedItems = null;
			this.m_ExpandedItemIds = new List<int>();
			this.m_ItemWrappers = new List<TreeView.TreeViewItemWrapper>();
			this.m_ListView = new ListView();
			this.m_ListView.name = TreeView.s_ListViewName;
			this.m_ListView.itemsSource = this.m_ItemWrappers;
			this.m_ListView.viewDataKey = TreeView.s_ListViewName;
			this.m_ListView.AddToClassList(TreeView.s_ListViewName);
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

		// Token: 0x06000CA8 RID: 3240 RVA: 0x000339FA File Offset: 0x00031BFA
		public TreeView(IList<ITreeViewItem> items, int fixedItemHeight, Func<VisualElement> makeItem, Action<VisualElement, ITreeViewItem> bindItem)
			: this()
		{
			this.m_ListView.fixedItemHeight = (float)fixedItemHeight;
			this.m_MakeItem = makeItem;
			this.m_BindItem = bindItem;
			this.m_RootItems = items;
			this.Rebuild();
		}

		// Token: 0x06000CA9 RID: 3241 RVA: 0x00033A2F File Offset: 0x00031C2F
		public void RefreshItems()
		{
			this.RegenerateWrappers();
			this.ListViewRefresh();
		}

		// Token: 0x06000CAA RID: 3242 RVA: 0x00033A40 File Offset: 0x00031C40
		public void Rebuild()
		{
			this.RegenerateWrappers();
			this.m_ListView.Rebuild();
		}

		// Token: 0x06000CAB RID: 3243 RVA: 0x00033A58 File Offset: 0x00031C58
		internal override void OnViewDataReady()
		{
			base.OnViewDataReady();
			string fullHierarchicalViewDataKey = base.GetFullHierarchicalViewDataKey();
			base.OverwriteFromViewData(this, fullHierarchicalViewDataKey);
			this.Rebuild();
		}

		// Token: 0x06000CAC RID: 3244 RVA: 0x00033A84 File Offset: 0x00031C84
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

		// Token: 0x06000CAD RID: 3245 RVA: 0x00033A94 File Offset: 0x00031C94
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

		// Token: 0x06000CAE RID: 3246 RVA: 0x00033B10 File Offset: 0x00031D10
		public void SetSelection(int id)
		{
			this.SetSelection(new int[] { id });
		}

		// Token: 0x06000CAF RID: 3247 RVA: 0x00033B24 File Offset: 0x00031D24
		public void SetSelection(IEnumerable<int> ids)
		{
			this.SetSelectionInternal(ids, true);
		}

		// Token: 0x06000CB0 RID: 3248 RVA: 0x00033B30 File Offset: 0x00031D30
		public void SetSelectionWithoutNotify(IEnumerable<int> ids)
		{
			this.SetSelectionInternal(ids, false);
		}

		// Token: 0x06000CB1 RID: 3249 RVA: 0x00033B3C File Offset: 0x00031D3C
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

		// Token: 0x06000CB2 RID: 3250 RVA: 0x00033B84 File Offset: 0x00031D84
		public void AddToSelection(int id)
		{
			int itemIndex = this.GetItemIndex(id, true);
			this.ListViewRefresh();
			this.m_ListView.AddToSelection(itemIndex);
		}

		// Token: 0x06000CB3 RID: 3251 RVA: 0x00033BB0 File Offset: 0x00031DB0
		public void RemoveFromSelection(int id)
		{
			int itemIndex = this.GetItemIndex(id, false);
			this.m_ListView.RemoveFromSelection(itemIndex);
		}

		// Token: 0x06000CB4 RID: 3252 RVA: 0x00033BD4 File Offset: 0x00031DD4
		private int GetItemIndex(int id, bool expand = false)
		{
			ITreeViewItem treeViewItem = this.FindItem(id);
			bool flag = treeViewItem == null;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("id", id, "TreeView: Item id not found.");
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

		// Token: 0x06000CB5 RID: 3253 RVA: 0x00033CB7 File Offset: 0x00031EB7
		public void ClearSelection()
		{
			this.m_ListView.ClearSelection();
		}

		// Token: 0x06000CB6 RID: 3254 RVA: 0x00033CC6 File Offset: 0x00031EC6
		public void ScrollTo(VisualElement visualElement)
		{
			this.m_ListView.ScrollTo(visualElement);
		}

		// Token: 0x06000CB7 RID: 3255 RVA: 0x00033CD8 File Offset: 0x00031ED8
		public void ScrollToItem(int id)
		{
			int itemIndex = this.GetItemIndex(id, true);
			this.RefreshItems();
			this.m_ListView.ScrollToItem(itemIndex);
		}

		// Token: 0x06000CB8 RID: 3256 RVA: 0x00033D04 File Offset: 0x00031F04
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

		// Token: 0x06000CB9 RID: 3257 RVA: 0x00033DE4 File Offset: 0x00031FE4
		public bool IsExpanded(int id)
		{
			return this.m_ExpandedItemIds.Contains(id);
		}

		// Token: 0x06000CBA RID: 3258 RVA: 0x00033E04 File Offset: 0x00032004
		public void CollapseItem(int id)
		{
			bool flag = this.FindItem(id) == null;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("id", id, "TreeView: Item id not found.");
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

		// Token: 0x06000CBB RID: 3259 RVA: 0x00033EB0 File Offset: 0x000320B0
		public void ExpandItem(int id)
		{
			bool flag = this.FindItem(id) == null;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("id", id, "TreeView: Item id not found.");
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

		// Token: 0x06000CBC RID: 3260 RVA: 0x00033F5C File Offset: 0x0003215C
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

		// Token: 0x06000CBD RID: 3261 RVA: 0x00033FBC File Offset: 0x000321BC
		private void ListViewRefresh()
		{
			this.m_ListView.RefreshItems();
		}

		// Token: 0x06000CBE RID: 3262 RVA: 0x00033FCC File Offset: 0x000321CC
		private void OnItemsChosen(IEnumerable<object> chosenItems)
		{
			bool flag = this.onItemsChosen == null;
			if (!flag)
			{
				List<ITreeViewItem> list = new List<ITreeViewItem>();
				foreach (object obj in chosenItems)
				{
					TreeView.TreeViewItemWrapper treeViewItemWrapper = (TreeView.TreeViewItemWrapper)obj;
					list.Add(treeViewItemWrapper.item);
				}
				this.onItemsChosen.Invoke(list);
			}
		}

		// Token: 0x06000CBF RID: 3263 RVA: 0x0003404C File Offset: 0x0003224C
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
				this.m_SelectedItems.Add(((TreeView.TreeViewItemWrapper)obj).item);
			}
			Action<IEnumerable<ITreeViewItem>> action = this.onSelectionChange;
			if (action != null)
			{
				action.Invoke(this.m_SelectedItems);
			}
		}

		// Token: 0x06000CC0 RID: 3264 RVA: 0x000340E4 File Offset: 0x000322E4
		private void OnTreeViewMouseUp(MouseUpEvent evt)
		{
			this.m_ScrollView.contentContainer.Focus();
		}

		// Token: 0x06000CC1 RID: 3265 RVA: 0x000340F8 File Offset: 0x000322F8
		private void OnItemMouseUp(MouseUpEvent evt)
		{
			bool flag = (evt.modifiers & EventModifiers.Alt) == EventModifiers.None;
			if (!flag)
			{
				VisualElement visualElement = evt.currentTarget as VisualElement;
				Toggle toggle = visualElement.Q(TreeView.s_ItemToggleName, null);
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
					foreach (ITreeViewItem treeViewItem in TreeView.GetAllItems(item.children))
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

		// Token: 0x06000CC2 RID: 3266 RVA: 0x00034240 File Offset: 0x00032440
		private VisualElement MakeTreeItem()
		{
			VisualElement visualElement = new VisualElement
			{
				name = TreeView.s_ItemName,
				style = 
				{
					flexDirection = FlexDirection.Row
				}
			};
			visualElement.AddToClassList(TreeView.s_ItemName);
			visualElement.RegisterCallback<MouseUpEvent>(new EventCallback<MouseUpEvent>(this.OnItemMouseUp), TrickleDown.NoTrickleDown);
			VisualElement visualElement2 = new VisualElement
			{
				name = TreeView.s_ItemIndentsContainerName,
				style = 
				{
					flexDirection = FlexDirection.Row
				}
			};
			visualElement2.AddToClassList(TreeView.s_ItemIndentsContainerName);
			visualElement.hierarchy.Add(visualElement2);
			Toggle toggle = new Toggle
			{
				name = TreeView.s_ItemToggleName
			};
			toggle.AddToClassList(Foldout.toggleUssClassName);
			toggle.RegisterValueChangedCallback(new EventCallback<ChangeEvent<bool>>(this.ToggleExpandedState));
			visualElement.hierarchy.Add(toggle);
			VisualElement visualElement3 = new VisualElement
			{
				name = TreeView.s_ItemContentContainerName,
				style = 
				{
					flexGrow = 1f
				}
			};
			visualElement3.AddToClassList(TreeView.s_ItemContentContainerName);
			visualElement.Add(visualElement3);
			bool flag = this.m_MakeItem != null;
			if (flag)
			{
				visualElement3.Add(this.m_MakeItem.Invoke());
			}
			return visualElement;
		}

		// Token: 0x06000CC3 RID: 3267 RVA: 0x0003437C File Offset: 0x0003257C
		private void UnbindTreeItem(VisualElement element, int index)
		{
			bool flag = this.unbindItem == null;
			if (!flag)
			{
				ITreeViewItem item = this.m_ItemWrappers[index].item;
				VisualElement visualElement = element.Q(TreeView.s_ItemContentContainerName, null).ElementAt(0);
				this.unbindItem.Invoke(visualElement, item);
			}
		}

		// Token: 0x06000CC4 RID: 3268 RVA: 0x000343CC File Offset: 0x000325CC
		private void BindTreeItem(VisualElement element, int index)
		{
			ITreeViewItem item = this.m_ItemWrappers[index].item;
			VisualElement visualElement = element.Q(TreeView.s_ItemIndentsContainerName, null);
			visualElement.Clear();
			for (int i = 0; i < this.m_ItemWrappers[index].depth; i++)
			{
				VisualElement visualElement2 = new VisualElement();
				visualElement2.AddToClassList(TreeView.s_ItemIndentName);
				visualElement.Add(visualElement2);
			}
			Toggle toggle = element.Q(TreeView.s_ItemToggleName, null);
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
				VisualElement visualElement3 = element.Q(TreeView.s_ItemContentContainerName, null).ElementAt(0);
				this.m_BindItem.Invoke(visualElement3, item);
			}
		}

		// Token: 0x06000CC5 RID: 3269 RVA: 0x000344C0 File Offset: 0x000326C0
		private int GetItemId(int index)
		{
			return this.m_ItemWrappers[index].id;
		}

		// Token: 0x06000CC6 RID: 3270 RVA: 0x000344E8 File Offset: 0x000326E8
		private bool IsExpandedByIndex(int index)
		{
			return this.m_ExpandedItemIds.Contains(this.m_ItemWrappers[index].id);
		}

		// Token: 0x06000CC7 RID: 3271 RVA: 0x0003451C File Offset: 0x0003271C
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

		// Token: 0x06000CC8 RID: 3272 RVA: 0x000345DC File Offset: 0x000327DC
		private void ExpandItemByIndex(int index)
		{
			bool flag = !this.m_ItemWrappers[index].item.hasChildren;
			if (!flag)
			{
				List<TreeView.TreeViewItemWrapper> list = new List<TreeView.TreeViewItemWrapper>();
				this.CreateWrappers(this.m_ItemWrappers[index].item.children, this.m_ItemWrappers[index].depth + 1, ref list);
				this.m_ItemWrappers.InsertRange(index + 1, list);
				this.m_ExpandedItemIds.Add(this.m_ItemWrappers[index].item.id);
				this.ListViewRefresh();
				base.SaveViewData();
			}
		}

		// Token: 0x06000CC9 RID: 3273 RVA: 0x00034684 File Offset: 0x00032884
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

		// Token: 0x06000CCA RID: 3274 RVA: 0x000346E8 File Offset: 0x000328E8
		private void CreateWrappers(IEnumerable<ITreeViewItem> treeViewItems, int depth, ref List<TreeView.TreeViewItemWrapper> wrappers)
		{
			foreach (ITreeViewItem treeViewItem in treeViewItems)
			{
				TreeView.TreeViewItemWrapper treeViewItemWrapper = new TreeView.TreeViewItemWrapper
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

		// Token: 0x06000CCB RID: 3275 RVA: 0x00034784 File Offset: 0x00032984
		private void RegenerateWrappers()
		{
			this.m_ItemWrappers.Clear();
			bool flag = this.m_RootItems == null;
			if (!flag)
			{
				this.CreateWrappers(this.m_RootItems, 0, ref this.m_ItemWrappers);
			}
		}

		// Token: 0x06000CCC RID: 3276 RVA: 0x000347C4 File Offset: 0x000329C4
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

		// Token: 0x040005C7 RID: 1479
		private static readonly string s_ListViewName = "unity-tree-view__list-view";

		// Token: 0x040005C8 RID: 1480
		private static readonly string s_ItemName = "unity-tree-view__item";

		// Token: 0x040005C9 RID: 1481
		private static readonly string s_ItemToggleName = "unity-tree-view__item-toggle";

		// Token: 0x040005CA RID: 1482
		private static readonly string s_ItemIndentsContainerName = "unity-tree-view__item-indents";

		// Token: 0x040005CB RID: 1483
		private static readonly string s_ItemIndentName = "unity-tree-view__item-indent";

		// Token: 0x040005CC RID: 1484
		private static readonly string s_ItemContentContainerName = "unity-tree-view__item-content";

		// Token: 0x040005CD RID: 1485
		private Func<VisualElement> m_MakeItem;

		// Token: 0x040005D0 RID: 1488
		private List<ITreeViewItem> m_SelectedItems;

		// Token: 0x040005D1 RID: 1489
		private Action<VisualElement, ITreeViewItem> m_BindItem;

		// Token: 0x040005D3 RID: 1491
		private IList<ITreeViewItem> m_RootItems;

		// Token: 0x040005D4 RID: 1492
		[SerializeField]
		private List<int> m_ExpandedItemIds;

		// Token: 0x040005D5 RID: 1493
		private List<TreeView.TreeViewItemWrapper> m_ItemWrappers;

		// Token: 0x040005D6 RID: 1494
		private readonly ListView m_ListView;

		// Token: 0x040005D7 RID: 1495
		private readonly ScrollView m_ScrollView;

		// Token: 0x0200018B RID: 395
		public new class UxmlFactory : UxmlFactory<TreeView, TreeView.UxmlTraits>
		{
		}

		// Token: 0x0200018C RID: 396
		public new class UxmlTraits : VisualElement.UxmlTraits
		{
			// Token: 0x17000295 RID: 661
			// (get) Token: 0x06000CD0 RID: 3280 RVA: 0x00034884 File Offset: 0x00032A84
			public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription
			{
				get
				{
					yield break;
				}
			}

			// Token: 0x06000CD1 RID: 3281 RVA: 0x000348A4 File Offset: 0x00032AA4
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				int num = 0;
				bool flag = this.m_ItemHeight.TryGetValueFromBag(bag, cc, ref num);
				if (flag)
				{
					((TreeView)ve).itemHeight = num;
				}
				((TreeView)ve).showBorder = this.m_ShowBorder.GetValueFromBag(bag, cc);
				((TreeView)ve).selectionType = this.m_SelectionType.GetValueFromBag(bag, cc);
				((TreeView)ve).showAlternatingRowBackgrounds = this.m_ShowAlternatingRowBackgrounds.GetValueFromBag(bag, cc);
			}

			// Token: 0x040005D8 RID: 1496
			private readonly UxmlIntAttributeDescription m_ItemHeight = new UxmlIntAttributeDescription
			{
				name = "item-height",
				defaultValue = BaseVerticalCollectionView.s_DefaultItemHeight
			};

			// Token: 0x040005D9 RID: 1497
			private readonly UxmlBoolAttributeDescription m_ShowBorder = new UxmlBoolAttributeDescription
			{
				name = "show-border",
				defaultValue = false
			};

			// Token: 0x040005DA RID: 1498
			private readonly UxmlEnumAttributeDescription<SelectionType> m_SelectionType = new UxmlEnumAttributeDescription<SelectionType>
			{
				name = "selection-type",
				defaultValue = SelectionType.Single
			};

			// Token: 0x040005DB RID: 1499
			private readonly UxmlEnumAttributeDescription<AlternatingRowBackground> m_ShowAlternatingRowBackgrounds = new UxmlEnumAttributeDescription<AlternatingRowBackground>
			{
				name = "show-alternating-row-backgrounds",
				defaultValue = AlternatingRowBackground.None
			};
		}

		// Token: 0x0200018E RID: 398
		private struct TreeViewItemWrapper
		{
			// Token: 0x17000298 RID: 664
			// (get) Token: 0x06000CDB RID: 3291 RVA: 0x00034A60 File Offset: 0x00032C60
			public int id
			{
				get
				{
					return this.item.id;
				}
			}

			// Token: 0x17000299 RID: 665
			// (get) Token: 0x06000CDC RID: 3292 RVA: 0x00034A6D File Offset: 0x00032C6D
			public bool hasChildren
			{
				get
				{
					return this.item.hasChildren;
				}
			}

			// Token: 0x040005E0 RID: 1504
			public int depth;

			// Token: 0x040005E1 RID: 1505
			public ITreeViewItem item;
		}
	}
}
