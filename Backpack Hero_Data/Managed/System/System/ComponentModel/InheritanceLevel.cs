using System;

namespace System.ComponentModel
{
	/// <summary>Defines identifiers for types of inheritance levels.</summary>
	// Token: 0x020006B4 RID: 1716
	public enum InheritanceLevel
	{
		/// <summary>The object is inherited.</summary>
		// Token: 0x0400208E RID: 8334
		Inherited = 1,
		/// <summary>The object is inherited, but has read-only access.</summary>
		// Token: 0x0400208F RID: 8335
		InheritedReadOnly,
		/// <summary>The object is not inherited.</summary>
		// Token: 0x04002090 RID: 8336
		NotInherited
	}
}
