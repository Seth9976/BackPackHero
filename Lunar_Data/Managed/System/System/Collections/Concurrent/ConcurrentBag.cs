using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace System.Collections.Concurrent
{
	/// <summary>Represents a thread-safe, unordered collection of objects.</summary>
	/// <typeparam name="T">The type of the elements to be stored in the collection.</typeparam>
	// Token: 0x020007B2 RID: 1970
	[DebuggerDisplay("Count = {Count}")]
	[DebuggerTypeProxy(typeof(IProducerConsumerCollectionDebugView<>))]
	[Serializable]
	public class ConcurrentBag<T> : IProducerConsumerCollection<T>, IEnumerable<T>, IEnumerable, ICollection, IReadOnlyCollection<T>
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Concurrent.ConcurrentBag`1" /> class.</summary>
		// Token: 0x06003E61 RID: 15969 RVA: 0x000DB53C File Offset: 0x000D973C
		public ConcurrentBag()
		{
			this._locals = new ThreadLocal<ConcurrentBag<T>.WorkStealingQueue>();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Concurrent.ConcurrentBag`1" /> class that contains elements copied from the specified collection.</summary>
		/// <param name="collection">The collection whose elements are copied to the new <see cref="T:System.Collections.Concurrent.ConcurrentBag`1" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="collection" /> is a null reference (Nothing in Visual Basic).</exception>
		// Token: 0x06003E62 RID: 15970 RVA: 0x000DB550 File Offset: 0x000D9750
		public ConcurrentBag(IEnumerable<T> collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection", "The collection argument is null.");
			}
			this._locals = new ThreadLocal<ConcurrentBag<T>.WorkStealingQueue>();
			ConcurrentBag<T>.WorkStealingQueue currentThreadWorkStealingQueue = this.GetCurrentThreadWorkStealingQueue(true);
			foreach (T t in collection)
			{
				currentThreadWorkStealingQueue.LocalPush(t, ref this._emptyToNonEmptyListTransitionCount);
			}
		}

		/// <summary>Adds an object to the <see cref="T:System.Collections.Concurrent.ConcurrentBag`1" />.</summary>
		/// <param name="item">The object to be added to the <see cref="T:System.Collections.Concurrent.ConcurrentBag`1" />. The value can be a null reference (Nothing in Visual Basic) for reference types.</param>
		// Token: 0x06003E63 RID: 15971 RVA: 0x000DB5CC File Offset: 0x000D97CC
		public void Add(T item)
		{
			this.GetCurrentThreadWorkStealingQueue(true).LocalPush(item, ref this._emptyToNonEmptyListTransitionCount);
		}

		/// <summary>Attempts to add an object to the <see cref="T:System.Collections.Concurrent.ConcurrentBag`1" />.</summary>
		/// <returns>Always returns true</returns>
		/// <param name="item">The object to be added to the <see cref="T:System.Collections.Concurrent.ConcurrentBag`1" />. The value can be a null reference (Nothing in Visual Basic) for reference types.</param>
		// Token: 0x06003E64 RID: 15972 RVA: 0x000DB5E1 File Offset: 0x000D97E1
		bool IProducerConsumerCollection<T>.TryAdd(T item)
		{
			this.Add(item);
			return true;
		}

		/// <summary>Attempts to remove and return an object from the <see cref="T:System.Collections.Concurrent.ConcurrentBag`1" />.</summary>
		/// <returns>true if an object was removed successfully; otherwise, false.</returns>
		/// <param name="result">When this method returns, <paramref name="result" /> contains the object removed from the <see cref="T:System.Collections.Concurrent.ConcurrentBag`1" /> or the default value of <paramref name="T" /> if the bag is empty.</param>
		// Token: 0x06003E65 RID: 15973 RVA: 0x000DB5EC File Offset: 0x000D97EC
		public bool TryTake(out T result)
		{
			ConcurrentBag<T>.WorkStealingQueue currentThreadWorkStealingQueue = this.GetCurrentThreadWorkStealingQueue(false);
			return (currentThreadWorkStealingQueue != null && currentThreadWorkStealingQueue.TryLocalPop(out result)) || this.TrySteal(out result, true);
		}

		/// <summary>Attempts to return an object from the <see cref="T:System.Collections.Concurrent.ConcurrentBag`1" /> without removing it.</summary>
		/// <returns>true if and object was returned successfully; otherwise, false.</returns>
		/// <param name="result">When this method returns, <paramref name="result" /> contains an object from the <see cref="T:System.Collections.Concurrent.ConcurrentBag`1" /> or the default value of <paramref name="T" /> if the operation failed.</param>
		// Token: 0x06003E66 RID: 15974 RVA: 0x000DB618 File Offset: 0x000D9818
		public bool TryPeek(out T result)
		{
			ConcurrentBag<T>.WorkStealingQueue currentThreadWorkStealingQueue = this.GetCurrentThreadWorkStealingQueue(false);
			return (currentThreadWorkStealingQueue != null && currentThreadWorkStealingQueue.TryLocalPeek(out result)) || this.TrySteal(out result, false);
		}

		// Token: 0x06003E67 RID: 15975 RVA: 0x000DB643 File Offset: 0x000D9843
		private ConcurrentBag<T>.WorkStealingQueue GetCurrentThreadWorkStealingQueue(bool forceCreate)
		{
			ConcurrentBag<T>.WorkStealingQueue workStealingQueue;
			if ((workStealingQueue = this._locals.Value) == null)
			{
				if (!forceCreate)
				{
					return null;
				}
				workStealingQueue = this.CreateWorkStealingQueueForCurrentThread();
			}
			return workStealingQueue;
		}

		// Token: 0x06003E68 RID: 15976 RVA: 0x000DB660 File Offset: 0x000D9860
		private ConcurrentBag<T>.WorkStealingQueue CreateWorkStealingQueueForCurrentThread()
		{
			object globalQueuesLock = this.GlobalQueuesLock;
			ConcurrentBag<T>.WorkStealingQueue workStealingQueue2;
			lock (globalQueuesLock)
			{
				ConcurrentBag<T>.WorkStealingQueue workStealingQueues = this._workStealingQueues;
				ConcurrentBag<T>.WorkStealingQueue workStealingQueue = ((workStealingQueues != null) ? this.GetUnownedWorkStealingQueue() : null);
				if (workStealingQueue == null)
				{
					workStealingQueue = (this._workStealingQueues = new ConcurrentBag<T>.WorkStealingQueue(workStealingQueues));
				}
				this._locals.Value = workStealingQueue;
				workStealingQueue2 = workStealingQueue;
			}
			return workStealingQueue2;
		}

		// Token: 0x06003E69 RID: 15977 RVA: 0x000DB6D4 File Offset: 0x000D98D4
		private ConcurrentBag<T>.WorkStealingQueue GetUnownedWorkStealingQueue()
		{
			int currentManagedThreadId = Environment.CurrentManagedThreadId;
			for (ConcurrentBag<T>.WorkStealingQueue workStealingQueue = this._workStealingQueues; workStealingQueue != null; workStealingQueue = workStealingQueue._nextQueue)
			{
				if (workStealingQueue._ownerThreadId == currentManagedThreadId)
				{
					return workStealingQueue;
				}
			}
			return null;
		}

		// Token: 0x06003E6A RID: 15978 RVA: 0x000DB708 File Offset: 0x000D9908
		private bool TrySteal(out T result, bool take)
		{
			if (take)
			{
				CDSCollectionETWBCLProvider.Log.ConcurrentBag_TryTakeSteals();
			}
			else
			{
				CDSCollectionETWBCLProvider.Log.ConcurrentBag_TryPeekSteals();
			}
			for (;;)
			{
				long num = Interlocked.Read(ref this._emptyToNonEmptyListTransitionCount);
				ConcurrentBag<T>.WorkStealingQueue currentThreadWorkStealingQueue = this.GetCurrentThreadWorkStealingQueue(false);
				if ((currentThreadWorkStealingQueue == null) ? this.TryStealFromTo(this._workStealingQueues, null, out result, take) : (this.TryStealFromTo(currentThreadWorkStealingQueue._nextQueue, null, out result, take) || this.TryStealFromTo(this._workStealingQueues, currentThreadWorkStealingQueue, out result, take)))
				{
					break;
				}
				if (Interlocked.Read(ref this._emptyToNonEmptyListTransitionCount) == num)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06003E6B RID: 15979 RVA: 0x000DB790 File Offset: 0x000D9990
		private bool TryStealFromTo(ConcurrentBag<T>.WorkStealingQueue startInclusive, ConcurrentBag<T>.WorkStealingQueue endExclusive, out T result, bool take)
		{
			for (ConcurrentBag<T>.WorkStealingQueue workStealingQueue = startInclusive; workStealingQueue != endExclusive; workStealingQueue = workStealingQueue._nextQueue)
			{
				if (workStealingQueue.TrySteal(out result, take))
				{
					return true;
				}
			}
			result = default(T);
			return false;
		}

		/// <summary>Copies the <see cref="T:System.Collections.Concurrent.ConcurrentBag`1" /> elements to an existing one-dimensional <see cref="T:System.Array" />, starting at the specified array index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from the <see cref="T:System.Collections.Concurrent.ConcurrentBag`1" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="index" /> is equal to or greater than the length of the <paramref name="array" /> -or- the number of elements in the source <see cref="T:System.Collections.Concurrent.ConcurrentBag`1" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />.</exception>
		// Token: 0x06003E6C RID: 15980 RVA: 0x000DB7C4 File Offset: 0x000D99C4
		public void CopyTo(T[] array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array", "The array argument is null.");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", "The index argument must be greater than or equal zero.");
			}
			if (this._workStealingQueues == null)
			{
				return;
			}
			bool flag = false;
			try
			{
				this.FreezeBag(ref flag);
				int dangerousCount = this.DangerousCount;
				if (index > array.Length - dangerousCount)
				{
					throw new ArgumentException("The number of elements in the collection is greater than the available space from index to the end of the destination array.", "index");
				}
				try
				{
					this.CopyFromEachQueueToArray(array, index);
				}
				catch (ArrayTypeMismatchException ex)
				{
					throw new InvalidCastException(ex.Message, ex);
				}
			}
			finally
			{
				this.UnfreezeBag(flag);
			}
		}

		// Token: 0x06003E6D RID: 15981 RVA: 0x000DB86C File Offset: 0x000D9A6C
		private int CopyFromEachQueueToArray(T[] array, int index)
		{
			int num = index;
			for (ConcurrentBag<T>.WorkStealingQueue workStealingQueue = this._workStealingQueues; workStealingQueue != null; workStealingQueue = workStealingQueue._nextQueue)
			{
				num += workStealingQueue.DangerousCopyTo(array, num);
			}
			return num - index;
		}

		/// <summary>Copies the elements of the <see cref="T:System.Collections.ICollection" /> to an <see cref="T:System.Array" />, starting at a particular <see cref="T:System.Array" /> index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from the <see cref="T:System.Collections.Concurrent.ConcurrentBag`1" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional. -or- <paramref name="array" /> does not have zero-based indexing. -or- <paramref name="index" /> is equal to or greater than the length of the <paramref name="array" /> -or- The number of elements in the source <see cref="T:System.Collections.ICollection" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />. -or- The type of the source <see cref="T:System.Collections.ICollection" /> cannot be cast automatically to the type of the destination <paramref name="array" />.</exception>
		// Token: 0x06003E6E RID: 15982 RVA: 0x000DB8A0 File Offset: 0x000D9AA0
		void ICollection.CopyTo(Array array, int index)
		{
			T[] array2 = array as T[];
			if (array2 != null)
			{
				this.CopyTo(array2, index);
				return;
			}
			if (array == null)
			{
				throw new ArgumentNullException("array", "The array argument is null.");
			}
			this.ToArray().CopyTo(array, index);
		}

		/// <summary>Copies the <see cref="T:System.Collections.Concurrent.ConcurrentBag`1" /> elements to a new array.</summary>
		/// <returns>A new array containing a snapshot of elements copied from the <see cref="T:System.Collections.Concurrent.ConcurrentBag`1" />.</returns>
		// Token: 0x06003E6F RID: 15983 RVA: 0x000DB8E0 File Offset: 0x000D9AE0
		public T[] ToArray()
		{
			if (this._workStealingQueues != null)
			{
				bool flag = false;
				try
				{
					this.FreezeBag(ref flag);
					int dangerousCount = this.DangerousCount;
					if (dangerousCount > 0)
					{
						T[] array = new T[dangerousCount];
						this.CopyFromEachQueueToArray(array, 0);
						return array;
					}
				}
				finally
				{
					this.UnfreezeBag(flag);
				}
			}
			return Array.Empty<T>();
		}

		// Token: 0x06003E70 RID: 15984 RVA: 0x000DB944 File Offset: 0x000D9B44
		public void Clear()
		{
			if (this._workStealingQueues == null)
			{
				return;
			}
			ConcurrentBag<T>.WorkStealingQueue currentThreadWorkStealingQueue = this.GetCurrentThreadWorkStealingQueue(false);
			if (currentThreadWorkStealingQueue != null)
			{
				currentThreadWorkStealingQueue.LocalClear();
				if (currentThreadWorkStealingQueue._nextQueue == null && currentThreadWorkStealingQueue == this._workStealingQueues)
				{
					return;
				}
			}
			bool flag = false;
			try
			{
				this.FreezeBag(ref flag);
				for (ConcurrentBag<T>.WorkStealingQueue workStealingQueue = this._workStealingQueues; workStealingQueue != null; workStealingQueue = workStealingQueue._nextQueue)
				{
					T t;
					while (workStealingQueue.TrySteal(out t, true))
					{
					}
				}
			}
			finally
			{
				this.UnfreezeBag(flag);
			}
		}

		/// <summary>Returns an enumerator that iterates through the <see cref="T:System.Collections.Concurrent.ConcurrentBag`1" />.</summary>
		/// <returns>An enumerator for the contents of the <see cref="T:System.Collections.Concurrent.ConcurrentBag`1" />.</returns>
		// Token: 0x06003E71 RID: 15985 RVA: 0x000DB9C8 File Offset: 0x000D9BC8
		public IEnumerator<T> GetEnumerator()
		{
			return new ConcurrentBag<T>.Enumerator(this.ToArray());
		}

		/// <summary>Returns an enumerator that iterates through the <see cref="T:System.Collections.Concurrent.ConcurrentBag`1" />.</summary>
		/// <returns>An enumerator for the contents of the <see cref="T:System.Collections.Concurrent.ConcurrentBag`1" />.</returns>
		// Token: 0x06003E72 RID: 15986 RVA: 0x000DB9D5 File Offset: 0x000D9BD5
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>Gets the number of elements contained in the <see cref="T:System.Collections.Concurrent.ConcurrentBag`1" />.</summary>
		/// <returns>The number of elements contained in the <see cref="T:System.Collections.Concurrent.ConcurrentBag`1" />.</returns>
		// Token: 0x17000E1E RID: 3614
		// (get) Token: 0x06003E73 RID: 15987 RVA: 0x000DB9E0 File Offset: 0x000D9BE0
		public int Count
		{
			get
			{
				if (this._workStealingQueues == null)
				{
					return 0;
				}
				bool flag = false;
				int dangerousCount;
				try
				{
					this.FreezeBag(ref flag);
					dangerousCount = this.DangerousCount;
				}
				finally
				{
					this.UnfreezeBag(flag);
				}
				return dangerousCount;
			}
		}

		// Token: 0x17000E1F RID: 3615
		// (get) Token: 0x06003E74 RID: 15988 RVA: 0x000DBA28 File Offset: 0x000D9C28
		private int DangerousCount
		{
			get
			{
				int num = 0;
				checked
				{
					for (ConcurrentBag<T>.WorkStealingQueue workStealingQueue = this._workStealingQueues; workStealingQueue != null; workStealingQueue = workStealingQueue._nextQueue)
					{
						num += workStealingQueue.DangerousCount;
					}
					return num;
				}
			}
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Collections.Concurrent.ConcurrentBag`1" /> is empty.</summary>
		/// <returns>true if the <see cref="T:System.Collections.Concurrent.ConcurrentBag`1" /> is empty; otherwise, false.</returns>
		// Token: 0x17000E20 RID: 3616
		// (get) Token: 0x06003E75 RID: 15989 RVA: 0x000DBA58 File Offset: 0x000D9C58
		public bool IsEmpty
		{
			get
			{
				ConcurrentBag<T>.WorkStealingQueue currentThreadWorkStealingQueue = this.GetCurrentThreadWorkStealingQueue(false);
				if (currentThreadWorkStealingQueue != null)
				{
					if (!currentThreadWorkStealingQueue.IsEmpty)
					{
						return false;
					}
					if (currentThreadWorkStealingQueue._nextQueue == null && currentThreadWorkStealingQueue == this._workStealingQueues)
					{
						return true;
					}
				}
				bool flag = false;
				try
				{
					this.FreezeBag(ref flag);
					for (ConcurrentBag<T>.WorkStealingQueue workStealingQueue = this._workStealingQueues; workStealingQueue != null; workStealingQueue = workStealingQueue._nextQueue)
					{
						if (!workStealingQueue.IsEmpty)
						{
							return false;
						}
					}
				}
				finally
				{
					this.UnfreezeBag(flag);
				}
				return true;
			}
		}

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Collections.ICollection" /> is synchronized with the SyncRoot.</summary>
		/// <returns>true if access to the <see cref="T:System.Collections.ICollection" /> is synchronized with the SyncRoot; otherwise, false. For <see cref="T:System.Collections.Concurrent.ConcurrentBag`1" />, this property always returns false.</returns>
		// Token: 0x17000E21 RID: 3617
		// (get) Token: 0x06003E76 RID: 15990 RVA: 0x00003062 File Offset: 0x00001262
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />. This property is not supported.</summary>
		/// <returns>Returns null  (Nothing in Visual Basic).</returns>
		/// <exception cref="T:System.NotSupportedException">The SyncRoot property is not supported.</exception>
		// Token: 0x17000E22 RID: 3618
		// (get) Token: 0x06003E77 RID: 15991 RVA: 0x000DA5B3 File Offset: 0x000D87B3
		object ICollection.SyncRoot
		{
			get
			{
				throw new NotSupportedException("The SyncRoot property may not be used for the synchronization of concurrent collections.");
			}
		}

		// Token: 0x17000E23 RID: 3619
		// (get) Token: 0x06003E78 RID: 15992 RVA: 0x000DBAD8 File Offset: 0x000D9CD8
		private object GlobalQueuesLock
		{
			get
			{
				return this._locals;
			}
		}

		// Token: 0x06003E79 RID: 15993 RVA: 0x000DBAE0 File Offset: 0x000D9CE0
		private void FreezeBag(ref bool lockTaken)
		{
			Monitor.Enter(this.GlobalQueuesLock, ref lockTaken);
			ConcurrentBag<T>.WorkStealingQueue workStealingQueues = this._workStealingQueues;
			for (ConcurrentBag<T>.WorkStealingQueue workStealingQueue = workStealingQueues; workStealingQueue != null; workStealingQueue = workStealingQueue._nextQueue)
			{
				Monitor.Enter(workStealingQueue, ref workStealingQueue._frozen);
			}
			Interlocked.MemoryBarrier();
			for (ConcurrentBag<T>.WorkStealingQueue workStealingQueue2 = workStealingQueues; workStealingQueue2 != null; workStealingQueue2 = workStealingQueue2._nextQueue)
			{
				if (workStealingQueue2._currentOp != 0)
				{
					SpinWait spinWait = default(SpinWait);
					do
					{
						spinWait.SpinOnce();
					}
					while (workStealingQueue2._currentOp != 0);
				}
			}
		}

		// Token: 0x06003E7A RID: 15994 RVA: 0x000DBB54 File Offset: 0x000D9D54
		private void UnfreezeBag(bool lockTaken)
		{
			if (lockTaken)
			{
				for (ConcurrentBag<T>.WorkStealingQueue workStealingQueue = this._workStealingQueues; workStealingQueue != null; workStealingQueue = workStealingQueue._nextQueue)
				{
					if (workStealingQueue._frozen)
					{
						workStealingQueue._frozen = false;
						Monitor.Exit(workStealingQueue);
					}
				}
				Monitor.Exit(this.GlobalQueuesLock);
			}
		}

		// Token: 0x04002636 RID: 9782
		private readonly ThreadLocal<ConcurrentBag<T>.WorkStealingQueue> _locals;

		// Token: 0x04002637 RID: 9783
		private volatile ConcurrentBag<T>.WorkStealingQueue _workStealingQueues;

		// Token: 0x04002638 RID: 9784
		private long _emptyToNonEmptyListTransitionCount;

		// Token: 0x020007B3 RID: 1971
		private sealed class WorkStealingQueue
		{
			// Token: 0x06003E7B RID: 15995 RVA: 0x000DBB99 File Offset: 0x000D9D99
			internal WorkStealingQueue(ConcurrentBag<T>.WorkStealingQueue nextQueue)
			{
				this._ownerThreadId = Environment.CurrentManagedThreadId;
				this._nextQueue = nextQueue;
			}

			// Token: 0x17000E24 RID: 3620
			// (get) Token: 0x06003E7C RID: 15996 RVA: 0x000DBBCC File Offset: 0x000D9DCC
			internal bool IsEmpty
			{
				get
				{
					return this._headIndex >= this._tailIndex;
				}
			}

			// Token: 0x06003E7D RID: 15997 RVA: 0x000DBBE4 File Offset: 0x000D9DE4
			internal void LocalPush(T item, ref long emptyToNonEmptyListTransitionCount)
			{
				bool flag = false;
				try
				{
					Interlocked.Exchange(ref this._currentOp, 1);
					int num = this._tailIndex;
					if (num == 2147483647)
					{
						this._currentOp = 0;
						lock (this)
						{
							this._headIndex &= this._mask;
							num = (this._tailIndex = num & this._mask);
							Interlocked.Exchange(ref this._currentOp, 1);
						}
					}
					int num2 = this._headIndex;
					if (!this._frozen && ((num2 < num - 1) & (num < num2 + this._mask)))
					{
						this._array[num & this._mask] = item;
						this._tailIndex = num + 1;
					}
					else
					{
						this._currentOp = 0;
						Monitor.Enter(this, ref flag);
						num2 = this._headIndex;
						int num3 = num - num2;
						if (num3 >= this._mask)
						{
							T[] array = new T[this._array.Length << 1];
							int num4 = num2 & this._mask;
							if (num4 == 0)
							{
								Array.Copy(this._array, 0, array, 0, this._array.Length);
							}
							else
							{
								Array.Copy(this._array, num4, array, 0, this._array.Length - num4);
								Array.Copy(this._array, 0, array, this._array.Length - num4, num4);
							}
							this._array = array;
							this._headIndex = 0;
							num = (this._tailIndex = num3);
							this._mask = (this._mask << 1) | 1;
						}
						this._array[num & this._mask] = item;
						this._tailIndex = num + 1;
						if (num3 == 0)
						{
							Interlocked.Increment(ref emptyToNonEmptyListTransitionCount);
						}
						this._addTakeCount -= this._stealCount;
						this._stealCount = 0;
					}
					checked
					{
						this._addTakeCount++;
					}
				}
				finally
				{
					this._currentOp = 0;
					if (flag)
					{
						Monitor.Exit(this);
					}
				}
			}

			// Token: 0x06003E7E RID: 15998 RVA: 0x000DBE3C File Offset: 0x000DA03C
			internal void LocalClear()
			{
				lock (this)
				{
					if (this._headIndex < this._tailIndex)
					{
						this._headIndex = (this._tailIndex = 0);
						this._addTakeCount = (this._stealCount = 0);
						Array.Clear(this._array, 0, this._array.Length);
					}
				}
			}

			// Token: 0x06003E7F RID: 15999 RVA: 0x000DBEC0 File Offset: 0x000DA0C0
			internal bool TryLocalPop(out T result)
			{
				int num = this._tailIndex;
				if (this._headIndex >= num)
				{
					result = default(T);
					return false;
				}
				bool flag = false;
				bool flag2;
				try
				{
					this._currentOp = 2;
					Interlocked.Exchange(ref this._tailIndex, --num);
					if (!this._frozen && this._headIndex < num)
					{
						int num2 = num & this._mask;
						result = this._array[num2];
						this._array[num2] = default(T);
						this._addTakeCount--;
						flag2 = true;
					}
					else
					{
						this._currentOp = 0;
						Monitor.Enter(this, ref flag);
						if (this._headIndex <= num)
						{
							int num3 = num & this._mask;
							result = this._array[num3];
							this._array[num3] = default(T);
							this._addTakeCount--;
							flag2 = true;
						}
						else
						{
							this._tailIndex = num + 1;
							result = default(T);
							flag2 = false;
						}
					}
				}
				finally
				{
					this._currentOp = 0;
					if (flag)
					{
						Monitor.Exit(this);
					}
				}
				return flag2;
			}

			// Token: 0x06003E80 RID: 16000 RVA: 0x000DC00C File Offset: 0x000DA20C
			internal bool TryLocalPeek(out T result)
			{
				int tailIndex = this._tailIndex;
				if (this._headIndex < tailIndex)
				{
					lock (this)
					{
						if (this._headIndex < tailIndex)
						{
							result = this._array[(tailIndex - 1) & this._mask];
							return true;
						}
					}
				}
				result = default(T);
				return false;
			}

			// Token: 0x06003E81 RID: 16001 RVA: 0x000DC090 File Offset: 0x000DA290
			internal bool TrySteal(out T result, bool take)
			{
				lock (this)
				{
					int headIndex = this._headIndex;
					if (take)
					{
						if (headIndex < this._tailIndex - 1 && this._currentOp != 1)
						{
							SpinWait spinWait = default(SpinWait);
							do
							{
								spinWait.SpinOnce();
							}
							while (this._currentOp == 1);
						}
						Interlocked.Exchange(ref this._headIndex, headIndex + 1);
						if (headIndex < this._tailIndex)
						{
							int num = headIndex & this._mask;
							result = this._array[num];
							this._array[num] = default(T);
							this._stealCount++;
							return true;
						}
						this._headIndex = headIndex;
					}
					else if (headIndex < this._tailIndex)
					{
						result = this._array[headIndex & this._mask];
						return true;
					}
				}
				result = default(T);
				return false;
			}

			// Token: 0x06003E82 RID: 16002 RVA: 0x000DC1B0 File Offset: 0x000DA3B0
			internal int DangerousCopyTo(T[] array, int arrayIndex)
			{
				int headIndex = this._headIndex;
				int dangerousCount = this.DangerousCount;
				for (int i = arrayIndex + dangerousCount - 1; i >= arrayIndex; i--)
				{
					array[i] = this._array[headIndex++ & this._mask];
				}
				return dangerousCount;
			}

			// Token: 0x17000E25 RID: 3621
			// (get) Token: 0x06003E83 RID: 16003 RVA: 0x000DC200 File Offset: 0x000DA400
			internal int DangerousCount
			{
				get
				{
					return this._addTakeCount - this._stealCount;
				}
			}

			// Token: 0x04002639 RID: 9785
			private const int InitialSize = 32;

			// Token: 0x0400263A RID: 9786
			private const int StartIndex = 0;

			// Token: 0x0400263B RID: 9787
			private volatile int _headIndex;

			// Token: 0x0400263C RID: 9788
			private volatile int _tailIndex;

			// Token: 0x0400263D RID: 9789
			private volatile T[] _array = new T[32];

			// Token: 0x0400263E RID: 9790
			private volatile int _mask = 31;

			// Token: 0x0400263F RID: 9791
			private int _addTakeCount;

			// Token: 0x04002640 RID: 9792
			private int _stealCount;

			// Token: 0x04002641 RID: 9793
			internal volatile int _currentOp;

			// Token: 0x04002642 RID: 9794
			internal bool _frozen;

			// Token: 0x04002643 RID: 9795
			internal readonly ConcurrentBag<T>.WorkStealingQueue _nextQueue;

			// Token: 0x04002644 RID: 9796
			internal readonly int _ownerThreadId;
		}

		// Token: 0x020007B4 RID: 1972
		internal enum Operation
		{
			// Token: 0x04002646 RID: 9798
			None,
			// Token: 0x04002647 RID: 9799
			Add,
			// Token: 0x04002648 RID: 9800
			Take
		}

		// Token: 0x020007B5 RID: 1973
		[Serializable]
		private sealed class Enumerator : IEnumerator<T>, IDisposable, IEnumerator
		{
			// Token: 0x06003E84 RID: 16004 RVA: 0x000DC20F File Offset: 0x000DA40F
			public Enumerator(T[] array)
			{
				this._array = array;
			}

			// Token: 0x06003E85 RID: 16005 RVA: 0x000DC220 File Offset: 0x000DA420
			public bool MoveNext()
			{
				if (this._index < this._array.Length)
				{
					T[] array = this._array;
					int index = this._index;
					this._index = index + 1;
					this._current = array[index];
					return true;
				}
				this._index = this._array.Length + 1;
				return false;
			}

			// Token: 0x17000E26 RID: 3622
			// (get) Token: 0x06003E86 RID: 16006 RVA: 0x000DC272 File Offset: 0x000DA472
			public T Current
			{
				get
				{
					return this._current;
				}
			}

			// Token: 0x17000E27 RID: 3623
			// (get) Token: 0x06003E87 RID: 16007 RVA: 0x000DC27A File Offset: 0x000DA47A
			object IEnumerator.Current
			{
				get
				{
					if (this._index == 0 || this._index == this._array.Length + 1)
					{
						throw new InvalidOperationException("Enumeration has either not started or has already finished.");
					}
					return this.Current;
				}
			}

			// Token: 0x06003E88 RID: 16008 RVA: 0x000DC2AC File Offset: 0x000DA4AC
			public void Reset()
			{
				this._index = 0;
				this._current = default(T);
			}

			// Token: 0x06003E89 RID: 16009 RVA: 0x00003917 File Offset: 0x00001B17
			public void Dispose()
			{
			}

			// Token: 0x04002649 RID: 9801
			private readonly T[] _array;

			// Token: 0x0400264A RID: 9802
			private T _current;

			// Token: 0x0400264B RID: 9803
			private int _index;
		}
	}
}
