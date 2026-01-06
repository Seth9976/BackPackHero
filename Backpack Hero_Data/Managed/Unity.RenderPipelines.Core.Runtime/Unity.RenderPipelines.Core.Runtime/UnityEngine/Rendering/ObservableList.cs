using System;
using System.Collections;
using System.Collections.Generic;

namespace UnityEngine.Rendering
{
	// Token: 0x02000061 RID: 97
	public class ObservableList<T> : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable
	{
		// Token: 0x14000005 RID: 5
		// (add) Token: 0x06000305 RID: 773 RVA: 0x0000EE80 File Offset: 0x0000D080
		// (remove) Token: 0x06000306 RID: 774 RVA: 0x0000EEB8 File Offset: 0x0000D0B8
		public event ListChangedEventHandler<T> ItemAdded;

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x06000307 RID: 775 RVA: 0x0000EEF0 File Offset: 0x0000D0F0
		// (remove) Token: 0x06000308 RID: 776 RVA: 0x0000EF28 File Offset: 0x0000D128
		public event ListChangedEventHandler<T> ItemRemoved;

		// Token: 0x17000050 RID: 80
		public T this[int index]
		{
			get
			{
				return this.m_List[index];
			}
			set
			{
				this.OnEvent(this.ItemRemoved, index, this.m_List[index]);
				this.m_List[index] = value;
				this.OnEvent(this.ItemAdded, index, value);
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600030B RID: 779 RVA: 0x0000EFA1 File Offset: 0x0000D1A1
		public int Count
		{
			get
			{
				return this.m_List.Count;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x0600030C RID: 780 RVA: 0x0000EFAE File Offset: 0x0000D1AE
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600030D RID: 781 RVA: 0x0000EFB1 File Offset: 0x0000D1B1
		public ObservableList()
			: this(0)
		{
		}

		// Token: 0x0600030E RID: 782 RVA: 0x0000EFBA File Offset: 0x0000D1BA
		public ObservableList(int capacity)
		{
			this.m_List = new List<T>(capacity);
		}

		// Token: 0x0600030F RID: 783 RVA: 0x0000EFCE File Offset: 0x0000D1CE
		public ObservableList(IEnumerable<T> collection)
		{
			this.m_List = new List<T>(collection);
		}

		// Token: 0x06000310 RID: 784 RVA: 0x0000EFE2 File Offset: 0x0000D1E2
		private void OnEvent(ListChangedEventHandler<T> e, int index, T item)
		{
			if (e != null)
			{
				e(this, new ListChangedEventArgs<T>(index, item));
			}
		}

		// Token: 0x06000311 RID: 785 RVA: 0x0000EFF5 File Offset: 0x0000D1F5
		public bool Contains(T item)
		{
			return this.m_List.Contains(item);
		}

		// Token: 0x06000312 RID: 786 RVA: 0x0000F003 File Offset: 0x0000D203
		public int IndexOf(T item)
		{
			return this.m_List.IndexOf(item);
		}

		// Token: 0x06000313 RID: 787 RVA: 0x0000F011 File Offset: 0x0000D211
		public void Add(T item)
		{
			this.m_List.Add(item);
			this.OnEvent(this.ItemAdded, this.m_List.IndexOf(item), item);
		}

		// Token: 0x06000314 RID: 788 RVA: 0x0000F038 File Offset: 0x0000D238
		public void Add(params T[] items)
		{
			foreach (T t in items)
			{
				this.Add(t);
			}
		}

		// Token: 0x06000315 RID: 789 RVA: 0x0000F064 File Offset: 0x0000D264
		public void Insert(int index, T item)
		{
			this.m_List.Insert(index, item);
			this.OnEvent(this.ItemAdded, index, item);
		}

		// Token: 0x06000316 RID: 790 RVA: 0x0000F084 File Offset: 0x0000D284
		public bool Remove(T item)
		{
			int num = this.m_List.IndexOf(item);
			bool flag = this.m_List.Remove(item);
			if (flag)
			{
				this.OnEvent(this.ItemRemoved, num, item);
			}
			return flag;
		}

		// Token: 0x06000317 RID: 791 RVA: 0x0000F0BC File Offset: 0x0000D2BC
		public int Remove(params T[] items)
		{
			if (items == null)
			{
				return 0;
			}
			int num = 0;
			foreach (T t in items)
			{
				num += (this.Remove(t) ? 1 : 0);
			}
			return num;
		}

		// Token: 0x06000318 RID: 792 RVA: 0x0000F0FC File Offset: 0x0000D2FC
		public void RemoveAt(int index)
		{
			T t = this.m_List[index];
			this.m_List.RemoveAt(index);
			this.OnEvent(this.ItemRemoved, index, t);
		}

		// Token: 0x06000319 RID: 793 RVA: 0x0000F130 File Offset: 0x0000D330
		public void Clear()
		{
			for (int i = 0; i < this.Count; i++)
			{
				this.RemoveAt(i);
			}
		}

		// Token: 0x0600031A RID: 794 RVA: 0x0000F155 File Offset: 0x0000D355
		public void CopyTo(T[] array, int arrayIndex)
		{
			this.m_List.CopyTo(array, arrayIndex);
		}

		// Token: 0x0600031B RID: 795 RVA: 0x0000F164 File Offset: 0x0000D364
		public IEnumerator<T> GetEnumerator()
		{
			return this.m_List.GetEnumerator();
		}

		// Token: 0x0600031C RID: 796 RVA: 0x0000F171 File Offset: 0x0000D371
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04000202 RID: 514
		private IList<T> m_List;
	}
}
