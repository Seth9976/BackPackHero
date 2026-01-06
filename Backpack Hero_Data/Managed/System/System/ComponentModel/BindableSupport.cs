using System;

namespace System.ComponentModel
{
	/// <summary>Specifies values to indicate whether a property can be bound to a data element or another property.</summary>
	// Token: 0x02000698 RID: 1688
	public enum BindableSupport
	{
		/// <summary>The property is not bindable at design time.</summary>
		// Token: 0x0400204E RID: 8270
		No,
		/// <summary>The property is bindable at design time.</summary>
		// Token: 0x0400204F RID: 8271
		Yes,
		/// <summary>The property is set to the default.</summary>
		// Token: 0x04002050 RID: 8272
		Default
	}
}
