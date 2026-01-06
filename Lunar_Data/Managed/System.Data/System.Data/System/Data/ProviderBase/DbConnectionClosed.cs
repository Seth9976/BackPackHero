using System;
using System.Data.Common;
using System.Threading.Tasks;
using System.Transactions;

namespace System.Data.ProviderBase
{
	// Token: 0x020002F4 RID: 756
	internal abstract class DbConnectionClosed : DbConnectionInternal
	{
		// Token: 0x06002258 RID: 8792 RVA: 0x0009EBB7 File Offset: 0x0009CDB7
		protected DbConnectionClosed(ConnectionState state, bool hidePassword, bool allowSetConnectionString)
			: base(state, hidePassword, allowSetConnectionString)
		{
		}

		// Token: 0x170005D9 RID: 1497
		// (get) Token: 0x06002259 RID: 8793 RVA: 0x0009EBC2 File Offset: 0x0009CDC2
		public override string ServerVersion
		{
			get
			{
				throw ADP.ClosedConnectionError();
			}
		}

		// Token: 0x0600225A RID: 8794 RVA: 0x0009EBC2 File Offset: 0x0009CDC2
		public override DbTransaction BeginTransaction(IsolationLevel il)
		{
			throw ADP.ClosedConnectionError();
		}

		// Token: 0x0600225B RID: 8795 RVA: 0x0009EBC2 File Offset: 0x0009CDC2
		public override void ChangeDatabase(string database)
		{
			throw ADP.ClosedConnectionError();
		}

		// Token: 0x0600225C RID: 8796 RVA: 0x000094D4 File Offset: 0x000076D4
		internal override void CloseConnection(DbConnection owningObject, DbConnectionFactory connectionFactory)
		{
		}

		// Token: 0x0600225D RID: 8797 RVA: 0x0009EBC9 File Offset: 0x0009CDC9
		protected override void Deactivate()
		{
			ADP.ClosedConnectionError();
		}

		// Token: 0x0600225E RID: 8798 RVA: 0x0009EBC2 File Offset: 0x0009CDC2
		protected internal override DataTable GetSchema(DbConnectionFactory factory, DbConnectionPoolGroup poolGroup, DbConnection outerConnection, string collectionName, string[] restrictions)
		{
			throw ADP.ClosedConnectionError();
		}

		// Token: 0x0600225F RID: 8799 RVA: 0x0009EBC2 File Offset: 0x0009CDC2
		protected override DbReferenceCollection CreateReferenceCollection()
		{
			throw ADP.ClosedConnectionError();
		}

		// Token: 0x06002260 RID: 8800 RVA: 0x0006A8D6 File Offset: 0x00068AD6
		internal override bool TryOpenConnection(DbConnection outerConnection, DbConnectionFactory connectionFactory, TaskCompletionSource<DbConnectionInternal> retry, DbConnectionOptions userOptions)
		{
			return base.TryOpenConnectionInternal(outerConnection, connectionFactory, retry, userOptions);
		}

		// Token: 0x06002261 RID: 8801 RVA: 0x0009EBC2 File Offset: 0x0009CDC2
		protected override void Activate(Transaction transaction)
		{
			throw ADP.ClosedConnectionError();
		}

		// Token: 0x06002262 RID: 8802 RVA: 0x0009EBC2 File Offset: 0x0009CDC2
		public override void EnlistTransaction(Transaction transaction)
		{
			throw ADP.ClosedConnectionError();
		}
	}
}
