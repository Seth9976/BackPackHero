using System;
using System.Security.Cryptography;
using Unity;

namespace System.Data.SqlClient
{
	// Token: 0x020003EA RID: 1002
	public abstract class SqlColumnEncryptionEnclaveProvider
	{
		// Token: 0x06002F87 RID: 12167 RVA: 0x0000E24C File Offset: 0x0000C44C
		protected SqlColumnEncryptionEnclaveProvider()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x06002F88 RID: 12168
		public abstract void CreateEnclaveSession(byte[] enclaveAttestationInfo, ECDiffieHellmanCng clientDiffieHellmanKey, string attestationUrl, string servername, out SqlEnclaveSession sqlEnclaveSession, out long counter);

		// Token: 0x06002F89 RID: 12169
		public abstract SqlEnclaveAttestationParameters GetAttestationParameters();

		// Token: 0x06002F8A RID: 12170
		public abstract void GetEnclaveSession(string serverName, string attestationUrl, out SqlEnclaveSession sqlEnclaveSession, out long counter);

		// Token: 0x06002F8B RID: 12171
		public abstract void InvalidateEnclaveSession(string serverName, string enclaveAttestationUrl, SqlEnclaveSession enclaveSession);
	}
}
