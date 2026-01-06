using System;
using System.Data.Common;

namespace System.Data.SqlClient
{
	/// <summary>Represents a set of data commands and a database connection that are used to fill the <see cref="T:System.Data.DataSet" /> and update a SQL Server database. This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000193 RID: 403
	public sealed class SqlDataAdapter : DbDataAdapter, IDbDataAdapter, IDataAdapter, ICloneable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlClient.SqlDataAdapter" /> class.</summary>
		// Token: 0x0600138A RID: 5002 RVA: 0x0005EAA5 File Offset: 0x0005CCA5
		public SqlDataAdapter()
		{
			GC.SuppressFinalize(this);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlClient.SqlDataAdapter" /> class with the specified <see cref="T:System.Data.SqlClient.SqlCommand" /> as the <see cref="P:System.Data.SqlClient.SqlDataAdapter.SelectCommand" /> property.</summary>
		/// <param name="selectCommand">A <see cref="T:System.Data.SqlClient.SqlCommand" /> that is a Transact-SQL SELECT statement or stored procedure and is set as the <see cref="P:System.Data.SqlClient.SqlDataAdapter.SelectCommand" /> property of the <see cref="T:System.Data.SqlClient.SqlDataAdapter" />. </param>
		// Token: 0x0600138B RID: 5003 RVA: 0x0005EABA File Offset: 0x0005CCBA
		public SqlDataAdapter(SqlCommand selectCommand)
			: this()
		{
			this.SelectCommand = selectCommand;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlClient.SqlDataAdapter" /> class with a <see cref="P:System.Data.SqlClient.SqlDataAdapter.SelectCommand" /> and a connection string.</summary>
		/// <param name="selectCommandText">A <see cref="T:System.String" /> that is a Transact-SQL SELECT statement or stored procedure to be used by the <see cref="P:System.Data.SqlClient.SqlDataAdapter.SelectCommand" /> property of the <see cref="T:System.Data.SqlClient.SqlDataAdapter" />. </param>
		/// <param name="selectConnectionString">The connection string. If your connection string does not use Integrated Security = true, you can use <see cref="M:System.Data.SqlClient.SqlDataAdapter.#ctor(System.String,System.Data.SqlClient.SqlConnection)" /> and <see cref="T:System.Data.SqlClient.SqlCredential" /> to pass the user ID and password more securely than by specifying the user ID and password as text in the connection string.</param>
		// Token: 0x0600138C RID: 5004 RVA: 0x0005EACC File Offset: 0x0005CCCC
		public SqlDataAdapter(string selectCommandText, string selectConnectionString)
			: this()
		{
			SqlConnection sqlConnection = new SqlConnection(selectConnectionString);
			this.SelectCommand = new SqlCommand(selectCommandText, sqlConnection);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlClient.SqlDataAdapter" /> class with a <see cref="P:System.Data.SqlClient.SqlDataAdapter.SelectCommand" /> and a <see cref="T:System.Data.SqlClient.SqlConnection" /> object.</summary>
		/// <param name="selectCommandText">A <see cref="T:System.String" /> that is a Transact-SQL SELECT statement or stored procedure to be used by the <see cref="P:System.Data.SqlClient.SqlDataAdapter.SelectCommand" /> property of the <see cref="T:System.Data.SqlClient.SqlDataAdapter" />. </param>
		/// <param name="selectConnection">A <see cref="T:System.Data.SqlClient.SqlConnection" /> that represents the connection. If your connection string does not use Integrated Security = true, you can use <see cref="T:System.Data.SqlClient.SqlCredential" /> to pass the user ID and password more securely than by specifying the user ID and password as text in the connection string.</param>
		// Token: 0x0600138D RID: 5005 RVA: 0x0005EAF3 File Offset: 0x0005CCF3
		public SqlDataAdapter(string selectCommandText, SqlConnection selectConnection)
			: this()
		{
			this.SelectCommand = new SqlCommand(selectCommandText, selectConnection);
		}

		// Token: 0x0600138E RID: 5006 RVA: 0x0005EB08 File Offset: 0x0005CD08
		private SqlDataAdapter(SqlDataAdapter from)
			: base(from)
		{
			GC.SuppressFinalize(this);
		}

		/// <summary>Gets or sets a Transact-SQL statement or stored procedure to delete records from the data set.</summary>
		/// <returns>A <see cref="T:System.Data.SqlClient.SqlCommand" /> used during <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" /> to delete records in the database that correspond to deleted rows in the <see cref="T:System.Data.DataSet" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700039A RID: 922
		// (get) Token: 0x0600138F RID: 5007 RVA: 0x0005EB1E File Offset: 0x0005CD1E
		// (set) Token: 0x06001390 RID: 5008 RVA: 0x0005EB26 File Offset: 0x0005CD26
		public new SqlCommand DeleteCommand
		{
			get
			{
				return this._deleteCommand;
			}
			set
			{
				this._deleteCommand = value;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Data.IDbDataAdapter.DeleteCommand" />.</summary>
		/// <returns>An <see cref="T:System.Data.IdbCommandthatis" /> used during <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" /> to delete records in the data source for deleted rows in the data set.</returns>
		// Token: 0x1700039B RID: 923
		// (get) Token: 0x06001391 RID: 5009 RVA: 0x0005EB1E File Offset: 0x0005CD1E
		// (set) Token: 0x06001392 RID: 5010 RVA: 0x0005EB2F File Offset: 0x0005CD2F
		IDbCommand IDbDataAdapter.DeleteCommand
		{
			get
			{
				return this._deleteCommand;
			}
			set
			{
				this._deleteCommand = (SqlCommand)value;
			}
		}

		/// <summary>Gets or sets a Transact-SQL statement or stored procedure to insert new records into the data source.</summary>
		/// <returns>A <see cref="T:System.Data.SqlClient.SqlCommand" /> used during <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" /> to insert records into the database that correspond to new rows in the <see cref="T:System.Data.DataSet" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700039C RID: 924
		// (get) Token: 0x06001393 RID: 5011 RVA: 0x0005EB3D File Offset: 0x0005CD3D
		// (set) Token: 0x06001394 RID: 5012 RVA: 0x0005EB45 File Offset: 0x0005CD45
		public new SqlCommand InsertCommand
		{
			get
			{
				return this._insertCommand;
			}
			set
			{
				this._insertCommand = value;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Data.IDbDataAdapter.InsertCommand" />.</summary>
		/// <returns>An <see cref="T:System.Data.IDbCommand" /> that is used during <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" /> to insert records in the data source for new rows in the data set.</returns>
		// Token: 0x1700039D RID: 925
		// (get) Token: 0x06001395 RID: 5013 RVA: 0x0005EB3D File Offset: 0x0005CD3D
		// (set) Token: 0x06001396 RID: 5014 RVA: 0x0005EB4E File Offset: 0x0005CD4E
		IDbCommand IDbDataAdapter.InsertCommand
		{
			get
			{
				return this._insertCommand;
			}
			set
			{
				this._insertCommand = (SqlCommand)value;
			}
		}

		/// <summary>Gets or sets a Transact-SQL statement or stored procedure used to select records in the data source.</summary>
		/// <returns>A <see cref="T:System.Data.SqlClient.SqlCommand" /> used during <see cref="M:System.Data.Common.DbDataAdapter.Fill(System.Data.DataSet)" /> to select records from the database for placement in the <see cref="T:System.Data.DataSet" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700039E RID: 926
		// (get) Token: 0x06001397 RID: 5015 RVA: 0x0005EB5C File Offset: 0x0005CD5C
		// (set) Token: 0x06001398 RID: 5016 RVA: 0x0005EB64 File Offset: 0x0005CD64
		public new SqlCommand SelectCommand
		{
			get
			{
				return this._selectCommand;
			}
			set
			{
				this._selectCommand = value;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Data.IDbDataAdapter.SelectCommand" />.</summary>
		/// <returns>An <see cref="T:System.Data.IDbCommand" /> that is used during <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" /> to select records from data source for placement in the data set.</returns>
		// Token: 0x1700039F RID: 927
		// (get) Token: 0x06001399 RID: 5017 RVA: 0x0005EB5C File Offset: 0x0005CD5C
		// (set) Token: 0x0600139A RID: 5018 RVA: 0x0005EB6D File Offset: 0x0005CD6D
		IDbCommand IDbDataAdapter.SelectCommand
		{
			get
			{
				return this._selectCommand;
			}
			set
			{
				this._selectCommand = (SqlCommand)value;
			}
		}

		/// <summary>Gets or sets a Transact-SQL statement or stored procedure used to update records in the data source.</summary>
		/// <returns>A <see cref="T:System.Data.SqlClient.SqlCommand" /> used during <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" /> to update records in the database that correspond to modified rows in the <see cref="T:System.Data.DataSet" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x0600139B RID: 5019 RVA: 0x0005EB7B File Offset: 0x0005CD7B
		// (set) Token: 0x0600139C RID: 5020 RVA: 0x0005EB83 File Offset: 0x0005CD83
		public new SqlCommand UpdateCommand
		{
			get
			{
				return this._updateCommand;
			}
			set
			{
				this._updateCommand = value;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Data.IDbDataAdapter.UpdateCommand" />.</summary>
		/// <returns>An <see cref="T:System.Data.IdbCommand" /> that is used during <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" /> to update records in the data source for modified rows in the data set.</returns>
		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x0600139D RID: 5021 RVA: 0x0005EB7B File Offset: 0x0005CD7B
		// (set) Token: 0x0600139E RID: 5022 RVA: 0x0005EB8C File Offset: 0x0005CD8C
		IDbCommand IDbDataAdapter.UpdateCommand
		{
			get
			{
				return this._updateCommand;
			}
			set
			{
				this._updateCommand = (SqlCommand)value;
			}
		}

		/// <summary>Gets or sets the number of rows that are processed in each round-trip to the server.</summary>
		/// <returns>The number of rows to process per-batch. Value isEffect0There is no limit on the batch size..1Disables batch updating.&gt;1Changes are sent using batches of <see cref="P:System.Data.SqlClient.SqlDataAdapter.UpdateBatchSize" /> operations at a time.When setting this to a value other than 1, all the commands associated with the <see cref="T:System.Data.SqlClient.SqlDataAdapter" /> have to have their UpdatedRowSource property set to None or OutputParameters. An exception is thrown otherwise.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x0600139F RID: 5023 RVA: 0x0005EB9A File Offset: 0x0005CD9A
		// (set) Token: 0x060013A0 RID: 5024 RVA: 0x0005EBA2 File Offset: 0x0005CDA2
		public override int UpdateBatchSize
		{
			get
			{
				return this._updateBatchSize;
			}
			set
			{
				if (0 > value)
				{
					throw ADP.ArgumentOutOfRange("UpdateBatchSize");
				}
				this._updateBatchSize = value;
			}
		}

		// Token: 0x060013A1 RID: 5025 RVA: 0x0005EBBA File Offset: 0x0005CDBA
		protected override int AddToBatch(IDbCommand command)
		{
			int commandCount = this._commandSet.CommandCount;
			this._commandSet.Append((SqlCommand)command);
			return commandCount;
		}

		// Token: 0x060013A2 RID: 5026 RVA: 0x0005EBD8 File Offset: 0x0005CDD8
		protected override void ClearBatch()
		{
			this._commandSet.Clear();
		}

		// Token: 0x060013A3 RID: 5027 RVA: 0x0005EBE5 File Offset: 0x0005CDE5
		protected override int ExecuteBatch()
		{
			return this._commandSet.ExecuteNonQuery();
		}

		// Token: 0x060013A4 RID: 5028 RVA: 0x0005EBF2 File Offset: 0x0005CDF2
		protected override IDataParameter GetBatchedParameter(int commandIdentifier, int parameterIndex)
		{
			return this._commandSet.GetParameter(commandIdentifier, parameterIndex);
		}

		// Token: 0x060013A5 RID: 5029 RVA: 0x0005EC01 File Offset: 0x0005CE01
		protected override bool GetBatchedRecordsAffected(int commandIdentifier, out int recordsAffected, out Exception error)
		{
			return this._commandSet.GetBatchedAffected(commandIdentifier, out recordsAffected, out error);
		}

		// Token: 0x060013A6 RID: 5030 RVA: 0x0005EC14 File Offset: 0x0005CE14
		protected override void InitializeBatching()
		{
			this._commandSet = new SqlCommandSet();
			SqlCommand sqlCommand = this.SelectCommand;
			if (sqlCommand == null)
			{
				sqlCommand = this.InsertCommand;
				if (sqlCommand == null)
				{
					sqlCommand = this.UpdateCommand;
					if (sqlCommand == null)
					{
						sqlCommand = this.DeleteCommand;
					}
				}
			}
			if (sqlCommand != null)
			{
				this._commandSet.Connection = sqlCommand.Connection;
				this._commandSet.Transaction = sqlCommand.Transaction;
				this._commandSet.CommandTimeout = sqlCommand.CommandTimeout;
			}
		}

		// Token: 0x060013A7 RID: 5031 RVA: 0x0005EC87 File Offset: 0x0005CE87
		protected override void TerminateBatching()
		{
			if (this._commandSet != null)
			{
				this._commandSet.Dispose();
				this._commandSet = null;
			}
		}

		/// <summary>For a description of this member, see <see cref="M:System.ICloneable.Clone" />.</summary>
		/// <returns>A new object that is a copy of the current instance.</returns>
		// Token: 0x060013A8 RID: 5032 RVA: 0x0005ECA3 File Offset: 0x0005CEA3
		object ICloneable.Clone()
		{
			return new SqlDataAdapter(this);
		}

		// Token: 0x060013A9 RID: 5033 RVA: 0x0005ECAB File Offset: 0x0005CEAB
		protected override RowUpdatedEventArgs CreateRowUpdatedEvent(DataRow dataRow, IDbCommand command, StatementType statementType, DataTableMapping tableMapping)
		{
			return new SqlRowUpdatedEventArgs(dataRow, command, statementType, tableMapping);
		}

		// Token: 0x060013AA RID: 5034 RVA: 0x0005ECB7 File Offset: 0x0005CEB7
		protected override RowUpdatingEventArgs CreateRowUpdatingEvent(DataRow dataRow, IDbCommand command, StatementType statementType, DataTableMapping tableMapping)
		{
			return new SqlRowUpdatingEventArgs(dataRow, command, statementType, tableMapping);
		}

		/// <summary>Occurs during <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" /> after a command is executed against the data source. The attempt to update is made, so the event fires.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x14000026 RID: 38
		// (add) Token: 0x060013AB RID: 5035 RVA: 0x0005ECC3 File Offset: 0x0005CEC3
		// (remove) Token: 0x060013AC RID: 5036 RVA: 0x0005ECD6 File Offset: 0x0005CED6
		public event SqlRowUpdatedEventHandler RowUpdated
		{
			add
			{
				base.Events.AddHandler(SqlDataAdapter.EventRowUpdated, value);
			}
			remove
			{
				base.Events.RemoveHandler(SqlDataAdapter.EventRowUpdated, value);
			}
		}

		/// <summary>Occurs during <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" /> before a command is executed against the data source. The attempt to update is made, so the event fires.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x14000027 RID: 39
		// (add) Token: 0x060013AD RID: 5037 RVA: 0x0005ECEC File Offset: 0x0005CEEC
		// (remove) Token: 0x060013AE RID: 5038 RVA: 0x0005ED50 File Offset: 0x0005CF50
		public event SqlRowUpdatingEventHandler RowUpdating
		{
			add
			{
				SqlRowUpdatingEventHandler sqlRowUpdatingEventHandler = (SqlRowUpdatingEventHandler)base.Events[SqlDataAdapter.EventRowUpdating];
				if (sqlRowUpdatingEventHandler != null && value.Target is DbCommandBuilder)
				{
					SqlRowUpdatingEventHandler sqlRowUpdatingEventHandler2 = (SqlRowUpdatingEventHandler)ADP.FindBuilder(sqlRowUpdatingEventHandler);
					if (sqlRowUpdatingEventHandler2 != null)
					{
						base.Events.RemoveHandler(SqlDataAdapter.EventRowUpdating, sqlRowUpdatingEventHandler2);
					}
				}
				base.Events.AddHandler(SqlDataAdapter.EventRowUpdating, value);
			}
			remove
			{
				base.Events.RemoveHandler(SqlDataAdapter.EventRowUpdating, value);
			}
		}

		// Token: 0x060013AF RID: 5039 RVA: 0x0005ED64 File Offset: 0x0005CF64
		protected override void OnRowUpdated(RowUpdatedEventArgs value)
		{
			SqlRowUpdatedEventHandler sqlRowUpdatedEventHandler = (SqlRowUpdatedEventHandler)base.Events[SqlDataAdapter.EventRowUpdated];
			if (sqlRowUpdatedEventHandler != null && value is SqlRowUpdatedEventArgs)
			{
				sqlRowUpdatedEventHandler(this, (SqlRowUpdatedEventArgs)value);
			}
			base.OnRowUpdated(value);
		}

		// Token: 0x060013B0 RID: 5040 RVA: 0x0005EDA8 File Offset: 0x0005CFA8
		protected override void OnRowUpdating(RowUpdatingEventArgs value)
		{
			SqlRowUpdatingEventHandler sqlRowUpdatingEventHandler = (SqlRowUpdatingEventHandler)base.Events[SqlDataAdapter.EventRowUpdating];
			if (sqlRowUpdatingEventHandler != null && value is SqlRowUpdatingEventArgs)
			{
				sqlRowUpdatingEventHandler(this, (SqlRowUpdatingEventArgs)value);
			}
			base.OnRowUpdating(value);
		}

		// Token: 0x04000D1B RID: 3355
		private static readonly object EventRowUpdated = new object();

		// Token: 0x04000D1C RID: 3356
		private static readonly object EventRowUpdating = new object();

		// Token: 0x04000D1D RID: 3357
		private SqlCommand _deleteCommand;

		// Token: 0x04000D1E RID: 3358
		private SqlCommand _insertCommand;

		// Token: 0x04000D1F RID: 3359
		private SqlCommand _selectCommand;

		// Token: 0x04000D20 RID: 3360
		private SqlCommand _updateCommand;

		// Token: 0x04000D21 RID: 3361
		private SqlCommandSet _commandSet;

		// Token: 0x04000D22 RID: 3362
		private int _updateBatchSize = 1;
	}
}
