using System;

namespace System.Data
{
	/// <summary>Determines the action that occurs when the <see cref="M:System.Data.DataSet.AcceptChanges" /> or <see cref="M:System.Data.DataTable.RejectChanges" /> method is invoked on a <see cref="T:System.Data.DataTable" /> with a <see cref="T:System.Data.ForeignKeyConstraint" />.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000037 RID: 55
	public enum AcceptRejectRule
	{
		/// <summary>No action occurs (default).</summary>
		// Token: 0x0400046C RID: 1132
		None,
		/// <summary>Changes are cascaded across the relationship.</summary>
		// Token: 0x0400046D RID: 1133
		Cascade
	}
}
