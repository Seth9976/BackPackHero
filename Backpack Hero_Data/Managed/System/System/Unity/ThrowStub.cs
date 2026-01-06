using System;

namespace Unity
{
	// Token: 0x02000879 RID: 2169
	internal sealed class ThrowStub : ObjectDisposedException
	{
		// Token: 0x060044C3 RID: 17603 RVA: 0x00011EB0 File Offset: 0x000100B0
		public static void ThrowNotSupportedException()
		{
			throw new PlatformNotSupportedException();
		}
	}
}
