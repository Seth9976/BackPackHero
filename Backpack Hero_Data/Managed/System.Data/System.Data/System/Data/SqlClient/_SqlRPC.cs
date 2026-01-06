using System;

namespace System.Data.SqlClient
{
	// Token: 0x02000217 RID: 535
	internal sealed class _SqlRPC
	{
		// Token: 0x060018E1 RID: 6369 RVA: 0x0007DB39 File Offset: 0x0007BD39
		internal string GetCommandTextOrRpcName()
		{
			if (10 == this.ProcID)
			{
				return (string)this.parameters[0].Value;
			}
			return this.rpcName;
		}

		// Token: 0x040011FA RID: 4602
		internal string rpcName;

		// Token: 0x040011FB RID: 4603
		internal ushort ProcID;

		// Token: 0x040011FC RID: 4604
		internal ushort options;

		// Token: 0x040011FD RID: 4605
		internal SqlParameter[] parameters;

		// Token: 0x040011FE RID: 4606
		internal byte[] paramoptions;

		// Token: 0x040011FF RID: 4607
		internal int? recordsAffected;

		// Token: 0x04001200 RID: 4608
		internal int cumulativeRecordsAffected;

		// Token: 0x04001201 RID: 4609
		internal int errorsIndexStart;

		// Token: 0x04001202 RID: 4610
		internal int errorsIndexEnd;

		// Token: 0x04001203 RID: 4611
		internal SqlErrorCollection errors;

		// Token: 0x04001204 RID: 4612
		internal int warningsIndexStart;

		// Token: 0x04001205 RID: 4613
		internal int warningsIndexEnd;

		// Token: 0x04001206 RID: 4614
		internal SqlErrorCollection warnings;
	}
}
