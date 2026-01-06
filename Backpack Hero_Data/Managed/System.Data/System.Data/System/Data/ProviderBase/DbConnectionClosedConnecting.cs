using System;
using System.Data.Common;
using System.Threading.Tasks;

namespace System.Data.ProviderBase
{
	// Token: 0x020002F8 RID: 760
	internal sealed class DbConnectionClosedConnecting : DbConnectionBusy
	{
		// Token: 0x06002269 RID: 8809 RVA: 0x0009EC13 File Offset: 0x0009CE13
		private DbConnectionClosedConnecting()
			: base(ConnectionState.Connecting)
		{
		}

		// Token: 0x0600226A RID: 8810 RVA: 0x0009EC1C File Offset: 0x0009CE1C
		internal override void CloseConnection(DbConnection owningObject, DbConnectionFactory connectionFactory)
		{
			connectionFactory.SetInnerConnectionTo(owningObject, DbConnectionClosedPreviouslyOpened.SingletonInstance);
		}

		// Token: 0x0600226B RID: 8811 RVA: 0x0009EC2A File Offset: 0x0009CE2A
		internal override bool TryReplaceConnection(DbConnection outerConnection, DbConnectionFactory connectionFactory, TaskCompletionSource<DbConnectionInternal> retry, DbConnectionOptions userOptions)
		{
			return this.TryOpenConnection(outerConnection, connectionFactory, retry, userOptions);
		}

		// Token: 0x0600226C RID: 8812 RVA: 0x0009EC38 File Offset: 0x0009CE38
		internal override bool TryOpenConnection(DbConnection outerConnection, DbConnectionFactory connectionFactory, TaskCompletionSource<DbConnectionInternal> retry, DbConnectionOptions userOptions)
		{
			if (retry == null || !retry.Task.IsCompleted)
			{
				throw ADP.ConnectionAlreadyOpen(base.State);
			}
			DbConnectionInternal result = retry.Task.Result;
			if (result == null)
			{
				connectionFactory.SetInnerConnectionTo(outerConnection, this);
				throw ADP.InternalConnectionError(ADP.ConnectionError.GetConnectionReturnsNull);
			}
			connectionFactory.SetInnerConnectionEvent(outerConnection, result);
			return true;
		}

		// Token: 0x0400171E RID: 5918
		internal static readonly DbConnectionInternal SingletonInstance = new DbConnectionClosedConnecting();
	}
}
