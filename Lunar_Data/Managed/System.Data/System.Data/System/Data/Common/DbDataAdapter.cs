using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.ProviderBase;

namespace System.Data.Common
{
	/// <summary>Aids implementation of the <see cref="T:System.Data.IDbDataAdapter" /> interface. Inheritors of <see cref="T:System.Data.Common.DbDataAdapter" /> implement a set of functions to provide strong typing, but inherit most of the functionality needed to fully implement a DataAdapter. </summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200033C RID: 828
	public abstract class DbDataAdapter : DataAdapter, IDbDataAdapter, IDataAdapter, ICloneable
	{
		/// <summary>Initializes a new instance of a DataAdapter class.</summary>
		// Token: 0x060027D7 RID: 10199 RVA: 0x000AC012 File Offset: 0x000AA212
		protected DbDataAdapter()
		{
		}

		/// <summary>Initializes a new instance of a DataAdapter class from an existing object of the same type.</summary>
		/// <param name="adapter">A DataAdapter object used to create the new DataAdapter. </param>
		// Token: 0x060027D8 RID: 10200 RVA: 0x000B0045 File Offset: 0x000AE245
		protected DbDataAdapter(DbDataAdapter adapter)
			: base(adapter)
		{
			this.CloneFrom(adapter);
		}

		// Token: 0x170006D8 RID: 1752
		// (get) Token: 0x060027D9 RID: 10201 RVA: 0x0000565A File Offset: 0x0000385A
		private IDbDataAdapter _IDbDataAdapter
		{
			get
			{
				return this;
			}
		}

		/// <summary>Gets or sets a command for deleting records from the data set.</summary>
		/// <returns>An <see cref="T:System.Data.IDbCommand" /> used during <see cref="M:System.Data.IDataAdapter.Update(System.Data.DataSet)" /> to delete records in the data source for deleted rows in the data set.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170006D9 RID: 1753
		// (get) Token: 0x060027DA RID: 10202 RVA: 0x000B0055 File Offset: 0x000AE255
		// (set) Token: 0x060027DB RID: 10203 RVA: 0x000B0067 File Offset: 0x000AE267
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public DbCommand DeleteCommand
		{
			get
			{
				return (DbCommand)this._IDbDataAdapter.DeleteCommand;
			}
			set
			{
				this._IDbDataAdapter.DeleteCommand = value;
			}
		}

		/// <summary>Gets or sets an SQL statement for deleting records from the data set.</summary>
		/// <returns>An <see cref="T:System.Data.IDbCommand" /> used during <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" /> to delete records in the data source for deleted rows in the data set.</returns>
		// Token: 0x170006DA RID: 1754
		// (get) Token: 0x060027DC RID: 10204 RVA: 0x000B0075 File Offset: 0x000AE275
		// (set) Token: 0x060027DD RID: 10205 RVA: 0x000B007D File Offset: 0x000AE27D
		IDbCommand IDbDataAdapter.DeleteCommand
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

		/// <summary>Gets or sets the behavior of the command used to fill the data adapter.</summary>
		/// <returns>The <see cref="T:System.Data.CommandBehavior" /> of the command used to fill the data adapter.</returns>
		// Token: 0x170006DB RID: 1755
		// (get) Token: 0x060027DE RID: 10206 RVA: 0x000B0086 File Offset: 0x000AE286
		// (set) Token: 0x060027DF RID: 10207 RVA: 0x000B0091 File Offset: 0x000AE291
		protected internal CommandBehavior FillCommandBehavior
		{
			get
			{
				return this._fillCommandBehavior | CommandBehavior.SequentialAccess;
			}
			set
			{
				this._fillCommandBehavior = value | CommandBehavior.SequentialAccess;
			}
		}

		/// <summary>Gets or sets a command used to insert new records into the data source.</summary>
		/// <returns>A <see cref="T:System.Data.IDbCommand" /> used during <see cref="M:System.Data.IDataAdapter.Update(System.Data.DataSet)" /> to insert records in the data source for new rows in the data set.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170006DC RID: 1756
		// (get) Token: 0x060027E0 RID: 10208 RVA: 0x000B009D File Offset: 0x000AE29D
		// (set) Token: 0x060027E1 RID: 10209 RVA: 0x000B00AF File Offset: 0x000AE2AF
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public DbCommand InsertCommand
		{
			get
			{
				return (DbCommand)this._IDbDataAdapter.InsertCommand;
			}
			set
			{
				this._IDbDataAdapter.InsertCommand = value;
			}
		}

		/// <summary>Gets or sets an SQL statement used to insert new records into the data source.</summary>
		/// <returns>An <see cref="T:System.Data.IDbCommand" /> used during <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" /> to insert records in the data source for new rows in the data set.</returns>
		// Token: 0x170006DD RID: 1757
		// (get) Token: 0x060027E2 RID: 10210 RVA: 0x000B00BD File Offset: 0x000AE2BD
		// (set) Token: 0x060027E3 RID: 10211 RVA: 0x000B00C5 File Offset: 0x000AE2C5
		IDbCommand IDbDataAdapter.InsertCommand
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

		/// <summary>Gets or sets a command used to select records in the data source.</summary>
		/// <returns>A <see cref="T:System.Data.IDbCommand" /> that is used during <see cref="M:System.Data.IDataAdapter.Update(System.Data.DataSet)" /> to select records from data source for placement in the data set.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170006DE RID: 1758
		// (get) Token: 0x060027E4 RID: 10212 RVA: 0x000B00CE File Offset: 0x000AE2CE
		// (set) Token: 0x060027E5 RID: 10213 RVA: 0x000B00E0 File Offset: 0x000AE2E0
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public DbCommand SelectCommand
		{
			get
			{
				return (DbCommand)this._IDbDataAdapter.SelectCommand;
			}
			set
			{
				this._IDbDataAdapter.SelectCommand = value;
			}
		}

		/// <summary>Gets or sets an SQL statement used to select records in the data source.</summary>
		/// <returns>An <see cref="T:System.Data.IDbCommand" /> that is used during <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" /> to select records from data source for placement in the data set.</returns>
		// Token: 0x170006DF RID: 1759
		// (get) Token: 0x060027E6 RID: 10214 RVA: 0x000B00EE File Offset: 0x000AE2EE
		// (set) Token: 0x060027E7 RID: 10215 RVA: 0x000B00F6 File Offset: 0x000AE2F6
		IDbCommand IDbDataAdapter.SelectCommand
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

		/// <summary>Gets or sets a value that enables or disables batch processing support, and specifies the number of commands that can be executed in a batch. </summary>
		/// <returns>The number of rows to process per batch. Value isEffect0There is no limit on the batch size.1Disables batch updating.&gt; 1Changes are sent using batches of <see cref="P:System.Data.Common.DbDataAdapter.UpdateBatchSize" /> operations at a time.When setting this to a value other than 1 ,all the commands associated with the <see cref="T:System.Data.Common.DbDataAdapter" /> must have their <see cref="P:System.Data.IDbCommand.UpdatedRowSource" /> property set to None or OutputParameters. An exception will be thrown otherwise. </returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x170006E0 RID: 1760
		// (get) Token: 0x060027E8 RID: 10216 RVA: 0x0000CD07 File Offset: 0x0000AF07
		// (set) Token: 0x060027E9 RID: 10217 RVA: 0x000B00FF File Offset: 0x000AE2FF
		[DefaultValue(1)]
		public virtual int UpdateBatchSize
		{
			get
			{
				return 1;
			}
			set
			{
				if (1 != value)
				{
					throw ADP.NotSupported();
				}
			}
		}

		/// <summary>Gets or sets a command used to update records in the data source.</summary>
		/// <returns>A <see cref="T:System.Data.IDbCommand" /> used during <see cref="M:System.Data.IDataAdapter.Update(System.Data.DataSet)" /> to update records in the data source for modified rows in the data set.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170006E1 RID: 1761
		// (get) Token: 0x060027EA RID: 10218 RVA: 0x000B010B File Offset: 0x000AE30B
		// (set) Token: 0x060027EB RID: 10219 RVA: 0x000B011D File Offset: 0x000AE31D
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public DbCommand UpdateCommand
		{
			get
			{
				return (DbCommand)this._IDbDataAdapter.UpdateCommand;
			}
			set
			{
				this._IDbDataAdapter.UpdateCommand = value;
			}
		}

		/// <summary>Gets or sets an SQL statement used to update records in the data source.</summary>
		/// <returns>An <see cref="T:System.Data.IDbCommand" /> used during <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" /> to update records in the data source for modified rows in the data set.</returns>
		// Token: 0x170006E2 RID: 1762
		// (get) Token: 0x060027EC RID: 10220 RVA: 0x000B012B File Offset: 0x000AE32B
		// (set) Token: 0x060027ED RID: 10221 RVA: 0x000B0133 File Offset: 0x000AE333
		IDbCommand IDbDataAdapter.UpdateCommand
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

		// Token: 0x170006E3 RID: 1763
		// (get) Token: 0x060027EE RID: 10222 RVA: 0x000B013C File Offset: 0x000AE33C
		private MissingMappingAction UpdateMappingAction
		{
			get
			{
				if (MissingMappingAction.Passthrough == base.MissingMappingAction)
				{
					return MissingMappingAction.Passthrough;
				}
				return MissingMappingAction.Error;
			}
		}

		// Token: 0x170006E4 RID: 1764
		// (get) Token: 0x060027EF RID: 10223 RVA: 0x000B014C File Offset: 0x000AE34C
		private MissingSchemaAction UpdateSchemaAction
		{
			get
			{
				MissingSchemaAction missingSchemaAction = base.MissingSchemaAction;
				if (MissingSchemaAction.Add == missingSchemaAction || MissingSchemaAction.AddWithKey == missingSchemaAction)
				{
					return MissingSchemaAction.Ignore;
				}
				return MissingSchemaAction.Error;
			}
		}

		/// <summary>Adds a <see cref="T:System.Data.IDbCommand" /> to the current batch.</summary>
		/// <returns>The number of commands in the batch before adding the <see cref="T:System.Data.IDbCommand" />.</returns>
		/// <param name="command">The <see cref="T:System.Data.IDbCommand" /> to add to the batch.</param>
		/// <exception cref="T:System.NotSupportedException">The adapter does not support batches. </exception>
		// Token: 0x060027F0 RID: 10224 RVA: 0x00060F32 File Offset: 0x0005F132
		protected virtual int AddToBatch(IDbCommand command)
		{
			throw ADP.NotSupported();
		}

		/// <summary>Removes all <see cref="T:System.Data.IDbCommand" /> objects from the batch.</summary>
		/// <exception cref="T:System.NotSupportedException">The adapter does not support batches. </exception>
		// Token: 0x060027F1 RID: 10225 RVA: 0x00060F32 File Offset: 0x0005F132
		protected virtual void ClearBatch()
		{
			throw ADP.NotSupported();
		}

		/// <summary>Creates a new object that is a copy of the current instance.</summary>
		/// <returns>A new object that is a copy of this instance.</returns>
		// Token: 0x060027F2 RID: 10226 RVA: 0x000B016B File Offset: 0x000AE36B
		object ICloneable.Clone()
		{
			DbDataAdapter dbDataAdapter = (DbDataAdapter)this.CloneInternals();
			dbDataAdapter.CloneFrom(this);
			return dbDataAdapter;
		}

		// Token: 0x060027F3 RID: 10227 RVA: 0x000B0180 File Offset: 0x000AE380
		private void CloneFrom(DbDataAdapter from)
		{
			IDbDataAdapter idbDataAdapter = from._IDbDataAdapter;
			this._IDbDataAdapter.SelectCommand = this.CloneCommand(idbDataAdapter.SelectCommand);
			this._IDbDataAdapter.InsertCommand = this.CloneCommand(idbDataAdapter.InsertCommand);
			this._IDbDataAdapter.UpdateCommand = this.CloneCommand(idbDataAdapter.UpdateCommand);
			this._IDbDataAdapter.DeleteCommand = this.CloneCommand(idbDataAdapter.DeleteCommand);
		}

		// Token: 0x060027F4 RID: 10228 RVA: 0x000B01F0 File Offset: 0x000AE3F0
		private IDbCommand CloneCommand(IDbCommand command)
		{
			return (IDbCommand)((command is ICloneable) ? ((ICloneable)command).Clone() : null);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Common.RowUpdatedEventArgs" /> class.</summary>
		/// <returns>A new instance of the <see cref="T:System.Data.Common.RowUpdatedEventArgs" /> class.</returns>
		/// <param name="dataRow">The <see cref="T:System.Data.DataRow" /> used to update the data source. </param>
		/// <param name="command">The <see cref="T:System.Data.IDbCommand" /> executed during the <see cref="M:System.Data.IDataAdapter.Update(System.Data.DataSet)" />. </param>
		/// <param name="statementType">Whether the command is an UPDATE, INSERT, DELETE, or SELECT statement. </param>
		/// <param name="tableMapping">A <see cref="T:System.Data.Common.DataTableMapping" /> object. </param>
		// Token: 0x060027F5 RID: 10229 RVA: 0x000B020D File Offset: 0x000AE40D
		protected virtual RowUpdatedEventArgs CreateRowUpdatedEvent(DataRow dataRow, IDbCommand command, StatementType statementType, DataTableMapping tableMapping)
		{
			return new RowUpdatedEventArgs(dataRow, command, statementType, tableMapping);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Common.RowUpdatingEventArgs" /> class.</summary>
		/// <returns>A new instance of the <see cref="T:System.Data.Common.RowUpdatingEventArgs" /> class.</returns>
		/// <param name="dataRow">The <see cref="T:System.Data.DataRow" /> that updates the data source. </param>
		/// <param name="command">The <see cref="T:System.Data.IDbCommand" /> to execute during the <see cref="M:System.Data.IDataAdapter.Update(System.Data.DataSet)" />. </param>
		/// <param name="statementType">Whether the command is an UPDATE, INSERT, DELETE, or SELECT statement. </param>
		/// <param name="tableMapping">A <see cref="T:System.Data.Common.DataTableMapping" /> object. </param>
		// Token: 0x060027F6 RID: 10230 RVA: 0x000B0219 File Offset: 0x000AE419
		protected virtual RowUpdatingEventArgs CreateRowUpdatingEvent(DataRow dataRow, IDbCommand command, StatementType statementType, DataTableMapping tableMapping)
		{
			return new RowUpdatingEventArgs(dataRow, command, statementType, tableMapping);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Data.Common.DbDataAdapter" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x060027F7 RID: 10231 RVA: 0x000B0225 File Offset: 0x000AE425
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				((IDbDataAdapter)this).SelectCommand = null;
				((IDbDataAdapter)this).InsertCommand = null;
				((IDbDataAdapter)this).UpdateCommand = null;
				((IDbDataAdapter)this).DeleteCommand = null;
			}
			base.Dispose(disposing);
		}

		/// <summary>Executes the current batch.</summary>
		/// <returns>The return value from the last command in the batch.</returns>
		// Token: 0x060027F8 RID: 10232 RVA: 0x00060F32 File Offset: 0x0005F132
		protected virtual int ExecuteBatch()
		{
			throw ADP.NotSupported();
		}

		/// <summary>Configures the schema of the specified <see cref="T:System.Data.DataTable" /> based on the specified <see cref="T:System.Data.SchemaType" />.</summary>
		/// <returns>A <see cref="T:System.Data.DataTable" /> that contains schema information returned from the data source.</returns>
		/// <param name="dataTable">The <see cref="T:System.Data.DataTable" /> to be filled with the schema from the data source. </param>
		/// <param name="schemaType">One of the <see cref="T:System.Data.SchemaType" /> values. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060027F9 RID: 10233 RVA: 0x000B0250 File Offset: 0x000AE450
		public DataTable FillSchema(DataTable dataTable, SchemaType schemaType)
		{
			long num = DataCommonEventSource.Log.EnterScope<int, SchemaType>("<comm.DbDataAdapter.FillSchema|API> {0}, dataTable, schemaType={1}", base.ObjectID, schemaType);
			DataTable dataTable2;
			try
			{
				IDbCommand selectCommand = this._IDbDataAdapter.SelectCommand;
				CommandBehavior fillCommandBehavior = this.FillCommandBehavior;
				dataTable2 = this.FillSchema(dataTable, schemaType, selectCommand, fillCommandBehavior);
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
			return dataTable2;
		}

		/// <summary>Adds a <see cref="T:System.Data.DataTable" /> named "Table" to the specified <see cref="T:System.Data.DataSet" /> and configures the schema to match that in the data source based on the specified <see cref="T:System.Data.SchemaType" />.</summary>
		/// <returns>A reference to a collection of <see cref="T:System.Data.DataTable" /> objects that were added to the <see cref="T:System.Data.DataSet" />.</returns>
		/// <param name="dataSet">A <see cref="T:System.Data.DataSet" /> to insert the schema in. </param>
		/// <param name="schemaType">One of the <see cref="T:System.Data.SchemaType" /> values that specify how to insert the schema. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060027FA RID: 10234 RVA: 0x000B02B4 File Offset: 0x000AE4B4
		public override DataTable[] FillSchema(DataSet dataSet, SchemaType schemaType)
		{
			long num = DataCommonEventSource.Log.EnterScope<int, SchemaType>("<comm.DbDataAdapter.FillSchema|API> {0}, dataSet, schemaType={1}", base.ObjectID, schemaType);
			DataTable[] array;
			try
			{
				IDbCommand selectCommand = this._IDbDataAdapter.SelectCommand;
				if (base.DesignMode && (selectCommand == null || selectCommand.Connection == null || string.IsNullOrEmpty(selectCommand.CommandText)))
				{
					array = Array.Empty<DataTable>();
				}
				else
				{
					CommandBehavior fillCommandBehavior = this.FillCommandBehavior;
					array = this.FillSchema(dataSet, schemaType, selectCommand, "Table", fillCommandBehavior);
				}
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
			return array;
		}

		/// <summary>Adds a <see cref="T:System.Data.DataTable" /> to the specified <see cref="T:System.Data.DataSet" /> and configures the schema to match that in the data source based upon the specified <see cref="T:System.Data.SchemaType" /> and <see cref="T:System.Data.DataTable" />.</summary>
		/// <returns>A reference to a collection of <see cref="T:System.Data.DataTable" /> objects that were added to the <see cref="T:System.Data.DataSet" />.</returns>
		/// <param name="dataSet">A <see cref="T:System.Data.DataSet" /> to insert the schema in. </param>
		/// <param name="schemaType">One of the <see cref="T:System.Data.SchemaType" /> values that specify how to insert the schema. </param>
		/// <param name="srcTable">The name of the source table to use for table mapping. </param>
		/// <exception cref="T:System.ArgumentException">A source table from which to get the schema could not be found. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060027FB RID: 10235 RVA: 0x000B0344 File Offset: 0x000AE544
		public DataTable[] FillSchema(DataSet dataSet, SchemaType schemaType, string srcTable)
		{
			long num = DataCommonEventSource.Log.EnterScope<int, int, string>("<comm.DbDataAdapter.FillSchema|API> {0}, dataSet, schemaType={1}, srcTable={2}", base.ObjectID, (int)schemaType, srcTable);
			DataTable[] array;
			try
			{
				IDbCommand selectCommand = this._IDbDataAdapter.SelectCommand;
				CommandBehavior fillCommandBehavior = this.FillCommandBehavior;
				array = this.FillSchema(dataSet, schemaType, selectCommand, srcTable, fillCommandBehavior);
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
			return array;
		}

		/// <summary>Adds a <see cref="T:System.Data.DataTable" /> to the specified <see cref="T:System.Data.DataSet" /> and configures the schema to match that in the data source based on the specified <see cref="T:System.Data.SchemaType" />.</summary>
		/// <returns>An array of <see cref="T:System.Data.DataTable" /> objects that contain schema information returned from the data source.</returns>
		/// <param name="dataSet">The <see cref="T:System.Data.DataSet" /> to be filled with the schema from the data source. </param>
		/// <param name="schemaType">One of the <see cref="T:System.Data.SchemaType" /> values. </param>
		/// <param name="command">The SQL SELECT statement used to retrieve rows from the data source. </param>
		/// <param name="srcTable">The name of the source table to use for table mapping. </param>
		/// <param name="behavior">One of the <see cref="T:System.Data.CommandBehavior" /> values. </param>
		// Token: 0x060027FC RID: 10236 RVA: 0x000B03A8 File Offset: 0x000AE5A8
		protected virtual DataTable[] FillSchema(DataSet dataSet, SchemaType schemaType, IDbCommand command, string srcTable, CommandBehavior behavior)
		{
			long num = DataCommonEventSource.Log.EnterScope<int, CommandBehavior>("<comm.DbDataAdapter.FillSchema|API> {0}, dataSet, schemaType, command, srcTable, behavior={1}", base.ObjectID, behavior);
			DataTable[] array;
			try
			{
				if (dataSet == null)
				{
					throw ADP.ArgumentNull("dataSet");
				}
				if (SchemaType.Source != schemaType && SchemaType.Mapped != schemaType)
				{
					throw ADP.InvalidSchemaType(schemaType);
				}
				if (string.IsNullOrEmpty(srcTable))
				{
					throw ADP.FillSchemaRequiresSourceTableName("srcTable");
				}
				if (command == null)
				{
					throw ADP.MissingSelectCommand("FillSchema");
				}
				array = (DataTable[])this.FillSchemaInternal(dataSet, null, schemaType, command, srcTable, behavior);
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
			return array;
		}

		/// <summary>Configures the schema of the specified <see cref="T:System.Data.DataTable" /> based on the specified <see cref="T:System.Data.SchemaType" />, command string, and <see cref="T:System.Data.CommandBehavior" /> values.</summary>
		/// <returns>A of <see cref="T:System.Data.DataTable" /> object that contains schema information returned from the data source.</returns>
		/// <param name="dataTable">The <see cref="T:System.Data.DataTable" /> to be filled with the schema from the data source. </param>
		/// <param name="schemaType">One of the <see cref="T:System.Data.SchemaType" /> values. </param>
		/// <param name="command">The SQL SELECT statement used to retrieve rows from the data source. </param>
		/// <param name="behavior">One of the <see cref="T:System.Data.CommandBehavior" /> values. </param>
		// Token: 0x060027FD RID: 10237 RVA: 0x000B0440 File Offset: 0x000AE640
		protected virtual DataTable FillSchema(DataTable dataTable, SchemaType schemaType, IDbCommand command, CommandBehavior behavior)
		{
			long num = DataCommonEventSource.Log.EnterScope<int, CommandBehavior>("<comm.DbDataAdapter.FillSchema|API> {0}, dataTable, schemaType, command, behavior={1}", base.ObjectID, behavior);
			DataTable dataTable2;
			try
			{
				if (dataTable == null)
				{
					throw ADP.ArgumentNull("dataTable");
				}
				if (SchemaType.Source != schemaType && SchemaType.Mapped != schemaType)
				{
					throw ADP.InvalidSchemaType(schemaType);
				}
				if (command == null)
				{
					throw ADP.MissingSelectCommand("FillSchema");
				}
				string text = dataTable.TableName;
				int num2 = base.IndexOfDataSetTable(text);
				if (-1 != num2)
				{
					text = base.TableMappings[num2].SourceTable;
				}
				dataTable2 = (DataTable)this.FillSchemaInternal(null, dataTable, schemaType, command, text, behavior | CommandBehavior.SingleResult);
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
			return dataTable2;
		}

		// Token: 0x060027FE RID: 10238 RVA: 0x000B04EC File Offset: 0x000AE6EC
		private object FillSchemaInternal(DataSet dataset, DataTable datatable, SchemaType schemaType, IDbCommand command, string srcTable, CommandBehavior behavior)
		{
			object obj = null;
			bool flag = command.Connection == null;
			try
			{
				IDbConnection connection = DbDataAdapter.GetConnection3(this, command, "FillSchema");
				ConnectionState connectionState = ConnectionState.Open;
				try
				{
					DbDataAdapter.QuietOpen(connection, out connectionState);
					using (IDataReader dataReader = command.ExecuteReader(behavior | CommandBehavior.SchemaOnly | CommandBehavior.KeyInfo))
					{
						if (datatable != null)
						{
							obj = this.FillSchema(datatable, schemaType, dataReader);
						}
						else
						{
							obj = this.FillSchema(dataset, schemaType, srcTable, dataReader);
						}
					}
				}
				finally
				{
					DbDataAdapter.QuietClose(connection, connectionState);
				}
			}
			finally
			{
				if (flag)
				{
					command.Transaction = null;
					command.Connection = null;
				}
			}
			return obj;
		}

		/// <summary>Adds or refreshes rows in the <see cref="T:System.Data.DataSet" />.</summary>
		/// <returns>The number of rows successfully added to or refreshed in the <see cref="T:System.Data.DataSet" />. This does not include rows affected by statements that do not return rows.</returns>
		/// <param name="dataSet">A <see cref="T:System.Data.DataSet" /> to fill with records and, if necessary, schema. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060027FF RID: 10239 RVA: 0x000B05A0 File Offset: 0x000AE7A0
		public override int Fill(DataSet dataSet)
		{
			long num = DataCommonEventSource.Log.EnterScope<int>("<comm.DbDataAdapter.Fill|API> {0}, dataSet", base.ObjectID);
			int num2;
			try
			{
				IDbCommand selectCommand = this._IDbDataAdapter.SelectCommand;
				CommandBehavior fillCommandBehavior = this.FillCommandBehavior;
				num2 = this.Fill(dataSet, 0, 0, "Table", selectCommand, fillCommandBehavior);
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
			return num2;
		}

		/// <summary>Adds or refreshes rows in the <see cref="T:System.Data.DataSet" /> to match those in the data source using the <see cref="T:System.Data.DataSet" /> and <see cref="T:System.Data.DataTable" /> names.</summary>
		/// <returns>The number of rows successfully added to or refreshed in the <see cref="T:System.Data.DataSet" />. This does not include rows affected by statements that do not return rows.</returns>
		/// <param name="dataSet">A <see cref="T:System.Data.DataSet" /> to fill with records and, if necessary, schema. </param>
		/// <param name="srcTable">The name of the source table to use for table mapping. </param>
		/// <exception cref="T:System.SystemException">The source table is invalid. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06002800 RID: 10240 RVA: 0x000B0608 File Offset: 0x000AE808
		public int Fill(DataSet dataSet, string srcTable)
		{
			long num = DataCommonEventSource.Log.EnterScope<int, string>("<comm.DbDataAdapter.Fill|API> {0}, dataSet, srcTable='{1}'", base.ObjectID, srcTable);
			int num2;
			try
			{
				IDbCommand selectCommand = this._IDbDataAdapter.SelectCommand;
				CommandBehavior fillCommandBehavior = this.FillCommandBehavior;
				num2 = this.Fill(dataSet, 0, 0, srcTable, selectCommand, fillCommandBehavior);
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
			return num2;
		}

		/// <summary>Adds or refreshes rows in a specified range in the <see cref="T:System.Data.DataSet" /> to match those in the data source using the <see cref="T:System.Data.DataSet" /> and <see cref="T:System.Data.DataTable" /> names.</summary>
		/// <returns>The number of rows successfully added to or refreshed in the <see cref="T:System.Data.DataSet" />. This does not include rows affected by statements that do not return rows.</returns>
		/// <param name="dataSet">A <see cref="T:System.Data.DataSet" /> to fill with records and, if necessary, schema. </param>
		/// <param name="startRecord">The zero-based record number to start with. </param>
		/// <param name="maxRecords">The maximum number of records to retrieve. </param>
		/// <param name="srcTable">The name of the source table to use for table mapping. </param>
		/// <exception cref="T:System.SystemException">The <see cref="T:System.Data.DataSet" /> is invalid. </exception>
		/// <exception cref="T:System.InvalidOperationException">The source table is invalid.-or- The connection is invalid. </exception>
		/// <exception cref="T:System.InvalidCastException">The connection could not be found. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="startRecord" /> parameter is less than 0.-or- The <paramref name="maxRecords" /> parameter is less than 0. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06002801 RID: 10241 RVA: 0x000B066C File Offset: 0x000AE86C
		public int Fill(DataSet dataSet, int startRecord, int maxRecords, string srcTable)
		{
			long num = DataCommonEventSource.Log.EnterScope<int, int, int, string>("<comm.DbDataAdapter.Fill|API> {0}, dataSet, startRecord={1}, maxRecords={2}, srcTable='{3}'", base.ObjectID, startRecord, maxRecords, srcTable);
			int num2;
			try
			{
				IDbCommand selectCommand = this._IDbDataAdapter.SelectCommand;
				CommandBehavior fillCommandBehavior = this.FillCommandBehavior;
				num2 = this.Fill(dataSet, startRecord, maxRecords, srcTable, selectCommand, fillCommandBehavior);
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
			return num2;
		}

		/// <summary>Adds or refreshes rows in a specified range in the <see cref="T:System.Data.DataSet" /> to match those in the data source using the <see cref="T:System.Data.DataSet" /> and source table names, command string, and command behavior.</summary>
		/// <returns>The number of rows successfully added to or refreshed in the <see cref="T:System.Data.DataSet" />. This does not include rows affected by statements that do not return rows.</returns>
		/// <param name="dataSet">A <see cref="T:System.Data.DataSet" /> to fill with records and, if necessary, schema. </param>
		/// <param name="startRecord">The zero-based record number to start with. </param>
		/// <param name="maxRecords">The maximum number of records to retrieve. </param>
		/// <param name="srcTable">The name of the source table to use for table mapping. </param>
		/// <param name="command">The SQL SELECT statement used to retrieve rows from the data source. </param>
		/// <param name="behavior">One of the <see cref="T:System.Data.CommandBehavior" /> values. </param>
		/// <exception cref="T:System.InvalidOperationException">The source table is invalid. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="startRecord" /> parameter is less than 0.-or- The <paramref name="maxRecords" /> parameter is less than 0. </exception>
		// Token: 0x06002802 RID: 10242 RVA: 0x000B06D4 File Offset: 0x000AE8D4
		protected virtual int Fill(DataSet dataSet, int startRecord, int maxRecords, string srcTable, IDbCommand command, CommandBehavior behavior)
		{
			long num = DataCommonEventSource.Log.EnterScope<int, CommandBehavior>("<comm.DbDataAdapter.Fill|API> {0}, dataSet, startRecord, maxRecords, srcTable, command, behavior={1}", base.ObjectID, behavior);
			int num2;
			try
			{
				if (dataSet == null)
				{
					throw ADP.FillRequires("dataSet");
				}
				if (startRecord < 0)
				{
					throw ADP.InvalidStartRecord("startRecord", startRecord);
				}
				if (maxRecords < 0)
				{
					throw ADP.InvalidMaxRecords("maxRecords", maxRecords);
				}
				if (string.IsNullOrEmpty(srcTable))
				{
					throw ADP.FillRequiresSourceTableName("srcTable");
				}
				if (command == null)
				{
					throw ADP.MissingSelectCommand("Fill");
				}
				num2 = this.FillInternal(dataSet, null, startRecord, maxRecords, srcTable, command, behavior);
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
			return num2;
		}

		/// <summary>Adds or refreshes rows in a specified range in the <see cref="T:System.Data.DataSet" /> to match those in the data source using the <see cref="T:System.Data.DataTable" /> name.</summary>
		/// <returns>The number of rows successfully added to or refreshed in the <see cref="T:System.Data.DataSet" />. This does not include rows affected by statements that do not return rows.</returns>
		/// <param name="dataTable">The name of the <see cref="T:System.Data.DataTable" /> to use for table mapping. </param>
		/// <exception cref="T:System.InvalidOperationException">The source table is invalid. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06002803 RID: 10243 RVA: 0x000B077C File Offset: 0x000AE97C
		public int Fill(DataTable dataTable)
		{
			long num = DataCommonEventSource.Log.EnterScope<int>("<comm.DbDataAdapter.Fill|API> {0}, dataTable", base.ObjectID);
			int num2;
			try
			{
				DataTable[] array = new DataTable[] { dataTable };
				IDbCommand selectCommand = this._IDbDataAdapter.SelectCommand;
				CommandBehavior fillCommandBehavior = this.FillCommandBehavior;
				num2 = this.Fill(array, 0, 0, selectCommand, fillCommandBehavior);
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
			return num2;
		}

		/// <summary>Adds or refreshes rows in a <see cref="T:System.Data.DataTable" /> to match those in the data source starting at the specified record and retrieving up to the specified maximum number of records.</summary>
		/// <returns>The number of rows successfully added to or refreshed in the <see cref="T:System.Data.DataTable" />. This value does not include rows affected by statements that do not return rows.</returns>
		/// <param name="startRecord">The zero-based record number to start with. </param>
		/// <param name="maxRecords">The maximum number of records to retrieve. </param>
		/// <param name="dataTables">The <see cref="T:System.Data.DataTable" /> objects to fill from the data source.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06002804 RID: 10244 RVA: 0x000B07EC File Offset: 0x000AE9EC
		public int Fill(int startRecord, int maxRecords, params DataTable[] dataTables)
		{
			long num = DataCommonEventSource.Log.EnterScope<int, int, int>("<comm.DbDataAdapter.Fill|API> {0}, startRecord={1}, maxRecords={2}, dataTable[]", base.ObjectID, startRecord, maxRecords);
			int num2;
			try
			{
				IDbCommand selectCommand = this._IDbDataAdapter.SelectCommand;
				CommandBehavior fillCommandBehavior = this.FillCommandBehavior;
				num2 = this.Fill(dataTables, startRecord, maxRecords, selectCommand, fillCommandBehavior);
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
			return num2;
		}

		/// <summary>Adds or refreshes rows in a <see cref="T:System.Data.DataTable" /> to match those in the data source using the specified <see cref="T:System.Data.DataTable" />, <see cref="T:System.Data.IDbCommand" /> and <see cref="T:System.Data.CommandBehavior" />.</summary>
		/// <returns>The number of rows successfully added to or refreshed in the <see cref="T:System.Data.DataTable" />. This does not include rows affected by statements that do not return rows.</returns>
		/// <param name="dataTable">A <see cref="T:System.Data.DataTable" /> to fill with records and, if necessary, schema. </param>
		/// <param name="command">The SQL SELECT statement used to retrieve rows from the data source. </param>
		/// <param name="behavior">One of the <see cref="T:System.Data.CommandBehavior" /> values.</param>
		// Token: 0x06002805 RID: 10245 RVA: 0x000B0850 File Offset: 0x000AEA50
		protected virtual int Fill(DataTable dataTable, IDbCommand command, CommandBehavior behavior)
		{
			long num = DataCommonEventSource.Log.EnterScope<int, CommandBehavior>("<comm.DbDataAdapter.Fill|API> {0}, dataTable, command, behavior={1}", base.ObjectID, behavior);
			int num2;
			try
			{
				DataTable[] array = new DataTable[] { dataTable };
				num2 = this.Fill(array, 0, 0, command, behavior);
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
			return num2;
		}

		/// <summary>Adds or refreshes rows in a specified range in the <see cref="T:System.Data.DataSet" /> to match those in the data source using the <see cref="T:System.Data.DataSet" /> and <see cref="T:System.Data.DataTable" /> names.</summary>
		/// <returns>The number of rows added to or refreshed in the data tables.</returns>
		/// <param name="dataTables">The <see cref="T:System.Data.DataTable" /> objects to fill from the data source.</param>
		/// <param name="startRecord">The zero-based record number to start with.</param>
		/// <param name="maxRecords">The maximum number of records to retrieve.</param>
		/// <param name="command">The <see cref="T:System.Data.IDbCommand" /> executed to fill the <see cref="T:System.Data.DataTable" /> objects.</param>
		/// <param name="behavior">One of the <see cref="T:System.Data.CommandBehavior" /> values.</param>
		/// <exception cref="T:System.SystemException">The <see cref="T:System.Data.DataSet" /> is invalid. </exception>
		/// <exception cref="T:System.InvalidOperationException">The source table is invalid.-or- The connection is invalid. </exception>
		/// <exception cref="T:System.InvalidCastException">The connection could not be found. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="startRecord" /> parameter is less than 0.-or- The <paramref name="maxRecords" /> parameter is less than 0. </exception>
		// Token: 0x06002806 RID: 10246 RVA: 0x000B08AC File Offset: 0x000AEAAC
		protected virtual int Fill(DataTable[] dataTables, int startRecord, int maxRecords, IDbCommand command, CommandBehavior behavior)
		{
			long num = DataCommonEventSource.Log.EnterScope<int, CommandBehavior>("<comm.DbDataAdapter.Fill|API> {0}, dataTables[], startRecord, maxRecords, command, behavior={1}", base.ObjectID, behavior);
			int num2;
			try
			{
				if (dataTables == null || dataTables.Length == 0 || dataTables[0] == null)
				{
					throw ADP.FillRequires("dataTable");
				}
				if (startRecord < 0)
				{
					throw ADP.InvalidStartRecord("startRecord", startRecord);
				}
				if (maxRecords < 0)
				{
					throw ADP.InvalidMaxRecords("maxRecords", maxRecords);
				}
				if (1 < dataTables.Length && (startRecord != 0 || maxRecords != 0))
				{
					throw ADP.OnlyOneTableForStartRecordOrMaxRecords();
				}
				if (command == null)
				{
					throw ADP.MissingSelectCommand("Fill");
				}
				if (1 == dataTables.Length)
				{
					behavior |= CommandBehavior.SingleResult;
				}
				num2 = this.FillInternal(null, dataTables, startRecord, maxRecords, null, command, behavior);
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
			return num2;
		}

		// Token: 0x06002807 RID: 10247 RVA: 0x000B0964 File Offset: 0x000AEB64
		private int FillInternal(DataSet dataset, DataTable[] datatables, int startRecord, int maxRecords, string srcTable, IDbCommand command, CommandBehavior behavior)
		{
			int num = 0;
			bool flag = command.Connection == null;
			try
			{
				IDbConnection connection = DbDataAdapter.GetConnection3(this, command, "Fill");
				ConnectionState connectionState = ConnectionState.Open;
				if (MissingSchemaAction.AddWithKey == base.MissingSchemaAction)
				{
					behavior |= CommandBehavior.KeyInfo;
				}
				try
				{
					DbDataAdapter.QuietOpen(connection, out connectionState);
					behavior |= CommandBehavior.SequentialAccess;
					IDataReader dataReader = null;
					try
					{
						dataReader = command.ExecuteReader(behavior);
						if (datatables != null)
						{
							num = this.Fill(datatables, dataReader, startRecord, maxRecords);
						}
						else
						{
							num = this.Fill(dataset, srcTable, dataReader, startRecord, maxRecords);
						}
					}
					finally
					{
						if (dataReader != null)
						{
							dataReader.Dispose();
						}
					}
				}
				finally
				{
					DbDataAdapter.QuietClose(connection, connectionState);
				}
			}
			finally
			{
				if (flag)
				{
					command.Transaction = null;
					command.Connection = null;
				}
			}
			return num;
		}

		/// <summary>Returns a <see cref="T:System.Data.IDataParameter" /> from one of the commands in the current batch.</summary>
		/// <returns>The <see cref="T:System.Data.IDataParameter" /> specified.</returns>
		/// <param name="commandIdentifier">The index of the command to retrieve the parameter from.</param>
		/// <param name="parameterIndex">The index of the parameter within the command.</param>
		/// <exception cref="T:System.NotSupportedException">The adapter does not support batches. </exception>
		// Token: 0x06002808 RID: 10248 RVA: 0x00060F32 File Offset: 0x0005F132
		protected virtual IDataParameter GetBatchedParameter(int commandIdentifier, int parameterIndex)
		{
			throw ADP.NotSupported();
		}

		/// <summary>Returns information about an individual update attempt within a larger batched update.</summary>
		/// <returns>Information about an individual update attempt within a larger batched update.</returns>
		/// <param name="commandIdentifier">The zero-based column ordinal of the individual command within the batch.</param>
		/// <param name="recordsAffected">The number of rows affected in the data store by the specified command within the batch.</param>
		/// <param name="error">An <see cref="T:System.Exception" /> thrown during execution of the specified command. Returns null (Nothing in Visual Basic) if no exception is thrown.</param>
		// Token: 0x06002809 RID: 10249 RVA: 0x000B0A30 File Offset: 0x000AEC30
		protected virtual bool GetBatchedRecordsAffected(int commandIdentifier, out int recordsAffected, out Exception error)
		{
			recordsAffected = 1;
			error = null;
			return true;
		}

		/// <summary>Gets the parameters set by the user when executing an SQL SELECT statement.</summary>
		/// <returns>An array of <see cref="T:System.Data.IDataParameter" /> objects that contains the parameters set by the user.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600280A RID: 10250 RVA: 0x000B0A3C File Offset: 0x000AEC3C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public override IDataParameter[] GetFillParameters()
		{
			IDataParameter[] array = null;
			IDbCommand selectCommand = this._IDbDataAdapter.SelectCommand;
			if (selectCommand != null)
			{
				IDataParameterCollection parameters = selectCommand.Parameters;
				if (parameters != null)
				{
					array = new IDataParameter[parameters.Count];
					parameters.CopyTo(array, 0);
				}
			}
			if (array == null)
			{
				array = Array.Empty<IDataParameter>();
			}
			return array;
		}

		// Token: 0x0600280B RID: 10251 RVA: 0x000B0A84 File Offset: 0x000AEC84
		internal DataTableMapping GetTableMapping(DataTable dataTable)
		{
			DataTableMapping dataTableMapping = null;
			int num = base.IndexOfDataSetTable(dataTable.TableName);
			if (-1 != num)
			{
				dataTableMapping = base.TableMappings[num];
			}
			if (dataTableMapping == null)
			{
				if (MissingMappingAction.Error == base.MissingMappingAction)
				{
					throw ADP.MissingTableMappingDestination(dataTable.TableName);
				}
				dataTableMapping = new DataTableMapping(dataTable.TableName, dataTable.TableName);
			}
			return dataTableMapping;
		}

		/// <summary>Initializes batching for the <see cref="T:System.Data.Common.DbDataAdapter" />.</summary>
		/// <exception cref="T:System.NotSupportedException">The adapter does not support batches. </exception>
		// Token: 0x0600280C RID: 10252 RVA: 0x00060F32 File Offset: 0x0005F132
		protected virtual void InitializeBatching()
		{
			throw ADP.NotSupported();
		}

		/// <summary>Raises the RowUpdated event of a .NET Framework data provider.</summary>
		/// <param name="value">A <see cref="T:System.Data.Common.RowUpdatedEventArgs" /> that contains the event data. </param>
		// Token: 0x0600280D RID: 10253 RVA: 0x000094D4 File Offset: 0x000076D4
		protected virtual void OnRowUpdated(RowUpdatedEventArgs value)
		{
		}

		/// <summary>Raises the RowUpdating event of a .NET Framework data provider.</summary>
		/// <param name="value">An <see cref="T:System.Data.Common.RowUpdatingEventArgs" />  that contains the event data. </param>
		// Token: 0x0600280E RID: 10254 RVA: 0x000094D4 File Offset: 0x000076D4
		protected virtual void OnRowUpdating(RowUpdatingEventArgs value)
		{
		}

		// Token: 0x0600280F RID: 10255 RVA: 0x000B0ADC File Offset: 0x000AECDC
		private void ParameterInput(IDataParameterCollection parameters, StatementType typeIndex, DataRow row, DataTableMapping mappings)
		{
			MissingMappingAction updateMappingAction = this.UpdateMappingAction;
			MissingSchemaAction updateSchemaAction = this.UpdateSchemaAction;
			foreach (object obj in parameters)
			{
				IDataParameter dataParameter = (IDataParameter)obj;
				if (dataParameter != null && (ParameterDirection.Input & dataParameter.Direction) != (ParameterDirection)0)
				{
					string sourceColumn = dataParameter.SourceColumn;
					if (!string.IsNullOrEmpty(sourceColumn))
					{
						DataColumn dataColumn = mappings.GetDataColumn(sourceColumn, null, row.Table, updateMappingAction, updateSchemaAction);
						if (dataColumn != null)
						{
							DataRowVersion parameterSourceVersion = DbDataAdapter.GetParameterSourceVersion(typeIndex, dataParameter);
							dataParameter.Value = row[dataColumn, parameterSourceVersion];
						}
						else
						{
							dataParameter.Value = null;
						}
						DbParameter dbParameter = dataParameter as DbParameter;
						if (dbParameter != null && dbParameter.SourceColumnNullMapping)
						{
							dataParameter.Value = (ADP.IsNull(dataParameter.Value) ? DbDataAdapter.s_parameterValueNullValue : DbDataAdapter.s_parameterValueNonNullValue);
						}
					}
				}
			}
		}

		// Token: 0x06002810 RID: 10256 RVA: 0x000B0BD0 File Offset: 0x000AEDD0
		private void ParameterOutput(IDataParameter parameter, DataRow row, DataTableMapping mappings, MissingMappingAction missingMapping, MissingSchemaAction missingSchema)
		{
			if ((ParameterDirection.Output & parameter.Direction) != (ParameterDirection)0)
			{
				object value = parameter.Value;
				if (value != null)
				{
					string sourceColumn = parameter.SourceColumn;
					if (!string.IsNullOrEmpty(sourceColumn))
					{
						DataColumn dataColumn = mappings.GetDataColumn(sourceColumn, null, row.Table, missingMapping, missingSchema);
						if (dataColumn != null)
						{
							if (dataColumn.ReadOnly)
							{
								try
								{
									dataColumn.ReadOnly = false;
									row[dataColumn] = value;
									return;
								}
								finally
								{
									dataColumn.ReadOnly = true;
								}
							}
							row[dataColumn] = value;
						}
					}
				}
			}
		}

		// Token: 0x06002811 RID: 10257 RVA: 0x000B0C50 File Offset: 0x000AEE50
		private void ParameterOutput(IDataParameterCollection parameters, DataRow row, DataTableMapping mappings)
		{
			MissingMappingAction updateMappingAction = this.UpdateMappingAction;
			MissingSchemaAction updateSchemaAction = this.UpdateSchemaAction;
			foreach (object obj in parameters)
			{
				IDataParameter dataParameter = (IDataParameter)obj;
				if (dataParameter != null)
				{
					this.ParameterOutput(dataParameter, row, mappings, updateMappingAction, updateSchemaAction);
				}
			}
		}

		/// <summary>Ends batching for the <see cref="T:System.Data.Common.DbDataAdapter" />.</summary>
		/// <exception cref="T:System.NotSupportedException">The adapter does not support batches. </exception>
		// Token: 0x06002812 RID: 10258 RVA: 0x00060F32 File Offset: 0x0005F132
		protected virtual void TerminateBatching()
		{
			throw ADP.NotSupported();
		}

		/// <summary>Updates the values in the database by executing the respective INSERT, UPDATE, or DELETE statements for each inserted, updated, or deleted row in the specified <see cref="T:System.Data.DataSet" />.</summary>
		/// <returns>The number of rows successfully updated from the <see cref="T:System.Data.DataSet" />.</returns>
		/// <param name="dataSet">The <see cref="T:System.Data.DataSet" /> used to update the data source. </param>
		/// <exception cref="T:System.InvalidOperationException">The source table is invalid. </exception>
		/// <exception cref="T:System.Data.DBConcurrencyException">An attempt to execute an INSERT, UPDATE, or DELETE statement resulted in zero records affected. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06002813 RID: 10259 RVA: 0x000B0CBC File Offset: 0x000AEEBC
		public override int Update(DataSet dataSet)
		{
			return this.Update(dataSet, "Table");
		}

		/// <summary>Updates the values in the database by executing the respective INSERT, UPDATE, or DELETE statements for each inserted, updated, or deleted row in the specified array in the <see cref="T:System.Data.DataSet" />.</summary>
		/// <returns>The number of rows successfully updated from the <see cref="T:System.Data.DataSet" />.</returns>
		/// <param name="dataRows">An array of <see cref="T:System.Data.DataRow" /> objects used to update the data source. </param>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Data.DataSet" /> is invalid. </exception>
		/// <exception cref="T:System.InvalidOperationException">The source table is invalid. </exception>
		/// <exception cref="T:System.SystemException">No <see cref="T:System.Data.DataRow" /> exists to update.-or- No <see cref="T:System.Data.DataTable" /> exists to update.-or- No <see cref="T:System.Data.DataSet" /> exists to use as a source. </exception>
		/// <exception cref="T:System.Data.DBConcurrencyException">An attempt to execute an INSERT, UPDATE, or DELETE statement resulted in zero records affected. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06002814 RID: 10260 RVA: 0x000B0CCC File Offset: 0x000AEECC
		public int Update(DataRow[] dataRows)
		{
			long num = DataCommonEventSource.Log.EnterScope<int>("<comm.DbDataAdapter.Update|API> {0}, dataRows[]", base.ObjectID);
			int num3;
			try
			{
				int num2 = 0;
				if (dataRows == null)
				{
					throw ADP.ArgumentNull("dataRows");
				}
				if (dataRows.Length != 0)
				{
					DataTable dataTable = null;
					for (int i = 0; i < dataRows.Length; i++)
					{
						if (dataRows[i] != null && dataTable != dataRows[i].Table)
						{
							if (dataTable != null)
							{
								throw ADP.UpdateMismatchRowTable(i);
							}
							dataTable = dataRows[i].Table;
						}
					}
					if (dataTable != null)
					{
						DataTableMapping tableMapping = this.GetTableMapping(dataTable);
						num2 = this.Update(dataRows, tableMapping);
					}
				}
				num3 = num2;
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
			return num3;
		}

		/// <summary>Updates the values in the database by executing the respective INSERT, UPDATE, or DELETE statements for each inserted, updated, or deleted row in the specified <see cref="T:System.Data.DataTable" />.</summary>
		/// <returns>The number of rows successfully updated from the <see cref="T:System.Data.DataTable" />.</returns>
		/// <param name="dataTable">The <see cref="T:System.Data.DataTable" /> used to update the data source. </param>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Data.DataSet" /> is invalid. </exception>
		/// <exception cref="T:System.InvalidOperationException">The source table is invalid. </exception>
		/// <exception cref="T:System.SystemException">No <see cref="T:System.Data.DataRow" /> exists to update.-or- No <see cref="T:System.Data.DataTable" /> exists to update.-or- No <see cref="T:System.Data.DataSet" /> exists to use as a source. </exception>
		/// <exception cref="T:System.Data.DBConcurrencyException">An attempt to execute an INSERT, UPDATE, or DELETE statement resulted in zero records affected. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06002815 RID: 10261 RVA: 0x000B0D70 File Offset: 0x000AEF70
		public int Update(DataTable dataTable)
		{
			long num = DataCommonEventSource.Log.EnterScope<int>("<comm.DbDataAdapter.Update|API> {0}, dataTable", base.ObjectID);
			int num3;
			try
			{
				if (dataTable == null)
				{
					throw ADP.UpdateRequiresDataTable("dataTable");
				}
				DataTableMapping dataTableMapping = null;
				int num2 = base.IndexOfDataSetTable(dataTable.TableName);
				if (-1 != num2)
				{
					dataTableMapping = base.TableMappings[num2];
				}
				if (dataTableMapping == null)
				{
					if (MissingMappingAction.Error == base.MissingMappingAction)
					{
						throw ADP.MissingTableMappingDestination(dataTable.TableName);
					}
					dataTableMapping = new DataTableMapping("Table", dataTable.TableName);
				}
				num3 = this.UpdateFromDataTable(dataTable, dataTableMapping);
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
			return num3;
		}

		/// <summary>Updates the values in the database by executing the respective INSERT, UPDATE, or DELETE statements for each inserted, updated, or deleted row in the <see cref="T:System.Data.DataSet" />  with the specified <see cref="T:System.Data.DataTable" /> name.</summary>
		/// <returns>The number of rows successfully updated from the <see cref="T:System.Data.DataSet" />.</returns>
		/// <param name="dataSet">The <see cref="T:System.Data.DataSet" /> to use to update the data source. </param>
		/// <param name="srcTable">The name of the source table to use for table mapping. </param>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Data.DataSet" /> is invalid. </exception>
		/// <exception cref="T:System.InvalidOperationException">The source table is invalid. </exception>
		/// <exception cref="T:System.Data.DBConcurrencyException">An attempt to execute an INSERT, UPDATE, or DELETE statement resulted in zero records affected. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06002816 RID: 10262 RVA: 0x000B0E14 File Offset: 0x000AF014
		public int Update(DataSet dataSet, string srcTable)
		{
			long num = DataCommonEventSource.Log.EnterScope<int, string>("<comm.DbDataAdapter.Update|API> {0}, dataSet, srcTable='{1}'", base.ObjectID, srcTable);
			int num3;
			try
			{
				if (dataSet == null)
				{
					throw ADP.UpdateRequiresNonNullDataSet("dataSet");
				}
				if (string.IsNullOrEmpty(srcTable))
				{
					throw ADP.UpdateRequiresSourceTableName("srcTable");
				}
				int num2 = 0;
				MissingMappingAction updateMappingAction = this.UpdateMappingAction;
				DataTableMapping tableMappingBySchemaAction = base.GetTableMappingBySchemaAction(srcTable, srcTable, this.UpdateMappingAction);
				MissingSchemaAction updateSchemaAction = this.UpdateSchemaAction;
				DataTable dataTableBySchemaAction = tableMappingBySchemaAction.GetDataTableBySchemaAction(dataSet, updateSchemaAction);
				if (dataTableBySchemaAction != null)
				{
					num2 = this.UpdateFromDataTable(dataTableBySchemaAction, tableMappingBySchemaAction);
				}
				else if (!base.HasTableMappings() || -1 == base.TableMappings.IndexOf(tableMappingBySchemaAction))
				{
					throw ADP.UpdateRequiresSourceTable(srcTable);
				}
				num3 = num2;
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
			return num3;
		}

		/// <summary>Updates the values in the database by executing the respective INSERT, UPDATE, or DELETE statements for each inserted, updated, or deleted row in the specified array of <see cref="T:System.Data.DataSet" /> objects.</summary>
		/// <returns>The number of rows successfully updated from the <see cref="T:System.Data.DataSet" />.</returns>
		/// <param name="dataRows">An array of <see cref="T:System.Data.DataRow" /> objects used to update the data source. </param>
		/// <param name="tableMapping">The <see cref="P:System.Data.IDataAdapter.TableMappings" /> collection to use. </param>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Data.DataSet" /> is invalid. </exception>
		/// <exception cref="T:System.InvalidOperationException">The source table is invalid. </exception>
		/// <exception cref="T:System.SystemException">No <see cref="T:System.Data.DataRow" /> exists to update.-or- No <see cref="T:System.Data.DataTable" /> exists to update.-or- No <see cref="T:System.Data.DataSet" /> exists to use as a source. </exception>
		/// <exception cref="T:System.Data.DBConcurrencyException">An attempt to execute an INSERT, UPDATE, or DELETE statement resulted in zero records affected. </exception>
		// Token: 0x06002817 RID: 10263 RVA: 0x000B0ED4 File Offset: 0x000AF0D4
		protected virtual int Update(DataRow[] dataRows, DataTableMapping tableMapping)
		{
			long num = DataCommonEventSource.Log.EnterScope<int>("<comm.DbDataAdapter.Update|API> {0}, dataRows[], tableMapping", base.ObjectID);
			int i;
			try
			{
				int num2 = 0;
				IDbConnection[] array = new IDbConnection[5];
				ConnectionState[] array2 = new ConnectionState[5];
				bool flag = false;
				IDbCommand selectCommand = this._IDbDataAdapter.SelectCommand;
				if (selectCommand != null)
				{
					array[0] = selectCommand.Connection;
					if (array[0] != null)
					{
						array2[0] = array[0].State;
						flag = true;
					}
				}
				int num3 = Math.Min(this.UpdateBatchSize, dataRows.Length);
				if (num3 < 1)
				{
					num3 = dataRows.Length;
				}
				DbDataAdapter.BatchCommandInfo[] array3 = new DbDataAdapter.BatchCommandInfo[num3];
				DataRow[] array4 = new DataRow[num3];
				int num4 = 0;
				try
				{
					try
					{
						if (1 != num3)
						{
							this.InitializeBatching();
						}
						StatementType statementType = StatementType.Select;
						IDbCommand dbCommand = null;
						foreach (DataRow dataRow in dataRows)
						{
							if (dataRow != null)
							{
								bool flag2 = false;
								DataRowState rowState = dataRow.RowState;
								if (rowState <= DataRowState.Added)
								{
									if (rowState - DataRowState.Detached <= 1)
									{
										goto IL_059B;
									}
									if (rowState != DataRowState.Added)
									{
										goto IL_0115;
									}
									statementType = StatementType.Insert;
									dbCommand = this._IDbDataAdapter.InsertCommand;
								}
								else if (rowState != DataRowState.Deleted)
								{
									if (rowState != DataRowState.Modified)
									{
										goto IL_0115;
									}
									statementType = StatementType.Update;
									dbCommand = this._IDbDataAdapter.UpdateCommand;
								}
								else
								{
									statementType = StatementType.Delete;
									dbCommand = this._IDbDataAdapter.DeleteCommand;
								}
								RowUpdatingEventArgs rowUpdatingEventArgs = this.CreateRowUpdatingEvent(dataRow, dbCommand, statementType, tableMapping);
								try
								{
									dataRow.RowError = null;
									if (dbCommand != null)
									{
										this.ParameterInput(dbCommand.Parameters, statementType, dataRow, tableMapping);
									}
								}
								catch (Exception ex) when (ADP.IsCatchableExceptionType(ex))
								{
									ADP.TraceExceptionForCapture(ex);
									rowUpdatingEventArgs.Errors = ex;
									rowUpdatingEventArgs.Status = UpdateStatus.ErrorsOccurred;
								}
								this.OnRowUpdating(rowUpdatingEventArgs);
								IDbCommand command = rowUpdatingEventArgs.Command;
								flag2 = dbCommand != command;
								dbCommand = command;
								UpdateStatus status = rowUpdatingEventArgs.Status;
								if (status != UpdateStatus.Continue)
								{
									if (UpdateStatus.ErrorsOccurred == status)
									{
										this.UpdatingRowStatusErrors(rowUpdatingEventArgs, dataRow);
										goto IL_059B;
									}
									if (UpdateStatus.SkipCurrentRow == status)
									{
										if (DataRowState.Unchanged == dataRow.RowState)
										{
											num2++;
											goto IL_059B;
										}
										goto IL_059B;
									}
									else
									{
										if (UpdateStatus.SkipAllRemainingRows != status)
										{
											throw ADP.InvalidUpdateStatus(status);
										}
										if (DataRowState.Unchanged == dataRow.RowState)
										{
											num2++;
											break;
										}
										break;
									}
								}
								else
								{
									rowUpdatingEventArgs = null;
									RowUpdatedEventArgs rowUpdatedEventArgs = null;
									if (1 == num3)
									{
										if (dbCommand != null)
										{
											array3[0]._commandIdentifier = 0;
											array3[0]._parameterCount = dbCommand.Parameters.Count;
											array3[0]._statementType = statementType;
											array3[0]._updatedRowSource = dbCommand.UpdatedRowSource;
										}
										array3[0]._row = dataRow;
										array4[0] = dataRow;
										num4 = 1;
									}
									else
									{
										Exception ex2 = null;
										try
										{
											if (dbCommand != null)
											{
												if ((UpdateRowSource.FirstReturnedRecord & dbCommand.UpdatedRowSource) == UpdateRowSource.None)
												{
													array3[num4]._commandIdentifier = this.AddToBatch(dbCommand);
													array3[num4]._parameterCount = dbCommand.Parameters.Count;
													array3[num4]._row = dataRow;
													array3[num4]._statementType = statementType;
													array3[num4]._updatedRowSource = dbCommand.UpdatedRowSource;
													array4[num4] = dataRow;
													num4++;
													if (num4 < num3)
													{
														goto IL_059B;
													}
												}
												else
												{
													ex2 = ADP.ResultsNotAllowedDuringBatch();
												}
											}
											else
											{
												ex2 = ADP.UpdateRequiresCommand(statementType, flag2);
											}
										}
										catch (Exception ex3) when (ADP.IsCatchableExceptionType(ex3))
										{
											ADP.TraceExceptionForCapture(ex3);
											ex2 = ex3;
										}
										if (ex2 != null)
										{
											rowUpdatedEventArgs = this.CreateRowUpdatedEvent(dataRow, dbCommand, StatementType.Batch, tableMapping);
											rowUpdatedEventArgs.Errors = ex2;
											rowUpdatedEventArgs.Status = UpdateStatus.ErrorsOccurred;
											this.OnRowUpdated(rowUpdatedEventArgs);
											if (ex2 != rowUpdatedEventArgs.Errors)
											{
												for (int j = 0; j < array3.Length; j++)
												{
													array3[j]._errors = null;
												}
											}
											num2 += this.UpdatedRowStatus(rowUpdatedEventArgs, array3, num4);
											if (UpdateStatus.SkipAllRemainingRows == rowUpdatedEventArgs.Status)
											{
												break;
											}
											goto IL_059B;
										}
									}
									rowUpdatedEventArgs = this.CreateRowUpdatedEvent(dataRow, dbCommand, statementType, tableMapping);
									try
									{
										if (1 != num3)
										{
											IDbConnection connection = DbDataAdapter.GetConnection1(this);
											ConnectionState connectionState = this.UpdateConnectionOpen(connection, StatementType.Batch, array, array2, flag);
											rowUpdatedEventArgs.AdapterInit(array4);
											if (ConnectionState.Open == connectionState)
											{
												this.UpdateBatchExecute(array3, num4, rowUpdatedEventArgs);
											}
											else
											{
												rowUpdatedEventArgs.Errors = ADP.UpdateOpenConnectionRequired(StatementType.Batch, false, connectionState);
												rowUpdatedEventArgs.Status = UpdateStatus.ErrorsOccurred;
											}
										}
										else if (dbCommand != null)
										{
											IDbConnection connection2 = DbDataAdapter.GetConnection4(this, dbCommand, statementType, flag2);
											ConnectionState connectionState2 = this.UpdateConnectionOpen(connection2, statementType, array, array2, flag);
											if (ConnectionState.Open == connectionState2)
											{
												this.UpdateRowExecute(rowUpdatedEventArgs, dbCommand, statementType);
												array3[0]._recordsAffected = new int?(rowUpdatedEventArgs.RecordsAffected);
												array3[0]._errors = null;
											}
											else
											{
												rowUpdatedEventArgs.Errors = ADP.UpdateOpenConnectionRequired(statementType, flag2, connectionState2);
												rowUpdatedEventArgs.Status = UpdateStatus.ErrorsOccurred;
											}
										}
										else
										{
											rowUpdatedEventArgs.Errors = ADP.UpdateRequiresCommand(statementType, flag2);
											rowUpdatedEventArgs.Status = UpdateStatus.ErrorsOccurred;
										}
									}
									catch (Exception ex4) when (ADP.IsCatchableExceptionType(ex4))
									{
										ADP.TraceExceptionForCapture(ex4);
										rowUpdatedEventArgs.Errors = ex4;
										rowUpdatedEventArgs.Status = UpdateStatus.ErrorsOccurred;
									}
									bool flag3 = UpdateStatus.ErrorsOccurred == rowUpdatedEventArgs.Status;
									Exception errors = rowUpdatedEventArgs.Errors;
									this.OnRowUpdated(rowUpdatedEventArgs);
									if (errors != rowUpdatedEventArgs.Errors)
									{
										for (int k = 0; k < array3.Length; k++)
										{
											array3[k]._errors = null;
										}
									}
									num2 += this.UpdatedRowStatus(rowUpdatedEventArgs, array3, num4);
									if (UpdateStatus.SkipAllRemainingRows != rowUpdatedEventArgs.Status)
									{
										if (1 != num3)
										{
											this.ClearBatch();
											num4 = 0;
										}
										for (int l = 0; l < array3.Length; l++)
										{
											array3[l] = default(DbDataAdapter.BatchCommandInfo);
										}
										num4 = 0;
										goto IL_059B;
									}
									if (flag3 && 1 != num3)
									{
										this.ClearBatch();
										num4 = 0;
										break;
									}
									break;
								}
								IL_0115:
								throw ADP.InvalidDataRowState(dataRow.RowState);
							}
							IL_059B:;
						}
						if (1 != num3 && 0 < num4)
						{
							RowUpdatedEventArgs rowUpdatedEventArgs2 = this.CreateRowUpdatedEvent(null, dbCommand, statementType, tableMapping);
							try
							{
								IDbConnection connection3 = DbDataAdapter.GetConnection1(this);
								ConnectionState connectionState3 = this.UpdateConnectionOpen(connection3, StatementType.Batch, array, array2, flag);
								DataRow[] array5 = array4;
								if (num4 < array4.Length)
								{
									array5 = new DataRow[num4];
									Array.Copy(array4, 0, array5, 0, num4);
								}
								rowUpdatedEventArgs2.AdapterInit(array5);
								if (ConnectionState.Open == connectionState3)
								{
									this.UpdateBatchExecute(array3, num4, rowUpdatedEventArgs2);
								}
								else
								{
									rowUpdatedEventArgs2.Errors = ADP.UpdateOpenConnectionRequired(StatementType.Batch, false, connectionState3);
									rowUpdatedEventArgs2.Status = UpdateStatus.ErrorsOccurred;
								}
							}
							catch (Exception ex5) when (ADP.IsCatchableExceptionType(ex5))
							{
								ADP.TraceExceptionForCapture(ex5);
								rowUpdatedEventArgs2.Errors = ex5;
								rowUpdatedEventArgs2.Status = UpdateStatus.ErrorsOccurred;
							}
							Exception errors2 = rowUpdatedEventArgs2.Errors;
							this.OnRowUpdated(rowUpdatedEventArgs2);
							if (errors2 != rowUpdatedEventArgs2.Errors)
							{
								for (int m = 0; m < array3.Length; m++)
								{
									array3[m]._errors = null;
								}
							}
							num2 += this.UpdatedRowStatus(rowUpdatedEventArgs2, array3, num4);
						}
					}
					finally
					{
						if (1 != num3)
						{
							this.TerminateBatching();
						}
					}
				}
				finally
				{
					for (int n = 0; n < array.Length; n++)
					{
						DbDataAdapter.QuietClose(array[n], array2[n]);
					}
				}
				i = num2;
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
			return i;
		}

		// Token: 0x06002818 RID: 10264 RVA: 0x000B1688 File Offset: 0x000AF888
		private void UpdateBatchExecute(DbDataAdapter.BatchCommandInfo[] batchCommands, int commandCount, RowUpdatedEventArgs rowUpdatedEvent)
		{
			try
			{
				int num = this.ExecuteBatch();
				rowUpdatedEvent.AdapterInit(num);
			}
			catch (DbException ex)
			{
				ADP.TraceExceptionForCapture(ex);
				rowUpdatedEvent.Errors = ex;
				rowUpdatedEvent.Status = UpdateStatus.ErrorsOccurred;
			}
			MissingMappingAction updateMappingAction = this.UpdateMappingAction;
			MissingSchemaAction updateSchemaAction = this.UpdateSchemaAction;
			int num2 = 0;
			bool flag = false;
			List<DataRow> list = null;
			for (int i = 0; i < commandCount; i++)
			{
				DbDataAdapter.BatchCommandInfo batchCommandInfo = batchCommands[i];
				StatementType statementType = batchCommandInfo._statementType;
				int num3;
				if (this.GetBatchedRecordsAffected(batchCommandInfo._commandIdentifier, out num3, out batchCommands[i]._errors))
				{
					batchCommands[i]._recordsAffected = new int?(num3);
				}
				if (batchCommands[i]._errors == null && batchCommands[i]._recordsAffected != null)
				{
					if (StatementType.Update == statementType || StatementType.Delete == statementType)
					{
						num2++;
						if (num3 == 0)
						{
							if (list == null)
							{
								list = new List<DataRow>();
							}
							batchCommands[i]._errors = ADP.UpdateConcurrencyViolation(batchCommands[i]._statementType, 0, 1, new DataRow[] { rowUpdatedEvent.Rows[i] });
							flag = true;
							list.Add(rowUpdatedEvent.Rows[i]);
						}
					}
					if ((StatementType.Insert == statementType || StatementType.Update == statementType) && (UpdateRowSource.OutputParameters & batchCommandInfo._updatedRowSource) != UpdateRowSource.None && num3 != 0)
					{
						if (StatementType.Insert == statementType)
						{
							rowUpdatedEvent.Rows[i].AcceptChanges();
						}
						for (int j = 0; j < batchCommandInfo._parameterCount; j++)
						{
							IDataParameter batchedParameter = this.GetBatchedParameter(batchCommandInfo._commandIdentifier, j);
							this.ParameterOutput(batchedParameter, batchCommandInfo._row, rowUpdatedEvent.TableMapping, updateMappingAction, updateSchemaAction);
						}
					}
				}
			}
			if (rowUpdatedEvent.Errors == null && rowUpdatedEvent.Status == UpdateStatus.Continue && 0 < num2 && (rowUpdatedEvent.RecordsAffected == 0 || flag))
			{
				DataRow[] array = ((list != null) ? list.ToArray() : rowUpdatedEvent.Rows);
				rowUpdatedEvent.Errors = ADP.UpdateConcurrencyViolation(StatementType.Batch, commandCount - array.Length, commandCount, array);
				rowUpdatedEvent.Status = UpdateStatus.ErrorsOccurred;
			}
		}

		// Token: 0x06002819 RID: 10265 RVA: 0x000B1888 File Offset: 0x000AFA88
		private ConnectionState UpdateConnectionOpen(IDbConnection connection, StatementType statementType, IDbConnection[] connections, ConnectionState[] connectionStates, bool useSelectConnectionState)
		{
			if (connection != connections[(int)statementType])
			{
				DbDataAdapter.QuietClose(connections[(int)statementType], connectionStates[(int)statementType]);
				connections[(int)statementType] = connection;
				connectionStates[(int)statementType] = ConnectionState.Closed;
				DbDataAdapter.QuietOpen(connection, out connectionStates[(int)statementType]);
				if (useSelectConnectionState && connections[0] == connection)
				{
					connectionStates[(int)statementType] = connections[0].State;
				}
			}
			return connection.State;
		}

		// Token: 0x0600281A RID: 10266 RVA: 0x000B18DC File Offset: 0x000AFADC
		private int UpdateFromDataTable(DataTable dataTable, DataTableMapping tableMapping)
		{
			int num = 0;
			DataRow[] array = ADP.SelectAdapterRows(dataTable, false);
			if (array != null && array.Length != 0)
			{
				num = this.Update(array, tableMapping);
			}
			return num;
		}

		// Token: 0x0600281B RID: 10267 RVA: 0x000B1904 File Offset: 0x000AFB04
		private void UpdateRowExecute(RowUpdatedEventArgs rowUpdatedEvent, IDbCommand dataCommand, StatementType cmdIndex)
		{
			bool flag = true;
			UpdateRowSource updatedRowSource = dataCommand.UpdatedRowSource;
			if (StatementType.Delete == cmdIndex || (UpdateRowSource.FirstReturnedRecord & updatedRowSource) == UpdateRowSource.None)
			{
				int num = dataCommand.ExecuteNonQuery();
				rowUpdatedEvent.AdapterInit(num);
			}
			else if (StatementType.Insert == cmdIndex || StatementType.Update == cmdIndex)
			{
				using (IDataReader dataReader = dataCommand.ExecuteReader(CommandBehavior.SequentialAccess))
				{
					DataReaderContainer dataReaderContainer = DataReaderContainer.Create(dataReader, this.ReturnProviderSpecificTypes);
					try
					{
						bool flag2 = false;
						while (0 >= dataReaderContainer.FieldCount)
						{
							if (!dataReader.NextResult())
							{
								IL_0061:
								if (flag2 && dataReader.RecordsAffected != 0)
								{
									SchemaMapping schemaMapping = new SchemaMapping(this, null, rowUpdatedEvent.Row.Table, dataReaderContainer, false, SchemaType.Mapped, rowUpdatedEvent.TableMapping.SourceTable, true, null, null);
									if (schemaMapping.DataTable != null && schemaMapping.DataValues != null && dataReader.Read())
									{
										if (StatementType.Insert == cmdIndex && flag)
										{
											rowUpdatedEvent.Row.AcceptChanges();
											flag = false;
										}
										schemaMapping.ApplyToDataRow(rowUpdatedEvent.Row);
									}
								}
								goto IL_00F2;
							}
						}
						flag2 = true;
						goto IL_0061;
					}
					finally
					{
						dataReader.Close();
						int recordsAffected = dataReader.RecordsAffected;
						rowUpdatedEvent.AdapterInit(recordsAffected);
					}
				}
			}
			IL_00F2:
			if ((StatementType.Insert == cmdIndex || StatementType.Update == cmdIndex) && (UpdateRowSource.OutputParameters & updatedRowSource) != UpdateRowSource.None && rowUpdatedEvent.RecordsAffected != 0)
			{
				if (StatementType.Insert == cmdIndex && flag)
				{
					rowUpdatedEvent.Row.AcceptChanges();
				}
				this.ParameterOutput(dataCommand.Parameters, rowUpdatedEvent.Row, rowUpdatedEvent.TableMapping);
			}
			if (rowUpdatedEvent.Status == UpdateStatus.Continue && cmdIndex - StatementType.Update <= 1 && rowUpdatedEvent.RecordsAffected == 0)
			{
				rowUpdatedEvent.Errors = ADP.UpdateConcurrencyViolation(cmdIndex, rowUpdatedEvent.RecordsAffected, 1, new DataRow[] { rowUpdatedEvent.Row });
				rowUpdatedEvent.Status = UpdateStatus.ErrorsOccurred;
			}
		}

		// Token: 0x0600281C RID: 10268 RVA: 0x000B1AA0 File Offset: 0x000AFCA0
		private int UpdatedRowStatus(RowUpdatedEventArgs rowUpdatedEvent, DbDataAdapter.BatchCommandInfo[] batchCommands, int commandCount)
		{
			int num;
			switch (rowUpdatedEvent.Status)
			{
			case UpdateStatus.Continue:
				num = this.UpdatedRowStatusContinue(rowUpdatedEvent, batchCommands, commandCount);
				break;
			case UpdateStatus.ErrorsOccurred:
				num = this.UpdatedRowStatusErrors(rowUpdatedEvent, batchCommands, commandCount);
				break;
			case UpdateStatus.SkipCurrentRow:
			case UpdateStatus.SkipAllRemainingRows:
				num = this.UpdatedRowStatusSkip(batchCommands, commandCount);
				break;
			default:
				throw ADP.InvalidUpdateStatus(rowUpdatedEvent.Status);
			}
			return num;
		}

		// Token: 0x0600281D RID: 10269 RVA: 0x000B1B00 File Offset: 0x000AFD00
		private int UpdatedRowStatusContinue(RowUpdatedEventArgs rowUpdatedEvent, DbDataAdapter.BatchCommandInfo[] batchCommands, int commandCount)
		{
			int num = 0;
			bool acceptChangesDuringUpdate = base.AcceptChangesDuringUpdate;
			for (int i = 0; i < commandCount; i++)
			{
				DataRow row = batchCommands[i]._row;
				if (batchCommands[i]._errors == null && batchCommands[i]._recordsAffected != null && batchCommands[i]._recordsAffected.Value != 0)
				{
					if (acceptChangesDuringUpdate && ((DataRowState.Added | DataRowState.Deleted | DataRowState.Modified) & row.RowState) != (DataRowState)0)
					{
						row.AcceptChanges();
					}
					num++;
				}
			}
			return num;
		}

		// Token: 0x0600281E RID: 10270 RVA: 0x000B1B7C File Offset: 0x000AFD7C
		private int UpdatedRowStatusErrors(RowUpdatedEventArgs rowUpdatedEvent, DbDataAdapter.BatchCommandInfo[] batchCommands, int commandCount)
		{
			Exception ex = rowUpdatedEvent.Errors;
			if (ex == null)
			{
				ex = ADP.RowUpdatedErrors();
				rowUpdatedEvent.Errors = ex;
			}
			int num = 0;
			bool flag = false;
			string message = ex.Message;
			for (int i = 0; i < commandCount; i++)
			{
				DataRow row = batchCommands[i]._row;
				if (batchCommands[i]._errors != null)
				{
					string text = batchCommands[i]._errors.Message;
					if (string.IsNullOrEmpty(text))
					{
						text = message;
					}
					DataRow dataRow = row;
					dataRow.RowError += text;
					flag = true;
				}
			}
			if (!flag)
			{
				for (int j = 0; j < commandCount; j++)
				{
					DataRow row2 = batchCommands[j]._row;
					row2.RowError += message;
				}
			}
			else
			{
				num = this.UpdatedRowStatusContinue(rowUpdatedEvent, batchCommands, commandCount);
			}
			if (!base.ContinueUpdateOnError)
			{
				throw ex;
			}
			return num;
		}

		// Token: 0x0600281F RID: 10271 RVA: 0x000B1C58 File Offset: 0x000AFE58
		private int UpdatedRowStatusSkip(DbDataAdapter.BatchCommandInfo[] batchCommands, int commandCount)
		{
			int num = 0;
			for (int i = 0; i < commandCount; i++)
			{
				DataRow row = batchCommands[i]._row;
				if (((DataRowState.Detached | DataRowState.Unchanged) & row.RowState) != (DataRowState)0)
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x06002820 RID: 10272 RVA: 0x000B1C90 File Offset: 0x000AFE90
		private void UpdatingRowStatusErrors(RowUpdatingEventArgs rowUpdatedEvent, DataRow dataRow)
		{
			Exception ex = rowUpdatedEvent.Errors;
			if (ex == null)
			{
				ex = ADP.RowUpdatingErrors();
				rowUpdatedEvent.Errors = ex;
			}
			string message = ex.Message;
			dataRow.RowError += message;
			if (!base.ContinueUpdateOnError)
			{
				throw ex;
			}
		}

		// Token: 0x06002821 RID: 10273 RVA: 0x000B1CD8 File Offset: 0x000AFED8
		private static IDbConnection GetConnection1(DbDataAdapter adapter)
		{
			IDbCommand dbCommand = adapter._IDbDataAdapter.SelectCommand;
			if (dbCommand == null)
			{
				dbCommand = adapter._IDbDataAdapter.InsertCommand;
				if (dbCommand == null)
				{
					dbCommand = adapter._IDbDataAdapter.UpdateCommand;
					if (dbCommand == null)
					{
						dbCommand = adapter._IDbDataAdapter.DeleteCommand;
					}
				}
			}
			IDbConnection dbConnection = null;
			if (dbCommand != null)
			{
				dbConnection = dbCommand.Connection;
			}
			if (dbConnection == null)
			{
				throw ADP.UpdateConnectionRequired(StatementType.Batch, false);
			}
			return dbConnection;
		}

		// Token: 0x06002822 RID: 10274 RVA: 0x000B1D38 File Offset: 0x000AFF38
		private static IDbConnection GetConnection3(DbDataAdapter adapter, IDbCommand command, string method)
		{
			IDbConnection connection = command.Connection;
			if (connection == null)
			{
				throw ADP.ConnectionRequired_Res(method);
			}
			return connection;
		}

		// Token: 0x06002823 RID: 10275 RVA: 0x000B1D58 File Offset: 0x000AFF58
		private static IDbConnection GetConnection4(DbDataAdapter adapter, IDbCommand command, StatementType statementType, bool isCommandFromRowUpdating)
		{
			IDbConnection connection = command.Connection;
			if (connection == null)
			{
				throw ADP.UpdateConnectionRequired(statementType, isCommandFromRowUpdating);
			}
			return connection;
		}

		// Token: 0x06002824 RID: 10276 RVA: 0x000B1D78 File Offset: 0x000AFF78
		private static DataRowVersion GetParameterSourceVersion(StatementType statementType, IDataParameter parameter)
		{
			switch (statementType)
			{
			case StatementType.Select:
			case StatementType.Batch:
				throw ADP.UnwantedStatementType(statementType);
			case StatementType.Insert:
				return DataRowVersion.Current;
			case StatementType.Update:
				return parameter.SourceVersion;
			case StatementType.Delete:
				return DataRowVersion.Original;
			default:
				throw ADP.InvalidStatementType(statementType);
			}
		}

		// Token: 0x06002825 RID: 10277 RVA: 0x000B1DB6 File Offset: 0x000AFFB6
		private static void QuietClose(IDbConnection connection, ConnectionState originalState)
		{
			if (connection != null && originalState == ConnectionState.Closed)
			{
				connection.Close();
			}
		}

		// Token: 0x06002826 RID: 10278 RVA: 0x000B1DC4 File Offset: 0x000AFFC4
		private static void QuietOpen(IDbConnection connection, out ConnectionState originalState)
		{
			originalState = connection.State;
			if (originalState == ConnectionState.Closed)
			{
				connection.Open();
			}
		}

		/// <summary>The default name used by the <see cref="T:System.Data.Common.DataAdapter" /> object for table mappings.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04001924 RID: 6436
		public const string DefaultSourceTableName = "Table";

		// Token: 0x04001925 RID: 6437
		internal static readonly object s_parameterValueNonNullValue = 0;

		// Token: 0x04001926 RID: 6438
		internal static readonly object s_parameterValueNullValue = 1;

		// Token: 0x04001927 RID: 6439
		private IDbCommand _deleteCommand;

		// Token: 0x04001928 RID: 6440
		private IDbCommand _insertCommand;

		// Token: 0x04001929 RID: 6441
		private IDbCommand _selectCommand;

		// Token: 0x0400192A RID: 6442
		private IDbCommand _updateCommand;

		// Token: 0x0400192B RID: 6443
		private CommandBehavior _fillCommandBehavior;

		// Token: 0x0200033D RID: 829
		private struct BatchCommandInfo
		{
			// Token: 0x0400192C RID: 6444
			internal int _commandIdentifier;

			// Token: 0x0400192D RID: 6445
			internal int _parameterCount;

			// Token: 0x0400192E RID: 6446
			internal DataRow _row;

			// Token: 0x0400192F RID: 6447
			internal StatementType _statementType;

			// Token: 0x04001930 RID: 6448
			internal UpdateRowSource _updatedRowSource;

			// Token: 0x04001931 RID: 6449
			internal int? _recordsAffected;

			// Token: 0x04001932 RID: 6450
			internal Exception _errors;
		}
	}
}
