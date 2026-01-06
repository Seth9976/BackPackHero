using System;

namespace Unity.VisualScripting.FullSerializer.Internal
{
	// Token: 0x020001AD RID: 429
	public static class fsOption
	{
		// Token: 0x06000B70 RID: 2928 RVA: 0x00030C72 File Offset: 0x0002EE72
		public static fsOption<T> Just<T>(T value)
		{
			return new fsOption<T>(value);
		}
	}
}
