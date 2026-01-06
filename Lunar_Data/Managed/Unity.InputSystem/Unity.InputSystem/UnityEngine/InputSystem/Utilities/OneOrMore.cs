using System;
using System.Collections;
using System.Collections.Generic;

namespace UnityEngine.InputSystem.Utilities
{
	// Token: 0x0200013C RID: 316
	internal struct OneOrMore<TValue, TList> : IReadOnlyList<TValue>, IEnumerable<TValue>, IEnumerable, IReadOnlyCollection<TValue> where TList : IReadOnlyList<TValue>
	{
		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x06001124 RID: 4388 RVA: 0x00051750 File Offset: 0x0004F950
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

		// Token: 0x170004BA RID: 1210
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

		// Token: 0x06001126 RID: 4390 RVA: 0x000517C0 File Offset: 0x0004F9C0
		public OneOrMore(TValue single)
		{
			this.m_IsSingle = true;
			this.m_Single = single;
			this.m_Multiple = default(TList);
		}

		// Token: 0x06001127 RID: 4391 RVA: 0x000517DC File Offset: 0x0004F9DC
		public OneOrMore(TList multiple)
		{
			this.m_IsSingle = false;
			this.m_Single = default(TValue);
			this.m_Multiple = multiple;
		}

		// Token: 0x06001128 RID: 4392 RVA: 0x000517F8 File Offset: 0x0004F9F8
		public static implicit operator OneOrMore<TValue, TList>(TValue single)
		{
			return new OneOrMore<TValue, TList>(single);
		}

		// Token: 0x06001129 RID: 4393 RVA: 0x00051800 File Offset: 0x0004FA00
		public static implicit operator OneOrMore<TValue, TList>(TList multiple)
		{
			return new OneOrMore<TValue, TList>(multiple);
		}

		// Token: 0x0600112A RID: 4394 RVA: 0x00051808 File Offset: 0x0004FA08
		public IEnumerator<TValue> GetEnumerator()
		{
			return new OneOrMore<TValue, TList>.Enumerator
			{
				m_List = this
			};
		}

		// Token: 0x0600112B RID: 4395 RVA: 0x0005181B File Offset: 0x0004FA1B
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x040006D1 RID: 1745
		private readonly bool m_IsSingle;

		// Token: 0x040006D2 RID: 1746
		private readonly TValue m_Single;

		// Token: 0x040006D3 RID: 1747
		private readonly TList m_Multiple;

		// Token: 0x02000244 RID: 580
		private class Enumerator : IEnumerator<TValue>, IEnumerator, IDisposable
		{
			// Token: 0x060015CA RID: 5578 RVA: 0x00063AC2 File Offset: 0x00061CC2
			public bool MoveNext()
			{
				this.m_Index++;
				return this.m_Index < this.m_List.Count;
			}

			// Token: 0x060015CB RID: 5579 RVA: 0x00063AE8 File Offset: 0x00061CE8
			public void Reset()
			{
				this.m_Index = -1;
			}

			// Token: 0x170005DC RID: 1500
			// (get) Token: 0x060015CC RID: 5580 RVA: 0x00063AF1 File Offset: 0x00061CF1
			public TValue Current
			{
				get
				{
					return this.m_List[this.m_Index];
				}
			}

			// Token: 0x170005DD RID: 1501
			// (get) Token: 0x060015CD RID: 5581 RVA: 0x00063B04 File Offset: 0x00061D04
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x060015CE RID: 5582 RVA: 0x00063B11 File Offset: 0x00061D11
			public void Dispose()
			{
			}

			// Token: 0x04000C23 RID: 3107
			internal int m_Index = -1;

			// Token: 0x04000C24 RID: 3108
			internal OneOrMore<TValue, TList> m_List;
		}
	}
}
