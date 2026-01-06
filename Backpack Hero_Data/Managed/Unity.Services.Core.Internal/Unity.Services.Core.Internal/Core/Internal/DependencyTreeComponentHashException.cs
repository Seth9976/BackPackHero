using System;

namespace Unity.Services.Core.Internal
{
	// Token: 0x0200003E RID: 62
	internal class DependencyTreeComponentHashException : HashException
	{
		// Token: 0x06000113 RID: 275 RVA: 0x000035C3 File Offset: 0x000017C3
		public DependencyTreeComponentHashException(int hash)
			: base(hash)
		{
		}

		// Token: 0x06000114 RID: 276 RVA: 0x000035CC File Offset: 0x000017CC
		public DependencyTreeComponentHashException(int hash, string message)
			: base(hash, message)
		{
		}

		// Token: 0x06000115 RID: 277 RVA: 0x000035D6 File Offset: 0x000017D6
		public DependencyTreeComponentHashException(int hash, string message, Exception inner)
			: base(hash, message, inner)
		{
		}
	}
}
