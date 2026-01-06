using System;

namespace System.Data
{
	/// <summary>Specifies how conflicting changes to the data source will be detected and resolved.</summary>
	// Token: 0x0200003E RID: 62
	public enum ConflictOption
	{
		/// <summary>Update and delete statements will include all searchable columns from the table in the WHERE clause. This is equivalent to specifying CompareAllValuesUpdate | CompareAllValuesDelete.</summary>
		// Token: 0x0400048D RID: 1165
		CompareAllSearchableValues = 1,
		/// <summary>If any Timestamp columns exist in the table, they are used in the WHERE clause for all generated update statements. This is equivalent to specifying CompareRowVersionUpdate | CompareRowVersionDelete.</summary>
		// Token: 0x0400048E RID: 1166
		CompareRowVersion,
		/// <summary>All update and delete statements include only <see cref="P:System.Data.DataTable.PrimaryKey" /> columns in the WHERE clause. If no <see cref="P:System.Data.DataTable.PrimaryKey" /> is defined, all searchable columns are included in the WHERE clause. This is equivalent to OverwriteChangesUpdate | OverwriteChangesDelete.</summary>
		// Token: 0x0400048F RID: 1167
		OverwriteChanges
	}
}
