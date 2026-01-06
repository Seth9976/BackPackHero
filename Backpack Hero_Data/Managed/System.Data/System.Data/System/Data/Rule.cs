using System;

namespace System.Data
{
	/// <summary>Indicates the action that occurs when a <see cref="T:System.Data.ForeignKeyConstraint" /> is enforced.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000D1 RID: 209
	public enum Rule
	{
		/// <summary>No action taken on related rows.</summary>
		// Token: 0x040007CC RID: 1996
		None,
		/// <summary>Delete or update related rows. This is the default.</summary>
		// Token: 0x040007CD RID: 1997
		Cascade,
		/// <summary>Set values in related rows to DBNull.</summary>
		// Token: 0x040007CE RID: 1998
		SetNull,
		/// <summary>Set values in related rows to the value contained in the <see cref="P:System.Data.DataColumn.DefaultValue" /> property.</summary>
		// Token: 0x040007CF RID: 1999
		SetDefault
	}
}
