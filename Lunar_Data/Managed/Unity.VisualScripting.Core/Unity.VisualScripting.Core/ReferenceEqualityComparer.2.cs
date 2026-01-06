using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Unity.VisualScripting
{
	// Token: 0x02000161 RID: 353
	public class ReferenceEqualityComparer<T> : IEqualityComparer<T>
	{
		// Token: 0x0600095C RID: 2396 RVA: 0x0002855B File Offset: 0x0002675B
		private ReferenceEqualityComparer()
		{
		}

		// Token: 0x0600095D RID: 2397 RVA: 0x00028563 File Offset: 0x00026763
		bool IEqualityComparer<T>.Equals(T a, T b)
		{
			return a == b;
		}

		// Token: 0x0600095E RID: 2398 RVA: 0x00028573 File Offset: 0x00026773
		int IEqualityComparer<T>.GetHashCode(T a)
		{
			return ReferenceEqualityComparer<T>.GetHashCode(a);
		}

		// Token: 0x0600095F RID: 2399 RVA: 0x0002857B File Offset: 0x0002677B
		public static int GetHashCode(T a)
		{
			return RuntimeHelpers.GetHashCode(a);
		}

		// Token: 0x0400023D RID: 573
		public static readonly ReferenceEqualityComparer<T> Instance = new ReferenceEqualityComparer<T>();
	}
}
