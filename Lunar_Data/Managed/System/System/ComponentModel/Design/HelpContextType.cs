using System;

namespace System.ComponentModel.Design
{
	/// <summary>Defines identifiers that indicate information about the context in which a request for Help information originated.</summary>
	// Token: 0x02000768 RID: 1896
	public enum HelpContextType
	{
		/// <summary>A general context.</summary>
		// Token: 0x04002232 RID: 8754
		Ambient,
		/// <summary>A window.</summary>
		// Token: 0x04002233 RID: 8755
		Window,
		/// <summary>A selection.</summary>
		// Token: 0x04002234 RID: 8756
		Selection,
		/// <summary>A tool window selection.</summary>
		// Token: 0x04002235 RID: 8757
		ToolWindowSelection
	}
}
