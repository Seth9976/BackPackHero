using System;

namespace Unity.Services.Core.Internal
{
	// Token: 0x0200003D RID: 61
	internal class DependencyTreePackageHashException : HashException
	{
		// Token: 0x06000110 RID: 272 RVA: 0x000035A5 File Offset: 0x000017A5
		public DependencyTreePackageHashException(int hash)
			: base(hash)
		{
		}

		// Token: 0x06000111 RID: 273 RVA: 0x000035AE File Offset: 0x000017AE
		public DependencyTreePackageHashException(int hash, string message)
			: base(hash, message)
		{
		}

		// Token: 0x06000112 RID: 274 RVA: 0x000035B8 File Offset: 0x000017B8
		public DependencyTreePackageHashException(int hash, string message, Exception inner)
			: base(hash, message, inner)
		{
		}
	}
}
