using System;
using System.Collections.Generic;
using UnityEngine.Pool;

namespace UnityEngine.UIElements
{
	// Token: 0x02000190 RID: 400
	internal readonly struct TreeData<T>
	{
		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06000CE5 RID: 3301 RVA: 0x00034C18 File Offset: 0x00032E18
		public IEnumerable<int> rootItemIds
		{
			get
			{
				return this.m_RootItemIds;
			}
		}

		// Token: 0x06000CE6 RID: 3302 RVA: 0x00034C20 File Offset: 0x00032E20
		public TreeData(IList<TreeViewItemData<T>> rootItems)
		{
			this.m_RootItemIds = new List<int>();
			this.m_Tree = new Dictionary<int, TreeViewItemData<T>>();
			this.m_ParentIds = new Dictionary<int, int>();
			this.m_ChildrenIds = new Dictionary<int, List<int>>();
			this.RefreshTree(rootItems);
		}

		// Token: 0x06000CE7 RID: 3303 RVA: 0x00034C58 File Offset: 0x00032E58
		public TreeViewItemData<T> GetDataForId(int id)
		{
			return this.m_Tree[id];
		}

		// Token: 0x06000CE8 RID: 3304 RVA: 0x00034C78 File Offset: 0x00032E78
		public int GetParentId(int id)
		{
			int num;
			bool flag = this.m_ParentIds.TryGetValue(id, ref num);
			int num2;
			if (flag)
			{
				num2 = num;
			}
			else
			{
				num2 = -1;
			}
			return num2;
		}

		// Token: 0x06000CE9 RID: 3305 RVA: 0x00034CA4 File Offset: 0x00032EA4
		public void AddItem(TreeViewItemData<T> item, int parentId, int childIndex)
		{
			List<TreeViewItemData<T>> list = CollectionPool<List<TreeViewItemData<T>>, TreeViewItemData<T>>.Get();
			list.Add(item);
			this.BuildTree(list, false);
			this.AddItemToParent(item, parentId, childIndex);
			CollectionPool<List<TreeViewItemData<T>>, TreeViewItemData<T>>.Release(list);
		}

		// Token: 0x06000CEA RID: 3306 RVA: 0x00034CDC File Offset: 0x00032EDC
		public bool TryRemove(int id)
		{
			int num;
			bool flag = this.m_ParentIds.TryGetValue(id, ref num);
			if (flag)
			{
				this.RemoveFromParent(id, num);
			}
			return this.TryRemoveChildrenIds(id);
		}

		// Token: 0x06000CEB RID: 3307 RVA: 0x00034D14 File Offset: 0x00032F14
		public void Move(int id, int newParentId, int childIndex)
		{
			TreeViewItemData<T> treeViewItemData;
			bool flag = !this.m_Tree.TryGetValue(id, ref treeViewItemData);
			if (!flag)
			{
				int num;
				bool flag2 = this.m_ParentIds.TryGetValue(id, ref num);
				if (flag2)
				{
					bool flag3 = num == newParentId;
					if (flag3)
					{
						int childIndex2 = this.m_Tree[num].GetChildIndex(id);
						bool flag4 = childIndex2 < childIndex;
						if (flag4)
						{
							childIndex--;
						}
					}
					this.RemoveFromParent(treeViewItemData.id, num);
				}
				else
				{
					this.m_RootItemIds.Remove(id);
				}
				this.AddItemToParent(treeViewItemData, newParentId, childIndex);
			}
		}

		// Token: 0x06000CEC RID: 3308 RVA: 0x00034DB0 File Offset: 0x00032FB0
		private void AddItemToParent(TreeViewItemData<T> item, int parentId, int childIndex)
		{
			bool flag = parentId == -1;
			if (flag)
			{
				this.m_ParentIds.Remove(item.id);
				bool flag2 = childIndex == -1;
				if (flag2)
				{
					this.m_RootItemIds.Add(item.id);
				}
				else
				{
					this.m_RootItemIds.Insert(childIndex, item.id);
				}
			}
			else
			{
				TreeViewItemData<T> treeViewItemData = this.m_Tree[parentId];
				treeViewItemData.InsertChild(item, childIndex);
				this.m_Tree[parentId] = treeViewItemData;
				this.m_ParentIds[item.id] = parentId;
				this.UpdateParentTree(treeViewItemData);
			}
		}

		// Token: 0x06000CED RID: 3309 RVA: 0x00034E50 File Offset: 0x00033050
		private void RemoveFromParent(int id, int parentId)
		{
			TreeViewItemData<T> treeViewItemData = this.m_Tree[parentId];
			treeViewItemData.RemoveChild(id);
			this.m_Tree[parentId] = treeViewItemData;
			List<int> list;
			bool flag = this.m_ChildrenIds.TryGetValue(parentId, ref list);
			if (flag)
			{
				list.Remove(id);
			}
			this.UpdateParentTree(treeViewItemData);
		}

		// Token: 0x06000CEE RID: 3310 RVA: 0x00034EA4 File Offset: 0x000330A4
		private void UpdateParentTree(TreeViewItemData<T> current)
		{
			for (;;)
			{
				int num;
				bool flag = this.m_ParentIds.TryGetValue(current.id, ref num);
				if (!flag)
				{
					break;
				}
				TreeViewItemData<T> treeViewItemData = this.m_Tree[num];
				treeViewItemData.ReplaceChild(current);
				this.m_Tree[num] = treeViewItemData;
				current = treeViewItemData;
			}
		}

		// Token: 0x06000CEF RID: 3311 RVA: 0x00034EF8 File Offset: 0x000330F8
		private bool TryRemoveChildrenIds(int id)
		{
			TreeViewItemData<T> treeViewItemData;
			bool flag = this.m_Tree.TryGetValue(id, ref treeViewItemData) && treeViewItemData.children != null;
			if (flag)
			{
				foreach (TreeViewItemData<T> treeViewItemData2 in treeViewItemData.children)
				{
					this.TryRemoveChildrenIds(treeViewItemData2.id);
				}
			}
			List<int> list;
			bool flag2 = this.m_ChildrenIds.TryGetValue(id, ref list);
			if (flag2)
			{
				CollectionPool<List<int>, int>.Release(list);
			}
			bool flag3 = false;
			flag3 |= this.m_ChildrenIds.Remove(id);
			flag3 |= this.m_ParentIds.Remove(id);
			return flag3 | this.m_Tree.Remove(id);
		}

		// Token: 0x06000CF0 RID: 3312 RVA: 0x00034FD0 File Offset: 0x000331D0
		private void RefreshTree(IList<TreeViewItemData<T>> rootItems)
		{
			this.m_Tree.Clear();
			this.m_ParentIds.Clear();
			this.m_ChildrenIds.Clear();
			this.m_RootItemIds.Clear();
			this.BuildTree(rootItems, true);
		}

		// Token: 0x06000CF1 RID: 3313 RVA: 0x0003500C File Offset: 0x0003320C
		private void BuildTree(IEnumerable<TreeViewItemData<T>> items, bool isRoot)
		{
			bool flag = items == null;
			if (!flag)
			{
				foreach (TreeViewItemData<T> treeViewItemData in items)
				{
					this.m_Tree.Add(treeViewItemData.id, treeViewItemData);
					if (isRoot)
					{
						this.m_RootItemIds.Add(treeViewItemData.id);
					}
					bool flag2 = treeViewItemData.children != null;
					if (flag2)
					{
						List<int> list;
						bool flag3 = !this.m_ChildrenIds.TryGetValue(treeViewItemData.id, ref list);
						if (flag3)
						{
							this.m_ChildrenIds.Add(treeViewItemData.id, list = CollectionPool<List<int>, int>.Get());
						}
						foreach (TreeViewItemData<T> treeViewItemData2 in treeViewItemData.children)
						{
							this.m_ParentIds.Add(treeViewItemData2.id, treeViewItemData.id);
							list.Add(treeViewItemData2.id);
						}
						this.BuildTree(treeViewItemData.children, false);
					}
				}
			}
		}

		// Token: 0x040005EB RID: 1515
		private readonly IList<int> m_RootItemIds;

		// Token: 0x040005EC RID: 1516
		private readonly Dictionary<int, TreeViewItemData<T>> m_Tree;

		// Token: 0x040005ED RID: 1517
		private readonly Dictionary<int, int> m_ParentIds;

		// Token: 0x040005EE RID: 1518
		private readonly Dictionary<int, List<int>> m_ChildrenIds;
	}
}
