using System;
using System.ComponentModel;
using Unity;

namespace System.Data
{
	/// <summary>Represents a customized view of a <see cref="T:System.Data.DataRow" />.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200006F RID: 111
	public class DataRowView : ICustomTypeDescriptor, IEditableObject, IDataErrorInfo, INotifyPropertyChanged
	{
		// Token: 0x06000627 RID: 1575 RVA: 0x000179C7 File Offset: 0x00015BC7
		internal DataRowView(DataView dataView, DataRow row)
		{
			this._dataView = dataView;
			this._row = row;
		}

		/// <summary>Gets a value indicating whether the current <see cref="T:System.Data.DataRowView" /> is identical to the specified object.</summary>
		/// <returns>true if <paramref name="object" /> is a <see cref="T:System.Data.DataRowView" /> and it returns the same row as the current <see cref="T:System.Data.DataRowView" />; otherwise false.</returns>
		/// <param name="other">An <see cref="T:System.Object" /> to be compared. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000628 RID: 1576 RVA: 0x000179DD File Offset: 0x00015BDD
		public override bool Equals(object other)
		{
			return this == other;
		}

		/// <summary>Returns the hash code of the <see cref="T:System.Data.DataRow" /> object.</summary>
		/// <returns>A 32-bit signed integer hash code 1, which represents Boolean true if the value of this instance is nonzero; otherwise the integer zero, which represents Boolean false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000629 RID: 1577 RVA: 0x000179E3 File Offset: 0x00015BE3
		public override int GetHashCode()
		{
			return this.Row.GetHashCode();
		}

		/// <summary>Gets the <see cref="T:System.Data.DataView" /> to which this row belongs.</summary>
		/// <returns>The DataView to which this row belongs.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000126 RID: 294
		// (get) Token: 0x0600062A RID: 1578 RVA: 0x000179F0 File Offset: 0x00015BF0
		public DataView DataView
		{
			get
			{
				return this._dataView;
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x0600062B RID: 1579 RVA: 0x000179F8 File Offset: 0x00015BF8
		internal int ObjectID
		{
			get
			{
				return this._row._objectID;
			}
		}

		/// <summary>Gets or sets a value in a specified column.</summary>
		/// <returns>The value of the column.</returns>
		/// <param name="ndx">The specified column. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000128 RID: 296
		public object this[int ndx]
		{
			get
			{
				return this.Row[ndx, this.RowVersionDefault];
			}
			set
			{
				if (!this._dataView.AllowEdit && !this.IsNew)
				{
					throw ExceptionBuilder.CanNotEdit();
				}
				this.SetColumnValue(this._dataView.Table.Columns[ndx], value);
			}
		}

		/// <summary>Gets or sets a value in a specified column.</summary>
		/// <returns>The value of the column.</returns>
		/// <param name="property">String that contains the specified column. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000129 RID: 297
		public object this[string property]
		{
			get
			{
				DataColumn dataColumn = this._dataView.Table.Columns[property];
				if (dataColumn != null)
				{
					return this.Row[dataColumn, this.RowVersionDefault];
				}
				if (this._dataView.Table.DataSet != null && this._dataView.Table.DataSet.Relations.Contains(property))
				{
					return this.CreateChildView(property);
				}
				throw ExceptionBuilder.PropertyNotFound(property, this._dataView.Table.TableName);
			}
			set
			{
				DataColumn dataColumn = this._dataView.Table.Columns[property];
				if (dataColumn == null)
				{
					throw ExceptionBuilder.SetFailed(property);
				}
				if (!this._dataView.AllowEdit && !this.IsNew)
				{
					throw ExceptionBuilder.CanNotEdit();
				}
				this.SetColumnValue(dataColumn, value);
			}
		}

		/// <summary>Gets the error message for the property with the given name.</summary>
		/// <returns>The error message for the property. The default is an empty string ("").</returns>
		/// <param name="colName">The name of the property whose error message to get. </param>
		// Token: 0x1700012A RID: 298
		string IDataErrorInfo.this[string colName]
		{
			get
			{
				return this.Row.GetColumnError(colName);
			}
		}

		/// <summary>Gets a message that describes any validation errors for the object.</summary>
		/// <returns>The validation error on the object.</returns>
		// Token: 0x1700012B RID: 299
		// (get) Token: 0x06000631 RID: 1585 RVA: 0x00017B3B File Offset: 0x00015D3B
		string IDataErrorInfo.Error
		{
			get
			{
				return this.Row.RowError;
			}
		}

		/// <summary>Gets the current version description of the <see cref="T:System.Data.DataRow" />.</summary>
		/// <returns>One of the <see cref="T:System.Data.DataRowVersion" /> values. Possible values for the <see cref="P:System.Data.DataRowView.RowVersion" /> property are Default, Original, Current, and Proposed.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06000632 RID: 1586 RVA: 0x00017B48 File Offset: 0x00015D48
		public DataRowVersion RowVersion
		{
			get
			{
				return this.RowVersionDefault & (DataRowVersion)(-1025);
			}
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x06000633 RID: 1587 RVA: 0x00017B56 File Offset: 0x00015D56
		private DataRowVersion RowVersionDefault
		{
			get
			{
				return this.Row.GetDefaultRowVersion(this._dataView.RowStateFilter);
			}
		}

		// Token: 0x06000634 RID: 1588 RVA: 0x00017B6E File Offset: 0x00015D6E
		internal int GetRecord()
		{
			return this.Row.GetRecordFromVersion(this.RowVersionDefault);
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x00017B81 File Offset: 0x00015D81
		internal bool HasRecord()
		{
			return this.Row.HasVersion(this.RowVersionDefault);
		}

		// Token: 0x06000636 RID: 1590 RVA: 0x00017B94 File Offset: 0x00015D94
		internal object GetColumnValue(DataColumn column)
		{
			return this.Row[column, this.RowVersionDefault];
		}

		// Token: 0x06000637 RID: 1591 RVA: 0x00017BA8 File Offset: 0x00015DA8
		internal void SetColumnValue(DataColumn column, object value)
		{
			if (this._delayBeginEdit)
			{
				this._delayBeginEdit = false;
				this.Row.BeginEdit();
			}
			if (DataRowVersion.Original == this.RowVersionDefault)
			{
				throw ExceptionBuilder.SetFailed(column.ColumnName);
			}
			this.Row[column] = value;
		}

		/// <summary>Returns a <see cref="T:System.Data.DataView" /> for the child <see cref="T:System.Data.DataTable" /> with the specified <see cref="T:System.Data.DataRelation" /> and parent..</summary>
		/// <returns>A <see cref="T:System.Data.DataView" /> for the child <see cref="T:System.Data.DataTable" />.</returns>
		/// <param name="relation">The <see cref="T:System.Data.DataRelation" /> object.</param>
		/// <param name="followParent">The parent object.</param>
		// Token: 0x06000638 RID: 1592 RVA: 0x00017BF8 File Offset: 0x00015DF8
		public DataView CreateChildView(DataRelation relation, bool followParent)
		{
			if (relation == null || relation.ParentKey.Table != this.DataView.Table)
			{
				throw ExceptionBuilder.CreateChildView();
			}
			RelatedView relatedView;
			if (!followParent)
			{
				int record = this.GetRecord();
				object[] keyValues = relation.ParentKey.GetKeyValues(record);
				relatedView = new RelatedView(relation.ChildColumnsReference, keyValues);
			}
			else
			{
				relatedView = new RelatedView(this, relation.ParentKey, relation.ChildColumnsReference);
			}
			relatedView.SetIndex("", DataViewRowState.CurrentRows, null);
			relatedView.SetDataViewManager(this.DataView.DataViewManager);
			return relatedView;
		}

		/// <summary>Returns a <see cref="T:System.Data.DataView" /> for the child <see cref="T:System.Data.DataTable" /> with the specified child <see cref="T:System.Data.DataRelation" />.</summary>
		/// <returns>a <see cref="T:System.Data.DataView" /> for the child <see cref="T:System.Data.DataTable" />.</returns>
		/// <param name="relation">The <see cref="T:System.Data.DataRelation" /> object. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000639 RID: 1593 RVA: 0x00017C85 File Offset: 0x00015E85
		public DataView CreateChildView(DataRelation relation)
		{
			return this.CreateChildView(relation, false);
		}

		/// <summary>Returns a <see cref="T:System.Data.DataView" /> for the child <see cref="T:System.Data.DataTable" /> with the specified <see cref="T:System.Data.DataRelation" /> name and parent.</summary>
		/// <returns>a <see cref="T:System.Data.DataView" /> for the child <see cref="T:System.Data.DataTable" />.</returns>
		/// <param name="relationName">A string containing the <see cref="T:System.Data.DataRelation" /> name.</param>
		/// <param name="followParent">The parent</param>
		// Token: 0x0600063A RID: 1594 RVA: 0x00017C8F File Offset: 0x00015E8F
		public DataView CreateChildView(string relationName, bool followParent)
		{
			return this.CreateChildView(this.DataView.Table.ChildRelations[relationName], followParent);
		}

		/// <summary>Returns a <see cref="T:System.Data.DataView" /> for the child <see cref="T:System.Data.DataTable" /> with the specified child <see cref="T:System.Data.DataRelation" /> name.</summary>
		/// <returns>a <see cref="T:System.Data.DataView" /> for the child <see cref="T:System.Data.DataTable" />.</returns>
		/// <param name="relationName">A string containing the <see cref="T:System.Data.DataRelation" /> name. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600063B RID: 1595 RVA: 0x00017CAE File Offset: 0x00015EAE
		public DataView CreateChildView(string relationName)
		{
			return this.CreateChildView(relationName, false);
		}

		/// <summary>Gets the <see cref="T:System.Data.DataRow" /> being viewed.</summary>
		/// <returns>The <see cref="T:System.Data.DataRow" /> being viewed by the <see cref="T:System.Data.DataRowView" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700012E RID: 302
		// (get) Token: 0x0600063C RID: 1596 RVA: 0x00017CB8 File Offset: 0x00015EB8
		public DataRow Row
		{
			get
			{
				return this._row;
			}
		}

		/// <summary>Begins an edit procedure.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600063D RID: 1597 RVA: 0x00017CC0 File Offset: 0x00015EC0
		public void BeginEdit()
		{
			this._delayBeginEdit = true;
		}

		/// <summary>Cancels an edit procedure.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600063E RID: 1598 RVA: 0x00017CCC File Offset: 0x00015ECC
		public void CancelEdit()
		{
			DataRow row = this.Row;
			if (this.IsNew)
			{
				this._dataView.FinishAddNew(false);
			}
			else
			{
				row.CancelEdit();
			}
			this._delayBeginEdit = false;
		}

		/// <summary>Commits changes to the underlying <see cref="T:System.Data.DataRow" /> and ends the editing session that was begun with <see cref="M:System.Data.DataRowView.BeginEdit" />.  Use <see cref="M:System.Data.DataRowView.CancelEdit" /> to discard the changes made to the <see cref="T:System.Data.DataRow" />.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600063F RID: 1599 RVA: 0x00017D03 File Offset: 0x00015F03
		public void EndEdit()
		{
			if (this.IsNew)
			{
				this._dataView.FinishAddNew(true);
			}
			else
			{
				this.Row.EndEdit();
			}
			this._delayBeginEdit = false;
		}

		/// <summary>Indicates whether a <see cref="T:System.Data.DataRowView" /> is new.</summary>
		/// <returns>true if the row is new; otherwise false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06000640 RID: 1600 RVA: 0x00017D2D File Offset: 0x00015F2D
		public bool IsNew
		{
			get
			{
				return this._row == this._dataView._addNewRow;
			}
		}

		/// <summary>Indicates whether the row is in edit mode.</summary>
		/// <returns>true if the row is in edit mode; otherwise false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000641 RID: 1601 RVA: 0x00017D42 File Offset: 0x00015F42
		public bool IsEdit
		{
			get
			{
				return this.Row.HasVersion(DataRowVersion.Proposed) || this._delayBeginEdit;
			}
		}

		/// <summary>Deletes a row.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000642 RID: 1602 RVA: 0x00017D5E File Offset: 0x00015F5E
		public void Delete()
		{
			this._dataView.Delete(this.Row);
		}

		/// <summary>Event that is raised when a <see cref="T:System.Data.DataRowView" /> property is changed.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1400000A RID: 10
		// (add) Token: 0x06000643 RID: 1603 RVA: 0x00017D74 File Offset: 0x00015F74
		// (remove) Token: 0x06000644 RID: 1604 RVA: 0x00017DAC File Offset: 0x00015FAC
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x06000645 RID: 1605 RVA: 0x00017DE1 File Offset: 0x00015FE1
		internal void RaisePropertyChangedEvent(string propName)
		{
			PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
			if (propertyChanged == null)
			{
				return;
			}
			propertyChanged(this, new PropertyChangedEventArgs(propName));
		}

		/// <summary>Returns a collection of custom attributes for this instance of a component.</summary>
		/// <returns>An AttributeCollection containing the attributes for this object.</returns>
		// Token: 0x06000646 RID: 1606 RVA: 0x00017DFA File Offset: 0x00015FFA
		AttributeCollection ICustomTypeDescriptor.GetAttributes()
		{
			return new AttributeCollection(null);
		}

		/// <summary>Returns the class name of this instance of a component.</summary>
		/// <returns>The class name of this instance of a component.</returns>
		// Token: 0x06000647 RID: 1607 RVA: 0x00003DF6 File Offset: 0x00001FF6
		string ICustomTypeDescriptor.GetClassName()
		{
			return null;
		}

		/// <summary>Returns the name of this instance of a component.</summary>
		/// <returns>The name of this instance of a component.</returns>
		// Token: 0x06000648 RID: 1608 RVA: 0x00003DF6 File Offset: 0x00001FF6
		string ICustomTypeDescriptor.GetComponentName()
		{
			return null;
		}

		/// <summary>Returns a type converter for this instance of a component.</summary>
		/// <returns>The type converter for this instance of a component.</returns>
		// Token: 0x06000649 RID: 1609 RVA: 0x00003DF6 File Offset: 0x00001FF6
		TypeConverter ICustomTypeDescriptor.GetConverter()
		{
			return null;
		}

		/// <summary>Returns the default event for this instance of a component.</summary>
		/// <returns>The default event for this instance of a component.</returns>
		// Token: 0x0600064A RID: 1610 RVA: 0x00003DF6 File Offset: 0x00001FF6
		EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
		{
			return null;
		}

		/// <summary>Returns the default property for this instance of a component.</summary>
		/// <returns>The default property for this instance of a component.</returns>
		// Token: 0x0600064B RID: 1611 RVA: 0x00003DF6 File Offset: 0x00001FF6
		PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
		{
			return null;
		}

		/// <summary>Returns an editor of the specified type for this instance of a component.</summary>
		/// <returns>An <see cref="T:System.Object" /> of the specified type that is the editor for this object, or null if the editor cannot be found.</returns>
		/// <param name="editorBaseType">A <see cref="T:System.Type" /> that represents the editor for this object. </param>
		// Token: 0x0600064C RID: 1612 RVA: 0x00003DF6 File Offset: 0x00001FF6
		object ICustomTypeDescriptor.GetEditor(Type editorBaseType)
		{
			return null;
		}

		/// <summary>Returns the events for this instance of a component.</summary>
		/// <returns>The events for this instance of a component.</returns>
		// Token: 0x0600064D RID: 1613 RVA: 0x00017E02 File Offset: 0x00016002
		EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
		{
			return new EventDescriptorCollection(null);
		}

		/// <summary>Returns the events for this instance of a component with specified attributes.</summary>
		/// <returns>The events for this instance of a component.</returns>
		/// <param name="attributes">The attributes</param>
		// Token: 0x0600064E RID: 1614 RVA: 0x00017E02 File Offset: 0x00016002
		EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] attributes)
		{
			return new EventDescriptorCollection(null);
		}

		/// <summary>Returns the properties for this instance of a component.</summary>
		/// <returns>The properties for this instance of a component.</returns>
		// Token: 0x0600064F RID: 1615 RVA: 0x00017E0A File Offset: 0x0001600A
		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
		{
			return ((ICustomTypeDescriptor)this).GetProperties(null);
		}

		/// <summary>Returns the properties for this instance of a component with specified attributes.</summary>
		/// <returns>The properties for this instance of a component.</returns>
		/// <param name="attributes">The attributes.</param>
		// Token: 0x06000650 RID: 1616 RVA: 0x00017E13 File Offset: 0x00016013
		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(Attribute[] attributes)
		{
			if (this._dataView.Table == null)
			{
				return DataRowView.s_zeroPropertyDescriptorCollection;
			}
			return this._dataView.Table.GetPropertyDescriptorCollection(attributes);
		}

		/// <summary>Returns an object that contains the property described by the specified property descriptor.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the owner of the specified property.</returns>
		/// <param name="pd">A <see cref="T:System.ComponentModel.PropertyDescriptor" /> that represents the property whose owner is to be found. </param>
		// Token: 0x06000651 RID: 1617 RVA: 0x0000565A File Offset: 0x0000385A
		object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd)
		{
			return this;
		}

		// Token: 0x06000653 RID: 1619 RVA: 0x0000E24C File Offset: 0x0000C44C
		internal DataRowView()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000537 RID: 1335
		private readonly DataView _dataView;

		// Token: 0x04000538 RID: 1336
		private readonly DataRow _row;

		// Token: 0x04000539 RID: 1337
		private bool _delayBeginEdit;

		// Token: 0x0400053A RID: 1338
		private static readonly PropertyDescriptorCollection s_zeroPropertyDescriptorCollection = new PropertyDescriptorCollection(null);
	}
}
