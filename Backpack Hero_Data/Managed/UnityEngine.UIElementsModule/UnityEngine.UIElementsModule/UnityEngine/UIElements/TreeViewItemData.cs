using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x02000193 RID: 403
	public readonly struct TreeViewItemData<T>
	{
		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x06000CFC RID: 3324 RVA: 0x000351FD File Offset: 0x000333FD
		public int id { get; }

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x06000CFD RID: 3325 RVA: 0x00035205 File Offset: 0x00033405
		public T data
		{
			get
			{
				return this.m_Data;
			}
		}

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x06000CFE RID: 3326 RVA: 0x0003520D File Offset: 0x0003340D
		public IEnumerable<TreeViewItemData<T>> children
		{
			get
			{
				return this.m_Children;
			}
		}

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x06000CFF RID: 3327 RVA: 0x00035215 File Offset: 0x00033415
		public bool hasChildren
		{
			get
			{
				return this.m_Children != null && this.m_Children.Count > 0;
			}
		}

		// Token: 0x06000D00 RID: 3328 RVA: 0x00035230 File Offset: 0x00033430
		public TreeViewItemData(int id, T data, List<TreeViewItemData<T>> children = null)
		{
			this.id = id;
			this.m_Data = data;
			this.m_Children = children ?? new List<TreeViewItemData<T>>();
		}

		// Token: 0x06000D01 RID: 3329 RVA: 0x00035251 File Offset: 0x00033451
		internal void AddChild(TreeViewItemData<T> child)
		{
			this.m_Children.Add(child);
		}

		// Token: 0x06000D02 RID: 3330 RVA: 0x00035264 File Offset: 0x00033464
		internal void AddChildren(IList<TreeViewItemData<T>> children)
		{
			foreach (TreeViewItemData<T> treeViewItemData in children)
			{
				this.AddChild(treeViewItemData);
			}
		}

		// Token: 0x06000D03 RID: 3331 RVA: 0x000352B0 File Offset: 0x000334B0
		internal void InsertChild(TreeViewItemData<T> child, int index)
		{
			bool flag = index == -1;
			if (flag)
			{
				this.m_Children.Add(child);
			}
			else
			{
				this.m_Children.Insert(index, child);
			}
		}

		// Token: 0x06000D04 RID: 3332 RVA: 0x000352E4 File Offset: 0x000334E4
		internal void RemoveChild(int childId)
		{
			bool flag = this.m_Children == null;
			if (!flag)
			{
				for (int i = 0; i < this.m_Children.Count; i++)
				{
					bool flag2 = childId == this.m_Children[i].id;
					if (flag2)
					{
						this.m_Children.RemoveAt(i);
						break;
					}
				}
			}
		}

		// Token: 0x06000D05 RID: 3333 RVA: 0x0003534C File Offset: 0x0003354C
		internal int GetChildIndex(int itemId)
		{
			int num = 0;
			foreach (TreeViewItemData<T> treeViewItemData in this.m_Children)
			{
				bool flag = treeViewItemData.id == itemId;
				if (flag)
				{
					return num;
				}
				num++;
			}
			return -1;
		}

		// Token: 0x06000D06 RID: 3334 RVA: 0x000353B8 File Offset: 0x000335B8
		internal bool HasChildRecursive(int childId)
		{
			bool flag = !this.hasChildren;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				foreach (TreeViewItemData<T> treeViewItemData in this.m_Children)
				{
					bool flag3 = treeViewItemData.id == childId;
					if (flag3)
					{
						return true;
					}
					bool flag4 = treeViewItemData.HasChildRecursive(childId);
					if (flag4)
					{
						return true;
					}
				}
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x06000D07 RID: 3335 RVA: 0x00035440 File Offset: 0x00033640
		internal void ReplaceChild(TreeViewItemData<T> newChild)
		{
			bool flag = !this.hasChildren;
			if (!flag)
			{
				int num = 0;
				foreach (TreeViewItemData<T> treeViewItemData in this.m_Children)
				{
					bool flag2 = treeViewItemData.id == newChild.id;
					if (flag2)
					{
						this.m_Children.RemoveAt(num);
						this.m_Children.Insert(num, newChild);
						break;
					}
					num++;
				}
			}
		}

		// Token: 0x040005F6 RID: 1526
		private readonly T m_Data;

		// Token: 0x040005F7 RID: 1527
		private readonly IList<TreeViewItemData<T>> m_Children;
	}
}
