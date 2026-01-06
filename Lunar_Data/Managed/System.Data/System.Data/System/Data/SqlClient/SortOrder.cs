using System;

namespace System.Data.SqlClient
{
	/// <summary>Specifies how rows of data are sorted.</summary>
	// Token: 0x0200013A RID: 314
	public enum SortOrder
	{
		/// <summary>The default. No sort order is specified.</summary>
		// Token: 0x04000A99 RID: 2713
		Unspecified = -1,
		/// <summary>Rows are sorted in ascending order.</summary>
		// Token: 0x04000A9A RID: 2714
		Ascending,
		/// <summary>Rows are sorted in descending order.</summary>
		// Token: 0x04000A9B RID: 2715
		Descending
	}
}
