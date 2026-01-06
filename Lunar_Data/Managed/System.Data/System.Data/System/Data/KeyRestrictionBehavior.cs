using System;

namespace System.Data
{
	/// <summary>Identifies a list of connection string parameters identified by the KeyRestrictions property that are either allowed or not allowed.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000BA RID: 186
	public enum KeyRestrictionBehavior
	{
		/// <summary>Default. Identifies the only additional connection string parameters that are allowed.</summary>
		// Token: 0x04000764 RID: 1892
		AllowOnly,
		/// <summary>Identifies additional connection string parameters that are not allowed.</summary>
		// Token: 0x04000765 RID: 1893
		PreventUsage
	}
}
