using System;
using Unity;

namespace System.Data.SqlClient
{
	// Token: 0x020003E9 RID: 1001
	public class SqlColumnEncryptionCspProvider : SqlColumnEncryptionKeyStoreProvider
	{
		// Token: 0x06002F82 RID: 12162 RVA: 0x0000E24C File Offset: 0x0000C44C
		public SqlColumnEncryptionCspProvider()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x06002F83 RID: 12163 RVA: 0x0005503D File Offset: 0x0005323D
		public override byte[] DecryptColumnEncryptionKey(string masterKeyPath, string encryptionAlgorithm, byte[] encryptedColumnEncryptionKey)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		// Token: 0x06002F84 RID: 12164 RVA: 0x0005503D File Offset: 0x0005323D
		public override byte[] EncryptColumnEncryptionKey(string masterKeyPath, string encryptionAlgorithm, byte[] columnEncryptionKey)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		// Token: 0x06002F85 RID: 12165 RVA: 0x0005503D File Offset: 0x0005323D
		public override byte[] SignColumnMasterKeyMetadata(string masterKeyPath, bool allowEnclaveComputations)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		// Token: 0x06002F86 RID: 12166 RVA: 0x000CB458 File Offset: 0x000C9658
		public override bool VerifyColumnMasterKeyMetadata(string masterKeyPath, bool allowEnclaveComputations, byte[] signature)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		// Token: 0x04001CA8 RID: 7336
		public const string ProviderName = "MSSQL_CSP_PROVIDER";
	}
}
