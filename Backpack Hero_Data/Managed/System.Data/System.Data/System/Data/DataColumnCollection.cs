using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Globalization;
using Unity;

namespace System.Data
{
	/// <summary>Represents a collection of <see cref="T:System.Data.DataColumn" /> objects for a <see cref="T:System.Data.DataTable" />.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200004E RID: 78
	[DefaultEvent("CollectionChanged")]
	public sealed class DataColumnCollection : InternalDataCollectionBase
	{
		// Token: 0x06000379 RID: 889 RVA: 0x00010B72 File Offset: 0x0000ED72
		internal DataColumnCollection(DataTable table)
		{
			this._list = new ArrayList();
			this._defaultNameIndex = 1;
			this._columnsImplementingIChangeTracking = Array.Empty<DataColumn>();
			base..ctor();
			this._table = table;
			this._columnFromName = new Dictionary<string, DataColumn>();
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x0600037A RID: 890 RVA: 0x00010BA9 File Offset: 0x0000EDA9
		protected override ArrayList List
		{
			get
			{
				return this._list;
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x0600037B RID: 891 RVA: 0x00010BB1 File Offset: 0x0000EDB1
		internal DataColumn[] ColumnsImplementingIChangeTracking
		{
			get
			{
				return this._columnsImplementingIChangeTracking;
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x0600037C RID: 892 RVA: 0x00010BB9 File Offset: 0x0000EDB9
		internal int ColumnsImplementingIChangeTrackingCount
		{
			get
			{
				return this._nColumnsImplementingIChangeTracking;
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x0600037D RID: 893 RVA: 0x00010BC1 File Offset: 0x0000EDC1
		internal int ColumnsImplementingIRevertibleChangeTrackingCount
		{
			get
			{
				return this._nColumnsImplementingIRevertibleChangeTracking;
			}
		}

		/// <summary>Gets the <see cref="T:System.Data.DataColumn" /> from the collection at the specified index.</summary>
		/// <returns>The <see cref="T:System.Data.DataColumn" /> at the specified index.</returns>
		/// <param name="index">The zero-based index of the column to return. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">The index value is greater than the number of items in the collection. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x170000E5 RID: 229
		public DataColumn this[int index]
		{
			get
			{
				DataColumn dataColumn;
				try
				{
					dataColumn = (DataColumn)this._list[index];
				}
				catch (ArgumentOutOfRangeException)
				{
					throw ExceptionBuilder.ColumnOutOfRange(index);
				}
				return dataColumn;
			}
		}

		/// <summary>Gets the <see cref="T:System.Data.DataColumn" /> from the collection with the specified name.</summary>
		/// <returns>The <see cref="T:System.Data.DataColumn" /> in the collection with the specified <see cref="P:System.Data.DataColumn.ColumnName" />; otherwise a null value if the <see cref="T:System.Data.DataColumn" /> does not exist.</returns>
		/// <param name="name">The <see cref="P:System.Data.DataColumn.ColumnName" /> of the column to return. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x170000E6 RID: 230
		public DataColumn this[string name]
		{
			get
			{
				if (name == null)
				{
					throw ExceptionBuilder.ArgumentNull("name");
				}
				DataColumn dataColumn;
				if (!this._columnFromName.TryGetValue(name, out dataColumn) || dataColumn == null)
				{
					int num = this.IndexOfCaseInsensitive(name);
					if (0 <= num)
					{
						dataColumn = (DataColumn)this._list[num];
					}
					else if (-2 == num)
					{
						throw ExceptionBuilder.CaseInsensitiveNameConflict(name);
					}
				}
				return dataColumn;
			}
		}

		// Token: 0x170000E7 RID: 231
		internal DataColumn this[string name, string ns]
		{
			get
			{
				DataColumn dataColumn;
				if (this._columnFromName.TryGetValue(name, out dataColumn) && dataColumn != null && dataColumn.Namespace == ns)
				{
					return dataColumn;
				}
				return null;
			}
		}

		// Token: 0x06000381 RID: 897 RVA: 0x00010C95 File Offset: 0x0000EE95
		internal void EnsureAdditionalCapacity(int capacity)
		{
			if (this._list.Capacity < capacity + this._list.Count)
			{
				this._list.Capacity = capacity + this._list.Count;
			}
		}

		/// <summary>Creates and adds the specified <see cref="T:System.Data.DataColumn" /> object to the <see cref="T:System.Data.DataColumnCollection" />.</summary>
		/// <param name="column">The <see cref="T:System.Data.DataColumn" /> to add. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="column" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The column already belongs to this collection, or to another collection. </exception>
		/// <exception cref="T:System.Data.DuplicateNameException">The collection already has a column with the specified name. (The comparison is not case-sensitive.) </exception>
		/// <exception cref="T:System.Data.InvalidExpressionException">The expression is invalid. See the <see cref="P:System.Data.DataColumn.Expression" /> property for more information about how to create expressions. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000382 RID: 898 RVA: 0x00010CC9 File Offset: 0x0000EEC9
		public void Add(DataColumn column)
		{
			this.AddAt(-1, column);
		}

		// Token: 0x06000383 RID: 899 RVA: 0x00010CD4 File Offset: 0x0000EED4
		internal void AddAt(int index, DataColumn column)
		{
			if (column != null && column.ColumnMapping == MappingType.SimpleContent)
			{
				if (this._table.XmlText != null && this._table.XmlText != column)
				{
					throw ExceptionBuilder.CannotAddColumn3();
				}
				if (this._table.ElementColumnCount > 0)
				{
					throw ExceptionBuilder.CannotAddColumn4(column.ColumnName);
				}
				this.OnCollectionChanging(new CollectionChangeEventArgs(CollectionChangeAction.Add, column));
				this.BaseAdd(column);
				if (index != -1)
				{
					this.ArrayAdd(index, column);
				}
				else
				{
					this.ArrayAdd(column);
				}
				this._table.XmlText = column;
			}
			else
			{
				this.OnCollectionChanging(new CollectionChangeEventArgs(CollectionChangeAction.Add, column));
				this.BaseAdd(column);
				if (index != -1)
				{
					this.ArrayAdd(index, column);
				}
				else
				{
					this.ArrayAdd(column);
				}
				if (column.ColumnMapping == MappingType.Element)
				{
					DataTable table = this._table;
					int elementColumnCount = table.ElementColumnCount;
					table.ElementColumnCount = elementColumnCount + 1;
				}
			}
			if (!this._table.fInitInProgress && column != null && column.Computed)
			{
				column.Expression = column.Expression;
			}
			this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Add, column));
		}

		/// <summary>Copies the elements of the specified <see cref="T:System.Data.DataColumn" /> array to the end of the collection.</summary>
		/// <param name="columns">The array of <see cref="T:System.Data.DataColumn" /> objects to add to the collection. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000384 RID: 900 RVA: 0x00010DD8 File Offset: 0x0000EFD8
		public void AddRange(DataColumn[] columns)
		{
			if (this._table.fInitInProgress)
			{
				this._delayedAddRangeColumns = columns;
				return;
			}
			if (columns != null)
			{
				foreach (DataColumn dataColumn in columns)
				{
					if (dataColumn != null)
					{
						this.Add(dataColumn);
					}
				}
			}
		}

		/// <summary>Creates and adds a <see cref="T:System.Data.DataColumn" /> object that has the specified name, type, and expression to the <see cref="T:System.Data.DataColumnCollection" />.</summary>
		/// <returns>The newly created <see cref="T:System.Data.DataColumn" />.</returns>
		/// <param name="columnName">The name to use when you create the column. </param>
		/// <param name="type">The <see cref="P:System.Data.DataColumn.DataType" /> of the new column. </param>
		/// <param name="expression">The expression to assign to the <see cref="P:System.Data.DataColumn.Expression" /> property. </param>
		/// <exception cref="T:System.Data.DuplicateNameException">The collection already has a column with the specified name. (The comparison is not case-sensitive.) </exception>
		/// <exception cref="T:System.Data.InvalidExpressionException">The expression is invalid. See the <see cref="P:System.Data.DataColumn.Expression" /> property for more information about how to create expressions. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000385 RID: 901 RVA: 0x00010E1C File Offset: 0x0000F01C
		public DataColumn Add(string columnName, Type type, string expression)
		{
			DataColumn dataColumn = new DataColumn(columnName, type, expression);
			this.Add(dataColumn);
			return dataColumn;
		}

		/// <summary>Creates and adds a <see cref="T:System.Data.DataColumn" /> object that has the specified name and type to the <see cref="T:System.Data.DataColumnCollection" />.</summary>
		/// <returns>The newly created <see cref="T:System.Data.DataColumn" />.</returns>
		/// <param name="columnName">The <see cref="P:System.Data.DataColumn.ColumnName" /> to use when you create the column. </param>
		/// <param name="type">The <see cref="P:System.Data.DataColumn.DataType" /> of the new column. </param>
		/// <exception cref="T:System.Data.DuplicateNameException">The collection already has a column with the specified name. (The comparison is not case-sensitive.) </exception>
		/// <exception cref="T:System.Data.InvalidExpressionException">The expression is invalid. See the <see cref="P:System.Data.DataColumn.Expression" /> property for more information about how to create expressions. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000386 RID: 902 RVA: 0x00010E3C File Offset: 0x0000F03C
		public DataColumn Add(string columnName, Type type)
		{
			DataColumn dataColumn = new DataColumn(columnName, type);
			this.Add(dataColumn);
			return dataColumn;
		}

		/// <summary>Creates and adds a <see cref="T:System.Data.DataColumn" /> object that has the specified name to the <see cref="T:System.Data.DataColumnCollection" />.</summary>
		/// <returns>The newly created <see cref="T:System.Data.DataColumn" />.</returns>
		/// <param name="columnName">The name of the column. </param>
		/// <exception cref="T:System.Data.DuplicateNameException">The collection already has a column with the specified name. (The comparison is not case-sensitive.) </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000387 RID: 903 RVA: 0x00010E5C File Offset: 0x0000F05C
		public DataColumn Add(string columnName)
		{
			DataColumn dataColumn = new DataColumn(columnName);
			this.Add(dataColumn);
			return dataColumn;
		}

		/// <summary>Creates and adds a <see cref="T:System.Data.DataColumn" /> object to the <see cref="T:System.Data.DataColumnCollection" />.</summary>
		/// <returns>The newly created <see cref="T:System.Data.DataColumn" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000388 RID: 904 RVA: 0x00010E78 File Offset: 0x0000F078
		public DataColumn Add()
		{
			DataColumn dataColumn = new DataColumn();
			this.Add(dataColumn);
			return dataColumn;
		}

		/// <summary>Occurs when the columns collection changes, either by adding or removing a column.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000389 RID: 905 RVA: 0x00010E94 File Offset: 0x0000F094
		// (remove) Token: 0x0600038A RID: 906 RVA: 0x00010ECC File Offset: 0x0000F0CC
		public event CollectionChangeEventHandler CollectionChanged;

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x0600038B RID: 907 RVA: 0x00010F04 File Offset: 0x0000F104
		// (remove) Token: 0x0600038C RID: 908 RVA: 0x00010F3C File Offset: 0x0000F13C
		internal event CollectionChangeEventHandler CollectionChanging;

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x0600038D RID: 909 RVA: 0x00010F74 File Offset: 0x0000F174
		// (remove) Token: 0x0600038E RID: 910 RVA: 0x00010FAC File Offset: 0x0000F1AC
		internal event CollectionChangeEventHandler ColumnPropertyChanged;

		// Token: 0x0600038F RID: 911 RVA: 0x00010FE1 File Offset: 0x0000F1E1
		private void ArrayAdd(DataColumn column)
		{
			this._list.Add(column);
			column.SetOrdinalInternal(this._list.Count - 1);
			this.CheckIChangeTracking(column);
		}

		// Token: 0x06000390 RID: 912 RVA: 0x0001100A File Offset: 0x0000F20A
		private void ArrayAdd(int index, DataColumn column)
		{
			this._list.Insert(index, column);
			this.CheckIChangeTracking(column);
		}

		// Token: 0x06000391 RID: 913 RVA: 0x00011020 File Offset: 0x0000F220
		private void ArrayRemove(DataColumn column)
		{
			column.SetOrdinalInternal(-1);
			this._list.Remove(column);
			int count = this._list.Count;
			for (int i = 0; i < count; i++)
			{
				((DataColumn)this._list[i]).SetOrdinalInternal(i);
			}
			if (column.ImplementsIChangeTracking)
			{
				this.RemoveColumnsImplementingIChangeTrackingList(column);
			}
		}

		// Token: 0x06000392 RID: 914 RVA: 0x00011080 File Offset: 0x0000F280
		internal string AssignName()
		{
			int num = this._defaultNameIndex;
			this._defaultNameIndex = num + 1;
			string text = this.MakeName(num);
			while (this._columnFromName.ContainsKey(text))
			{
				num = this._defaultNameIndex;
				this._defaultNameIndex = num + 1;
				text = this.MakeName(num);
			}
			return text;
		}

		// Token: 0x06000393 RID: 915 RVA: 0x000110D0 File Offset: 0x0000F2D0
		private void BaseAdd(DataColumn column)
		{
			if (column == null)
			{
				throw ExceptionBuilder.ArgumentNull("column");
			}
			if (column._table == this._table)
			{
				throw ExceptionBuilder.CannotAddColumn1(column.ColumnName);
			}
			if (column._table != null)
			{
				throw ExceptionBuilder.CannotAddColumn2(column.ColumnName);
			}
			if (column.ColumnName.Length == 0)
			{
				column.ColumnName = this.AssignName();
			}
			this.RegisterColumnName(column.ColumnName, column);
			try
			{
				column.SetTable(this._table);
				if (!this._table.fInitInProgress && column.Computed && column.DataExpression.DependsOn(column))
				{
					throw ExceptionBuilder.ExpressionCircular();
				}
				if (0 < this._table.RecordCapacity)
				{
					column.SetCapacity(this._table.RecordCapacity);
				}
				for (int i = 0; i < this._table.RecordCapacity; i++)
				{
					column.InitializeRecord(i);
				}
				if (this._table.DataSet != null)
				{
					column.OnSetDataSet();
				}
			}
			catch (Exception ex) when (ADP.IsCatchableOrSecurityExceptionType(ex))
			{
				this.UnregisterName(column.ColumnName);
				throw;
			}
		}

		// Token: 0x06000394 RID: 916 RVA: 0x000111FC File Offset: 0x0000F3FC
		private void BaseGroupSwitch(DataColumn[] oldArray, int oldLength, DataColumn[] newArray, int newLength)
		{
			int num = 0;
			for (int i = 0; i < oldLength; i++)
			{
				bool flag = false;
				for (int j = num; j < newLength; j++)
				{
					if (oldArray[i] == newArray[j])
					{
						if (num == j)
						{
							num++;
						}
						flag = true;
						break;
					}
				}
				if (!flag && oldArray[i].Table == this._table)
				{
					this.BaseRemove(oldArray[i]);
					this._list.Remove(oldArray[i]);
					oldArray[i].SetOrdinalInternal(-1);
				}
			}
			for (int k = 0; k < newLength; k++)
			{
				if (newArray[k].Table != this._table)
				{
					this.BaseAdd(newArray[k]);
					this._list.Add(newArray[k]);
				}
				newArray[k].SetOrdinalInternal(k);
			}
		}

		// Token: 0x06000395 RID: 917 RVA: 0x000112B4 File Offset: 0x0000F4B4
		private void BaseRemove(DataColumn column)
		{
			if (this.CanRemove(column, true))
			{
				if (column._errors > 0)
				{
					for (int i = 0; i < this._table.Rows.Count; i++)
					{
						this._table.Rows[i].ClearError(column);
					}
				}
				this.UnregisterName(column.ColumnName);
				column.SetTable(null);
			}
		}

		/// <summary>Checks whether a specific column can be removed from the collection.</summary>
		/// <returns>true if the column can be removed. false if,The <paramref name="column" /> parameter is null.The column does not belong to this collection.The column is part of a relationship.Another column's expression depends on this column.</returns>
		/// <param name="column">A <see cref="T:System.Data.DataColumn" /> in the collection.</param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000396 RID: 918 RVA: 0x00011319 File Offset: 0x0000F519
		public bool CanRemove(DataColumn column)
		{
			return this.CanRemove(column, false);
		}

		// Token: 0x06000397 RID: 919 RVA: 0x00011324 File Offset: 0x0000F524
		internal bool CanRemove(DataColumn column, bool fThrowException)
		{
			if (column == null)
			{
				if (!fThrowException)
				{
					return false;
				}
				throw ExceptionBuilder.ArgumentNull("column");
			}
			else if (column._table != this._table)
			{
				if (!fThrowException)
				{
					return false;
				}
				throw ExceptionBuilder.CannotRemoveColumn();
			}
			else
			{
				this._table.OnRemoveColumnInternal(column);
				if (this._table._primaryKey == null || !this._table._primaryKey.Key.ContainsColumn(column))
				{
					int i = 0;
					while (i < this._table.ParentRelations.Count)
					{
						if (this._table.ParentRelations[i].ChildKey.ContainsColumn(column))
						{
							if (!fThrowException)
							{
								return false;
							}
							throw ExceptionBuilder.CannotRemoveChildKey(this._table.ParentRelations[i].RelationName);
						}
						else
						{
							i++;
						}
					}
					int j = 0;
					while (j < this._table.ChildRelations.Count)
					{
						if (this._table.ChildRelations[j].ParentKey.ContainsColumn(column))
						{
							if (!fThrowException)
							{
								return false;
							}
							throw ExceptionBuilder.CannotRemoveChildKey(this._table.ChildRelations[j].RelationName);
						}
						else
						{
							j++;
						}
					}
					int k = 0;
					while (k < this._table.Constraints.Count)
					{
						if (this._table.Constraints[k].ContainsColumn(column))
						{
							if (!fThrowException)
							{
								return false;
							}
							throw ExceptionBuilder.CannotRemoveConstraint(this._table.Constraints[k].ConstraintName, this._table.Constraints[k].Table.TableName);
						}
						else
						{
							k++;
						}
					}
					if (this._table.DataSet != null)
					{
						ParentForeignKeyConstraintEnumerator parentForeignKeyConstraintEnumerator = new ParentForeignKeyConstraintEnumerator(this._table.DataSet, this._table);
						while (parentForeignKeyConstraintEnumerator.GetNext())
						{
							Constraint constraint = parentForeignKeyConstraintEnumerator.GetConstraint();
							if (((ForeignKeyConstraint)constraint).ParentKey.ContainsColumn(column))
							{
								if (!fThrowException)
								{
									return false;
								}
								throw ExceptionBuilder.CannotRemoveConstraint(constraint.ConstraintName, constraint.Table.TableName);
							}
						}
					}
					if (column._dependentColumns != null)
					{
						for (int l = 0; l < column._dependentColumns.Count; l++)
						{
							DataColumn dataColumn = column._dependentColumns[l];
							if ((!this._fInClear || (dataColumn.Table != this._table && dataColumn.Table != null)) && dataColumn.Table != null)
							{
								DataExpression dataExpression = dataColumn.DataExpression;
								if (dataExpression != null && dataExpression.DependsOn(column))
								{
									if (!fThrowException)
									{
										return false;
									}
									throw ExceptionBuilder.CannotRemoveExpression(dataColumn.ColumnName, dataColumn.Expression);
								}
							}
						}
					}
					foreach (Index index in this._table.LiveIndexes)
					{
					}
					return true;
				}
				if (!fThrowException)
				{
					return false;
				}
				throw ExceptionBuilder.CannotRemovePrimaryKey();
			}
		}

		// Token: 0x06000398 RID: 920 RVA: 0x00011608 File Offset: 0x0000F808
		private void CheckIChangeTracking(DataColumn column)
		{
			if (column.ImplementsIRevertibleChangeTracking)
			{
				this._nColumnsImplementingIRevertibleChangeTracking++;
				this._nColumnsImplementingIChangeTracking++;
				this.AddColumnsImplementingIChangeTrackingList(column);
				return;
			}
			if (column.ImplementsIChangeTracking)
			{
				this._nColumnsImplementingIChangeTracking++;
				this.AddColumnsImplementingIChangeTrackingList(column);
			}
		}

		/// <summary>Clears the collection of any columns.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000399 RID: 921 RVA: 0x00011660 File Offset: 0x0000F860
		public void Clear()
		{
			int count = this._list.Count;
			DataColumn[] array = new DataColumn[this._list.Count];
			this._list.CopyTo(array, 0);
			this.OnCollectionChanging(InternalDataCollectionBase.s_refreshEventArgs);
			if (this._table.fInitInProgress && this._delayedAddRangeColumns != null)
			{
				this._delayedAddRangeColumns = null;
			}
			try
			{
				this._fInClear = true;
				this.BaseGroupSwitch(array, count, null, 0);
				this._fInClear = false;
			}
			catch (Exception ex) when (ADP.IsCatchableOrSecurityExceptionType(ex))
			{
				this._fInClear = false;
				this.BaseGroupSwitch(null, 0, array, count);
				this._list.Clear();
				for (int i = 0; i < count; i++)
				{
					this._list.Add(array[i]);
				}
				throw;
			}
			this._list.Clear();
			this._table.ElementColumnCount = 0;
			this.OnCollectionChanged(InternalDataCollectionBase.s_refreshEventArgs);
		}

		/// <summary>Checks whether the collection contains a column with the specified name.</summary>
		/// <returns>true if a column exists with this name; otherwise, false.</returns>
		/// <param name="name">The <see cref="P:System.Data.DataColumn.ColumnName" /> of the column to look for. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600039A RID: 922 RVA: 0x0001175C File Offset: 0x0000F95C
		public bool Contains(string name)
		{
			DataColumn dataColumn;
			return (this._columnFromName.TryGetValue(name, out dataColumn) && dataColumn != null) || this.IndexOfCaseInsensitive(name) >= 0;
		}

		// Token: 0x0600039B RID: 923 RVA: 0x0001178C File Offset: 0x0000F98C
		internal bool Contains(string name, bool caseSensitive)
		{
			DataColumn dataColumn;
			return (this._columnFromName.TryGetValue(name, out dataColumn) && dataColumn != null) || (!caseSensitive && this.IndexOfCaseInsensitive(name) >= 0);
		}

		/// <summary>Copies the entire collection into an existing array, starting at a specified index within the array.</summary>
		/// <param name="array">An array of <see cref="T:System.Data.DataColumn" /> objects to copy the collection into. </param>
		/// <param name="index">The index to start from.</param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600039C RID: 924 RVA: 0x000117C0 File Offset: 0x0000F9C0
		public void CopyTo(DataColumn[] array, int index)
		{
			if (array == null)
			{
				throw ExceptionBuilder.ArgumentNull("array");
			}
			if (index < 0)
			{
				throw ExceptionBuilder.ArgumentOutOfRange("index");
			}
			if (array.Length - index < this._list.Count)
			{
				throw ExceptionBuilder.InvalidOffsetLength();
			}
			for (int i = 0; i < this._list.Count; i++)
			{
				array[index + i] = (DataColumn)this._list[i];
			}
		}

		/// <summary>Gets the index of a column specified by name.</summary>
		/// <returns>The index of the column specified by <paramref name="column" /> if it is found; otherwise, -1.</returns>
		/// <param name="column">The name of the column to return. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600039D RID: 925 RVA: 0x00011830 File Offset: 0x0000FA30
		public int IndexOf(DataColumn column)
		{
			int count = this._list.Count;
			for (int i = 0; i < count; i++)
			{
				if (column == (DataColumn)this._list[i])
				{
					return i;
				}
			}
			return -1;
		}

		/// <summary>Gets the index of the column with the specific name (the name is not case sensitive).</summary>
		/// <returns>The zero-based index of the column with the specified name, or -1 if the column does not exist in the collection.</returns>
		/// <param name="columnName">The name of the column to find. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600039E RID: 926 RVA: 0x0001186C File Offset: 0x0000FA6C
		public int IndexOf(string columnName)
		{
			if (columnName != null && 0 < columnName.Length)
			{
				int count = this.Count;
				DataColumn dataColumn;
				if (this._columnFromName.TryGetValue(columnName, out dataColumn) && dataColumn != null)
				{
					for (int i = 0; i < count; i++)
					{
						if (dataColumn == this._list[i])
						{
							return i;
						}
					}
				}
				else
				{
					int num = this.IndexOfCaseInsensitive(columnName);
					if (num >= 0)
					{
						return num;
					}
					return -1;
				}
			}
			return -1;
		}

		// Token: 0x0600039F RID: 927 RVA: 0x000118D0 File Offset: 0x0000FAD0
		internal int IndexOfCaseInsensitive(string name)
		{
			int specialHashCode = this._table.GetSpecialHashCode(name);
			int num = -1;
			for (int i = 0; i < this.Count; i++)
			{
				DataColumn dataColumn = (DataColumn)this._list[i];
				if ((specialHashCode == 0 || dataColumn._hashCode == 0 || dataColumn._hashCode == specialHashCode) && base.NamesEqual(dataColumn.ColumnName, name, false, this._table.Locale) != 0)
				{
					if (num != -1)
					{
						return -2;
					}
					num = i;
				}
			}
			return num;
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x0001194C File Offset: 0x0000FB4C
		internal void FinishInitCollection()
		{
			if (this._delayedAddRangeColumns != null)
			{
				foreach (DataColumn dataColumn in this._delayedAddRangeColumns)
				{
					if (dataColumn != null)
					{
						this.Add(dataColumn);
					}
				}
				foreach (DataColumn dataColumn2 in this._delayedAddRangeColumns)
				{
					if (dataColumn2 != null)
					{
						dataColumn2.FinishInitInProgress();
					}
				}
				this._delayedAddRangeColumns = null;
			}
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x000119AD File Offset: 0x0000FBAD
		private string MakeName(int index)
		{
			if (index != 1)
			{
				return "Column" + index.ToString(CultureInfo.InvariantCulture);
			}
			return "Column1";
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x000119D0 File Offset: 0x0000FBD0
		internal void MoveTo(DataColumn column, int newPosition)
		{
			if (0 > newPosition || newPosition > this.Count - 1)
			{
				throw ExceptionBuilder.InvalidOrdinal("ordinal", newPosition);
			}
			if (column.ImplementsIChangeTracking)
			{
				this.RemoveColumnsImplementingIChangeTrackingList(column);
			}
			this._list.Remove(column);
			this._list.Insert(newPosition, column);
			int count = this._list.Count;
			for (int i = 0; i < count; i++)
			{
				((DataColumn)this._list[i]).SetOrdinalInternal(i);
			}
			this.CheckIChangeTracking(column);
			this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Refresh, column));
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x00011A64 File Offset: 0x0000FC64
		private void OnCollectionChanged(CollectionChangeEventArgs ccevent)
		{
			this._table.UpdatePropertyDescriptorCollectionCache();
			if (ccevent != null && !this._table.SchemaLoading && !this._table.fInitInProgress)
			{
				DataColumn dataColumn = (DataColumn)ccevent.Element;
			}
			CollectionChangeEventHandler collectionChanged = this.CollectionChanged;
			if (collectionChanged == null)
			{
				return;
			}
			collectionChanged(this, ccevent);
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x00011AB7 File Offset: 0x0000FCB7
		private void OnCollectionChanging(CollectionChangeEventArgs ccevent)
		{
			CollectionChangeEventHandler collectionChanging = this.CollectionChanging;
			if (collectionChanging == null)
			{
				return;
			}
			collectionChanging(this, ccevent);
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x00011ACB File Offset: 0x0000FCCB
		internal void OnColumnPropertyChanged(CollectionChangeEventArgs ccevent)
		{
			this._table.UpdatePropertyDescriptorCollectionCache();
			CollectionChangeEventHandler columnPropertyChanged = this.ColumnPropertyChanged;
			if (columnPropertyChanged == null)
			{
				return;
			}
			columnPropertyChanged(this, ccevent);
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x00011AEC File Offset: 0x0000FCEC
		internal void RegisterColumnName(string name, DataColumn column)
		{
			try
			{
				this._columnFromName.Add(name, column);
				if (column != null)
				{
					column._hashCode = this._table.GetSpecialHashCode(name);
				}
			}
			catch (ArgumentException)
			{
				if (this._columnFromName[name] == null)
				{
					throw ExceptionBuilder.CannotAddDuplicate2(name);
				}
				if (column != null)
				{
					throw ExceptionBuilder.CannotAddDuplicate(name);
				}
				throw ExceptionBuilder.CannotAddDuplicate3(name);
			}
			if (column == null && base.NamesEqual(name, this.MakeName(this._defaultNameIndex), true, this._table.Locale) != 0)
			{
				do
				{
					this._defaultNameIndex++;
				}
				while (this.Contains(this.MakeName(this._defaultNameIndex)));
			}
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x00011B9C File Offset: 0x0000FD9C
		internal bool CanRegisterName(string name)
		{
			return !this._columnFromName.ContainsKey(name);
		}

		/// <summary>Removes the specified <see cref="T:System.Data.DataColumn" /> object from the collection.</summary>
		/// <param name="column">The <see cref="T:System.Data.DataColumn" /> to remove. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="column" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The column does not belong to this collection.-Or- The column is part of a relationship.-Or- Another column's expression depends on this column. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060003A8 RID: 936 RVA: 0x00011BB0 File Offset: 0x0000FDB0
		public void Remove(DataColumn column)
		{
			this.OnCollectionChanging(new CollectionChangeEventArgs(CollectionChangeAction.Remove, column));
			this.BaseRemove(column);
			this.ArrayRemove(column);
			this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Remove, column));
			if (column.ColumnMapping == MappingType.Element)
			{
				DataTable table = this._table;
				int elementColumnCount = table.ElementColumnCount;
				table.ElementColumnCount = elementColumnCount - 1;
			}
		}

		/// <summary>Removes the column at the specified index from the collection.</summary>
		/// <param name="index">The index of the column to remove. </param>
		/// <exception cref="T:System.ArgumentException">The collection does not have a column at the specified index. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060003A9 RID: 937 RVA: 0x00011C04 File Offset: 0x0000FE04
		public void RemoveAt(int index)
		{
			DataColumn dataColumn = this[index];
			if (dataColumn == null)
			{
				throw ExceptionBuilder.ColumnOutOfRange(index);
			}
			this.Remove(dataColumn);
		}

		/// <summary>Removes the <see cref="T:System.Data.DataColumn" /> object that has the specified name from the collection.</summary>
		/// <param name="name">The name of the column to remove. </param>
		/// <exception cref="T:System.ArgumentException">The collection does not have a column with the specified name. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060003AA RID: 938 RVA: 0x00011C2C File Offset: 0x0000FE2C
		public void Remove(string name)
		{
			DataColumn dataColumn = this[name];
			if (dataColumn == null)
			{
				throw ExceptionBuilder.ColumnNotInTheTable(name, this._table.TableName);
			}
			this.Remove(dataColumn);
		}

		// Token: 0x060003AB RID: 939 RVA: 0x00011C60 File Offset: 0x0000FE60
		internal void UnregisterName(string name)
		{
			this._columnFromName.Remove(name);
			if (base.NamesEqual(name, this.MakeName(this._defaultNameIndex - 1), true, this._table.Locale) != 0)
			{
				do
				{
					this._defaultNameIndex--;
				}
				while (this._defaultNameIndex > 1 && !this.Contains(this.MakeName(this._defaultNameIndex - 1)));
			}
		}

		// Token: 0x060003AC RID: 940 RVA: 0x00011CCC File Offset: 0x0000FECC
		private void AddColumnsImplementingIChangeTrackingList(DataColumn dataColumn)
		{
			DataColumn[] columnsImplementingIChangeTracking = this._columnsImplementingIChangeTracking;
			DataColumn[] array = new DataColumn[columnsImplementingIChangeTracking.Length + 1];
			columnsImplementingIChangeTracking.CopyTo(array, 0);
			array[columnsImplementingIChangeTracking.Length] = dataColumn;
			this._columnsImplementingIChangeTracking = array;
		}

		// Token: 0x060003AD RID: 941 RVA: 0x00011D00 File Offset: 0x0000FF00
		private void RemoveColumnsImplementingIChangeTrackingList(DataColumn dataColumn)
		{
			DataColumn[] columnsImplementingIChangeTracking = this._columnsImplementingIChangeTracking;
			DataColumn[] array = new DataColumn[columnsImplementingIChangeTracking.Length - 1];
			int i = 0;
			int num = 0;
			while (i < columnsImplementingIChangeTracking.Length)
			{
				if (columnsImplementingIChangeTracking[i] != dataColumn)
				{
					array[num++] = columnsImplementingIChangeTracking[i];
				}
				i++;
			}
			this._columnsImplementingIChangeTracking = array;
		}

		// Token: 0x060003AE RID: 942 RVA: 0x0000E24C File Offset: 0x0000C44C
		internal DataColumnCollection()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040004D5 RID: 1237
		private readonly DataTable _table;

		// Token: 0x040004D6 RID: 1238
		private readonly ArrayList _list;

		// Token: 0x040004D7 RID: 1239
		private int _defaultNameIndex;

		// Token: 0x040004D8 RID: 1240
		private DataColumn[] _delayedAddRangeColumns;

		// Token: 0x040004D9 RID: 1241
		private readonly Dictionary<string, DataColumn> _columnFromName;

		// Token: 0x040004DA RID: 1242
		private bool _fInClear;

		// Token: 0x040004DB RID: 1243
		private DataColumn[] _columnsImplementingIChangeTracking;

		// Token: 0x040004DC RID: 1244
		private int _nColumnsImplementingIChangeTracking;

		// Token: 0x040004DD RID: 1245
		private int _nColumnsImplementingIRevertibleChangeTracking;
	}
}
