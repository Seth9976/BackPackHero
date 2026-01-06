using System;

namespace Unity.VisualScripting
{
	// Token: 0x0200004C RID: 76
	internal static class XComparable
	{
		// Token: 0x06000252 RID: 594 RVA: 0x00005EF7 File Offset: 0x000040F7
		internal static bool IsLt<T>(this IComparable<T> x, T y)
		{
			return x.CompareTo(y) < 0;
		}

		// Token: 0x06000253 RID: 595 RVA: 0x00005F03 File Offset: 0x00004103
		internal static bool IsEq<T>(this IComparable<T> x, T y)
		{
			return x.CompareTo(y) == 0;
		}

		// Token: 0x06000254 RID: 596 RVA: 0x00005F0F File Offset: 0x0000410F
		internal static bool IsGt<T>(this IComparable<T> x, T y)
		{
			return x.CompareTo(y) > 0;
		}
	}
}
