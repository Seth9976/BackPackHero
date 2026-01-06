using System;

namespace System.Data
{
	/// <summary>Describes the version of a <see cref="T:System.Data.DataRow" />.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200006E RID: 110
	public enum DataRowVersion
	{
		/// <summary>The row contains its original values.</summary>
		// Token: 0x04000533 RID: 1331
		Original = 256,
		/// <summary>The row contains current values.</summary>
		// Token: 0x04000534 RID: 1332
		Current = 512,
		/// <summary>The row contains a proposed value.</summary>
		// Token: 0x04000535 RID: 1333
		Proposed = 1024,
		/// <summary>The default version of <see cref="T:System.Data.DataRowState" />. For a DataRowState value of Added, Modified or Deleted, the default version is Current. For a <see cref="T:System.Data.DataRowState" /> value of Detached, the version is Proposed.</summary>
		// Token: 0x04000536 RID: 1334
		Default = 1536
	}
}
