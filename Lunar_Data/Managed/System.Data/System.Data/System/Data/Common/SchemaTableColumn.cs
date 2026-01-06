using System;

namespace System.Data.Common
{
	/// <summary>Describes the column metadata of the schema for a database table.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000369 RID: 873
	public static class SchemaTableColumn
	{
		/// <summary>Specifies the name of the column in the schema table.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040019AA RID: 6570
		public static readonly string ColumnName = "ColumnName";

		/// <summary>Specifies the ordinal of the column.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040019AB RID: 6571
		public static readonly string ColumnOrdinal = "ColumnOrdinal";

		/// <summary>Specifies the size of the column.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040019AC RID: 6572
		public static readonly string ColumnSize = "ColumnSize";

		/// <summary>Specifies the precision of the column data, if the data is numeric.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040019AD RID: 6573
		public static readonly string NumericPrecision = "NumericPrecision";

		/// <summary>Specifies the scale of the column data, if the data is numeric.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040019AE RID: 6574
		public static readonly string NumericScale = "NumericScale";

		/// <summary>Specifies the type of data in the column.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040019AF RID: 6575
		public static readonly string DataType = "DataType";

		/// <summary>Specifies the provider-specific data type of the column.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040019B0 RID: 6576
		public static readonly string ProviderType = "ProviderType";

		/// <summary>Specifies the non-versioned provider-specific data type of the column.</summary>
		// Token: 0x040019B1 RID: 6577
		public static readonly string NonVersionedProviderType = "NonVersionedProviderType";

		/// <summary>Specifies whether this column contains long data.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040019B2 RID: 6578
		public static readonly string IsLong = "IsLong";

		/// <summary>Specifies whether value DBNull is allowed.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040019B3 RID: 6579
		public static readonly string AllowDBNull = "AllowDBNull";

		/// <summary>Specifies whether this column is aliased.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040019B4 RID: 6580
		public static readonly string IsAliased = "IsAliased";

		/// <summary>Specifies whether this column is an expression.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040019B5 RID: 6581
		public static readonly string IsExpression = "IsExpression";

		/// <summary>Specifies whether this column is a key for the table. </summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040019B6 RID: 6582
		public static readonly string IsKey = "IsKey";

		/// <summary>Specifies whether a unique constraint applies to this column.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040019B7 RID: 6583
		public static readonly string IsUnique = "IsUnique";

		/// <summary>Specifies the name of the schema in the schema table.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040019B8 RID: 6584
		public static readonly string BaseSchemaName = "BaseSchemaName";

		/// <summary>Specifies the name of the table in the schema table.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040019B9 RID: 6585
		public static readonly string BaseTableName = "BaseTableName";

		/// <summary>Specifies the name of the column in the schema table.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040019BA RID: 6586
		public static readonly string BaseColumnName = "BaseColumnName";
	}
}
