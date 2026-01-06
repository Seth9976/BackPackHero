using System;
using System.Data.Common;

namespace System.Data.Odbc
{
	/// <summary>Provides data for the <see cref="E:System.Data.Odbc.OdbcDataAdapter.RowUpdating" /> event.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020002A7 RID: 679
	public sealed class OdbcRowUpdatingEventArgs : RowUpdatingEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Odbc.OdbcRowUpdatingEventArgs" /> class.</summary>
		/// <param name="row">The <see cref="T:System.Data.DataRow" /> to update. </param>
		/// <param name="command">The <see cref="T:System.Data.Odbc.OdbcCommand" /> to execute during the update operation. </param>
		/// <param name="statementType">One of the <see cref="T:System.Data.StatementType" /> values that specifies the type of query executed. </param>
		/// <param name="tableMapping">The <see cref="T:System.Data.Common.DataTableMapping" /> sent through <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" />. </param>
		// Token: 0x06001DDA RID: 7642 RVA: 0x0006E7BE File Offset: 0x0006C9BE
		public OdbcRowUpdatingEventArgs(DataRow row, IDbCommand command, StatementType statementType, DataTableMapping tableMapping)
			: base(row, command, statementType, tableMapping)
		{
		}

		/// <summary>Gets or sets the <see cref="T:System.Data.Odbc.OdbcCommand" /> to execute when <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" /> is called.</summary>
		/// <returns>The <see cref="T:System.Data.Odbc.OdbcCommand" /> to execute when <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" /> is called.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700056A RID: 1386
		// (get) Token: 0x06001DDB RID: 7643 RVA: 0x000920E3 File Offset: 0x000902E3
		// (set) Token: 0x06001DDC RID: 7644 RVA: 0x0006E7D8 File Offset: 0x0006C9D8
		public new OdbcCommand Command
		{
			get
			{
				return base.Command as OdbcCommand;
			}
			set
			{
				base.Command = value;
			}
		}

		// Token: 0x1700056B RID: 1387
		// (get) Token: 0x06001DDD RID: 7645 RVA: 0x0006E7E1 File Offset: 0x0006C9E1
		// (set) Token: 0x06001DDE RID: 7646 RVA: 0x000920F0 File Offset: 0x000902F0
		protected override IDbCommand BaseCommand
		{
			get
			{
				return base.BaseCommand;
			}
			set
			{
				base.BaseCommand = value as OdbcCommand;
			}
		}
	}
}
