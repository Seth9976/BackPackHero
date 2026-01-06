using System;
using System.Collections;
using System.Data.Common;
using System.Globalization;

namespace System.Data
{
	/// <summary>The <see cref="T:System.Data.DataTableReader" /> obtains the contents of one or more <see cref="T:System.Data.DataTable" /> objects in the form of one or more read-only, forward-only result sets. </summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200007E RID: 126
	public sealed class DataTableReader : DbDataReader
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DataTableReader" /> class by using data from the supplied <see cref="T:System.Data.DataTable" />.</summary>
		/// <param name="dataTable">The <see cref="T:System.Data.DataTable" /> from which the new <see cref="T:System.Data.DataTableReader" /> obtains its result set. </param>
		// Token: 0x0600088E RID: 2190 RVA: 0x000268A8 File Offset: 0x00024AA8
		public DataTableReader(DataTable dataTable)
		{
			if (dataTable == null)
			{
				throw ExceptionBuilder.ArgumentNull("DataTable");
			}
			this._tables = new DataTable[] { dataTable };
			this.Init();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DataTableReader" /> class using the supplied array of <see cref="T:System.Data.DataTable" /> objects.</summary>
		/// <param name="dataTables">The array of <see cref="T:System.Data.DataTable" /> objects that supplies the results for the new <see cref="T:System.Data.DataTableReader" /> object. </param>
		// Token: 0x0600088F RID: 2191 RVA: 0x000268FC File Offset: 0x00024AFC
		public DataTableReader(DataTable[] dataTables)
		{
			if (dataTables == null)
			{
				throw ExceptionBuilder.ArgumentNull("DataTable");
			}
			if (dataTables.Length == 0)
			{
				throw ExceptionBuilder.DataTableReaderArgumentIsEmpty();
			}
			this._tables = new DataTable[dataTables.Length];
			for (int i = 0; i < dataTables.Length; i++)
			{
				if (dataTables[i] == null)
				{
					throw ExceptionBuilder.ArgumentNull("DataTable");
				}
				this._tables[i] = dataTables[i];
			}
			this.Init();
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x06000890 RID: 2192 RVA: 0x00026980 File Offset: 0x00024B80
		// (set) Token: 0x06000891 RID: 2193 RVA: 0x00026988 File Offset: 0x00024B88
		private bool ReaderIsInvalid
		{
			get
			{
				return this._readerIsInvalid;
			}
			set
			{
				if (this._readerIsInvalid == value)
				{
					return;
				}
				this._readerIsInvalid = value;
				if (this._readerIsInvalid && this._listener != null)
				{
					this._listener.CleanUp();
				}
			}
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x06000892 RID: 2194 RVA: 0x000269B6 File Offset: 0x00024BB6
		// (set) Token: 0x06000893 RID: 2195 RVA: 0x000269BE File Offset: 0x00024BBE
		private bool IsSchemaChanged
		{
			get
			{
				return this._schemaIsChanged;
			}
			set
			{
				if (!value || this._schemaIsChanged == value)
				{
					return;
				}
				this._schemaIsChanged = value;
				if (this._listener != null)
				{
					this._listener.CleanUp();
				}
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x06000894 RID: 2196 RVA: 0x000269E7 File Offset: 0x00024BE7
		internal DataTable CurrentDataTable
		{
			get
			{
				return this._currentDataTable;
			}
		}

		// Token: 0x06000895 RID: 2197 RVA: 0x000269F0 File Offset: 0x00024BF0
		private void Init()
		{
			this._tableCounter = 0;
			this._reachEORows = false;
			this._schemaIsChanged = false;
			this._currentDataTable = this._tables[this._tableCounter];
			this._hasRows = this._currentDataTable.Rows.Count > 0;
			this.ReaderIsInvalid = false;
			this._listener = new DataTableReaderListener(this);
		}

		/// <summary>Closes the current <see cref="T:System.Data.DataTableReader" />.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000896 RID: 2198 RVA: 0x00026A51 File Offset: 0x00024C51
		public override void Close()
		{
			if (!this._isOpen)
			{
				return;
			}
			if (this._listener != null)
			{
				this._listener.CleanUp();
			}
			this._listener = null;
			this._schemaTable = null;
			this._isOpen = false;
		}

		/// <summary>Returns a <see cref="T:System.Data.DataTable" /> that describes the column metadata of the <see cref="T:System.Data.DataTableReader" />.</summary>
		/// <returns>A <see cref="T:System.Data.DataTable" /> that describes the column metadata.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Data.DataTableReader" /> is closed. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000897 RID: 2199 RVA: 0x00026A84 File Offset: 0x00024C84
		public override DataTable GetSchemaTable()
		{
			this.ValidateOpen("GetSchemaTable");
			this.ValidateReader();
			if (this._schemaTable == null)
			{
				this._schemaTable = DataTableReader.GetSchemaTableFromDataTable(this._currentDataTable);
			}
			return this._schemaTable;
		}

		/// <summary>Advances the <see cref="T:System.Data.DataTableReader" /> to the next result set, if any.</summary>
		/// <returns>true if there was another result set; otherwise false.</returns>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to navigate within a closed <see cref="T:System.Data.DataTableReader" />.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000898 RID: 2200 RVA: 0x00026AB8 File Offset: 0x00024CB8
		public override bool NextResult()
		{
			this.ValidateOpen("NextResult");
			if (this._tableCounter == this._tables.Length - 1)
			{
				return false;
			}
			DataTable[] tables = this._tables;
			int num = this._tableCounter + 1;
			this._tableCounter = num;
			this._currentDataTable = tables[num];
			if (this._listener != null)
			{
				this._listener.UpdataTable(this._currentDataTable);
			}
			this._schemaTable = null;
			this._rowCounter = -1;
			this._currentRowRemoved = false;
			this._reachEORows = false;
			this._schemaIsChanged = false;
			this._started = false;
			this.ReaderIsInvalid = false;
			this._tableCleared = false;
			this._hasRows = this._currentDataTable.Rows.Count > 0;
			return true;
		}

		/// <summary>Advances the <see cref="T:System.Data.DataTableReader" /> to the next record.</summary>
		/// <returns>true if there was another row to read; otherwise false.</returns>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to read or access a column in a closed <see cref="T:System.Data.DataTableReader" /> .</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000899 RID: 2201 RVA: 0x00026B70 File Offset: 0x00024D70
		public override bool Read()
		{
			if (!this._started)
			{
				this._started = true;
			}
			this.ValidateOpen("Read");
			this.ValidateReader();
			if (this._reachEORows)
			{
				return false;
			}
			if (this._rowCounter >= this._currentDataTable.Rows.Count - 1)
			{
				this._reachEORows = true;
				if (this._listener != null)
				{
					this._listener.CleanUp();
				}
				return false;
			}
			this._rowCounter++;
			this.ValidateRow(this._rowCounter);
			this._currentDataRow = this._currentDataTable.Rows[this._rowCounter];
			while (this._currentDataRow.RowState == DataRowState.Deleted)
			{
				this._rowCounter++;
				if (this._rowCounter == this._currentDataTable.Rows.Count)
				{
					this._reachEORows = true;
					if (this._listener != null)
					{
						this._listener.CleanUp();
					}
					return false;
				}
				this.ValidateRow(this._rowCounter);
				this._currentDataRow = this._currentDataTable.Rows[this._rowCounter];
			}
			if (this._currentRowRemoved)
			{
				this._currentRowRemoved = false;
			}
			return true;
		}

		/// <summary>The depth of nesting for the current row of the <see cref="T:System.Data.DataTableReader" />.</summary>
		/// <returns>The depth of nesting for the current row; always zero.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000184 RID: 388
		// (get) Token: 0x0600089A RID: 2202 RVA: 0x00026C9D File Offset: 0x00024E9D
		public override int Depth
		{
			get
			{
				this.ValidateOpen("Depth");
				this.ValidateReader();
				return 0;
			}
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Data.DataTableReader" /> is closed.</summary>
		/// <returns>Returns true if the <see cref="T:System.Data.DataTableReader" /> is closed; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000185 RID: 389
		// (get) Token: 0x0600089B RID: 2203 RVA: 0x00026CB1 File Offset: 0x00024EB1
		public override bool IsClosed
		{
			get
			{
				return !this._isOpen;
			}
		}

		/// <summary>Gets the number of rows inserted, changed, or deleted by execution of the SQL statement.</summary>
		/// <returns>The <see cref="T:System.Data.DataTableReader" /> does not support this property and always returns 0.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000186 RID: 390
		// (get) Token: 0x0600089C RID: 2204 RVA: 0x00026CBC File Offset: 0x00024EBC
		public override int RecordsAffected
		{
			get
			{
				this.ValidateReader();
				return 0;
			}
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Data.DataTableReader" /> contains one or more rows.</summary>
		/// <returns>true if the <see cref="T:System.Data.DataTableReader" /> contains one or more rows; otherwise false.</returns>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to retrieve information about a closed <see cref="T:System.Data.DataTableReader" />.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000187 RID: 391
		// (get) Token: 0x0600089D RID: 2205 RVA: 0x00026CC5 File Offset: 0x00024EC5
		public override bool HasRows
		{
			get
			{
				this.ValidateOpen("HasRows");
				this.ValidateReader();
				return this._hasRows;
			}
		}

		/// <summary>Gets the value of the specified column in its native format given the column ordinal.</summary>
		/// <returns>The value of the specified column in its native format.</returns>
		/// <param name="ordinal">The zero-based column ordinal. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">The index passed was outside the range of 0 to <see cref="P:System.Data.DataTableReader.FieldCount" /> - 1.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000188 RID: 392
		public override object this[int ordinal]
		{
			get
			{
				this.ValidateOpen("Item");
				this.ValidateReader();
				if (this._currentDataRow == null || this._currentDataRow.RowState == DataRowState.Deleted)
				{
					this.ReaderIsInvalid = true;
					throw ExceptionBuilder.InvalidDataTableReader(this._currentDataTable.TableName);
				}
				object obj;
				try
				{
					obj = this._currentDataRow[ordinal];
				}
				catch (IndexOutOfRangeException ex)
				{
					ExceptionBuilder.TraceExceptionWithoutRethrow(ex);
					throw ExceptionBuilder.ArgumentOutOfRange("ordinal");
				}
				return obj;
			}
		}

		/// <summary>Gets the value of the specified column in its native format given the column name.</summary>
		/// <returns>The value of the specified column in its native format.</returns>
		/// <param name="name">The name of the column. </param>
		/// <exception cref="T:System.ArgumentException">The name specified is not a valid column name. </exception>
		/// <exception cref="T:System.Data.DeletedRowInaccessibleException">An attempt was made to retrieve data from a deleted row.</exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to read or access a column in a closed <see cref="T:System.Data.DataTableReader" />.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000189 RID: 393
		public override object this[string name]
		{
			get
			{
				this.ValidateOpen("Item");
				this.ValidateReader();
				if (this._currentDataRow == null || this._currentDataRow.RowState == DataRowState.Deleted)
				{
					this.ReaderIsInvalid = true;
					throw ExceptionBuilder.InvalidDataTableReader(this._currentDataTable.TableName);
				}
				return this._currentDataRow[name];
			}
		}

		/// <summary>Returns the number of columns in the current row.</summary>
		/// <returns>When not positioned in a valid result set, 0; otherwise the number of columns in the current row. </returns>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to retrieve the field count in a closed <see cref="T:System.Data.DataTableReader" />.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x1700018A RID: 394
		// (get) Token: 0x060008A0 RID: 2208 RVA: 0x00026DB8 File Offset: 0x00024FB8
		public override int FieldCount
		{
			get
			{
				this.ValidateOpen("FieldCount");
				this.ValidateReader();
				return this._currentDataTable.Columns.Count;
			}
		}

		/// <summary>Gets the type of the specified column in provider-specific format.</summary>
		/// <returns>The <see cref="T:System.Type" /> that is the data type of the object.</returns>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index passed was outside the range of 0 to <see cref="P:System.Data.DataTableReader.FieldCount" /> - 1. </exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to read or access a column in a closed <see cref="T:System.Data.DataTableReader" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060008A1 RID: 2209 RVA: 0x00026DDB File Offset: 0x00024FDB
		public override Type GetProviderSpecificFieldType(int ordinal)
		{
			this.ValidateOpen("GetProviderSpecificFieldType");
			this.ValidateReader();
			return this.GetFieldType(ordinal);
		}

		/// <summary>Gets the value of the specified column in provider-specific format.</summary>
		/// <returns>The value of the specified column in provider-specific format.</returns>
		/// <param name="ordinal">The zero-based number of the column whose value is retrieved. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index passed was outside the range of 0 to <see cref="P:System.Data.DataTableReader.FieldCount" /> - 1. </exception>
		/// <exception cref="T:System.Data.DeletedRowInaccessibleException">An attempt was made to retrieve data from a deleted row.</exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to read or access a column in a closed <see cref="T:System.Data.DataTableReader" /></exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060008A2 RID: 2210 RVA: 0x00026DF5 File Offset: 0x00024FF5
		public override object GetProviderSpecificValue(int ordinal)
		{
			this.ValidateOpen("GetProviderSpecificValue");
			this.ValidateReader();
			return this.GetValue(ordinal);
		}

		/// <summary>Fills the supplied array with provider-specific type information for all the columns in the <see cref="T:System.Data.DataTableReader" />.</summary>
		/// <returns>The number of column values copied into the array.</returns>
		/// <param name="values">An array of objects to be filled in with type information for the columns in the <see cref="T:System.Data.DataTableReader" />. </param>
		/// <exception cref="T:System.Data.DeletedRowInaccessibleException">An attempt was made to retrieve data from a deleted row.</exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to read or access a column in a closed <see cref="T:System.Data.DataTableReader" />.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060008A3 RID: 2211 RVA: 0x00026E0F File Offset: 0x0002500F
		public override int GetProviderSpecificValues(object[] values)
		{
			this.ValidateOpen("GetProviderSpecificValues");
			this.ValidateReader();
			return this.GetValues(values);
		}

		/// <summary>Gets the value of the specified column as a <see cref="T:System.Boolean" />.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="ordinal">The zero-based column ordinal. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index passed was outside the range of 0 to <see cref="P:System.Data.DataTableReader.FieldCount" /> - 1. </exception>
		/// <exception cref="T:System.Data.DeletedRowInaccessibleException">An attempt was made to retrieve data from a deleted row.</exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to read or access a column in a closed <see cref="T:System.Data.DataTableReader" />.</exception>
		/// <exception cref="T:System.InvalidCastException">The specified column does not contain a Boolean. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060008A4 RID: 2212 RVA: 0x00026E2C File Offset: 0x0002502C
		public override bool GetBoolean(int ordinal)
		{
			this.ValidateState("GetBoolean");
			this.ValidateReader();
			bool flag;
			try
			{
				flag = (bool)this._currentDataRow[ordinal];
			}
			catch (IndexOutOfRangeException ex)
			{
				ExceptionBuilder.TraceExceptionWithoutRethrow(ex);
				throw ExceptionBuilder.ArgumentOutOfRange("ordinal");
			}
			return flag;
		}

		/// <summary>Gets the value of the specified column as a byte.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="ordinal">The zero-based column ordinal. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index passed was outside the range of 0 to <see cref="P:System.Data.DataTableReader.FieldCount" /> - 1. </exception>
		/// <exception cref="T:System.Data.DeletedRowInaccessibleException">An attempt was made to retrieve data from a deleted row.</exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to read or access a column in a closed DataTableReader.</exception>
		/// <exception cref="T:System.InvalidCastException">The specified column does not contain a byte. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060008A5 RID: 2213 RVA: 0x00026E80 File Offset: 0x00025080
		public override byte GetByte(int ordinal)
		{
			this.ValidateState("GetByte");
			this.ValidateReader();
			byte b;
			try
			{
				b = (byte)this._currentDataRow[ordinal];
			}
			catch (IndexOutOfRangeException ex)
			{
				ExceptionBuilder.TraceExceptionWithoutRethrow(ex);
				throw ExceptionBuilder.ArgumentOutOfRange("ordinal");
			}
			return b;
		}

		/// <summary>Reads a stream of bytes starting at the specified column offset into the buffer as an array starting at the specified buffer offset.</summary>
		/// <returns>The actual number of bytes read.</returns>
		/// <param name="ordinal">The zero-based column ordinal. </param>
		/// <param name="dataIndex">The index within the field from which to start the read operation. </param>
		/// <param name="buffer">The buffer into which to read the stream of bytes. </param>
		/// <param name="bufferIndex">The index within the buffer at which to start placing the data. </param>
		/// <param name="length">The maximum length to copy into the buffer. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index passed was outside the range of 0 to <see cref="P:System.Data.DataTableReader.FieldCount" /> - 1. </exception>
		/// <exception cref="T:System.Data.DeletedRowInaccessibleException">An attempt was made to retrieve data from a deleted row.</exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to read or access a column in a closed DataTableReader.</exception>
		/// <exception cref="T:System.InvalidCastException">The specified column does not contain a byte array. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060008A6 RID: 2214 RVA: 0x00026ED4 File Offset: 0x000250D4
		public override long GetBytes(int ordinal, long dataIndex, byte[] buffer, int bufferIndex, int length)
		{
			this.ValidateState("GetBytes");
			this.ValidateReader();
			byte[] array;
			try
			{
				array = (byte[])this._currentDataRow[ordinal];
			}
			catch (IndexOutOfRangeException ex)
			{
				ExceptionBuilder.TraceExceptionWithoutRethrow(ex);
				throw ExceptionBuilder.ArgumentOutOfRange("ordinal");
			}
			if (buffer == null)
			{
				return (long)array.Length;
			}
			int num = (int)dataIndex;
			int num2 = Math.Min(array.Length - num, length);
			if (num < 0)
			{
				throw ADP.InvalidSourceBufferIndex(array.Length, (long)num, "dataIndex");
			}
			if (bufferIndex < 0 || (bufferIndex > 0 && bufferIndex >= buffer.Length))
			{
				throw ADP.InvalidDestinationBufferIndex(buffer.Length, bufferIndex, "bufferIndex");
			}
			if (0 < num2)
			{
				Array.Copy(array, dataIndex, buffer, (long)bufferIndex, (long)num2);
			}
			else
			{
				if (length < 0)
				{
					throw ADP.InvalidDataLength((long)length);
				}
				num2 = 0;
			}
			return (long)num2;
		}

		/// <summary>Gets the value of the specified column as a character.</summary>
		/// <returns>The value of the column.</returns>
		/// <param name="ordinal">The zero-based column ordinal. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index passed was outside the range of 0 to <see cref="P:System.Data.DataTableReader.FieldCount" /> - 1. </exception>
		/// <exception cref="T:System.Data.DeletedRowInaccessibleException">An attempt was made to retrieve data from a deleted row.</exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to read or access a column in a closed DataTableReader.</exception>
		/// <exception cref="T:System.InvalidCastException">The specified field does not contain a character. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060008A7 RID: 2215 RVA: 0x00026F9C File Offset: 0x0002519C
		public override char GetChar(int ordinal)
		{
			this.ValidateState("GetChar");
			this.ValidateReader();
			char c;
			try
			{
				c = (char)this._currentDataRow[ordinal];
			}
			catch (IndexOutOfRangeException ex)
			{
				ExceptionBuilder.TraceExceptionWithoutRethrow(ex);
				throw ExceptionBuilder.ArgumentOutOfRange("ordinal");
			}
			return c;
		}

		/// <summary>Returns the value of the specified column as a character array.</summary>
		/// <returns>The actual number of characters read.</returns>
		/// <param name="ordinal">The zero-based column ordinal. </param>
		/// <param name="dataIndex">The index within the field from which to start the read operation. </param>
		/// <param name="buffer">The buffer into which to read the stream of chars. </param>
		/// <param name="bufferIndex">The index within the buffer at which to start placing the data. </param>
		/// <param name="length">The maximum length to copy into the buffer. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index passed was outside the range of 0 to <see cref="P:System.Data.DataTableReader.FieldCount" /> - 1. </exception>
		/// <exception cref="T:System.Data.DeletedRowInaccessibleException">An attempt was made to retrieve data from a deleted row.</exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to read or access a column in a closed DataTableReader.</exception>
		/// <exception cref="T:System.InvalidCastException">The specified column does not contain a character array. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060008A8 RID: 2216 RVA: 0x00026FF0 File Offset: 0x000251F0
		public override long GetChars(int ordinal, long dataIndex, char[] buffer, int bufferIndex, int length)
		{
			this.ValidateState("GetChars");
			this.ValidateReader();
			char[] array;
			try
			{
				array = (char[])this._currentDataRow[ordinal];
			}
			catch (IndexOutOfRangeException ex)
			{
				ExceptionBuilder.TraceExceptionWithoutRethrow(ex);
				throw ExceptionBuilder.ArgumentOutOfRange("ordinal");
			}
			if (buffer == null)
			{
				return (long)array.Length;
			}
			int num = (int)dataIndex;
			int num2 = Math.Min(array.Length - num, length);
			if (num < 0)
			{
				throw ADP.InvalidSourceBufferIndex(array.Length, (long)num, "dataIndex");
			}
			if (bufferIndex < 0 || (bufferIndex > 0 && bufferIndex >= buffer.Length))
			{
				throw ADP.InvalidDestinationBufferIndex(buffer.Length, bufferIndex, "bufferIndex");
			}
			if (0 < num2)
			{
				Array.Copy(array, dataIndex, buffer, (long)bufferIndex, (long)num2);
			}
			else
			{
				if (length < 0)
				{
					throw ADP.InvalidDataLength((long)length);
				}
				num2 = 0;
			}
			return (long)num2;
		}

		/// <summary>Gets a string representing the data type of the specified column.</summary>
		/// <returns>A string representing the column's data type.</returns>
		/// <param name="ordinal">The zero-based column ordinal. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index passed was outside the range of 0 to <see cref="P:System.Data.DataTableReader.FieldCount" /> - 1. </exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to read or access a column in a closed <see cref="T:System.Data.DataTableReader" />.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060008A9 RID: 2217 RVA: 0x000270B8 File Offset: 0x000252B8
		public override string GetDataTypeName(int ordinal)
		{
			this.ValidateOpen("GetDataTypeName");
			this.ValidateReader();
			return this.GetFieldType(ordinal).Name;
		}

		/// <summary>Gets the value of the specified column as a <see cref="T:System.DateTime" /> object.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="ordinal">The zero-based column ordinal. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index passed was outside the range of 0 to <see cref="P:System.Data.DataTableReader.FieldCount" /> - 1. </exception>
		/// <exception cref="T:System.Data.DeletedRowInaccessibleException">An attempt was made to retrieve data from a deleted row.</exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to read or access a column in a closed DataTableReader.</exception>
		/// <exception cref="T:System.InvalidCastException">The specified column does not contain a DateTime value. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060008AA RID: 2218 RVA: 0x000270D8 File Offset: 0x000252D8
		public override DateTime GetDateTime(int ordinal)
		{
			this.ValidateState("GetDateTime");
			this.ValidateReader();
			DateTime dateTime;
			try
			{
				dateTime = (DateTime)this._currentDataRow[ordinal];
			}
			catch (IndexOutOfRangeException ex)
			{
				ExceptionBuilder.TraceExceptionWithoutRethrow(ex);
				throw ExceptionBuilder.ArgumentOutOfRange("ordinal");
			}
			return dateTime;
		}

		/// <summary>Gets the value of the specified column as a <see cref="T:System.Decimal" />.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="ordinal">The zero-based column ordinal. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index passed was outside the range of 0 to <see cref="P:System.Data.DataTableReader.FieldCount" /> - 1. </exception>
		/// <exception cref="T:System.Data.DeletedRowInaccessibleException">An attempt was made to retrieve data from a deleted row.</exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to read or access a column in a closed DataTableReader.</exception>
		/// <exception cref="T:System.InvalidCastException">The specified column does not contain a Decimal value. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060008AB RID: 2219 RVA: 0x0002712C File Offset: 0x0002532C
		public override decimal GetDecimal(int ordinal)
		{
			this.ValidateState("GetDecimal");
			this.ValidateReader();
			decimal num;
			try
			{
				num = (decimal)this._currentDataRow[ordinal];
			}
			catch (IndexOutOfRangeException ex)
			{
				ExceptionBuilder.TraceExceptionWithoutRethrow(ex);
				throw ExceptionBuilder.ArgumentOutOfRange("ordinal");
			}
			return num;
		}

		/// <summary>Gets the value of the column as a double-precision floating point number.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="ordinal">The zero-based ordinal of the column. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index passed was outside the range of 0 to <see cref="P:System.Data.DataTableReader.FieldCount" /> - 1. </exception>
		/// <exception cref="T:System.Data.DeletedRowInaccessibleException">An attempt was made to retrieve data from a deleted row.</exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to read or access a column in a closed DataTableReader.</exception>
		/// <exception cref="T:System.InvalidCastException">The specified column does not contain a double-precision floating point number. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060008AC RID: 2220 RVA: 0x00027180 File Offset: 0x00025380
		public override double GetDouble(int ordinal)
		{
			this.ValidateState("GetDouble");
			this.ValidateReader();
			double num;
			try
			{
				num = (double)this._currentDataRow[ordinal];
			}
			catch (IndexOutOfRangeException ex)
			{
				ExceptionBuilder.TraceExceptionWithoutRethrow(ex);
				throw ExceptionBuilder.ArgumentOutOfRange("ordinal");
			}
			return num;
		}

		/// <summary>Gets the <see cref="T:System.Type" /> that is the data type of the object.</summary>
		/// <returns>The <see cref="T:System.Type" /> that is the data type of the object.</returns>
		/// <param name="ordinal">The zero-based column ordinal. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index passed was outside the range of 0 to <see cref="P:System.Data.DataTableReader.FieldCount" /> - 1. </exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to read or access a column in a closed <see cref="T:System.Data.DataTableReader" /> .</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060008AD RID: 2221 RVA: 0x000271D4 File Offset: 0x000253D4
		public override Type GetFieldType(int ordinal)
		{
			this.ValidateOpen("GetFieldType");
			this.ValidateReader();
			Type dataType;
			try
			{
				dataType = this._currentDataTable.Columns[ordinal].DataType;
			}
			catch (IndexOutOfRangeException ex)
			{
				ExceptionBuilder.TraceExceptionWithoutRethrow(ex);
				throw ExceptionBuilder.ArgumentOutOfRange("ordinal");
			}
			return dataType;
		}

		/// <summary>Gets the value of the specified column as a single-precision floating point number.</summary>
		/// <returns>The value of the column.</returns>
		/// <param name="ordinal">The zero-based column ordinal. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index passed was outside the range of 0 to <see cref="P:System.Data.DataTableReader.FieldCount" /> - 1. </exception>
		/// <exception cref="T:System.Data.DeletedRowInaccessibleException">An attempt was made to retrieve data from a deleted row.</exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to read or access a column in a closed <see cref="T:System.Data.DataTableReader" />.</exception>
		/// <exception cref="T:System.InvalidCastException">The specified column does not contain a single-precision floating point number. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060008AE RID: 2222 RVA: 0x00027230 File Offset: 0x00025430
		public override float GetFloat(int ordinal)
		{
			this.ValidateState("GetFloat");
			this.ValidateReader();
			float num;
			try
			{
				num = (float)this._currentDataRow[ordinal];
			}
			catch (IndexOutOfRangeException ex)
			{
				ExceptionBuilder.TraceExceptionWithoutRethrow(ex);
				throw ExceptionBuilder.ArgumentOutOfRange("ordinal");
			}
			return num;
		}

		/// <summary>Gets the value of the specified column as a globally-unique identifier (GUID).</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="ordinal">The zero-based column ordinal. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index passed was outside the range of 0 to <see cref="P:System.Data.DataTableReader.FieldCount" /> - 1. </exception>
		/// <exception cref="T:System.Data.DeletedRowInaccessibleException">An attempt was made to retrieve data from a deleted row.</exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to read or access a column in a closed <see cref="T:System.Data.DataTableReader" />.</exception>
		/// <exception cref="T:System.InvalidCastException">The specified column does not contain a GUID. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060008AF RID: 2223 RVA: 0x00027284 File Offset: 0x00025484
		public override Guid GetGuid(int ordinal)
		{
			this.ValidateState("GetGuid");
			this.ValidateReader();
			Guid guid;
			try
			{
				guid = (Guid)this._currentDataRow[ordinal];
			}
			catch (IndexOutOfRangeException ex)
			{
				ExceptionBuilder.TraceExceptionWithoutRethrow(ex);
				throw ExceptionBuilder.ArgumentOutOfRange("ordinal");
			}
			return guid;
		}

		/// <summary>Gets the value of the specified column as a 16-bit signed integer.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="ordinal">The zero-based column ordinal </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index passed was outside the range of 0 to <see cref="P:System.Data.DataTableReader.FieldCount" /> - 1. </exception>
		/// <exception cref="T:System.Data.DeletedRowInaccessibleException">An attempt was made to retrieve data from a deleted row.</exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to read or access a column in a closed <see cref="T:System.Data.DataTableReader" />.</exception>
		/// <exception cref="T:System.InvalidCastException">The specified column does not contain a 16-bit signed integer. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060008B0 RID: 2224 RVA: 0x000272D8 File Offset: 0x000254D8
		public override short GetInt16(int ordinal)
		{
			this.ValidateState("GetInt16");
			this.ValidateReader();
			short num;
			try
			{
				num = (short)this._currentDataRow[ordinal];
			}
			catch (IndexOutOfRangeException ex)
			{
				ExceptionBuilder.TraceExceptionWithoutRethrow(ex);
				throw ExceptionBuilder.ArgumentOutOfRange("ordinal");
			}
			return num;
		}

		/// <summary>Gets the value of the specified column as a 32-bit signed integer.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="ordinal">The zero-based column ordinal </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index passed was outside the range of 0 to <see cref="P:System.Data.DataTableReader.FieldCount" /> - 1. </exception>
		/// <exception cref="T:System.Data.DeletedRowInaccessibleException">An attempt was made to retrieve data from a deleted row.</exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to read or access a column in a closed <see cref="T:System.Data.DataTableReader" /> .</exception>
		/// <exception cref="T:System.InvalidCastException">The specified column does not contain a 32-bit signed integer value. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060008B1 RID: 2225 RVA: 0x0002732C File Offset: 0x0002552C
		public override int GetInt32(int ordinal)
		{
			this.ValidateState("GetInt32");
			this.ValidateReader();
			int num;
			try
			{
				num = (int)this._currentDataRow[ordinal];
			}
			catch (IndexOutOfRangeException ex)
			{
				ExceptionBuilder.TraceExceptionWithoutRethrow(ex);
				throw ExceptionBuilder.ArgumentOutOfRange("ordinal");
			}
			return num;
		}

		/// <summary>Gets the value of the specified column as a 64-bit signed integer.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="ordinal">The zero-based column ordinal </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index passed was outside the range of 0 to <see cref="P:System.Data.DataTableReader.FieldCount" /> - 1. </exception>
		/// <exception cref="T:System.Data.DeletedRowInaccessibleException">An attempt was made to retrieve data from a deleted row.</exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to read or access a column in a closed <see cref="T:System.Data.DataTableReader" /> .</exception>
		/// <exception cref="T:System.InvalidCastException">The specified column does not contain a 64-bit signed integer value. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060008B2 RID: 2226 RVA: 0x00027380 File Offset: 0x00025580
		public override long GetInt64(int ordinal)
		{
			this.ValidateState("GetInt64");
			this.ValidateReader();
			long num;
			try
			{
				num = (long)this._currentDataRow[ordinal];
			}
			catch (IndexOutOfRangeException ex)
			{
				ExceptionBuilder.TraceExceptionWithoutRethrow(ex);
				throw ExceptionBuilder.ArgumentOutOfRange("ordinal");
			}
			return num;
		}

		/// <summary>Gets the value of the specified column as a <see cref="T:System.String" />.</summary>
		/// <returns>The name of the specified column.</returns>
		/// <param name="ordinal">The zero-based column ordinal </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index passed was outside the range of 0 to <see cref="P:System.Data.DataTableReader.FieldCount" /> - 1. </exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to read or access a column in a closed <see cref="T:System.Data.DataTableReader" />.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060008B3 RID: 2227 RVA: 0x000273D4 File Offset: 0x000255D4
		public override string GetName(int ordinal)
		{
			this.ValidateOpen("GetName");
			this.ValidateReader();
			string columnName;
			try
			{
				columnName = this._currentDataTable.Columns[ordinal].ColumnName;
			}
			catch (IndexOutOfRangeException ex)
			{
				ExceptionBuilder.TraceExceptionWithoutRethrow(ex);
				throw ExceptionBuilder.ArgumentOutOfRange("ordinal");
			}
			return columnName;
		}

		/// <summary>Gets the column ordinal, given the name of the column.</summary>
		/// <returns>The zero-based column ordinal.</returns>
		/// <param name="name">The name of the column. </param>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to read or access a column in a closed <see cref="T:System.Data.DataTableReader" />.</exception>
		/// <exception cref="T:System.ArgumentException">The name specified is not a valid column name. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060008B4 RID: 2228 RVA: 0x00027430 File Offset: 0x00025630
		public override int GetOrdinal(string name)
		{
			this.ValidateOpen("GetOrdinal");
			this.ValidateReader();
			DataColumn dataColumn = this._currentDataTable.Columns[name];
			if (dataColumn != null)
			{
				return dataColumn.Ordinal;
			}
			throw ExceptionBuilder.ColumnNotInTheTable(name, this._currentDataTable.TableName);
		}

		/// <summary>Gets the value of the specified column as a string.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="ordinal">The zero-based column ordinal </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index passed was outside the range of 0 to <see cref="P:System.Data.DataTableReader.FieldCount" /> - 1. </exception>
		/// <exception cref="T:System.Data.DeletedRowInaccessibleException">An attempt was made to retrieve data from a deleted row.</exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to read or access a column in a closed <see cref="T:System.Data.DataTableReader" />.</exception>
		/// <exception cref="T:System.InvalidCastException">The specified column does not contain a string. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060008B5 RID: 2229 RVA: 0x0002747C File Offset: 0x0002567C
		public override string GetString(int ordinal)
		{
			this.ValidateState("GetString");
			this.ValidateReader();
			string text;
			try
			{
				text = (string)this._currentDataRow[ordinal];
			}
			catch (IndexOutOfRangeException ex)
			{
				ExceptionBuilder.TraceExceptionWithoutRethrow(ex);
				throw ExceptionBuilder.ArgumentOutOfRange("ordinal");
			}
			return text;
		}

		/// <summary>Gets the value of the specified column in its native format.</summary>
		/// <returns>The value of the specified column. This method returns DBNull for null columns.</returns>
		/// <param name="ordinal">The zero-based column ordinal </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index passed was outside the range of 0 to <see cref="P:System.Data.DataTableReader.FieldCount" /> - 1. </exception>
		/// <exception cref="T:System.Data.DeletedRowInaccessibleException">An attempt was made to retrieve data from a deleted row.</exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to read or access columns in a closed <see cref="T:System.Data.DataTableReader" /> .</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060008B6 RID: 2230 RVA: 0x000274D0 File Offset: 0x000256D0
		public override object GetValue(int ordinal)
		{
			this.ValidateState("GetValue");
			this.ValidateReader();
			object obj;
			try
			{
				obj = this._currentDataRow[ordinal];
			}
			catch (IndexOutOfRangeException ex)
			{
				ExceptionBuilder.TraceExceptionWithoutRethrow(ex);
				throw ExceptionBuilder.ArgumentOutOfRange("ordinal");
			}
			return obj;
		}

		/// <summary>Populates an array of objects with the column values of the current row.</summary>
		/// <returns>The number of column values copied into the array.</returns>
		/// <param name="values">An array of <see cref="T:System.Object" /> into which to copy the column values from the <see cref="T:System.Data.DataTableReader" />.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index passed was outside the range of 0 to <see cref="P:System.Data.DataTableReader.FieldCount" /> - 1. </exception>
		/// <exception cref="T:System.Data.DeletedRowInaccessibleException">An attempt was made to retrieve data from a deleted row.</exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to read or access a column in a closed <see cref="T:System.Data.DataTableReader" /> .</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060008B7 RID: 2231 RVA: 0x00027520 File Offset: 0x00025720
		public override int GetValues(object[] values)
		{
			this.ValidateState("GetValues");
			this.ValidateReader();
			if (values == null)
			{
				throw ExceptionBuilder.ArgumentNull("values");
			}
			Array.Copy(this._currentDataRow.ItemArray, values, (this._currentDataRow.ItemArray.Length > values.Length) ? values.Length : this._currentDataRow.ItemArray.Length);
			if (this._currentDataRow.ItemArray.Length <= values.Length)
			{
				return this._currentDataRow.ItemArray.Length;
			}
			return values.Length;
		}

		/// <summary>Gets a value that indicates whether the column contains non-existent or missing values.</summary>
		/// <returns>true if the specified column value is equivalent to <see cref="T:System.DBNull" />; otherwise, false.</returns>
		/// <param name="ordinal">The zero-based column ordinal </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index passed was outside the range of 0 to <see cref="P:System.Data.DataTableReader.FieldCount" /> - 1.</exception>
		/// <exception cref="T:System.Data.DeletedRowInaccessibleException">An attempt was made to retrieve data from a deleted row.</exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to read or access a column in a closed <see cref="T:System.Data.DataTableReader" /> .</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060008B8 RID: 2232 RVA: 0x000275A4 File Offset: 0x000257A4
		public override bool IsDBNull(int ordinal)
		{
			this.ValidateState("IsDBNull");
			this.ValidateReader();
			bool flag;
			try
			{
				flag = this._currentDataRow.IsNull(ordinal);
			}
			catch (IndexOutOfRangeException ex)
			{
				ExceptionBuilder.TraceExceptionWithoutRethrow(ex);
				throw ExceptionBuilder.ArgumentOutOfRange("ordinal");
			}
			return flag;
		}

		/// <summary>Returns an enumerator that can be used to iterate through the item collection. </summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> object that represents the item collection.</returns>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to read or access a column in a closed <see cref="T:System.Data.DataTableReader" />.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060008B9 RID: 2233 RVA: 0x000275F4 File Offset: 0x000257F4
		public override IEnumerator GetEnumerator()
		{
			this.ValidateOpen("GetEnumerator");
			return new DbEnumerator(this);
		}

		// Token: 0x060008BA RID: 2234 RVA: 0x00027608 File Offset: 0x00025808
		internal static DataTable GetSchemaTableFromDataTable(DataTable table)
		{
			if (table == null)
			{
				throw ExceptionBuilder.ArgumentNull("DataTable");
			}
			DataTable dataTable = new DataTable("SchemaTable");
			dataTable.Locale = CultureInfo.InvariantCulture;
			DataColumn dataColumn = new DataColumn(SchemaTableColumn.ColumnName, typeof(string));
			DataColumn dataColumn2 = new DataColumn(SchemaTableColumn.ColumnOrdinal, typeof(int));
			DataColumn dataColumn3 = new DataColumn(SchemaTableColumn.ColumnSize, typeof(int));
			DataColumn dataColumn4 = new DataColumn(SchemaTableColumn.NumericPrecision, typeof(short));
			DataColumn dataColumn5 = new DataColumn(SchemaTableColumn.NumericScale, typeof(short));
			DataColumn dataColumn6 = new DataColumn(SchemaTableColumn.DataType, typeof(Type));
			DataColumn dataColumn7 = new DataColumn(SchemaTableColumn.ProviderType, typeof(int));
			DataColumn dataColumn8 = new DataColumn(SchemaTableColumn.IsLong, typeof(bool));
			DataColumn dataColumn9 = new DataColumn(SchemaTableColumn.AllowDBNull, typeof(bool));
			DataColumn dataColumn10 = new DataColumn(SchemaTableOptionalColumn.IsReadOnly, typeof(bool));
			DataColumn dataColumn11 = new DataColumn(SchemaTableOptionalColumn.IsRowVersion, typeof(bool));
			DataColumn dataColumn12 = new DataColumn(SchemaTableColumn.IsUnique, typeof(bool));
			DataColumn dataColumn13 = new DataColumn(SchemaTableColumn.IsKey, typeof(bool));
			DataColumn dataColumn14 = new DataColumn(SchemaTableOptionalColumn.IsAutoIncrement, typeof(bool));
			DataColumn dataColumn15 = new DataColumn(SchemaTableColumn.BaseSchemaName, typeof(string));
			DataColumn dataColumn16 = new DataColumn(SchemaTableOptionalColumn.BaseCatalogName, typeof(string));
			DataColumn dataColumn17 = new DataColumn(SchemaTableColumn.BaseTableName, typeof(string));
			DataColumn dataColumn18 = new DataColumn(SchemaTableColumn.BaseColumnName, typeof(string));
			DataColumn dataColumn19 = new DataColumn(SchemaTableOptionalColumn.AutoIncrementSeed, typeof(long));
			DataColumn dataColumn20 = new DataColumn(SchemaTableOptionalColumn.AutoIncrementStep, typeof(long));
			DataColumn dataColumn21 = new DataColumn(SchemaTableOptionalColumn.DefaultValue, typeof(object));
			DataColumn dataColumn22 = new DataColumn(SchemaTableOptionalColumn.Expression, typeof(string));
			DataColumn dataColumn23 = new DataColumn(SchemaTableOptionalColumn.ColumnMapping, typeof(MappingType));
			DataColumn dataColumn24 = new DataColumn(SchemaTableOptionalColumn.BaseTableNamespace, typeof(string));
			DataColumn dataColumn25 = new DataColumn(SchemaTableOptionalColumn.BaseColumnNamespace, typeof(string));
			dataColumn3.DefaultValue = -1;
			if (table.DataSet != null)
			{
				dataColumn16.DefaultValue = table.DataSet.DataSetName;
			}
			dataColumn17.DefaultValue = table.TableName;
			dataColumn24.DefaultValue = table.Namespace;
			dataColumn11.DefaultValue = false;
			dataColumn8.DefaultValue = false;
			dataColumn10.DefaultValue = false;
			dataColumn13.DefaultValue = false;
			dataColumn14.DefaultValue = false;
			dataColumn19.DefaultValue = 0;
			dataColumn20.DefaultValue = 1;
			dataTable.Columns.Add(dataColumn);
			dataTable.Columns.Add(dataColumn2);
			dataTable.Columns.Add(dataColumn3);
			dataTable.Columns.Add(dataColumn4);
			dataTable.Columns.Add(dataColumn5);
			dataTable.Columns.Add(dataColumn6);
			dataTable.Columns.Add(dataColumn7);
			dataTable.Columns.Add(dataColumn8);
			dataTable.Columns.Add(dataColumn9);
			dataTable.Columns.Add(dataColumn10);
			dataTable.Columns.Add(dataColumn11);
			dataTable.Columns.Add(dataColumn12);
			dataTable.Columns.Add(dataColumn13);
			dataTable.Columns.Add(dataColumn14);
			dataTable.Columns.Add(dataColumn16);
			dataTable.Columns.Add(dataColumn15);
			dataTable.Columns.Add(dataColumn17);
			dataTable.Columns.Add(dataColumn18);
			dataTable.Columns.Add(dataColumn19);
			dataTable.Columns.Add(dataColumn20);
			dataTable.Columns.Add(dataColumn21);
			dataTable.Columns.Add(dataColumn22);
			dataTable.Columns.Add(dataColumn23);
			dataTable.Columns.Add(dataColumn24);
			dataTable.Columns.Add(dataColumn25);
			foreach (object obj in table.Columns)
			{
				DataColumn dataColumn26 = (DataColumn)obj;
				DataRow dataRow = dataTable.NewRow();
				dataRow[dataColumn] = dataColumn26.ColumnName;
				dataRow[dataColumn2] = dataColumn26.Ordinal;
				dataRow[dataColumn6] = dataColumn26.DataType;
				if (dataColumn26.DataType == typeof(string))
				{
					dataRow[dataColumn3] = dataColumn26.MaxLength;
				}
				dataRow[dataColumn9] = dataColumn26.AllowDBNull;
				dataRow[dataColumn10] = dataColumn26.ReadOnly;
				dataRow[dataColumn12] = dataColumn26.Unique;
				if (dataColumn26.AutoIncrement)
				{
					dataRow[dataColumn14] = true;
					dataRow[dataColumn19] = dataColumn26.AutoIncrementSeed;
					dataRow[dataColumn20] = dataColumn26.AutoIncrementStep;
				}
				if (dataColumn26.DefaultValue != DBNull.Value)
				{
					dataRow[dataColumn21] = dataColumn26.DefaultValue;
				}
				if (dataColumn26.Expression.Length != 0)
				{
					bool flag = false;
					DataColumn[] dependency = dataColumn26.DataExpression.GetDependency();
					for (int i = 0; i < dependency.Length; i++)
					{
						if (dependency[i].Table != table)
						{
							flag = true;
							break;
						}
					}
					if (!flag)
					{
						dataRow[dataColumn22] = dataColumn26.Expression;
					}
				}
				dataRow[dataColumn23] = dataColumn26.ColumnMapping;
				dataRow[dataColumn18] = dataColumn26.ColumnName;
				dataRow[dataColumn25] = dataColumn26.Namespace;
				dataTable.Rows.Add(dataRow);
			}
			foreach (DataColumn dataColumn27 in table.PrimaryKey)
			{
				dataTable.Rows[dataColumn27.Ordinal][dataColumn13] = true;
			}
			dataTable.AcceptChanges();
			return dataTable;
		}

		// Token: 0x060008BB RID: 2235 RVA: 0x00027C80 File Offset: 0x00025E80
		private void ValidateOpen(string caller)
		{
			if (!this._isOpen)
			{
				throw ADP.DataReaderClosed(caller);
			}
		}

		// Token: 0x060008BC RID: 2236 RVA: 0x00027C91 File Offset: 0x00025E91
		private void ValidateReader()
		{
			if (this.ReaderIsInvalid)
			{
				throw ExceptionBuilder.InvalidDataTableReader(this._currentDataTable.TableName);
			}
			if (this.IsSchemaChanged)
			{
				throw ExceptionBuilder.DataTableReaderSchemaIsInvalid(this._currentDataTable.TableName);
			}
		}

		// Token: 0x060008BD RID: 2237 RVA: 0x00027CC8 File Offset: 0x00025EC8
		private void ValidateState(string caller)
		{
			this.ValidateOpen(caller);
			if (this._tableCleared)
			{
				throw ExceptionBuilder.EmptyDataTableReader(this._currentDataTable.TableName);
			}
			if (this._currentDataRow == null || this._currentDataTable == null)
			{
				this.ReaderIsInvalid = true;
				throw ExceptionBuilder.InvalidDataTableReader(this._currentDataTable.TableName);
			}
			if (this._currentDataRow.RowState == DataRowState.Deleted || this._currentDataRow.RowState == DataRowState.Detached || this._currentRowRemoved)
			{
				throw ExceptionBuilder.InvalidCurrentRowInDataTableReader();
			}
			if (0 > this._rowCounter || this._currentDataTable.Rows.Count <= this._rowCounter)
			{
				this.ReaderIsInvalid = true;
				throw ExceptionBuilder.InvalidDataTableReader(this._currentDataTable.TableName);
			}
		}

		// Token: 0x060008BE RID: 2238 RVA: 0x00027D80 File Offset: 0x00025F80
		private void ValidateRow(int rowPosition)
		{
			if (this.ReaderIsInvalid)
			{
				throw ExceptionBuilder.InvalidDataTableReader(this._currentDataTable.TableName);
			}
			if (0 > rowPosition || this._currentDataTable.Rows.Count <= rowPosition)
			{
				this.ReaderIsInvalid = true;
				throw ExceptionBuilder.InvalidDataTableReader(this._currentDataTable.TableName);
			}
		}

		// Token: 0x060008BF RID: 2239 RVA: 0x00027DD5 File Offset: 0x00025FD5
		internal void SchemaChanged()
		{
			this.IsSchemaChanged = true;
		}

		// Token: 0x060008C0 RID: 2240 RVA: 0x00027DDE File Offset: 0x00025FDE
		internal void DataTableCleared()
		{
			if (!this._started)
			{
				return;
			}
			this._rowCounter = -1;
			if (!this._reachEORows)
			{
				this._currentRowRemoved = true;
			}
		}

		// Token: 0x060008C1 RID: 2241 RVA: 0x00027E00 File Offset: 0x00026000
		internal void DataChanged(DataRowChangeEventArgs args)
		{
			if (!this._started || (this._rowCounter == -1 && !this._tableCleared))
			{
				return;
			}
			DataRowAction action = args.Action;
			if (action <= DataRowAction.Rollback)
			{
				if (action != DataRowAction.Delete && action != DataRowAction.Rollback)
				{
					return;
				}
			}
			else if (action != DataRowAction.Commit)
			{
				if (action != DataRowAction.Add)
				{
					return;
				}
				this.ValidateRow(this._rowCounter + 1);
				if (this._currentDataRow == this._currentDataTable.Rows[this._rowCounter + 1])
				{
					this._rowCounter++;
					return;
				}
				return;
			}
			if (args.Row.RowState == DataRowState.Detached)
			{
				if (args.Row != this._currentDataRow)
				{
					if (this._rowCounter != 0)
					{
						this.ValidateRow(this._rowCounter - 1);
						if (this._currentDataRow == this._currentDataTable.Rows[this._rowCounter - 1])
						{
							this._rowCounter--;
							return;
						}
					}
				}
				else
				{
					this._currentRowRemoved = true;
					if (this._rowCounter > 0)
					{
						this._rowCounter--;
						this._currentDataRow = this._currentDataTable.Rows[this._rowCounter];
						return;
					}
					this._rowCounter = -1;
					this._currentDataRow = null;
				}
			}
		}

		// Token: 0x040005C5 RID: 1477
		private readonly DataTable[] _tables;

		// Token: 0x040005C6 RID: 1478
		private bool _isOpen = true;

		// Token: 0x040005C7 RID: 1479
		private DataTable _schemaTable;

		// Token: 0x040005C8 RID: 1480
		private int _tableCounter = -1;

		// Token: 0x040005C9 RID: 1481
		private int _rowCounter = -1;

		// Token: 0x040005CA RID: 1482
		private DataTable _currentDataTable;

		// Token: 0x040005CB RID: 1483
		private DataRow _currentDataRow;

		// Token: 0x040005CC RID: 1484
		private bool _hasRows = true;

		// Token: 0x040005CD RID: 1485
		private bool _reachEORows;

		// Token: 0x040005CE RID: 1486
		private bool _currentRowRemoved;

		// Token: 0x040005CF RID: 1487
		private bool _schemaIsChanged;

		// Token: 0x040005D0 RID: 1488
		private bool _started;

		// Token: 0x040005D1 RID: 1489
		private bool _readerIsInvalid;

		// Token: 0x040005D2 RID: 1490
		private DataTableReaderListener _listener;

		// Token: 0x040005D3 RID: 1491
		private bool _tableCleared;
	}
}
