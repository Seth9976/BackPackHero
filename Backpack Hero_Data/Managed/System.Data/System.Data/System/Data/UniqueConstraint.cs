using System;
using System.ComponentModel;
using System.Diagnostics;

namespace System.Data
{
	/// <summary>Represents a restriction on a set of columns in which all values must be unique.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020000E6 RID: 230
	[DefaultProperty("ConstraintName")]
	public class UniqueConstraint : Constraint
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.UniqueConstraint" /> class with the specified name and <see cref="T:System.Data.DataColumn" />.</summary>
		/// <param name="name">The name of the constraint. </param>
		/// <param name="column">The <see cref="T:System.Data.DataColumn" /> to constrain. </param>
		// Token: 0x06000CB7 RID: 3255 RVA: 0x0003A040 File Offset: 0x00038240
		public UniqueConstraint(string name, DataColumn column)
		{
			this.Create(name, new DataColumn[] { column });
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.UniqueConstraint" /> class with the specified <see cref="T:System.Data.DataColumn" />.</summary>
		/// <param name="column">The <see cref="T:System.Data.DataColumn" /> to constrain. </param>
		// Token: 0x06000CB8 RID: 3256 RVA: 0x0003A068 File Offset: 0x00038268
		public UniqueConstraint(DataColumn column)
		{
			this.Create(null, new DataColumn[] { column });
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.UniqueConstraint" /> class with the specified name and array of <see cref="T:System.Data.DataColumn" /> objects.</summary>
		/// <param name="name">The name of the constraint. </param>
		/// <param name="columns">The array of <see cref="T:System.Data.DataColumn" /> objects to constrain. </param>
		// Token: 0x06000CB9 RID: 3257 RVA: 0x0003A08E File Offset: 0x0003828E
		public UniqueConstraint(string name, DataColumn[] columns)
		{
			this.Create(name, columns);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.UniqueConstraint" /> class with the given array of <see cref="T:System.Data.DataColumn" /> objects.</summary>
		/// <param name="columns">The array of <see cref="T:System.Data.DataColumn" /> objects to constrain. </param>
		// Token: 0x06000CBA RID: 3258 RVA: 0x0003A09E File Offset: 0x0003829E
		public UniqueConstraint(DataColumn[] columns)
		{
			this.Create(null, columns);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.UniqueConstraint" /> class with the specified name, an array of <see cref="T:System.Data.DataColumn" /> objects to constrain, and a value specifying whether the constraint is a primary key.</summary>
		/// <param name="name">The name of the constraint. </param>
		/// <param name="columnNames">An array of <see cref="T:System.Data.DataColumn" /> objects to constrain. </param>
		/// <param name="isPrimaryKey">true to indicate that the constraint is a primary key; otherwise, false. </param>
		// Token: 0x06000CBB RID: 3259 RVA: 0x0003A0AE File Offset: 0x000382AE
		[Browsable(false)]
		public UniqueConstraint(string name, string[] columnNames, bool isPrimaryKey)
		{
			this._constraintName = name;
			this._columnNames = columnNames;
			this._bPrimaryKey = isPrimaryKey;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.UniqueConstraint" /> class with the specified name, the <see cref="T:System.Data.DataColumn" /> to constrain, and a value specifying whether the constraint is a primary key.</summary>
		/// <param name="name">The name of the constraint. </param>
		/// <param name="column">The <see cref="T:System.Data.DataColumn" /> to constrain. </param>
		/// <param name="isPrimaryKey">true to indicate that the constraint is a primary key; otherwise, false. </param>
		// Token: 0x06000CBC RID: 3260 RVA: 0x0003A0CC File Offset: 0x000382CC
		public UniqueConstraint(string name, DataColumn column, bool isPrimaryKey)
		{
			DataColumn[] array = new DataColumn[] { column };
			this._bPrimaryKey = isPrimaryKey;
			this.Create(name, array);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.UniqueConstraint" /> class with the <see cref="T:System.Data.DataColumn" /> to constrain, and a value specifying whether the constraint is a primary key.</summary>
		/// <param name="column">The <see cref="T:System.Data.DataColumn" /> to constrain. </param>
		/// <param name="isPrimaryKey">true to indicate that the constraint is a primary key; otherwise, false. </param>
		// Token: 0x06000CBD RID: 3261 RVA: 0x0003A0FC File Offset: 0x000382FC
		public UniqueConstraint(DataColumn column, bool isPrimaryKey)
		{
			DataColumn[] array = new DataColumn[] { column };
			this._bPrimaryKey = isPrimaryKey;
			this.Create(null, array);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.UniqueConstraint" /> class with the specified name, an array of <see cref="T:System.Data.DataColumn" /> objects to constrain, and a value specifying whether the constraint is a primary key.</summary>
		/// <param name="name">The name of the constraint. </param>
		/// <param name="columns">An array of <see cref="T:System.Data.DataColumn" /> objects to constrain. </param>
		/// <param name="isPrimaryKey">true to indicate that the constraint is a primary key; otherwise, false. </param>
		// Token: 0x06000CBE RID: 3262 RVA: 0x0003A129 File Offset: 0x00038329
		public UniqueConstraint(string name, DataColumn[] columns, bool isPrimaryKey)
		{
			this._bPrimaryKey = isPrimaryKey;
			this.Create(name, columns);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.UniqueConstraint" /> class with an array of <see cref="T:System.Data.DataColumn" /> objects to constrain, and a value specifying whether the constraint is a primary key.</summary>
		/// <param name="columns">An array of <see cref="T:System.Data.DataColumn" /> objects to constrain. </param>
		/// <param name="isPrimaryKey">true to indicate that the constraint is a primary key; otherwise, false. </param>
		// Token: 0x06000CBF RID: 3263 RVA: 0x0003A140 File Offset: 0x00038340
		public UniqueConstraint(DataColumn[] columns, bool isPrimaryKey)
		{
			this._bPrimaryKey = isPrimaryKey;
			this.Create(null, columns);
		}

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x06000CC0 RID: 3264 RVA: 0x0003A157 File Offset: 0x00038357
		internal string[] ColumnNames
		{
			get
			{
				return this._key.GetColumnNames();
			}
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x06000CC1 RID: 3265 RVA: 0x0003A164 File Offset: 0x00038364
		internal Index ConstraintIndex
		{
			get
			{
				return this._constraintIndex;
			}
		}

		// Token: 0x06000CC2 RID: 3266 RVA: 0x0003A16C File Offset: 0x0003836C
		[Conditional("DEBUG")]
		private void AssertConstraintAndKeyIndexes()
		{
			DataColumn[] array = new DataColumn[this._constraintIndex._indexFields.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = this._constraintIndex._indexFields[i].Column;
			}
		}

		// Token: 0x06000CC3 RID: 3267 RVA: 0x0003A1B3 File Offset: 0x000383B3
		internal void ConstraintIndexClear()
		{
			if (this._constraintIndex != null)
			{
				this._constraintIndex.RemoveRef();
				this._constraintIndex = null;
			}
		}

		// Token: 0x06000CC4 RID: 3268 RVA: 0x0003A1D0 File Offset: 0x000383D0
		internal void ConstraintIndexInitialize()
		{
			if (this._constraintIndex == null)
			{
				this._constraintIndex = this._key.GetSortIndex();
				this._constraintIndex.AddRef();
			}
		}

		// Token: 0x06000CC5 RID: 3269 RVA: 0x0003A1F6 File Offset: 0x000383F6
		internal override void CheckState()
		{
			this.NonVirtualCheckState();
		}

		// Token: 0x06000CC6 RID: 3270 RVA: 0x0003A1FE File Offset: 0x000383FE
		private void NonVirtualCheckState()
		{
			this._key.CheckState();
		}

		// Token: 0x06000CC7 RID: 3271 RVA: 0x000094D4 File Offset: 0x000076D4
		internal override void CheckCanAddToCollection(ConstraintCollection constraints)
		{
		}

		// Token: 0x06000CC8 RID: 3272 RVA: 0x0003A20C File Offset: 0x0003840C
		internal override bool CanBeRemovedFromCollection(ConstraintCollection constraints, bool fThrowException)
		{
			if (!this.Equals(constraints.Table._primaryKey))
			{
				ParentForeignKeyConstraintEnumerator parentForeignKeyConstraintEnumerator = new ParentForeignKeyConstraintEnumerator(this.Table.DataSet, this.Table);
				while (parentForeignKeyConstraintEnumerator.GetNext())
				{
					ForeignKeyConstraint foreignKeyConstraint = parentForeignKeyConstraintEnumerator.GetForeignKeyConstraint();
					if (this._key.ColumnsEqual(foreignKeyConstraint.ParentKey))
					{
						if (!fThrowException)
						{
							return false;
						}
						throw ExceptionBuilder.NeededForForeignKeyConstraint(this, foreignKeyConstraint);
					}
				}
				return true;
			}
			if (!fThrowException)
			{
				return false;
			}
			throw ExceptionBuilder.RemovePrimaryKey(constraints.Table);
		}

		// Token: 0x06000CC9 RID: 3273 RVA: 0x0003A286 File Offset: 0x00038486
		internal override bool CanEnableConstraint()
		{
			return !this.Table.EnforceConstraints || this.ConstraintIndex.CheckUnique();
		}

		// Token: 0x06000CCA RID: 3274 RVA: 0x0003A2A4 File Offset: 0x000384A4
		internal override bool IsConstraintViolated()
		{
			bool flag = false;
			Index constraintIndex = this.ConstraintIndex;
			if (constraintIndex.HasDuplicates)
			{
				object[] uniqueKeyValues = constraintIndex.GetUniqueKeyValues();
				for (int i = 0; i < uniqueKeyValues.Length; i++)
				{
					Range range = constraintIndex.FindRecords((object[])uniqueKeyValues[i]);
					if (1 < range.Count)
					{
						DataRow[] rows = constraintIndex.GetRows(range);
						string text = ExceptionBuilder.UniqueConstraintViolationText(this._key.ColumnsReference, (object[])uniqueKeyValues[i]);
						for (int j = 0; j < rows.Length; j++)
						{
							rows[j].RowError = text;
							foreach (DataColumn dataColumn in this._key.ColumnsReference)
							{
								rows[j].SetColumnError(dataColumn, text);
							}
						}
						flag = true;
					}
				}
			}
			return flag;
		}

		// Token: 0x06000CCB RID: 3275 RVA: 0x0003A378 File Offset: 0x00038578
		internal override void CheckConstraint(DataRow row, DataRowAction action)
		{
			if (this.Table.EnforceConstraints && (action == DataRowAction.Add || action == DataRowAction.Change || (action == DataRowAction.Rollback && row._tempRecord != -1)) && row.HaveValuesChanged(this.ColumnsReference) && this.ConstraintIndex.IsKeyRecordInIndex(row.GetDefaultRecord()))
			{
				object[] columnValues = row.GetColumnValues(this.ColumnsReference);
				throw ExceptionBuilder.ConstraintViolation(this.ColumnsReference, columnValues);
			}
		}

		// Token: 0x06000CCC RID: 3276 RVA: 0x0003A3E3 File Offset: 0x000385E3
		internal override bool ContainsColumn(DataColumn column)
		{
			return this._key.ContainsColumn(column);
		}

		// Token: 0x06000CCD RID: 3277 RVA: 0x00032B13 File Offset: 0x00030D13
		internal override Constraint Clone(DataSet destination)
		{
			return this.Clone(destination, false);
		}

		// Token: 0x06000CCE RID: 3278 RVA: 0x0003A3F4 File Offset: 0x000385F4
		internal override Constraint Clone(DataSet destination, bool ignorNSforTableLookup)
		{
			int num;
			if (ignorNSforTableLookup)
			{
				num = destination.Tables.IndexOf(this.Table.TableName);
			}
			else
			{
				num = destination.Tables.IndexOf(this.Table.TableName, this.Table.Namespace, false);
			}
			if (num < 0)
			{
				return null;
			}
			DataTable dataTable = destination.Tables[num];
			int num2 = this.ColumnsReference.Length;
			DataColumn[] array = new DataColumn[num2];
			for (int i = 0; i < num2; i++)
			{
				DataColumn dataColumn = this.ColumnsReference[i];
				num = dataTable.Columns.IndexOf(dataColumn.ColumnName);
				if (num < 0)
				{
					return null;
				}
				array[i] = dataTable.Columns[num];
			}
			UniqueConstraint uniqueConstraint = new UniqueConstraint(this.ConstraintName, array);
			foreach (object obj in base.ExtendedProperties.Keys)
			{
				uniqueConstraint.ExtendedProperties[obj] = base.ExtendedProperties[obj];
			}
			return uniqueConstraint;
		}

		// Token: 0x06000CCF RID: 3279 RVA: 0x0003A520 File Offset: 0x00038720
		internal UniqueConstraint Clone(DataTable table)
		{
			int num = this.ColumnsReference.Length;
			DataColumn[] array = new DataColumn[num];
			for (int i = 0; i < num; i++)
			{
				DataColumn dataColumn = this.ColumnsReference[i];
				int num2 = table.Columns.IndexOf(dataColumn.ColumnName);
				if (num2 < 0)
				{
					return null;
				}
				array[i] = table.Columns[num2];
			}
			UniqueConstraint uniqueConstraint = new UniqueConstraint(this.ConstraintName, array);
			foreach (object obj in base.ExtendedProperties.Keys)
			{
				uniqueConstraint.ExtendedProperties[obj] = base.ExtendedProperties[obj];
			}
			return uniqueConstraint;
		}

		/// <summary>Gets the array of columns that this constraint affects.</summary>
		/// <returns>An array of <see cref="T:System.Data.DataColumn" /> objects.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700023C RID: 572
		// (get) Token: 0x06000CD0 RID: 3280 RVA: 0x0003A5F4 File Offset: 0x000387F4
		[ReadOnly(true)]
		public virtual DataColumn[] Columns
		{
			get
			{
				return this._key.ToArray();
			}
		}

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x06000CD1 RID: 3281 RVA: 0x0003A601 File Offset: 0x00038801
		internal DataColumn[] ColumnsReference
		{
			get
			{
				return this._key.ColumnsReference;
			}
		}

		/// <summary>Gets a value indicating whether or not the constraint is on a primary key.</summary>
		/// <returns>true, if the constraint is on a primary key; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700023E RID: 574
		// (get) Token: 0x06000CD2 RID: 3282 RVA: 0x0003A60E File Offset: 0x0003880E
		public bool IsPrimaryKey
		{
			get
			{
				return this.Table != null && this == this.Table._primaryKey;
			}
		}

		// Token: 0x06000CD3 RID: 3283 RVA: 0x0003A628 File Offset: 0x00038828
		private void Create(string constraintName, DataColumn[] columns)
		{
			for (int i = 0; i < columns.Length; i++)
			{
				if (columns[i].Computed)
				{
					throw ExceptionBuilder.ExpressionInConstraint(columns[i]);
				}
			}
			this._key = new DataKey(columns, true);
			this.ConstraintName = constraintName;
			this.NonVirtualCheckState();
		}

		/// <summary>Compares this constraint to a second to determine if both are identical.</summary>
		/// <returns>true, if the contraints are equal; otherwise, false.</returns>
		/// <param name="key2">The object to which this <see cref="T:System.Data.UniqueConstraint" /> is compared. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000CD4 RID: 3284 RVA: 0x0003A670 File Offset: 0x00038870
		public override bool Equals(object key2)
		{
			return key2 is UniqueConstraint && this.Key.ColumnsEqual(((UniqueConstraint)key2).Key);
		}

		/// <summary>Gets the hash code of this instance of the <see cref="T:System.Data.UniqueConstraint" /> object.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000CD5 RID: 3285 RVA: 0x00032F3A File Offset: 0x0003113A
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x1700023F RID: 575
		// (set) Token: 0x06000CD6 RID: 3286 RVA: 0x0003A6A0 File Offset: 0x000388A0
		internal override bool InCollection
		{
			set
			{
				base.InCollection = value;
				if (this._key.ColumnsReference.Length == 1)
				{
					this._key.ColumnsReference[0].InternalUnique(value);
				}
			}
		}

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x06000CD7 RID: 3287 RVA: 0x0003A6CC File Offset: 0x000388CC
		internal DataKey Key
		{
			get
			{
				return this._key;
			}
		}

		/// <summary>Gets the table to which this constraint belongs.</summary>
		/// <returns>The <see cref="T:System.Data.DataTable" /> to which the constraint belongs.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000241 RID: 577
		// (get) Token: 0x06000CD8 RID: 3288 RVA: 0x0003A6D4 File Offset: 0x000388D4
		[ReadOnly(true)]
		public override DataTable Table
		{
			get
			{
				if (this._key.HasValue)
				{
					return this._key.Table;
				}
				return null;
			}
		}

		// Token: 0x0400083A RID: 2106
		private DataKey _key;

		// Token: 0x0400083B RID: 2107
		private Index _constraintIndex;

		// Token: 0x0400083C RID: 2108
		internal bool _bPrimaryKey;

		// Token: 0x0400083D RID: 2109
		internal string _constraintName;

		// Token: 0x0400083E RID: 2110
		internal string[] _columnNames;
	}
}
