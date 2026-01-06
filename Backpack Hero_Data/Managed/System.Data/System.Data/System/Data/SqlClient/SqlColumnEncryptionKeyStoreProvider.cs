using System;
using Unity;

namespace System.Data.SqlClient
{
	// Token: 0x020003E0 RID: 992
	public abstract class SqlColumnEncryptionKeyStoreProvider
	{
		// Token: 0x06002F4A RID: 12106 RVA: 0x0000E24C File Offset: 0x0000C44C
		protected SqlColumnEncryptionKeyStoreProvider()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x06002F4B RID: 12107
		public abstract byte[] DecryptColumnEncryptionKey(string masterKeyPath, string encryptionAlgorithm, byte[] encryptedColumnEncryptionKey);

		// Token: 0x06002F4C RID: 12108
		public abstract byte[] EncryptColumnEncryptionKey(string masterKeyPath, string encryptionAlgorithm, byte[] columnEncryptionKey);

		// Token: 0x06002F4D RID: 12109 RVA: 0x0005503D File Offset: 0x0005323D
		public virtual byte[] SignColumnMasterKeyMetadata(string masterKeyPath, bool allowEnclaveComputations)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		// Token: 0x06002F4E RID: 12110 RVA: 0x000CB298 File Offset: 0x000C9498
		public virtual bool VerifyColumnMasterKeyMetadata(string masterKeyPath, bool allowEnclaveComputations, byte[] signature)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}
	}
}
