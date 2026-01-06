using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity;

namespace System.Text.RegularExpressions
{
	/// <summary>Represents the set of captures made by a single capturing group. </summary>
	// Token: 0x020001E8 RID: 488
	[DebuggerDisplay("Count = {Count}")]
	[DebuggerTypeProxy(typeof(CollectionDebuggerProxy<Capture>))]
	public class CaptureCollection : IList<Capture>, ICollection<Capture>, IEnumerable<Capture>, IEnumerable, IReadOnlyList<Capture>, IReadOnlyCollection<Capture>, IList, ICollection
	{
		// Token: 0x06000CC4 RID: 3268 RVA: 0x000354F7 File Offset: 0x000336F7
		internal CaptureCollection(Group group)
		{
			this._group = group;
			this._capcount = this._group._capcount;
		}

		/// <summary>Gets a value that indicates whether the collection is read only.</summary>
		/// <returns>true in all cases.</returns>
		// Token: 0x1700022F RID: 559
		// (get) Token: 0x06000CC5 RID: 3269 RVA: 0x0000390E File Offset: 0x00001B0E
		public bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets the number of substrings captured by the group.</summary>
		/// <returns>The number of items in the <see cref="T:System.Text.RegularExpressions.CaptureCollection" />.</returns>
		// Token: 0x17000230 RID: 560
		// (get) Token: 0x06000CC6 RID: 3270 RVA: 0x00035517 File Offset: 0x00033717
		public int Count
		{
			get
			{
				return this._capcount;
			}
		}

		/// <summary>Gets an individual member of the collection.</summary>
		/// <returns>The captured substring at position <paramref name="i" /> in the collection.</returns>
		/// <param name="i">Index into the capture collection. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="i" /> is less than 0 or greater than <see cref="P:System.Text.RegularExpressions.CaptureCollection.Count" />. </exception>
		// Token: 0x17000231 RID: 561
		public Capture this[int i]
		{
			get
			{
				return this.GetCapture(i);
			}
		}

		/// <summary>Provides an enumerator that iterates through the collection.</summary>
		/// <returns>An object that contains all <see cref="T:System.Text.RegularExpressions.Capture" /> objects within the <see cref="T:System.Text.RegularExpressions.CaptureCollection" />.</returns>
		// Token: 0x06000CC8 RID: 3272 RVA: 0x00035528 File Offset: 0x00033728
		public IEnumerator GetEnumerator()
		{
			return new CaptureCollection.Enumerator(this);
		}

		// Token: 0x06000CC9 RID: 3273 RVA: 0x00035528 File Offset: 0x00033728
		IEnumerator<Capture> IEnumerable<Capture>.GetEnumerator()
		{
			return new CaptureCollection.Enumerator(this);
		}

		// Token: 0x06000CCA RID: 3274 RVA: 0x00035530 File Offset: 0x00033730
		private Capture GetCapture(int i)
		{
			if (i == this._capcount - 1 && i >= 0)
			{
				return this._group;
			}
			if (i >= this._capcount || i < 0)
			{
				throw new ArgumentOutOfRangeException("i");
			}
			if (this._captures == null)
			{
				this.ForceInitialized();
			}
			return this._captures[i];
		}

		// Token: 0x06000CCB RID: 3275 RVA: 0x00035584 File Offset: 0x00033784
		internal void ForceInitialized()
		{
			this._captures = new Capture[this._capcount];
			for (int i = 0; i < this._capcount - 1; i++)
			{
				this._captures[i] = new Capture(this._group.Text, this._group._caps[i * 2], this._group._caps[i * 2 + 1]);
			}
		}

		/// <summary>Gets a value that indicates whether access to the collection is synchronized (thread-safe).</summary>
		/// <returns>false in all cases.</returns>
		// Token: 0x17000232 RID: 562
		// (get) Token: 0x06000CCC RID: 3276 RVA: 0x00003062 File Offset: 0x00001262
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the collection.</summary>
		/// <returns>An object that can be used to synchronize access to the collection.</returns>
		// Token: 0x17000233 RID: 563
		// (get) Token: 0x06000CCD RID: 3277 RVA: 0x000355ED File Offset: 0x000337ED
		public object SyncRoot
		{
			get
			{
				return this._group;
			}
		}

		/// <summary>Copies all the elements of the collection to the given array beginning at the given index.</summary>
		/// <param name="array">The array the collection is to be copied into. </param>
		/// <param name="arrayIndex">The position in the destination array where copying is to begin. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array " />is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="arrayIndex" /> is outside the bounds of <paramref name="array" />. -or-<paramref name="arrayIndex" /> plus <see cref="P:System.Text.RegularExpressions.CaptureCollection.Count" /> is outside the bounds of <paramref name="array" />. </exception>
		// Token: 0x06000CCE RID: 3278 RVA: 0x000355F8 File Offset: 0x000337F8
		public void CopyTo(Array array, int arrayIndex)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			int num = arrayIndex;
			for (int i = 0; i < this.Count; i++)
			{
				array.SetValue(this[i], num);
				num++;
			}
		}

		// Token: 0x06000CCF RID: 3279 RVA: 0x00035638 File Offset: 0x00033838
		public void CopyTo(Capture[] array, int arrayIndex)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (arrayIndex < 0 || arrayIndex > array.Length)
			{
				throw new ArgumentOutOfRangeException("arrayIndex");
			}
			if (array.Length - arrayIndex < this.Count)
			{
				throw new ArgumentException("Destination array is not long enough to copy all the items in the collection. Check array index and length.");
			}
			int num = arrayIndex;
			for (int i = 0; i < this.Count; i++)
			{
				array[num] = this[i];
				num++;
			}
		}

		// Token: 0x06000CD0 RID: 3280 RVA: 0x000356A4 File Offset: 0x000338A4
		int IList<Capture>.IndexOf(Capture item)
		{
			for (int i = 0; i < this.Count; i++)
			{
				if (EqualityComparer<Capture>.Default.Equals(this[i], item))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06000CD1 RID: 3281 RVA: 0x000356D9 File Offset: 0x000338D9
		void IList<Capture>.Insert(int index, Capture item)
		{
			throw new NotSupportedException("Collection is read-only.");
		}

		// Token: 0x06000CD2 RID: 3282 RVA: 0x000356D9 File Offset: 0x000338D9
		void IList<Capture>.RemoveAt(int index)
		{
			throw new NotSupportedException("Collection is read-only.");
		}

		// Token: 0x17000234 RID: 564
		Capture IList<Capture>.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				throw new NotSupportedException("Collection is read-only.");
			}
		}

		// Token: 0x06000CD5 RID: 3285 RVA: 0x000356D9 File Offset: 0x000338D9
		void ICollection<Capture>.Add(Capture item)
		{
			throw new NotSupportedException("Collection is read-only.");
		}

		// Token: 0x06000CD6 RID: 3286 RVA: 0x000356D9 File Offset: 0x000338D9
		void ICollection<Capture>.Clear()
		{
			throw new NotSupportedException("Collection is read-only.");
		}

		// Token: 0x06000CD7 RID: 3287 RVA: 0x000356EE File Offset: 0x000338EE
		bool ICollection<Capture>.Contains(Capture item)
		{
			return ((IList<Capture>)this).IndexOf(item) >= 0;
		}

		// Token: 0x06000CD8 RID: 3288 RVA: 0x000356D9 File Offset: 0x000338D9
		bool ICollection<Capture>.Remove(Capture item)
		{
			throw new NotSupportedException("Collection is read-only.");
		}

		// Token: 0x06000CD9 RID: 3289 RVA: 0x000356D9 File Offset: 0x000338D9
		int IList.Add(object value)
		{
			throw new NotSupportedException("Collection is read-only.");
		}

		// Token: 0x06000CDA RID: 3290 RVA: 0x000356D9 File Offset: 0x000338D9
		void IList.Clear()
		{
			throw new NotSupportedException("Collection is read-only.");
		}

		// Token: 0x06000CDB RID: 3291 RVA: 0x000356FD File Offset: 0x000338FD
		bool IList.Contains(object value)
		{
			return value is Capture && ((ICollection<Capture>)this).Contains((Capture)value);
		}

		// Token: 0x06000CDC RID: 3292 RVA: 0x00035715 File Offset: 0x00033915
		int IList.IndexOf(object value)
		{
			if (!(value is Capture))
			{
				return -1;
			}
			return ((IList<Capture>)this).IndexOf((Capture)value);
		}

		// Token: 0x06000CDD RID: 3293 RVA: 0x000356D9 File Offset: 0x000338D9
		void IList.Insert(int index, object value)
		{
			throw new NotSupportedException("Collection is read-only.");
		}

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x06000CDE RID: 3294 RVA: 0x0000390E File Offset: 0x00001B0E
		bool IList.IsFixedSize
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000CDF RID: 3295 RVA: 0x000356D9 File Offset: 0x000338D9
		void IList.Remove(object value)
		{
			throw new NotSupportedException("Collection is read-only.");
		}

		// Token: 0x06000CE0 RID: 3296 RVA: 0x000356D9 File Offset: 0x000338D9
		void IList.RemoveAt(int index)
		{
			throw new NotSupportedException("Collection is read-only.");
		}

		// Token: 0x17000236 RID: 566
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				throw new NotSupportedException("Collection is read-only.");
			}
		}

		// Token: 0x06000CE3 RID: 3299 RVA: 0x00013B26 File Offset: 0x00011D26
		internal CaptureCollection()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040007D4 RID: 2004
		private readonly Group _group;

		// Token: 0x040007D5 RID: 2005
		private readonly int _capcount;

		// Token: 0x040007D6 RID: 2006
		private Capture[] _captures;

		// Token: 0x020001E9 RID: 489
		[Serializable]
		private sealed class Enumerator : IEnumerator<Capture>, IDisposable, IEnumerator
		{
			// Token: 0x06000CE4 RID: 3300 RVA: 0x0003572D File Offset: 0x0003392D
			internal Enumerator(CaptureCollection collection)
			{
				this._collection = collection;
				this._index = -1;
			}

			// Token: 0x06000CE5 RID: 3301 RVA: 0x00035744 File Offset: 0x00033944
			public bool MoveNext()
			{
				int count = this._collection.Count;
				if (this._index >= count)
				{
					return false;
				}
				this._index++;
				return this._index < count;
			}

			// Token: 0x17000237 RID: 567
			// (get) Token: 0x06000CE6 RID: 3302 RVA: 0x0003577F File Offset: 0x0003397F
			public Capture Current
			{
				get
				{
					if (this._index < 0 || this._index >= this._collection.Count)
					{
						throw new InvalidOperationException("Enumeration has either not started or has already finished.");
					}
					return this._collection[this._index];
				}
			}

			// Token: 0x17000238 RID: 568
			// (get) Token: 0x06000CE7 RID: 3303 RVA: 0x000357B9 File Offset: 0x000339B9
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06000CE8 RID: 3304 RVA: 0x000357C1 File Offset: 0x000339C1
			void IEnumerator.Reset()
			{
				this._index = -1;
			}

			// Token: 0x06000CE9 RID: 3305 RVA: 0x00003917 File Offset: 0x00001B17
			void IDisposable.Dispose()
			{
			}

			// Token: 0x040007D7 RID: 2007
			private readonly CaptureCollection _collection;

			// Token: 0x040007D8 RID: 2008
			private int _index;
		}
	}
}
