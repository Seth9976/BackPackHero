using System;
using System.Reflection;

namespace Unity.VisualScripting
{
	// Token: 0x020000F9 RID: 249
	public abstract class InstanceActionInvokerBase<TTarget> : InstanceInvokerBase<TTarget>
	{
		// Token: 0x06000679 RID: 1657 RVA: 0x0001EC7A File Offset: 0x0001CE7A
		protected InstanceActionInvokerBase(MethodInfo methodInfo)
			: base(methodInfo)
		{
		}
	}
}
