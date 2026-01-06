using System;
using System.Collections;
using System.Collections.Generic;

namespace Unity.VisualScripting
{
	// Token: 0x0200001C RID: 28
	public class MergedList<T> : IMergedCollection<T>, ICollection<T>, IEnumerable<T>, IEnumerable
	{
		// Token: 0x060000D0 RID: 208 RVA: 0x000035FB File Offset: 0x000017FB
		public MergedList()
		{
			this.lists = new Dictionary<Type, IList<T>>();
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x00003610 File Offset: 0x00001810
		public int Count
		{
			get
			{
				int num = 0;
				foreach (KeyValuePair<Type, IList<T>> keyValuePair in this.lists)
				{
					num += keyValuePair.Value.Count;
				}
				return num;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x00003670 File Offset: 0x00001870
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00003673 File Offset: 0x00001873
		public virtual void Include<TI>(IList<TI> list) where TI : T
		{
			this.lists.Add(typeof(TI), new VariantList<T, TI>(list));
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00003690 File Offset: 0x00001890
		public bool Includes<TI>() where TI : T
		{
			return this.Includes(typeof(TI));
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x000036A2 File Offset: 0x000018A2
		public bool Includes(Type elementType)
		{
			return this.GetListForType(elementType, false) != null;
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x000036AF File Offset: 0x000018AF
		public IList<TI> ForType<TI>() where TI : T
		{
			return ((VariantList<T, TI>)this.GetListForType(typeof(TI), true)).implementation;
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x000036CC File Offset: 0x000018CC
		protected IList<T> GetListForItem(T item)
		{
			Ensure.That("item").IsNotNull<T>(item);
			return this.GetListForType(item.GetType(), true);
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x000036F4 File Offset: 0x000018F4
		protected IList<T> GetListForType(Type type, bool throwOnFail = true)
		{
			if (this.lists.ContainsKey(type))
			{
				return this.lists[type];
			}
			foreach (KeyValuePair<Type, IList<T>> keyValuePair in this.lists)
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

		// Token: 0x060000D9 RID: 217 RVA: 0x0000378C File Offset: 0x0000198C
		public bool Contains(T item)
		{
			return this.GetListForItem(item).Contains(item);
		}

		// Token: 0x060000DA RID: 218 RVA: 0x0000379B File Offset: 0x0000199B
		public virtual void Add(T item)
		{
			this.GetListForItem(item).Add(item);
		}

		// Token: 0x060000DB RID: 219 RVA: 0x000037AC File Offset: 0x000019AC
		public virtual void Clear()
		{
			foreach (KeyValuePair<Type, IList<T>> keyValuePair in this.lists)
			{
				keyValuePair.Value.Clear();
			}
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00003804 File Offset: 0x00001A04
		public virtual bool Remove(T item)
		{
			return this.GetListForItem(item).Remove(item);
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00003814 File Offset: 0x00001A14
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
			foreach (KeyValuePair<Type, IList<T>> keyValuePair in this.lists)
			{
				IList<T> value = keyValuePair.Value;
				value.CopyTo(array, arrayIndex + num);
				num += value.Count;
			}
		}

		// Token: 0x060000DE RID: 222 RVA: 0x000038B0 File Offset: 0x00001AB0
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x060000DF RID: 223 RVA: 0x000038BD File Offset: 0x00001ABD
		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x000038CA File Offset: 0x00001ACA
		public MergedList<T>.Enumerator GetEnumerator()
		{
			return new MergedList<T>.Enumerator(this);
		}

		// Token: 0x04000018 RID: 24
		protected readonly Dictionary<Type, IList<T>> lists;

		// Token: 0x020001B9 RID: 441
		public struct Enumerator : IEnumerator<T>, IEnumerator, IDisposable
		{
			// Token: 0x06000BC2 RID: 3010 RVA: 0x00031D88 File Offset: 0x0002FF88
			public Enumerator(MergedList<T> merged)
			{
				this = default(MergedList<T>.Enumerator);
				this.listsEnumerator = merged.lists.GetEnumerator();
			}

			// Token: 0x06000BC3 RID: 3011 RVA: 0x00031DA2 File Offset: 0x0002FFA2
			public void Dispose()
			{
			}

			// Token: 0x06000BC4 RID: 3012 RVA: 0x00031DA4 File Offset: 0x0002FFA4
			public bool MoveNext()
			{
				if (this.currentList == null)
				{
					if (!this.listsEnumerator.MoveNext())
					{
						this.currentItem = default(T);
						this.exceeded = true;
						return false;
					}
					KeyValuePair<Type, IList<T>> keyValuePair = this.listsEnumerator.Current;
					this.currentList = keyValuePair.Value;
					if (this.currentList == null)
					{
						throw new InvalidOperationException("Merged sub list is null.");
					}
				}
				if (this.indexInCurrentList < this.currentList.Count)
				{
					this.currentItem = this.currentList[this.indexInCurrentList];
					this.indexInCurrentList++;
					return true;
				}
				while (this.listsEnumerator.MoveNext())
				{
					KeyValuePair<Type, IList<T>> keyValuePair = this.listsEnumerator.Current;
					this.currentList = keyValuePair.Value;
					this.indexInCurrentList = 0;
					if (this.currentList == null)
					{
						throw new InvalidOperationException("Merged sub list is null.");
					}
					if (this.indexInCurrentList < this.currentList.Count)
					{
						this.currentItem = this.currentList[this.indexInCurrentList];
						this.indexInCurrentList++;
						return true;
					}
				}
				this.currentItem = default(T);
				this.exceeded = true;
				return false;
			}

			// Token: 0x17000200 RID: 512
			// (get) Token: 0x06000BC5 RID: 3013 RVA: 0x00031ED2 File Offset: 0x000300D2
			public T Current
			{
				get
				{
					return this.currentItem;
				}
			}

			// Token: 0x17000201 RID: 513
			// (get) Token: 0x06000BC6 RID: 3014 RVA: 0x00031EDA File Offset: 0x000300DA
			object IEnumerator.Current
			{
				get
				{
					if (this.exceeded)
					{
						throw new InvalidOperationException();
					}
					return this.Current;
				}
			}

			// Token: 0x06000BC7 RID: 3015 RVA: 0x00031EF5 File Offset: 0x000300F5
			void IEnumerator.Reset()
			{
				throw new InvalidOperationException();
			}

			// Token: 0x040002E6 RID: 742
			private Dictionary<Type, IList<T>>.Enumerator listsEnumerator;

			// Token: 0x040002E7 RID: 743
			private T currentItem;

			// Token: 0x040002E8 RID: 744
			private IList<T> currentList;

			// Token: 0x040002E9 RID: 745
			private int indexInCurrentList;

			// Token: 0x040002EA RID: 746
			private bool exceeded;
		}
	}
}
