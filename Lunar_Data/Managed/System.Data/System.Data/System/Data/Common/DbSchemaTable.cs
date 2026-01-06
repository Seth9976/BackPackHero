using System;

namespace System.Data.Common
{
	// Token: 0x02000326 RID: 806
	internal sealed class DbSchemaTable
	{
		// Token: 0x060025BF RID: 9663 RVA: 0x000AB2BD File Offset: 0x000A94BD
		internal DbSchemaTable(DataTable dataTable, bool returnProviderSpecificTypes)
		{
			this._dataTable = dataTable;
			this._columns = dataTable.Columns;
			this._returnProviderSpecificTypes = returnProviderSpecificTypes;
		}

		// Token: 0x1700065A RID: 1626
		// (get) Token: 0x060025C0 RID: 9664 RVA: 0x000AB2F1 File Offset: 0x000A94F1
		internal DataColumn ColumnName
		{
			get
			{
				return this.CachedDataColumn(DbSchemaTable.ColumnEnum.ColumnName);
			}
		}

		// Token: 0x1700065B RID: 1627
		// (get) Token: 0x060025C1 RID: 9665 RVA: 0x000AB2FA File Offset: 0x000A94FA
		internal DataColumn Size
		{
			get
			{
				return this.CachedDataColumn(DbSchemaTable.ColumnEnum.ColumnSize);
			}
		}

		// Token: 0x1700065C RID: 1628
		// (get) Token: 0x060025C2 RID: 9666 RVA: 0x000AB303 File Offset: 0x000A9503
		internal DataColumn BaseServerName
		{
			get
			{
				return this.CachedDataColumn(DbSchemaTable.ColumnEnum.BaseServerName);
			}
		}

		// Token: 0x1700065D RID: 1629
		// (get) Token: 0x060025C3 RID: 9667 RVA: 0x000AB30C File Offset: 0x000A950C
		internal DataColumn BaseColumnName
		{
			get
			{
				return this.CachedDataColumn(DbSchemaTable.ColumnEnum.BaseColumnName);
			}
		}

		// Token: 0x1700065E RID: 1630
		// (get) Token: 0x060025C4 RID: 9668 RVA: 0x000AB315 File Offset: 0x000A9515
		internal DataColumn BaseTableName
		{
			get
			{
				return this.CachedDataColumn(DbSchemaTable.ColumnEnum.BaseTableName);
			}
		}

		// Token: 0x1700065F RID: 1631
		// (get) Token: 0x060025C5 RID: 9669 RVA: 0x000AB31E File Offset: 0x000A951E
		internal DataColumn BaseCatalogName
		{
			get
			{
				return this.CachedDataColumn(DbSchemaTable.ColumnEnum.BaseCatalogName);
			}
		}

		// Token: 0x17000660 RID: 1632
		// (get) Token: 0x060025C6 RID: 9670 RVA: 0x000AB327 File Offset: 0x000A9527
		internal DataColumn BaseSchemaName
		{
			get
			{
				return this.CachedDataColumn(DbSchemaTable.ColumnEnum.BaseSchemaName);
			}
		}

		// Token: 0x17000661 RID: 1633
		// (get) Token: 0x060025C7 RID: 9671 RVA: 0x000AB330 File Offset: 0x000A9530
		internal DataColumn IsAutoIncrement
		{
			get
			{
				return this.CachedDataColumn(DbSchemaTable.ColumnEnum.IsAutoIncrement);
			}
		}

		// Token: 0x17000662 RID: 1634
		// (get) Token: 0x060025C8 RID: 9672 RVA: 0x000AB339 File Offset: 0x000A9539
		internal DataColumn IsUnique
		{
			get
			{
				return this.CachedDataColumn(DbSchemaTable.ColumnEnum.IsUnique);
			}
		}

		// Token: 0x17000663 RID: 1635
		// (get) Token: 0x060025C9 RID: 9673 RVA: 0x000AB343 File Offset: 0x000A9543
		internal DataColumn IsKey
		{
			get
			{
				return this.CachedDataColumn(DbSchemaTable.ColumnEnum.IsKey);
			}
		}

		// Token: 0x17000664 RID: 1636
		// (get) Token: 0x060025CA RID: 9674 RVA: 0x000AB34D File Offset: 0x000A954D
		internal DataColumn IsRowVersion
		{
			get
			{
				return this.CachedDataColumn(DbSchemaTable.ColumnEnum.IsRowVersion);
			}
		}

		// Token: 0x17000665 RID: 1637
		// (get) Token: 0x060025CB RID: 9675 RVA: 0x000AB357 File Offset: 0x000A9557
		internal DataColumn AllowDBNull
		{
			get
			{
				return this.CachedDataColumn(DbSchemaTable.ColumnEnum.AllowDBNull);
			}
		}

		// Token: 0x17000666 RID: 1638
		// (get) Token: 0x060025CC RID: 9676 RVA: 0x000AB361 File Offset: 0x000A9561
		internal DataColumn IsExpression
		{
			get
			{
				return this.CachedDataColumn(DbSchemaTable.ColumnEnum.IsExpression);
			}
		}

		// Token: 0x17000667 RID: 1639
		// (get) Token: 0x060025CD RID: 9677 RVA: 0x000AB36B File Offset: 0x000A956B
		internal DataColumn IsHidden
		{
			get
			{
				return this.CachedDataColumn(DbSchemaTable.ColumnEnum.IsHidden);
			}
		}

		// Token: 0x17000668 RID: 1640
		// (get) Token: 0x060025CE RID: 9678 RVA: 0x000AB375 File Offset: 0x000A9575
		internal DataColumn IsLong
		{
			get
			{
				return this.CachedDataColumn(DbSchemaTable.ColumnEnum.IsLong);
			}
		}

		// Token: 0x17000669 RID: 1641
		// (get) Token: 0x060025CF RID: 9679 RVA: 0x000AB37F File Offset: 0x000A957F
		internal DataColumn IsReadOnly
		{
			get
			{
				return this.CachedDataColumn(DbSchemaTable.ColumnEnum.IsReadOnly);
			}
		}

		// Token: 0x1700066A RID: 1642
		// (get) Token: 0x060025D0 RID: 9680 RVA: 0x000AB389 File Offset: 0x000A9589
		internal DataColumn UnsortedIndex
		{
			get
			{
				return this.CachedDataColumn(DbSchemaTable.ColumnEnum.SchemaMappingUnsortedIndex);
			}
		}

		// Token: 0x1700066B RID: 1643
		// (get) Token: 0x060025D1 RID: 9681 RVA: 0x000AB393 File Offset: 0x000A9593
		internal DataColumn DataType
		{
			get
			{
				if (this._returnProviderSpecificTypes)
				{
					return this.CachedDataColumn(DbSchemaTable.ColumnEnum.ProviderSpecificDataType, DbSchemaTable.ColumnEnum.DataType);
				}
				return this.CachedDataColumn(DbSchemaTable.ColumnEnum.DataType);
			}
		}

		// Token: 0x060025D2 RID: 9682 RVA: 0x000AB3B0 File Offset: 0x000A95B0
		private DataColumn CachedDataColumn(DbSchemaTable.ColumnEnum column)
		{
			return this.CachedDataColumn(column, column);
		}

		// Token: 0x060025D3 RID: 9683 RVA: 0x000AB3BC File Offset: 0x000A95BC
		private DataColumn CachedDataColumn(DbSchemaTable.ColumnEnum column, DbSchemaTable.ColumnEnum column2)
		{
			DataColumn dataColumn = this._columnCache[(int)column];
			if (dataColumn == null)
			{
				int num = this._columns.IndexOf(DbSchemaTable.s_DBCOLUMN_NAME[(int)column]);
				if (-1 == num && column != column2)
				{
					num = this._columns.IndexOf(DbSchemaTable.s_DBCOLUMN_NAME[(int)column2]);
				}
				if (-1 != num)
				{
					dataColumn = this._columns[num];
					this._columnCache[(int)column] = dataColumn;
				}
			}
			return dataColumn;
		}

		// Token: 0x04001888 RID: 6280
		private static readonly string[] s_DBCOLUMN_NAME = new string[]
		{
			SchemaTableColumn.ColumnName,
			SchemaTableColumn.ColumnOrdinal,
			SchemaTableColumn.ColumnSize,
			SchemaTableOptionalColumn.BaseServerName,
			SchemaTableOptionalColumn.BaseCatalogName,
			SchemaTableColumn.BaseColumnName,
			SchemaTableColumn.BaseSchemaName,
			SchemaTableColumn.BaseTableName,
			SchemaTableOptionalColumn.IsAutoIncrement,
			SchemaTableColumn.IsUnique,
			SchemaTableColumn.IsKey,
			SchemaTableOptionalColumn.IsRowVersion,
			SchemaTableColumn.DataType,
			SchemaTableOptionalColumn.ProviderSpecificDataType,
			SchemaTableColumn.AllowDBNull,
			SchemaTableColumn.ProviderType,
			SchemaTableColumn.IsExpression,
			SchemaTableOptionalColumn.IsHidden,
			SchemaTableColumn.IsLong,
			SchemaTableOptionalColumn.IsReadOnly,
			"SchemaMapping Unsorted Index"
		};

		// Token: 0x04001889 RID: 6281
		internal DataTable _dataTable;

		// Token: 0x0400188A RID: 6282
		private DataColumnCollection _columns;

		// Token: 0x0400188B RID: 6283
		private DataColumn[] _columnCache = new DataColumn[DbSchemaTable.s_DBCOLUMN_NAME.Length];

		// Token: 0x0400188C RID: 6284
		private bool _returnProviderSpecificTypes;

		// Token: 0x02000327 RID: 807
		private enum ColumnEnum
		{
			// Token: 0x0400188E RID: 6286
			ColumnName,
			// Token: 0x0400188F RID: 6287
			ColumnOrdinal,
			// Token: 0x04001890 RID: 6288
			ColumnSize,
			// Token: 0x04001891 RID: 6289
			BaseServerName,
			// Token: 0x04001892 RID: 6290
			BaseCatalogName,
			// Token: 0x04001893 RID: 6291
			BaseColumnName,
			// Token: 0x04001894 RID: 6292
			BaseSchemaName,
			// Token: 0x04001895 RID: 6293
			BaseTableName,
			// Token: 0x04001896 RID: 6294
			IsAutoIncrement,
			// Token: 0x04001897 RID: 6295
			IsUnique,
			// Token: 0x04001898 RID: 6296
			IsKey,
			// Token: 0x04001899 RID: 6297
			IsRowVersion,
			// Token: 0x0400189A RID: 6298
			DataType,
			// Token: 0x0400189B RID: 6299
			ProviderSpecificDataType,
			// Token: 0x0400189C RID: 6300
			AllowDBNull,
			// Token: 0x0400189D RID: 6301
			ProviderType,
			// Token: 0x0400189E RID: 6302
			IsExpression,
			// Token: 0x0400189F RID: 6303
			IsHidden,
			// Token: 0x040018A0 RID: 6304
			IsLong,
			// Token: 0x040018A1 RID: 6305
			IsReadOnly,
			// Token: 0x040018A2 RID: 6306
			SchemaMappingUnsortedIndex
		}
	}
}
