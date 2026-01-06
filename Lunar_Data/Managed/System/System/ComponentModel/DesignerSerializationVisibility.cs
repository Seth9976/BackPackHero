using System;

namespace System.ComponentModel
{
	/// <summary>Specifies the visibility a property has to the design-time serializer.</summary>
	// Token: 0x0200067E RID: 1662
	public enum DesignerSerializationVisibility
	{
		/// <summary>The code generator does not produce code for the object.</summary>
		// Token: 0x04002019 RID: 8217
		Hidden,
		/// <summary>The code generator produces code for the object.</summary>
		// Token: 0x0400201A RID: 8218
		Visible,
		/// <summary>The code generator produces code for the contents of the object, rather than for the object itself.</summary>
		// Token: 0x0400201B RID: 8219
		Content
	}
}
