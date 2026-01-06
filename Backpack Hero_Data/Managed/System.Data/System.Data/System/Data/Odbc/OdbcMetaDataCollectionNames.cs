using System;

namespace System.Data.Odbc
{
	/// <summary>Provides a list of constants for use with the GetSchema method to retrieve metadata collections.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200029E RID: 670
	public static class OdbcMetaDataCollectionNames
	{
		/// <summary>A constant for use with the GetSchema method that represents the Columns collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040015AF RID: 5551
		public static readonly string Columns = "Columns";

		/// <summary>A constant for use with the GetSchema method that represents the Indexes collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040015B0 RID: 5552
		public static readonly string Indexes = "Indexes";

		/// <summary>A constant for use with the GetSchema method that represents the Procedures collection. </summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040015B1 RID: 5553
		public static readonly string Procedures = "Procedures";

		/// <summary>A constant for use with the GetSchema method that represents the ProcedureColumns collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040015B2 RID: 5554
		public static readonly string ProcedureColumns = "ProcedureColumns";

		/// <summary>A constant for use with the GetSchema method that represents the ProcedureParameters collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040015B3 RID: 5555
		public static readonly string ProcedureParameters = "ProcedureParameters";

		/// <summary>A constant for use with the GetSchema method that represents the Tables collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040015B4 RID: 5556
		public static readonly string Tables = "Tables";

		/// <summary>A constant for use with the GetSchema method that represents the Views collection.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040015B5 RID: 5557
		public static readonly string Views = "Views";
	}
}
