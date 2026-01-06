using System;
using System.Data.Common;
using System.Data.ProviderBase;
using System.Transactions;

namespace System.Data.Odbc
{
	// Token: 0x020002AF RID: 687
	internal sealed class OdbcConnectionOpen : DbConnectionInternal
	{
		// Token: 0x06001E1A RID: 7706 RVA: 0x000930AC File Offset: 0x000912AC
		internal OdbcConnectionOpen(OdbcConnection outerConnection, OdbcConnectionString connectionOptions)
		{
			OdbcEnvironmentHandle globalEnvironmentHandle = OdbcEnvironment.GetGlobalEnvironmentHandle();
			outerConnection.ConnectionHandle = new OdbcConnectionHandle(outerConnection, connectionOptions, globalEnvironmentHandle);
		}

		// Token: 0x17000572 RID: 1394
		// (get) Token: 0x06001E1B RID: 7707 RVA: 0x000930D4 File Offset: 0x000912D4
		internal OdbcConnection OuterConnection
		{
			get
			{
				OdbcConnection odbcConnection = (OdbcConnection)base.Owner;
				if (odbcConnection == null)
				{
					throw ODBC.OpenConnectionNoOwner();
				}
				return odbcConnection;
			}
		}

		// Token: 0x17000573 RID: 1395
		// (get) Token: 0x06001E1C RID: 7708 RVA: 0x000930F7 File Offset: 0x000912F7
		public override string ServerVersion
		{
			get
			{
				return this.OuterConnection.Open_GetServerVersion();
			}
		}

		// Token: 0x06001E1D RID: 7709 RVA: 0x000094D4 File Offset: 0x000076D4
		protected override void Activate(Transaction transaction)
		{
		}

		// Token: 0x06001E1E RID: 7710 RVA: 0x00093104 File Offset: 0x00091304
		public override DbTransaction BeginTransaction(IsolationLevel isolevel)
		{
			return this.BeginOdbcTransaction(isolevel);
		}

		// Token: 0x06001E1F RID: 7711 RVA: 0x0009310D File Offset: 0x0009130D
		internal OdbcTransaction BeginOdbcTransaction(IsolationLevel isolevel)
		{
			return this.OuterConnection.Open_BeginTransaction(isolevel);
		}

		// Token: 0x06001E20 RID: 7712 RVA: 0x0009311B File Offset: 0x0009131B
		public override void ChangeDatabase(string value)
		{
			this.OuterConnection.Open_ChangeDatabase(value);
		}

		// Token: 0x06001E21 RID: 7713 RVA: 0x00093129 File Offset: 0x00091329
		protected override DbReferenceCollection CreateReferenceCollection()
		{
			return new OdbcReferenceCollection();
		}

		// Token: 0x06001E22 RID: 7714 RVA: 0x00093130 File Offset: 0x00091330
		protected override void Deactivate()
		{
			base.NotifyWeakReference(0);
		}

		// Token: 0x06001E23 RID: 7715 RVA: 0x000094D4 File Offset: 0x000076D4
		public override void EnlistTransaction(Transaction transaction)
		{
		}
	}
}
