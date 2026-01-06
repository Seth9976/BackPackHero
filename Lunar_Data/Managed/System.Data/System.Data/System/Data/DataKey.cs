using System;

namespace System.Data
{
	// Token: 0x0200005E RID: 94
	internal readonly struct DataKey
	{
		// Token: 0x06000514 RID: 1300 RVA: 0x000137AC File Offset: 0x000119AC
		internal DataKey(DataColumn[] columns, bool copyColumns)
		{
			if (columns == null)
			{
				throw ExceptionBuilder.ArgumentNull("columns");
			}
			if (columns.Length == 0)
			{
				throw ExceptionBuilder.KeyNoColumns();
			}
			if (columns.Length > 32)
			{
				throw ExceptionBuilder.KeyTooManyColumns(32);
			}
			for (int i = 0; i < columns.Length; i++)
			{
				if (columns[i] == null)
				{
					throw ExceptionBuilder.ArgumentNull("column");
				}
			}
			for (int j = 0; j < columns.Length; j++)
			{
				for (int k = 0; k < j; k++)
				{
					if (columns[j] == columns[k])
					{
						throw ExceptionBuilder.KeyDuplicateColumns(columns[j].ColumnName);
					}
				}
			}
			if (copyColumns)
			{
				this._columns = new DataColumn[columns.Length];
				for (int l = 0; l < columns.Length; l++)
				{
					this._columns[l] = columns[l];
				}
			}
			else
			{
				this._columns = columns;
			}
			this.CheckState();
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000515 RID: 1301 RVA: 0x00013867 File Offset: 0x00011A67
		internal DataColumn[] ColumnsReference
		{
			get
			{
				return this._columns;
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000516 RID: 1302 RVA: 0x0001386F File Offset: 0x00011A6F
		internal bool HasValue
		{
			get
			{
				return this._columns != null;
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000517 RID: 1303 RVA: 0x0001387A File Offset: 0x00011A7A
		internal DataTable Table
		{
			get
			{
				return this._columns[0].Table;
			}
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x0001388C File Offset: 0x00011A8C
		internal void CheckState()
		{
			DataTable table = this._columns[0].Table;
			if (table == null)
			{
				throw ExceptionBuilder.ColumnNotInAnyTable();
			}
			for (int i = 1; i < this._columns.Length; i++)
			{
				if (this._columns[i].Table == null)
				{
					throw ExceptionBuilder.ColumnNotInAnyTable();
				}
				if (this._columns[i].Table != table)
				{
					throw ExceptionBuilder.KeyTableMismatch();
				}
			}
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x000138EE File Offset: 0x00011AEE
		internal bool ColumnsEqual(DataKey key)
		{
			return DataKey.ColumnsEqual(this._columns, key._columns);
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x00013904 File Offset: 0x00011B04
		internal static bool ColumnsEqual(DataColumn[] column1, DataColumn[] column2)
		{
			if (column1 == column2)
			{
				return true;
			}
			if (column1 == null || column2 == null)
			{
				return false;
			}
			if (column1.Length != column2.Length)
			{
				return false;
			}
			for (int i = 0; i < column1.Length; i++)
			{
				bool flag = false;
				for (int j = 0; j < column2.Length; j++)
				{
					if (column1[i].Equals(column2[j]))
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

		// Token: 0x0600051B RID: 1307 RVA: 0x00013960 File Offset: 0x00011B60
		internal bool ContainsColumn(DataColumn column)
		{
			for (int i = 0; i < this._columns.Length; i++)
			{
				if (column == this._columns[i])
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x0001398E File Offset: 0x00011B8E
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x000139A0 File Offset: 0x00011BA0
		public override bool Equals(object value)
		{
			return this.Equals((DataKey)value);
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x000139B0 File Offset: 0x00011BB0
		internal bool Equals(DataKey value)
		{
			DataColumn[] columns = this._columns;
			DataColumn[] columns2 = value._columns;
			if (columns == columns2)
			{
				return true;
			}
			if (columns == null || columns2 == null)
			{
				return false;
			}
			if (columns.Length != columns2.Length)
			{
				return false;
			}
			for (int i = 0; i < columns.Length; i++)
			{
				if (!columns[i].Equals(columns2[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x00013A04 File Offset: 0x00011C04
		internal string[] GetColumnNames()
		{
			string[] array = new string[this._columns.Length];
			for (int i = 0; i < this._columns.Length; i++)
			{
				array[i] = this._columns[i].ColumnName;
			}
			return array;
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x00013A44 File Offset: 0x00011C44
		internal IndexField[] GetIndexDesc()
		{
			IndexField[] array = new IndexField[this._columns.Length];
			for (int i = 0; i < this._columns.Length; i++)
			{
				array[i] = new IndexField(this._columns[i], false);
			}
			return array;
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x00013A88 File Offset: 0x00011C88
		internal object[] GetKeyValues(int record)
		{
			object[] array = new object[this._columns.Length];
			for (int i = 0; i < this._columns.Length; i++)
			{
				array[i] = this._columns[i][record];
			}
			return array;
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x00013AC8 File Offset: 0x00011CC8
		internal Index GetSortIndex()
		{
			return this.GetSortIndex(DataViewRowState.CurrentRows);
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x00013AD4 File Offset: 0x00011CD4
		internal Index GetSortIndex(DataViewRowState recordStates)
		{
			IndexField[] indexDesc = this.GetIndexDesc();
			return this._columns[0].Table.GetIndex(indexDesc, recordStates, null);
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x00013B00 File Offset: 0x00011D00
		internal bool RecordsEqual(int record1, int record2)
		{
			for (int i = 0; i < this._columns.Length; i++)
			{
				if (this._columns[i].Compare(record1, record2) != 0)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x00013B34 File Offset: 0x00011D34
		internal DataColumn[] ToArray()
		{
			DataColumn[] array = new DataColumn[this._columns.Length];
			for (int i = 0; i < this._columns.Length; i++)
			{
				array[i] = this._columns[i];
			}
			return array;
		}

		// Token: 0x040004E8 RID: 1256
		private const int maxColumns = 32;

		// Token: 0x040004E9 RID: 1257
		private readonly DataColumn[] _columns;
	}
}
