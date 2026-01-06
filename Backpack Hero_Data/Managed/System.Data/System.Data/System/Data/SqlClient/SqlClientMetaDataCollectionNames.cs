using System;

namespace System.Data.SqlClient
{
	/// <summary>Provides a list of constants for use with the GetSchema method to retrieve metadata collections.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200015F RID: 351
	public static class SqlClientMetaDataCollectionNames
	{
		/// <summary>A constant for use with the GetSchema method that represents the Columns collection.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x04000B72 RID: 2930
		public static readonly string Columns = "Columns";

		/// <summary>A constant for use with the GetSchema method that represents the Databases collection.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x04000B73 RID: 2931
		public static readonly string Databases = "Databases";

		/// <summary>A constant for use with the GetSchema method that represents the ForeignKeys collection.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x04000B74 RID: 2932
		public static readonly string ForeignKeys = "ForeignKeys";

		/// <summary>A constant for use with the GetSchema method that represents the IndexColumns collection.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x04000B75 RID: 2933
		public static readonly string IndexColumns = "IndexColumns";

		/// <summary>A constant for use with the GetSchema method that represents the Indexes collection.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x04000B76 RID: 2934
		public static readonly string Indexes = "Indexes";

		/// <summary>A constant for use with the GetSchema method that represents the Parameters collection.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x04000B77 RID: 2935
		public static readonly string Parameters = "Parameters";

		/// <summary>A constant for use with the GetSchema method that represents the ProcedureColumns collection.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x04000B78 RID: 2936
		public static readonly string ProcedureColumns = "ProcedureColumns";

		/// <summary>A constant for use with the GetSchema method that represents the Procedures collection.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x04000B79 RID: 2937
		public static readonly string Procedures = "Procedures";

		/// <summary>A constant for use with the GetSchema method that represents the Tables collection.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x04000B7A RID: 2938
		public static readonly string Tables = "Tables";

		/// <summary>A constant for use with the GetSchema method that represents the UserDefinedTypes collection.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x04000B7B RID: 2939
		public static readonly string UserDefinedTypes = "UserDefinedTypes";

		/// <summary>A constant for use with the GetSchema method that represents the Users collection.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x04000B7C RID: 2940
		public static readonly string Users = "Users";

		/// <summary>A constant for use with the GetSchema method that represents the ViewColumns collection.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x04000B7D RID: 2941
		public static readonly string ViewColumns = "ViewColumns";

		/// <summary>A constant for use with the GetSchema method that represents the Views collection. </summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x04000B7E RID: 2942
		public static readonly string Views = "Views";
	}
}
