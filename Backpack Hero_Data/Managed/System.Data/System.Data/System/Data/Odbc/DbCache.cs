using System;

namespace System.Data.Odbc
{
	// Token: 0x02000258 RID: 600
	internal sealed class DbCache
	{
		// Token: 0x06001B46 RID: 6982 RVA: 0x00087970 File Offset: 0x00085B70
		internal DbCache(OdbcDataReader record, int count)
		{
			this._count = count;
			this._record = record;
			this._randomaccess = !record.IsBehavior(CommandBehavior.SequentialAccess);
			this._values = new object[count];
			this._isBadValue = new bool[count];
		}

		// Token: 0x170004E8 RID: 1256
		internal object this[int i]
		{
			get
			{
				if (this._isBadValue[i])
				{
					OverflowException ex = (OverflowException)this.Values[i];
					throw new OverflowException(ex.Message, ex);
				}
				return this.Values[i];
			}
			set
			{
				this.Values[i] = value;
				this._isBadValue[i] = false;
			}
		}

		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x06001B49 RID: 6985 RVA: 0x00087A12 File Offset: 0x00085C12
		internal int Count
		{
			get
			{
				return this._count;
			}
		}

		// Token: 0x06001B4A RID: 6986 RVA: 0x00087A1A File Offset: 0x00085C1A
		internal void InvalidateValue(int i)
		{
			this._isBadValue[i] = true;
		}

		// Token: 0x170004EA RID: 1258
		// (get) Token: 0x06001B4B RID: 6987 RVA: 0x00087A25 File Offset: 0x00085C25
		internal object[] Values
		{
			get
			{
				return this._values;
			}
		}

		// Token: 0x06001B4C RID: 6988 RVA: 0x00087A30 File Offset: 0x00085C30
		internal object AccessIndex(int i)
		{
			object[] values = this.Values;
			if (this._randomaccess)
			{
				for (int j = 0; j < i; j++)
				{
					if (values[j] == null)
					{
						values[j] = this._record.GetValue(j);
					}
				}
			}
			return values[i];
		}

		// Token: 0x06001B4D RID: 6989 RVA: 0x00087A6F File Offset: 0x00085C6F
		internal DbSchemaInfo GetSchema(int i)
		{
			if (this._schema == null)
			{
				this._schema = new DbSchemaInfo[this.Count];
			}
			if (this._schema[i] == null)
			{
				this._schema[i] = new DbSchemaInfo();
			}
			return this._schema[i];
		}

		// Token: 0x06001B4E RID: 6990 RVA: 0x00087AAC File Offset: 0x00085CAC
		internal void FlushValues()
		{
			int num = this._values.Length;
			for (int i = 0; i < num; i++)
			{
				this._values[i] = null;
			}
		}

		// Token: 0x0400139A RID: 5018
		private bool[] _isBadValue;

		// Token: 0x0400139B RID: 5019
		private DbSchemaInfo[] _schema;

		// Token: 0x0400139C RID: 5020
		private object[] _values;

		// Token: 0x0400139D RID: 5021
		private OdbcDataReader _record;

		// Token: 0x0400139E RID: 5022
		internal int _count;

		// Token: 0x0400139F RID: 5023
		internal bool _randomaccess = true;
	}
}
