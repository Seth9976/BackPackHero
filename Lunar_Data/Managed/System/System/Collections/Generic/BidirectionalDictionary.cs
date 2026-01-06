using System;

namespace System.Collections.Generic
{
	// Token: 0x020007D9 RID: 2009
	internal sealed class BidirectionalDictionary<T1, T2> : IEnumerable<KeyValuePair<T1, T2>>, IEnumerable
	{
		// Token: 0x0600400B RID: 16395 RVA: 0x000DFC48 File Offset: 0x000DDE48
		public BidirectionalDictionary(int capacity)
		{
			this._forward = new Dictionary<T1, T2>(capacity);
			this._backward = new Dictionary<T2, T1>(capacity);
		}

		// Token: 0x17000E93 RID: 3731
		// (get) Token: 0x0600400C RID: 16396 RVA: 0x000DFC68 File Offset: 0x000DDE68
		public int Count
		{
			get
			{
				return this._forward.Count;
			}
		}

		// Token: 0x0600400D RID: 16397 RVA: 0x000DFC75 File Offset: 0x000DDE75
		public void Add(T1 item1, T2 item2)
		{
			this._forward.Add(item1, item2);
			this._backward.Add(item2, item1);
		}

		// Token: 0x0600400E RID: 16398 RVA: 0x000DFC91 File Offset: 0x000DDE91
		public bool TryGetForward(T1 item1, out T2 item2)
		{
			return this._forward.TryGetValue(item1, out item2);
		}

		// Token: 0x0600400F RID: 16399 RVA: 0x000DFCA0 File Offset: 0x000DDEA0
		public bool TryGetBackward(T2 item2, out T1 item1)
		{
			return this._backward.TryGetValue(item2, out item1);
		}

		// Token: 0x06004010 RID: 16400 RVA: 0x000DFCAF File Offset: 0x000DDEAF
		public Dictionary<T1, T2>.Enumerator GetEnumerator()
		{
			return this._forward.GetEnumerator();
		}

		// Token: 0x06004011 RID: 16401 RVA: 0x000DFCBC File Offset: 0x000DDEBC
		IEnumerator<KeyValuePair<T1, T2>> IEnumerable<KeyValuePair<T1, T2>>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06004012 RID: 16402 RVA: 0x000DFCBC File Offset: 0x000DDEBC
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x040026BB RID: 9915
		private readonly Dictionary<T1, T2> _forward;

		// Token: 0x040026BC RID: 9916
		private readonly Dictionary<T2, T1> _backward;
	}
}
