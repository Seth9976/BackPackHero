using System;

namespace System.Data
{
	// Token: 0x02000050 RID: 80
	internal sealed class DataError
	{
		// Token: 0x060003BD RID: 957 RVA: 0x00011EC3 File Offset: 0x000100C3
		internal DataError()
		{
		}

		// Token: 0x060003BE RID: 958 RVA: 0x00011ED6 File Offset: 0x000100D6
		internal DataError(string rowError)
		{
			this.SetText(rowError);
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060003BF RID: 959 RVA: 0x00011EF0 File Offset: 0x000100F0
		// (set) Token: 0x060003C0 RID: 960 RVA: 0x00011EF8 File Offset: 0x000100F8
		internal string Text
		{
			get
			{
				return this._rowError;
			}
			set
			{
				this.SetText(value);
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060003C1 RID: 961 RVA: 0x00011F01 File Offset: 0x00010101
		internal bool HasErrors
		{
			get
			{
				return this._rowError.Length != 0 || this._count != 0;
			}
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x00011F1C File Offset: 0x0001011C
		internal void SetColumnError(DataColumn column, string error)
		{
			if (error == null || error.Length == 0)
			{
				this.Clear(column);
				return;
			}
			if (this._errorList == null)
			{
				this._errorList = new DataError.ColumnError[1];
			}
			int num = this.IndexOf(column);
			this._errorList[num]._column = column;
			this._errorList[num]._error = error;
			column._errors++;
			if (num == this._count)
			{
				this._count++;
			}
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x00011FA4 File Offset: 0x000101A4
		internal string GetColumnError(DataColumn column)
		{
			for (int i = 0; i < this._count; i++)
			{
				if (this._errorList[i]._column == column)
				{
					return this._errorList[i]._error;
				}
			}
			return string.Empty;
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x00011FF0 File Offset: 0x000101F0
		internal void Clear(DataColumn column)
		{
			if (this._count == 0)
			{
				return;
			}
			for (int i = 0; i < this._count; i++)
			{
				if (this._errorList[i]._column == column)
				{
					Array.Copy(this._errorList, i + 1, this._errorList, i, this._count - i - 1);
					this._count--;
					column._errors--;
				}
			}
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x00012068 File Offset: 0x00010268
		internal void Clear()
		{
			for (int i = 0; i < this._count; i++)
			{
				this._errorList[i]._column._errors--;
			}
			this._count = 0;
			this._rowError = string.Empty;
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x000120B8 File Offset: 0x000102B8
		internal DataColumn[] GetColumnsInError()
		{
			DataColumn[] array = new DataColumn[this._count];
			for (int i = 0; i < this._count; i++)
			{
				array[i] = this._errorList[i]._column;
			}
			return array;
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x000120F7 File Offset: 0x000102F7
		private void SetText(string errorText)
		{
			if (errorText == null)
			{
				errorText = string.Empty;
			}
			this._rowError = errorText;
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x0001210C File Offset: 0x0001030C
		internal int IndexOf(DataColumn column)
		{
			for (int i = 0; i < this._count; i++)
			{
				if (this._errorList[i]._column == column)
				{
					return i;
				}
			}
			if (this._count >= this._errorList.Length)
			{
				DataError.ColumnError[] array = new DataError.ColumnError[Math.Min(this._count * 2, column.Table.Columns.Count)];
				Array.Copy(this._errorList, 0, array, 0, this._count);
				this._errorList = array;
			}
			return this._count;
		}

		// Token: 0x040004E2 RID: 1250
		private string _rowError = string.Empty;

		// Token: 0x040004E3 RID: 1251
		private int _count;

		// Token: 0x040004E4 RID: 1252
		private DataError.ColumnError[] _errorList;

		// Token: 0x040004E5 RID: 1253
		internal const int initialCapacity = 1;

		// Token: 0x02000051 RID: 81
		internal struct ColumnError
		{
			// Token: 0x040004E6 RID: 1254
			internal DataColumn _column;

			// Token: 0x040004E7 RID: 1255
			internal string _error;
		}
	}
}
