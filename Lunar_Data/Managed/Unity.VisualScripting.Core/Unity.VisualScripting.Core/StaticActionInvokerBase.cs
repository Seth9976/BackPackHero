using System;
using System.Reflection;

namespace Unity.VisualScripting
{
	// Token: 0x02000111 RID: 273
	public abstract class StaticActionInvokerBase : StaticInvokerBase
	{
		// Token: 0x0600072E RID: 1838 RVA: 0x0002118B File Offset: 0x0001F38B
		protected StaticActionInvokerBase(MethodInfo methodInfo)
			: base(methodInfo)
		{
		}
	}
}
