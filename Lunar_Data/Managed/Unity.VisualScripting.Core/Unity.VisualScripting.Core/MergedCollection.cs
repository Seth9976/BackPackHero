using System;
using System.Collections;
using System.Collections.Generic;

namespace Unity.VisualScripting
{
	// Token: 0x0200001A RID: 26
	public class MergedCollection<T> : IMergedCollection<T>, ICollection<T>, IEnumerable<T>, IEnumerable
	{
		// Token: 0x060000AA RID: 170 RVA: 0x00002ECC File Offset: 0x000010CC
		public MergedCollection()
		{
			this.collections = new Dictionary<Type, ICollection<T>>();
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000AB RID: 171 RVA: 0x00002EE0 File Offset: 0x000010E0
		public int Count
		{
			get
			{
				int num = 0;
				foreach (ICollection<T> collection in this.collections.Values)
				{
					num += collection.Count;
				}
				return num;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000AC RID: 172 RVA: 0x00002F40 File Offset: 0x00001140
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00002F43 File Offset: 0x00001143
		public void Include<TI>(ICollection<TI> collection) where TI : T
		{
			this.collections.Add(typeof(TI), new VariantCollection<T, TI>(collection));
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00002F60 File Offset: 0x00001160
		public bool Includes<TI>() where TI : T
		{
			return this.Includes(typeof(TI));
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00002F72 File Offset: 0x00001172
		public bool Includes(Type implementationType)
		{
			return this.GetCollectionForType(implementationType, false) != null;
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00002F7F File Offset: 0x0000117F
		public ICollection<TI> ForType<TI>() where TI : T
		{
			return ((VariantCollection<T, TI>)this.GetCollectionForType(typeof(TI), true)).implementation;
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00002F9C File Offset: 0x0000119C
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00002FA4 File Offset: 0x000011A4
		public IEnumerator<T> GetEnumerator()
		{
			foreach (ICollection<T> collection in this.collections.Values)
			{
				foreach (T t in collection)
				{
					yield return t;
				}
				IEnumerator<T> enumerator2 = null;
			}
			Dictionary<Type, ICollection<T>>.ValueCollection.Enumerator enumerator = default(Dictionary<Type, ICollection<T>>.ValueCollection.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00002FB3 File Offset: 0x000011B3
		private ICollection<T> GetCollectionForItem(T item)
		{
			Ensure.That("item").IsNotNull<T>(item);
			return this.GetCollectionForType(item.GetType(), true);
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00002FDC File Offset: 0x000011DC
		private ICollection<T> GetCollectionForType(Type type, bool throwOnFail = true)
		{
			if (this.collections.ContainsKey(type))
			{
				return this.collections[type];
			}
			foreach (KeyValuePair<Type, ICollection<T>> keyValuePair in this.collections)
			{
				if (keyValuePair.Key.IsAssignableFrom(type))
				{
					return keyValuePair.Value;
				}
			}
			if (throwOnFail)
			{
				throw new InvalidOperationException(string.Format("No sub-collection available for type '{0}'.", type));
			}
			return null;
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00003074 File Offset: 0x00001274
		public bool Contains(T item)
		{
			return this.GetCollectionForItem(item).Contains(item);
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00003083 File Offset: 0x00001283
		public virtual void Add(T item)
		{
			this.GetCollectionForItem(item).Add(item);
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00003094 File Offset: 0x00001294
		public virtual void Clear()
		{
			foreach (ICollection<T> collection in this.collections.Values)
			{
				collection.Clear();
			}
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x000030EC File Offset: 0x000012EC
		public virtual bool Remove(T item)
		{
			return this.GetCollectionForItem(item).Remove(item);
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x000030FC File Offset: 0x000012FC
		public void CopyTo(T[] array, int arrayIndex)
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
			int num = 0;
			foreach (ICollection<T> collection in this.collections.Values)
			{
				collection.CopyTo(array, arrayIndex + num);
				num += collection.Count;
			}
		}

		// Token: 0x04000015 RID: 21
		private readonly Dictionary<Type, ICollection<T>> collections;
	}
}
