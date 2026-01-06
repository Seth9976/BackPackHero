using System;
using System.Globalization;

namespace System.Data.Common
{
	// Token: 0x02000325 RID: 805
	internal sealed class DbSchemaRow
	{
		// Token: 0x060025AA RID: 9642 RVA: 0x000AACB8 File Offset: 0x000A8EB8
		internal static DbSchemaRow[] GetSortedSchemaRows(DataTable dataTable, bool returnProviderSpecificTypes)
		{
			DataColumn dataColumn = dataTable.Columns["SchemaMapping Unsorted Index"];
			if (dataColumn == null)
			{
				dataColumn = new DataColumn("SchemaMapping Unsorted Index", typeof(int));
				dataTable.Columns.Add(dataColumn);
			}
			int count = dataTable.Rows.Count;
			for (int i = 0; i < count; i++)
			{
				dataTable.Rows[i][dataColumn] = i;
			}
			DbSchemaTable dbSchemaTable = new DbSchemaTable(dataTable, returnProviderSpecificTypes);
			DataRow[] array = dataTable.Select(null, "ColumnOrdinal ASC", DataViewRowState.CurrentRows);
			DbSchemaRow[] array2 = new DbSchemaRow[array.Length];
			for (int j = 0; j < array.Length; j++)
			{
				array2[j] = new DbSchemaRow(dbSchemaTable, array[j]);
			}
			return array2;
		}

		// Token: 0x060025AB RID: 9643 RVA: 0x000AAD74 File Offset: 0x000A8F74
		internal DbSchemaRow(DbSchemaTable schemaTable, DataRow dataRow)
		{
			this._schemaTable = schemaTable;
			this._dataRow = dataRow;
		}

		// Token: 0x17000647 RID: 1607
		// (get) Token: 0x060025AC RID: 9644 RVA: 0x000AAD8A File Offset: 0x000A8F8A
		internal DataRow DataRow
		{
			get
			{
				return this._dataRow;
			}
		}

		// Token: 0x17000648 RID: 1608
		// (get) Token: 0x060025AD RID: 9645 RVA: 0x000AAD94 File Offset: 0x000A8F94
		internal string ColumnName
		{
			get
			{
				object obj = this._dataRow[this._schemaTable.ColumnName, DataRowVersion.Default];
				if (!Convert.IsDBNull(obj))
				{
					return Convert.ToString(obj, CultureInfo.InvariantCulture);
				}
				return string.Empty;
			}
		}

		// Token: 0x17000649 RID: 1609
		// (get) Token: 0x060025AE RID: 9646 RVA: 0x000AADD8 File Offset: 0x000A8FD8
		internal int Size
		{
			get
			{
				object obj = this._dataRow[this._schemaTable.Size, DataRowVersion.Default];
				if (!Convert.IsDBNull(obj))
				{
					return Convert.ToInt32(obj, CultureInfo.InvariantCulture);
				}
				return 0;
			}
		}

		// Token: 0x1700064A RID: 1610
		// (get) Token: 0x060025AF RID: 9647 RVA: 0x000AAE18 File Offset: 0x000A9018
		internal string BaseColumnName
		{
			get
			{
				if (this._schemaTable.BaseColumnName != null)
				{
					object obj = this._dataRow[this._schemaTable.BaseColumnName, DataRowVersion.Default];
					if (!Convert.IsDBNull(obj))
					{
						return Convert.ToString(obj, CultureInfo.InvariantCulture);
					}
				}
				return string.Empty;
			}
		}

		// Token: 0x1700064B RID: 1611
		// (get) Token: 0x060025B0 RID: 9648 RVA: 0x000AAE68 File Offset: 0x000A9068
		internal string BaseServerName
		{
			get
			{
				if (this._schemaTable.BaseServerName != null)
				{
					object obj = this._dataRow[this._schemaTable.BaseServerName, DataRowVersion.Default];
					if (!Convert.IsDBNull(obj))
					{
						return Convert.ToString(obj, CultureInfo.InvariantCulture);
					}
				}
				return string.Empty;
			}
		}

		// Token: 0x1700064C RID: 1612
		// (get) Token: 0x060025B1 RID: 9649 RVA: 0x000AAEB8 File Offset: 0x000A90B8
		internal string BaseCatalogName
		{
			get
			{
				if (this._schemaTable.BaseCatalogName != null)
				{
					object obj = this._dataRow[this._schemaTable.BaseCatalogName, DataRowVersion.Default];
					if (!Convert.IsDBNull(obj))
					{
						return Convert.ToString(obj, CultureInfo.InvariantCulture);
					}
				}
				return string.Empty;
			}
		}

		// Token: 0x1700064D RID: 1613
		// (get) Token: 0x060025B2 RID: 9650 RVA: 0x000AAF08 File Offset: 0x000A9108
		internal string BaseSchemaName
		{
			get
			{
				if (this._schemaTable.BaseSchemaName != null)
				{
					object obj = this._dataRow[this._schemaTable.BaseSchemaName, DataRowVersion.Default];
					if (!Convert.IsDBNull(obj))
					{
						return Convert.ToString(obj, CultureInfo.InvariantCulture);
					}
				}
				return string.Empty;
			}
		}

		// Token: 0x1700064E RID: 1614
		// (get) Token: 0x060025B3 RID: 9651 RVA: 0x000AAF58 File Offset: 0x000A9158
		internal string BaseTableName
		{
			get
			{
				if (this._schemaTable.BaseTableName != null)
				{
					object obj = this._dataRow[this._schemaTable.BaseTableName, DataRowVersion.Default];
					if (!Convert.IsDBNull(obj))
					{
						return Convert.ToString(obj, CultureInfo.InvariantCulture);
					}
				}
				return string.Empty;
			}
		}

		// Token: 0x1700064F RID: 1615
		// (get) Token: 0x060025B4 RID: 9652 RVA: 0x000AAFA8 File Offset: 0x000A91A8
		internal bool IsAutoIncrement
		{
			get
			{
				if (this._schemaTable.IsAutoIncrement != null)
				{
					object obj = this._dataRow[this._schemaTable.IsAutoIncrement, DataRowVersion.Default];
					if (!Convert.IsDBNull(obj))
					{
						return Convert.ToBoolean(obj, CultureInfo.InvariantCulture);
					}
				}
				return false;
			}
		}

		// Token: 0x17000650 RID: 1616
		// (get) Token: 0x060025B5 RID: 9653 RVA: 0x000AAFF4 File Offset: 0x000A91F4
		internal bool IsUnique
		{
			get
			{
				if (this._schemaTable.IsUnique != null)
				{
					object obj = this._dataRow[this._schemaTable.IsUnique, DataRowVersion.Default];
					if (!Convert.IsDBNull(obj))
					{
						return Convert.ToBoolean(obj, CultureInfo.InvariantCulture);
					}
				}
				return false;
			}
		}

		// Token: 0x17000651 RID: 1617
		// (get) Token: 0x060025B6 RID: 9654 RVA: 0x000AB040 File Offset: 0x000A9240
		internal bool IsRowVersion
		{
			get
			{
				if (this._schemaTable.IsRowVersion != null)
				{
					object obj = this._dataRow[this._schemaTable.IsRowVersion, DataRowVersion.Default];
					if (!Convert.IsDBNull(obj))
					{
						return Convert.ToBoolean(obj, CultureInfo.InvariantCulture);
					}
				}
				return false;
			}
		}

		// Token: 0x17000652 RID: 1618
		// (get) Token: 0x060025B7 RID: 9655 RVA: 0x000AB08C File Offset: 0x000A928C
		internal bool IsKey
		{
			get
			{
				if (this._schemaTable.IsKey != null)
				{
					object obj = this._dataRow[this._schemaTable.IsKey, DataRowVersion.Default];
					if (!Convert.IsDBNull(obj))
					{
						return Convert.ToBoolean(obj, CultureInfo.InvariantCulture);
					}
				}
				return false;
			}
		}

		// Token: 0x17000653 RID: 1619
		// (get) Token: 0x060025B8 RID: 9656 RVA: 0x000AB0D8 File Offset: 0x000A92D8
		internal bool IsExpression
		{
			get
			{
				if (this._schemaTable.IsExpression != null)
				{
					object obj = this._dataRow[this._schemaTable.IsExpression, DataRowVersion.Default];
					if (!Convert.IsDBNull(obj))
					{
						return Convert.ToBoolean(obj, CultureInfo.InvariantCulture);
					}
				}
				return false;
			}
		}

		// Token: 0x17000654 RID: 1620
		// (get) Token: 0x060025B9 RID: 9657 RVA: 0x000AB124 File Offset: 0x000A9324
		internal bool IsHidden
		{
			get
			{
				if (this._schemaTable.IsHidden != null)
				{
					object obj = this._dataRow[this._schemaTable.IsHidden, DataRowVersion.Default];
					if (!Convert.IsDBNull(obj))
					{
						return Convert.ToBoolean(obj, CultureInfo.InvariantCulture);
					}
				}
				return false;
			}
		}

		// Token: 0x17000655 RID: 1621
		// (get) Token: 0x060025BA RID: 9658 RVA: 0x000AB170 File Offset: 0x000A9370
		internal bool IsLong
		{
			get
			{
				if (this._schemaTable.IsLong != null)
				{
					object obj = this._dataRow[this._schemaTable.IsLong, DataRowVersion.Default];
					if (!Convert.IsDBNull(obj))
					{
						return Convert.ToBoolean(obj, CultureInfo.InvariantCulture);
					}
				}
				return false;
			}
		}

		// Token: 0x17000656 RID: 1622
		// (get) Token: 0x060025BB RID: 9659 RVA: 0x000AB1BC File Offset: 0x000A93BC
		internal bool IsReadOnly
		{
			get
			{
				if (this._schemaTable.IsReadOnly != null)
				{
					object obj = this._dataRow[this._schemaTable.IsReadOnly, DataRowVersion.Default];
					if (!Convert.IsDBNull(obj))
					{
						return Convert.ToBoolean(obj, CultureInfo.InvariantCulture);
					}
				}
				return false;
			}
		}

		// Token: 0x17000657 RID: 1623
		// (get) Token: 0x060025BC RID: 9660 RVA: 0x000AB208 File Offset: 0x000A9408
		internal Type DataType
		{
			get
			{
				if (this._schemaTable.DataType != null)
				{
					object obj = this._dataRow[this._schemaTable.DataType, DataRowVersion.Default];
					if (!Convert.IsDBNull(obj))
					{
						return (Type)obj;
					}
				}
				return null;
			}
		}

		// Token: 0x17000658 RID: 1624
		// (get) Token: 0x060025BD RID: 9661 RVA: 0x000AB250 File Offset: 0x000A9450
		internal bool AllowDBNull
		{
			get
			{
				if (this._schemaTable.AllowDBNull != null)
				{
					object obj = this._dataRow[this._schemaTable.AllowDBNull, DataRowVersion.Default];
					if (!Convert.IsDBNull(obj))
					{
						return Convert.ToBoolean(obj, CultureInfo.InvariantCulture);
					}
				}
				return true;
			}
		}

		// Token: 0x17000659 RID: 1625
		// (get) Token: 0x060025BE RID: 9662 RVA: 0x000AB29B File Offset: 0x000A949B
		internal int UnsortedIndex
		{
			get
			{
				return (int)this._dataRow[this._schemaTable.UnsortedIndex, DataRowVersion.Default];
			}
		}

		// Token: 0x04001885 RID: 6277
		internal const string SchemaMappingUnsortedIndex = "SchemaMapping Unsorted Index";

		// Token: 0x04001886 RID: 6278
		private DbSchemaTable _schemaTable;

		// Token: 0x04001887 RID: 6279
		private DataRow _dataRow;
	}
}
