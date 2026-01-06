using System;

namespace System.Data
{
	/// <summary>Specifies the type of SQL query to be used by the <see cref="T:System.Data.OleDb.OleDbRowUpdatedEventArgs" />, <see cref="T:System.Data.OleDb.OleDbRowUpdatingEventArgs" />, <see cref="T:System.Data.SqlClient.SqlRowUpdatedEventArgs" />, or <see cref="T:System.Data.SqlClient.SqlRowUpdatingEventArgs" /> class.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000E4 RID: 228
	public enum StatementType
	{
		/// <summary>An SQL query that is a SELECT statement.</summary>
		// Token: 0x04000835 RID: 2101
		Select,
		/// <summary>An SQL query that is an INSERT statement.</summary>
		// Token: 0x04000836 RID: 2102
		Insert,
		/// <summary>An SQL query that is an UPDATE statement.</summary>
		// Token: 0x04000837 RID: 2103
		Update,
		/// <summary>An SQL query that is a DELETE statement.</summary>
		// Token: 0x04000838 RID: 2104
		Delete,
		/// <summary>A SQL query that is a batch statement.</summary>
		// Token: 0x04000839 RID: 2105
		Batch
	}
}
