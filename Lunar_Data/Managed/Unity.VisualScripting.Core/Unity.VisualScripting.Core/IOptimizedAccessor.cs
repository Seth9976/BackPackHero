using System;

namespace Unity.VisualScripting
{
	// Token: 0x0200010B RID: 267
	public interface IOptimizedAccessor
	{
		// Token: 0x060006F0 RID: 1776
		void Compile();

		// Token: 0x060006F1 RID: 1777
		object GetValue(object target);

		// Token: 0x060006F2 RID: 1778
		void SetValue(object target, object value);
	}
}
