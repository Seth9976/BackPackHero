using System;

namespace Unity
{
	// Token: 0x0200006E RID: 110
	internal sealed class ThrowStub : ObjectDisposedException
	{
		// Token: 0x060003EE RID: 1006 RVA: 0x000027DD File Offset: 0x000009DD
		public static void ThrowNotSupportedException()
		{
			throw new PlatformNotSupportedException();
		}
	}
}
