using System;

namespace System.Data.Common
{
	/// <summary>Specifies the relationship between the columns in a GROUP BY clause and the non-aggregated columns in the select-list of a SELECT statement.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200034E RID: 846
	public enum GroupByBehavior
	{
		/// <summary>The support for the GROUP BY clause is unknown.</summary>
		// Token: 0x04001973 RID: 6515
		Unknown,
		/// <summary>The GROUP BY clause is not supported.</summary>
		// Token: 0x04001974 RID: 6516
		NotSupported,
		/// <summary>There is no relationship between the columns in the GROUP BY clause and the nonaggregated columns in the SELECT list. You may group by any column.</summary>
		// Token: 0x04001975 RID: 6517
		Unrelated,
		/// <summary>The GROUP BY clause must contain all nonaggregated columns in the select list, and can contain other columns not in the select list.</summary>
		// Token: 0x04001976 RID: 6518
		MustContainAll,
		/// <summary>The GROUP BY clause must contain all nonaggregated columns in the select list, and must not contain other columns not in the select list.</summary>
		// Token: 0x04001977 RID: 6519
		ExactMatch
	}
}
