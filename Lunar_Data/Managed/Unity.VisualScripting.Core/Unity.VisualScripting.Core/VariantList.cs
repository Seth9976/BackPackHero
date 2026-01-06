using System;
using System.Collections;
using System.Collections.Generic;

namespace Unity.VisualScripting
{
	// Token: 0x02000024 RID: 36
	public class VariantList<TBase, TImplementation> : IList<TBase>, ICollection<TBase>, IEnumerable<TBase>, IEnumerable where TImplementation : TBase
	{
		// Token: 0x06000157 RID: 343 RVA: 0x0000422C File Offset: 0x0000242C
		public VariantList(IList<TImplementation> implementation)
		{
			if (implementation == null)
			{
				throw new ArgumentNullException("implementation");
			}
			this.implementation = implementation;
		}

		// Token: 0x17000043 RID: 67
		public TBase this[int index]
		{
			get
			{
				return (TBase)((object)this.implementation[index]);
			}
			set
			{
				if (!(value is TImplementation))
				{
					throw new NotSupportedException();
				}
				this.implementation[index] = (TImplementation)((object)value);
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600015A RID: 346 RVA: 0x0000428D File Offset: 0x0000248D
		// (set) Token: 0x0600015B RID: 347 RVA: 0x00004295 File Offset: 0x00002495
		public IList<TImplementation> implementation { get; private set; }

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x0600015C RID: 348 RVA: 0x0000429E File Offset: 0x0000249E
		public int Count
		{
			get
			{
				return this.implementation.Count;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x0600015D RID: 349 RVA: 0x000042AB File Offset: 0x000024AB
		public bool IsReadOnly
		{
			get
			{
				return this.implementation.IsReadOnly;
			}
		}

		// Token: 0x0600015E RID: 350 RVA: 0x000042B8 File Offset: 0x000024B8
		public void Add(TBase item)
		{
			if (!(item is TImplementation))
			{
				throw new NotSupportedException();
			}
			this.implementation.Add((TImplementation)((object)item));
		}

		// Token: 0x0600015F RID: 351 RVA: 0x000042E3 File Offset: 0x000024E3
		public void Clear()
		{
			this.implementation.Clear();
		}

		// Token: 0x06000160 RID: 352 RVA: 0x000042F0 File Offset: 0x000024F0
		public bool Contains(TBase item)
		{
			if (!(item is TImplementation))
			{
				throw new NotSupportedException();
			}
			return this.implementation.Contains((TImplementation)((object)item));
		}

		// Token: 0x06000161 RID: 353 RVA: 0x0000431B File Offset: 0x0000251B
		public bool Remove(TBase item)
		{
			if (!(item is TImplementation))
			{
				throw new NotSupportedException();
			}
			return this.implementation.Remove((TImplementation)((object)item));
		}

		// Token: 0x06000162 RID: 354 RVA: 0x00004348 File Offset: 0x00002548
		public void CopyTo(TBase[] array, int arrayIndex)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (arrayIndex < 0)
			{
				throw new ArgumentOutOfRangeException("arrayIndex");
			}
			if (array.Length - arrayIndex < this.Count)
			{
				throw new ArgumentException();
			}
			TImplementation[] array2 = new TImplementation[this.Count];
			this.implementation.CopyTo(array2, 0);
			for (int i = 0; i < this.Count; i++)
			{
				array[i + arrayIndex] = (TBase)((object)array2[i]);
			}
		}

		// Token: 0x06000163 RID: 355 RVA: 0x000043C9 File Offset: 0x000025C9
		public int IndexOf(TBase item)
		{
			if (!(item is TImplementation))
			{
				throw new NotSupportedException();
			}
			return this.implementation.IndexOf((TImplementation)((object)item));
		}

		// Token: 0x06000164 RID: 356 RVA: 0x000043F4 File Offset: 0x000025F4
		public void Insert(int index, TBase item)
		{
			if (!(item is TImplementation))
			{
				throw new NotSupportedException();
			}
			this.implementation.Insert(index, (TImplementation)((object)item));
		}

		// Token: 0x06000165 RID: 357 RVA: 0x00004420 File Offset: 0x00002620
		public void RemoveAt(int index)
		{
			this.implementation.RemoveAt(index);
		}

		// Token: 0x06000166 RID: 358 RVA: 0x0000442E File Offset: 0x0000262E
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000167 RID: 359 RVA: 0x0000443B File Offset: 0x0000263B
		IEnumerator<TBase> IEnumerable<TBase>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000168 RID: 360 RVA: 0x00004448 File Offset: 0x00002648
		public NoAllocEnumerator<TBase> GetEnumerator()
		{
			return new NoAllocEnumerator<TBase>(this);
		}
	}
}
