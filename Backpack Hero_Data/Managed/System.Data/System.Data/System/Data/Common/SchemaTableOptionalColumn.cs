using System;

namespace System.Data.Common
{
	/// <summary>Describes optional column metadata of the schema for a database table.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200036A RID: 874
	public static class SchemaTableOptionalColumn
	{
		/// <summary>Specifies the provider-specific data type of the column.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040019BB RID: 6587
		public static readonly string ProviderSpecificDataType = "ProviderSpecificDataType";

		/// <summary>Specifies whether the column values in the column are automatically incremented.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040019BC RID: 6588
		public static readonly string IsAutoIncrement = "IsAutoIncrement";

		/// <summary>Specifies whether this column is hidden.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040019BD RID: 6589
		public static readonly string IsHidden = "IsHidden";

		/// <summary>Specifies whether this column is read-only.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040019BE RID: 6590
		public static readonly string IsReadOnly = "IsReadOnly";

		/// <summary>Specifies whether this column contains row version information.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040019BF RID: 6591
		public static readonly string IsRowVersion = "IsRowVersion";

		/// <summary>The server name of the column.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040019C0 RID: 6592
		public static readonly string BaseServerName = "BaseServerName";

		/// <summary>The name of the catalog associated with the results of the latest query.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040019C1 RID: 6593
		public static readonly string BaseCatalogName = "BaseCatalogName";

		/// <summary>Specifies the value at which the series for new identity columns is assigned.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040019C2 RID: 6594
		public static readonly string AutoIncrementSeed = "AutoIncrementSeed";

		/// <summary>Specifies the increment between values in the identity column.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040019C3 RID: 6595
		public static readonly string AutoIncrementStep = "AutoIncrementStep";

		/// <summary>The default value for the column.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040019C4 RID: 6596
		public static readonly string DefaultValue = "DefaultValue";

		/// <summary>The expression used to compute the column.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040019C5 RID: 6597
		public static readonly string Expression = "Expression";

		/// <summary>The namespace for the table that contains the column.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040019C6 RID: 6598
		public static readonly string BaseTableNamespace = "BaseTableNamespace";

		/// <summary>The namespace of the column.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040019C7 RID: 6599
		public static readonly string BaseColumnNamespace = "BaseColumnNamespace";

		/// <summary>Specifies the mapping for the column.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x040019C8 RID: 6600
		public static readonly string ColumnMapping = "ColumnMapping";
	}
}
