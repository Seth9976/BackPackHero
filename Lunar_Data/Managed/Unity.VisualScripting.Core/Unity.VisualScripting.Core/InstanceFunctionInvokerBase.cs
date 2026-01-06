using System;
using System.Reflection;

namespace Unity.VisualScripting
{
	// Token: 0x02000101 RID: 257
	public abstract class InstanceFunctionInvokerBase<TTarget, TResult> : InstanceInvokerBase<TTarget>
	{
		// Token: 0x060006AC RID: 1708 RVA: 0x0001F6D2 File Offset: 0x0001D8D2
		protected InstanceFunctionInvokerBase(MethodInfo methodInfo)
			: base(methodInfo)
		{
			if (OptimizedReflection.safeMode && methodInfo.ReturnType != typeof(TResult))
			{
				throw new ArgumentException("Return type of method info doesn't match generic type.", "methodInfo");
			}
		}
	}
}
