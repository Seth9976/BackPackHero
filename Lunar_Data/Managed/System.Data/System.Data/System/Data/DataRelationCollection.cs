using System;
using System.Collections;
using System.ComponentModel;
using System.Data.Common;
using System.Globalization;
using System.Threading;

namespace System.Data
{
	/// <summary>Represents the collection of <see cref="T:System.Data.DataRelation" /> objects for this <see cref="T:System.Data.DataSet" />.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000060 RID: 96
	[DefaultEvent("CollectionChanged")]
	[DefaultProperty("Table")]
	public abstract class DataRelationCollection : InternalDataCollectionBase
	{
		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000556 RID: 1366 RVA: 0x00014D55 File Offset: 0x00012F55
		internal int ObjectID
		{
			get
			{
				return this._objectID;
			}
		}

		/// <summary>Gets the <see cref="T:System.Data.DataRelation" /> object at the specified index.</summary>
		/// <returns>The <see cref="T:System.Data.DataRelation" />, or a null value if the specified <see cref="T:System.Data.DataRelation" /> does not exist.</returns>
		/// <param name="index">The zero-based index to find. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">The index value is greater than the number of items in the collection. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000106 RID: 262
		public abstract DataRelation this[int index] { get; }

		/// <summary>Gets the <see cref="T:System.Data.DataRelation" /> object specified by name.</summary>
		/// <returns>The named <see cref="T:System.Data.DataRelation" />, or a null value if the specified <see cref="T:System.Data.DataRelation" /> does not exist.</returns>
		/// <param name="name">The name of the relation to find. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000107 RID: 263
		public abstract DataRelation this[string name] { get; }

		/// <summary>Adds a <see cref="T:System.Data.DataRelation" /> to the <see cref="T:System.Data.DataRelationCollection" />.</summary>
		/// <param name="relation">The DataRelation to add to the collection. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="relation" /> parameter is a null value. </exception>
		/// <exception cref="T:System.ArgumentException">The relation already belongs to this collection, or it belongs to another collection. </exception>
		/// <exception cref="T:System.Data.DuplicateNameException">The collection already has a relation with the specified name. (The comparison is not case sensitive.) </exception>
		/// <exception cref="T:System.Data.InvalidConstraintException">The relation has entered an invalid state since it was created. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000559 RID: 1369 RVA: 0x00014D60 File Offset: 0x00012F60
		public void Add(DataRelation relation)
		{
			long num = DataCommonEventSource.Log.EnterScope<int, int>("<ds.DataRelationCollection.Add|API> {0}, relation={1}", this.ObjectID, (relation != null) ? relation.ObjectID : 0);
			try
			{
				if (this._inTransition != relation)
				{
					this._inTransition = relation;
					try
					{
						this.OnCollectionChanging(new CollectionChangeEventArgs(CollectionChangeAction.Add, relation));
						this.AddCore(relation);
						this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Add, relation));
					}
					finally
					{
						this._inTransition = null;
					}
				}
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
		}

		/// <summary>Copies the elements of the specified <see cref="T:System.Data.DataRelation" /> array to the end of the collection.</summary>
		/// <param name="relations">The array of <see cref="T:System.Data.DataRelation" /> objects to add to the collection. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600055A RID: 1370 RVA: 0x00014DF4 File Offset: 0x00012FF4
		public virtual void AddRange(DataRelation[] relations)
		{
			if (relations != null)
			{
				foreach (DataRelation dataRelation in relations)
				{
					if (dataRelation != null)
					{
						this.Add(dataRelation);
					}
				}
			}
		}

		/// <summary>Creates a <see cref="T:System.Data.DataRelation" /> with the specified name and arrays of parent and child columns, and adds it to the collection.</summary>
		/// <returns>The created DataRelation.</returns>
		/// <param name="name">The name of the DataRelation to create. </param>
		/// <param name="parentColumns">An array of parent <see cref="T:System.Data.DataColumn" /> objects. </param>
		/// <param name="childColumns">An array of child DataColumn objects. </param>
		/// <exception cref="T:System.ArgumentNullException">The relation name is a null value. </exception>
		/// <exception cref="T:System.ArgumentException">The relation already belongs to this collection, or it belongs to another collection. </exception>
		/// <exception cref="T:System.Data.DuplicateNameException">The collection already has a relation with the same name. (The comparison is not case sensitive.) </exception>
		/// <exception cref="T:System.Data.InvalidConstraintException">The relation has entered an invalid state since it was created. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600055B RID: 1371 RVA: 0x00014E24 File Offset: 0x00013024
		public virtual DataRelation Add(string name, DataColumn[] parentColumns, DataColumn[] childColumns)
		{
			DataRelation dataRelation = new DataRelation(name, parentColumns, childColumns);
			this.Add(dataRelation);
			return dataRelation;
		}

		/// <summary>Creates a <see cref="T:System.Data.DataRelation" /> with the specified name, arrays of parent and child columns, and value specifying whether to create a constraint, and adds it to the collection.</summary>
		/// <returns>The created relation.</returns>
		/// <param name="name">The name of the DataRelation to create. </param>
		/// <param name="parentColumns">An array of parent <see cref="T:System.Data.DataColumn" /> objects. </param>
		/// <param name="childColumns">An array of child DataColumn objects. </param>
		/// <param name="createConstraints">true to create a constraint; otherwise false. </param>
		/// <exception cref="T:System.ArgumentNullException">The relation name is a null value. </exception>
		/// <exception cref="T:System.ArgumentException">The relation already belongs to this collection, or it belongs to another collection. </exception>
		/// <exception cref="T:System.Data.DuplicateNameException">The collection already has a relation with the same name. (The comparison is not case sensitive.) </exception>
		/// <exception cref="T:System.Data.InvalidConstraintException">The relation has entered an invalid state since it was created. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600055C RID: 1372 RVA: 0x00014E44 File Offset: 0x00013044
		public virtual DataRelation Add(string name, DataColumn[] parentColumns, DataColumn[] childColumns, bool createConstraints)
		{
			DataRelation dataRelation = new DataRelation(name, parentColumns, childColumns, createConstraints);
			this.Add(dataRelation);
			return dataRelation;
		}

		/// <summary>Creates a <see cref="T:System.Data.DataRelation" /> with the specified parent and child columns, and adds it to the collection.</summary>
		/// <returns>The created relation.</returns>
		/// <param name="parentColumns">The parent columns of the relation. </param>
		/// <param name="childColumns">The child columns of the relation. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="relation" /> argument is a null value. </exception>
		/// <exception cref="T:System.ArgumentException">The relation already belongs to this collection, or it belongs to another collection. </exception>
		/// <exception cref="T:System.Data.DuplicateNameException">The collection already has a relation with the same name. (The comparison is not case sensitive.) </exception>
		/// <exception cref="T:System.Data.InvalidConstraintException">The relation has entered an invalid state since it was created. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600055D RID: 1373 RVA: 0x00014E64 File Offset: 0x00013064
		public virtual DataRelation Add(DataColumn[] parentColumns, DataColumn[] childColumns)
		{
			DataRelation dataRelation = new DataRelation(null, parentColumns, childColumns);
			this.Add(dataRelation);
			return dataRelation;
		}

		/// <summary>Creates a <see cref="T:System.Data.DataRelation" /> with the specified name, and parent and child columns, and adds it to the collection.</summary>
		/// <returns>The created relation.</returns>
		/// <param name="name">The name of the relation. </param>
		/// <param name="parentColumn">The parent column of the relation. </param>
		/// <param name="childColumn">The child column of the relation. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600055E RID: 1374 RVA: 0x00014E84 File Offset: 0x00013084
		public virtual DataRelation Add(string name, DataColumn parentColumn, DataColumn childColumn)
		{
			DataRelation dataRelation = new DataRelation(name, parentColumn, childColumn);
			this.Add(dataRelation);
			return dataRelation;
		}

		/// <summary>Creates a <see cref="T:System.Data.DataRelation" /> with the specified name, parent and child columns, with optional constraints according to the value of the <paramref name="createConstraints" /> parameter, and adds it to the collection.</summary>
		/// <returns>The created relation.</returns>
		/// <param name="name">The name of the relation. </param>
		/// <param name="parentColumn">The parent column of the relation. </param>
		/// <param name="childColumn">The child column of the relation. </param>
		/// <param name="createConstraints">true to create constraints; otherwise false. (The default is true). </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600055F RID: 1375 RVA: 0x00014EA4 File Offset: 0x000130A4
		public virtual DataRelation Add(string name, DataColumn parentColumn, DataColumn childColumn, bool createConstraints)
		{
			DataRelation dataRelation = new DataRelation(name, parentColumn, childColumn, createConstraints);
			this.Add(dataRelation);
			return dataRelation;
		}

		/// <summary>Creates a <see cref="T:System.Data.DataRelation" /> with a specified parent and child column, and adds it to the collection.</summary>
		/// <returns>The created relation.</returns>
		/// <param name="parentColumn">The parent column of the relation. </param>
		/// <param name="childColumn">The child column of the relation. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000560 RID: 1376 RVA: 0x00014EC4 File Offset: 0x000130C4
		public virtual DataRelation Add(DataColumn parentColumn, DataColumn childColumn)
		{
			DataRelation dataRelation = new DataRelation(null, parentColumn, childColumn);
			this.Add(dataRelation);
			return dataRelation;
		}

		/// <summary>Performs verification on the table.</summary>
		/// <param name="relation">The relation to check.</param>
		/// <exception cref="T:System.ArgumentNullException">The relation is null. </exception>
		/// <exception cref="T:System.ArgumentException">The relation already belongs to this collection, or it belongs to another collection. </exception>
		/// <exception cref="T:System.Data.DuplicateNameException">The collection already has a relation with the same name. (The comparison is not case sensitive.) </exception>
		// Token: 0x06000561 RID: 1377 RVA: 0x00014EE4 File Offset: 0x000130E4
		protected virtual void AddCore(DataRelation relation)
		{
			DataCommonEventSource.Log.Trace<int, int>("<ds.DataRelationCollection.AddCore|INFO> {0}, relation={1}", this.ObjectID, (relation != null) ? relation.ObjectID : 0);
			if (relation == null)
			{
				throw ExceptionBuilder.ArgumentNull("relation");
			}
			relation.CheckState();
			DataSet dataSet = this.GetDataSet();
			if (relation.DataSet == dataSet)
			{
				throw ExceptionBuilder.RelationAlreadyInTheDataSet();
			}
			if (relation.DataSet != null)
			{
				throw ExceptionBuilder.RelationAlreadyInOtherDataSet();
			}
			if (relation.ChildTable.Locale.LCID != relation.ParentTable.Locale.LCID || relation.ChildTable.CaseSensitive != relation.ParentTable.CaseSensitive)
			{
				throw ExceptionBuilder.CaseLocaleMismatch();
			}
			if (relation.Nested)
			{
				relation.CheckNamespaceValidityForNestedRelations(relation.ParentTable.Namespace);
				relation.ValidateMultipleNestedRelations();
				DataTable parentTable = relation.ParentTable;
				int elementColumnCount = parentTable.ElementColumnCount;
				parentTable.ElementColumnCount = elementColumnCount + 1;
			}
		}

		/// <summary>Occurs when the collection has changed.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x14000007 RID: 7
		// (add) Token: 0x06000562 RID: 1378 RVA: 0x00014FBE File Offset: 0x000131BE
		// (remove) Token: 0x06000563 RID: 1379 RVA: 0x00014FEC File Offset: 0x000131EC
		public event CollectionChangeEventHandler CollectionChanged
		{
			add
			{
				DataCommonEventSource.Log.Trace<int>("<ds.DataRelationCollection.add_CollectionChanged|API> {0}", this.ObjectID);
				this._onCollectionChangedDelegate = (CollectionChangeEventHandler)Delegate.Combine(this._onCollectionChangedDelegate, value);
			}
			remove
			{
				DataCommonEventSource.Log.Trace<int>("<ds.DataRelationCollection.remove_CollectionChanged|API> {0}", this.ObjectID);
				this._onCollectionChangedDelegate = (CollectionChangeEventHandler)Delegate.Remove(this._onCollectionChangedDelegate, value);
			}
		}

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x06000564 RID: 1380 RVA: 0x0001501A File Offset: 0x0001321A
		// (remove) Token: 0x06000565 RID: 1381 RVA: 0x00015048 File Offset: 0x00013248
		internal event CollectionChangeEventHandler CollectionChanging
		{
			add
			{
				DataCommonEventSource.Log.Trace<int>("<ds.DataRelationCollection.add_CollectionChanging|INFO> {0}", this.ObjectID);
				this._onCollectionChangingDelegate = (CollectionChangeEventHandler)Delegate.Combine(this._onCollectionChangingDelegate, value);
			}
			remove
			{
				DataCommonEventSource.Log.Trace<int>("<ds.DataRelationCollection.remove_CollectionChanging|INFO> {0}", this.ObjectID);
				this._onCollectionChangingDelegate = (CollectionChangeEventHandler)Delegate.Remove(this._onCollectionChangingDelegate, value);
			}
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x00015076 File Offset: 0x00013276
		internal string AssignName()
		{
			string text = this.MakeName(this._defaultNameIndex);
			this._defaultNameIndex++;
			return text;
		}

		/// <summary>Clears the collection of any relations.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000567 RID: 1383 RVA: 0x00015094 File Offset: 0x00013294
		public virtual void Clear()
		{
			long num = DataCommonEventSource.Log.EnterScope<int>("<ds.DataRelationCollection.Clear|API> {0}", this.ObjectID);
			try
			{
				int count = this.Count;
				this.OnCollectionChanging(InternalDataCollectionBase.s_refreshEventArgs);
				for (int i = count - 1; i >= 0; i--)
				{
					this._inTransition = this[i];
					this.RemoveCore(this._inTransition);
				}
				this.OnCollectionChanged(InternalDataCollectionBase.s_refreshEventArgs);
				this._inTransition = null;
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
		}

		/// <summary>Verifies whether a <see cref="T:System.Data.DataRelation" /> with the specific name (case insensitive) exists in the collection.</summary>
		/// <returns>true, if a relation with the specified name exists; otherwise false.</returns>
		/// <param name="name">The name of the relation to find. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000568 RID: 1384 RVA: 0x00015120 File Offset: 0x00013320
		public virtual bool Contains(string name)
		{
			return this.InternalIndexOf(name) >= 0;
		}

		/// <summary>Copies the collection of <see cref="T:System.Data.DataRelation" /> objects starting at the specified index.</summary>
		/// <param name="array">The array of <see cref="T:System.Data.DataRelation" /> objects to copy the collection to.</param>
		/// <param name="index">The index to start from.</param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000569 RID: 1385 RVA: 0x00015130 File Offset: 0x00013330
		public void CopyTo(DataRelation[] array, int index)
		{
			if (array == null)
			{
				throw ExceptionBuilder.ArgumentNull("array");
			}
			if (index < 0)
			{
				throw ExceptionBuilder.ArgumentOutOfRange("index");
			}
			ArrayList list = this.List;
			if (array.Length - index < list.Count)
			{
				throw ExceptionBuilder.InvalidOffsetLength();
			}
			for (int i = 0; i < list.Count; i++)
			{
				array[index + i] = (DataRelation)list[i];
			}
		}

		/// <summary>Gets the index of the specified <see cref="T:System.Data.DataRelation" /> object.</summary>
		/// <returns>The 0-based index of the relation, or -1 if the relation is not found in the collection.</returns>
		/// <param name="relation">The relation to search for. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600056A RID: 1386 RVA: 0x00015198 File Offset: 0x00013398
		public virtual int IndexOf(DataRelation relation)
		{
			int count = this.List.Count;
			for (int i = 0; i < count; i++)
			{
				if (relation == (DataRelation)this.List[i])
				{
					return i;
				}
			}
			return -1;
		}

		/// <summary>Gets the index of the <see cref="T:System.Data.DataRelation" /> specified by name.</summary>
		/// <returns>The zero-based index of the relation with the specified name, or -1 if the relation does not exist in the collection.</returns>
		/// <param name="relationName">The name of the relation to find. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600056B RID: 1387 RVA: 0x000151D4 File Offset: 0x000133D4
		public virtual int IndexOf(string relationName)
		{
			int num = this.InternalIndexOf(relationName);
			if (num >= 0)
			{
				return num;
			}
			return -1;
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x000151F0 File Offset: 0x000133F0
		internal int InternalIndexOf(string name)
		{
			int num = -1;
			if (name != null && 0 < name.Length)
			{
				int count = this.List.Count;
				for (int i = 0; i < count; i++)
				{
					DataRelation dataRelation = (DataRelation)this.List[i];
					int num2 = base.NamesEqual(dataRelation.RelationName, name, false, this.GetDataSet().Locale);
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

		/// <summary>This method supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <returns>The referenced DataSet.</returns>
		// Token: 0x0600056D RID: 1389
		protected abstract DataSet GetDataSet();

		// Token: 0x0600056E RID: 1390 RVA: 0x00015268 File Offset: 0x00013468
		private string MakeName(int index)
		{
			if (index != 1)
			{
				return "Relation" + index.ToString(CultureInfo.InvariantCulture);
			}
			return "Relation1";
		}

		/// <summary>Raises the <see cref="E:System.Data.DataRelationCollection.CollectionChanged" /> event.</summary>
		/// <param name="ccevent">A <see cref="T:System.ComponentModel.CollectionChangeEventArgs" /> that contains the event data. </param>
		// Token: 0x0600056F RID: 1391 RVA: 0x0001528A File Offset: 0x0001348A
		protected virtual void OnCollectionChanged(CollectionChangeEventArgs ccevent)
		{
			if (this._onCollectionChangedDelegate != null)
			{
				DataCommonEventSource.Log.Trace<int>("<ds.DataRelationCollection.OnCollectionChanged|INFO> {0}", this.ObjectID);
				this._onCollectionChangedDelegate(this, ccevent);
			}
		}

		/// <summary>Raises the <see cref="E:System.Data.DataRelationCollection.CollectionChanged" /> event.</summary>
		/// <param name="ccevent">A <see cref="T:System.ComponentModel.CollectionChangeEventArgs" /> that contains the event data. </param>
		// Token: 0x06000570 RID: 1392 RVA: 0x000152B6 File Offset: 0x000134B6
		protected virtual void OnCollectionChanging(CollectionChangeEventArgs ccevent)
		{
			if (this._onCollectionChangingDelegate != null)
			{
				DataCommonEventSource.Log.Trace<int>("<ds.DataRelationCollection.OnCollectionChanging|INFO> {0}", this.ObjectID);
				this._onCollectionChangingDelegate(this, ccevent);
			}
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x000152E4 File Offset: 0x000134E4
		internal void RegisterName(string name)
		{
			DataCommonEventSource.Log.Trace<int, string>("<ds.DataRelationCollection.RegisterName|INFO> {0}, name='{1}'", this.ObjectID, name);
			CultureInfo locale = this.GetDataSet().Locale;
			int count = this.Count;
			for (int i = 0; i < count; i++)
			{
				if (base.NamesEqual(name, this[i].RelationName, true, locale) != 0)
				{
					throw ExceptionBuilder.DuplicateRelation(this[i].RelationName);
				}
			}
			if (base.NamesEqual(name, this.MakeName(this._defaultNameIndex), true, locale) != 0)
			{
				this._defaultNameIndex++;
			}
		}

		/// <summary>Verifies whether the specified <see cref="T:System.Data.DataRelation" /> can be removed from the collection.</summary>
		/// <returns>true if the <see cref="T:System.Data.DataRelation" /> can be removed; otherwise, false.</returns>
		/// <param name="relation">The relation to perform the check against. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000572 RID: 1394 RVA: 0x00015374 File Offset: 0x00013574
		public virtual bool CanRemove(DataRelation relation)
		{
			return relation != null && relation.DataSet == this.GetDataSet();
		}

		/// <summary>Removes the specified relation from the collection.</summary>
		/// <param name="relation">The relation to remove. </param>
		/// <exception cref="T:System.ArgumentNullException">The relation is a null value.</exception>
		/// <exception cref="T:System.ArgumentException">The relation does not belong to the collection.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000573 RID: 1395 RVA: 0x0001538C File Offset: 0x0001358C
		public void Remove(DataRelation relation)
		{
			DataCommonEventSource.Log.Trace<int, int>("<ds.DataRelationCollection.Remove|API> {0}, relation={1}", this.ObjectID, (relation != null) ? relation.ObjectID : 0);
			if (this._inTransition == relation)
			{
				return;
			}
			this._inTransition = relation;
			try
			{
				this.OnCollectionChanging(new CollectionChangeEventArgs(CollectionChangeAction.Remove, relation));
				this.RemoveCore(relation);
				this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Remove, relation));
			}
			finally
			{
				this._inTransition = null;
			}
		}

		/// <summary>Removes the relation at the specified index from the collection.</summary>
		/// <param name="index">The index of the relation to remove. </param>
		/// <exception cref="T:System.ArgumentException">The collection does not have a relation at the specified index. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000574 RID: 1396 RVA: 0x00015408 File Offset: 0x00013608
		public void RemoveAt(int index)
		{
			DataRelation dataRelation = this[index];
			if (dataRelation == null)
			{
				throw ExceptionBuilder.RelationOutOfRange(index);
			}
			this.Remove(dataRelation);
		}

		/// <summary>Removes the relation with the specified name from the collection.</summary>
		/// <param name="name">The name of the relation to remove. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">The collection does not have a relation with the specified name.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000575 RID: 1397 RVA: 0x00015434 File Offset: 0x00013634
		public void Remove(string name)
		{
			DataRelation dataRelation = this[name];
			if (dataRelation == null)
			{
				throw ExceptionBuilder.RelationNotInTheDataSet(name);
			}
			this.Remove(dataRelation);
		}

		/// <summary>Performs a verification on the specified <see cref="T:System.Data.DataRelation" /> object.</summary>
		/// <param name="relation">The DataRelation object to verify. </param>
		/// <exception cref="T:System.ArgumentNullException">The collection does not have a relation at the specified index. </exception>
		/// <exception cref="T:System.ArgumentException">The specified relation does not belong to this collection, or it belongs to another collection. </exception>
		// Token: 0x06000576 RID: 1398 RVA: 0x0001545C File Offset: 0x0001365C
		protected virtual void RemoveCore(DataRelation relation)
		{
			DataCommonEventSource.Log.Trace<int, int>("<ds.DataRelationCollection.RemoveCore|INFO> {0}, relation={1}", this.ObjectID, (relation != null) ? relation.ObjectID : 0);
			if (relation == null)
			{
				throw ExceptionBuilder.ArgumentNull("relation");
			}
			DataSet dataSet = this.GetDataSet();
			if (relation.DataSet != dataSet)
			{
				throw ExceptionBuilder.RelationNotInTheDataSet(relation.RelationName);
			}
			if (relation.Nested)
			{
				DataTable parentTable = relation.ParentTable;
				int elementColumnCount = parentTable.ElementColumnCount;
				parentTable.ElementColumnCount = elementColumnCount - 1;
				relation.ParentTable.Columns.UnregisterName(relation.ChildTable.TableName);
			}
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x000154EC File Offset: 0x000136EC
		internal void UnregisterName(string name)
		{
			DataCommonEventSource.Log.Trace<int, string>("<ds.DataRelationCollection.UnregisterName|INFO> {0}, name='{1}'", this.ObjectID, name);
			if (base.NamesEqual(name, this.MakeName(this._defaultNameIndex - 1), true, this.GetDataSet().Locale) != 0)
			{
				do
				{
					this._defaultNameIndex--;
				}
				while (this._defaultNameIndex > 1 && !this.Contains(this.MakeName(this._defaultNameIndex - 1)));
			}
		}

		// Token: 0x040004FD RID: 1277
		private DataRelation _inTransition;

		// Token: 0x040004FE RID: 1278
		private int _defaultNameIndex = 1;

		// Token: 0x040004FF RID: 1279
		private CollectionChangeEventHandler _onCollectionChangedDelegate;

		// Token: 0x04000500 RID: 1280
		private CollectionChangeEventHandler _onCollectionChangingDelegate;

		// Token: 0x04000501 RID: 1281
		private static int s_objectTypeCount;

		// Token: 0x04000502 RID: 1282
		private readonly int _objectID = Interlocked.Increment(ref DataRelationCollection.s_objectTypeCount);

		// Token: 0x02000061 RID: 97
		internal sealed class DataTableRelationCollection : DataRelationCollection
		{
			// Token: 0x06000579 RID: 1401 RVA: 0x0001557E File Offset: 0x0001377E
			internal DataTableRelationCollection(DataTable table, bool fParentCollection)
			{
				if (table == null)
				{
					throw ExceptionBuilder.RelationTableNull();
				}
				this._table = table;
				this._fParentCollection = fParentCollection;
				this._relations = new ArrayList();
			}

			// Token: 0x17000108 RID: 264
			// (get) Token: 0x0600057A RID: 1402 RVA: 0x000155A8 File Offset: 0x000137A8
			protected override ArrayList List
			{
				get
				{
					return this._relations;
				}
			}

			// Token: 0x0600057B RID: 1403 RVA: 0x000155B0 File Offset: 0x000137B0
			private void EnsureDataSet()
			{
				if (this._table.DataSet == null)
				{
					throw ExceptionBuilder.RelationTableWasRemoved();
				}
			}

			// Token: 0x0600057C RID: 1404 RVA: 0x000155C5 File Offset: 0x000137C5
			protected override DataSet GetDataSet()
			{
				this.EnsureDataSet();
				return this._table.DataSet;
			}

			// Token: 0x17000109 RID: 265
			public override DataRelation this[int index]
			{
				get
				{
					if (index >= 0 && index < this._relations.Count)
					{
						return (DataRelation)this._relations[index];
					}
					throw ExceptionBuilder.RelationOutOfRange(index);
				}
			}

			// Token: 0x1700010A RID: 266
			public override DataRelation this[string name]
			{
				get
				{
					int num = base.InternalIndexOf(name);
					if (num == -2)
					{
						throw ExceptionBuilder.CaseInsensitiveNameConflict(name);
					}
					if (num >= 0)
					{
						return (DataRelation)this.List[num];
					}
					return null;
				}
			}

			// Token: 0x14000009 RID: 9
			// (add) Token: 0x0600057F RID: 1407 RVA: 0x00015644 File Offset: 0x00013844
			// (remove) Token: 0x06000580 RID: 1408 RVA: 0x0001567C File Offset: 0x0001387C
			internal event CollectionChangeEventHandler RelationPropertyChanged;

			// Token: 0x06000581 RID: 1409 RVA: 0x000156B1 File Offset: 0x000138B1
			internal void OnRelationPropertyChanged(CollectionChangeEventArgs ccevent)
			{
				if (!this._fParentCollection)
				{
					this._table.UpdatePropertyDescriptorCollectionCache();
				}
				CollectionChangeEventHandler relationPropertyChanged = this.RelationPropertyChanged;
				if (relationPropertyChanged == null)
				{
					return;
				}
				relationPropertyChanged(this, ccevent);
			}

			// Token: 0x06000582 RID: 1410 RVA: 0x000156D8 File Offset: 0x000138D8
			private void AddCache(DataRelation relation)
			{
				this._relations.Add(relation);
				if (!this._fParentCollection)
				{
					this._table.UpdatePropertyDescriptorCollectionCache();
				}
			}

			// Token: 0x06000583 RID: 1411 RVA: 0x000156FC File Offset: 0x000138FC
			protected override void AddCore(DataRelation relation)
			{
				if (this._fParentCollection)
				{
					if (relation.ChildTable != this._table)
					{
						throw ExceptionBuilder.ChildTableMismatch();
					}
				}
				else if (relation.ParentTable != this._table)
				{
					throw ExceptionBuilder.ParentTableMismatch();
				}
				this.GetDataSet().Relations.Add(relation);
				this.AddCache(relation);
			}

			// Token: 0x06000584 RID: 1412 RVA: 0x00015751 File Offset: 0x00013951
			public override bool CanRemove(DataRelation relation)
			{
				if (!base.CanRemove(relation))
				{
					return false;
				}
				if (this._fParentCollection)
				{
					if (relation.ChildTable != this._table)
					{
						return false;
					}
				}
				else if (relation.ParentTable != this._table)
				{
					return false;
				}
				return true;
			}

			// Token: 0x06000585 RID: 1413 RVA: 0x00015788 File Offset: 0x00013988
			private void RemoveCache(DataRelation relation)
			{
				for (int i = 0; i < this._relations.Count; i++)
				{
					if (relation == this._relations[i])
					{
						this._relations.RemoveAt(i);
						if (!this._fParentCollection)
						{
							this._table.UpdatePropertyDescriptorCollectionCache();
						}
						return;
					}
				}
				throw ExceptionBuilder.RelationDoesNotExist();
			}

			// Token: 0x06000586 RID: 1414 RVA: 0x000157E0 File Offset: 0x000139E0
			protected override void RemoveCore(DataRelation relation)
			{
				if (this._fParentCollection)
				{
					if (relation.ChildTable != this._table)
					{
						throw ExceptionBuilder.ChildTableMismatch();
					}
				}
				else if (relation.ParentTable != this._table)
				{
					throw ExceptionBuilder.ParentTableMismatch();
				}
				this.GetDataSet().Relations.Remove(relation);
				this.RemoveCache(relation);
			}

			// Token: 0x04000503 RID: 1283
			private readonly DataTable _table;

			// Token: 0x04000504 RID: 1284
			private readonly ArrayList _relations;

			// Token: 0x04000505 RID: 1285
			private readonly bool _fParentCollection;
		}

		// Token: 0x02000062 RID: 98
		internal sealed class DataSetRelationCollection : DataRelationCollection
		{
			// Token: 0x06000587 RID: 1415 RVA: 0x00015835 File Offset: 0x00013A35
			internal DataSetRelationCollection(DataSet dataSet)
			{
				if (dataSet == null)
				{
					throw ExceptionBuilder.RelationDataSetNull();
				}
				this._dataSet = dataSet;
				this._relations = new ArrayList();
			}

			// Token: 0x1700010B RID: 267
			// (get) Token: 0x06000588 RID: 1416 RVA: 0x00015858 File Offset: 0x00013A58
			protected override ArrayList List
			{
				get
				{
					return this._relations;
				}
			}

			// Token: 0x06000589 RID: 1417 RVA: 0x00015860 File Offset: 0x00013A60
			public override void AddRange(DataRelation[] relations)
			{
				if (this._dataSet._fInitInProgress)
				{
					this._delayLoadingRelations = relations;
					return;
				}
				if (relations != null)
				{
					foreach (DataRelation dataRelation in relations)
					{
						if (dataRelation != null)
						{
							base.Add(dataRelation);
						}
					}
				}
			}

			// Token: 0x0600058A RID: 1418 RVA: 0x000158A3 File Offset: 0x00013AA3
			public override void Clear()
			{
				base.Clear();
				if (this._dataSet._fInitInProgress && this._delayLoadingRelations != null)
				{
					this._delayLoadingRelations = null;
				}
			}

			// Token: 0x0600058B RID: 1419 RVA: 0x000158C7 File Offset: 0x00013AC7
			protected override DataSet GetDataSet()
			{
				return this._dataSet;
			}

			// Token: 0x1700010C RID: 268
			public override DataRelation this[int index]
			{
				get
				{
					if (index >= 0 && index < this._relations.Count)
					{
						return (DataRelation)this._relations[index];
					}
					throw ExceptionBuilder.RelationOutOfRange(index);
				}
			}

			// Token: 0x1700010D RID: 269
			public override DataRelation this[string name]
			{
				get
				{
					int num = base.InternalIndexOf(name);
					if (num == -2)
					{
						throw ExceptionBuilder.CaseInsensitiveNameConflict(name);
					}
					if (num >= 0)
					{
						return (DataRelation)this.List[num];
					}
					return null;
				}
			}

			// Token: 0x0600058E RID: 1422 RVA: 0x00015938 File Offset: 0x00013B38
			protected override void AddCore(DataRelation relation)
			{
				base.AddCore(relation);
				if (relation.ChildTable.DataSet != this._dataSet || relation.ParentTable.DataSet != this._dataSet)
				{
					throw ExceptionBuilder.ForeignRelation();
				}
				relation.CheckState();
				if (relation.Nested)
				{
					relation.CheckNestedRelations();
				}
				if (relation._relationName.Length == 0)
				{
					relation._relationName = base.AssignName();
				}
				else
				{
					base.RegisterName(relation._relationName);
				}
				DataKey childKey = relation.ChildKey;
				for (int i = 0; i < this._relations.Count; i++)
				{
					if (childKey.ColumnsEqual(((DataRelation)this._relations[i]).ChildKey) && relation.ParentKey.ColumnsEqual(((DataRelation)this._relations[i]).ParentKey))
					{
						throw ExceptionBuilder.RelationAlreadyExists();
					}
				}
				this._relations.Add(relation);
				((DataRelationCollection.DataTableRelationCollection)relation.ParentTable.ChildRelations).Add(relation);
				((DataRelationCollection.DataTableRelationCollection)relation.ChildTable.ParentRelations).Add(relation);
				relation.SetDataSet(this._dataSet);
				relation.ChildKey.GetSortIndex().AddRef();
				if (relation.Nested)
				{
					relation.ChildTable.CacheNestedParent();
				}
				ForeignKeyConstraint foreignKeyConstraint = relation.ChildTable.Constraints.FindForeignKeyConstraint(relation.ParentColumnsReference, relation.ChildColumnsReference);
				if (relation._createConstraints && foreignKeyConstraint == null)
				{
					relation.ChildTable.Constraints.Add(foreignKeyConstraint = new ForeignKeyConstraint(relation.ParentColumnsReference, relation.ChildColumnsReference));
					try
					{
						foreignKeyConstraint.ConstraintName = relation.RelationName;
					}
					catch (Exception ex) when (ADP.IsCatchableExceptionType(ex))
					{
						ExceptionBuilder.TraceExceptionWithoutRethrow(ex);
					}
				}
				UniqueConstraint uniqueConstraint = relation.ParentTable.Constraints.FindKeyConstraint(relation.ParentColumnsReference);
				relation.SetParentKeyConstraint(uniqueConstraint);
				relation.SetChildKeyConstraint(foreignKeyConstraint);
			}

			// Token: 0x0600058F RID: 1423 RVA: 0x00015B3C File Offset: 0x00013D3C
			protected override void RemoveCore(DataRelation relation)
			{
				base.RemoveCore(relation);
				this._dataSet.OnRemoveRelationHack(relation);
				relation.SetDataSet(null);
				relation.ChildKey.GetSortIndex().RemoveRef();
				if (relation.Nested)
				{
					relation.ChildTable.CacheNestedParent();
				}
				for (int i = 0; i < this._relations.Count; i++)
				{
					if (relation == this._relations[i])
					{
						this._relations.RemoveAt(i);
						((DataRelationCollection.DataTableRelationCollection)relation.ParentTable.ChildRelations).Remove(relation);
						((DataRelationCollection.DataTableRelationCollection)relation.ChildTable.ParentRelations).Remove(relation);
						if (relation.Nested)
						{
							relation.ChildTable.CacheNestedParent();
						}
						base.UnregisterName(relation.RelationName);
						relation.SetParentKeyConstraint(null);
						relation.SetChildKeyConstraint(null);
						return;
					}
				}
				throw ExceptionBuilder.RelationDoesNotExist();
			}

			// Token: 0x06000590 RID: 1424 RVA: 0x00015C20 File Offset: 0x00013E20
			internal void FinishInitRelations()
			{
				if (this._delayLoadingRelations == null)
				{
					return;
				}
				for (int i = 0; i < this._delayLoadingRelations.Length; i++)
				{
					DataRelation dataRelation = this._delayLoadingRelations[i];
					if (dataRelation._parentColumnNames == null || dataRelation._childColumnNames == null)
					{
						base.Add(dataRelation);
					}
					else
					{
						int num = dataRelation._parentColumnNames.Length;
						DataColumn[] array = new DataColumn[num];
						DataColumn[] array2 = new DataColumn[num];
						for (int j = 0; j < num; j++)
						{
							if (dataRelation._parentTableNamespace == null)
							{
								array[j] = this._dataSet.Tables[dataRelation._parentTableName].Columns[dataRelation._parentColumnNames[j]];
							}
							else
							{
								array[j] = this._dataSet.Tables[dataRelation._parentTableName, dataRelation._parentTableNamespace].Columns[dataRelation._parentColumnNames[j]];
							}
							if (dataRelation._childTableNamespace == null)
							{
								array2[j] = this._dataSet.Tables[dataRelation._childTableName].Columns[dataRelation._childColumnNames[j]];
							}
							else
							{
								array2[j] = this._dataSet.Tables[dataRelation._childTableName, dataRelation._childTableNamespace].Columns[dataRelation._childColumnNames[j]];
							}
						}
						base.Add(new DataRelation(dataRelation._relationName, array, array2, false)
						{
							Nested = dataRelation._nested
						});
					}
				}
				this._delayLoadingRelations = null;
			}

			// Token: 0x04000507 RID: 1287
			private readonly DataSet _dataSet;

			// Token: 0x04000508 RID: 1288
			private readonly ArrayList _relations;

			// Token: 0x04000509 RID: 1289
			private DataRelation[] _delayLoadingRelations;
		}
	}
}
