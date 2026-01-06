using System;

namespace System.Data
{
	/// <summary>Describes an action performed on a <see cref="T:System.Data.DataRow" />.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000066 RID: 102
	[Flags]
	public enum DataRowAction
	{
		/// <summary>The row has not changed.</summary>
		// Token: 0x0400051F RID: 1311
		Nothing = 0,
		/// <summary>The row was deleted from the table.</summary>
		// Token: 0x04000520 RID: 1312
		Delete = 1,
		/// <summary>The row has changed.</summary>
		// Token: 0x04000521 RID: 1313
		Change = 2,
		/// <summary>The most recent change to the row has been rolled back.</summary>
		// Token: 0x04000522 RID: 1314
		Rollback = 4,
		/// <summary>The changes to the row have been committed.</summary>
		// Token: 0x04000523 RID: 1315
		Commit = 8,
		/// <summary>The row has been added to the table.</summary>
		// Token: 0x04000524 RID: 1316
		Add = 16,
		/// <summary>The original version of the row has been changed.</summary>
		// Token: 0x04000525 RID: 1317
		ChangeOriginal = 32,
		/// <summary>The original and the current versions of the row have been changed.</summary>
		// Token: 0x04000526 RID: 1318
		ChangeCurrentAndOriginal = 64
	}
}
