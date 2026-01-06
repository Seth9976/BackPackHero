using System;
using System.Diagnostics;
using System.Threading;

namespace System.Collections
{
	/// <summary>Represents a first-in, first-out collection of objects.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000A27 RID: 2599
	[DebuggerTypeProxy(typeof(Queue.QueueDebugView))]
	[DebuggerDisplay("Count = {Count}")]
	[Serializable]
	public class Queue : ICollection, IEnumerable, ICloneable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Queue" /> class that is empty, has the default initial capacity, and uses the default growth factor.</summary>
		// Token: 0x06005C15 RID: 23573 RVA: 0x00135AD3 File Offset: 0x00133CD3
		public Queue()
			: this(32, 2f)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Queue" /> class that is empty, has the specified initial capacity, and uses the default growth factor.</summary>
		/// <param name="capacity">The initial number of elements that the <see cref="T:System.Collections.Queue" /> can contain. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="capacity" /> is less than zero. </exception>
		// Token: 0x06005C16 RID: 23574 RVA: 0x00135AE2 File Offset: 0x00133CE2
		public Queue(int capacity)
			: this(capacity, 2f)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Queue" /> class that is empty, has the specified initial capacity, and uses the specified growth factor.</summary>
		/// <param name="capacity">The initial number of elements that the <see cref="T:System.Collections.Queue" /> can contain. </param>
		/// <param name="growFactor">The factor by which the capacity of the <see cref="T:System.Collections.Queue" /> is expanded. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="capacity" /> is less than zero.-or- <paramref name="growFactor" /> is less than 1.0 or greater than 10.0. </exception>
		// Token: 0x06005C17 RID: 23575 RVA: 0x00135AF0 File Offset: 0x00133CF0
		public Queue(int capacity, float growFactor)
		{
			if (capacity < 0)
			{
				throw new ArgumentOutOfRangeException("capacity", "Non-negative number required.");
			}
			if ((double)growFactor < 1.0 || (double)growFactor > 10.0)
			{
				throw new ArgumentOutOfRangeException("growFactor", SR.Format("Queue grow factor must be between {0} and {1}.", 1, 10));
			}
			this._array = new object[capacity];
			this._head = 0;
			this._tail = 0;
			this._size = 0;
			this._growFactor = (int)(growFactor * 100f);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Queue" /> class that contains elements copied from the specified collection, has the same initial capacity as the number of elements copied, and uses the default growth factor.</summary>
		/// <param name="col">The <see cref="T:System.Collections.ICollection" /> to copy elements from. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="col" /> is null. </exception>
		// Token: 0x06005C18 RID: 23576 RVA: 0x00135B84 File Offset: 0x00133D84
		public Queue(ICollection col)
			: this((col == null) ? 32 : col.Count)
		{
			if (col == null)
			{
				throw new ArgumentNullException("col");
			}
			foreach (object obj in col)
			{
				this.Enqueue(obj);
			}
		}

		/// <summary>Gets the number of elements contained in the <see cref="T:System.Collections.Queue" />.</summary>
		/// <returns>The number of elements contained in the <see cref="T:System.Collections.Queue" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000FF7 RID: 4087
		// (get) Token: 0x06005C19 RID: 23577 RVA: 0x00135BCF File Offset: 0x00133DCF
		public virtual int Count
		{
			get
			{
				return this._size;
			}
		}

		/// <summary>Creates a shallow copy of the <see cref="T:System.Collections.Queue" />.</summary>
		/// <returns>A shallow copy of the <see cref="T:System.Collections.Queue" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06005C1A RID: 23578 RVA: 0x00135BD8 File Offset: 0x00133DD8
		public virtual object Clone()
		{
			Queue queue = new Queue(this._size);
			queue._size = this._size;
			int num = this._size;
			int num2 = ((this._array.Length - this._head < num) ? (this._array.Length - this._head) : num);
			Array.Copy(this._array, this._head, queue._array, 0, num2);
			num -= num2;
			if (num > 0)
			{
				Array.Copy(this._array, 0, queue._array, this._array.Length - this._head, num);
			}
			queue._version = this._version;
			return queue;
		}

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Collections.Queue" /> is synchronized (thread safe).</summary>
		/// <returns>true if access to the <see cref="T:System.Collections.Queue" /> is synchronized (thread safe); otherwise, false. The default is false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000FF8 RID: 4088
		// (get) Token: 0x06005C1B RID: 23579 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		public virtual bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.Queue" />.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.Queue" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000FF9 RID: 4089
		// (get) Token: 0x06005C1C RID: 23580 RVA: 0x00135C79 File Offset: 0x00133E79
		public virtual object SyncRoot
		{
			get
			{
				if (this._syncRoot == null)
				{
					Interlocked.CompareExchange(ref this._syncRoot, new object(), null);
				}
				return this._syncRoot;
			}
		}

		/// <summary>Removes all objects from the <see cref="T:System.Collections.Queue" />.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06005C1D RID: 23581 RVA: 0x00135C9C File Offset: 0x00133E9C
		public virtual void Clear()
		{
			if (this._size != 0)
			{
				if (this._head < this._tail)
				{
					Array.Clear(this._array, this._head, this._size);
				}
				else
				{
					Array.Clear(this._array, this._head, this._array.Length - this._head);
					Array.Clear(this._array, 0, this._tail);
				}
				this._size = 0;
			}
			this._head = 0;
			this._tail = 0;
			this._version++;
		}

		/// <summary>Copies the <see cref="T:System.Collections.Queue" /> elements to an existing one-dimensional <see cref="T:System.Array" />, starting at the specified array index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.Queue" />. The <see cref="T:System.Array" /> must have zero-based indexing. </param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.-or- The number of elements in the source <see cref="T:System.Collections.Queue" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />. </exception>
		/// <exception cref="T:System.ArrayTypeMismatchException">The type of the source <see cref="T:System.Collections.Queue" /> cannot be cast automatically to the type of the destination <paramref name="array" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06005C1E RID: 23582 RVA: 0x00135D30 File Offset: 0x00133F30
		public virtual void CopyTo(Array array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (array.Rank != 1)
			{
				throw new ArgumentException("Only single dimensional arrays are supported for the requested action.", "array");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			if (array.Length - index < this._size)
			{
				throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
			}
			int num = this._size;
			if (num == 0)
			{
				return;
			}
			int num2 = ((this._array.Length - this._head < num) ? (this._array.Length - this._head) : num);
			Array.Copy(this._array, this._head, array, index, num2);
			num -= num2;
			if (num > 0)
			{
				Array.Copy(this._array, 0, array, index + this._array.Length - this._head, num);
			}
		}

		/// <summary>Adds an object to the end of the <see cref="T:System.Collections.Queue" />.</summary>
		/// <param name="obj">The object to add to the <see cref="T:System.Collections.Queue" />. The value can be null. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06005C1F RID: 23583 RVA: 0x00135E00 File Offset: 0x00134000
		public virtual void Enqueue(object obj)
		{
			if (this._size == this._array.Length)
			{
				int num = (int)((long)this._array.Length * (long)this._growFactor / 100L);
				if (num < this._array.Length + 4)
				{
					num = this._array.Length + 4;
				}
				this.SetCapacity(num);
			}
			this._array[this._tail] = obj;
			this._tail = (this._tail + 1) % this._array.Length;
			this._size++;
			this._version++;
		}

		/// <summary>Returns an enumerator that iterates through the <see cref="T:System.Collections.Queue" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the <see cref="T:System.Collections.Queue" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06005C20 RID: 23584 RVA: 0x00135E94 File Offset: 0x00134094
		public virtual IEnumerator GetEnumerator()
		{
			return new Queue.QueueEnumerator(this);
		}

		/// <summary>Removes and returns the object at the beginning of the <see cref="T:System.Collections.Queue" />.</summary>
		/// <returns>The object that is removed from the beginning of the <see cref="T:System.Collections.Queue" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Collections.Queue" /> is empty. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06005C21 RID: 23585 RVA: 0x00135E9C File Offset: 0x0013409C
		public virtual object Dequeue()
		{
			if (this.Count == 0)
			{
				throw new InvalidOperationException("Queue empty.");
			}
			object obj = this._array[this._head];
			this._array[this._head] = null;
			this._head = (this._head + 1) % this._array.Length;
			this._size--;
			this._version++;
			return obj;
		}

		/// <summary>Returns the object at the beginning of the <see cref="T:System.Collections.Queue" /> without removing it.</summary>
		/// <returns>The object at the beginning of the <see cref="T:System.Collections.Queue" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Collections.Queue" /> is empty. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06005C22 RID: 23586 RVA: 0x00135F0A File Offset: 0x0013410A
		public virtual object Peek()
		{
			if (this.Count == 0)
			{
				throw new InvalidOperationException("Queue empty.");
			}
			return this._array[this._head];
		}

		/// <summary>Returns a <see cref="T:System.Collections.Queue" /> wrapper that is synchronized (thread safe).</summary>
		/// <returns>A <see cref="T:System.Collections.Queue" /> wrapper that is synchronized (thread safe).</returns>
		/// <param name="queue">The <see cref="T:System.Collections.Queue" /> to synchronize. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="queue" /> is null. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06005C23 RID: 23587 RVA: 0x00135F2C File Offset: 0x0013412C
		public static Queue Synchronized(Queue queue)
		{
			if (queue == null)
			{
				throw new ArgumentNullException("queue");
			}
			return new Queue.SynchronizedQueue(queue);
		}

		/// <summary>Determines whether an element is in the <see cref="T:System.Collections.Queue" />.</summary>
		/// <returns>true if <paramref name="obj" /> is found in the <see cref="T:System.Collections.Queue" />; otherwise, false.</returns>
		/// <param name="obj">The <see cref="T:System.Object" /> to locate in the <see cref="T:System.Collections.Queue" />. The value can be null. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06005C24 RID: 23588 RVA: 0x00135F44 File Offset: 0x00134144
		public virtual bool Contains(object obj)
		{
			int num = this._head;
			int size = this._size;
			while (size-- > 0)
			{
				if (obj == null)
				{
					if (this._array[num] == null)
					{
						return true;
					}
				}
				else if (this._array[num] != null && this._array[num].Equals(obj))
				{
					return true;
				}
				num = (num + 1) % this._array.Length;
			}
			return false;
		}

		// Token: 0x06005C25 RID: 23589 RVA: 0x00135FA2 File Offset: 0x001341A2
		internal object GetElement(int i)
		{
			return this._array[(this._head + i) % this._array.Length];
		}

		/// <summary>Copies the <see cref="T:System.Collections.Queue" /> elements to a new array.</summary>
		/// <returns>A new array containing elements copied from the <see cref="T:System.Collections.Queue" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06005C26 RID: 23590 RVA: 0x00135FBC File Offset: 0x001341BC
		public virtual object[] ToArray()
		{
			if (this._size == 0)
			{
				return Array.Empty<object>();
			}
			object[] array = new object[this._size];
			if (this._head < this._tail)
			{
				Array.Copy(this._array, this._head, array, 0, this._size);
			}
			else
			{
				Array.Copy(this._array, this._head, array, 0, this._array.Length - this._head);
				Array.Copy(this._array, 0, array, this._array.Length - this._head, this._tail);
			}
			return array;
		}

		// Token: 0x06005C27 RID: 23591 RVA: 0x00136054 File Offset: 0x00134254
		private void SetCapacity(int capacity)
		{
			object[] array = new object[capacity];
			if (this._size > 0)
			{
				if (this._head < this._tail)
				{
					Array.Copy(this._array, this._head, array, 0, this._size);
				}
				else
				{
					Array.Copy(this._array, this._head, array, 0, this._array.Length - this._head);
					Array.Copy(this._array, 0, array, this._array.Length - this._head, this._tail);
				}
			}
			this._array = array;
			this._head = 0;
			this._tail = ((this._size == capacity) ? 0 : this._size);
			this._version++;
		}

		/// <summary>Sets the capacity to the actual number of elements in the <see cref="T:System.Collections.Queue" />.</summary>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Queue" /> is read-only.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06005C28 RID: 23592 RVA: 0x00136112 File Offset: 0x00134312
		public virtual void TrimToSize()
		{
			this.SetCapacity(this._size);
		}

		// Token: 0x0400388A RID: 14474
		private object[] _array;

		// Token: 0x0400388B RID: 14475
		private int _head;

		// Token: 0x0400388C RID: 14476
		private int _tail;

		// Token: 0x0400388D RID: 14477
		private int _size;

		// Token: 0x0400388E RID: 14478
		private int _growFactor;

		// Token: 0x0400388F RID: 14479
		private int _version;

		// Token: 0x04003890 RID: 14480
		[NonSerialized]
		private object _syncRoot;

		// Token: 0x04003891 RID: 14481
		private const int _MinimumGrow = 4;

		// Token: 0x04003892 RID: 14482
		private const int _ShrinkThreshold = 32;

		// Token: 0x02000A28 RID: 2600
		[Serializable]
		private class SynchronizedQueue : Queue
		{
			// Token: 0x06005C29 RID: 23593 RVA: 0x00136120 File Offset: 0x00134320
			internal SynchronizedQueue(Queue q)
			{
				this._q = q;
				this._root = this._q.SyncRoot;
			}

			// Token: 0x17000FFA RID: 4090
			// (get) Token: 0x06005C2A RID: 23594 RVA: 0x000040F7 File Offset: 0x000022F7
			public override bool IsSynchronized
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17000FFB RID: 4091
			// (get) Token: 0x06005C2B RID: 23595 RVA: 0x00136140 File Offset: 0x00134340
			public override object SyncRoot
			{
				get
				{
					return this._root;
				}
			}

			// Token: 0x17000FFC RID: 4092
			// (get) Token: 0x06005C2C RID: 23596 RVA: 0x00136148 File Offset: 0x00134348
			public override int Count
			{
				get
				{
					object root = this._root;
					int count;
					lock (root)
					{
						count = this._q.Count;
					}
					return count;
				}
			}

			// Token: 0x06005C2D RID: 23597 RVA: 0x00136190 File Offset: 0x00134390
			public override void Clear()
			{
				object root = this._root;
				lock (root)
				{
					this._q.Clear();
				}
			}

			// Token: 0x06005C2E RID: 23598 RVA: 0x001361D8 File Offset: 0x001343D8
			public override object Clone()
			{
				object root = this._root;
				object obj;
				lock (root)
				{
					obj = new Queue.SynchronizedQueue((Queue)this._q.Clone());
				}
				return obj;
			}

			// Token: 0x06005C2F RID: 23599 RVA: 0x0013622C File Offset: 0x0013442C
			public override bool Contains(object obj)
			{
				object root = this._root;
				bool flag2;
				lock (root)
				{
					flag2 = this._q.Contains(obj);
				}
				return flag2;
			}

			// Token: 0x06005C30 RID: 23600 RVA: 0x00136274 File Offset: 0x00134474
			public override void CopyTo(Array array, int arrayIndex)
			{
				object root = this._root;
				lock (root)
				{
					this._q.CopyTo(array, arrayIndex);
				}
			}

			// Token: 0x06005C31 RID: 23601 RVA: 0x001362BC File Offset: 0x001344BC
			public override void Enqueue(object value)
			{
				object root = this._root;
				lock (root)
				{
					this._q.Enqueue(value);
				}
			}

			// Token: 0x06005C32 RID: 23602 RVA: 0x00136304 File Offset: 0x00134504
			public override object Dequeue()
			{
				object root = this._root;
				object obj;
				lock (root)
				{
					obj = this._q.Dequeue();
				}
				return obj;
			}

			// Token: 0x06005C33 RID: 23603 RVA: 0x0013634C File Offset: 0x0013454C
			public override IEnumerator GetEnumerator()
			{
				object root = this._root;
				IEnumerator enumerator;
				lock (root)
				{
					enumerator = this._q.GetEnumerator();
				}
				return enumerator;
			}

			// Token: 0x06005C34 RID: 23604 RVA: 0x00136394 File Offset: 0x00134594
			public override object Peek()
			{
				object root = this._root;
				object obj;
				lock (root)
				{
					obj = this._q.Peek();
				}
				return obj;
			}

			// Token: 0x06005C35 RID: 23605 RVA: 0x001363DC File Offset: 0x001345DC
			public override object[] ToArray()
			{
				object root = this._root;
				object[] array;
				lock (root)
				{
					array = this._q.ToArray();
				}
				return array;
			}

			// Token: 0x06005C36 RID: 23606 RVA: 0x00136424 File Offset: 0x00134624
			public override void TrimToSize()
			{
				object root = this._root;
				lock (root)
				{
					this._q.TrimToSize();
				}
			}

			// Token: 0x04003893 RID: 14483
			private Queue _q;

			// Token: 0x04003894 RID: 14484
			private object _root;
		}

		// Token: 0x02000A29 RID: 2601
		[Serializable]
		private class QueueEnumerator : IEnumerator, ICloneable
		{
			// Token: 0x06005C37 RID: 23607 RVA: 0x0013646C File Offset: 0x0013466C
			internal QueueEnumerator(Queue q)
			{
				this._q = q;
				this._version = this._q._version;
				this._index = 0;
				this._currentElement = this._q._array;
				if (this._q._size == 0)
				{
					this._index = -1;
				}
			}

			// Token: 0x06005C38 RID: 23608 RVA: 0x000231D1 File Offset: 0x000213D1
			public object Clone()
			{
				return base.MemberwiseClone();
			}

			// Token: 0x06005C39 RID: 23609 RVA: 0x001364C4 File Offset: 0x001346C4
			public virtual bool MoveNext()
			{
				if (this._version != this._q._version)
				{
					throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
				}
				if (this._index < 0)
				{
					this._currentElement = this._q._array;
					return false;
				}
				this._currentElement = this._q.GetElement(this._index);
				this._index++;
				if (this._index == this._q._size)
				{
					this._index = -1;
				}
				return true;
			}

			// Token: 0x17000FFD RID: 4093
			// (get) Token: 0x06005C3A RID: 23610 RVA: 0x0013654B File Offset: 0x0013474B
			public virtual object Current
			{
				get
				{
					if (this._currentElement != this._q._array)
					{
						return this._currentElement;
					}
					if (this._index == 0)
					{
						throw new InvalidOperationException("Enumeration has not started. Call MoveNext.");
					}
					throw new InvalidOperationException("Enumeration already finished.");
				}
			}

			// Token: 0x06005C3B RID: 23611 RVA: 0x00136584 File Offset: 0x00134784
			public virtual void Reset()
			{
				if (this._version != this._q._version)
				{
					throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
				}
				if (this._q._size == 0)
				{
					this._index = -1;
				}
				else
				{
					this._index = 0;
				}
				this._currentElement = this._q._array;
			}

			// Token: 0x04003895 RID: 14485
			private Queue _q;

			// Token: 0x04003896 RID: 14486
			private int _index;

			// Token: 0x04003897 RID: 14487
			private int _version;

			// Token: 0x04003898 RID: 14488
			private object _currentElement;
		}

		// Token: 0x02000A2A RID: 2602
		internal class QueueDebugView
		{
			// Token: 0x06005C3C RID: 23612 RVA: 0x001365DD File Offset: 0x001347DD
			public QueueDebugView(Queue queue)
			{
				if (queue == null)
				{
					throw new ArgumentNullException("queue");
				}
				this._queue = queue;
			}

			// Token: 0x17000FFE RID: 4094
			// (get) Token: 0x06005C3D RID: 23613 RVA: 0x001365FA File Offset: 0x001347FA
			[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
			public object[] Items
			{
				get
				{
					return this._queue.ToArray();
				}
			}

			// Token: 0x04003899 RID: 14489
			private Queue _queue;
		}
	}
}
