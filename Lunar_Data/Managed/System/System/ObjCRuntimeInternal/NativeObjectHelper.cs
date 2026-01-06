using System;

namespace ObjCRuntimeInternal
{
	// Token: 0x02000116 RID: 278
	internal static class NativeObjectHelper
	{
		// Token: 0x060006AA RID: 1706 RVA: 0x000124FA File Offset: 0x000106FA
		public static IntPtr GetHandle(this INativeObject self)
		{
			if (self != null)
			{
				return self.Handle;
			}
			return IntPtr.Zero;
		}
	}
}
