using System;
using System.Collections.Generic;

namespace Pathfinding.Util
{
	// Token: 0x020000C7 RID: 199
	public class GridLookup<T> where T : class
	{
		// Token: 0x06000879 RID: 2169 RVA: 0x000386FC File Offset: 0x000368FC
		public GridLookup(Int2 size)
		{
			this.size = size;
			this.cells = new GridLookup<T>.Item[size.x * size.y];
			for (int i = 0; i < this.cells.Length; i++)
			{
				this.cells[i] = new GridLookup<T>.Item();
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x0600087A RID: 2170 RVA: 0x0003876F File Offset: 0x0003696F
		public GridLookup<T>.Root AllItems
		{
			get
			{
				return this.all.next;
			}
		}

		// Token: 0x0600087B RID: 2171 RVA: 0x0003877C File Offset: 0x0003697C
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

		// Token: 0x0600087C RID: 2172 RVA: 0x000387C0 File Offset: 0x000369C0
		public GridLookup<T>.Root GetRoot(T item)
		{
			GridLookup<T>.Root root;
			this.rootLookup.TryGetValue(item, out root);
			return root;
		}

		// Token: 0x0600087D RID: 2173 RVA: 0x000387E0 File Offset: 0x000369E0
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

		// Token: 0x0600087E RID: 2174 RVA: 0x00038850 File Offset: 0x00036A50
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

		// Token: 0x0600087F RID: 2175 RVA: 0x000388B8 File Offset: 0x00036AB8
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

		// Token: 0x06000880 RID: 2176 RVA: 0x00038A94 File Offset: 0x00036C94
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

		// Token: 0x040004EA RID: 1258
		private Int2 size;

		// Token: 0x040004EB RID: 1259
		private GridLookup<T>.Item[] cells;

		// Token: 0x040004EC RID: 1260
		private GridLookup<T>.Root all = new GridLookup<T>.Root();

		// Token: 0x040004ED RID: 1261
		private Dictionary<T, GridLookup<T>.Root> rootLookup = new Dictionary<T, GridLookup<T>.Root>();

		// Token: 0x040004EE RID: 1262
		private Stack<GridLookup<T>.Item> itemPool = new Stack<GridLookup<T>.Item>();

		// Token: 0x02000152 RID: 338
		internal class Item
		{
			// Token: 0x04000796 RID: 1942
			public GridLookup<T>.Root root;

			// Token: 0x04000797 RID: 1943
			public GridLookup<T>.Item prev;

			// Token: 0x04000798 RID: 1944
			public GridLookup<T>.Item next;
		}

		// Token: 0x02000153 RID: 339
		public class Root
		{
			// Token: 0x04000799 RID: 1945
			public T obj;

			// Token: 0x0400079A RID: 1946
			public GridLookup<T>.Root next;

			// Token: 0x0400079B RID: 1947
			internal GridLookup<T>.Root prev;

			// Token: 0x0400079C RID: 1948
			internal IntRect previousBounds = new IntRect(0, 0, -1, -1);

			// Token: 0x0400079D RID: 1949
			internal List<GridLookup<T>.Item> items = new List<GridLookup<T>.Item>();

			// Token: 0x0400079E RID: 1950
			internal bool flag;
		}
	}
}
