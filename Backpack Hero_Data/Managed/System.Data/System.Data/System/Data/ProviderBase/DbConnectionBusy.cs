using System;
using System.Data.Common;
using System.Threading.Tasks;

namespace System.Data.ProviderBase
{
	// Token: 0x020002F5 RID: 757
	internal abstract class DbConnectionBusy : DbConnectionClosed
	{
		// Token: 0x06002263 RID: 8803 RVA: 0x0009EBD1 File Offset: 0x0009CDD1
		protected DbConnectionBusy(ConnectionState state)
			: base(state, true, false)
		{
		}

		// Token: 0x06002264 RID: 8804 RVA: 0x0009EBDC File Offset: 0x0009CDDC
		internal override bool TryOpenConnection(DbConnection outerConnection, DbConnectionFactory connectionFactory, TaskCompletionSource<DbConnectionInternal> retry, DbConnectionOptions userOptions)
		{
			throw ADP.ConnectionAlreadyOpen(base.State);
		}
	}
}
