using System;
using System.Data.Common;

namespace System.Data.OleDb
{
	/// <summary>Provides data for the <see cref="E:System.Data.OleDb.OleDbDataAdapter.RowUpdating" /> event. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000120 RID: 288
	[MonoTODO("OleDb is not implemented.")]
	public sealed class OleDbRowUpdatingEventArgs : RowUpdatingEventArgs
	{
		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x06000FCF RID: 4047 RVA: 0x0004EFE0 File Offset: 0x0004D1E0
		// (set) Token: 0x06000FD0 RID: 4048 RVA: 0x000094D4 File Offset: 0x000076D4
		protected override IDbCommand BaseCommand
		{
			get
			{
				throw ADP.OleDb();
			}
			set
			{
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Data.OleDb.OleDbCommand" /> to execute when performing the <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" />.</summary>
		/// <returns>The <see cref="T:System.Data.OleDb.OleDbCommand" /> to execute when performing the <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x06000FD1 RID: 4049 RVA: 0x0004EFE0 File Offset: 0x0004D1E0
		// (set) Token: 0x06000FD2 RID: 4050 RVA: 0x000094D4 File Offset: 0x000076D4
		public new OleDbCommand Command
		{
			get
			{
				throw ADP.OleDb();
			}
			set
			{
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.OleDb.OleDbRowUpdatingEventArgs" /> class.</summary>
		/// <param name="dataRow">The <see cref="T:System.Data.DataRow" /> to <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" />. </param>
		/// <param name="command">The <see cref="T:System.Data.IDbCommand" /> to execute during <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" />. </param>
		/// <param name="statementType">One of the <see cref="T:System.Data.StatementType" /> values that specifies the type of query executed. </param>
		/// <param name="tableMapping">The <see cref="T:System.Data.Common.DataTableMapping" /> sent through an <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" />. </param>
		// Token: 0x06000FD3 RID: 4051 RVA: 0x0004F215 File Offset: 0x0004D415
		public OleDbRowUpdatingEventArgs(DataRow dataRow, IDbCommand command, StatementType statementType, DataTableMapping tableMapping)
			: base(null, null, StatementType.Select, null)
		{
			throw ADP.OleDb();
		}
	}
}
