using System;
using System.Collections;
using System.Collections.Generic;

namespace UnityEngine.InputSystem.Utilities
{
	// Token: 0x0200013F RID: 319
	public struct ReadOnlyArray<TValue> : IReadOnlyList<TValue>, IEnumerable<TValue>, IEnumerable, IReadOnlyCollection<TValue>
	{
		// Token: 0x0600117C RID: 4476 RVA: 0x00052BC7 File Offset: 0x00050DC7
		public ReadOnlyArray(TValue[] array)
		{
			this.m_Array = array;
			this.m_StartIndex = 0;
			this.m_Length = ((array != null) ? array.Length : 0);
		}

		// Token: 0x0600117D RID: 4477 RVA: 0x00052BE6 File Offset: 0x00050DE6
		public ReadOnlyArray(TValue[] array, int index, int length)
		{
			this.m_Array = array;
			this.m_StartIndex = index;
			this.m_Length = length;
		}

		// Token: 0x0600117E RID: 4478 RVA: 0x00052C00 File Offset: 0x00050E00
		public TValue[] ToArray()
		{
			TValue[] array = new TValue[this.m_Length];
			if (this.m_Length > 0)
			{
				Array.Copy(this.m_Array, this.m_StartIndex, array, 0, this.m_Length);
			}
			return array;
		}

		// Token: 0x0600117F RID: 4479 RVA: 0x00052C3C File Offset: 0x00050E3C
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

		// Token: 0x06001180 RID: 4480 RVA: 0x00052C86 File Offset: 0x00050E86
		public ReadOnlyArray<TValue>.Enumerator GetEnumerator()
		{
			return new ReadOnlyArray<TValue>.Enumerator(this.m_Array, this.m_StartIndex, this.m_Length);
		}

		// Token: 0x06001181 RID: 4481 RVA: 0x00052C9F File Offset: 0x00050E9F
		IEnumerator<TValue> IEnumerable<TValue>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06001182 RID: 4482 RVA: 0x00052CAC File Offset: 0x00050EAC
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06001183 RID: 4483 RVA: 0x00052CB9 File Offset: 0x00050EB9
		public static implicit operator ReadOnlyArray<TValue>(TValue[] array)
		{
			return new ReadOnlyArray<TValue>(array);
		}

		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x06001184 RID: 4484 RVA: 0x00052CC1 File Offset: 0x00050EC1
		public int Count
		{
			get
			{
				return this.m_Length;
			}
		}

		// Token: 0x170004C1 RID: 1217
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

		// Token: 0x040006E3 RID: 1763
		internal TValue[] m_Array;

		// Token: 0x040006E4 RID: 1764
		internal int m_StartIndex;

		// Token: 0x040006E5 RID: 1765
		internal int m_Length;

		// Token: 0x02000245 RID: 581
		public struct Enumerator : IEnumerator<TValue>, IEnumerator, IDisposable
		{
			// Token: 0x060015D7 RID: 5591 RVA: 0x00063D36 File Offset: 0x00061F36
			internal Enumerator(TValue[] array, int index, int length)
			{
				this.m_Array = array;
				this.m_IndexStart = index - 1;
				this.m_IndexEnd = index + length;
				this.m_Index = this.m_IndexStart;
			}

			// Token: 0x060015D8 RID: 5592 RVA: 0x00063D5D File Offset: 0x00061F5D
			public void Dispose()
			{
			}

			// Token: 0x060015D9 RID: 5593 RVA: 0x00063D5F File Offset: 0x00061F5F
			public bool MoveNext()
			{
				if (this.m_Index < this.m_IndexEnd)
				{
					this.m_Index++;
				}
				return this.m_Index != this.m_IndexEnd;
			}

			// Token: 0x060015DA RID: 5594 RVA: 0x00063D8E File Offset: 0x00061F8E
			public void Reset()
			{
				this.m_Index = this.m_IndexStart;
			}

			// Token: 0x170005E0 RID: 1504
			// (get) Token: 0x060015DB RID: 5595 RVA: 0x00063D9C File Offset: 0x00061F9C
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

			// Token: 0x170005E1 RID: 1505
			// (get) Token: 0x060015DC RID: 5596 RVA: 0x00063DC8 File Offset: 0x00061FC8
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x04000C26 RID: 3110
			private readonly TValue[] m_Array;

			// Token: 0x04000C27 RID: 3111
			private readonly int m_IndexStart;

			// Token: 0x04000C28 RID: 3112
			private readonly int m_IndexEnd;

			// Token: 0x04000C29 RID: 3113
			private int m_Index;
		}
	}
}
