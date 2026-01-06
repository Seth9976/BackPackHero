using System;

namespace System.Data
{
	/// <summary>Determines the action that occurs when a mapping is missing from a source table or a source column.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000C0 RID: 192
	public enum MissingMappingAction
	{
		/// <summary>The source column or source table is created and added to the <see cref="T:System.Data.DataSet" /> using its original name.</summary>
		// Token: 0x04000778 RID: 1912
		Passthrough = 1,
		/// <summary>The column or table not having a mapping is ignored. Returns null.</summary>
		// Token: 0x04000779 RID: 1913
		Ignore,
		/// <summary>An <see cref="T:System.InvalidOperationException" /> is generated if the specified column mapping is missing.</summary>
		// Token: 0x0400077A RID: 1914
		Error
	}
}
