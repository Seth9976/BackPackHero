using System;
using System.Collections.Generic;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding.Graphs.Util
{
	// Token: 0x02000199 RID: 409
	public class GridLookup<T> where T : class
	{
		// Token: 0x06000AFF RID: 2815 RVA: 0x0003DD78 File Offset: 0x0003BF78
		public GridLookup(Int2 size)
		{
			this.size = size;
			this.cells = new GridLookup<T>.Item[size.x * size.y];
			for (int i = 0; i < this.cells.Length; i++)
			{
				this.cells[i] = new GridLookup<T>.Item();
			}
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x06000B00 RID: 2816 RVA: 0x0003DDEB File Offset: 0x0003BFEB
		public GridLookup<T>.Root AllItems
		{
			get
			{
				return this.all.next;
			}
		}

		// Token: 0x06000B01 RID: 2817 RVA: 0x0003DDF8 File Offset: 0x0003BFF8
		public void Clear()
		{
			this.rootLookup.Clear();
			this.all.next = null;
			GridLookup<T>.Item[] array = this.cells;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].next = null;
			}
		}

		// Token: 0x06000B02 RID: 2818 RVA: 0x0003DE3C File Offset: 0x0003C03C
		public GridLookup<T>.Root GetRoot(T item)
		{
			GridLookup<T>.Root root;
			this.rootLookup.TryGetValue(item, out root);
			return root;
		}

		// Token: 0x06000B03 RID: 2819 RVA: 0x0003DE5C File Offset: 0x0003C05C
		public GridLookup<T>.Root Add(T item, IntRect bounds)
		{
			GridLookup<T>.Root root = new GridLookup<T>.Root
			{
				obj = item,
				prev = this.all,
				next = this.all.next
			};
			this.all.next = root;
			if (root.next != null)
			{
				root.next.prev = root;
			}
			this.rootLookup.Add(item, root);
			this.Move(item, bounds);
			return root;
		}

		// Token: 0x06000B04 RID: 2820 RVA: 0x0003DECC File Offset: 0x0003C0CC
		public void Remove(T item)
		{
			GridLookup<T>.Root root;
			if (!this.rootLookup.TryGetValue(item, out root))
			{
				return;
			}
			this.Move(item, new IntRect(0, 0, -1, -1));
			this.rootLookup.Remove(item);
			root.prev.next = root.next;
			if (root.next != null)
			{
				root.next.prev = root.prev;
			}
		}

		// Token: 0x06000B05 RID: 2821 RVA: 0x0003DF34 File Offset: 0x0003C134
		public void Dirty(T item)
		{
			GridLookup<T>.Root root;
			if (!this.rootLookup.TryGetValue(item, out root))
			{
				return;
			}
			root.previousPosition = new Vector3(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity);
		}

		// Token: 0x06000B06 RID: 2822 RVA: 0x0003DF6C File Offset: 0x0003C16C
		public void Move(T item, IntRect bounds)
		{
			GridLookup<T>.Root root;
			if (!this.rootLookup.TryGetValue(item, out root))
			{
				throw new ArgumentException("The item has not been added to this object");
			}
			if (root.previousBounds == bounds)
			{
				return;
			}
			for (int i = 0; i < root.items.Count; i++)
			{
				GridLookup<T>.Item item2 = root.items[i];
				item2.prev.next = item2.next;
				if (item2.next != null)
				{
					item2.next.prev = item2.prev;
				}
			}
			root.previousBounds = bounds;
			int num = 0;
			for (int j = bounds.ymin; j <= bounds.ymax; j++)
			{
				for (int k = bounds.xmin; k <= bounds.xmax; k++)
				{
					GridLookup<T>.Item item3;
					if (num < root.items.Count)
					{
						item3 = root.items[num];
					}
					else
					{
						item3 = ((this.itemPool.Count > 0) ? this.itemPool.Pop() : new GridLookup<T>.Item());
						item3.root = root;
						root.items.Add(item3);
					}
					num++;
					item3.prev = this.cells[k + j * this.size.x];
					item3.next = item3.prev.next;
					item3.prev.next = item3;
					if (item3.next != null)
					{
						item3.next.prev = item3;
					}
				}
			}
			for (int l = root.items.Count - 1; l >= num; l--)
			{
				GridLookup<T>.Item item4 = root.items[l];
				item4.root = null;
				item4.next = null;
				item4.prev = null;
				root.items.RemoveAt(l);
				this.itemPool.Push(item4);
			}
		}

		// Token: 0x06000B07 RID: 2823 RVA: 0x0003E148 File Offset: 0x0003C348
		public List<U> QueryRect<U>(IntRect r) where U : class, T
		{
			List<U> list = ListPool<U>.Claim();
			for (int i = r.ymin; i <= r.ymax; i++)
			{
				int num = i * this.size.x;
				for (int j = r.xmin; j <= r.xmax; j++)
				{
					GridLookup<T>.Item item = this.cells[j + num];
					while (item.next != null)
					{
						item = item.next;
						U u = item.root.obj as U;
						if (!item.root.flag && u != null)
						{
							item.root.flag = true;
							list.Add(u);
						}
					}
				}
			}
			for (int k = r.ymin; k <= r.ymax; k++)
			{
				int num2 = k * this.size.x;
				for (int l = r.xmin; l <= r.xmax; l++)
				{
					GridLookup<T>.Item item2 = this.cells[l + num2];
					while (item2.next != null)
					{
						item2 = item2.next;
						item2.root.flag = false;
					}
				}
			}
			return list;
		}

		// Token: 0x06000B08 RID: 2824 RVA: 0x0003E278 File Offset: 0x0003C478
		public void Resize(IntRect newBounds)
		{
			GridLookup<T>.Item[] array = new GridLookup<T>.Item[newBounds.Width * newBounds.Height];
			for (int i = 0; i < this.size.y; i++)
			{
				for (int j = 0; j < this.size.x; j++)
				{
					if (newBounds.Contains(j, i))
					{
						array[j - newBounds.xmin + (i - newBounds.ymin) * newBounds.Width] = this.cells[j + i * this.size.x];
					}
				}
			}
			for (int k = 0; k < array.Length; k++)
			{
				if (array[k] == null)
				{
					array[k] = new GridLookup<T>.Item();
				}
			}
			this.size = new Int2(newBounds.Width, newBounds.Height);
			this.cells = array;
			GridLookup<T>.Root root = this.AllItems;
			Int2 @int = new Int2(-newBounds.xmin, -newBounds.ymin);
			IntRect intRect = new IntRect(0, 0, newBounds.Width - 1, newBounds.Height - 1);
			while (root != null)
			{
				root.previousBounds = IntRect.Intersection(root.previousBounds.Offset(@int), intRect);
				root = root.next;
			}
		}

		// Token: 0x0400078C RID: 1932
		private Int2 size;

		// Token: 0x0400078D RID: 1933
		private GridLookup<T>.Item[] cells;

		// Token: 0x0400078E RID: 1934
		private GridLookup<T>.Root all = new GridLookup<T>.Root();

		// Token: 0x0400078F RID: 1935
		private Dictionary<T, GridLookup<T>.Root> rootLookup = new Dictionary<T, GridLookup<T>.Root>();

		// Token: 0x04000790 RID: 1936
		private Stack<GridLookup<T>.Item> itemPool = new Stack<GridLookup<T>.Item>();

		// Token: 0x0200019A RID: 410
		internal class Item
		{
			// Token: 0x04000791 RID: 1937
			public GridLookup<T>.Root root;

			// Token: 0x04000792 RID: 1938
			public GridLookup<T>.Item prev;

			// Token: 0x04000793 RID: 1939
			public GridLookup<T>.Item next;
		}

		// Token: 0x0200019B RID: 411
		public class Root
		{
			// Token: 0x04000794 RID: 1940
			public T obj;

			// Token: 0x04000795 RID: 1941
			public GridLookup<T>.Root next;

			// Token: 0x04000796 RID: 1942
			internal GridLookup<T>.Root prev;

			// Token: 0x04000797 RID: 1943
			internal IntRect previousBounds = new IntRect(0, 0, -1, -1);

			// Token: 0x04000798 RID: 1944
			internal List<GridLookup<T>.Item> items = new List<GridLookup<T>.Item>();

			// Token: 0x04000799 RID: 1945
			internal bool flag;

			// Token: 0x0400079A RID: 1946
			public Vector3 previousPosition = new Vector3(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity);

			// Token: 0x0400079B RID: 1947
			public Quaternion previousRotation;
		}
	}
}
