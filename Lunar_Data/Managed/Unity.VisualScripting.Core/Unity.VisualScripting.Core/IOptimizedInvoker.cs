using System;

namespace Unity.VisualScripting
{
	// Token: 0x0200010C RID: 268
	public interface IOptimizedInvoker
	{
		// Token: 0x060006F3 RID: 1779
		void Compile();

		// Token: 0x060006F4 RID: 1780
		object Invoke(object target);

		// Token: 0x060006F5 RID: 1781
		object Invoke(object target, object arg0);

		// Token: 0x060006F6 RID: 1782
		object Invoke(object target, object arg0, object arg1);

		// Token: 0x060006F7 RID: 1783
		object Invoke(object target, object arg0, object arg1, object arg2);

		// Token: 0x060006F8 RID: 1784
		object Invoke(object target, object arg0, object arg1, object arg2, object arg3);

		// Token: 0x060006F9 RID: 1785
		object Invoke(object target, object arg0, object arg1, object arg2, object arg3, object arg4);

		// Token: 0x060006FA RID: 1786
		object Invoke(object target, params object[] args);
	}
}
