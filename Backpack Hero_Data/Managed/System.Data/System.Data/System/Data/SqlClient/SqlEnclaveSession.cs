using System;
using System.Runtime.CompilerServices;
using Unity;

namespace System.Data.SqlClient
{
	// Token: 0x020003EB RID: 1003
	public class SqlEnclaveSession
	{
		// Token: 0x06002F8C RID: 12172 RVA: 0x0000E24C File Offset: 0x0000C44C
		public SqlEnclaveSession(byte[] sessionKey, long sessionId)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x170007E7 RID: 2023
		// (get) Token: 0x06002F8D RID: 12173 RVA: 0x000CB474 File Offset: 0x000C9674
		public long SessionId
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0L;
			}
		}

		// Token: 0x06002F8E RID: 12174 RVA: 0x0005503D File Offset: 0x0005323D
		public byte[] GetSessionKey()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}
	}
}
