using System;

namespace Unity.Burst.CompilerServices
{
	// Token: 0x02000026 RID: 38
	public static class Hint
	{
		// Token: 0x06000137 RID: 311 RVA: 0x00007964 File Offset: 0x00005B64
		public static bool Likely(bool condition)
		{
			return condition;
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00007967 File Offset: 0x00005B67
		public static bool Unlikely(bool condition)
		{
			return condition;
		}

		// Token: 0x06000139 RID: 313 RVA: 0x0000796A File Offset: 0x00005B6A
		public static void Assume(bool condition)
		{
		}
	}
}
