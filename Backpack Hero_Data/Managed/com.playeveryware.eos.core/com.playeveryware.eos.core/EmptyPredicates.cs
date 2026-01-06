using System;
using System.Collections.Generic;

namespace PlayEveryWare.EpicOnlineServices
{
	// Token: 0x02000010 RID: 16
	public static class EmptyPredicates
	{
		// Token: 0x0600003E RID: 62 RVA: 0x0000259F File Offset: 0x0000079F
		public static bool IsEmptyOrNull(string s)
		{
			return s == null || s.Length == 0;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x000025AF File Offset: 0x000007AF
		public static bool IsEmptyOrNull(bool? b)
		{
			return b == null;
		}

		// Token: 0x06000040 RID: 64 RVA: 0x000025BB File Offset: 0x000007BB
		public static bool IsEmptyOrNull<T>(List<T> list)
		{
			return list == null || list.Count == 0;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x000025CB File Offset: 0x000007CB
		public static bool IsEmptyOrNull(IEmpty o)
		{
			return o == null || o.IsEmpty();
		}

		// Token: 0x06000042 RID: 66 RVA: 0x000025D8 File Offset: 0x000007D8
		public static bool IsEmptyOrNullOrContainsOnlyEmpty(List<string> list)
		{
			return list == null || list.Count == 0 || list.TrueForAll(new Predicate<string>(EmptyPredicates.IsEmptyOrNull));
		}

		// Token: 0x06000043 RID: 67 RVA: 0x000025F9 File Offset: 0x000007F9
		public static bool IsEmptyOrContainsOnlyEmpty(List<string> list)
		{
			return list.Count == 0 || list.TrueForAll(new Predicate<string>(EmptyPredicates.IsEmptyOrNull));
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002617 File Offset: 0x00000817
		public static T NewIfNull<T>(T value) where T : new()
		{
			if (value == null)
			{
				return new T();
			}
			return value;
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002628 File Offset: 0x00000828
		public static string NewIfNull(string value)
		{
			if (value != null)
			{
				return value;
			}
			return "";
		}
	}
}
