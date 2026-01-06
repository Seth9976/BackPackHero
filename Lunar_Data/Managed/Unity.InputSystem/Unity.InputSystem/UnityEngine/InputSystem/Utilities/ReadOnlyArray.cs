using System;
using System.Collections;
using System.Collections.Generic;

namespace UnityEngine.InputSystem.Utilities
{
	// Token: 0x0200013F RID: 319
	public struct ReadOnlyArray<TValue> : IReadOnlyList<TValue>, IEnumerable<TValue>, IEnumerable, IReadOnlyCollection<TValue>
	{
		// Token: 0x06001175 RID: 4469 RVA: 0x000529B3 File Offset: 0x00050BB3
		public ReadOnlyArray(TValue[] array)
		{
			this.m_Array = array;
			this.m_StartIndex = 0;
			this.m_Length = ((array != null) ? array.Length : 0);
		}

		// Token: 0x06001176 RID: 4470 RVA: 0x000529D2 File Offset: 0x00050BD2
		public ReadOnlyArray(TValue[] array, int index, int length)
		{
			this.m_Array = array;
			this.m_StartIndex = index;
			this.m_Length = length;
		}

		// Token: 0x06001177 RID: 4471 RVA: 0x000529EC File Offset: 0x00050BEC
		public TValue[] ToArray()
		{
			TValue[] array = new TValue[this.m_Length];
			if (this.m_Length > 0)
			{
				Array.Copy(this.m_Array, this.m_StartIndex, array, 0, this.m_Length);
			}
			return array;
		}

		// Token: 0x06001178 RID: 4472 RVA: 0x00052A28 File Offset: 0x00050C28
		public int IndexOf(Predicate<TValue> predicate)
		{
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			for (int i = 0; i < this.m_Length; i++)
			{
				if (predicate(this.m_Array[this.m_StartIndex + i]))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06001179 RID: 4473 RVA: 0x00052A72 File Offset: 0x00050C72
		public ReadOnlyArray<TValue>.Enumerator GetEnumerator()
		{
			return new ReadOnlyArray<TValue>.Enumerator(this.m_Array, this.m_StartIndex, this.m_Length);
		}

		// Token: 0x0600117A RID: 4474 RVA: 0x00052A8B File Offset: 0x00050C8B
		IEnumerator<TValue> IEnumerable<TValue>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0600117B RID: 4475 RVA: 0x00052A98 File Offset: 0x00050C98
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0600117C RID: 4476 RVA: 0x00052AA5 File Offset: 0x00050CA5
		public static implicit operator ReadOnlyArray<TValue>(TValue[] array)
		{
			return new ReadOnlyArray<TValue>(array);
		}

		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x0600117D RID: 4477 RVA: 0x00052AAD File Offset: 0x00050CAD
		public int Count
		{
			get
			{
				return this.m_Length;
			}
		}

		// Token: 0x170004BF RID: 1215
		public TValue this[int index]
		{
			get
			{
				if (index < 0 || index >= this.m_Length)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				if (this.m_Array == null)
				{
					throw new InvalidOperationException();
				}
				return this.m_Array[this.m_StartIndex + index];
			}
		}

		// Token: 0x040006E2 RID: 1762
		internal TValue[] m_Array;

		// Token: 0x040006E3 RID: 1763
		internal int m_StartIndex;

		// Token: 0x040006E4 RID: 1764
		internal int m_Length;

		// Token: 0x02000245 RID: 581
		public struct Enumerator : IEnumerator<TValue>, IEnumerator, IDisposable
		{
			// Token: 0x060015D0 RID: 5584 RVA: 0x00063B22 File Offset: 0x00061D22
			internal Enumerator(TValue[] array, int index, int length)
			{
				this.m_Array = array;
				this.m_IndexStart = index - 1;
				this.m_IndexEnd = index + length;
				this.m_Index = this.m_IndexStart;
			}

			// Token: 0x060015D1 RID: 5585 RVA: 0x00063B49 File Offset: 0x00061D49
			public void Dispose()
			{
			}

			// Token: 0x060015D2 RID: 5586 RVA: 0x00063B4B File Offset: 0x00061D4B
			public bool MoveNext()
			{
				if (this.m_Index < this.m_IndexEnd)
				{
					this.m_Index++;
				}
				return this.m_Index != this.m_IndexEnd;
			}

			// Token: 0x060015D3 RID: 5587 RVA: 0x00063B7A File Offset: 0x00061D7A
			public void Reset()
			{
				this.m_Index = this.m_IndexStart;
			}

			// Token: 0x170005DE RID: 1502
			// (get) Token: 0x060015D4 RID: 5588 RVA: 0x00063B88 File Offset: 0x00061D88
			public TValue Current
			{
				get
				{
					if (this.m_Index == this.m_IndexEnd)
					{
						throw new InvalidOperationException("Iterated beyond end");
					}
					return this.m_Array[this.m_Index];
				}
			}

			// Token: 0x170005DF RID: 1503
			// (get) Token: 0x060015D5 RID: 5589 RVA: 0x00063BB4 File Offset: 0x00061DB4
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x04000C25 RID: 3109
			private readonly TValue[] m_Array;

			// Token: 0x04000C26 RID: 3110
			private readonly int m_IndexStart;

			// Token: 0x04000C27 RID: 3111
			private readonly int m_IndexEnd;

			// Token: 0x04000C28 RID: 3112
			private int m_Index;
		}
	}
}
