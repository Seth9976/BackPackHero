using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using Unity;

namespace System.Data.SqlClient
{
	// Token: 0x020003EC RID: 1004
	public class SqlEnclaveAttestationParameters
	{
		// Token: 0x06002F8F RID: 12175 RVA: 0x0000E24C File Offset: 0x0000C44C
		public SqlEnclaveAttestationParameters(int protocol, byte[] input, ECDiffieHellmanCng clientDiffieHellmanKey)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x170007E8 RID: 2024
		// (get) Token: 0x06002F90 RID: 12176 RVA: 0x0005503D File Offset: 0x0005323D
		public ECDiffieHellmanCng ClientDiffieHellmanKey
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		// Token: 0x170007E9 RID: 2025
		// (get) Token: 0x06002F91 RID: 12177 RVA: 0x000CB490 File Offset: 0x000C9690
		public int Protocol
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		// Token: 0x06002F92 RID: 12178 RVA: 0x0005503D File Offset: 0x0005323D
		public byte[] GetInput()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}
	}
}
