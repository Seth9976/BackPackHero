using System;

namespace System.Collections.Generic
{
	// Token: 0x02000803 RID: 2051
	internal sealed class SortedSetEqualityComparer<T> : IEqualityComparer<SortedSet<T>>
	{
		// Token: 0x060041D2 RID: 16850 RVA: 0x000E536F File Offset: 0x000E356F
		public SortedSetEqualityComparer(IEqualityComparer<T> memberEqualityComparer)
			: this(null, memberEqualityComparer)
		{
		}

		// Token: 0x060041D3 RID: 16851 RVA: 0x000E5379 File Offset: 0x000E3579
		private SortedSetEqualityComparer(IComparer<T> comparer, IEqualityComparer<T> memberEqualityComparer)
		{
			this._comparer = comparer ?? Comparer<T>.Default;
			this._memberEqualityComparer = memberEqualityComparer ?? EqualityComparer<T>.Default;
		}

		// Token: 0x060041D4 RID: 16852 RVA: 0x000E53A1 File Offset: 0x000E35A1
		public bool Equals(SortedSet<T> x, SortedSet<T> y)
		{
			return SortedSet<T>.SortedSetEquals(x, y, this._comparer);
		}

		// Token: 0x060041D5 RID: 16853 RVA: 0x000E53B0 File Offset: 0x000E35B0
		public int GetHashCode(SortedSet<T> obj)
		{
			int num = 0;
			if (obj != null)
			{
				foreach (T t in obj)
				{
					num ^= this._memberEqualityComparer.GetHashCode(t) & int.MaxValue;
				}
			}
			return num;
		}

		// Token: 0x060041D6 RID: 16854 RVA: 0x000E5414 File Offset: 0x000E3614
		public override bool Equals(object obj)
		{
			SortedSetEqualityComparer<T> sortedSetEqualityComparer = obj as SortedSetEqualityComparer<T>;
			return sortedSetEqualityComparer != null && this._comparer == sortedSetEqualityComparer._comparer;
		}

		// Token: 0x060041D7 RID: 16855 RVA: 0x000E543B File Offset: 0x000E363B
		public override int GetHashCode()
		{
			return this._comparer.GetHashCode() ^ this._memberEqualityComparer.GetHashCode();
		}

		// Token: 0x0400274A RID: 10058
		private readonly IComparer<T> _comparer;

		// Token: 0x0400274B RID: 10059
		private readonly IEqualityComparer<T> _memberEqualityComparer;
	}
}
