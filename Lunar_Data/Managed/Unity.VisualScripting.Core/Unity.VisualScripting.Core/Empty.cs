using System;
using System.Collections.Generic;

namespace Unity.VisualScripting
{
	// Token: 0x02000150 RID: 336
	public static class Empty<T>
	{
		// Token: 0x04000229 RID: 553
		public static readonly T[] array = new T[0];

		// Token: 0x0400022A RID: 554
		public static readonly List<T> list = new List<T>(0);

		// Token: 0x0400022B RID: 555
		public static readonly HashSet<T> hashSet = new HashSet<T>();
	}
}
