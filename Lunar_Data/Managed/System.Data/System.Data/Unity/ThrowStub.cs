using System;

namespace Unity
{
	// Token: 0x020003ED RID: 1005
	internal sealed class ThrowStub : ObjectDisposedException
	{
		// Token: 0x06002F93 RID: 12179 RVA: 0x00039889 File Offset: 0x00037A89
		public static void ThrowNotSupportedException()
		{
			throw new PlatformNotSupportedException();
		}
	}
}
