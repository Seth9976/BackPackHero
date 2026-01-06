using System;

namespace System.ComponentModel
{
	/// <summary>Specifies whether the template can be bound one way or two ways.</summary>
	// Token: 0x02000699 RID: 1689
	public enum BindingDirection
	{
		/// <summary>The template can only accept property values. Used with a generic <see cref="T:System.Web.UI.ITemplate" />.</summary>
		// Token: 0x04002052 RID: 8274
		OneWay,
		/// <summary>The template can accept and expose property values. Used with an <see cref="T:System.Web.UI.IBindableTemplate" />.</summary>
		// Token: 0x04002053 RID: 8275
		TwoWay
	}
}
