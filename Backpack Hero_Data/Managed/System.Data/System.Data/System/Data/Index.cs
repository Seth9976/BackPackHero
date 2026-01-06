using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;

namespace System.Data
{
	// Token: 0x020000D7 RID: 215
	internal sealed class Index
	{
		// Token: 0x06000C33 RID: 3123 RVA: 0x00038304 File Offset: 0x00036504
		public Index(DataTable table, IndexField[] indexFields, DataViewRowState recordStates, IFilter rowFilter)
			: this(table, indexFields, null, recordStates, rowFilter)
		{
		}

		// Token: 0x06000C34 RID: 3124 RVA: 0x00038312 File Offset: 0x00036512
		public Index(DataTable table, Comparison<DataRow> comparison, DataViewRowState recordStates, IFilter rowFilter)
			: this(table, Index.GetAllFields(table.Columns), comparison, recordStates, rowFilter)
		{
		}

		// Token: 0x06000C35 RID: 3125 RVA: 0x0003832C File Offset: 0x0003652C
		private static IndexField[] GetAllFields(DataColumnCollection columns)
		{
			IndexField[] array = new IndexField[columns.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = new IndexField(columns[i], false);
			}
			return array;
		}

		// Token: 0x06000C36 RID: 3126 RVA: 0x00038368 File Offset: 0x00036568
		private Index(DataTable table, IndexField[] indexFields, Comparison<DataRow> comparison, DataViewRowState recordStates, IFilter rowFilter)
		{
			DataCommonEventSource.Log.Trace<int, int, DataViewRowState>("<ds.Index.Index|API> {0}, table={1}, recordStates={2}", this.ObjectID, (table != null) ? table.ObjectID : 0, recordStates);
			if ((recordStates & ~(DataViewRowState.Unchanged | DataViewRowState.Added | DataViewRowState.Deleted | DataViewRowState.ModifiedCurrent | DataViewRowState.ModifiedOriginal)) != DataViewRowState.None)
			{
				throw ExceptionBuilder.RecordStateRange();
			}
			this._table = table;
			this._listeners = new Listeners<DataViewListener>(this.ObjectID, (DataViewListener listener) => listener != null);
			this._indexFields = indexFields;
			this._recordStates = recordStates;
			this._comparison = comparison;
			DataColumnCollection columns = table.Columns;
			this._isSharable = rowFilter == null && comparison == null;
			if (rowFilter != null)
			{
				this._rowFilter = new WeakReference(rowFilter);
				DataExpression dataExpression = rowFilter as DataExpression;
				if (dataExpression != null)
				{
					this._hasRemoteAggregate = dataExpression.HasRemoteAggregate();
				}
			}
			this.InitRecords(rowFilter);
		}

		// Token: 0x06000C37 RID: 3127 RVA: 0x00038450 File Offset: 0x00036650
		public bool Equal(IndexField[] indexDesc, DataViewRowState recordStates, IFilter rowFilter)
		{
			if (!this._isSharable || this._indexFields.Length != indexDesc.Length || this._recordStates != recordStates || rowFilter != null)
			{
				return false;
			}
			for (int i = 0; i < this._indexFields.Length; i++)
			{
				if (this._indexFields[i].Column != indexDesc[i].Column || this._indexFields[i].IsDescending != indexDesc[i].IsDescending)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x06000C38 RID: 3128 RVA: 0x000384D4 File Offset: 0x000366D4
		internal bool HasRemoteAggregate
		{
			get
			{
				return this._hasRemoteAggregate;
			}
		}

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x06000C39 RID: 3129 RVA: 0x000384DC File Offset: 0x000366DC
		internal int ObjectID
		{
			get
			{
				return this._objectID;
			}
		}

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x06000C3A RID: 3130 RVA: 0x000384E4 File Offset: 0x000366E4
		public DataViewRowState RecordStates
		{
			get
			{
				return this._recordStates;
			}
		}

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x06000C3B RID: 3131 RVA: 0x000384EC File Offset: 0x000366EC
		public IFilter RowFilter
		{
			get
			{
				return (IFilter)((this._rowFilter != null) ? this._rowFilter.Target : null);
			}
		}

		// Token: 0x06000C3C RID: 3132 RVA: 0x00038509 File Offset: 0x00036709
		public int GetRecord(int recordIndex)
		{
			return this._records[recordIndex];
		}

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x06000C3D RID: 3133 RVA: 0x00038517 File Offset: 0x00036717
		public bool HasDuplicates
		{
			get
			{
				return this._records.HasDuplicates;
			}
		}

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x06000C3E RID: 3134 RVA: 0x00038524 File Offset: 0x00036724
		public int RecordCount
		{
			get
			{
				return this._recordCount;
			}
		}

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x06000C3F RID: 3135 RVA: 0x0003852C File Offset: 0x0003672C
		public bool IsSharable
		{
			get
			{
				return this._isSharable;
			}
		}

		// Token: 0x06000C40 RID: 3136 RVA: 0x00038534 File Offset: 0x00036734
		private bool AcceptRecord(int record)
		{
			return this.AcceptRecord(record, this.RowFilter);
		}

		// Token: 0x06000C41 RID: 3137 RVA: 0x00038544 File Offset: 0x00036744
		private bool AcceptRecord(int record, IFilter filter)
		{
			DataCommonEventSource.Log.Trace<int, int>("<ds.Index.AcceptRecord|API> {0}, record={1}", this.ObjectID, record);
			if (filter == null)
			{
				return true;
			}
			DataRow dataRow = this._table._recordManager[record];
			if (dataRow == null)
			{
				return true;
			}
			DataRowVersion dataRowVersion = DataRowVersion.Default;
			if (dataRow._oldRecord == record)
			{
				dataRowVersion = DataRowVersion.Original;
			}
			else if (dataRow._newRecord == record)
			{
				dataRowVersion = DataRowVersion.Current;
			}
			else if (dataRow._tempRecord == record)
			{
				dataRowVersion = DataRowVersion.Proposed;
			}
			return filter.Invoke(dataRow, dataRowVersion);
		}

		// Token: 0x06000C42 RID: 3138 RVA: 0x000385C2 File Offset: 0x000367C2
		internal void ListChangedAdd(DataViewListener listener)
		{
			this._listeners.Add(listener);
		}

		// Token: 0x06000C43 RID: 3139 RVA: 0x000385D0 File Offset: 0x000367D0
		internal void ListChangedRemove(DataViewListener listener)
		{
			this._listeners.Remove(listener);
		}

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x06000C44 RID: 3140 RVA: 0x000385DE File Offset: 0x000367DE
		public int RefCount
		{
			get
			{
				return this._refCount;
			}
		}

		// Token: 0x06000C45 RID: 3141 RVA: 0x000385E8 File Offset: 0x000367E8
		public void AddRef()
		{
			DataCommonEventSource.Log.Trace<int>("<ds.Index.AddRef|API> {0}", this.ObjectID);
			this._table._indexesLock.EnterWriteLock();
			try
			{
				if (this._refCount == 0)
				{
					this._table.ShadowIndexCopy();
					this._table._indexes.Add(this);
				}
				this._refCount++;
			}
			finally
			{
				this._table._indexesLock.ExitWriteLock();
			}
		}

		// Token: 0x06000C46 RID: 3142 RVA: 0x00038670 File Offset: 0x00036870
		public int RemoveRef()
		{
			DataCommonEventSource.Log.Trace<int>("<ds.Index.RemoveRef|API> {0}", this.ObjectID);
			this._table._indexesLock.EnterWriteLock();
			int num2;
			try
			{
				int num = this._refCount - 1;
				this._refCount = num;
				num2 = num;
				if (this._refCount <= 0)
				{
					this._table.ShadowIndexCopy();
					this._table._indexes.Remove(this);
				}
			}
			finally
			{
				this._table._indexesLock.ExitWriteLock();
			}
			return num2;
		}

		// Token: 0x06000C47 RID: 3143 RVA: 0x00038700 File Offset: 0x00036900
		private void ApplyChangeAction(int record, int action, int changeRecord)
		{
			if (action != 0)
			{
				if (action > 0)
				{
					if (this.AcceptRecord(record))
					{
						this.InsertRecord(record, true);
						return;
					}
				}
				else
				{
					if (this._comparison != null && -1 != record)
					{
						this.DeleteRecord(this.GetIndex(record, changeRecord));
						return;
					}
					this.DeleteRecord(this.GetIndex(record));
				}
			}
		}

		// Token: 0x06000C48 RID: 3144 RVA: 0x0003874F File Offset: 0x0003694F
		public bool CheckUnique()
		{
			return !this.HasDuplicates;
		}

		// Token: 0x06000C49 RID: 3145 RVA: 0x0003875C File Offset: 0x0003695C
		private int CompareRecords(int record1, int record2)
		{
			if (this._comparison != null)
			{
				return this.CompareDataRows(record1, record2);
			}
			if (this._indexFields.Length != 0)
			{
				int i = 0;
				while (i < this._indexFields.Length)
				{
					int num = this._indexFields[i].Column.Compare(record1, record2);
					if (num != 0)
					{
						if (!this._indexFields[i].IsDescending)
						{
							return num;
						}
						return -num;
					}
					else
					{
						i++;
					}
				}
				return 0;
			}
			return this._table.Rows.IndexOf(this._table._recordManager[record1]).CompareTo(this._table.Rows.IndexOf(this._table._recordManager[record2]));
		}

		// Token: 0x06000C4A RID: 3146 RVA: 0x00038816 File Offset: 0x00036A16
		private int CompareDataRows(int record1, int record2)
		{
			return this._comparison(this._table._recordManager[record1], this._table._recordManager[record2]);
		}

		// Token: 0x06000C4B RID: 3147 RVA: 0x00038848 File Offset: 0x00036A48
		private int CompareDuplicateRecords(int record1, int record2)
		{
			if (this._table._recordManager[record1] == null)
			{
				if (this._table._recordManager[record2] != null)
				{
					return -1;
				}
				return 0;
			}
			else
			{
				if (this._table._recordManager[record2] == null)
				{
					return 1;
				}
				int num = this._table._recordManager[record1].rowID.CompareTo(this._table._recordManager[record2].rowID);
				if (num == 0 && record1 != record2)
				{
					num = ((int)this._table._recordManager[record1].GetRecordState(record1)).CompareTo((int)this._table._recordManager[record2].GetRecordState(record2));
				}
				return num;
			}
		}

		// Token: 0x06000C4C RID: 3148 RVA: 0x00038908 File Offset: 0x00036B08
		private int CompareRecordToKey(int record1, object[] vals)
		{
			int i = 0;
			while (i < this._indexFields.Length)
			{
				int num = this._indexFields[i].Column.CompareValueTo(record1, vals[i]);
				if (num != 0)
				{
					if (!this._indexFields[i].IsDescending)
					{
						return num;
					}
					return -num;
				}
				else
				{
					i++;
				}
			}
			return 0;
		}

		// Token: 0x06000C4D RID: 3149 RVA: 0x0003895F File Offset: 0x00036B5F
		public void DeleteRecordFromIndex(int recordIndex)
		{
			this.DeleteRecord(recordIndex, false);
		}

		// Token: 0x06000C4E RID: 3150 RVA: 0x00038969 File Offset: 0x00036B69
		private void DeleteRecord(int recordIndex)
		{
			this.DeleteRecord(recordIndex, true);
		}

		// Token: 0x06000C4F RID: 3151 RVA: 0x00038974 File Offset: 0x00036B74
		private void DeleteRecord(int recordIndex, bool fireEvent)
		{
			DataCommonEventSource.Log.Trace<int, int, bool>("<ds.Index.DeleteRecord|INFO> {0}, recordIndex={1}, fireEvent={2}", this.ObjectID, recordIndex, fireEvent);
			if (recordIndex >= 0)
			{
				this._recordCount--;
				int num = this._records.DeleteByIndex(recordIndex);
				this.MaintainDataView(ListChangedType.ItemDeleted, num, !fireEvent);
				if (fireEvent)
				{
					this.OnListChanged(ListChangedType.ItemDeleted, recordIndex);
				}
			}
		}

		// Token: 0x06000C50 RID: 3152 RVA: 0x000389CE File Offset: 0x00036BCE
		public RBTree<int>.RBTreeEnumerator GetEnumerator(int startIndex)
		{
			return new RBTree<int>.RBTreeEnumerator(this._records, startIndex);
		}

		// Token: 0x06000C51 RID: 3153 RVA: 0x000389DC File Offset: 0x00036BDC
		public int GetIndex(int record)
		{
			return this._records.GetIndexByKey(record);
		}

		// Token: 0x06000C52 RID: 3154 RVA: 0x000389EC File Offset: 0x00036BEC
		private int GetIndex(int record, int changeRecord)
		{
			DataRow dataRow = this._table._recordManager[record];
			int newRecord = dataRow._newRecord;
			int oldRecord = dataRow._oldRecord;
			int indexByKey;
			try
			{
				if (changeRecord != 1)
				{
					if (changeRecord == 2)
					{
						dataRow._oldRecord = record;
					}
				}
				else
				{
					dataRow._newRecord = record;
				}
				indexByKey = this._records.GetIndexByKey(record);
			}
			finally
			{
				if (changeRecord != 1)
				{
					if (changeRecord == 2)
					{
						dataRow._oldRecord = oldRecord;
					}
				}
				else
				{
					dataRow._newRecord = newRecord;
				}
			}
			return indexByKey;
		}

		// Token: 0x06000C53 RID: 3155 RVA: 0x00038A70 File Offset: 0x00036C70
		public object[] GetUniqueKeyValues()
		{
			if (this._indexFields == null || this._indexFields.Length == 0)
			{
				return Array.Empty<object>();
			}
			List<object[]> list = new List<object[]>();
			this.GetUniqueKeyValues(list, this._records.root);
			return list.ToArray();
		}

		// Token: 0x06000C54 RID: 3156 RVA: 0x00038AB4 File Offset: 0x00036CB4
		public int FindRecord(int record)
		{
			int num = this._records.Search(record);
			if (num != 0)
			{
				return this._records.GetIndexByNode(num);
			}
			return -1;
		}

		// Token: 0x06000C55 RID: 3157 RVA: 0x00038AE0 File Offset: 0x00036CE0
		public int FindRecordByKey(object key)
		{
			int num = this.FindNodeByKey(key);
			if (num != 0)
			{
				return this._records.GetIndexByNode(num);
			}
			return -1;
		}

		// Token: 0x06000C56 RID: 3158 RVA: 0x00038B08 File Offset: 0x00036D08
		public int FindRecordByKey(object[] key)
		{
			int num = this.FindNodeByKeys(key);
			if (num != 0)
			{
				return this._records.GetIndexByNode(num);
			}
			return -1;
		}

		// Token: 0x06000C57 RID: 3159 RVA: 0x00038B30 File Offset: 0x00036D30
		private int FindNodeByKey(object originalKey)
		{
			if (this._indexFields.Length != 1)
			{
				throw ExceptionBuilder.IndexKeyLength(this._indexFields.Length, 1);
			}
			int num = this._records.root;
			if (num != 0)
			{
				DataColumn column = this._indexFields[0].Column;
				object obj = column.ConvertValue(originalKey);
				num = this._records.root;
				if (this._indexFields[0].IsDescending)
				{
					while (num != 0)
					{
						int num2 = column.CompareValueTo(this._records.Key(num), obj);
						if (num2 == 0)
						{
							break;
						}
						if (num2 < 0)
						{
							num = this._records.Left(num);
						}
						else
						{
							num = this._records.Right(num);
						}
					}
				}
				else
				{
					while (num != 0)
					{
						int num2 = column.CompareValueTo(this._records.Key(num), obj);
						if (num2 == 0)
						{
							break;
						}
						if (num2 > 0)
						{
							num = this._records.Left(num);
						}
						else
						{
							num = this._records.Right(num);
						}
					}
				}
			}
			return num;
		}

		// Token: 0x06000C58 RID: 3160 RVA: 0x00038C1C File Offset: 0x00036E1C
		private int FindNodeByKeys(object[] originalKey)
		{
			int num = ((originalKey != null) ? originalKey.Length : 0);
			if (num == 0 || this._indexFields.Length != num)
			{
				throw ExceptionBuilder.IndexKeyLength(this._indexFields.Length, num);
			}
			int num2 = this._records.root;
			if (num2 != 0)
			{
				object[] array = new object[originalKey.Length];
				for (int i = 0; i < originalKey.Length; i++)
				{
					array[i] = this._indexFields[i].Column.ConvertValue(originalKey[i]);
				}
				num2 = this._records.root;
				while (num2 != 0)
				{
					num = this.CompareRecordToKey(this._records.Key(num2), array);
					if (num == 0)
					{
						break;
					}
					if (num > 0)
					{
						num2 = this._records.Left(num2);
					}
					else
					{
						num2 = this._records.Right(num2);
					}
				}
			}
			return num2;
		}

		// Token: 0x06000C59 RID: 3161 RVA: 0x00038CDC File Offset: 0x00036EDC
		private int FindNodeByKeyRecord(int record)
		{
			int num = this._records.root;
			if (num != 0)
			{
				num = this._records.root;
				while (num != 0)
				{
					int num2 = this.CompareRecords(this._records.Key(num), record);
					if (num2 == 0)
					{
						break;
					}
					if (num2 > 0)
					{
						num = this._records.Left(num);
					}
					else
					{
						num = this._records.Right(num);
					}
				}
			}
			return num;
		}

		// Token: 0x06000C5A RID: 3162 RVA: 0x00038D44 File Offset: 0x00036F44
		private Range GetRangeFromNode(int nodeId)
		{
			if (nodeId == 0)
			{
				return default(Range);
			}
			int indexByNode = this._records.GetIndexByNode(nodeId);
			if (this._records.Next(nodeId) == 0)
			{
				return new Range(indexByNode, indexByNode);
			}
			int num = this._records.SubTreeSize(this._records.Next(nodeId));
			return new Range(indexByNode, indexByNode + num - 1);
		}

		// Token: 0x06000C5B RID: 3163 RVA: 0x00038DA4 File Offset: 0x00036FA4
		public Range FindRecords(object key)
		{
			int num = this.FindNodeByKey(key);
			return this.GetRangeFromNode(num);
		}

		// Token: 0x06000C5C RID: 3164 RVA: 0x00038DC0 File Offset: 0x00036FC0
		public Range FindRecords(object[] key)
		{
			int num = this.FindNodeByKeys(key);
			return this.GetRangeFromNode(num);
		}

		// Token: 0x06000C5D RID: 3165 RVA: 0x00038DDC File Offset: 0x00036FDC
		internal void FireResetEvent()
		{
			DataCommonEventSource.Log.Trace<int>("<ds.Index.FireResetEvent|API> {0}", this.ObjectID);
			if (this.DoListChanged)
			{
				this.OnListChanged(DataView.s_resetEventArgs);
			}
		}

		// Token: 0x06000C5E RID: 3166 RVA: 0x00038E08 File Offset: 0x00037008
		private int GetChangeAction(DataViewRowState oldState, DataViewRowState newState)
		{
			int num = (((this._recordStates & oldState) == DataViewRowState.None) ? 0 : 1);
			return (((this._recordStates & newState) == DataViewRowState.None) ? 0 : 1) - num;
		}

		// Token: 0x06000C5F RID: 3167 RVA: 0x00038E34 File Offset: 0x00037034
		private static int GetReplaceAction(DataViewRowState oldState)
		{
			if ((DataViewRowState.CurrentRows & oldState) != DataViewRowState.None)
			{
				return 1;
			}
			if ((DataViewRowState.OriginalRows & oldState) == DataViewRowState.None)
			{
				return 0;
			}
			return 2;
		}

		// Token: 0x06000C60 RID: 3168 RVA: 0x00038E47 File Offset: 0x00037047
		public DataRow GetRow(int i)
		{
			return this._table._recordManager[this.GetRecord(i)];
		}

		// Token: 0x06000C61 RID: 3169 RVA: 0x00038E60 File Offset: 0x00037060
		public DataRow[] GetRows(object[] values)
		{
			return this.GetRows(this.FindRecords(values));
		}

		// Token: 0x06000C62 RID: 3170 RVA: 0x00038E70 File Offset: 0x00037070
		public DataRow[] GetRows(Range range)
		{
			DataRow[] array = this._table.NewRowArray(range.Count);
			if (array.Length != 0)
			{
				RBTree<int>.RBTreeEnumerator enumerator = this.GetEnumerator(range.Min);
				int num = 0;
				while (num < array.Length && enumerator.MoveNext())
				{
					array[num] = this._table._recordManager[enumerator.Current];
					num++;
				}
			}
			return array;
		}

		// Token: 0x06000C63 RID: 3171 RVA: 0x00038ED4 File Offset: 0x000370D4
		private void InitRecords(IFilter filter)
		{
			DataViewRowState recordStates = this._recordStates;
			bool flag = this._indexFields.Length == 0;
			this._records = new Index.IndexTree(this);
			this._recordCount = 0;
			foreach (object obj in this._table.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				int num = -1;
				if (dataRow._oldRecord == dataRow._newRecord)
				{
					if ((recordStates & DataViewRowState.Unchanged) != DataViewRowState.None)
					{
						num = dataRow._oldRecord;
					}
				}
				else if (dataRow._oldRecord == -1)
				{
					if ((recordStates & DataViewRowState.Added) != DataViewRowState.None)
					{
						num = dataRow._newRecord;
					}
				}
				else if (dataRow._newRecord == -1)
				{
					if ((recordStates & DataViewRowState.Deleted) != DataViewRowState.None)
					{
						num = dataRow._oldRecord;
					}
				}
				else if ((recordStates & DataViewRowState.ModifiedCurrent) != DataViewRowState.None)
				{
					num = dataRow._newRecord;
				}
				else if ((recordStates & DataViewRowState.ModifiedOriginal) != DataViewRowState.None)
				{
					num = dataRow._oldRecord;
				}
				if (num != -1 && this.AcceptRecord(num, filter))
				{
					this._records.InsertAt(-1, num, flag);
					this._recordCount++;
				}
			}
		}

		// Token: 0x06000C64 RID: 3172 RVA: 0x00038FF8 File Offset: 0x000371F8
		public int InsertRecordToIndex(int record)
		{
			int num = -1;
			if (this.AcceptRecord(record))
			{
				num = this.InsertRecord(record, false);
			}
			return num;
		}

		// Token: 0x06000C65 RID: 3173 RVA: 0x0003901C File Offset: 0x0003721C
		private int InsertRecord(int record, bool fireEvent)
		{
			DataCommonEventSource.Log.Trace<int, int, bool>("<ds.Index.InsertRecord|INFO> {0}, record={1}, fireEvent={2}", this.ObjectID, record, fireEvent);
			bool flag = false;
			if (this._indexFields.Length == 0 && this._table != null)
			{
				DataRow dataRow = this._table._recordManager[record];
				flag = this._table.Rows.IndexOf(dataRow) + 1 == this._table.Rows.Count;
			}
			int num = this._records.InsertAt(-1, record, flag);
			this._recordCount++;
			this.MaintainDataView(ListChangedType.ItemAdded, record, !fireEvent);
			if (fireEvent)
			{
				if (this.DoListChanged)
				{
					this.OnListChanged(ListChangedType.ItemAdded, this._records.GetIndexByNode(num));
				}
				return 0;
			}
			return this._records.GetIndexByNode(num);
		}

		// Token: 0x06000C66 RID: 3174 RVA: 0x000390E0 File Offset: 0x000372E0
		public bool IsKeyInIndex(object key)
		{
			int num = this.FindNodeByKey(key);
			return num != 0;
		}

		// Token: 0x06000C67 RID: 3175 RVA: 0x000390FC File Offset: 0x000372FC
		public bool IsKeyInIndex(object[] key)
		{
			int num = this.FindNodeByKeys(key);
			return num != 0;
		}

		// Token: 0x06000C68 RID: 3176 RVA: 0x00039118 File Offset: 0x00037318
		public bool IsKeyRecordInIndex(int record)
		{
			int num = this.FindNodeByKeyRecord(record);
			return num != 0;
		}

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x06000C69 RID: 3177 RVA: 0x00039131 File Offset: 0x00037331
		private bool DoListChanged
		{
			get
			{
				return !this._suspendEvents && this._listeners.HasListeners && !this._table.AreIndexEventsSuspended;
			}
		}

		// Token: 0x06000C6A RID: 3178 RVA: 0x00039158 File Offset: 0x00037358
		private void OnListChanged(ListChangedType changedType, int newIndex, int oldIndex)
		{
			if (this.DoListChanged)
			{
				this.OnListChanged(new ListChangedEventArgs(changedType, newIndex, oldIndex));
			}
		}

		// Token: 0x06000C6B RID: 3179 RVA: 0x00039170 File Offset: 0x00037370
		private void OnListChanged(ListChangedType changedType, int index)
		{
			if (this.DoListChanged)
			{
				this.OnListChanged(new ListChangedEventArgs(changedType, index));
			}
		}

		// Token: 0x06000C6C RID: 3180 RVA: 0x00039188 File Offset: 0x00037388
		private void OnListChanged(ListChangedEventArgs e)
		{
			DataCommonEventSource.Log.Trace<int>("<ds.Index.OnListChanged|INFO> {0}", this.ObjectID);
			this._listeners.Notify<ListChangedEventArgs, bool, bool>(e, false, false, delegate(DataViewListener listener, ListChangedEventArgs args, bool arg2, bool arg3)
			{
				listener.IndexListChanged(args);
			});
		}

		// Token: 0x06000C6D RID: 3181 RVA: 0x000391D8 File Offset: 0x000373D8
		private void MaintainDataView(ListChangedType changedType, int record, bool trackAddRemove)
		{
			this._listeners.Notify<ListChangedType, DataRow, bool>(changedType, (0 <= record) ? this._table._recordManager[record] : null, trackAddRemove, delegate(DataViewListener listener, ListChangedType type, DataRow row, bool track)
			{
				listener.MaintainDataView(changedType, row, track);
			});
		}

		// Token: 0x06000C6E RID: 3182 RVA: 0x00039228 File Offset: 0x00037428
		public void Reset()
		{
			DataCommonEventSource.Log.Trace<int>("<ds.Index.Reset|API> {0}", this.ObjectID);
			this.InitRecords(this.RowFilter);
			this.MaintainDataView(ListChangedType.Reset, -1, false);
			this.FireResetEvent();
		}

		// Token: 0x06000C6F RID: 3183 RVA: 0x0003925C File Offset: 0x0003745C
		public void RecordChanged(int record)
		{
			DataCommonEventSource.Log.Trace<int, int>("<ds.Index.RecordChanged|API> {0}, record={1}", this.ObjectID, record);
			if (this.DoListChanged)
			{
				int index = this.GetIndex(record);
				if (index >= 0)
				{
					this.OnListChanged(ListChangedType.ItemChanged, index);
				}
			}
		}

		// Token: 0x06000C70 RID: 3184 RVA: 0x0003929C File Offset: 0x0003749C
		public void RecordChanged(int oldIndex, int newIndex)
		{
			DataCommonEventSource.Log.Trace<int, int, int>("<ds.Index.RecordChanged|API> {0}, oldIndex={1}, newIndex={2}", this.ObjectID, oldIndex, newIndex);
			if (oldIndex > -1 || newIndex > -1)
			{
				if (oldIndex == newIndex)
				{
					this.OnListChanged(ListChangedType.ItemChanged, newIndex, oldIndex);
					return;
				}
				if (oldIndex == -1)
				{
					this.OnListChanged(ListChangedType.ItemAdded, newIndex, oldIndex);
					return;
				}
				if (newIndex == -1)
				{
					this.OnListChanged(ListChangedType.ItemDeleted, oldIndex);
					return;
				}
				this.OnListChanged(ListChangedType.ItemMoved, newIndex, oldIndex);
			}
		}

		// Token: 0x06000C71 RID: 3185 RVA: 0x000392FC File Offset: 0x000374FC
		public void RecordStateChanged(int record, DataViewRowState oldState, DataViewRowState newState)
		{
			DataCommonEventSource.Log.Trace<int, int, DataViewRowState, DataViewRowState>("<ds.Index.RecordStateChanged|API> {0}, record={1}, oldState={2}, newState={3}", this.ObjectID, record, oldState, newState);
			int changeAction = this.GetChangeAction(oldState, newState);
			this.ApplyChangeAction(record, changeAction, Index.GetReplaceAction(oldState));
		}

		// Token: 0x06000C72 RID: 3186 RVA: 0x00039338 File Offset: 0x00037538
		public void RecordStateChanged(int oldRecord, DataViewRowState oldOldState, DataViewRowState oldNewState, int newRecord, DataViewRowState newOldState, DataViewRowState newNewState)
		{
			DataCommonEventSource.Log.Trace<int, int, DataViewRowState, DataViewRowState, int, DataViewRowState, DataViewRowState>("<ds.Index.RecordStateChanged|API> {0}, oldRecord={1}, oldOldState={2}, oldNewState={3}, newRecord={4}, newOldState={5}, newNewState={6}", this.ObjectID, oldRecord, oldOldState, oldNewState, newRecord, newOldState, newNewState);
			int changeAction = this.GetChangeAction(oldOldState, oldNewState);
			int changeAction2 = this.GetChangeAction(newOldState, newNewState);
			if (changeAction != -1 || changeAction2 != 1 || !this.AcceptRecord(newRecord))
			{
				this.ApplyChangeAction(oldRecord, changeAction, Index.GetReplaceAction(oldOldState));
				this.ApplyChangeAction(newRecord, changeAction2, Index.GetReplaceAction(newOldState));
				return;
			}
			int num;
			if (this._comparison != null && changeAction < 0)
			{
				num = this.GetIndex(oldRecord, Index.GetReplaceAction(oldOldState));
			}
			else
			{
				num = this.GetIndex(oldRecord);
			}
			if (this._comparison == null && num != -1 && this.CompareRecords(oldRecord, newRecord) == 0)
			{
				this._records.UpdateNodeKey(oldRecord, newRecord);
				int index = this.GetIndex(newRecord);
				this.OnListChanged(ListChangedType.ItemChanged, index, index);
				return;
			}
			this._suspendEvents = true;
			if (num != -1)
			{
				this._records.DeleteByIndex(num);
				this._recordCount--;
			}
			this._records.Insert(newRecord);
			this._recordCount++;
			this._suspendEvents = false;
			int index2 = this.GetIndex(newRecord);
			if (num == index2)
			{
				this.OnListChanged(ListChangedType.ItemChanged, index2, num);
				return;
			}
			if (num == -1)
			{
				this.MaintainDataView(ListChangedType.ItemAdded, newRecord, false);
				this.OnListChanged(ListChangedType.ItemAdded, this.GetIndex(newRecord));
				return;
			}
			this.OnListChanged(ListChangedType.ItemMoved, index2, num);
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x06000C73 RID: 3187 RVA: 0x00039498 File Offset: 0x00037698
		internal DataTable Table
		{
			get
			{
				return this._table;
			}
		}

		// Token: 0x06000C74 RID: 3188 RVA: 0x000394A0 File Offset: 0x000376A0
		private void GetUniqueKeyValues(List<object[]> list, int curNodeId)
		{
			if (curNodeId != 0)
			{
				this.GetUniqueKeyValues(list, this._records.Left(curNodeId));
				int num = this._records.Key(curNodeId);
				object[] array = new object[this._indexFields.Length];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = this._indexFields[i].Column[num];
				}
				list.Add(array);
				this.GetUniqueKeyValues(list, this._records.Right(curNodeId));
			}
		}

		// Token: 0x06000C75 RID: 3189 RVA: 0x00039520 File Offset: 0x00037720
		internal static int IndexOfReference<T>(List<T> list, T item) where T : class
		{
			if (list != null)
			{
				for (int i = 0; i < list.Count; i++)
				{
					if (list[i] == item)
					{
						return i;
					}
				}
			}
			return -1;
		}

		// Token: 0x06000C76 RID: 3190 RVA: 0x00039558 File Offset: 0x00037758
		internal static bool ContainsReference<T>(List<T> list, T item) where T : class
		{
			return 0 <= Index.IndexOfReference<T>(list, item);
		}

		// Token: 0x040007E8 RID: 2024
		private const int DoNotReplaceCompareRecord = 0;

		// Token: 0x040007E9 RID: 2025
		private const int ReplaceNewRecordForCompare = 1;

		// Token: 0x040007EA RID: 2026
		private const int ReplaceOldRecordForCompare = 2;

		// Token: 0x040007EB RID: 2027
		private readonly DataTable _table;

		// Token: 0x040007EC RID: 2028
		internal readonly IndexField[] _indexFields;

		// Token: 0x040007ED RID: 2029
		private readonly Comparison<DataRow> _comparison;

		// Token: 0x040007EE RID: 2030
		private readonly DataViewRowState _recordStates;

		// Token: 0x040007EF RID: 2031
		private WeakReference _rowFilter;

		// Token: 0x040007F0 RID: 2032
		private Index.IndexTree _records;

		// Token: 0x040007F1 RID: 2033
		private int _recordCount;

		// Token: 0x040007F2 RID: 2034
		private int _refCount;

		// Token: 0x040007F3 RID: 2035
		private Listeners<DataViewListener> _listeners;

		// Token: 0x040007F4 RID: 2036
		private bool _suspendEvents;

		// Token: 0x040007F5 RID: 2037
		private readonly bool _isSharable;

		// Token: 0x040007F6 RID: 2038
		private readonly bool _hasRemoteAggregate;

		// Token: 0x040007F7 RID: 2039
		internal const int MaskBits = 2147483647;

		// Token: 0x040007F8 RID: 2040
		private static int s_objectTypeCount;

		// Token: 0x040007F9 RID: 2041
		private readonly int _objectID = Interlocked.Increment(ref Index.s_objectTypeCount);

		// Token: 0x020000D8 RID: 216
		private sealed class IndexTree : RBTree<int>
		{
			// Token: 0x06000C77 RID: 3191 RVA: 0x00039567 File Offset: 0x00037767
			internal IndexTree(Index index)
				: base(TreeAccessMethod.KEY_SEARCH_AND_INDEX)
			{
				this._index = index;
			}

			// Token: 0x06000C78 RID: 3192 RVA: 0x00039577 File Offset: 0x00037777
			protected override int CompareNode(int record1, int record2)
			{
				return this._index.CompareRecords(record1, record2);
			}

			// Token: 0x06000C79 RID: 3193 RVA: 0x00039586 File Offset: 0x00037786
			protected override int CompareSateliteTreeNode(int record1, int record2)
			{
				return this._index.CompareDuplicateRecords(record1, record2);
			}

			// Token: 0x040007FA RID: 2042
			private readonly Index _index;
		}
	}
}
