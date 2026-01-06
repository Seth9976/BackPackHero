using System;

namespace UnityEngine.Rendering.Universal.LibTessDotNet
{
	// Token: 0x020000F0 RID: 240
	internal class Dict<TValue> where TValue : class
	{
		// Token: 0x060006E2 RID: 1762 RVA: 0x000270C8 File Offset: 0x000252C8
		public Dict(Dict<TValue>.LessOrEqual leq)
		{
			this._leq = leq;
			this._head = new Dict<TValue>.Node
			{
				_key = default(TValue)
			};
			this._head._prev = this._head;
			this._head._next = this._head;
		}

		// Token: 0x060006E3 RID: 1763 RVA: 0x0002711B File Offset: 0x0002531B
		public Dict<TValue>.Node Insert(TValue key)
		{
			return this.InsertBefore(this._head, key);
		}

		// Token: 0x060006E4 RID: 1764 RVA: 0x0002712C File Offset: 0x0002532C
		public Dict<TValue>.Node InsertBefore(Dict<TValue>.Node node, TValue key)
		{
			do
			{
				node = node._prev;
			}
			while (node._key != null && !this._leq(node._key, key));
			Dict<TValue>.Node node2 = new Dict<TValue>.Node
			{
				_key = key
			};
			node2._next = node._next;
			node._next._prev = node2;
			node2._prev = node;
			node._next = node2;
			return node2;
		}

		// Token: 0x060006E5 RID: 1765 RVA: 0x00027198 File Offset: 0x00025398
		public Dict<TValue>.Node Find(TValue key)
		{
			Dict<TValue>.Node node = this._head;
			do
			{
				node = node._next;
			}
			while (node._key != null && !this._leq(key, node._key));
			return node;
		}

		// Token: 0x060006E6 RID: 1766 RVA: 0x000271D5 File Offset: 0x000253D5
		public Dict<TValue>.Node Min()
		{
			return this._head._next;
		}

		// Token: 0x060006E7 RID: 1767 RVA: 0x000271E2 File Offset: 0x000253E2
		public void Remove(Dict<TValue>.Node node)
		{
			node._next._prev = node._prev;
			node._prev._next = node._next;
		}

		// Token: 0x040006A3 RID: 1699
		private Dict<TValue>.LessOrEqual _leq;

		// Token: 0x040006A4 RID: 1700
		private Dict<TValue>.Node _head;

		// Token: 0x02000191 RID: 401
		public class Node
		{
			// Token: 0x17000232 RID: 562
			// (get) Token: 0x060009F6 RID: 2550 RVA: 0x00041E2B File Offset: 0x0004002B
			public TValue Key
			{
				get
				{
					return this._key;
				}
			}

			// Token: 0x17000233 RID: 563
			// (get) Token: 0x060009F7 RID: 2551 RVA: 0x00041E33 File Offset: 0x00040033
			public Dict<TValue>.Node Prev
			{
				get
				{
					return this._prev;
				}
			}

			// Token: 0x17000234 RID: 564
			// (get) Token: 0x060009F8 RID: 2552 RVA: 0x00041E3B File Offset: 0x0004003B
			public Dict<TValue>.Node Next
			{
				get
				{
					return this._next;
				}
			}

			// Token: 0x04000A0D RID: 2573
			internal TValue _key;

			// Token: 0x04000A0E RID: 2574
			internal Dict<TValue>.Node _prev;

			// Token: 0x04000A0F RID: 2575
			internal Dict<TValue>.Node _next;
		}

		// Token: 0x02000192 RID: 402
		// (Invoke) Token: 0x060009FB RID: 2555
		public delegate bool LessOrEqual(TValue lhs, TValue rhs);
	}
}
