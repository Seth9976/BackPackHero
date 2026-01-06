using System;

namespace Unity.Burst.CompilerServices
{
	// Token: 0x02000025 RID: 37
	public static class Hint
	{
		// Token: 0x06000137 RID: 311 RVA: 0x0000792C File Offset: 0x00005B2C
		public static bool Likely(bool condition)
		{
			return condition;
		}

		// Token: 0x06000138 RID: 312 RVA: 0x0000792F File Offset: 0x00005B2F
		public static bool Unlikely(bool condition)
		{
			return condition;
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00007932 File Offset: 0x00005B32
		public static void Assume(bool condition)
		{
		}
	}
}
