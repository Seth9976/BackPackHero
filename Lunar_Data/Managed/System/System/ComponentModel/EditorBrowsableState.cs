using System;

namespace System.ComponentModel
{
	/// <summary>Specifies the browsable state of a property or method from within an editor.</summary>
	// Token: 0x02000675 RID: 1653
	public enum EditorBrowsableState
	{
		/// <summary>The property or method is always browsable from within an editor.</summary>
		// Token: 0x04001FF9 RID: 8185
		Always,
		/// <summary>The property or method is never browsable from within an editor.</summary>
		// Token: 0x04001FFA RID: 8186
		Never,
		/// <summary>The property or method is a feature that only advanced users should see. An editor can either show or hide such properties.</summary>
		// Token: 0x04001FFB RID: 8187
		Advanced
	}
}
