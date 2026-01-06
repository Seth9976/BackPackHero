using System;
using System.Data.Common;
using System.Threading.Tasks;

namespace System.Data.ProviderBase
{
	// Token: 0x020002FA RID: 762
	internal sealed class DbConnectionClosedPreviouslyOpened : DbConnectionClosed
	{
		// Token: 0x06002270 RID: 8816 RVA: 0x0009ECAB File Offset: 0x0009CEAB
		private DbConnectionClosedPreviouslyOpened()
			: base(ConnectionState.Closed, true, true)
		{
		}

		// Token: 0x06002271 RID: 8817 RVA: 0x0009EC2A File Offset: 0x0009CE2A
		internal override bool TryReplaceConnection(DbConnection outerConnection, DbConnectionFactory connectionFactory, TaskCompletionSource<DbConnectionInternal> retry, DbConnectionOptions userOptions)
		{
			return this.TryOpenConnection(outerConnection, connectionFactory, retry, userOptions);
		}

		// Token: 0x04001720 RID: 5920
		internal static readonly DbConnectionInternal SingletonInstance = new DbConnectionClosedPreviouslyOpened();
	}
}
