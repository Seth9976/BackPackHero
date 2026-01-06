using System;

namespace System.Collections.Specialized
{
	// Token: 0x020007CC RID: 1996
	internal sealed class ReadOnlyList : IList, ICollection, IEnumerable
	{
		// Token: 0x06003F86 RID: 16262 RVA: 0x000DE6CA File Offset: 0x000DC8CA
		internal ReadOnlyList(IList list)
		{
			this._list = list;
		}

		// Token: 0x17000E70 RID: 3696
		// (get) Token: 0x06003F87 RID: 16263 RVA: 0x000DE6D9 File Offset: 0x000DC8D9
		public int Count
		{
			get
			{
				return this._list.Count;
			}
		}

		// Token: 0x17000E71 RID: 3697
		// (get) Token: 0x06003F88 RID: 16264 RVA: 0x0000390E File Offset: 0x00001B0E
		public bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000E72 RID: 3698
		// (get) Token: 0x06003F89 RID: 16265 RVA: 0x0000390E File Offset: 0x00001B0E
		public bool IsFixedSize
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000E73 RID: 3699
		// (get) Token: 0x06003F8A RID: 16266 RVA: 0x000DE6E6 File Offset: 0x000DC8E6
		public bool IsSynchronized
		{
			get
			{
				return this._list.IsSynchronized;
			}
		}

		// Token: 0x17000E74 RID: 3700
		public object this[int index]
		{
			get
			{
				return this._list[index];
			}
			set
			{
				throw new NotSupportedException("Collection is read-only.");
			}
		}

		// Token: 0x17000E75 RID: 3701
		// (get) Token: 0x06003F8D RID: 16269 RVA: 0x000DE701 File Offset: 0x000DC901
		public object SyncRoot
		{
			get
			{
				return this._list.SyncRoot;
			}
		}

		// Token: 0x06003F8E RID: 16270 RVA: 0x000356D9 File Offset: 0x000338D9
		public int Add(object value)
		{
			throw new NotSupportedException("Collection is read-only.");
		}

		// Token: 0x06003F8F RID: 16271 RVA: 0x000356D9 File Offset: 0x000338D9
		public void Clear()
		{
			throw new NotSupportedException("Collection is read-only.");
		}

		// Token: 0x06003F90 RID: 16272 RVA: 0x000DE70E File Offset: 0x000DC90E
		public bool Contains(object value)
		{
			return this._list.Contains(value);
		}

		// Token: 0x06003F91 RID: 16273 RVA: 0x000DE71C File Offset: 0x000DC91C
		public void CopyTo(Array array, int index)
		{
			this._list.CopyTo(array, index);
		}

		// Token: 0x06003F92 RID: 16274 RVA: 0x000DE72B File Offset: 0x000DC92B
		public IEnumerator GetEnumerator()
		{
			return this._list.GetEnumerator();
		}

		// Token: 0x06003F93 RID: 16275 RVA: 0x000DE738 File Offset: 0x000DC938
		public int IndexOf(object value)
		{
			return this._list.IndexOf(value);
		}

		// Token: 0x06003F94 RID: 16276 RVA: 0x000356D9 File Offset: 0x000338D9
		public void Insert(int index, object value)
		{
			throw new NotSupportedException("Collection is read-only.");
		}

		// Token: 0x06003F95 RID: 16277 RVA: 0x000356D9 File Offset: 0x000338D9
		public void Remove(object value)
		{
			throw new NotSupportedException("Collection is read-only.");
		}

		// Token: 0x06003F96 RID: 16278 RVA: 0x000356D9 File Offset: 0x000338D9
		public void RemoveAt(int index)
		{
			throw new NotSupportedException("Collection is read-only.");
		}

		// Token: 0x0400268B RID: 9867
		private readonly IList _list;
	}
}
