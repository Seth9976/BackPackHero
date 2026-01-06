using System;

namespace Microsoft.SqlServer.Server
{
	/// <summary>Describes the type of access to user data for a user-defined method or function.</summary>
	// Token: 0x020003C2 RID: 962
	[Serializable]
	public enum DataAccessKind
	{
		/// <summary>The method or function does not access user data.</summary>
		// Token: 0x04001BBB RID: 7099
		None,
		/// <summary>The method or function reads user data.</summary>
		// Token: 0x04001BBC RID: 7100
		Read
	}
}
