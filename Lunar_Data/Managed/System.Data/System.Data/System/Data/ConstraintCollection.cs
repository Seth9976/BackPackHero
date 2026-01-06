using System;
using System.Collections;
using System.ComponentModel;
using System.Data.Common;
using System.Globalization;
using Unity;

namespace System.Data
{
	/// <summary>Represents a collection of constraints for a <see cref="T:System.Data.DataTable" />.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000041 RID: 65
	[DefaultEvent("CollectionChanged")]
	public sealed class ConstraintCollection : InternalDataCollectionBase
	{
		// Token: 0x06000282 RID: 642 RVA: 0x0000D3B3 File Offset: 0x0000B5B3
		internal ConstraintCollection(DataTable table)
		{
			this._list = new ArrayList();
			this._defaultNameIndex = 1;
			base..ctor();
			this._table = table;
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000283 RID: 643 RVA: 0x0000D3D4 File Offset: 0x0000B5D4
		protected override ArrayList List
		{
			get
			{
				return this._list;
			}
		}

		/// <summary>Gets the <see cref="T:System.Data.Constraint" /> from the collection at the specified index.</summary>
		/// <returns>The <see cref="T:System.Data.Constraint" /> at the specified index.</returns>
		/// <param name="index">The index of the constraint to return. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">The index value is greater than the number of items in the collection. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x170000A1 RID: 161
		public Constraint this[int index]
		{
			get
			{
				if (index >= 0 && index < this.List.Count)
				{
					return (Constraint)this.List[index];
				}
				throw ExceptionBuilder.ConstraintOutOfRange(index);
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000285 RID: 645 RVA: 0x0000D408 File Offset: 0x0000B608
		internal DataTable Table
		{
			get
			{
				return this._table;
			}
		}

		/// <summary>Gets the <see cref="T:System.Data.Constraint" /> from the collection with the specified name.</summary>
		/// <returns>The <see cref="T:System.Data.Constraint" /> with the specified name; otherwise a null value if the <see cref="T:System.Data.Constraint" /> does not exist.</returns>
		/// <param name="name">The <see cref="P:System.Data.Constraint.ConstraintName" /> of the constraint to return. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x170000A3 RID: 163
		public Constraint this[string name]
		{
			get
			{
				int num = this.InternalIndexOf(name);
				if (num == -2)
				{
					throw ExceptionBuilder.CaseInsensitiveNameConflict(name);
				}
				if (num >= 0)
				{
					return (Constraint)this.List[num];
				}
				return null;
			}
		}

		/// <summary>Adds the specified <see cref="T:System.Data.Constraint" /> object to the collection.</summary>
		/// <param name="constraint">The Constraint to add. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="constraint" /> argument is null. </exception>
		/// <exception cref="T:System.ArgumentException">The constraint already belongs to this collection, or belongs to another collection. </exception>
		/// <exception cref="T:System.Data.DuplicateNameException">The collection already has a constraint with the same name. (The comparison is not case-sensitive.) </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000287 RID: 647 RVA: 0x0000D448 File Offset: 0x0000B648
		public void Add(Constraint constraint)
		{
			this.Add(constraint, true);
		}

		// Token: 0x06000288 RID: 648 RVA: 0x0000D454 File Offset: 0x0000B654
		internal void Add(Constraint constraint, bool addUniqueWhenAddingForeign)
		{
			if (constraint == null)
			{
				throw ExceptionBuilder.ArgumentNull("constraint");
			}
			if (this.FindConstraint(constraint) != null)
			{
				throw ExceptionBuilder.DuplicateConstraint(this.FindConstraint(constraint).ConstraintName);
			}
			if (1 < this._table.NestedParentRelations.Length && !this.AutoGenerated(constraint))
			{
				throw ExceptionBuilder.CantAddConstraintToMultipleNestedTable(this._table.TableName);
			}
			if (constraint is UniqueConstraint)
			{
				if (((UniqueConstraint)constraint)._bPrimaryKey && this.Table._primaryKey != null)
				{
					throw ExceptionBuilder.AddPrimaryKeyConstraint();
				}
				this.AddUniqueConstraint((UniqueConstraint)constraint);
			}
			else if (constraint is ForeignKeyConstraint)
			{
				ForeignKeyConstraint foreignKeyConstraint = (ForeignKeyConstraint)constraint;
				if (addUniqueWhenAddingForeign && foreignKeyConstraint.RelatedTable.Constraints.FindKeyConstraint(foreignKeyConstraint.RelatedColumnsReference) == null)
				{
					if (constraint.ConstraintName.Length == 0)
					{
						constraint.ConstraintName = this.AssignName();
					}
					else
					{
						this.RegisterName(constraint.ConstraintName);
					}
					UniqueConstraint uniqueConstraint = new UniqueConstraint(foreignKeyConstraint.RelatedColumnsReference);
					foreignKeyConstraint.RelatedTable.Constraints.Add(uniqueConstraint);
				}
				this.AddForeignKeyConstraint((ForeignKeyConstraint)constraint);
			}
			this.BaseAdd(constraint);
			this.ArrayAdd(constraint);
			this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Add, constraint));
			if (constraint is UniqueConstraint && ((UniqueConstraint)constraint)._bPrimaryKey)
			{
				this.Table.PrimaryKey = ((UniqueConstraint)constraint).ColumnsReference;
			}
		}

		/// <summary>Constructs a new <see cref="T:System.Data.UniqueConstraint" /> with the specified name, array of <see cref="T:System.Data.DataColumn" /> objects, and value that indicates whether the column is a primary key, and adds it to the collection.</summary>
		/// <returns>A new UniqueConstraint.</returns>
		/// <param name="name">The name of the <see cref="T:System.Data.UniqueConstraint" />. </param>
		/// <param name="columns">An array of <see cref="T:System.Data.DataColumn" /> objects to which the constraint applies. </param>
		/// <param name="primaryKey">Specifies whether the column should be the primary key. If true, the column will be a primary key column.</param>
		/// <exception cref="T:System.ArgumentException">The constraint already belongs to this collection.-Or- The constraint belongs to another collection. </exception>
		/// <exception cref="T:System.Data.DuplicateNameException">The collection already has a constraint with the specified name. (The comparison is not case-sensitive.) </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000289 RID: 649 RVA: 0x0000D5AC File Offset: 0x0000B7AC
		public Constraint Add(string name, DataColumn[] columns, bool primaryKey)
		{
			UniqueConstraint uniqueConstraint = new UniqueConstraint(name, columns);
			this.Add(uniqueConstraint);
			if (primaryKey)
			{
				this.Table.PrimaryKey = columns;
			}
			return uniqueConstraint;
		}

		/// <summary>Constructs a new <see cref="T:System.Data.UniqueConstraint" /> with the specified name, <see cref="T:System.Data.DataColumn" />, and value that indicates whether the column is a primary key, and adds it to the collection.</summary>
		/// <returns>A new UniqueConstraint.</returns>
		/// <param name="name">The name of the UniqueConstraint. </param>
		/// <param name="column">The <see cref="T:System.Data.DataColumn" /> to which the constraint applies. </param>
		/// <param name="primaryKey">Specifies whether the column should be the primary key. If true, the column will be a primary key column. </param>
		/// <exception cref="T:System.ArgumentException">The constraint already belongs to this collection.-Or- The constraint belongs to another collection. </exception>
		/// <exception cref="T:System.Data.DuplicateNameException">The collection already has a constraint with the specified name. (The comparison is not case-sensitive.) </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600028A RID: 650 RVA: 0x0000D5D8 File Offset: 0x0000B7D8
		public Constraint Add(string name, DataColumn column, bool primaryKey)
		{
			UniqueConstraint uniqueConstraint = new UniqueConstraint(name, column);
			this.Add(uniqueConstraint);
			if (primaryKey)
			{
				this.Table.PrimaryKey = uniqueConstraint.ColumnsReference;
			}
			return uniqueConstraint;
		}

		/// <summary>Constructs a new <see cref="T:System.Data.ForeignKeyConstraint" /> with the specified name, parent column, and child column, and adds the constraint to the collection.</summary>
		/// <returns>A new ForeignKeyConstraint.</returns>
		/// <param name="name">The name of the <see cref="T:System.Data.ForeignKeyConstraint" />. </param>
		/// <param name="primaryKeyColumn">The primary key, or parent, <see cref="T:System.Data.DataColumn" />. </param>
		/// <param name="foreignKeyColumn">The foreign key, or child, <see cref="T:System.Data.DataColumn" />. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600028B RID: 651 RVA: 0x0000D60C File Offset: 0x0000B80C
		public Constraint Add(string name, DataColumn primaryKeyColumn, DataColumn foreignKeyColumn)
		{
			ForeignKeyConstraint foreignKeyConstraint = new ForeignKeyConstraint(name, primaryKeyColumn, foreignKeyColumn);
			this.Add(foreignKeyConstraint);
			return foreignKeyConstraint;
		}

		/// <summary>Constructs a new <see cref="T:System.Data.ForeignKeyConstraint" />, with the specified arrays of parent columns and child columns, and adds the constraint to the collection.</summary>
		/// <returns>A new ForeignKeyConstraint.</returns>
		/// <param name="name">The name of the <see cref="T:System.Data.ForeignKeyConstraint" />. </param>
		/// <param name="primaryKeyColumns">An array of <see cref="T:System.Data.DataColumn" /> objects that are the primary key, or parent, columns. </param>
		/// <param name="foreignKeyColumns">An array of <see cref="T:System.Data.DataColumn" /> objects that are the foreign key, or child, columns. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600028C RID: 652 RVA: 0x0000D62C File Offset: 0x0000B82C
		public Constraint Add(string name, DataColumn[] primaryKeyColumns, DataColumn[] foreignKeyColumns)
		{
			ForeignKeyConstraint foreignKeyConstraint = new ForeignKeyConstraint(name, primaryKeyColumns, foreignKeyColumns);
			this.Add(foreignKeyConstraint);
			return foreignKeyConstraint;
		}

		/// <summary>Copies the elements of the specified <see cref="T:System.Data.ConstraintCollection" /> array to the end of the collection.</summary>
		/// <param name="constraints">An array of <see cref="T:System.Data.ConstraintCollection" /> objects to add to the collection. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600028D RID: 653 RVA: 0x0000D64C File Offset: 0x0000B84C
		public void AddRange(Constraint[] constraints)
		{
			if (this._table.fInitInProgress)
			{
				this._delayLoadingConstraints = constraints;
				this._fLoadForeignKeyConstraintsOnly = false;
				return;
			}
			if (constraints != null)
			{
				foreach (Constraint constraint in constraints)
				{
					if (constraint != null)
					{
						this.Add(constraint);
					}
				}
			}
		}

		// Token: 0x0600028E RID: 654 RVA: 0x0000D698 File Offset: 0x0000B898
		private void AddUniqueConstraint(UniqueConstraint constraint)
		{
			DataColumn[] columnsReference = constraint.ColumnsReference;
			for (int i = 0; i < columnsReference.Length; i++)
			{
				if (columnsReference[i].Table != this._table)
				{
					throw ExceptionBuilder.ConstraintForeignTable();
				}
			}
			constraint.ConstraintIndexInitialize();
			if (!constraint.CanEnableConstraint())
			{
				constraint.ConstraintIndexClear();
				throw ExceptionBuilder.UniqueConstraintViolation();
			}
		}

		// Token: 0x0600028F RID: 655 RVA: 0x0000D6EA File Offset: 0x0000B8EA
		private void AddForeignKeyConstraint(ForeignKeyConstraint constraint)
		{
			if (!constraint.CanEnableConstraint())
			{
				throw ExceptionBuilder.ConstraintParentValues();
			}
			constraint.CheckCanAddToCollection(this);
		}

		// Token: 0x06000290 RID: 656 RVA: 0x0000D704 File Offset: 0x0000B904
		private bool AutoGenerated(Constraint constraint)
		{
			ForeignKeyConstraint foreignKeyConstraint = constraint as ForeignKeyConstraint;
			if (foreignKeyConstraint != null)
			{
				return XmlTreeGen.AutoGenerated(foreignKeyConstraint, false);
			}
			return XmlTreeGen.AutoGenerated((UniqueConstraint)constraint);
		}

		/// <summary>Occurs whenever the <see cref="T:System.Data.ConstraintCollection" /> is changed because of <see cref="T:System.Data.Constraint" /> objects being added or removed.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000291 RID: 657 RVA: 0x0000D72E File Offset: 0x0000B92E
		// (remove) Token: 0x06000292 RID: 658 RVA: 0x0000D747 File Offset: 0x0000B947
		public event CollectionChangeEventHandler CollectionChanged
		{
			add
			{
				this._onCollectionChanged = (CollectionChangeEventHandler)Delegate.Combine(this._onCollectionChanged, value);
			}
			remove
			{
				this._onCollectionChanged = (CollectionChangeEventHandler)Delegate.Remove(this._onCollectionChanged, value);
			}
		}

		// Token: 0x06000293 RID: 659 RVA: 0x0000D760 File Offset: 0x0000B960
		private void ArrayAdd(Constraint constraint)
		{
			this.List.Add(constraint);
		}

		// Token: 0x06000294 RID: 660 RVA: 0x0000D76F File Offset: 0x0000B96F
		private void ArrayRemove(Constraint constraint)
		{
			this.List.Remove(constraint);
		}

		// Token: 0x06000295 RID: 661 RVA: 0x0000D77D File Offset: 0x0000B97D
		internal string AssignName()
		{
			string text = this.MakeName(this._defaultNameIndex);
			this._defaultNameIndex++;
			return text;
		}

		// Token: 0x06000296 RID: 662 RVA: 0x0000D799 File Offset: 0x0000B999
		private void BaseAdd(Constraint constraint)
		{
			if (constraint == null)
			{
				throw ExceptionBuilder.ArgumentNull("constraint");
			}
			if (constraint.ConstraintName.Length == 0)
			{
				constraint.ConstraintName = this.AssignName();
			}
			else
			{
				this.RegisterName(constraint.ConstraintName);
			}
			constraint.InCollection = true;
		}

		// Token: 0x06000297 RID: 663 RVA: 0x0000D7D8 File Offset: 0x0000B9D8
		private void BaseGroupSwitch(Constraint[] oldArray, int oldLength, Constraint[] newArray, int newLength)
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
				if (!flag)
				{
					this.BaseRemove(oldArray[i]);
					this.List.Remove(oldArray[i]);
				}
			}
			for (int k = 0; k < newLength; k++)
			{
				if (!newArray[k].InCollection)
				{
					this.BaseAdd(newArray[k]);
				}
				this.List.Add(newArray[k]);
			}
		}

		// Token: 0x06000298 RID: 664 RVA: 0x0000D868 File Offset: 0x0000BA68
		private void BaseRemove(Constraint constraint)
		{
			if (constraint == null)
			{
				throw ExceptionBuilder.ArgumentNull("constraint");
			}
			if (constraint.Table != this._table)
			{
				throw ExceptionBuilder.ConstraintRemoveFailed();
			}
			this.UnregisterName(constraint.ConstraintName);
			constraint.InCollection = false;
			if (constraint is UniqueConstraint)
			{
				for (int i = 0; i < this.Table.ChildRelations.Count; i++)
				{
					DataRelation dataRelation = this.Table.ChildRelations[i];
					if (dataRelation.ParentKeyConstraint == constraint)
					{
						dataRelation.SetParentKeyConstraint(null);
					}
				}
				((UniqueConstraint)constraint).ConstraintIndexClear();
				return;
			}
			if (constraint is ForeignKeyConstraint)
			{
				for (int j = 0; j < this.Table.ParentRelations.Count; j++)
				{
					DataRelation dataRelation2 = this.Table.ParentRelations[j];
					if (dataRelation2.ChildKeyConstraint == constraint)
					{
						dataRelation2.SetChildKeyConstraint(null);
					}
				}
			}
		}

		/// <summary>Indicates whether a <see cref="T:System.Data.Constraint" /> can be removed.</summary>
		/// <returns>true if the <see cref="T:System.Data.Constraint" /> can be removed from collection; otherwise, false.</returns>
		/// <param name="constraint">The <see cref="T:System.Data.Constraint" /> to be tested for removal from the collection. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000299 RID: 665 RVA: 0x0000D940 File Offset: 0x0000BB40
		public bool CanRemove(Constraint constraint)
		{
			return this.CanRemove(constraint, false);
		}

		// Token: 0x0600029A RID: 666 RVA: 0x0000D94A File Offset: 0x0000BB4A
		internal bool CanRemove(Constraint constraint, bool fThrowException)
		{
			return constraint.CanBeRemovedFromCollection(this, fThrowException);
		}

		/// <summary>Clears the collection of any <see cref="T:System.Data.Constraint" /> objects.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600029B RID: 667 RVA: 0x0000D954 File Offset: 0x0000BB54
		public void Clear()
		{
			if (this._table != null)
			{
				this._table.PrimaryKey = null;
				for (int i = 0; i < this._table.ParentRelations.Count; i++)
				{
					this._table.ParentRelations[i].SetChildKeyConstraint(null);
				}
				for (int j = 0; j < this._table.ChildRelations.Count; j++)
				{
					this._table.ChildRelations[j].SetParentKeyConstraint(null);
				}
			}
			if (this._table.fInitInProgress && this._delayLoadingConstraints != null)
			{
				this._delayLoadingConstraints = null;
				this._fLoadForeignKeyConstraintsOnly = false;
			}
			int count = this.List.Count;
			Constraint[] array = new Constraint[this.List.Count];
			this.List.CopyTo(array, 0);
			try
			{
				this.BaseGroupSwitch(array, count, null, 0);
			}
			catch (Exception ex) when (ADP.IsCatchableOrSecurityExceptionType(ex))
			{
				this.BaseGroupSwitch(null, 0, array, count);
				this.List.Clear();
				for (int k = 0; k < count; k++)
				{
					this.List.Add(array[k]);
				}
				throw;
			}
			this.List.Clear();
			this.OnCollectionChanged(InternalDataCollectionBase.s_refreshEventArgs);
		}

		/// <summary>Indicates whether the <see cref="T:System.Data.Constraint" /> object specified by name exists in the collection.</summary>
		/// <returns>true if the collection contains the specified constraint; otherwise, false.</returns>
		/// <param name="name">The <see cref="P:System.Data.Constraint.ConstraintName" /> of the constraint. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600029C RID: 668 RVA: 0x0000DAA8 File Offset: 0x0000BCA8
		public bool Contains(string name)
		{
			return this.InternalIndexOf(name) >= 0;
		}

		// Token: 0x0600029D RID: 669 RVA: 0x0000DAB8 File Offset: 0x0000BCB8
		internal bool Contains(string name, bool caseSensitive)
		{
			if (!caseSensitive)
			{
				return this.Contains(name);
			}
			int num = this.InternalIndexOf(name);
			return num >= 0 && name == ((Constraint)this.List[num]).ConstraintName;
		}

		/// <summary>Copies the collection objects to a one-dimensional <see cref="T:System.Array" /> instance starting at the specified index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the values copied from the collection.</param>
		/// <param name="index">The index of the array at which to start inserting. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600029E RID: 670 RVA: 0x0000DAFC File Offset: 0x0000BCFC
		public void CopyTo(Constraint[] array, int index)
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
				array[index + i] = (Constraint)this._list[i];
			}
		}

		// Token: 0x0600029F RID: 671 RVA: 0x0000DB6C File Offset: 0x0000BD6C
		internal Constraint FindConstraint(Constraint constraint)
		{
			int count = this.List.Count;
			for (int i = 0; i < count; i++)
			{
				if (((Constraint)this.List[i]).Equals(constraint))
				{
					return (Constraint)this.List[i];
				}
			}
			return null;
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x0000DBC0 File Offset: 0x0000BDC0
		internal UniqueConstraint FindKeyConstraint(DataColumn[] columns)
		{
			int count = this.List.Count;
			for (int i = 0; i < count; i++)
			{
				UniqueConstraint uniqueConstraint = this.List[i] as UniqueConstraint;
				if (uniqueConstraint != null && ConstraintCollection.CompareArrays(uniqueConstraint.Key.ColumnsReference, columns))
				{
					return uniqueConstraint;
				}
			}
			return null;
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x0000DC14 File Offset: 0x0000BE14
		internal UniqueConstraint FindKeyConstraint(DataColumn column)
		{
			int count = this.List.Count;
			for (int i = 0; i < count; i++)
			{
				UniqueConstraint uniqueConstraint = this.List[i] as UniqueConstraint;
				if (uniqueConstraint != null && uniqueConstraint.Key.ColumnsReference.Length == 1 && uniqueConstraint.Key.ColumnsReference[0] == column)
				{
					return uniqueConstraint;
				}
			}
			return null;
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x0000DC78 File Offset: 0x0000BE78
		internal ForeignKeyConstraint FindForeignKeyConstraint(DataColumn[] parentColumns, DataColumn[] childColumns)
		{
			int count = this.List.Count;
			for (int i = 0; i < count; i++)
			{
				ForeignKeyConstraint foreignKeyConstraint = this.List[i] as ForeignKeyConstraint;
				if (foreignKeyConstraint != null && ConstraintCollection.CompareArrays(foreignKeyConstraint.ParentKey.ColumnsReference, parentColumns) && ConstraintCollection.CompareArrays(foreignKeyConstraint.ChildKey.ColumnsReference, childColumns))
				{
					return foreignKeyConstraint;
				}
			}
			return null;
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x0000DCE4 File Offset: 0x0000BEE4
		private static bool CompareArrays(DataColumn[] a1, DataColumn[] a2)
		{
			if (a1.Length != a2.Length)
			{
				return false;
			}
			for (int i = 0; i < a1.Length; i++)
			{
				bool flag = false;
				for (int j = 0; j < a2.Length; j++)
				{
					if (a1[i] == a2[j])
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Gets the index of the specified <see cref="T:System.Data.Constraint" />.</summary>
		/// <returns>The zero-based index of the <see cref="T:System.Data.Constraint" /> if it is in the collection; otherwise, -1.</returns>
		/// <param name="constraint">The <see cref="T:System.Data.Constraint" /> to search for. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060002A4 RID: 676 RVA: 0x0000DD2C File Offset: 0x0000BF2C
		public int IndexOf(Constraint constraint)
		{
			if (constraint != null)
			{
				int count = this.Count;
				for (int i = 0; i < count; i++)
				{
					if (constraint == (Constraint)this.List[i])
					{
						return i;
					}
				}
			}
			return -1;
		}

		/// <summary>Gets the index of the <see cref="T:System.Data.Constraint" /> specified by name.</summary>
		/// <returns>The index of the <see cref="T:System.Data.Constraint" /> if it is in the collection; otherwise, -1.</returns>
		/// <param name="constraintName">The name of the <see cref="T:System.Data.Constraint" />. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060002A5 RID: 677 RVA: 0x0000DD68 File Offset: 0x0000BF68
		public int IndexOf(string constraintName)
		{
			int num = this.InternalIndexOf(constraintName);
			if (num >= 0)
			{
				return num;
			}
			return -1;
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x0000DD84 File Offset: 0x0000BF84
		internal int InternalIndexOf(string constraintName)
		{
			int num = -1;
			if (constraintName != null && 0 < constraintName.Length)
			{
				int count = this.List.Count;
				for (int i = 0; i < count; i++)
				{
					Constraint constraint = (Constraint)this.List[i];
					int num2 = base.NamesEqual(constraint.ConstraintName, constraintName, false, this._table.Locale);
					if (num2 == 1)
					{
						return i;
					}
					if (num2 == -1)
					{
						num = ((num == -1) ? i : (-2));
					}
				}
			}
			return num;
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x0000DDFC File Offset: 0x0000BFFC
		private string MakeName(int index)
		{
			if (1 == index)
			{
				return "Constraint1";
			}
			return "Constraint" + index.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x0000DE1E File Offset: 0x0000C01E
		private void OnCollectionChanged(CollectionChangeEventArgs ccevent)
		{
			CollectionChangeEventHandler onCollectionChanged = this._onCollectionChanged;
			if (onCollectionChanged == null)
			{
				return;
			}
			onCollectionChanged(this, ccevent);
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x0000DE34 File Offset: 0x0000C034
		internal void RegisterName(string name)
		{
			int count = this.List.Count;
			for (int i = 0; i < count; i++)
			{
				if (base.NamesEqual(name, ((Constraint)this.List[i]).ConstraintName, true, this._table.Locale) != 0)
				{
					throw ExceptionBuilder.DuplicateConstraintName(((Constraint)this.List[i]).ConstraintName);
				}
			}
			if (base.NamesEqual(name, this.MakeName(this._defaultNameIndex), true, this._table.Locale) != 0)
			{
				this._defaultNameIndex++;
			}
		}

		/// <summary>Removes the specified <see cref="T:System.Data.Constraint" /> from the collection.</summary>
		/// <param name="constraint">The <see cref="T:System.Data.Constraint" /> to remove. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="constraint" /> argument is null. </exception>
		/// <exception cref="T:System.ArgumentException">The constraint does not belong to the collection. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060002AA RID: 682 RVA: 0x0000DED0 File Offset: 0x0000C0D0
		public void Remove(Constraint constraint)
		{
			if (constraint == null)
			{
				throw ExceptionBuilder.ArgumentNull("constraint");
			}
			if (this.CanRemove(constraint, true))
			{
				this.BaseRemove(constraint);
				this.ArrayRemove(constraint);
				if (constraint is UniqueConstraint && ((UniqueConstraint)constraint).IsPrimaryKey)
				{
					this.Table.PrimaryKey = null;
				}
				this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Remove, constraint));
			}
		}

		/// <summary>Removes the <see cref="T:System.Data.Constraint" /> object at the specified index from the collection.</summary>
		/// <param name="index">The index of the <see cref="T:System.Data.Constraint" /> to remove. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">The collection does not have a constraint at this index. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060002AB RID: 683 RVA: 0x0000DF34 File Offset: 0x0000C134
		public void RemoveAt(int index)
		{
			Constraint constraint = this[index];
			if (constraint == null)
			{
				throw ExceptionBuilder.ConstraintOutOfRange(index);
			}
			this.Remove(constraint);
		}

		/// <summary>Removes the <see cref="T:System.Data.Constraint" /> object specified by name from the collection.</summary>
		/// <param name="name">The name of the <see cref="T:System.Data.Constraint" /> to remove. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060002AC RID: 684 RVA: 0x0000DF5C File Offset: 0x0000C15C
		public void Remove(string name)
		{
			Constraint constraint = this[name];
			if (constraint == null)
			{
				throw ExceptionBuilder.ConstraintNotInTheTable(name);
			}
			this.Remove(constraint);
		}

		// Token: 0x060002AD RID: 685 RVA: 0x0000DF84 File Offset: 0x0000C184
		internal void UnregisterName(string name)
		{
			if (base.NamesEqual(name, this.MakeName(this._defaultNameIndex - 1), true, this._table.Locale) != 0)
			{
				do
				{
					this._defaultNameIndex--;
				}
				while (this._defaultNameIndex > 1 && !this.Contains(this.MakeName(this._defaultNameIndex - 1)));
			}
		}

		// Token: 0x060002AE RID: 686 RVA: 0x0000DFE4 File Offset: 0x0000C1E4
		internal void FinishInitConstraints()
		{
			if (this._delayLoadingConstraints == null)
			{
				return;
			}
			for (int i = 0; i < this._delayLoadingConstraints.Length; i++)
			{
				if (this._delayLoadingConstraints[i] is UniqueConstraint)
				{
					if (!this._fLoadForeignKeyConstraintsOnly)
					{
						UniqueConstraint uniqueConstraint = (UniqueConstraint)this._delayLoadingConstraints[i];
						if (uniqueConstraint._columnNames == null)
						{
							this.Add(uniqueConstraint);
						}
						else
						{
							int num = uniqueConstraint._columnNames.Length;
							DataColumn[] array = new DataColumn[num];
							for (int j = 0; j < num; j++)
							{
								array[j] = this._table.Columns[uniqueConstraint._columnNames[j]];
							}
							if (uniqueConstraint._bPrimaryKey)
							{
								if (this._table._primaryKey != null)
								{
									throw ExceptionBuilder.AddPrimaryKeyConstraint();
								}
								this.Add(uniqueConstraint.ConstraintName, array, true);
							}
							else
							{
								UniqueConstraint uniqueConstraint2 = new UniqueConstraint(uniqueConstraint._constraintName, array);
								if (this.FindConstraint(uniqueConstraint2) == null)
								{
									this.Add(uniqueConstraint2);
								}
							}
						}
					}
				}
				else
				{
					ForeignKeyConstraint foreignKeyConstraint = (ForeignKeyConstraint)this._delayLoadingConstraints[i];
					if (foreignKeyConstraint._parentColumnNames == null || foreignKeyConstraint._childColumnNames == null)
					{
						this.Add(foreignKeyConstraint);
					}
					else if (this._table.DataSet == null)
					{
						this._fLoadForeignKeyConstraintsOnly = true;
					}
					else
					{
						int num = foreignKeyConstraint._parentColumnNames.Length;
						DataColumn[] array = new DataColumn[num];
						DataColumn[] array2 = new DataColumn[num];
						for (int k = 0; k < num; k++)
						{
							if (foreignKeyConstraint._parentTableNamespace == null)
							{
								array[k] = this._table.DataSet.Tables[foreignKeyConstraint._parentTableName].Columns[foreignKeyConstraint._parentColumnNames[k]];
							}
							else
							{
								array[k] = this._table.DataSet.Tables[foreignKeyConstraint._parentTableName, foreignKeyConstraint._parentTableNamespace].Columns[foreignKeyConstraint._parentColumnNames[k]];
							}
							array2[k] = this._table.Columns[foreignKeyConstraint._childColumnNames[k]];
						}
						this.Add(new ForeignKeyConstraint(foreignKeyConstraint._constraintName, array, array2)
						{
							AcceptRejectRule = foreignKeyConstraint._acceptRejectRule,
							DeleteRule = foreignKeyConstraint._deleteRule,
							UpdateRule = foreignKeyConstraint._updateRule
						});
					}
				}
			}
			if (!this._fLoadForeignKeyConstraintsOnly)
			{
				this._delayLoadingConstraints = null;
			}
		}

		// Token: 0x060002AF RID: 687 RVA: 0x0000E24C File Offset: 0x0000C44C
		internal ConstraintCollection()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x0400049C RID: 1180
		private readonly DataTable _table;

		// Token: 0x0400049D RID: 1181
		private readonly ArrayList _list;

		// Token: 0x0400049E RID: 1182
		private int _defaultNameIndex;

		// Token: 0x0400049F RID: 1183
		private CollectionChangeEventHandler _onCollectionChanged;

		// Token: 0x040004A0 RID: 1184
		private Constraint[] _delayLoadingConstraints;

		// Token: 0x040004A1 RID: 1185
		private bool _fLoadForeignKeyConstraintsOnly;
	}
}
