using System;

namespace Unity.Services.Core
{
	// Token: 0x02000006 RID: 6
	public class ServicesInitializationException : Exception
	{
		// Token: 0x06000018 RID: 24 RVA: 0x00002204 File Offset: 0x00000404
		public ServicesInitializationException()
		{
		}

		// Token: 0x06000019 RID: 25 RVA: 0x0000220C File Offset: 0x0000040C
		public ServicesInitializationException(string message)
			: base(message)
		{
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002215 File Offset: 0x00000415
		public ServicesInitializationException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
