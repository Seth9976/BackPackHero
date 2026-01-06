using System;

namespace System.Data.Common
{
	/// <summary>Indicates the position of the catalog name in a qualified table name in a text command. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200031E RID: 798
	public enum CatalogLocation
	{
		/// <summary>Indicates that the position of the catalog name occurs before the schema portion of a fully qualified table name in a text command.</summary>
		// Token: 0x04001845 RID: 6213
		Start = 1,
		/// <summary>Indicates that the position of the catalog name occurs after the schema portion of a fully qualified table name in a text command.</summary>
		// Token: 0x04001846 RID: 6214
		End
	}
}
