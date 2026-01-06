using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Unity.Services.Core.Scheduler.Internal
{
	// Token: 0x02000004 RID: 4
	internal class MinimumBinaryHeap<T> : MinimumBinaryHeap
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000B RID: 11 RVA: 0x00002410 File Offset: 0x00000610
		internal IReadOnlyList<T> HeapArray
		{
			get
			{
				return this.m_HeapArray;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000C RID: 12 RVA: 0x00002418 File Offset: 0x00000618
		// (set) Token: 0x0600000D RID: 13 RVA: 0x00002420 File Offset: 0x00000620
		public int Count { get; private set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000E RID: 14 RVA: 0x00002429 File Offset: 0x00000629
		public T Min
		{
			get
			{
				return this.m_HeapArray[0];
			}
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002437 File Offset: 0x00000637
		public MinimumBinaryHeap(int minimumCapacity = 10)
			: this(Comparer<T>.Default, minimumCapacity)
		{
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002445 File Offset: 0x00000645
		public MinimumBinaryHeap(IComparer<T> comparer, int minimumCapacity = 10)
			: this(null, comparer, minimumCapacity)
		{
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002450 File Offset: 0x00000650
		internal MinimumBinaryHeap(ICollection<T> collection, IComparer<T> comparer, int minimumCapacity = 10)
		{
			if (minimumCapacity <= 0)
			{
				throw new ArgumentException("capacity must be more than 0");
			}
			this.m_MinimumCapacity = minimumCapacity;
			this.m_Comparer = comparer;
			object @lock = this.m_Lock;
			lock (@lock)
			{
				this.Count = ((collection != null) ? collection.Count : 0);
				int num = Math.Max(this.Count, minimumCapacity);
				this.m_HeapArray = new T[num];
				if (collection != null)
				{
					this.Count = 0;
					foreach (T t in collection)
					{
						this.Insert(t);
					}
				}
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002528 File Offset: 0x00000728
		public void Insert(T item)
		{
			object @lock = this.m_Lock;
			lock (@lock)
			{
				this.IncreaseHeapCapacityWhenFull();
				int num = this.Count;
				this.m_HeapArray[this.Count] = item;
				int count = this.Count;
				this.Count = count + 1;
				while (num != 0 && this.m_Comparer.Compare(this.m_HeapArray[num], this.m_HeapArray[MinimumBinaryHeap<T>.GetParentIndex(num)]) < 0)
				{
					MinimumBinaryHeap<T>.Swap(ref this.m_HeapArray[num], ref this.m_HeapArray[MinimumBinaryHeap<T>.GetParentIndex(num)]);
					num = MinimumBinaryHeap<T>.GetParentIndex(num);
				}
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000025EC File Offset: 0x000007EC
		private void IncreaseHeapCapacityWhenFull()
		{
			if (this.Count != this.m_HeapArray.Length)
			{
				return;
			}
			T[] array = new T[(int)Math.Ceiling((double)((float)this.Count * 1.5f))];
			Array.Copy(this.m_HeapArray, array, this.Count);
			this.m_HeapArray = array;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002640 File Offset: 0x00000840
		public void Remove(T item)
		{
			object @lock = this.m_Lock;
			lock (@lock)
			{
				int num = this.IndexOf(item);
				if (num >= 0)
				{
					while (num != 0)
					{
						MinimumBinaryHeap<T>.Swap(ref this.m_HeapArray[num], ref this.m_HeapArray[MinimumBinaryHeap<T>.GetParentIndex(num)]);
						num = MinimumBinaryHeap<T>.GetParentIndex(num);
					}
					this.ExtractMin();
				}
			}
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000026BC File Offset: 0x000008BC
		private int IndexOf(T item)
		{
			for (int i = 0; i < this.Count; i++)
			{
				if (this.m_HeapArray[i].Equals(item))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002700 File Offset: 0x00000900
		public T ExtractMin()
		{
			object @lock = this.m_Lock;
			T t2;
			lock (@lock)
			{
				if (this.Count <= 0)
				{
					throw new InvalidOperationException("Can not ExtractMin: BinaryHeap is empty.");
				}
				T t = this.m_HeapArray[0];
				if (this.Count == 1)
				{
					int num = this.Count;
					this.Count = num - 1;
					T[] heapArray = this.m_HeapArray;
					int num2 = 0;
					t2 = default(T);
					heapArray[num2] = t2;
					t2 = t;
				}
				else
				{
					int num = this.Count;
					this.Count = num - 1;
					this.m_HeapArray[0] = this.m_HeapArray[this.Count];
					this.m_HeapArray[this.Count] = default(T);
					this.MinHeapify();
					this.DecreaseHeapCapacityWhenSpare();
					t2 = t;
				}
			}
			return t2;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000027E8 File Offset: 0x000009E8
		private void DecreaseHeapCapacityWhenSpare()
		{
			int num = (int)Math.Ceiling((double)((float)this.m_HeapArray.Length / 2f));
			if (this.Count <= this.m_MinimumCapacity || this.Count > num)
			{
				return;
			}
			T[] array = new T[this.Count];
			Array.Copy(this.m_HeapArray, array, this.Count);
			this.m_HeapArray = array;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x0000284C File Offset: 0x00000A4C
		private void MinHeapify()
		{
			MinimumBinaryHeap<T>.<>c__DisplayClass21_0 CS$<>8__locals1;
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.smallest = 0;
			for (;;)
			{
				CS$<>8__locals1.currentIndex = CS$<>8__locals1.smallest;
				this.<MinHeapify>g__UpdateSmallestIndex|21_0(ref CS$<>8__locals1);
				if (CS$<>8__locals1.smallest == CS$<>8__locals1.currentIndex)
				{
					break;
				}
				MinimumBinaryHeap<T>.Swap(ref this.m_HeapArray[CS$<>8__locals1.currentIndex], ref this.m_HeapArray[CS$<>8__locals1.smallest]);
				if (CS$<>8__locals1.smallest == CS$<>8__locals1.currentIndex)
				{
					return;
				}
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000028C4 File Offset: 0x00000AC4
		private static void Swap(ref T lhs, ref T rhs)
		{
			T t = rhs;
			T t2 = lhs;
			lhs = t;
			rhs = t2;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000028ED File Offset: 0x00000AED
		private static int GetParentIndex(int index)
		{
			return (index - 1) / 2;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000028F4 File Offset: 0x00000AF4
		private static int GetLeftChildIndex(int index)
		{
			return 2 * index + 1;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000028FB File Offset: 0x00000AFB
		private static int GetRightChildIndex(int index)
		{
			return 2 * index + 2;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002904 File Offset: 0x00000B04
		[CompilerGenerated]
		private void <MinHeapify>g__UpdateSmallestIndex|21_0(ref MinimumBinaryHeap<T>.<>c__DisplayClass21_0 A_1)
		{
			A_1.smallest = A_1.currentIndex;
			int leftChildIndex = MinimumBinaryHeap<T>.GetLeftChildIndex(A_1.currentIndex);
			int rightChildIndex = MinimumBinaryHeap<T>.GetRightChildIndex(A_1.currentIndex);
			this.<MinHeapify>g__UpdateSmallestIfCandidateIsSmaller|21_1(leftChildIndex, ref A_1);
			this.<MinHeapify>g__UpdateSmallestIfCandidateIsSmaller|21_1(rightChildIndex, ref A_1);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002945 File Offset: 0x00000B45
		[CompilerGenerated]
		private void <MinHeapify>g__UpdateSmallestIfCandidateIsSmaller|21_1(int candidate, ref MinimumBinaryHeap<T>.<>c__DisplayClass21_0 A_2)
		{
			if (candidate >= this.Count || this.m_Comparer.Compare(this.m_HeapArray[candidate], this.m_HeapArray[A_2.smallest]) >= 0)
			{
				return;
			}
			A_2.smallest = candidate;
		}

		// Token: 0x0400000B RID: 11
		private readonly object m_Lock = new object();

		// Token: 0x0400000C RID: 12
		private readonly IComparer<T> m_Comparer;

		// Token: 0x0400000D RID: 13
		private readonly int m_MinimumCapacity;

		// Token: 0x0400000E RID: 14
		private T[] m_HeapArray;
	}
}
