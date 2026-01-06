using System;

namespace System.Data.Odbc
{
	/// <summary>Provides static values that are used for the column names in the <see cref="T:System.Data.Odbc.OdbcMetaDataCollectionNames" /> objects contained in the <see cref="T:System.Data.DataTable" />. The <see cref="T:System.Data.DataTable" /> is created by the GetSchema method.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200029F RID: 671
	public static class OdbcMetaDataColumnNames
	{
		/// <summary>Used by the GetSchema method to create the BooleanFalseLiteral column.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040015B6 RID: 5558
		public static readonly string BooleanFalseLiteral = "BooleanFalseLiteral";

		/// <summary>Used by the GetSchema method to create the BooleanTrueLiteral column.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040015B7 RID: 5559
		public static readonly string BooleanTrueLiteral = "BooleanTrueLiteral";

		/// <summary>Used by the GetSchema method to create the SQLType column. </summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040015B8 RID: 5560
		public static readonly string SQLType = "SQLType";
	}
}
