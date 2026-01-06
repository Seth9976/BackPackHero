using System;

namespace Unity.SpriteShape.External.LibTessDotNet
{
	// Token: 0x02000004 RID: 4
	internal class Dict<TValue> where TValue : class
	{
		// Token: 0x0600000C RID: 12 RVA: 0x0000232C File Offset: 0x0000052C
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

		// Token: 0x0600000D RID: 13 RVA: 0x0000237F File Offset: 0x0000057F
		public Dict<TValue>.Node Insert(TValue key)
		{
			return this.InsertBefore(this._head, key);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002390 File Offset: 0x00000590
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

		// Token: 0x0600000F RID: 15 RVA: 0x000023FC File Offset: 0x000005FC
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

		// Token: 0x06000010 RID: 16 RVA: 0x00002439 File Offset: 0x00000639
		public Dict<TValue>.Node Min()
		{
			return this._head._next;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002446 File Offset: 0x00000646
		public void Remove(Dict<TValue>.Node node)
		{
			node._next._prev = node._prev;
			node._prev._next = node._next;
		}

		// Token: 0x04000012 RID: 18
		private Dict<TValue>.LessOrEqual _leq;

		// Token: 0x04000013 RID: 19
		private Dict<TValue>.Node _head;

		// Token: 0x02000021 RID: 33
		public class Node
		{
			// Token: 0x17000045 RID: 69
			// (get) Token: 0x06000179 RID: 377 RVA: 0x0000E896 File Offset: 0x0000CA96
			public TValue Key
			{
				get
				{
					return this._key;
				}
			}

			// Token: 0x17000046 RID: 70
			// (get) Token: 0x0600017A RID: 378 RVA: 0x0000E89E File Offset: 0x0000CA9E
			public Dict<TValue>.Node Prev
			{
				get
				{
					return this._prev;
				}
			}

			// Token: 0x17000047 RID: 71
			// (get) Token: 0x0600017B RID: 379 RVA: 0x0000E8A6 File Offset: 0x0000CAA6
			public Dict<TValue>.Node Next
			{
				get
				{
					return this._next;
				}
			}

			// Token: 0x040000EC RID: 236
			internal TValue _key;

			// Token: 0x040000ED RID: 237
			internal Dict<TValue>.Node _prev;

			// Token: 0x040000EE RID: 238
			internal Dict<TValue>.Node _next;
		}

		// Token: 0x02000022 RID: 34
		// (Invoke) Token: 0x0600017E RID: 382
		public delegate bool LessOrEqual(TValue lhs, TValue rhs);
	}
}
