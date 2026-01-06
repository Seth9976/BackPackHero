using System;

namespace Unity.Services.Core
{
	// Token: 0x02000008 RID: 8
	internal class UnityProjectNotLinkedException : ServicesInitializationException
	{
		// Token: 0x0600001B RID: 27 RVA: 0x0000221F File Offset: 0x0000041F
		public UnityProjectNotLinkedException()
		{
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002227 File Offset: 0x00000427
		public UnityProjectNotLinkedException(string message)
			: base(message)
		{
		}
	}
}
