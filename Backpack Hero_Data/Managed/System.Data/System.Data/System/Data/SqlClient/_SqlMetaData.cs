using System;

namespace System.Data.SqlClient
{
	// Token: 0x02000213 RID: 531
	internal sealed class _SqlMetaData : SqlMetaDataPriv
	{
		// Token: 0x060018CD RID: 6349 RVA: 0x0007D661 File Offset: 0x0007B861
		internal _SqlMetaData(int ordinal)
		{
			this.ordinal = ordinal;
		}

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x060018CE RID: 6350 RVA: 0x0007D670 File Offset: 0x0007B870
		internal string serverName
		{
			get
			{
				return this.multiPartTableName.ServerName;
			}
		}

		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x060018CF RID: 6351 RVA: 0x0007D67D File Offset: 0x0007B87D
		internal string catalogName
		{
			get
			{
				return this.multiPartTableName.CatalogName;
			}
		}

		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x060018D0 RID: 6352 RVA: 0x0007D68A File Offset: 0x0007B88A
		internal string schemaName
		{
			get
			{
				return this.multiPartTableName.SchemaName;
			}
		}

		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x060018D1 RID: 6353 RVA: 0x0007D697 File Offset: 0x0007B897
		internal string tableName
		{
			get
			{
				return this.multiPartTableName.TableName;
			}
		}

		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x060018D2 RID: 6354 RVA: 0x0007D6A4 File Offset: 0x0007B8A4
		internal bool IsNewKatmaiDateTimeType
		{
			get
			{
				return SqlDbType.Date == this.type || SqlDbType.Time == this.type || SqlDbType.DateTime2 == this.type || SqlDbType.DateTimeOffset == this.type;
			}
		}

		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x060018D3 RID: 6355 RVA: 0x0007D6D0 File Offset: 0x0007B8D0
		internal bool IsLargeUdt
		{
			get
			{
				return this.type == SqlDbType.Udt && this.length == int.MaxValue;
			}
		}

		// Token: 0x060018D4 RID: 6356 RVA: 0x0007D6EC File Offset: 0x0007B8EC
		public object Clone()
		{
			_SqlMetaData sqlMetaData = new _SqlMetaData(this.ordinal);
			sqlMetaData.CopyFrom(this);
			sqlMetaData.column = this.column;
			sqlMetaData.baseColumn = this.baseColumn;
			sqlMetaData.multiPartTableName = this.multiPartTableName;
			sqlMetaData.updatability = this.updatability;
			sqlMetaData.tableNum = this.tableNum;
			sqlMetaData.isDifferentName = this.isDifferentName;
			sqlMetaData.isKey = this.isKey;
			sqlMetaData.isHidden = this.isHidden;
			sqlMetaData.isExpression = this.isExpression;
			sqlMetaData.isIdentity = this.isIdentity;
			sqlMetaData.isColumnSet = this.isColumnSet;
			sqlMetaData.op = this.op;
			sqlMetaData.operand = this.operand;
			return sqlMetaData;
		}

		// Token: 0x040011CD RID: 4557
		internal string column;

		// Token: 0x040011CE RID: 4558
		internal string baseColumn;

		// Token: 0x040011CF RID: 4559
		internal MultiPartTableName multiPartTableName;

		// Token: 0x040011D0 RID: 4560
		internal readonly int ordinal;

		// Token: 0x040011D1 RID: 4561
		internal byte updatability;

		// Token: 0x040011D2 RID: 4562
		internal byte tableNum;

		// Token: 0x040011D3 RID: 4563
		internal bool isDifferentName;

		// Token: 0x040011D4 RID: 4564
		internal bool isKey;

		// Token: 0x040011D5 RID: 4565
		internal bool isHidden;

		// Token: 0x040011D6 RID: 4566
		internal bool isExpression;

		// Token: 0x040011D7 RID: 4567
		internal bool isIdentity;

		// Token: 0x040011D8 RID: 4568
		internal bool isColumnSet;

		// Token: 0x040011D9 RID: 4569
		internal byte op;

		// Token: 0x040011DA RID: 4570
		internal ushort operand;
	}
}
