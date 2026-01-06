using System;
using Unity;

namespace System.Data.SqlClient
{
	// Token: 0x020003E7 RID: 999
	public class SqlColumnEncryptionCertificateStoreProvider : SqlColumnEncryptionKeyStoreProvider
	{
		// Token: 0x06002F78 RID: 12152 RVA: 0x0000E24C File Offset: 0x0000C44C
		public SqlColumnEncryptionCertificateStoreProvider()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x06002F79 RID: 12153 RVA: 0x0005503D File Offset: 0x0005323D
		public override byte[] DecryptColumnEncryptionKey(string masterKeyPath, string encryptionAlgorithm, byte[] encryptedColumnEncryptionKey)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		// Token: 0x06002F7A RID: 12154 RVA: 0x0005503D File Offset: 0x0005323D
		public override byte[] EncryptColumnEncryptionKey(string masterKeyPath, string encryptionAlgorithm, byte[] columnEncryptionKey)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		// Token: 0x06002F7B RID: 12155 RVA: 0x0005503D File Offset: 0x0005323D
		public override byte[] SignColumnMasterKeyMetadata(string masterKeyPath, bool allowEnclaveComputations)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		// Token: 0x06002F7C RID: 12156 RVA: 0x000CB420 File Offset: 0x000C9620
		public override bool VerifyColumnMasterKeyMetadata(string masterKeyPath, bool allowEnclaveComputations, byte[] signature)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		// Token: 0x04001CA6 RID: 7334
		public const string ProviderName = "MSSQL_CERTIFICATE_STORE";
	}
}
