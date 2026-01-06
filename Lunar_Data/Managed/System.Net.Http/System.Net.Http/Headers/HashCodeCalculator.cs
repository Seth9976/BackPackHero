using System;
using System.Collections.Generic;

namespace System.Net.Http.Headers
{
	// Token: 0x0200003B RID: 59
	internal static class HashCodeCalculator
	{
		// Token: 0x06000216 RID: 534 RVA: 0x00008A2C File Offset: 0x00006C2C
		public static int Calculate<T>(ICollection<T> list)
		{
			if (list == null)
			{
				return 0;
			}
			int num = 17;
			foreach (T t in list)
			{
				num = num * 29 + t.GetHashCode();
			}
			return num;
		}
	}
}
