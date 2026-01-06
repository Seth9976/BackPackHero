using System;
using System.ComponentModel;
using System.Data.ProviderBase;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Threading;

namespace System.Data.Common
{
	/// <summary>Represents a set of SQL commands and a database connection that are used to fill the <see cref="T:System.Data.DataSet" /> and update the data source.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000328 RID: 808
	public class DataAdapter : Component, IDataAdapter
	{
		// Token: 0x060025D5 RID: 9685 RVA: 0x000094D4 File Offset: 0x000076D4
		[Conditional("DEBUG")]
		private void AssertReaderHandleFieldCount(DataReaderContainer readerHandler)
		{
		}

		// Token: 0x060025D6 RID: 9686 RVA: 0x000094D4 File Offset: 0x000076D4
		[Conditional("DEBUG")]
		private void AssertSchemaMapping(SchemaMapping mapping)
		{
		}

		/// <summary>Initializes a new instance of a <see cref="T:System.Data.Common.DataAdapter" /> class.</summary>
		// Token: 0x060025D7 RID: 9687 RVA: 0x000AB4F0 File Offset: 0x000A96F0
		protected DataAdapter()
		{
			GC.SuppressFinalize(this);
		}

		/// <summary>Initializes a new instance of a <see cref="T:System.Data.Common.DataAdapter" /> class from an existing object of the same type.</summary>
		/// <param name="from">A <see cref="T:System.Data.Common.DataAdapter" /> object used to create the new <see cref="T:System.Data.Common.DataAdapter" />. </param>
		// Token: 0x060025D8 RID: 9688 RVA: 0x000AB53C File Offset: 0x000A973C
		protected DataAdapter(DataAdapter from)
		{
			this.CloneFrom(from);
		}

		/// <summary>Gets or sets a value indicating whether <see cref="M:System.Data.DataRow.AcceptChanges" /> is called on a <see cref="T:System.Data.DataRow" /> after it is added to the <see cref="T:System.Data.DataTable" /> during any of the Fill operations.</summary>
		/// <returns>true if <see cref="M:System.Data.DataRow.AcceptChanges" /> is called on the <see cref="T:System.Data.DataRow" />; otherwise false. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700066C RID: 1644
		// (get) Token: 0x060025D9 RID: 9689 RVA: 0x000AB589 File Offset: 0x000A9789
		// (set) Token: 0x060025DA RID: 9690 RVA: 0x000AB591 File Offset: 0x000A9791
		[DefaultValue(true)]
		public bool AcceptChangesDuringFill
		{
			get
			{
				return this._acceptChangesDuringFill;
			}
			set
			{
				this._acceptChangesDuringFill = value;
			}
		}

		/// <summary>Determines whether the <see cref="P:System.Data.Common.DataAdapter.AcceptChangesDuringFill" /> property should be persisted.</summary>
		/// <returns>true if the <see cref="P:System.Data.Common.DataAdapter.AcceptChangesDuringFill" /> property is persisted; otherwise false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060025DB RID: 9691 RVA: 0x000AB59A File Offset: 0x000A979A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public virtual bool ShouldSerializeAcceptChangesDuringFill()
		{
			return this._fillLoadOption == (LoadOption)0;
		}

		/// <summary>Gets or sets whether <see cref="M:System.Data.DataRow.AcceptChanges" /> is called during a <see cref="M:System.Data.Common.DataAdapter.Update(System.Data.DataSet)" />.</summary>
		/// <returns>true if <see cref="M:System.Data.DataRow.AcceptChanges" /> is called during an <see cref="M:System.Data.Common.DataAdapter.Update(System.Data.DataSet)" />; otherwise false. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700066D RID: 1645
		// (get) Token: 0x060025DC RID: 9692 RVA: 0x000AB5A5 File Offset: 0x000A97A5
		// (set) Token: 0x060025DD RID: 9693 RVA: 0x000AB5AD File Offset: 0x000A97AD
		[DefaultValue(true)]
		public bool AcceptChangesDuringUpdate
		{
			get
			{
				return this._acceptChangesDuringUpdate;
			}
			set
			{
				this._acceptChangesDuringUpdate = value;
			}
		}

		/// <summary>Gets or sets a value that specifies whether to generate an exception when an error is encountered during a row update.</summary>
		/// <returns>true to continue the update without generating an exception; otherwise false. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700066E RID: 1646
		// (get) Token: 0x060025DE RID: 9694 RVA: 0x000AB5B6 File Offset: 0x000A97B6
		// (set) Token: 0x060025DF RID: 9695 RVA: 0x000AB5BE File Offset: 0x000A97BE
		[DefaultValue(false)]
		public bool ContinueUpdateOnError
		{
			get
			{
				return this._continueUpdateOnError;
			}
			set
			{
				this._continueUpdateOnError = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Data.LoadOption" /> that determines how the adapter fills the <see cref="T:System.Data.DataTable" /> from the <see cref="T:System.Data.Common.DbDataReader" />.</summary>
		/// <returns>A <see cref="T:System.Data.LoadOption" /> value.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x1700066F RID: 1647
		// (get) Token: 0x060025E0 RID: 9696 RVA: 0x000AB5C8 File Offset: 0x000A97C8
		// (set) Token: 0x060025E1 RID: 9697 RVA: 0x000AB5E7 File Offset: 0x000A97E7
		[RefreshProperties(RefreshProperties.All)]
		public LoadOption FillLoadOption
		{
			get
			{
				if (this._fillLoadOption == (LoadOption)0)
				{
					return LoadOption.OverwriteChanges;
				}
				return this._fillLoadOption;
			}
			set
			{
				if (value <= LoadOption.Upsert)
				{
					this._fillLoadOption = value;
					return;
				}
				throw ADP.InvalidLoadOption(value);
			}
		}

		/// <summary>Resets <see cref="P:System.Data.Common.DataAdapter.FillLoadOption" /> to its default state and causes <see cref="M:System.Data.Common.DataAdapter.Fill(System.Data.DataSet)" /> to honor <see cref="P:System.Data.Common.DataAdapter.AcceptChangesDuringFill" />.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060025E2 RID: 9698 RVA: 0x000AB5FB File Offset: 0x000A97FB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void ResetFillLoadOption()
		{
			this._fillLoadOption = (LoadOption)0;
		}

		/// <summary>Determines whether the <see cref="P:System.Data.Common.DataAdapter.FillLoadOption" /> property should be persisted.</summary>
		/// <returns>true if the <see cref="P:System.Data.Common.DataAdapter.FillLoadOption" /> property is persisted; otherwise false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060025E3 RID: 9699 RVA: 0x000AB604 File Offset: 0x000A9804
		[EditorBrowsable(EditorBrowsableState.Never)]
		public virtual bool ShouldSerializeFillLoadOption()
		{
			return this._fillLoadOption > (LoadOption)0;
		}

		/// <summary>Determines the action to take when incoming data does not have a matching table or column.</summary>
		/// <returns>One of the <see cref="T:System.Data.MissingMappingAction" /> values. The default is Passthrough.</returns>
		/// <exception cref="T:System.ArgumentException">The value set is not one of the <see cref="T:System.Data.MissingMappingAction" /> values. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000670 RID: 1648
		// (get) Token: 0x060025E4 RID: 9700 RVA: 0x000AB60F File Offset: 0x000A980F
		// (set) Token: 0x060025E5 RID: 9701 RVA: 0x000AB617 File Offset: 0x000A9817
		[DefaultValue(MissingMappingAction.Passthrough)]
		public MissingMappingAction MissingMappingAction
		{
			get
			{
				return this._missingMappingAction;
			}
			set
			{
				if (value - MissingMappingAction.Passthrough <= 2)
				{
					this._missingMappingAction = value;
					return;
				}
				throw ADP.InvalidMissingMappingAction(value);
			}
		}

		/// <summary>Determines the action to take when existing <see cref="T:System.Data.DataSet" /> schema does not match incoming data.</summary>
		/// <returns>One of the <see cref="T:System.Data.MissingSchemaAction" /> values. The default is Add.</returns>
		/// <exception cref="T:System.ArgumentException">The value set is not one of the <see cref="T:System.Data.MissingSchemaAction" /> values. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000671 RID: 1649
		// (get) Token: 0x060025E6 RID: 9702 RVA: 0x000AB62D File Offset: 0x000A982D
		// (set) Token: 0x060025E7 RID: 9703 RVA: 0x000AB635 File Offset: 0x000A9835
		[DefaultValue(MissingSchemaAction.Add)]
		public MissingSchemaAction MissingSchemaAction
		{
			get
			{
				return this._missingSchemaAction;
			}
			set
			{
				if (value - MissingSchemaAction.Add <= 3)
				{
					this._missingSchemaAction = value;
					return;
				}
				throw ADP.InvalidMissingSchemaAction(value);
			}
		}

		// Token: 0x17000672 RID: 1650
		// (get) Token: 0x060025E8 RID: 9704 RVA: 0x000AB64B File Offset: 0x000A984B
		internal int ObjectID
		{
			get
			{
				return this._objectID;
			}
		}

		/// <summary>Gets or sets whether the Fill method should return provider-specific values or common CLS-compliant values.</summary>
		/// <returns>true if the Fill method should return provider-specific values; otherwise false to return common CLS-compliant values.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000673 RID: 1651
		// (get) Token: 0x060025E9 RID: 9705 RVA: 0x000AB653 File Offset: 0x000A9853
		// (set) Token: 0x060025EA RID: 9706 RVA: 0x000AB65B File Offset: 0x000A985B
		[DefaultValue(false)]
		public virtual bool ReturnProviderSpecificTypes
		{
			get
			{
				return this._returnProviderSpecificTypes;
			}
			set
			{
				this._returnProviderSpecificTypes = value;
			}
		}

		/// <summary>Gets a collection that provides the master mapping between a source table and a <see cref="T:System.Data.DataTable" />.</summary>
		/// <returns>A collection that provides the master mapping between the returned records and the <see cref="T:System.Data.DataSet" />. The default value is an empty collection.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000674 RID: 1652
		// (get) Token: 0x060025EB RID: 9707 RVA: 0x000AB664 File Offset: 0x000A9864
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public DataTableMappingCollection TableMappings
		{
			get
			{
				DataTableMappingCollection dataTableMappingCollection = this._tableMappings;
				if (dataTableMappingCollection == null)
				{
					dataTableMappingCollection = this.CreateTableMappings();
					if (dataTableMappingCollection == null)
					{
						dataTableMappingCollection = new DataTableMappingCollection();
					}
					this._tableMappings = dataTableMappingCollection;
				}
				return dataTableMappingCollection;
			}
		}

		/// <summary>Indicates how a source table is mapped to a dataset table.</summary>
		/// <returns>A collection that provides the master mapping between the returned records and the <see cref="T:System.Data.DataSet" />. The default value is an empty collection.</returns>
		// Token: 0x17000675 RID: 1653
		// (get) Token: 0x060025EC RID: 9708 RVA: 0x000AB693 File Offset: 0x000A9893
		ITableMappingCollection IDataAdapter.TableMappings
		{
			get
			{
				return this.TableMappings;
			}
		}

		/// <summary>Determines whether one or more <see cref="T:System.Data.Common.DataTableMapping" /> objects exist and they should be persisted.</summary>
		/// <returns>true if one or more <see cref="T:System.Data.Common.DataTableMapping" /> objects exist; otherwise false.</returns>
		// Token: 0x060025ED RID: 9709 RVA: 0x0000CD07 File Offset: 0x0000AF07
		protected virtual bool ShouldSerializeTableMappings()
		{
			return true;
		}

		/// <summary>Indicates whether a <see cref="T:System.Data.Common.DataTableMappingCollection" /> has been created.</summary>
		/// <returns>true if a <see cref="T:System.Data.Common.DataTableMappingCollection" /> has been created; otherwise false.</returns>
		// Token: 0x060025EE RID: 9710 RVA: 0x000AB69B File Offset: 0x000A989B
		protected bool HasTableMappings()
		{
			return this._tableMappings != null && 0 < this.TableMappings.Count;
		}

		/// <summary>Returned when an error occurs during a fill operation.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1400002C RID: 44
		// (add) Token: 0x060025EF RID: 9711 RVA: 0x000AB6B5 File Offset: 0x000A98B5
		// (remove) Token: 0x060025F0 RID: 9712 RVA: 0x000AB6CF File Offset: 0x000A98CF
		public event FillErrorEventHandler FillError
		{
			add
			{
				this._hasFillErrorHandler = true;
				base.Events.AddHandler(DataAdapter.s_eventFillError, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataAdapter.s_eventFillError, value);
			}
		}

		/// <summary>Creates a copy of this instance of <see cref="T:System.Data.Common.DataAdapter" />.</summary>
		/// <returns>The cloned instance of <see cref="T:System.Data.Common.DataAdapter" />.</returns>
		// Token: 0x060025F1 RID: 9713 RVA: 0x000AB6E2 File Offset: 0x000A98E2
		[Obsolete("CloneInternals() has been deprecated.  Use the DataAdapter(DataAdapter from) constructor.  http://go.microsoft.com/fwlink/?linkid=14202")]
		protected virtual DataAdapter CloneInternals()
		{
			DataAdapter dataAdapter = (DataAdapter)Activator.CreateInstance(base.GetType(), BindingFlags.Instance | BindingFlags.Public, null, null, CultureInfo.InvariantCulture, null);
			dataAdapter.CloneFrom(this);
			return dataAdapter;
		}

		// Token: 0x060025F2 RID: 9714 RVA: 0x000AB708 File Offset: 0x000A9908
		private void CloneFrom(DataAdapter from)
		{
			this._acceptChangesDuringUpdate = from._acceptChangesDuringUpdate;
			this._acceptChangesDuringUpdateAfterInsert = from._acceptChangesDuringUpdateAfterInsert;
			this._continueUpdateOnError = from._continueUpdateOnError;
			this._returnProviderSpecificTypes = from._returnProviderSpecificTypes;
			this._acceptChangesDuringFill = from._acceptChangesDuringFill;
			this._fillLoadOption = from._fillLoadOption;
			this._missingMappingAction = from._missingMappingAction;
			this._missingSchemaAction = from._missingSchemaAction;
			if (from._tableMappings != null && 0 < from.TableMappings.Count)
			{
				DataTableMappingCollection tableMappings = this.TableMappings;
				foreach (object obj in from.TableMappings)
				{
					tableMappings.Add((obj is ICloneable) ? ((ICloneable)obj).Clone() : obj);
				}
			}
		}

		/// <summary>Creates a new <see cref="T:System.Data.Common.DataTableMappingCollection" />.</summary>
		/// <returns>A new table mapping collection.</returns>
		// Token: 0x060025F3 RID: 9715 RVA: 0x000AB7F0 File Offset: 0x000A99F0
		protected virtual DataTableMappingCollection CreateTableMappings()
		{
			DataCommonEventSource.Log.Trace<int>("<comm.DataAdapter.CreateTableMappings|API> {0}", this.ObjectID);
			return new DataTableMappingCollection();
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Data.Common.DataAdapter" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x060025F4 RID: 9716 RVA: 0x000AB80C File Offset: 0x000A9A0C
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this._tableMappings = null;
			}
			base.Dispose(disposing);
		}

		/// <summary>Adds a <see cref="T:System.Data.DataTable" /> to the specified <see cref="T:System.Data.DataSet" /> and configures the schema to match that in the data source based on the specified <see cref="T:System.Data.SchemaType" />.</summary>
		/// <returns>A <see cref="T:System.Data.DataTable" /> object that contains schema information returned from the data source.</returns>
		/// <param name="dataSet">The <see cref="T:System.Data.DataSet" /> to be filled with the schema from the data source. </param>
		/// <param name="schemaType">One of the <see cref="T:System.Data.SchemaType" /> values. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060025F5 RID: 9717 RVA: 0x00060F32 File Offset: 0x0005F132
		public virtual DataTable[] FillSchema(DataSet dataSet, SchemaType schemaType)
		{
			throw ADP.NotSupported();
		}

		/// <summary>Adds a <see cref="T:System.Data.DataTable" /> to the specified <see cref="T:System.Data.DataSet" />.</summary>
		/// <returns>A reference to a collection of <see cref="T:System.Data.DataTable" /> objects that were added to the <see cref="T:System.Data.DataSet" />.</returns>
		/// <param name="dataSet">The <see cref="T:System.Data.DataTable" /> to be filled from the <see cref="T:System.Data.IDataReader" />.</param>
		/// <param name="schemaType">One of the <see cref="T:System.Data.SchemaType" /> values.</param>
		/// <param name="srcTable">The name of the source table to use for table mapping.</param>
		/// <param name="dataReader">The <see cref="T:System.Data.IDataReader" /> to be used as the data source when filling the <see cref="T:System.Data.DataTable" />.</param>
		// Token: 0x060025F6 RID: 9718 RVA: 0x000AB820 File Offset: 0x000A9A20
		protected virtual DataTable[] FillSchema(DataSet dataSet, SchemaType schemaType, string srcTable, IDataReader dataReader)
		{
			long num = DataCommonEventSource.Log.EnterScope<int, SchemaType>("<comm.DataAdapter.FillSchema|API> {0}, dataSet, schemaType={1}, srcTable, dataReader", this.ObjectID, schemaType);
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
				if (dataReader == null || dataReader.IsClosed)
				{
					throw ADP.FillRequires("dataReader");
				}
				array = (DataTable[])this.FillSchemaFromReader(dataSet, null, schemaType, srcTable, dataReader);
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
			return array;
		}

		/// <summary>Adds a <see cref="T:System.Data.DataTable" /> to the specified <see cref="T:System.Data.DataSet" />.</summary>
		/// <returns>A <see cref="T:System.Data.DataTable" /> object that contains schema information returned from the data source.</returns>
		/// <param name="dataTable">The <see cref="T:System.Data.DataTable" /> to be filled from the <see cref="T:System.Data.IDataReader" />.</param>
		/// <param name="schemaType">One of the <see cref="T:System.Data.SchemaType" /> values.</param>
		/// <param name="dataReader">The <see cref="T:System.Data.IDataReader" /> to be used as the data source when filling the <see cref="T:System.Data.DataTable" />.</param>
		// Token: 0x060025F7 RID: 9719 RVA: 0x000AB8C0 File Offset: 0x000A9AC0
		protected virtual DataTable FillSchema(DataTable dataTable, SchemaType schemaType, IDataReader dataReader)
		{
			long num = DataCommonEventSource.Log.EnterScope<int>("<comm.DataAdapter.FillSchema|API> {0}, dataTable, schemaType, dataReader", this.ObjectID);
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
				if (dataReader == null || dataReader.IsClosed)
				{
					throw ADP.FillRequires("dataReader");
				}
				dataTable2 = (DataTable)this.FillSchemaFromReader(null, dataTable, schemaType, null, dataReader);
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
			return dataTable2;
		}

		// Token: 0x060025F8 RID: 9720 RVA: 0x000AB948 File Offset: 0x000A9B48
		internal object FillSchemaFromReader(DataSet dataset, DataTable datatable, SchemaType schemaType, string srcTable, IDataReader dataReader)
		{
			DataTable[] array = null;
			int num = 0;
			SchemaMapping schemaMapping;
			for (;;)
			{
				DataReaderContainer dataReaderContainer = DataReaderContainer.Create(dataReader, this.ReturnProviderSpecificTypes);
				if (0 < dataReaderContainer.FieldCount)
				{
					string text = null;
					if (dataset != null)
					{
						text = DataAdapter.GetSourceTableName(srcTable, num);
						num++;
					}
					schemaMapping = new SchemaMapping(this, dataset, datatable, dataReaderContainer, true, schemaType, text, false, null, null);
					if (datatable != null)
					{
						break;
					}
					if (schemaMapping.DataTable != null)
					{
						if (array == null)
						{
							array = new DataTable[] { schemaMapping.DataTable };
						}
						else
						{
							array = DataAdapter.AddDataTableToArray(array, schemaMapping.DataTable);
						}
					}
				}
				if (!dataReader.NextResult())
				{
					goto Block_6;
				}
			}
			return schemaMapping.DataTable;
			Block_6:
			object obj = array;
			if (obj == null && datatable == null)
			{
				obj = Array.Empty<DataTable>();
			}
			return obj;
		}

		/// <summary>Adds or refreshes rows in the <see cref="T:System.Data.DataSet" /> to match those in the data source.</summary>
		/// <returns>The number of rows successfully added to or refreshed in the <see cref="T:System.Data.DataSet" />. This does not include rows affected by statements that do not return rows.</returns>
		/// <param name="dataSet">A <see cref="T:System.Data.DataSet" /> to fill with records and, if necessary, schema. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060025F9 RID: 9721 RVA: 0x00060F32 File Offset: 0x0005F132
		public virtual int Fill(DataSet dataSet)
		{
			throw ADP.NotSupported();
		}

		/// <summary>Adds or refreshes rows in a specified range in the <see cref="T:System.Data.DataSet" /> to match those in the data source using the <see cref="T:System.Data.DataSet" /> and <see cref="T:System.Data.DataTable" /> names.</summary>
		/// <returns>The number of rows successfully added to or refreshed in the <see cref="T:System.Data.DataSet" />. This does not include rows affected by statements that do not return rows.</returns>
		/// <param name="dataSet">A <see cref="T:System.Data.DataSet" /> to fill with records.</param>
		/// <param name="srcTable">A string indicating the name of the source table.</param>
		/// <param name="dataReader">An instance of <see cref="T:System.Data.IDataReader" />.</param>
		/// <param name="startRecord">The zero-based index of the starting record.</param>
		/// <param name="maxRecords">An integer indicating the maximum number of records.</param>
		// Token: 0x060025FA RID: 9722 RVA: 0x000AB9E8 File Offset: 0x000A9BE8
		protected virtual int Fill(DataSet dataSet, string srcTable, IDataReader dataReader, int startRecord, int maxRecords)
		{
			long num = DataCommonEventSource.Log.EnterScope<int>("<comm.DataAdapter.Fill|API> {0}, dataSet, srcTable, dataReader, startRecord, maxRecords", this.ObjectID);
			int num2;
			try
			{
				if (dataSet == null)
				{
					throw ADP.FillRequires("dataSet");
				}
				if (string.IsNullOrEmpty(srcTable))
				{
					throw ADP.FillRequiresSourceTableName("srcTable");
				}
				if (dataReader == null)
				{
					throw ADP.FillRequires("dataReader");
				}
				if (startRecord < 0)
				{
					throw ADP.InvalidStartRecord("startRecord", startRecord);
				}
				if (maxRecords < 0)
				{
					throw ADP.InvalidMaxRecords("maxRecords", maxRecords);
				}
				if (dataReader.IsClosed)
				{
					num2 = 0;
				}
				else
				{
					DataReaderContainer dataReaderContainer = DataReaderContainer.Create(dataReader, this.ReturnProviderSpecificTypes);
					num2 = this.FillFromReader(dataSet, null, srcTable, dataReaderContainer, startRecord, maxRecords, null, null);
				}
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
			return num2;
		}

		/// <summary>Adds or refreshes rows in the <see cref="T:System.Data.DataTable" /> to match those in the data source using the <see cref="T:System.Data.DataTable" /> name and the specified <see cref="T:System.Data.IDataReader" />.</summary>
		/// <returns>The number of rows successfully added to or refreshed in the <see cref="T:System.Data.DataTable" />. This does not include rows affected by statements that do not return rows.</returns>
		/// <param name="dataTable">A <see cref="T:System.Data.DataTable" /> to fill with records.</param>
		/// <param name="dataReader">An instance of <see cref="T:System.Data.IDataReader" />.</param>
		// Token: 0x060025FB RID: 9723 RVA: 0x000ABAA8 File Offset: 0x000A9CA8
		protected virtual int Fill(DataTable dataTable, IDataReader dataReader)
		{
			DataTable[] array = new DataTable[] { dataTable };
			return this.Fill(array, dataReader, 0, 0);
		}

		/// <summary>Adds or refreshes rows in a specified range in the collection of <see cref="T:System.Data.DataTable" /> objects to match those in the data source.</summary>
		/// <returns>The number of rows successfully added to or refreshed in the <see cref="T:System.Data.DataTable" />. This does not include rows affected by statements that do not return rows.</returns>
		/// <param name="dataTables">A collection of <see cref="T:System.Data.DataTable" /> objects to fill with records.</param>
		/// <param name="dataReader">An instance of <see cref="T:System.Data.IDataReader" />.</param>
		/// <param name="startRecord">The zero-based index of the starting record.</param>
		/// <param name="maxRecords">An integer indicating the maximum number of records.</param>
		// Token: 0x060025FC RID: 9724 RVA: 0x000ABACC File Offset: 0x000A9CCC
		protected virtual int Fill(DataTable[] dataTables, IDataReader dataReader, int startRecord, int maxRecords)
		{
			long num = DataCommonEventSource.Log.EnterScope<int>("<comm.DataAdapter.Fill|API> {0}, dataTables[], dataReader, startRecord, maxRecords", this.ObjectID);
			int num5;
			try
			{
				ADP.CheckArgumentLength(dataTables, "dataTables");
				if (dataTables == null || dataTables.Length == 0 || dataTables[0] == null)
				{
					throw ADP.FillRequires("dataTable");
				}
				if (dataReader == null)
				{
					throw ADP.FillRequires("dataReader");
				}
				if (1 < dataTables.Length && (startRecord != 0 || maxRecords != 0))
				{
					throw ADP.NotSupported();
				}
				int num2 = 0;
				bool flag = false;
				DataSet dataSet = dataTables[0].DataSet;
				try
				{
					if (dataSet != null)
					{
						flag = dataSet.EnforceConstraints;
						dataSet.EnforceConstraints = false;
					}
					int num3 = 0;
					while (num3 < dataTables.Length && !dataReader.IsClosed)
					{
						DataReaderContainer dataReaderContainer = DataReaderContainer.Create(dataReader, this.ReturnProviderSpecificTypes);
						if (dataReaderContainer.FieldCount > 0)
						{
							goto IL_00BC;
						}
						if (num3 == 0)
						{
							bool flag2;
							do
							{
								flag2 = this.FillNextResult(dataReaderContainer);
							}
							while (flag2 && dataReaderContainer.FieldCount <= 0);
							if (flag2)
							{
								goto IL_00BC;
							}
							break;
						}
						IL_00E7:
						num3++;
						continue;
						IL_00BC:
						if (0 < num3 && !this.FillNextResult(dataReaderContainer))
						{
							break;
						}
						int num4 = this.FillFromReader(null, dataTables[num3], null, dataReaderContainer, startRecord, maxRecords, null, null);
						if (num3 == 0)
						{
							num2 = num4;
							goto IL_00E7;
						}
						goto IL_00E7;
					}
				}
				catch (ConstraintException)
				{
					flag = false;
					throw;
				}
				finally
				{
					if (flag)
					{
						dataSet.EnforceConstraints = true;
					}
				}
				num5 = num2;
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
			return num5;
		}

		// Token: 0x060025FD RID: 9725 RVA: 0x000ABC1C File Offset: 0x000A9E1C
		internal int FillFromReader(DataSet dataset, DataTable datatable, string srcTable, DataReaderContainer dataReader, int startRecord, int maxRecords, DataColumn parentChapterColumn, object parentChapterValue)
		{
			int num = 0;
			int num2 = 0;
			do
			{
				if (0 < dataReader.FieldCount)
				{
					SchemaMapping schemaMapping = this.FillMapping(dataset, datatable, srcTable, dataReader, num2, parentChapterColumn, parentChapterValue);
					num2++;
					if (schemaMapping != null && schemaMapping.DataValues != null && schemaMapping.DataTable != null)
					{
						schemaMapping.DataTable.BeginLoadData();
						try
						{
							if (1 == num2 && (0 < startRecord || 0 < maxRecords))
							{
								num = this.FillLoadDataRowChunk(schemaMapping, startRecord, maxRecords);
							}
							else
							{
								int num3 = this.FillLoadDataRow(schemaMapping);
								if (1 == num2)
								{
									num = num3;
								}
							}
						}
						finally
						{
							schemaMapping.DataTable.EndLoadData();
						}
						if (datatable != null)
						{
							break;
						}
					}
				}
			}
			while (this.FillNextResult(dataReader));
			return num;
		}

		// Token: 0x060025FE RID: 9726 RVA: 0x000ABCC4 File Offset: 0x000A9EC4
		private int FillLoadDataRowChunk(SchemaMapping mapping, int startRecord, int maxRecords)
		{
			DataReaderContainer dataReader = mapping.DataReader;
			while (0 < startRecord)
			{
				if (!dataReader.Read())
				{
					return 0;
				}
				startRecord--;
			}
			int i = 0;
			if (0 < maxRecords)
			{
				while (i < maxRecords)
				{
					if (!dataReader.Read())
					{
						break;
					}
					if (this._hasFillErrorHandler)
					{
						try
						{
							mapping.LoadDataRowWithClear();
							i++;
							continue;
						}
						catch (Exception ex) when (ADP.IsCatchableExceptionType(ex))
						{
							ADP.TraceExceptionForCapture(ex);
							this.OnFillErrorHandler(ex, mapping.DataTable, mapping.DataValues);
							continue;
						}
					}
					mapping.LoadDataRow();
					i++;
				}
			}
			else
			{
				i = this.FillLoadDataRow(mapping);
			}
			return i;
		}

		// Token: 0x060025FF RID: 9727 RVA: 0x000ABD70 File Offset: 0x000A9F70
		private int FillLoadDataRow(SchemaMapping mapping)
		{
			int num = 0;
			DataReaderContainer dataReader = mapping.DataReader;
			if (this._hasFillErrorHandler)
			{
				while (dataReader.Read())
				{
					try
					{
						mapping.LoadDataRowWithClear();
						num++;
					}
					catch (Exception ex) when (ADP.IsCatchableExceptionType(ex))
					{
						ADP.TraceExceptionForCapture(ex);
						this.OnFillErrorHandler(ex, mapping.DataTable, mapping.DataValues);
					}
				}
			}
			else
			{
				while (dataReader.Read())
				{
					mapping.LoadDataRow();
					num++;
				}
			}
			return num;
		}

		// Token: 0x06002600 RID: 9728 RVA: 0x000ABE00 File Offset: 0x000AA000
		private SchemaMapping FillMappingInternal(DataSet dataset, DataTable datatable, string srcTable, DataReaderContainer dataReader, int schemaCount, DataColumn parentChapterColumn, object parentChapterValue)
		{
			bool flag = MissingSchemaAction.AddWithKey == this.MissingSchemaAction;
			string text = null;
			if (dataset != null)
			{
				text = DataAdapter.GetSourceTableName(srcTable, schemaCount);
			}
			return new SchemaMapping(this, dataset, datatable, dataReader, flag, SchemaType.Mapped, text, true, parentChapterColumn, parentChapterValue);
		}

		// Token: 0x06002601 RID: 9729 RVA: 0x000ABE38 File Offset: 0x000AA038
		private SchemaMapping FillMapping(DataSet dataset, DataTable datatable, string srcTable, DataReaderContainer dataReader, int schemaCount, DataColumn parentChapterColumn, object parentChapterValue)
		{
			SchemaMapping schemaMapping = null;
			if (this._hasFillErrorHandler)
			{
				try
				{
					return this.FillMappingInternal(dataset, datatable, srcTable, dataReader, schemaCount, parentChapterColumn, parentChapterValue);
				}
				catch (Exception ex) when (ADP.IsCatchableExceptionType(ex))
				{
					ADP.TraceExceptionForCapture(ex);
					this.OnFillErrorHandler(ex, null, null);
					return schemaMapping;
				}
			}
			schemaMapping = this.FillMappingInternal(dataset, datatable, srcTable, dataReader, schemaCount, parentChapterColumn, parentChapterValue);
			return schemaMapping;
		}

		// Token: 0x06002602 RID: 9730 RVA: 0x000ABEB0 File Offset: 0x000AA0B0
		private bool FillNextResult(DataReaderContainer dataReader)
		{
			bool flag = true;
			if (this._hasFillErrorHandler)
			{
				try
				{
					return dataReader.NextResult();
				}
				catch (Exception ex) when (ADP.IsCatchableExceptionType(ex))
				{
					ADP.TraceExceptionForCapture(ex);
					this.OnFillErrorHandler(ex, null, null);
					return flag;
				}
			}
			flag = dataReader.NextResult();
			return flag;
		}

		/// <summary>Gets the parameters set by the user when executing an SQL SELECT statement.</summary>
		/// <returns>An array of <see cref="T:System.Data.IDataParameter" /> objects that contains the parameters set by the user.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06002603 RID: 9731 RVA: 0x000ABF14 File Offset: 0x000AA114
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public virtual IDataParameter[] GetFillParameters()
		{
			return Array.Empty<IDataParameter>();
		}

		// Token: 0x06002604 RID: 9732 RVA: 0x000ABF1B File Offset: 0x000AA11B
		internal DataTableMapping GetTableMappingBySchemaAction(string sourceTableName, string dataSetTableName, MissingMappingAction mappingAction)
		{
			return DataTableMappingCollection.GetTableMappingBySchemaAction(this._tableMappings, sourceTableName, dataSetTableName, mappingAction);
		}

		// Token: 0x06002605 RID: 9733 RVA: 0x000ABF2B File Offset: 0x000AA12B
		internal int IndexOfDataSetTable(string dataSetTable)
		{
			if (this._tableMappings != null)
			{
				return this.TableMappings.IndexOfDataSetTable(dataSetTable);
			}
			return -1;
		}

		/// <summary>Invoked when an error occurs during a Fill.</summary>
		/// <param name="value">A <see cref="T:System.Data.FillErrorEventArgs" /> object.</param>
		// Token: 0x06002606 RID: 9734 RVA: 0x000ABF43 File Offset: 0x000AA143
		protected virtual void OnFillError(FillErrorEventArgs value)
		{
			FillErrorEventHandler fillErrorEventHandler = (FillErrorEventHandler)base.Events[DataAdapter.s_eventFillError];
			if (fillErrorEventHandler == null)
			{
				return;
			}
			fillErrorEventHandler(this, value);
		}

		// Token: 0x06002607 RID: 9735 RVA: 0x000ABF68 File Offset: 0x000AA168
		private void OnFillErrorHandler(Exception e, DataTable dataTable, object[] dataValues)
		{
			FillErrorEventArgs fillErrorEventArgs = new FillErrorEventArgs(dataTable, dataValues);
			fillErrorEventArgs.Errors = e;
			this.OnFillError(fillErrorEventArgs);
			if (fillErrorEventArgs.Continue)
			{
				return;
			}
			if (fillErrorEventArgs.Errors != null)
			{
				throw fillErrorEventArgs.Errors;
			}
			throw e;
		}

		/// <summary>Calls the respective INSERT, UPDATE, or DELETE statements for each inserted, updated, or deleted row in the specified <see cref="T:System.Data.DataSet" /> from a <see cref="T:System.Data.DataTable" /> named "Table."</summary>
		/// <returns>The number of rows successfully updated from the <see cref="T:System.Data.DataSet" />.</returns>
		/// <param name="dataSet">The <see cref="T:System.Data.DataSet" /> used to update the data source. </param>
		/// <exception cref="T:System.InvalidOperationException">The source table is invalid. </exception>
		/// <exception cref="T:System.Data.DBConcurrencyException">An attempt to execute an INSERT, UPDATE, or DELETE statement resulted in zero records affected. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06002608 RID: 9736 RVA: 0x00060F32 File Offset: 0x0005F132
		public virtual int Update(DataSet dataSet)
		{
			throw ADP.NotSupported();
		}

		// Token: 0x06002609 RID: 9737 RVA: 0x000ABFA4 File Offset: 0x000AA1A4
		private static DataTable[] AddDataTableToArray(DataTable[] tables, DataTable newTable)
		{
			for (int i = 0; i < tables.Length; i++)
			{
				if (tables[i] == newTable)
				{
					return tables;
				}
			}
			DataTable[] array = new DataTable[tables.Length + 1];
			for (int j = 0; j < tables.Length; j++)
			{
				array[j] = tables[j];
			}
			array[tables.Length] = newTable;
			return array;
		}

		// Token: 0x0600260A RID: 9738 RVA: 0x000ABFED File Offset: 0x000AA1ED
		private static string GetSourceTableName(string srcTable, int index)
		{
			if (index == 0)
			{
				return srcTable;
			}
			return srcTable + index.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x040018A3 RID: 6307
		private static readonly object s_eventFillError = new object();

		// Token: 0x040018A4 RID: 6308
		private bool _acceptChangesDuringUpdate = true;

		// Token: 0x040018A5 RID: 6309
		private bool _acceptChangesDuringUpdateAfterInsert = true;

		// Token: 0x040018A6 RID: 6310
		private bool _continueUpdateOnError;

		// Token: 0x040018A7 RID: 6311
		private bool _hasFillErrorHandler;

		// Token: 0x040018A8 RID: 6312
		private bool _returnProviderSpecificTypes;

		// Token: 0x040018A9 RID: 6313
		private bool _acceptChangesDuringFill = true;

		// Token: 0x040018AA RID: 6314
		private LoadOption _fillLoadOption;

		// Token: 0x040018AB RID: 6315
		private MissingMappingAction _missingMappingAction = MissingMappingAction.Passthrough;

		// Token: 0x040018AC RID: 6316
		private MissingSchemaAction _missingSchemaAction = MissingSchemaAction.Add;

		// Token: 0x040018AD RID: 6317
		private DataTableMappingCollection _tableMappings;

		// Token: 0x040018AE RID: 6318
		private static int s_objectTypeCount;

		// Token: 0x040018AF RID: 6319
		internal readonly int _objectID = Interlocked.Increment(ref DataAdapter.s_objectTypeCount);
	}
}
