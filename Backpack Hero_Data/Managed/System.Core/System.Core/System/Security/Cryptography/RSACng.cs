using System;

namespace System.Security.Cryptography
{
	// Token: 0x02000054 RID: 84
	public sealed class RSACng : RSA
	{
		// Token: 0x060001AE RID: 430 RVA: 0x00003EF3 File Offset: 0x000020F3
		public RSACng()
			: this(2048)
		{
		}

		// Token: 0x060001AF RID: 431 RVA: 0x00003F00 File Offset: 0x00002100
		public RSACng(int keySize)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x00003F00 File Offset: 0x00002100
		public RSACng(CngKey key)
		{
			throw new NotImplementedException();
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060001B1 RID: 433 RVA: 0x000023CA File Offset: 0x000005CA
		// (set) Token: 0x060001B2 RID: 434 RVA: 0x000023CA File Offset: 0x000005CA
		public CngKey Key
		{
			[SecuritySafeCritical]
			get
			{
				throw new NotImplementedException();
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x000023CA File Offset: 0x000005CA
		public override RSAParameters ExportParameters(bool includePrivateParameters)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x000023CA File Offset: 0x000005CA
		public override void ImportParameters(RSAParameters parameters)
		{
			throw new NotImplementedException();
		}
	}
}
