using System;

namespace System.ComponentModel
{
	/// <summary>Defines identifiers that indicate the type of a refresh of the Properties window.</summary>
	// Token: 0x0200074B RID: 1867
	public enum RefreshProperties
	{
		/// <summary>No refresh is necessary.</summary>
		// Token: 0x0400220A RID: 8714
		None,
		/// <summary>The properties should be requeried and the view should be refreshed.</summary>
		// Token: 0x0400220B RID: 8715
		All,
		/// <summary>The view should be refreshed.</summary>
		// Token: 0x0400220C RID: 8716
		Repaint
	}
}
