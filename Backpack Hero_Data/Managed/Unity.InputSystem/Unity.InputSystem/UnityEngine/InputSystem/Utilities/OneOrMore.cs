using System;
using System.Collections;
using System.Collections.Generic;

namespace UnityEngine.InputSystem.Utilities
{
	// Token: 0x0200013C RID: 316
	internal struct OneOrMore<TValue, TList> : IReadOnlyList<TValue>, IEnumerable<TValue>, IEnumerable, IReadOnlyCollection<TValue> where TList : IReadOnlyList<TValue>
	{
		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x0600112B RID: 4395 RVA: 0x00051964 File Offset: 0x0004FB64
		public int Count
		{
			get
			{
				if (!this.m_IsSingle)
				{
					TList multiple = this.m_Multiple;
					return multiple.Count;
				}
				return 1;
			}
		}

		// Token: 0x170004BC RID: 1212
		public TValue this[int index]
		{
			get
			{
				if (!this.m_IsSingle)
				{
					TList multiple = this.m_Multiple;
					return multiple[index];
				}
				if (index < 0 || index > 1)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				return this.m_Single;
			}
		}

		// Token: 0x0600112D RID: 4397 RVA: 0x000519D4 File Offset: 0x0004FBD4
		public OneOrMore(TValue single)
		{
			this.m_IsSingle = true;
			this.m_Single = single;
			this.m_Multiple = default(TList);
		}

		// Token: 0x0600112E RID: 4398 RVA: 0x000519F0 File Offset: 0x0004FBF0
		public OneOrMore(TList multiple)
		{
			this.m_IsSingle = false;
			this.m_Single = default(TValue);
			this.m_Multiple = multiple;
		}

		// Token: 0x0600112F RID: 4399 RVA: 0x00051A0C File Offset: 0x0004FC0C
		public static implicit operator OneOrMore<TValue, TList>(TValue single)
		{
			return new OneOrMore<TValue, TList>(single);
		}

		// Token: 0x06001130 RID: 4400 RVA: 0x00051A14 File Offset: 0x0004FC14
		public static implicit operator OneOrMore<TValue, TList>(TList multiple)
		{
			return new OneOrMore<TValue, TList>(multiple);
		}

		// Token: 0x06001131 RID: 4401 RVA: 0x00051A1C File Offset: 0x0004FC1C
		public IEnumerator<TValue> GetEnumerator()
		{
			return new OneOrMore<TValue, TList>.Enumerator
			{
				m_List = this
			};
		}

		// Token: 0x06001132 RID: 4402 RVA: 0x00051A2F File Offset: 0x0004FC2F
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x040006D2 RID: 1746
		private readonly bool m_IsSingle;

		// Token: 0x040006D3 RID: 1747
		private readonly TValue m_Single;

		// Token: 0x040006D4 RID: 1748
		private readonly TList m_Multiple;

		// Token: 0x02000244 RID: 580
		private class Enumerator : IEnumerator<TValue>, IEnumerator, IDisposable
		{
			// Token: 0x060015D1 RID: 5585 RVA: 0x00063CD6 File Offset: 0x00061ED6
			public bool MoveNext()
			{
				this.m_Index++;
				return this.m_Index < this.m_List.Count;
			}

			// Token: 0x060015D2 RID: 5586 RVA: 0x00063CFC File Offset: 0x00061EFC
			public void Reset()
			{
				this.m_Index = -1;
			}

			// Token: 0x170005DE RID: 1502
			// (get) Token: 0x060015D3 RID: 5587 RVA: 0x00063D05 File Offset: 0x00061F05
			public TValue Current
			{
				get
				{
					return this.m_List[this.m_Index];
				}
			}

			// Token: 0x170005DF RID: 1503
			// (get) Token: 0x060015D4 RID: 5588 RVA: 0x00063D18 File Offset: 0x00061F18
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x060015D5 RID: 5589 RVA: 0x00063D25 File Offset: 0x00061F25
			public void Dispose()
			{
			}

			// Token: 0x04000C24 RID: 3108
			internal int m_Index = -1;

			// Token: 0x04000C25 RID: 3109
			internal OneOrMore<TValue, TList> m_List;
		}
	}
}
