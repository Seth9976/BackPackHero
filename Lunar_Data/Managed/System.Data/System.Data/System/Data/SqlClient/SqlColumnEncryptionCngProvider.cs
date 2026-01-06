using System;
using Unity;

namespace System.Data.SqlClient
{
	// Token: 0x020003E8 RID: 1000
	public class SqlColumnEncryptionCngProvider : SqlColumnEncryptionKeyStoreProvider
	{
		// Token: 0x06002F7D RID: 12157 RVA: 0x0000E24C File Offset: 0x0000C44C
		public SqlColumnEncryptionCngProvider()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x06002F7E RID: 12158 RVA: 0x0005503D File Offset: 0x0005323D
		public override byte[] DecryptColumnEncryptionKey(string masterKeyPath, string encryptionAlgorithm, byte[] encryptedColumnEncryptionKey)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		// Token: 0x06002F7F RID: 12159 RVA: 0x0005503D File Offset: 0x0005323D
		public override byte[] EncryptColumnEncryptionKey(string masterKeyPath, string encryptionAlgorithm, byte[] columnEncryptionKey)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		// Token: 0x06002F80 RID: 12160 RVA: 0x0005503D File Offset: 0x0005323D
		public override byte[] SignColumnMasterKeyMetadata(string masterKeyPath, bool allowEnclaveComputations)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		// Token: 0x06002F81 RID: 12161 RVA: 0x000CB43C File Offset: 0x000C963C
		public override bool VerifyColumnMasterKeyMetadata(string masterKeyPath, bool allowEnclaveComputations, byte[] signature)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		// Token: 0x04001CA7 RID: 7335
		public const string ProviderName = "MSSQL_CNG_STORE";
	}
}
