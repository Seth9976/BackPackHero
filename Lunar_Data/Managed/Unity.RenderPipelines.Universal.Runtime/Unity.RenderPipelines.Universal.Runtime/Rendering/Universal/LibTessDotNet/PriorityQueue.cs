using System;
using System.Collections.Generic;

namespace UnityEngine.Rendering.Universal.LibTessDotNet
{
	// Token: 0x020000F7 RID: 247
	internal class PriorityQueue<TValue> where TValue : class
	{
		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x0600071F RID: 1823 RVA: 0x000288C0 File Offset: 0x00026AC0
		public bool Empty
		{
			get
			{
				return this._size == 0 && this._heap.Empty;
			}
		}

		// Token: 0x06000720 RID: 1824 RVA: 0x000288D7 File Offset: 0x00026AD7
		public PriorityQueue(int initialSize, PriorityHeap<TValue>.LessOrEqual leq)
		{
			this._leq = leq;
			this._heap = new PriorityHeap<TValue>(initialSize, leq);
			this._keys = new TValue[initialSize];
			this._size = 0;
			this._max = initialSize;
			this._initialized = false;
		}

		// Token: 0x06000721 RID: 1825 RVA: 0x00028914 File Offset: 0x00026B14
		private static void Swap(ref int a, ref int b)
		{
			int num = a;
			a = b;
			b = num;
		}

		// Token: 0x06000722 RID: 1826 RVA: 0x0002892C File Offset: 0x00026B2C
		public void Init()
		{
			Stack<PriorityQueue<TValue>.StackItem> stack = new Stack<PriorityQueue<TValue>.StackItem>();
			uint num = 2016473283U;
			int num2 = 0;
			int i = this._size - 1;
			this._order = new int[this._size + 1];
			int num3 = 0;
			for (int j = num2; j <= i; j++)
			{
				this._order[j] = num3;
				num3++;
			}
			stack.Push(new PriorityQueue<TValue>.StackItem
			{
				p = num2,
				r = i
			});
			while (stack.Count > 0)
			{
				PriorityQueue<TValue>.StackItem stackItem = stack.Pop();
				num2 = stackItem.p;
				i = stackItem.r;
				while (i > num2 + 10)
				{
					num = num * 1539415821U + 1U;
					int j = num2 + (int)((ulong)num % (ulong)((long)(i - num2 + 1)));
					num3 = this._order[j];
					this._order[j] = this._order[num2];
					this._order[num2] = num3;
					j = num2 - 1;
					int num4 = i + 1;
					for (;;)
					{
						j++;
						if (this._leq(this._keys[this._order[j]], this._keys[num3]))
						{
							do
							{
								num4--;
							}
							while (!this._leq(this._keys[num3], this._keys[this._order[num4]]));
							PriorityQueue<TValue>.Swap(ref this._order[j], ref this._order[num4]);
							if (j >= num4)
							{
								break;
							}
						}
					}
					PriorityQueue<TValue>.Swap(ref this._order[j], ref this._order[num4]);
					if (j - num2 < i - num4)
					{
						stack.Push(new PriorityQueue<TValue>.StackItem
						{
							p = num4 + 1,
							r = i
						});
						i = j - 1;
					}
					else
					{
						stack.Push(new PriorityQueue<TValue>.StackItem
						{
							p = num2,
							r = j - 1
						});
						num2 = num4 + 1;
					}
				}
				for (int j = num2 + 1; j <= i; j++)
				{
					num3 = this._order[j];
					int num4 = j;
					while (num4 > num2 && !this._leq(this._keys[num3], this._keys[this._order[num4 - 1]]))
					{
						this._order[num4] = this._order[num4 - 1];
						num4--;
					}
					this._order[num4] = num3;
				}
			}
			this._max = this._size;
			this._initialized = true;
			this._heap.Init();
		}

		// Token: 0x06000723 RID: 1827 RVA: 0x00028BA0 File Offset: 0x00026DA0
		public PQHandle Insert(TValue value)
		{
			if (this._initialized)
			{
				return this._heap.Insert(value);
			}
			int size = this._size;
			int num = this._size + 1;
			this._size = num;
			if (num >= this._max)
			{
				this._max <<= 1;
				Array.Resize<TValue>(ref this._keys, this._max);
			}
			this._keys[size] = value;
			return new PQHandle
			{
				_handle = -(size + 1)
			};
		}

		// Token: 0x06000724 RID: 1828 RVA: 0x00028C24 File Offset: 0x00026E24
		public TValue ExtractMin()
		{
			if (this._size == 0)
			{
				return this._heap.ExtractMin();
			}
			TValue tvalue = this._keys[this._order[this._size - 1]];
			if (!this._heap.Empty)
			{
				TValue tvalue2 = this._heap.Minimum();
				if (this._leq(tvalue2, tvalue))
				{
					return this._heap.ExtractMin();
				}
			}
			do
			{
				this._size--;
			}
			while (this._size > 0 && this._keys[this._order[this._size - 1]] == null);
			return tvalue;
		}

		// Token: 0x06000725 RID: 1829 RVA: 0x00028CD0 File Offset: 0x00026ED0
		public TValue Minimum()
		{
			if (this._size == 0)
			{
				return this._heap.Minimum();
			}
			TValue tvalue = this._keys[this._order[this._size - 1]];
			if (!this._heap.Empty)
			{
				TValue tvalue2 = this._heap.Minimum();
				if (this._leq(tvalue2, tvalue))
				{
					return tvalue2;
				}
			}
			return tvalue;
		}

		// Token: 0x06000726 RID: 1830 RVA: 0x00028D38 File Offset: 0x00026F38
		public void Remove(PQHandle handle)
		{
			int num = handle._handle;
			if (num >= 0)
			{
				this._heap.Remove(handle);
				return;
			}
			num = -(num + 1);
			this._keys[num] = default(TValue);
			while (this._size > 0 && this._keys[this._order[this._size - 1]] == null)
			{
				this._size--;
			}
		}

		// Token: 0x040006B7 RID: 1719
		private PriorityHeap<TValue>.LessOrEqual _leq;

		// Token: 0x040006B8 RID: 1720
		private PriorityHeap<TValue> _heap;

		// Token: 0x040006B9 RID: 1721
		private TValue[] _keys;

		// Token: 0x040006BA RID: 1722
		private int[] _order;

		// Token: 0x040006BB RID: 1723
		private int _size;

		// Token: 0x040006BC RID: 1724
		private int _max;

		// Token: 0x040006BD RID: 1725
		private bool _initialized;

		// Token: 0x0200019A RID: 410
		private class StackItem
		{
			// Token: 0x04000A2E RID: 2606
			internal int p;

			// Token: 0x04000A2F RID: 2607
			internal int r;
		}
	}
}
