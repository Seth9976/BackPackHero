using System;
using System.Data.SqlClient.SNI;

namespace System.Data.SqlClient
{
	// Token: 0x02000228 RID: 552
	internal sealed class TdsParserStateObjectFactory
	{
		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x060019AE RID: 6574 RVA: 0x0000CD07 File Offset: 0x0000AF07
		public static bool UseManagedSNI
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x060019AF RID: 6575 RVA: 0x000817F5 File Offset: 0x0007F9F5
		public EncryptionOptions EncryptionOptions
		{
			get
			{
				return SNILoadHandle.SingletonInstance.Options;
			}
		}

		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x060019B0 RID: 6576 RVA: 0x00081801 File Offset: 0x0007FA01
		public uint SNIStatus
		{
			get
			{
				return SNILoadHandle.SingletonInstance.Status;
			}
		}

		// Token: 0x060019B1 RID: 6577 RVA: 0x0008180D File Offset: 0x0007FA0D
		public TdsParserStateObject CreateTdsParserStateObject(TdsParser parser)
		{
			return new TdsParserStateObjectManaged(parser);
		}

		// Token: 0x060019B2 RID: 6578 RVA: 0x00081815 File Offset: 0x0007FA15
		internal TdsParserStateObject CreateSessionObject(TdsParser tdsParser, TdsParserStateObject _pMarsPhysicalConObj, bool v)
		{
			return new TdsParserStateObjectManaged(tdsParser, _pMarsPhysicalConObj, true);
		}

		// Token: 0x04001286 RID: 4742
		public static readonly TdsParserStateObjectFactory Singleton = new TdsParserStateObjectFactory();
	}
}
