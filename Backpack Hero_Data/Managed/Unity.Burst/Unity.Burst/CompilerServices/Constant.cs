using System;
using System.Runtime.CompilerServices;

namespace Unity.Burst.CompilerServices
{
	// Token: 0x02000024 RID: 36
	public static class Constant
	{
		// Token: 0x06000135 RID: 309 RVA: 0x00007926 File Offset: 0x00005B26
		public static bool IsConstantExpression<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(T t) where T : struct, ValueType
		{
			return false;
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00007929 File Offset: 0x00005B29
		public unsafe static bool IsConstantExpression(void* t)
		{
			return false;
		}
	}
}
