using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Unity.VisualScripting
{
	// Token: 0x02000160 RID: 352
	public class ReferenceEqualityComparer : IEqualityComparer<object>
	{
		// Token: 0x06000957 RID: 2391 RVA: 0x00028531 File Offset: 0x00026731
		private ReferenceEqualityComparer()
		{
		}

		// Token: 0x06000958 RID: 2392 RVA: 0x00028539 File Offset: 0x00026739
		bool IEqualityComparer<object>.Equals(object a, object b)
		{
			return a == b;
		}

		// Token: 0x06000959 RID: 2393 RVA: 0x0002853F File Offset: 0x0002673F
		int IEqualityComparer<object>.GetHashCode(object a)
		{
			return ReferenceEqualityComparer.GetHashCode(a);
		}

		// Token: 0x0600095A RID: 2394 RVA: 0x00028547 File Offset: 0x00026747
		public static int GetHashCode(object a)
		{
			return RuntimeHelpers.GetHashCode(a);
		}

		// Token: 0x0400023C RID: 572
		public static readonly ReferenceEqualityComparer Instance = new ReferenceEqualityComparer();
	}
}
