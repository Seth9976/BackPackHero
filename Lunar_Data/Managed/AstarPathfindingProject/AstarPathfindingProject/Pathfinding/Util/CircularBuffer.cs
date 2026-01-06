using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Pathfinding.Util
{
	// Token: 0x02000246 RID: 582
	public struct CircularBuffer<T> : IReadOnlyList<T>, IEnumerable<T>, IEnumerable, IReadOnlyCollection<T>
	{
		// Token: 0x170001CD RID: 461
		// (get) Token: 0x06000D89 RID: 3465 RVA: 0x00056A15 File Offset: 0x00054C15
		public readonly int Length
		{
			get
			{
				return this.length;
			}
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x06000D8A RID: 3466 RVA: 0x00056A1D File Offset: 0x00054C1D
		public readonly int AbsoluteStartIndex
		{
			get
			{
				return this.head;
			}
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x06000D8B RID: 3467 RVA: 0x00056A25 File Offset: 0x00054C25
		public readonly int AbsoluteEndIndex
		{
			get
			{
				return this.head + this.length - 1;
			}
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x06000D8C RID: 3468 RVA: 0x00056A36 File Offset: 0x00054C36
		public readonly ref T First
		{
			get
			{
				return ref this.data[this.head & (this.data.Length - 1)];
			}
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x06000D8D RID: 3469 RVA: 0x00056A54 File Offset: 0x00054C54
		public readonly ref T Last
		{
			get
			{
				return ref this.data[(this.head + this.length - 1) & (this.data.Length - 1)];
			}
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x06000D8E RID: 3470 RVA: 0x00056A15 File Offset: 0x00054C15
		int IReadOnlyCollection<T>.Count
		{
			get
			{
				return this.length;
			}
		}

		// Token: 0x06000D8F RID: 3471 RVA: 0x00056A7B File Offset: 0x00054C7B
		public CircularBuffer(int initialCapacity)
		{
			this.data = ArrayPool<T>.Claim(initialCapacity);
			this.head = 0;
			this.length = 0;
		}

		// Token: 0x06000D90 RID: 3472 RVA: 0x00056A97 File Offset: 0x00054C97
		public CircularBuffer(T[] backingArray)
		{
			this.data = backingArray;
			this.head = 0;
			this.length = 0;
		}

		// Token: 0x06000D91 RID: 3473 RVA: 0x00056AAE File Offset: 0x00054CAE
		public void Clear()
		{
			this.length = 0;
			this.head = 0;
		}

		// Token: 0x06000D92 RID: 3474 RVA: 0x00056AC0 File Offset: 0x00054CC0
		public void AddRange(List<T> items)
		{
			for (int i = 0; i < items.Count; i++)
			{
				this.PushEnd(items[i]);
			}
		}

		// Token: 0x06000D93 RID: 3475 RVA: 0x00056AEC File Offset: 0x00054CEC
		[MethodImpl(256)]
		public void PushStart(T item)
		{
			if (this.data == null || this.length >= this.data.Length)
			{
				this.Grow();
			}
			this.length++;
			this.head--;
			this[0] = item;
		}

		// Token: 0x06000D94 RID: 3476 RVA: 0x00056B3B File Offset: 0x00054D3B
		[MethodImpl(256)]
		public void PushEnd(T item)
		{
			if (this.data == null || this.length >= this.data.Length)
			{
				this.Grow();
			}
			this.length++;
			this[this.length - 1] = item;
		}

		// Token: 0x06000D95 RID: 3477 RVA: 0x00056B78 File Offset: 0x00054D78
		[MethodImpl(256)]
		public void Push(bool toStart, T item)
		{
			if (toStart)
			{
				this.PushStart(item);
				return;
			}
			this.PushEnd(item);
		}

		// Token: 0x06000D96 RID: 3478 RVA: 0x00056B8C File Offset: 0x00054D8C
		public T PopStart()
		{
			if (this.length == 0)
			{
				throw new InvalidOperationException();
			}
			T t = this[0];
			this.head++;
			this.length--;
			return t;
		}

		// Token: 0x06000D97 RID: 3479 RVA: 0x00056BBF File Offset: 0x00054DBF
		public T PopEnd()
		{
			if (this.length == 0)
			{
				throw new InvalidOperationException();
			}
			T t = this[this.length - 1];
			this.length--;
			return t;
		}

		// Token: 0x06000D98 RID: 3480 RVA: 0x00056BEB File Offset: 0x00054DEB
		public T Pop(bool fromStart)
		{
			if (fromStart)
			{
				return this.PopStart();
			}
			return this.PopEnd();
		}

		// Token: 0x06000D99 RID: 3481 RVA: 0x00056BFD File Offset: 0x00054DFD
		public readonly T GetBoundaryValue(bool start)
		{
			return this.GetAbsolute(start ? this.AbsoluteStartIndex : this.AbsoluteEndIndex);
		}

		// Token: 0x06000D9A RID: 3482 RVA: 0x00056C16 File Offset: 0x00054E16
		public void InsertAbsolute(int index, T item)
		{
			this.SpliceUninitializedAbsolute(index, 0, 1);
			this.data[index & (this.data.Length - 1)] = item;
		}

		// Token: 0x06000D9B RID: 3483 RVA: 0x00056C39 File Offset: 0x00054E39
		public void Splice(int startIndex, int toRemove, List<T> toInsert)
		{
			this.SpliceAbsolute(startIndex + this.head, toRemove, toInsert);
		}

		// Token: 0x06000D9C RID: 3484 RVA: 0x00056C4C File Offset: 0x00054E4C
		public void SpliceAbsolute(int startIndex, int toRemove, List<T> toInsert)
		{
			if (toInsert == null)
			{
				this.SpliceUninitializedAbsolute(startIndex, toRemove, 0);
				return;
			}
			this.SpliceUninitializedAbsolute(startIndex, toRemove, toInsert.Count);
			for (int i = 0; i < toInsert.Count; i++)
			{
				this.data[(startIndex + i) & (this.data.Length - 1)] = toInsert[i];
			}
		}

		// Token: 0x06000D9D RID: 3485 RVA: 0x00056CA5 File Offset: 0x00054EA5
		public void SpliceUninitialized(int startIndex, int toRemove, int toInsert)
		{
			this.SpliceUninitializedAbsolute(startIndex + this.head, toRemove, toInsert);
		}

		// Token: 0x06000D9E RID: 3486 RVA: 0x00056CB8 File Offset: 0x00054EB8
		public void SpliceUninitializedAbsolute(int startIndex, int toRemove, int toInsert)
		{
			int num = toInsert - toRemove;
			while (this.length + num > this.data.Length)
			{
				this.Grow();
			}
			this.MoveAbsolute(startIndex + toRemove, this.AbsoluteEndIndex, num);
			this.length += num;
		}

		// Token: 0x06000D9F RID: 3487 RVA: 0x00056D04 File Offset: 0x00054F04
		private void MoveAbsolute(int startIndex, int endIndex, int deltaIndex)
		{
			if (deltaIndex > 0)
			{
				for (int i = endIndex; i >= startIndex; i--)
				{
					this.data[(i + deltaIndex) & (this.data.Length - 1)] = this.data[i & (this.data.Length - 1)];
				}
				return;
			}
			if (deltaIndex < 0)
			{
				for (int j = startIndex; j <= endIndex; j++)
				{
					this.data[(j + deltaIndex) & (this.data.Length - 1)] = this.data[j & (this.data.Length - 1)];
				}
			}
		}

		// Token: 0x170001D3 RID: 467
		public T this[int index]
		{
			[MethodImpl(256)]
			readonly get
			{
				return this.data[(index + this.head) & (this.data.Length - 1)];
			}
			[MethodImpl(256)]
			set
			{
				this.data[(index + this.head) & (this.data.Length - 1)] = value;
			}
		}

		// Token: 0x06000DA2 RID: 3490 RVA: 0x00056DD3 File Offset: 0x00054FD3
		[MethodImpl(256)]
		public readonly T GetAbsolute(int index)
		{
			return this.data[index & (this.data.Length - 1)];
		}

		// Token: 0x06000DA3 RID: 3491 RVA: 0x00056DEC File Offset: 0x00054FEC
		private void Grow()
		{
			T[] array = ArrayPool<T>.Claim(Math.Max(4, (this.data != null) ? (this.data.Length * 2) : 0));
			if (this.data != null)
			{
				int num = this.data.Length - (this.head & (this.data.Length - 1));
				Array.Copy(this.data, this.head & (this.data.Length - 1), array, this.head & (array.Length - 1), num);
				int num2 = this.length - num;
				if (num2 > 0)
				{
					Array.Copy(this.data, 0, array, (this.head + num) & (array.Length - 1), num2);
				}
				ArrayPool<T>.Release(ref this.data, false);
			}
			this.data = array;
		}

		// Token: 0x06000DA4 RID: 3492 RVA: 0x00056EA3 File Offset: 0x000550A3
		public void Pool()
		{
			ArrayPool<T>.Release(ref this.data, false);
			this.length = 0;
			this.head = 0;
		}

		// Token: 0x06000DA5 RID: 3493 RVA: 0x00056EBF File Offset: 0x000550BF
		public IEnumerator<T> GetEnumerator()
		{
			int num;
			for (int i = 0; i < this.length; i = num + 1)
			{
				yield return this[i];
				num = i;
			}
			yield break;
		}

		// Token: 0x06000DA6 RID: 3494 RVA: 0x00056ED3 File Offset: 0x000550D3
		IEnumerator IEnumerable.GetEnumerator()
		{
			int num;
			for (int i = 0; i < this.length; i = num + 1)
			{
				yield return this[i];
				num = i;
			}
			yield break;
		}

		// Token: 0x06000DA7 RID: 3495 RVA: 0x00056EE8 File Offset: 0x000550E8
		public CircularBuffer<T> Clone()
		{
			return new CircularBuffer<T>
			{
				data = ((this.data != null) ? ((T[])this.data.Clone()) : null),
				length = this.length,
				head = this.head
			};
		}

		// Token: 0x04000AAE RID: 2734
		internal T[] data;

		// Token: 0x04000AAF RID: 2735
		internal int head;

		// Token: 0x04000AB0 RID: 2736
		private int length;
	}
}
