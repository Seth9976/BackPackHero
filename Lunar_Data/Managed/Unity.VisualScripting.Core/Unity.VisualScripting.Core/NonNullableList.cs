using System;
using System.Collections;
using System.Collections.Generic;

namespace Unity.VisualScripting
{
	// Token: 0x02000021 RID: 33
	public class NonNullableList<T> : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable, IList, ICollection
	{
		// Token: 0x06000126 RID: 294 RVA: 0x00003DCF File Offset: 0x00001FCF
		public NonNullableList()
		{
			this.list = new List<T>();
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00003DE2 File Offset: 0x00001FE2
		public NonNullableList(int capacity)
		{
			this.list = new List<T>(capacity);
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00003DF6 File Offset: 0x00001FF6
		public NonNullableList(IEnumerable<T> collection)
		{
			this.list = new List<T>(collection);
		}

		// Token: 0x17000036 RID: 54
		public T this[int index]
		{
			get
			{
				return this.list[index];
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.list[index] = value;
			}
		}

		// Token: 0x17000037 RID: 55
		object IList.this[int index]
		{
			get
			{
				return ((IList)this.list)[index];
			}
			set
			{
				((IList)this.list)[index] = value;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600012D RID: 301 RVA: 0x00003E57 File Offset: 0x00002057
		public int Count
		{
			get
			{
				return this.list.Count;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600012E RID: 302 RVA: 0x00003E64 File Offset: 0x00002064
		public bool IsSynchronized
		{
			get
			{
				return ((ICollection)this.list).IsSynchronized;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x0600012F RID: 303 RVA: 0x00003E71 File Offset: 0x00002071
		public object SyncRoot
		{
			get
			{
				return ((ICollection)this.list).SyncRoot;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000130 RID: 304 RVA: 0x00003E7E File Offset: 0x0000207E
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000131 RID: 305 RVA: 0x00003E81 File Offset: 0x00002081
		public bool IsFixedSize
		{
			get
			{
				return ((IList)this.list).IsFixedSize;
			}
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00003E8E File Offset: 0x0000208E
		public void CopyTo(Array array, int index)
		{
			((ICollection)this.list).CopyTo(array, index);
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00003E9D File Offset: 0x0000209D
		public void Add(T item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			this.list.Add(item);
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00003EBE File Offset: 0x000020BE
		public int Add(object value)
		{
			return ((IList)this.list).Add(value);
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00003ECC File Offset: 0x000020CC
		public void Clear()
		{
			this.list.Clear();
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00003ED9 File Offset: 0x000020D9
		public bool Contains(object value)
		{
			return ((IList)this.list).Contains(value);
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00003EE7 File Offset: 0x000020E7
		public int IndexOf(object value)
		{
			return ((IList)this.list).IndexOf(value);
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00003EF5 File Offset: 0x000020F5
		public void Insert(int index, object value)
		{
			((IList)this.list).Insert(index, value);
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00003F04 File Offset: 0x00002104
		public void Remove(object value)
		{
			((IList)this.list).Remove(value);
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00003F12 File Offset: 0x00002112
		public bool Contains(T item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			return this.list.Contains(item);
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00003F33 File Offset: 0x00002133
		public void CopyTo(T[] array, int arrayIndex)
		{
			this.list.CopyTo(array, arrayIndex);
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00003F42 File Offset: 0x00002142
		public IEnumerator<T> GetEnumerator()
		{
			return this.list.GetEnumerator();
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00003F54 File Offset: 0x00002154
		public int IndexOf(T item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			return this.list.IndexOf(item);
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00003F75 File Offset: 0x00002175
		public void Insert(int index, T item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			this.list.Insert(index, item);
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00003F97 File Offset: 0x00002197
		public bool Remove(T item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			return this.list.Remove(item);
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00003FB8 File Offset: 0x000021B8
		public void RemoveAt(int index)
		{
			this.list.RemoveAt(index);
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00003FC6 File Offset: 0x000021C6
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.list.GetEnumerator();
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00003FD8 File Offset: 0x000021D8
		public void AddRange(IEnumerable<T> collection)
		{
			foreach (T t in collection)
			{
				this.Add(t);
			}
		}

		// Token: 0x0400001F RID: 31
		private readonly List<T> list;
	}
}
