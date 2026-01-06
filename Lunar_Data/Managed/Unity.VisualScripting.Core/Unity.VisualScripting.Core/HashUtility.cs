using System;

namespace Unity.VisualScripting
{
	// Token: 0x02000153 RID: 339
	public static class HashUtility
	{
		// Token: 0x06000913 RID: 2323 RVA: 0x000279BE File Offset: 0x00025BBE
		public static int GetHashCode<T>(T a)
		{
			if (a == null)
			{
				return 0;
			}
			return a.GetHashCode();
		}

		// Token: 0x06000914 RID: 2324 RVA: 0x000279D7 File Offset: 0x00025BD7
		public static int GetHashCode<T1, T2>(T1 a, T2 b)
		{
			return (17 * 23 + ((a != null) ? a.GetHashCode() : 0)) * 23 + ((b != null) ? b.GetHashCode() : 0);
		}

		// Token: 0x06000915 RID: 2325 RVA: 0x00027A14 File Offset: 0x00025C14
		public static int GetHashCode<T1, T2, T3>(T1 a, T2 b, T3 c)
		{
			return ((17 * 23 + ((a != null) ? a.GetHashCode() : 0)) * 23 + ((b != null) ? b.GetHashCode() : 0)) * 23 + ((c != null) ? c.GetHashCode() : 0);
		}

		// Token: 0x06000916 RID: 2326 RVA: 0x00027A78 File Offset: 0x00025C78
		public static int GetHashCode<T1, T2, T3, T4>(T1 a, T2 b, T3 c, T4 d)
		{
			return (((17 * 23 + ((a != null) ? a.GetHashCode() : 0)) * 23 + ((b != null) ? b.GetHashCode() : 0)) * 23 + ((c != null) ? c.GetHashCode() : 0)) * 23 + ((d != null) ? d.GetHashCode() : 0);
		}

		// Token: 0x06000917 RID: 2327 RVA: 0x00027AF8 File Offset: 0x00025CF8
		public static int GetHashCode<T1, T2, T3, T4, T5>(T1 a, T2 b, T3 c, T4 d, T5 e)
		{
			return ((((17 * 23 + ((a != null) ? a.GetHashCode() : 0)) * 23 + ((b != null) ? b.GetHashCode() : 0)) * 23 + ((c != null) ? c.GetHashCode() : 0)) * 23 + ((d != null) ? d.GetHashCode() : 0)) * 23 + ((e != null) ? e.GetHashCode() : 0);
		}

		// Token: 0x06000918 RID: 2328 RVA: 0x00027B94 File Offset: 0x00025D94
		public static int GetHashCodeAlloc(params object[] values)
		{
			int num = 17;
			foreach (object obj in values)
			{
				num = num * 23 + ((obj != null) ? obj.GetHashCode() : 0);
			}
			return num;
		}
	}
}
