using System;
using System.Collections;
using System.Collections.Generic;

namespace Unity.VisualScripting
{
	// Token: 0x02000022 RID: 34
	public class VariantCollection<TBase, TImplementation> : ICollection<TBase>, IEnumerable<TBase>, IEnumerable where TImplementation : TBase
	{
		// Token: 0x06000143 RID: 323 RVA: 0x00004020 File Offset: 0x00002220
		public VariantCollection(ICollection<TImplementation> implementation)
		{
			if (implementation == null)
			{
				throw new ArgumentNullException("implementation");
			}
			this.implementation = implementation;
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000144 RID: 324 RVA: 0x0000403D File Offset: 0x0000223D
		// (set) Token: 0x06000145 RID: 325 RVA: 0x00004045 File Offset: 0x00002245
		public ICollection<TImplementation> implementation { get; private set; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000146 RID: 326 RVA: 0x0000404E File Offset: 0x0000224E
		public int Count
		{
			get
			{
				return this.implementation.Count;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000147 RID: 327 RVA: 0x0000405B File Offset: 0x0000225B
		public bool IsReadOnly
		{
			get
			{
				return this.implementation.IsReadOnly;
			}
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00004068 File Offset: 0x00002268
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00004070 File Offset: 0x00002270
		public IEnumerator<TBase> GetEnumerator()
		{
			foreach (TImplementation timplementation in this.implementation)
			{
				yield return (TBase)((object)timplementation);
			}
			IEnumerator<TImplementation> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x0600014A RID: 330 RVA: 0x0000407F File Offset: 0x0000227F
		public void Add(TBase item)
		{
			if (!(item is TImplementation))
			{
				throw new NotSupportedException();
			}
			this.implementation.Add((TImplementation)((object)item));
		}

		// Token: 0x0600014B RID: 331 RVA: 0x000040AA File Offset: 0x000022AA
		public void Clear()
		{
			this.implementation.Clear();
		}

		// Token: 0x0600014C RID: 332 RVA: 0x000040B7 File Offset: 0x000022B7
		public bool Contains(TBase item)
		{
			if (!(item is TImplementation))
			{
				throw new NotSupportedException();
			}
			return this.implementation.Contains((TImplementation)((object)item));
		}

		// Token: 0x0600014D RID: 333 RVA: 0x000040E2 File Offset: 0x000022E2
		public bool Remove(TBase item)
		{
			if (!(item is TImplementation))
			{
				throw new NotSupportedException();
			}
			return this.implementation.Remove((TImplementation)((object)item));
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00004110 File Offset: 0x00002310
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
	}
}
