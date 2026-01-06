using System;

namespace System.Collections.Generic
{
	// Token: 0x02000AC4 RID: 2756
	[Serializable]
	internal class ComparisonComparer<T> : Comparer<T>
	{
		// Token: 0x06006286 RID: 25222 RVA: 0x00149CF9 File Offset: 0x00147EF9
		public ComparisonComparer(Comparison<T> comparison)
		{
			this._comparison = comparison;
		}

		// Token: 0x06006287 RID: 25223 RVA: 0x00149D08 File Offset: 0x00147F08
		public override int Compare(T x, T y)
		{
			return this._comparison(x, y);
		}

		// Token: 0x04003A35 RID: 14901
		private readonly Comparison<T> _comparison;
	}
}
