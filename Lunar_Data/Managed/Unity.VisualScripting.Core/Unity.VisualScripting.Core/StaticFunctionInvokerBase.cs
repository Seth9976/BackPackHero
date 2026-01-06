using System;
using System.Reflection;

namespace Unity.VisualScripting
{
	// Token: 0x02000119 RID: 281
	public abstract class StaticFunctionInvokerBase<TResult> : StaticInvokerBase
	{
		// Token: 0x06000767 RID: 1895 RVA: 0x00021BF2 File Offset: 0x0001FDF2
		protected StaticFunctionInvokerBase(MethodInfo methodInfo)
			: base(methodInfo)
		{
			if (OptimizedReflection.safeMode && methodInfo.ReturnType != typeof(TResult))
			{
				throw new ArgumentException("Return type of method info doesn't match generic type.", "methodInfo");
			}
		}
	}
}
