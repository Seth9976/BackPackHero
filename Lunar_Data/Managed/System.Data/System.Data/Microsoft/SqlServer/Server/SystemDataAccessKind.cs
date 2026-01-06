using System;

namespace Microsoft.SqlServer.Server
{
	/// <summary>Describes the type of access to system data for a user-defined method or function.</summary>
	// Token: 0x020003C3 RID: 963
	[Serializable]
	public enum SystemDataAccessKind
	{
		/// <summary>The method or function does not access system data. </summary>
		// Token: 0x04001BBE RID: 7102
		None,
		/// <summary>The method or function reads system data.</summary>
		// Token: 0x04001BBF RID: 7103
		Read
	}
}
