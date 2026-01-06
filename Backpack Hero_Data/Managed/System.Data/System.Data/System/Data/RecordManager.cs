using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;

namespace System.Data
{
	// Token: 0x020000CE RID: 206
	internal sealed class RecordManager
	{
		// Token: 0x06000BF6 RID: 3062 RVA: 0x00036274 File Offset: 0x00034474
		internal RecordManager(DataTable table)
		{
			if (table == null)
			{
				throw ExceptionBuilder.ArgumentNull("table");
			}
			this._table = table;
		}

		// Token: 0x06000BF7 RID: 3063 RVA: 0x000362A4 File Offset: 0x000344A4
		private void GrowRecordCapacity()
		{
			this.RecordCapacity = ((RecordManager.NewCapacity(this._recordCapacity) < this.NormalizedMinimumCapacity(this._minimumCapacity)) ? this.NormalizedMinimumCapacity(this._minimumCapacity) : RecordManager.NewCapacity(this._recordCapacity));
			DataRow[] array = this._table.NewRowArray(this._recordCapacity);
			if (this._rows != null)
			{
				Array.Copy(this._rows, 0, array, 0, Math.Min(this._lastFreeRecord, this._rows.Length));
			}
			this._rows = array;
		}

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x06000BF8 RID: 3064 RVA: 0x0003632B File Offset: 0x0003452B
		internal int LastFreeRecord
		{
			get
			{
				return this._lastFreeRecord;
			}
		}

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x06000BF9 RID: 3065 RVA: 0x00036333 File Offset: 0x00034533
		// (set) Token: 0x06000BFA RID: 3066 RVA: 0x0003633B File Offset: 0x0003453B
		internal int MinimumCapacity
		{
			get
			{
				return this._minimumCapacity;
			}
			set
			{
				if (this._minimumCapacity != value)
				{
					if (value < 0)
					{
						throw ExceptionBuilder.NegativeMinimumCapacity();
					}
					this._minimumCapacity = value;
				}
			}
		}

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x06000BFB RID: 3067 RVA: 0x00036357 File Offset: 0x00034557
		// (set) Token: 0x06000BFC RID: 3068 RVA: 0x00036360 File Offset: 0x00034560
		internal int RecordCapacity
		{
			get
			{
				return this._recordCapacity;
			}
			set
			{
				if (this._recordCapacity != value)
				{
					for (int i = 0; i < this._table.Columns.Count; i++)
					{
						this._table.Columns[i].SetCapacity(value);
					}
					this._recordCapacity = value;
				}
			}
		}

		// Token: 0x06000BFD RID: 3069 RVA: 0x000363AF File Offset: 0x000345AF
		internal static int NewCapacity(int capacity)
		{
			if (capacity >= 128)
			{
				return capacity + capacity;
			}
			return 128;
		}

		// Token: 0x06000BFE RID: 3070 RVA: 0x000363C2 File Offset: 0x000345C2
		private int NormalizedMinimumCapacity(int capacity)
		{
			if (capacity >= 1014)
			{
				return (capacity + 10 >> 10) + 1 << 10;
			}
			if (capacity >= 246)
			{
				return 1024;
			}
			if (capacity < 54)
			{
				return 64;
			}
			return 256;
		}

		// Token: 0x06000BFF RID: 3071 RVA: 0x000363F4 File Offset: 0x000345F4
		internal int NewRecordBase()
		{
			int num;
			if (this._freeRecordList.Count != 0)
			{
				num = this._freeRecordList[this._freeRecordList.Count - 1];
				this._freeRecordList.RemoveAt(this._freeRecordList.Count - 1);
			}
			else
			{
				if (this._lastFreeRecord >= this._recordCapacity)
				{
					this.GrowRecordCapacity();
				}
				num = this._lastFreeRecord;
				this._lastFreeRecord++;
			}
			return num;
		}

		// Token: 0x06000C00 RID: 3072 RVA: 0x0003646C File Offset: 0x0003466C
		internal void FreeRecord(ref int record)
		{
			if (-1 != record)
			{
				this[record] = null;
				int count = this._table._columnCollection.Count;
				for (int i = 0; i < count; i++)
				{
					this._table._columnCollection[i].FreeRecord(record);
				}
				if (this._lastFreeRecord == record + 1)
				{
					this._lastFreeRecord--;
				}
				else if (record < this._lastFreeRecord)
				{
					this._freeRecordList.Add(record);
				}
				record = -1;
			}
		}

		// Token: 0x06000C01 RID: 3073 RVA: 0x000364F4 File Offset: 0x000346F4
		internal void Clear(bool clearAll)
		{
			if (clearAll)
			{
				for (int i = 0; i < this._recordCapacity; i++)
				{
					this._rows[i] = null;
				}
				int count = this._table._columnCollection.Count;
				for (int j = 0; j < count; j++)
				{
					DataColumn dataColumn = this._table._columnCollection[j];
					for (int k = 0; k < this._recordCapacity; k++)
					{
						dataColumn.FreeRecord(k);
					}
				}
				this._lastFreeRecord = 0;
				this._freeRecordList.Clear();
				return;
			}
			this._freeRecordList.Capacity = this._freeRecordList.Count + this._table.Rows.Count;
			for (int l = 0; l < this._recordCapacity; l++)
			{
				if (this._rows[l] != null && this._rows[l].rowID != -1L)
				{
					int num = l;
					this.FreeRecord(ref num);
				}
			}
		}

		// Token: 0x17000223 RID: 547
		internal DataRow this[int record]
		{
			get
			{
				return this._rows[record];
			}
			set
			{
				this._rows[record] = value;
			}
		}

		// Token: 0x06000C04 RID: 3076 RVA: 0x000365F8 File Offset: 0x000347F8
		internal void SetKeyValues(int record, DataKey key, object[] keyValues)
		{
			for (int i = 0; i < keyValues.Length; i++)
			{
				key.ColumnsReference[i][record] = keyValues[i];
			}
		}

		// Token: 0x06000C05 RID: 3077 RVA: 0x00036625 File Offset: 0x00034825
		internal int ImportRecord(DataTable src, int record)
		{
			return this.CopyRecord(src, record, -1);
		}

		// Token: 0x06000C06 RID: 3078 RVA: 0x00036630 File Offset: 0x00034830
		internal int CopyRecord(DataTable src, int record, int copy)
		{
			if (record == -1)
			{
				return copy;
			}
			int num = -1;
			try
			{
				num = ((copy == -1) ? this._table.NewUninitializedRecord() : copy);
				int count = this._table.Columns.Count;
				for (int i = 0; i < count; i++)
				{
					DataColumn dataColumn = this._table.Columns[i];
					DataColumn dataColumn2 = src.Columns[dataColumn.ColumnName];
					if (dataColumn2 != null)
					{
						object obj = dataColumn2[record];
						ICloneable cloneable = obj as ICloneable;
						if (cloneable != null)
						{
							dataColumn[num] = cloneable.Clone();
						}
						else
						{
							dataColumn[num] = obj;
						}
					}
					else if (-1 == copy)
					{
						dataColumn.Init(num);
					}
				}
			}
			catch (Exception ex) when (ADP.IsCatchableOrSecurityExceptionType(ex))
			{
				if (-1 == copy)
				{
					this.FreeRecord(ref num);
				}
				throw;
			}
			return num;
		}

		// Token: 0x06000C07 RID: 3079 RVA: 0x00036714 File Offset: 0x00034914
		internal void SetRowCache(DataRow[] newRows)
		{
			this._rows = newRows;
			this._lastFreeRecord = this._rows.Length;
			this._recordCapacity = this._lastFreeRecord;
		}

		// Token: 0x06000C08 RID: 3080 RVA: 0x000094D4 File Offset: 0x000076D4
		[Conditional("DEBUG")]
		internal void VerifyRecord(int record)
		{
		}

		// Token: 0x06000C09 RID: 3081 RVA: 0x000094D4 File Offset: 0x000076D4
		[Conditional("DEBUG")]
		internal void VerifyRecord(int record, DataRow row)
		{
		}

		// Token: 0x040007C1 RID: 1985
		private readonly DataTable _table;

		// Token: 0x040007C2 RID: 1986
		private int _lastFreeRecord;

		// Token: 0x040007C3 RID: 1987
		private int _minimumCapacity = 50;

		// Token: 0x040007C4 RID: 1988
		private int _recordCapacity;

		// Token: 0x040007C5 RID: 1989
		private readonly List<int> _freeRecordList = new List<int>();

		// Token: 0x040007C6 RID: 1990
		private DataRow[] _rows;
	}
}
