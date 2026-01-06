using System;
using System.Collections;
using System.Collections.Generic;

namespace Unity.VisualScripting
{
	// Token: 0x02000020 RID: 32
	public class NonNullableHashSet<T> : ISet<T>, ICollection<T>, IEnumerable<T>, IEnumerable
	{
		// Token: 0x0600010E RID: 270 RVA: 0x00003C24 File Offset: 0x00001E24
		public NonNullableHashSet()
		{
			this.set = new HashSet<T>();
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00003C37 File Offset: 0x00001E37
		public NonNullableHashSet(IEqualityComparer<T> comparer)
		{
			this.set = new HashSet<T>(comparer);
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00003C4B File Offset: 0x00001E4B
		public NonNullableHashSet(IEnumerable<T> collection)
		{
			this.set = new HashSet<T>(collection);
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00003C5F File Offset: 0x00001E5F
		public NonNullableHashSet(IEnumerable<T> collection, IEqualityComparer<T> comparer)
		{
			this.set = new HashSet<T>(collection, comparer);
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000112 RID: 274 RVA: 0x00003C74 File Offset: 0x00001E74
		public int Count
		{
			get
			{
				return this.set.Count;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000113 RID: 275 RVA: 0x00003C81 File Offset: 0x00001E81
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00003C84 File Offset: 0x00001E84
		public bool Add(T item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			return this.set.Add(item);
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00003CA5 File Offset: 0x00001EA5
		public void Clear()
		{
			this.set.Clear();
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00003CB2 File Offset: 0x00001EB2
		public bool Contains(T item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			return this.set.Contains(item);
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00003CD3 File Offset: 0x00001ED3
		public void CopyTo(T[] array, int arrayIndex)
		{
			this.set.CopyTo(array, arrayIndex);
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00003CE2 File Offset: 0x00001EE2
		public void ExceptWith(IEnumerable<T> other)
		{
			this.set.ExceptWith(other);
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00003CF0 File Offset: 0x00001EF0
		public IEnumerator<T> GetEnumerator()
		{
			return this.set.GetEnumerator();
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00003D02 File Offset: 0x00001F02
		public void IntersectWith(IEnumerable<T> other)
		{
			this.set.IntersectWith(other);
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00003D10 File Offset: 0x00001F10
		public bool IsProperSubsetOf(IEnumerable<T> other)
		{
			return this.set.IsProperSubsetOf(other);
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00003D1E File Offset: 0x00001F1E
		public bool IsProperSupersetOf(IEnumerable<T> other)
		{
			return this.set.IsProperSupersetOf(other);
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00003D2C File Offset: 0x00001F2C
		public bool IsSubsetOf(IEnumerable<T> other)
		{
			return this.set.IsSubsetOf(other);
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00003D3A File Offset: 0x00001F3A
		public bool IsSupersetOf(IEnumerable<T> other)
		{
			return this.set.IsSupersetOf(other);
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00003D48 File Offset: 0x00001F48
		public bool Overlaps(IEnumerable<T> other)
		{
			return this.set.Overlaps(other);
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00003D56 File Offset: 0x00001F56
		public bool Remove(T item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			return this.set.Remove(item);
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00003D77 File Offset: 0x00001F77
		public bool SetEquals(IEnumerable<T> other)
		{
			return this.set.SetEquals(other);
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00003D85 File Offset: 0x00001F85
		public void SymmetricExceptWith(IEnumerable<T> other)
		{
			this.set.SymmetricExceptWith(other);
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00003D93 File Offset: 0x00001F93
		public void UnionWith(IEnumerable<T> other)
		{
			this.set.UnionWith(other);
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00003DA1 File Offset: 0x00001FA1
		void ICollection<T>.Add(T item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			((ICollection<T>)this.set).Add(item);
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00003DC2 File Offset: 0x00001FC2
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable)this.set).GetEnumerator();
		}

		// Token: 0x0400001E RID: 30
		private readonly HashSet<T> set;
	}
}
