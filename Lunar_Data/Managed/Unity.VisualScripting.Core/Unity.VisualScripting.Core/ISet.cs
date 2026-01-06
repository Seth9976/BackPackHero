using System;
using System.Collections;
using System.Collections.Generic;

namespace Unity.VisualScripting
{
	// Token: 0x02000019 RID: 25
	public interface ISet<T> : ICollection<T>, IEnumerable<T>, IEnumerable
	{
		// Token: 0x0600009F RID: 159
		bool Add(T item);

		// Token: 0x060000A0 RID: 160
		void UnionWith(IEnumerable<T> other);

		// Token: 0x060000A1 RID: 161
		void IntersectWith(IEnumerable<T> other);

		// Token: 0x060000A2 RID: 162
		void ExceptWith(IEnumerable<T> other);

		// Token: 0x060000A3 RID: 163
		void SymmetricExceptWith(IEnumerable<T> other);

		// Token: 0x060000A4 RID: 164
		bool IsSubsetOf(IEnumerable<T> other);

		// Token: 0x060000A5 RID: 165
		bool IsSupersetOf(IEnumerable<T> other);

		// Token: 0x060000A6 RID: 166
		bool IsProperSupersetOf(IEnumerable<T> other);

		// Token: 0x060000A7 RID: 167
		bool IsProperSubsetOf(IEnumerable<T> other);

		// Token: 0x060000A8 RID: 168
		bool Overlaps(IEnumerable<T> other);

		// Token: 0x060000A9 RID: 169
		bool SetEquals(IEnumerable<T> other);
	}
}
