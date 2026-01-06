using System;

namespace System.Data
{
	/// <summary>Describes the version of data in a <see cref="T:System.Data.DataRow" />.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000087 RID: 135
	[Flags]
	public enum DataViewRowState
	{
		/// <summary>None.</summary>
		// Token: 0x04000606 RID: 1542
		None = 0,
		/// <summary>An unchanged row.</summary>
		// Token: 0x04000607 RID: 1543
		Unchanged = 2,
		/// <summary>A new row.</summary>
		// Token: 0x04000608 RID: 1544
		Added = 4,
		/// <summary>A deleted row.</summary>
		// Token: 0x04000609 RID: 1545
		Deleted = 8,
		/// <summary>A current version of original data that has been modified (see ModifiedOriginal).</summary>
		// Token: 0x0400060A RID: 1546
		ModifiedCurrent = 16,
		/// <summary>The original version of the data that was modified. (Although the data has since been modified, it is available as ModifiedCurrent).</summary>
		// Token: 0x0400060B RID: 1547
		ModifiedOriginal = 32,
		/// <summary>Original rows including unchanged and deleted rows.</summary>
		// Token: 0x0400060C RID: 1548
		OriginalRows = 42,
		/// <summary>Current rows including unchanged, new, and modified rows. By default, <see cref="T:System.Data.DataViewRowState" /> is set to CurrentRows.</summary>
		// Token: 0x0400060D RID: 1549
		CurrentRows = 22
	}
}
