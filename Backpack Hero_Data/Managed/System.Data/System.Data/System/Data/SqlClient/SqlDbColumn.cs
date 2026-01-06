using System;
using System.Data.Common;

namespace System.Data.SqlClient
{
	// Token: 0x020001A3 RID: 419
	internal class SqlDbColumn : DbColumn
	{
		// Token: 0x06001460 RID: 5216 RVA: 0x0006484A File Offset: 0x00062A4A
		internal SqlDbColumn(_SqlMetaData md)
		{
			this._metadata = md;
			this.Populate();
		}

		// Token: 0x06001461 RID: 5217 RVA: 0x00064860 File Offset: 0x00062A60
		private void Populate()
		{
			base.AllowDBNull = new bool?(this._metadata.isNullable);
			base.BaseCatalogName = this._metadata.catalogName;
			base.BaseColumnName = this._metadata.baseColumn;
			base.BaseSchemaName = this._metadata.schemaName;
			base.BaseServerName = this._metadata.serverName;
			base.BaseTableName = this._metadata.tableName;
			base.ColumnName = this._metadata.column;
			base.ColumnOrdinal = new int?(this._metadata.ordinal);
			base.ColumnSize = new int?((this._metadata.metaType.IsSizeInCharacters && this._metadata.length != int.MaxValue) ? (this._metadata.length / 2) : this._metadata.length);
			base.IsAutoIncrement = new bool?(this._metadata.isIdentity);
			base.IsIdentity = new bool?(this._metadata.isIdentity);
			base.IsLong = new bool?(this._metadata.metaType.IsLong);
			if (SqlDbType.Timestamp == this._metadata.type)
			{
				base.IsUnique = new bool?(true);
			}
			else
			{
				base.IsUnique = new bool?(false);
			}
			if (255 != this._metadata.precision)
			{
				base.NumericPrecision = new int?((int)this._metadata.precision);
			}
			else
			{
				base.NumericPrecision = new int?((int)this._metadata.metaType.Precision);
			}
			base.IsReadOnly = new bool?(this._metadata.updatability == 0);
			base.UdtAssemblyQualifiedName = this._metadata.udtAssemblyQualifiedName;
		}

		// Token: 0x170003B3 RID: 947
		// (set) Token: 0x06001462 RID: 5218 RVA: 0x00064A27 File Offset: 0x00062C27
		internal bool? SqlIsAliased
		{
			set
			{
				base.IsAliased = value;
			}
		}

		// Token: 0x170003B4 RID: 948
		// (set) Token: 0x06001463 RID: 5219 RVA: 0x00064A30 File Offset: 0x00062C30
		internal bool? SqlIsKey
		{
			set
			{
				base.IsKey = value;
			}
		}

		// Token: 0x170003B5 RID: 949
		// (set) Token: 0x06001464 RID: 5220 RVA: 0x00064A39 File Offset: 0x00062C39
		internal bool? SqlIsHidden
		{
			set
			{
				base.IsHidden = value;
			}
		}

		// Token: 0x170003B6 RID: 950
		// (set) Token: 0x06001465 RID: 5221 RVA: 0x00064A42 File Offset: 0x00062C42
		internal bool? SqlIsExpression
		{
			set
			{
				base.IsExpression = value;
			}
		}

		// Token: 0x170003B7 RID: 951
		// (set) Token: 0x06001466 RID: 5222 RVA: 0x00064A4B File Offset: 0x00062C4B
		internal Type SqlDataType
		{
			set
			{
				base.DataType = value;
			}
		}

		// Token: 0x170003B8 RID: 952
		// (set) Token: 0x06001467 RID: 5223 RVA: 0x00064A54 File Offset: 0x00062C54
		internal string SqlDataTypeName
		{
			set
			{
				base.DataTypeName = value;
			}
		}

		// Token: 0x170003B9 RID: 953
		// (set) Token: 0x06001468 RID: 5224 RVA: 0x00064A5D File Offset: 0x00062C5D
		internal int? SqlNumericScale
		{
			set
			{
				base.NumericScale = value;
			}
		}

		// Token: 0x04000D8F RID: 3471
		private readonly _SqlMetaData _metadata;
	}
}
