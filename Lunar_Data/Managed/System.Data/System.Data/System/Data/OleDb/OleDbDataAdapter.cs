using System;
using System.Data.Common;

namespace System.Data.OleDb
{
	/// <summary>Represents a set of data commands and a database connection that are used to fill the <see cref="T:System.Data.DataSet" /> and update the data source.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000110 RID: 272
	[MonoTODO("OleDb is not implemented.")]
	public sealed class OleDbDataAdapter : DbDataAdapter, IDataAdapter, IDbDataAdapter, ICloneable
	{
		/// <summary>Gets or sets an SQL statement or stored procedure for deleting records from the data set.</summary>
		/// <returns>An <see cref="T:System.Data.OleDb.OleDbCommand" /> used during <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" /> to delete records in the data source that correspond to deleted rows in the <see cref="T:System.Data.DataSet" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000281 RID: 641
		// (get) Token: 0x06000F10 RID: 3856 RVA: 0x0004EFE0 File Offset: 0x0004D1E0
		// (set) Token: 0x06000F11 RID: 3857 RVA: 0x000094D4 File Offset: 0x000076D4
		public new OleDbCommand DeleteCommand
		{
			get
			{
				throw ADP.OleDb();
			}
			set
			{
			}
		}

		/// <summary>Gets or sets an SQL statement or stored procedure used to insert new records into the data source.</summary>
		/// <returns>An <see cref="T:System.Data.OleDb.OleDbCommand" /> used during <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" /> to insert records in the data source that correspond to new rows in the <see cref="T:System.Data.DataSet" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000282 RID: 642
		// (get) Token: 0x06000F12 RID: 3858 RVA: 0x0004EFE0 File Offset: 0x0004D1E0
		// (set) Token: 0x06000F13 RID: 3859 RVA: 0x000094D4 File Offset: 0x000076D4
		public new OleDbCommand InsertCommand
		{
			get
			{
				throw ADP.OleDb();
			}
			set
			{
			}
		}

		/// <summary>Gets or sets an SQL statement or stored procedure used to select records in the data source.</summary>
		/// <returns>An <see cref="T:System.Data.OleDb.OleDbCommand" /> that is used during <see cref="M:System.Data.Common.DbDataAdapter.Fill(System.Data.DataSet)" /> to select records from data source for placement in the <see cref="T:System.Data.DataSet" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000283 RID: 643
		// (get) Token: 0x06000F14 RID: 3860 RVA: 0x0004EFE0 File Offset: 0x0004D1E0
		// (set) Token: 0x06000F15 RID: 3861 RVA: 0x000094D4 File Offset: 0x000076D4
		public new OleDbCommand SelectCommand
		{
			get
			{
				throw ADP.OleDb();
			}
			set
			{
			}
		}

		/// <summary>For a description of this member, see <see cref="M:System.Data.IDbDataAdapter.DeleteCommand" />.</summary>
		/// <returns>An <see cref="T:System.Data.IDbCommand" /> used during an update to delete records in the data source for deleted rows in the data set.</returns>
		// Token: 0x17000284 RID: 644
		// (get) Token: 0x06000F16 RID: 3862 RVA: 0x0004EFE0 File Offset: 0x0004D1E0
		// (set) Token: 0x06000F17 RID: 3863 RVA: 0x000094D4 File Offset: 0x000076D4
		IDbCommand IDbDataAdapter.DeleteCommand
		{
			get
			{
				throw ADP.OleDb();
			}
			set
			{
			}
		}

		/// <summary>For a description of this member, see <see cref="M:System.Data.IDbDataAdapter.InsertCommand" />.</summary>
		/// <returns>An <see cref="T:System.Data.IDbCommand" /> that is used during an update to insert records from a data source for placement in the data set.</returns>
		// Token: 0x17000285 RID: 645
		// (get) Token: 0x06000F18 RID: 3864 RVA: 0x0004EFE0 File Offset: 0x0004D1E0
		// (set) Token: 0x06000F19 RID: 3865 RVA: 0x000094D4 File Offset: 0x000076D4
		IDbCommand IDbDataAdapter.InsertCommand
		{
			get
			{
				throw ADP.OleDb();
			}
			set
			{
			}
		}

		/// <summary>For a description of this member, see <see cref="M:System.Data.IDbDataAdapter.SelectCommand" />.</summary>
		/// <returns>An <see cref="T:System.Data.IDbCommand" /> that is used during an update to select records from a data source for placement in the data set.</returns>
		// Token: 0x17000286 RID: 646
		// (get) Token: 0x06000F1A RID: 3866 RVA: 0x0004EFE0 File Offset: 0x0004D1E0
		// (set) Token: 0x06000F1B RID: 3867 RVA: 0x000094D4 File Offset: 0x000076D4
		IDbCommand IDbDataAdapter.SelectCommand
		{
			get
			{
				throw ADP.OleDb();
			}
			set
			{
			}
		}

		/// <summary>For a description of this member, see <see cref="M:System.Data.IDbDataAdapter.UpdateCommand" />.</summary>
		/// <returns>An <see cref="T:System.Data.IDbCommand" /> used during an update to update records in the data source for modified rows in the data set.</returns>
		// Token: 0x17000287 RID: 647
		// (get) Token: 0x06000F1C RID: 3868 RVA: 0x0004EFE0 File Offset: 0x0004D1E0
		// (set) Token: 0x06000F1D RID: 3869 RVA: 0x000094D4 File Offset: 0x000076D4
		IDbCommand IDbDataAdapter.UpdateCommand
		{
			get
			{
				throw ADP.OleDb();
			}
			set
			{
			}
		}

		/// <summary>Gets or sets an SQL statement or stored procedure used to update records in the data source.</summary>
		/// <returns>An <see cref="T:System.Data.OleDb.OleDbCommand" /> used during <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" /> to update records in the data source that correspond to modified rows in the <see cref="T:System.Data.DataSet" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000288 RID: 648
		// (get) Token: 0x06000F1E RID: 3870 RVA: 0x0004EFE0 File Offset: 0x0004D1E0
		// (set) Token: 0x06000F1F RID: 3871 RVA: 0x000094D4 File Offset: 0x000076D4
		public new OleDbCommand UpdateCommand
		{
			get
			{
				throw ADP.OleDb();
			}
			set
			{
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.OleDb.OleDbDataAdapter" /> class.</summary>
		// Token: 0x06000F20 RID: 3872 RVA: 0x0004F07E File Offset: 0x0004D27E
		public OleDbDataAdapter()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.OleDb.OleDbDataAdapter" /> class with the specified <see cref="T:System.Data.OleDb.OleDbCommand" /> as the <see cref="P:System.Data.OleDb.OleDbDataAdapter.SelectCommand" /> property.</summary>
		/// <param name="selectCommand">An <see cref="T:System.Data.OleDb.OleDbCommand" /> that is a SELECT statement or stored procedure, and is set as the <see cref="P:System.Data.OleDb.OleDbDataAdapter.SelectCommand" /> property of the <see cref="T:System.Data.OleDb.OleDbDataAdapter" />.</param>
		// Token: 0x06000F21 RID: 3873 RVA: 0x0004F086 File Offset: 0x0004D286
		public OleDbDataAdapter(OleDbCommand selectCommand)
		{
			throw ADP.OleDb();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.OleDb.OleDbDataAdapter" /> class with a <see cref="P:System.Data.OleDb.OleDbDataAdapter.SelectCommand" />.</summary>
		/// <param name="selectCommandText">A string that is an SQL SELECT statement or stored procedure to be used by the <see cref="P:System.Data.OleDb.OleDbDataAdapter.SelectCommand" /> property of the <see cref="T:System.Data.OleDb.OleDbDataAdapter" />. </param>
		/// <param name="selectConnection">An <see cref="T:System.Data.OleDb.OleDbConnection" /> that represents the connection. </param>
		// Token: 0x06000F22 RID: 3874 RVA: 0x0004F086 File Offset: 0x0004D286
		public OleDbDataAdapter(string selectCommandText, OleDbConnection selectConnection)
		{
			throw ADP.OleDb();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.OleDb.OleDbDataAdapter" /> class with a <see cref="P:System.Data.OleDb.OleDbDataAdapter.SelectCommand" />.</summary>
		/// <param name="selectCommandText">A string that is an SQL SELECT statement or stored procedure to be used by the <see cref="P:System.Data.OleDb.OleDbDataAdapter.SelectCommand" /> property of the <see cref="T:System.Data.OleDb.OleDbDataAdapter" />. </param>
		/// <param name="selectConnectionString">The connection string. </param>
		// Token: 0x06000F23 RID: 3875 RVA: 0x0004F086 File Offset: 0x0004D286
		public OleDbDataAdapter(string selectCommandText, string selectConnectionString)
		{
			throw ADP.OleDb();
		}

		// Token: 0x06000F24 RID: 3876 RVA: 0x0004EFE0 File Offset: 0x0004D1E0
		protected override RowUpdatedEventArgs CreateRowUpdatedEvent(DataRow dataRow, IDbCommand command, StatementType statementType, DataTableMapping tableMapping)
		{
			throw ADP.OleDb();
		}

		// Token: 0x06000F25 RID: 3877 RVA: 0x0004EFE0 File Offset: 0x0004D1E0
		protected override RowUpdatingEventArgs CreateRowUpdatingEvent(DataRow dataRow, IDbCommand command, StatementType statementType, DataTableMapping tableMapping)
		{
			throw ADP.OleDb();
		}

		/// <summary>Adds or refreshes rows in the <see cref="T:System.Data.DataSet" /> to match those in an ADO Recordset or Record object using the specified <see cref="T:System.Data.DataSet" />, ADO object, and source table name.</summary>
		/// <returns>The number of rows successfully added to or refreshed in the <see cref="T:System.Data.DataSet" />. This does not include rows affected by statements that do not return rows.</returns>
		/// <param name="dataSet">A <see cref="T:System.Data.DataSet" /> to fill with records and, if it is required, schema. </param>
		/// <param name="ADODBRecordSet">An ADO Recordset or Record object. </param>
		/// <param name="srcTable">The source table used for the table mappings. </param>
		/// <exception cref="T:System.SystemException">The source table is invalid. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000F26 RID: 3878 RVA: 0x0004EFE0 File Offset: 0x0004D1E0
		public int Fill(DataSet dataSet, object ADODBRecordSet, string srcTable)
		{
			throw ADP.OleDb();
		}

		/// <summary>Adds or refreshes rows in a <see cref="T:System.Data.DataTable" /> to match those in an ADO Recordset or Record object using the specified <see cref="T:System.Data.DataTable" /> and ADO objects.</summary>
		/// <returns>The number of rows successfully refreshed to the <see cref="T:System.Data.DataTable" />. This does not include rows affected by statements that do not return rows.</returns>
		/// <param name="dataTable">A <see cref="T:System.Data.DataTable" /> to fill with records and, if it is required, schema. </param>
		/// <param name="ADODBRecordSet">An ADO Recordset or Record object. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000F27 RID: 3879 RVA: 0x0004EFE0 File Offset: 0x0004D1E0
		public int Fill(DataTable dataTable, object ADODBRecordSet)
		{
			throw ADP.OleDb();
		}

		// Token: 0x06000F28 RID: 3880 RVA: 0x0004EFE0 File Offset: 0x0004D1E0
		protected override void OnRowUpdated(RowUpdatedEventArgs value)
		{
			throw ADP.OleDb();
		}

		// Token: 0x06000F29 RID: 3881 RVA: 0x0004EFE0 File Offset: 0x0004D1E0
		protected override void OnRowUpdating(RowUpdatingEventArgs value)
		{
			throw ADP.OleDb();
		}

		/// <summary>Occurs during <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" /> after a command is executed against the data source. The attempt to update is made. Therefore, the event occurs.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x14000021 RID: 33
		// (add) Token: 0x06000F2A RID: 3882 RVA: 0x0004F094 File Offset: 0x0004D294
		// (remove) Token: 0x06000F2B RID: 3883 RVA: 0x0004F0CC File Offset: 0x0004D2CC
		public event OleDbRowUpdatedEventHandler RowUpdated;

		/// <summary>Occurs during <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" /> before a command is executed against the data source. The attempt to update is made. Therefore, the event occurs.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x14000022 RID: 34
		// (add) Token: 0x06000F2C RID: 3884 RVA: 0x0004F104 File Offset: 0x0004D304
		// (remove) Token: 0x06000F2D RID: 3885 RVA: 0x0004F13C File Offset: 0x0004D33C
		public event OleDbRowUpdatingEventHandler RowUpdating;

		/// <summary>For a description of this member, see <see cref="M:System.ICloneable.Clone" />.</summary>
		/// <returns>A new <see cref="T:System.Object" /> that is a copy of this instance.</returns>
		// Token: 0x06000F2E RID: 3886 RVA: 0x0004EFE0 File Offset: 0x0004D1E0
		object ICloneable.Clone()
		{
			throw ADP.OleDb();
		}
	}
}
