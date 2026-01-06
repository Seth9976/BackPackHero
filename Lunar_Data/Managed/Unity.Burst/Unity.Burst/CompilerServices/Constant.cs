using System;
using System.Runtime.CompilerServices;

namespace Unity.Burst.CompilerServices
{
	// Token: 0x02000025 RID: 37
	public static class Constant
	{
		// Token: 0x06000135 RID: 309 RVA: 0x0000795E File Offset: 0x00005B5E
		public static bool IsConstantExpression<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(T t) where T : struct, ValueType
		{
			return false;
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00007961 File Offset: 0x00005B61
		public unsafe static bool IsConstantExpression(void* t)
		{
			return false;
		}
	}
}
