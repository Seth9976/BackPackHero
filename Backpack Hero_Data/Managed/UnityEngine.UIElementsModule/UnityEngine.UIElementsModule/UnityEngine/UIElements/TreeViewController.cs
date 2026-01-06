using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UIElements.Experimental;

namespace UnityEngine.UIElements
{
	// Token: 0x02000107 RID: 263
	internal abstract class TreeViewController : CollectionViewController
	{
		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x0600083B RID: 2107 RVA: 0x0001E555 File Offset: 0x0001C755
		protected TreeView treeView
		{
			get
			{
				return base.view as TreeView;
			}
		}

		// Token: 0x0600083C RID: 2108 RVA: 0x0001E564 File Offset: 0x0001C764
		public void RebuildTree()
		{
			this.m_TreeItems.Clear();
			this.m_RootIndices.Clear();
			foreach (int num in this.GetAllItemIds(null))
			{
				int parentId = this.GetParentId(num);
				bool flag = parentId == -1;
				if (flag)
				{
					this.m_RootIndices.Add(num);
				}
				this.m_TreeItems.Add(num, new TreeItem(num, parentId, this.GetChildrenIds(num)));
			}
			this.RegenerateWrappers();
		}

		// Token: 0x0600083D RID: 2109 RVA: 0x0001E608 File Offset: 0x0001C808
		public IEnumerable<int> GetRootItemIds()
		{
			return this.m_RootIndices;
		}

		// Token: 0x0600083E RID: 2110
		public abstract IEnumerable<int> GetAllItemIds(IEnumerable<int> rootIds = null);

		// Token: 0x0600083F RID: 2111
		public abstract int GetParentId(int id);

		// Token: 0x06000840 RID: 2112
		public abstract IEnumerable<int> GetChildrenIds(int id);

		// Token: 0x06000841 RID: 2113
		public abstract void Move(int id, int newParentId, int childIndex = -1);

		// Token: 0x06000842 RID: 2114
		public abstract bool TryRemoveItem(int id);

		// Token: 0x06000843 RID: 2115 RVA: 0x0001E620 File Offset: 0x0001C820
		internal override void InvokeMakeItem(ReusableCollectionItem reusableItem)
		{
			ReusableTreeViewItem treeItem = reusableItem as ReusableTreeViewItem;
			bool flag = treeItem != null;
			if (flag)
			{
				treeItem.Init(this.MakeItem());
				treeItem.onPointerUp += new Action<PointerUpEvent>(this.OnItemPointerUp);
				treeItem.onToggleValueChanged += new Action<ChangeEvent<bool>>(this.ToggleExpandedState);
				bool autoExpand = this.treeView.autoExpand;
				if (autoExpand)
				{
					this.treeView.expandedItemIds.Remove(treeItem.id);
					this.treeView.schedule.Execute(delegate
					{
						this.ExpandItem(treeItem.id, true);
					});
				}
			}
		}

		// Token: 0x06000844 RID: 2116 RVA: 0x0001E6E8 File Offset: 0x0001C8E8
		internal override void InvokeBindItem(ReusableCollectionItem reusableItem, int index)
		{
			ReusableTreeViewItem reusableTreeViewItem = reusableItem as ReusableTreeViewItem;
			bool flag = reusableTreeViewItem != null;
			if (flag)
			{
				reusableTreeViewItem.Indent(this.GetIndentationDepth(index));
				reusableTreeViewItem.SetExpandedWithoutNotify(this.IsExpandedByIndex(index));
				reusableTreeViewItem.SetToggleVisibility(this.HasChildrenByIndex(index));
			}
			base.InvokeBindItem(reusableItem, index);
		}

		// Token: 0x06000845 RID: 2117 RVA: 0x0001E73C File Offset: 0x0001C93C
		internal override void InvokeDestroyItem(ReusableCollectionItem reusableItem)
		{
			ReusableTreeViewItem reusableTreeViewItem = reusableItem as ReusableTreeViewItem;
			bool flag = reusableTreeViewItem != null;
			if (flag)
			{
				reusableTreeViewItem.onPointerUp -= new Action<PointerUpEvent>(this.OnItemPointerUp);
				reusableTreeViewItem.onToggleValueChanged -= new Action<ChangeEvent<bool>>(this.ToggleExpandedState);
			}
			base.InvokeDestroyItem(reusableItem);
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x0001E78C File Offset: 0x0001C98C
		private void OnItemPointerUp(PointerUpEvent evt)
		{
			bool flag = (evt.modifiers & EventModifiers.Alt) == EventModifiers.None;
			if (!flag)
			{
				VisualElement visualElement = evt.currentTarget as VisualElement;
				Toggle toggle = visualElement.Q(TreeView.itemToggleUssClassName, null);
				int index = ((ReusableTreeViewItem)toggle.userData).index;
				int idForIndex = this.GetIdForIndex(index);
				bool flag2 = this.IsExpandedByIndex(index);
				bool flag3 = !this.HasChildrenByIndex(index);
				if (!flag3)
				{
					HashSet<int> hashSet = new HashSet<int>(this.treeView.expandedItemIds);
					bool flag4 = flag2;
					if (flag4)
					{
						hashSet.Remove(idForIndex);
					}
					else
					{
						hashSet.Add(idForIndex);
					}
					IEnumerable<int> childrenIdsByIndex = this.GetChildrenIdsByIndex(index);
					foreach (int num in this.GetAllItemIds(childrenIdsByIndex))
					{
						bool flag5 = this.HasChildren(num);
						if (flag5)
						{
							bool flag6 = flag2;
							if (flag6)
							{
								hashSet.Remove(num);
							}
							else
							{
								hashSet.Add(num);
							}
						}
					}
					this.treeView.expandedItemIds = Enumerable.ToList<int>(hashSet);
					this.RegenerateWrappers();
					this.treeView.RefreshItems();
					evt.StopPropagation();
				}
			}
		}

		// Token: 0x06000847 RID: 2119 RVA: 0x0001E8D8 File Offset: 0x0001CAD8
		private void ToggleExpandedState(ChangeEvent<bool> evt)
		{
			Toggle toggle = evt.target as Toggle;
			int index = ((ReusableTreeViewItem)toggle.userData).index;
			bool flag = this.IsExpandedByIndex(index);
			bool flag2 = flag;
			if (flag2)
			{
				this.CollapseItemByIndex(index, false);
			}
			else
			{
				this.ExpandItemByIndex(index, false, true);
			}
			this.treeView.scrollView.contentContainer.Focus();
		}

		// Token: 0x06000848 RID: 2120 RVA: 0x0001E93C File Offset: 0x0001CB3C
		public override int GetItemCount()
		{
			List<TreeViewItemWrapper> itemWrappers = this.m_ItemWrappers;
			return (itemWrappers != null) ? itemWrappers.Count : 0;
		}

		// Token: 0x06000849 RID: 2121 RVA: 0x0001E960 File Offset: 0x0001CB60
		public virtual int GetTreeCount()
		{
			return this.m_TreeItems.Count;
		}

		// Token: 0x0600084A RID: 2122 RVA: 0x0001E980 File Offset: 0x0001CB80
		public override int GetIndexForId(int id)
		{
			for (int i = 0; i < this.m_ItemWrappers.Count; i++)
			{
				bool flag = this.m_ItemWrappers[i].id == id;
				if (flag)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x0600084B RID: 2123 RVA: 0x0001E9D4 File Offset: 0x0001CBD4
		public override int GetIdForIndex(int index)
		{
			return this.IsIndexValid(index) ? this.m_ItemWrappers[index].id : (-1);
		}

		// Token: 0x0600084C RID: 2124 RVA: 0x0001EA08 File Offset: 0x0001CC08
		public virtual bool HasChildren(int id)
		{
			TreeItem treeItem;
			bool flag = this.m_TreeItems.TryGetValue(id, ref treeItem);
			return flag && treeItem.hasChildren;
		}

		// Token: 0x0600084D RID: 2125 RVA: 0x0001EA38 File Offset: 0x0001CC38
		public bool HasChildrenByIndex(int index)
		{
			return this.IsIndexValid(index) && this.m_ItemWrappers[index].hasChildren;
		}

		// Token: 0x0600084E RID: 2126 RVA: 0x0001EA6C File Offset: 0x0001CC6C
		public IEnumerable<int> GetChildrenIdsByIndex(int index)
		{
			return this.IsIndexValid(index) ? this.m_ItemWrappers[index].childrenIds : null;
		}

		// Token: 0x0600084F RID: 2127 RVA: 0x0001EAA0 File Offset: 0x0001CCA0
		public int GetChildIndexForId(int id)
		{
			TreeItem treeItem;
			bool flag = !this.m_TreeItems.TryGetValue(id, ref treeItem);
			int num;
			if (flag)
			{
				num = -1;
			}
			else
			{
				int num2 = 0;
				TreeItem treeItem2;
				IEnumerable<int> enumerable;
				if (!this.m_TreeItems.TryGetValue(treeItem.parentId, ref treeItem2))
				{
					IEnumerable<int> rootIndices = this.m_RootIndices;
					enumerable = rootIndices;
				}
				else
				{
					enumerable = treeItem2.childrenIds;
				}
				IEnumerable<int> enumerable2 = enumerable;
				foreach (int num3 in enumerable2)
				{
					bool flag2 = num3 == id;
					if (flag2)
					{
						return num2;
					}
					num2++;
				}
				num = -1;
			}
			return num;
		}

		// Token: 0x06000850 RID: 2128 RVA: 0x0001EB50 File Offset: 0x0001CD50
		private int GetIndentationDepth(int index)
		{
			return this.IsIndexValid(index) ? this.m_ItemWrappers[index].depth : 0;
		}

		// Token: 0x06000851 RID: 2129 RVA: 0x0001EB80 File Offset: 0x0001CD80
		public bool IsExpanded(int id)
		{
			return this.treeView.expandedItemIds.Contains(id);
		}

		// Token: 0x06000852 RID: 2130 RVA: 0x0001EBA4 File Offset: 0x0001CDA4
		public bool IsExpandedByIndex(int index)
		{
			bool flag = !this.IsIndexValid(index);
			return !flag && this.IsExpanded(this.m_ItemWrappers[index].id);
		}

		// Token: 0x06000853 RID: 2131 RVA: 0x0001EBE4 File Offset: 0x0001CDE4
		public void ExpandItemByIndex(int index, bool expandAllChildren, bool refresh = true)
		{
			bool flag = !this.HasChildrenByIndex(index);
			if (!flag)
			{
				bool flag2 = !this.treeView.expandedItemIds.Contains(this.GetIdForIndex(index)) || expandAllChildren;
				if (flag2)
				{
					IEnumerable<int> childrenIdsByIndex = this.GetChildrenIdsByIndex(index);
					List<int> list = new List<int>();
					using (IEnumerator<int> enumerator = childrenIdsByIndex.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							int childId = enumerator.Current;
							bool flag3 = Enumerable.All<TreeViewItemWrapper>(this.m_ItemWrappers, (TreeViewItemWrapper x) => x.id != childId);
							if (flag3)
							{
								list.Add(childId);
							}
						}
					}
					this.CreateWrappers(list, this.GetIndentationDepth(index) + 1, ref this.m_WrapperInsertionList);
					this.m_ItemWrappers.InsertRange(index + 1, this.m_WrapperInsertionList);
					bool flag4 = !this.treeView.expandedItemIds.Contains(this.m_ItemWrappers[index].id);
					if (flag4)
					{
						this.treeView.expandedItemIds.Add(this.m_ItemWrappers[index].id);
					}
					this.m_WrapperInsertionList.Clear();
				}
				if (expandAllChildren)
				{
					int idForIndex = this.GetIdForIndex(index);
					IEnumerable<int> childrenIds = this.GetChildrenIds(idForIndex);
					foreach (int num in this.GetAllItemIds(childrenIds))
					{
						bool flag5 = !this.treeView.expandedItemIds.Contains(num);
						if (flag5)
						{
							this.ExpandItemByIndex(this.GetIndexForId(num), true, false);
						}
					}
				}
				if (refresh)
				{
					this.treeView.RefreshItems();
				}
			}
		}

		// Token: 0x06000854 RID: 2132 RVA: 0x0001EDD4 File Offset: 0x0001CFD4
		public void ExpandItem(int id, bool expandAllChildren)
		{
			bool flag = !this.HasChildren(id);
			if (!flag)
			{
				for (int i = 0; i < this.m_ItemWrappers.Count; i++)
				{
					bool flag2 = this.m_ItemWrappers[i].id == id;
					if (flag2)
					{
						bool flag3 = expandAllChildren || !this.IsExpandedByIndex(i);
						if (flag3)
						{
							this.ExpandItemByIndex(i, expandAllChildren, true);
							return;
						}
					}
				}
				bool flag4 = this.treeView.expandedItemIds.Contains(id);
				if (!flag4)
				{
					this.treeView.expandedItemIds.Add(id);
				}
			}
		}

		// Token: 0x06000855 RID: 2133 RVA: 0x0001EE7C File Offset: 0x0001D07C
		public void CollapseItemByIndex(int index, bool collapseAllChildren)
		{
			bool flag = !this.HasChildrenByIndex(index);
			if (!flag)
			{
				if (collapseAllChildren)
				{
					int idForIndex = this.GetIdForIndex(index);
					IEnumerable<int> childrenIds = this.GetChildrenIds(idForIndex);
					foreach (int num in this.GetAllItemIds(childrenIds))
					{
						this.treeView.expandedItemIds.Remove(num);
					}
				}
				this.treeView.expandedItemIds.Remove(this.GetIdForIndex(index));
				int num2 = 0;
				int num3 = index + 1;
				int indentationDepth = this.GetIndentationDepth(index);
				while (num3 < this.m_ItemWrappers.Count && this.GetIndentationDepth(num3) > indentationDepth)
				{
					num2++;
					num3++;
				}
				this.m_ItemWrappers.RemoveRange(index + 1, num2);
				this.treeView.RefreshItems();
			}
		}

		// Token: 0x06000856 RID: 2134 RVA: 0x0001EF80 File Offset: 0x0001D180
		public void CollapseItem(int id, bool collapseAllChildren)
		{
			for (int i = 0; i < this.m_ItemWrappers.Count; i++)
			{
				bool flag = this.m_ItemWrappers[i].id == id;
				if (flag)
				{
					bool flag2 = this.IsExpandedByIndex(i);
					if (flag2)
					{
						this.CollapseItemByIndex(i, collapseAllChildren);
						return;
					}
				}
			}
			bool flag3 = !this.treeView.expandedItemIds.Contains(id);
			if (flag3)
			{
				return;
			}
			this.treeView.expandedItemIds.Remove(id);
		}

		// Token: 0x06000857 RID: 2135 RVA: 0x0001F00C File Offset: 0x0001D20C
		public void ExpandAll()
		{
			foreach (int num in this.GetAllItemIds(null))
			{
				bool flag = !this.treeView.expandedItemIds.Contains(num);
				if (flag)
				{
					this.treeView.expandedItemIds.Add(num);
				}
			}
			this.RegenerateWrappers();
			this.treeView.RefreshItems();
		}

		// Token: 0x06000858 RID: 2136 RVA: 0x0001F094 File Offset: 0x0001D294
		public void CollapseAll()
		{
			bool flag = this.treeView.expandedItemIds.Count == 0;
			if (!flag)
			{
				this.treeView.expandedItemIds.Clear();
				this.RegenerateWrappers();
				this.treeView.RefreshItems();
			}
		}

		// Token: 0x06000859 RID: 2137 RVA: 0x0001F0E0 File Offset: 0x0001D2E0
		internal void RegenerateWrappers()
		{
			this.m_ItemWrappers.Clear();
			IEnumerable<int> rootItemIds = this.GetRootItemIds();
			bool flag = rootItemIds == null;
			if (!flag)
			{
				this.CreateWrappers(rootItemIds, 0, ref this.m_ItemWrappers);
				base.SetItemsSourceWithoutNotify(this.m_ItemWrappers);
			}
		}

		// Token: 0x0600085A RID: 2138 RVA: 0x0001F128 File Offset: 0x0001D328
		private void CreateWrappers(IEnumerable<int> treeViewItemIds, int depth, ref List<TreeViewItemWrapper> wrappers)
		{
			foreach (int num in treeViewItemIds)
			{
				TreeViewItemWrapper treeViewItemWrapper = new TreeViewItemWrapper(this.m_TreeItems[num], depth);
				wrappers.Add(treeViewItemWrapper);
				TreeView treeView = this.treeView;
				bool flag = ((treeView != null) ? treeView.expandedItemIds : null) == null;
				if (!flag)
				{
					bool flag2 = this.treeView.expandedItemIds.Contains(treeViewItemWrapper.id) && treeViewItemWrapper.hasChildren;
					if (flag2)
					{
						this.CreateWrappers(this.GetChildrenIds(treeViewItemWrapper.id), depth + 1, ref wrappers);
					}
				}
			}
		}

		// Token: 0x0600085B RID: 2139 RVA: 0x0001F1F0 File Offset: 0x0001D3F0
		private bool IsIndexValid(int index)
		{
			return index >= 0 && index < this.m_ItemWrappers.Count;
		}

		// Token: 0x04000364 RID: 868
		private Dictionary<int, TreeItem> m_TreeItems = new Dictionary<int, TreeItem>();

		// Token: 0x04000365 RID: 869
		private List<int> m_RootIndices = new List<int>();

		// Token: 0x04000366 RID: 870
		private List<TreeViewItemWrapper> m_ItemWrappers = new List<TreeViewItemWrapper>();

		// Token: 0x04000367 RID: 871
		private List<TreeViewItemWrapper> m_WrapperInsertionList = new List<TreeViewItemWrapper>();
	}
}
