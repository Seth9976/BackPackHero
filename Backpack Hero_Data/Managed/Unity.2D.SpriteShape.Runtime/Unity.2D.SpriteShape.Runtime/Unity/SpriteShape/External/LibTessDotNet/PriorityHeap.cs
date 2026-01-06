using System;

namespace Unity.SpriteShape.External.LibTessDotNet
{
	// Token: 0x0200000A RID: 10
	internal class PriorityHeap<TValue> where TValue : class
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000040 RID: 64 RVA: 0x000036AD File Offset: 0x000018AD
		public bool Empty
		{
			get
			{
				return this._size == 0;
			}
		}

		// Token: 0x06000041 RID: 65 RVA: 0x000036B8 File Offset: 0x000018B8
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

		// Token: 0x06000042 RID: 66 RVA: 0x0000372C File Offset: 0x0000192C
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

		// Token: 0x06000043 RID: 67 RVA: 0x000037FC File Offset: 0x000019FC
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

		// Token: 0x06000044 RID: 68 RVA: 0x0000387C File Offset: 0x00001A7C
		public void Init()
		{
			for (int i = this._size; i >= 1; i--)
			{
				this.FloatDown(i);
			}
			this._initialized = true;
		}

		// Token: 0x06000045 RID: 69 RVA: 0x000038A8 File Offset: 0x00001AA8
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

		// Token: 0x06000046 RID: 70 RVA: 0x00003998 File Offset: 0x00001B98
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

		// Token: 0x06000047 RID: 71 RVA: 0x00003A36 File Offset: 0x00001C36
		public TValue Minimum()
		{
			return this._handles[this._nodes[1]]._key;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00003A4C File Offset: 0x00001C4C
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

		// Token: 0x0400001F RID: 31
		private PriorityHeap<TValue>.LessOrEqual _leq;

		// Token: 0x04000020 RID: 32
		private int[] _nodes;

		// Token: 0x04000021 RID: 33
		private PriorityHeap<TValue>.HandleElem[] _handles;

		// Token: 0x04000022 RID: 34
		private int _size;

		// Token: 0x04000023 RID: 35
		private int _max;

		// Token: 0x04000024 RID: 36
		private int _freeList;

		// Token: 0x04000025 RID: 37
		private bool _initialized;

		// Token: 0x02000028 RID: 40
		// (Invoke) Token: 0x060001A1 RID: 417
		public delegate bool LessOrEqual(TValue lhs, TValue rhs);

		// Token: 0x02000029 RID: 41
		protected class HandleElem
		{
			// Token: 0x0400010B RID: 267
			internal TValue _key;

			// Token: 0x0400010C RID: 268
			internal int _node;
		}
	}
}
