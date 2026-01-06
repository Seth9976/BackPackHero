using System;

namespace UnityEngine.Rendering.Universal.LibTessDotNet
{
	// Token: 0x020000F6 RID: 246
	internal class PriorityHeap<TValue> where TValue : class
	{
		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x06000716 RID: 1814 RVA: 0x00028449 File Offset: 0x00026649
		public bool Empty
		{
			get
			{
				return this._size == 0;
			}
		}

		// Token: 0x06000717 RID: 1815 RVA: 0x00028454 File Offset: 0x00026654
		public PriorityHeap(int initialSize, PriorityHeap<TValue>.LessOrEqual leq)
		{
			this._leq = leq;
			this._nodes = new int[initialSize + 1];
			this._handles = new PriorityHeap<TValue>.HandleElem[initialSize + 1];
			this._size = 0;
			this._max = initialSize;
			this._freeList = 0;
			this._initialized = false;
			this._nodes[1] = 1;
			this._handles[1] = new PriorityHeap<TValue>.HandleElem
			{
				_key = default(TValue)
			};
		}

		// Token: 0x06000718 RID: 1816 RVA: 0x000284C8 File Offset: 0x000266C8
		private void FloatDown(int curr)
		{
			int num = this._nodes[curr];
			for (;;)
			{
				int num2 = curr << 1;
				if (num2 < this._size && this._leq(this._handles[this._nodes[num2 + 1]]._key, this._handles[this._nodes[num2]]._key))
				{
					num2++;
				}
				int num3 = this._nodes[num2];
				if (num2 > this._size || this._leq(this._handles[num]._key, this._handles[num3]._key))
				{
					break;
				}
				this._nodes[curr] = num3;
				this._handles[num3]._node = curr;
				curr = num2;
			}
			this._nodes[curr] = num;
			this._handles[num]._node = curr;
		}

		// Token: 0x06000719 RID: 1817 RVA: 0x00028598 File Offset: 0x00026798
		private void FloatUp(int curr)
		{
			int num = this._nodes[curr];
			for (;;)
			{
				int num2 = curr >> 1;
				int num3 = this._nodes[num2];
				if (num2 == 0 || this._leq(this._handles[num3]._key, this._handles[num]._key))
				{
					break;
				}
				this._nodes[curr] = num3;
				this._handles[num3]._node = curr;
				curr = num2;
			}
			this._nodes[curr] = num;
			this._handles[num]._node = curr;
		}

		// Token: 0x0600071A RID: 1818 RVA: 0x00028618 File Offset: 0x00026818
		public void Init()
		{
			for (int i = this._size; i >= 1; i--)
			{
				this.FloatDown(i);
			}
			this._initialized = true;
		}

		// Token: 0x0600071B RID: 1819 RVA: 0x00028644 File Offset: 0x00026844
		public PQHandle Insert(TValue value)
		{
			int num = this._size + 1;
			this._size = num;
			int num2 = num;
			if (num2 * 2 > this._max)
			{
				this._max <<= 1;
				Array.Resize<int>(ref this._nodes, this._max + 1);
				Array.Resize<PriorityHeap<TValue>.HandleElem>(ref this._handles, this._max + 1);
			}
			int num3;
			if (this._freeList == 0)
			{
				num3 = num2;
			}
			else
			{
				num3 = this._freeList;
				this._freeList = this._handles[num3]._node;
			}
			this._nodes[num2] = num3;
			if (this._handles[num3] == null)
			{
				this._handles[num3] = new PriorityHeap<TValue>.HandleElem
				{
					_key = value,
					_node = num2
				};
			}
			else
			{
				this._handles[num3]._node = num2;
				this._handles[num3]._key = value;
			}
			if (this._initialized)
			{
				this.FloatUp(num2);
			}
			return new PQHandle
			{
				_handle = num3
			};
		}

		// Token: 0x0600071C RID: 1820 RVA: 0x00028734 File Offset: 0x00026934
		public TValue ExtractMin()
		{
			int num = this._nodes[1];
			TValue key = this._handles[num]._key;
			if (this._size > 0)
			{
				this._nodes[1] = this._nodes[this._size];
				this._handles[this._nodes[1]]._node = 1;
				this._handles[num]._key = default(TValue);
				this._handles[num]._node = this._freeList;
				this._freeList = num;
				int num2 = this._size - 1;
				this._size = num2;
				if (num2 > 0)
				{
					this.FloatDown(1);
				}
			}
			return key;
		}

		// Token: 0x0600071D RID: 1821 RVA: 0x000287D2 File Offset: 0x000269D2
		public TValue Minimum()
		{
			return this._handles[this._nodes[1]]._key;
		}

		// Token: 0x0600071E RID: 1822 RVA: 0x000287E8 File Offset: 0x000269E8
		public void Remove(PQHandle handle)
		{
			int handle2 = handle._handle;
			int node = this._handles[handle2]._node;
			this._nodes[node] = this._nodes[this._size];
			this._handles[this._nodes[node]]._node = node;
			int num = node;
			int num2 = this._size - 1;
			this._size = num2;
			if (num <= num2)
			{
				if (node <= 1 || this._leq(this._handles[this._nodes[node >> 1]]._key, this._handles[this._nodes[node]]._key))
				{
					this.FloatDown(node);
				}
				else
				{
					this.FloatUp(node);
				}
			}
			this._handles[handle2]._key = default(TValue);
			this._handles[handle2]._node = this._freeList;
			this._freeList = handle2;
		}

		// Token: 0x040006B0 RID: 1712
		private PriorityHeap<TValue>.LessOrEqual _leq;

		// Token: 0x040006B1 RID: 1713
		private int[] _nodes;

		// Token: 0x040006B2 RID: 1714
		private PriorityHeap<TValue>.HandleElem[] _handles;

		// Token: 0x040006B3 RID: 1715
		private int _size;

		// Token: 0x040006B4 RID: 1716
		private int _max;

		// Token: 0x040006B5 RID: 1717
		private int _freeList;

		// Token: 0x040006B6 RID: 1718
		private bool _initialized;

		// Token: 0x02000198 RID: 408
		// (Invoke) Token: 0x06000A1E RID: 2590
		public delegate bool LessOrEqual(TValue lhs, TValue rhs);

		// Token: 0x02000199 RID: 409
		protected class HandleElem
		{
			// Token: 0x04000A2C RID: 2604
			internal TValue _key;

			// Token: 0x04000A2D RID: 2605
			internal int _node;
		}
	}
}
