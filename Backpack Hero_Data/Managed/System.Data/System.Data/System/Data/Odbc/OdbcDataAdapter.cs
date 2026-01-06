using System;
using System.Data.Common;

namespace System.Data.Odbc
{
	/// <summary>Represents a set of data commands and a connection to a data source that are used to fill the <see cref="T:System.Data.DataSet" /> and update the data source. This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200028F RID: 655
	public sealed class OdbcDataAdapter : DbDataAdapter, IDbDataAdapter, IDataAdapter, ICloneable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Odbc.OdbcDataAdapter" /> class.</summary>
		// Token: 0x06001C75 RID: 7285 RVA: 0x0008B4EE File Offset: 0x000896EE
		public OdbcDataAdapter()
		{
			GC.SuppressFinalize(this);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Odbc.OdbcDataAdapter" /> class with the specified SQL SELECT statement.</summary>
		/// <param name="selectCommand">An <see cref="T:System.Data.Odbc.OdbcCommand" /> that is an SQL SELECT statement or stored procedure, and is set as the <see cref="P:System.Data.Odbc.OdbcDataAdapter.SelectCommand" /> property of the <see cref="T:System.Data.Odbc.OdbcDataAdapter" />. </param>
		// Token: 0x06001C76 RID: 7286 RVA: 0x0008B4FC File Offset: 0x000896FC
		public OdbcDataAdapter(OdbcCommand selectCommand)
			: this()
		{
			this.SelectCommand = selectCommand;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Odbc.OdbcDataAdapter" /> class with an SQL SELECT statement and an <see cref="T:System.Data.Odbc.OdbcConnection" />.</summary>
		/// <param name="selectCommandText">A string that is a SQL SELECT statement or stored procedure to be used by the <see cref="P:System.Data.Odbc.OdbcDataAdapter.SelectCommand" /> property of the <see cref="T:System.Data.Odbc.OdbcDataAdapter" />. </param>
		/// <param name="selectConnection">An <see cref="T:System.Data.Odbc.OdbcConnection" /> that represents the connection. </param>
		// Token: 0x06001C77 RID: 7287 RVA: 0x0008B50B File Offset: 0x0008970B
		public OdbcDataAdapter(string selectCommandText, OdbcConnection selectConnection)
			: this()
		{
			this.SelectCommand = new OdbcCommand(selectCommandText, selectConnection);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Odbc.OdbcDataAdapter" /> class with an SQL SELECT statement and a connection string.</summary>
		/// <param name="selectCommandText">A string that is a SQL SELECT statement or stored procedure to be used by the <see cref="P:System.Data.Odbc.OdbcDataAdapter.SelectCommand" /> property of the <see cref="T:System.Data.Odbc.OdbcDataAdapter" />. </param>
		/// <param name="selectConnectionString">The connection string. </param>
		// Token: 0x06001C78 RID: 7288 RVA: 0x0008B520 File Offset: 0x00089720
		public OdbcDataAdapter(string selectCommandText, string selectConnectionString)
			: this()
		{
			OdbcConnection odbcConnection = new OdbcConnection(selectConnectionString);
			this.SelectCommand = new OdbcCommand(selectCommandText, odbcConnection);
		}

		// Token: 0x06001C79 RID: 7289 RVA: 0x0008B547 File Offset: 0x00089747
		private OdbcDataAdapter(OdbcDataAdapter from)
			: base(from)
		{
			GC.SuppressFinalize(this);
		}

		/// <summary>Gets or sets an SQL statement or stored procedure used to delete records in the data source.</summary>
		/// <returns>An <see cref="T:System.Data.Odbc.OdbcCommand" /> used during an update operation to delete records in the data source that correspond to deleted rows in the <see cref="T:System.Data.DataSet" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000527 RID: 1319
		// (get) Token: 0x06001C7A RID: 7290 RVA: 0x0008B556 File Offset: 0x00089756
		// (set) Token: 0x06001C7B RID: 7291 RVA: 0x0008B55E File Offset: 0x0008975E
		public new OdbcCommand DeleteCommand
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

		/// <summary>For a description of this member, see <see cref="M:System.Data.IDbDataAdapter.DeleteCommand" />.</summary>
		/// <returns>An <see cref="T:System.Data.IDbCommand" /> used during an update to delete records in the data source for deleted rows in the data set.</returns>
		// Token: 0x17000528 RID: 1320
		// (get) Token: 0x06001C7C RID: 7292 RVA: 0x0008B556 File Offset: 0x00089756
		// (set) Token: 0x06001C7D RID: 7293 RVA: 0x0008B567 File Offset: 0x00089767
		IDbCommand IDbDataAdapter.DeleteCommand
		{
			get
			{
				return this._deleteCommand;
			}
			set
			{
				this._deleteCommand = (OdbcCommand)value;
			}
		}

		/// <summary>Gets or sets an SQL statement or stored procedure used to insert new records into the data source.</summary>
		/// <returns>An <see cref="T:System.Data.Odbc.OdbcCommand" /> used during an update operation to insert records in the data source that correspond to new rows in the <see cref="T:System.Data.DataSet" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000529 RID: 1321
		// (get) Token: 0x06001C7E RID: 7294 RVA: 0x0008B575 File Offset: 0x00089775
		// (set) Token: 0x06001C7F RID: 7295 RVA: 0x0008B57D File Offset: 0x0008977D
		public new OdbcCommand InsertCommand
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

		/// <summary>For a description of this member, see <see cref="M:System.Data.IDbDataAdapter.InsertCommand" />.</summary>
		/// <returns>An <see cref="T:System.Data.IDbCommand" /> that is used during an update to insert records from a data source for placement in the data set.</returns>
		// Token: 0x1700052A RID: 1322
		// (get) Token: 0x06001C80 RID: 7296 RVA: 0x0008B575 File Offset: 0x00089775
		// (set) Token: 0x06001C81 RID: 7297 RVA: 0x0008B586 File Offset: 0x00089786
		IDbCommand IDbDataAdapter.InsertCommand
		{
			get
			{
				return this._insertCommand;
			}
			set
			{
				this._insertCommand = (OdbcCommand)value;
			}
		}

		/// <summary>Gets or sets an SQL statement or stored procedure used to select records in the data source.</summary>
		/// <returns>An <see cref="T:System.Data.Odbc.OdbcCommand" /> that is used during a fill operation to select records from data source for placement in the <see cref="T:System.Data.DataSet" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700052B RID: 1323
		// (get) Token: 0x06001C82 RID: 7298 RVA: 0x0008B594 File Offset: 0x00089794
		// (set) Token: 0x06001C83 RID: 7299 RVA: 0x0008B59C File Offset: 0x0008979C
		public new OdbcCommand SelectCommand
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

		/// <summary>For a description of this member, see <see cref="M:System.Data.IDbDataAdapter.SelectCommand" />.</summary>
		/// <returns>An <see cref="T:System.Data.IDbCommand" /> that is used during an update to select records from a data source for placement in the data set.</returns>
		// Token: 0x1700052C RID: 1324
		// (get) Token: 0x06001C84 RID: 7300 RVA: 0x0008B594 File Offset: 0x00089794
		// (set) Token: 0x06001C85 RID: 7301 RVA: 0x0008B5A5 File Offset: 0x000897A5
		IDbCommand IDbDataAdapter.SelectCommand
		{
			get
			{
				return this._selectCommand;
			}
			set
			{
				this._selectCommand = (OdbcCommand)value;
			}
		}

		/// <summary>Gets or sets an SQL statement or stored procedure used to update records in the data source.</summary>
		/// <returns>An <see cref="T:System.Data.Odbc.OdbcCommand" /> used during an update operation to update records in the data source that correspond to modified rows in the <see cref="T:System.Data.DataSet" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700052D RID: 1325
		// (get) Token: 0x06001C86 RID: 7302 RVA: 0x0008B5B3 File Offset: 0x000897B3
		// (set) Token: 0x06001C87 RID: 7303 RVA: 0x0008B5BB File Offset: 0x000897BB
		public new OdbcCommand UpdateCommand
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

		/// <summary>For a description of this member, see <see cref="M:System.Data.IDbDataAdapter.UpdateCommand" />.</summary>
		/// <returns>An <see cref="T:System.Data.IDbCommand" /> used during an update to update records in the data source for modified rows in the data set.</returns>
		// Token: 0x1700052E RID: 1326
		// (get) Token: 0x06001C88 RID: 7304 RVA: 0x0008B5B3 File Offset: 0x000897B3
		// (set) Token: 0x06001C89 RID: 7305 RVA: 0x0008B5C4 File Offset: 0x000897C4
		IDbCommand IDbDataAdapter.UpdateCommand
		{
			get
			{
				return this._updateCommand;
			}
			set
			{
				this._updateCommand = (OdbcCommand)value;
			}
		}

		/// <summary>Occurs during an update operation after a command is executed against the data source.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1400002A RID: 42
		// (add) Token: 0x06001C8A RID: 7306 RVA: 0x0008B5D2 File Offset: 0x000897D2
		// (remove) Token: 0x06001C8B RID: 7307 RVA: 0x0008B5E5 File Offset: 0x000897E5
		public event OdbcRowUpdatedEventHandler RowUpdated
		{
			add
			{
				base.Events.AddHandler(OdbcDataAdapter.s_eventRowUpdated, value);
			}
			remove
			{
				base.Events.RemoveHandler(OdbcDataAdapter.s_eventRowUpdated, value);
			}
		}

		/// <summary>Occurs during <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" /> before a command is executed against the data source.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1400002B RID: 43
		// (add) Token: 0x06001C8C RID: 7308 RVA: 0x0008B5F8 File Offset: 0x000897F8
		// (remove) Token: 0x06001C8D RID: 7309 RVA: 0x0008B65C File Offset: 0x0008985C
		public event OdbcRowUpdatingEventHandler RowUpdating
		{
			add
			{
				OdbcRowUpdatingEventHandler odbcRowUpdatingEventHandler = (OdbcRowUpdatingEventHandler)base.Events[OdbcDataAdapter.s_eventRowUpdating];
				if (odbcRowUpdatingEventHandler != null && value.Target is OdbcCommandBuilder)
				{
					OdbcRowUpdatingEventHandler odbcRowUpdatingEventHandler2 = (OdbcRowUpdatingEventHandler)ADP.FindBuilder(odbcRowUpdatingEventHandler);
					if (odbcRowUpdatingEventHandler2 != null)
					{
						base.Events.RemoveHandler(OdbcDataAdapter.s_eventRowUpdating, odbcRowUpdatingEventHandler2);
					}
				}
				base.Events.AddHandler(OdbcDataAdapter.s_eventRowUpdating, value);
			}
			remove
			{
				base.Events.RemoveHandler(OdbcDataAdapter.s_eventRowUpdating, value);
			}
		}

		/// <summary>For a description of this member, see <see cref="M:System.ICloneable.Clone" />.</summary>
		/// <returns>A new <see cref="T:System.Object" /> that is a copy of this instance.</returns>
		// Token: 0x06001C8E RID: 7310 RVA: 0x0008B66F File Offset: 0x0008986F
		object ICloneable.Clone()
		{
			return new OdbcDataAdapter(this);
		}

		// Token: 0x06001C8F RID: 7311 RVA: 0x0008B677 File Offset: 0x00089877
		protected override RowUpdatedEventArgs CreateRowUpdatedEvent(DataRow dataRow, IDbCommand command, StatementType statementType, DataTableMapping tableMapping)
		{
			return new OdbcRowUpdatedEventArgs(dataRow, command, statementType, tableMapping);
		}

		// Token: 0x06001C90 RID: 7312 RVA: 0x0008B683 File Offset: 0x00089883
		protected override RowUpdatingEventArgs CreateRowUpdatingEvent(DataRow dataRow, IDbCommand command, StatementType statementType, DataTableMapping tableMapping)
		{
			return new OdbcRowUpdatingEventArgs(dataRow, command, statementType, tableMapping);
		}

		// Token: 0x06001C91 RID: 7313 RVA: 0x0008B690 File Offset: 0x00089890
		protected override void OnRowUpdated(RowUpdatedEventArgs value)
		{
			OdbcRowUpdatedEventHandler odbcRowUpdatedEventHandler = (OdbcRowUpdatedEventHandler)base.Events[OdbcDataAdapter.s_eventRowUpdated];
			if (odbcRowUpdatedEventHandler != null && value is OdbcRowUpdatedEventArgs)
			{
				odbcRowUpdatedEventHandler(this, (OdbcRowUpdatedEventArgs)value);
			}
			base.OnRowUpdated(value);
		}

		// Token: 0x06001C92 RID: 7314 RVA: 0x0008B6D4 File Offset: 0x000898D4
		protected override void OnRowUpdating(RowUpdatingEventArgs value)
		{
			OdbcRowUpdatingEventHandler odbcRowUpdatingEventHandler = (OdbcRowUpdatingEventHandler)base.Events[OdbcDataAdapter.s_eventRowUpdating];
			if (odbcRowUpdatingEventHandler != null && value is OdbcRowUpdatingEventArgs)
			{
				odbcRowUpdatingEventHandler(this, (OdbcRowUpdatingEventArgs)value);
			}
			base.OnRowUpdating(value);
		}

		// Token: 0x0400156E RID: 5486
		private static readonly object s_eventRowUpdated = new object();

		// Token: 0x0400156F RID: 5487
		private static readonly object s_eventRowUpdating = new object();

		// Token: 0x04001570 RID: 5488
		private OdbcCommand _deleteCommand;

		// Token: 0x04001571 RID: 5489
		private OdbcCommand _insertCommand;

		// Token: 0x04001572 RID: 5490
		private OdbcCommand _selectCommand;

		// Token: 0x04001573 RID: 5491
		private OdbcCommand _updateCommand;
	}
}
