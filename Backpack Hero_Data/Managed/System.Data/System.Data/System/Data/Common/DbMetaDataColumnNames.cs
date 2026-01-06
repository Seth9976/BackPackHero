using System;

namespace System.Data.Common
{
	/// <summary>Provides static values that are used for the column names in the MetaDataCollection objects contained in the <see cref="T:System.Data.DataTable" />. The <see cref="T:System.Data.DataTable" /> is created by the GetSchema method.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000346 RID: 838
	public static class DbMetaDataColumnNames
	{
		/// <summary>Used by the GetSchema method to create the CollectionName column in the DataTypes collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04001940 RID: 6464
		public static readonly string CollectionName = "CollectionName";

		/// <summary>Used by the GetSchema method to create the ColumnSize column in the DataTypes collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04001941 RID: 6465
		public static readonly string ColumnSize = "ColumnSize";

		/// <summary>Used by the GetSchema method to create the CompositeIdentifierSeparatorPattern column in the DataSourceInformation collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04001942 RID: 6466
		public static readonly string CompositeIdentifierSeparatorPattern = "CompositeIdentifierSeparatorPattern";

		/// <summary>Used by the GetSchema method to create the CreateFormat column in the DataTypes collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04001943 RID: 6467
		public static readonly string CreateFormat = "CreateFormat";

		/// <summary>Used by the GetSchema method to create the CreateParameters column in the DataTypes collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04001944 RID: 6468
		public static readonly string CreateParameters = "CreateParameters";

		/// <summary>Used by the GetSchema method to create the DataSourceProductName column in the DataSourceInformation collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04001945 RID: 6469
		public static readonly string DataSourceProductName = "DataSourceProductName";

		/// <summary>Used by the GetSchema method to create the DataSourceProductVersion column in the DataSourceInformation collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04001946 RID: 6470
		public static readonly string DataSourceProductVersion = "DataSourceProductVersion";

		/// <summary>Used by the GetSchema method to create the DataType column in the DataTypes collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04001947 RID: 6471
		public static readonly string DataType = "DataType";

		/// <summary>Used by the GetSchema method to create the DataSourceProductVersionNormalized column in the DataSourceInformation collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04001948 RID: 6472
		public static readonly string DataSourceProductVersionNormalized = "DataSourceProductVersionNormalized";

		/// <summary>Used by the GetSchema method to create the GroupByBehavior column in the DataSourceInformation collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04001949 RID: 6473
		public static readonly string GroupByBehavior = "GroupByBehavior";

		/// <summary>Used by the GetSchema method to create the IdentifierCase column in the DataSourceInformation collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0400194A RID: 6474
		public static readonly string IdentifierCase = "IdentifierCase";

		/// <summary>Used by the GetSchema method to create the IdentifierPattern column in the DataSourceInformation collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0400194B RID: 6475
		public static readonly string IdentifierPattern = "IdentifierPattern";

		/// <summary>Used by the GetSchema method to create the IsAutoIncrementable column in the DataTypes collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0400194C RID: 6476
		public static readonly string IsAutoIncrementable = "IsAutoIncrementable";

		/// <summary>Used by the GetSchema method to create the IsBestMatch column in the DataTypes collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0400194D RID: 6477
		public static readonly string IsBestMatch = "IsBestMatch";

		/// <summary>Used by the GetSchema method to create the IsCaseSensitive column in the DataTypes collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0400194E RID: 6478
		public static readonly string IsCaseSensitive = "IsCaseSensitive";

		/// <summary>Used by the GetSchema method to create the IsConcurrencyType column in the DataTypes collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0400194F RID: 6479
		public static readonly string IsConcurrencyType = "IsConcurrencyType";

		/// <summary>Used by the GetSchema method to create the IsFixedLength column in the DataTypes collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04001950 RID: 6480
		public static readonly string IsFixedLength = "IsFixedLength";

		/// <summary>Used by the GetSchema method to create the IsFixedPrecisionScale column in the DataTypes collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04001951 RID: 6481
		public static readonly string IsFixedPrecisionScale = "IsFixedPrecisionScale";

		/// <summary>Used by the GetSchema method to create the IsLiteralSupported column in the DataTypes collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04001952 RID: 6482
		public static readonly string IsLiteralSupported = "IsLiteralSupported";

		/// <summary>Used by the GetSchema method to create the IsLong column in the DataTypes collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04001953 RID: 6483
		public static readonly string IsLong = "IsLong";

		/// <summary>Used by the GetSchema method to create the IsNullable column in the DataTypes collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04001954 RID: 6484
		public static readonly string IsNullable = "IsNullable";

		/// <summary>Used by the GetSchema method to create the IsSearchable column in the DataTypes collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04001955 RID: 6485
		public static readonly string IsSearchable = "IsSearchable";

		/// <summary>Used by the GetSchema method to create the IsSearchableWithLike column in the DataTypes collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04001956 RID: 6486
		public static readonly string IsSearchableWithLike = "IsSearchableWithLike";

		/// <summary>Used by the GetSchema method to create the IsUnsigned column in the DataTypes collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04001957 RID: 6487
		public static readonly string IsUnsigned = "IsUnsigned";

		/// <summary>Used by the GetSchema method to create the LiteralPrefix column in the DataTypes collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04001958 RID: 6488
		public static readonly string LiteralPrefix = "LiteralPrefix";

		/// <summary>Used by the GetSchema method to create the LiteralSuffix column in the DataTypes collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04001959 RID: 6489
		public static readonly string LiteralSuffix = "LiteralSuffix";

		/// <summary>Used by the GetSchema method to create the MaximumScale column in the DataTypes collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0400195A RID: 6490
		public static readonly string MaximumScale = "MaximumScale";

		/// <summary>Used by the GetSchema method to create the MinimumScale column in the DataTypes collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0400195B RID: 6491
		public static readonly string MinimumScale = "MinimumScale";

		/// <summary>Used by the GetSchema method to create the NumberOfIdentifierParts column in the MetaDataCollections collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0400195C RID: 6492
		public static readonly string NumberOfIdentifierParts = "NumberOfIdentifierParts";

		/// <summary>Used by the GetSchema method to create the NumberOfRestrictions column in the MetaDataCollections collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0400195D RID: 6493
		public static readonly string NumberOfRestrictions = "NumberOfRestrictions";

		/// <summary>Used by the GetSchema method to create the OrderByColumnsInSelect column in the DataSourceInformation collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0400195E RID: 6494
		public static readonly string OrderByColumnsInSelect = "OrderByColumnsInSelect";

		/// <summary>Used by the GetSchema method to create the ParameterMarkerFormat column in the DataSourceInformation collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0400195F RID: 6495
		public static readonly string ParameterMarkerFormat = "ParameterMarkerFormat";

		/// <summary>Used by the GetSchema method to create the ParameterMarkerPattern column in the DataSourceInformation collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04001960 RID: 6496
		public static readonly string ParameterMarkerPattern = "ParameterMarkerPattern";

		/// <summary>Used by the GetSchema method to create the ParameterNameMaxLength column in the DataSourceInformation collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04001961 RID: 6497
		public static readonly string ParameterNameMaxLength = "ParameterNameMaxLength";

		/// <summary>Used by the GetSchema method to create the ParameterNamePattern column in the DataSourceInformation collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04001962 RID: 6498
		public static readonly string ParameterNamePattern = "ParameterNamePattern";

		/// <summary>Used by the GetSchema method to create the ProviderDbType column in the DataTypes collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04001963 RID: 6499
		public static readonly string ProviderDbType = "ProviderDbType";

		/// <summary>Used by the GetSchema method to create the QuotedIdentifierCase column in the DataSourceInformation collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04001964 RID: 6500
		public static readonly string QuotedIdentifierCase = "QuotedIdentifierCase";

		/// <summary>Used by the GetSchema method to create the QuotedIdentifierPattern column in the DataSourceInformation collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04001965 RID: 6501
		public static readonly string QuotedIdentifierPattern = "QuotedIdentifierPattern";

		/// <summary>Used by the GetSchema method to create the ReservedWord column in the ReservedWords collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04001966 RID: 6502
		public static readonly string ReservedWord = "ReservedWord";

		/// <summary>Used by the GetSchema method to create the StatementSeparatorPattern column in the DataSourceInformation collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04001967 RID: 6503
		public static readonly string StatementSeparatorPattern = "StatementSeparatorPattern";

		/// <summary>Used by the GetSchema method to create the StringLiteralPattern column in the DataSourceInformation collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04001968 RID: 6504
		public static readonly string StringLiteralPattern = "StringLiteralPattern";

		/// <summary>Used by the GetSchema method to create the SupportedJoinOperators column in the DataSourceInformation collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04001969 RID: 6505
		public static readonly string SupportedJoinOperators = "SupportedJoinOperators";

		/// <summary>Used by the GetSchema method to create the TypeName column in the DataTypes collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0400196A RID: 6506
		public static readonly string TypeName = "TypeName";
	}
}
