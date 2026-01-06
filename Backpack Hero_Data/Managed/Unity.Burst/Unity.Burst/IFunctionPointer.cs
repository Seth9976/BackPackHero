using System;

namespace Unity.Burst
{
	// Token: 0x02000013 RID: 19
	public interface IFunctionPointer
	{
		// Token: 0x060000A6 RID: 166
		[Obsolete("This method will be removed in a future version of Burst")]
		IFunctionPointer FromIntPtr(IntPtr ptr);
	}
}
