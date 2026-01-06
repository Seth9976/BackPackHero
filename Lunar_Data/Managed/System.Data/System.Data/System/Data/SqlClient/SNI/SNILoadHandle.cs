using System;
using System.Threading;

namespace System.Data.SqlClient.SNI
{
	// Token: 0x02000240 RID: 576
	internal class SNILoadHandle
	{
		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x06001A4F RID: 6735 RVA: 0x00083830 File Offset: 0x00081A30
		// (set) Token: 0x06001A50 RID: 6736 RVA: 0x0008383D File Offset: 0x00081A3D
		public SNIError LastError
		{
			get
			{
				return this._lastError.Value;
			}
			set
			{
				this._lastError.Value = value;
			}
		}

		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x06001A51 RID: 6737 RVA: 0x0008384B File Offset: 0x00081A4B
		public uint Status
		{
			get
			{
				return this._status;
			}
		}

		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x06001A52 RID: 6738 RVA: 0x00083853 File Offset: 0x00081A53
		public EncryptionOptions Options
		{
			get
			{
				return this._encryptionOption;
			}
		}

		// Token: 0x040012F1 RID: 4849
		public static readonly SNILoadHandle SingletonInstance = new SNILoadHandle();

		// Token: 0x040012F2 RID: 4850
		public readonly EncryptionOptions _encryptionOption;

		// Token: 0x040012F3 RID: 4851
		public ThreadLocal<SNIError> _lastError = new ThreadLocal<SNIError>(() => new SNIError(SNIProviders.INVALID_PROV, 0U, 0U, string.Empty));

		// Token: 0x040012F4 RID: 4852
		private readonly uint _status;
	}
}
