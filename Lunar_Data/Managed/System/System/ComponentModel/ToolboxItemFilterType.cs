using System;

namespace System.ComponentModel
{
	/// <summary>Defines identifiers used to indicate the type of filter that a <see cref="T:System.ComponentModel.ToolboxItemFilterAttribute" /> uses.</summary>
	// Token: 0x02000706 RID: 1798
	public enum ToolboxItemFilterType
	{
		/// <summary>Indicates that a toolbox item filter string is allowed, but not required.</summary>
		// Token: 0x04002158 RID: 8536
		Allow,
		/// <summary>Indicates that custom processing is required to determine whether to use a toolbox item filter string. </summary>
		// Token: 0x04002159 RID: 8537
		Custom,
		/// <summary>Indicates that a toolbox item filter string is not allowed. </summary>
		// Token: 0x0400215A RID: 8538
		Prevent,
		/// <summary>Indicates that a toolbox item filter string must be present for a toolbox item to be enabled. </summary>
		// Token: 0x0400215B RID: 8539
		Require
	}
}
