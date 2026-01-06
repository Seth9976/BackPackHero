using System;

namespace System.ComponentModel
{
	/// <summary>Provides the base class for a custom component editor.</summary>
	// Token: 0x02000695 RID: 1685
	public abstract class ComponentEditor
	{
		/// <summary>Edits the component and returns a value indicating whether the component was modified.</summary>
		/// <returns>true if the component was modified; otherwise, false.</returns>
		/// <param name="component">The component to be edited. </param>
		// Token: 0x060035F5 RID: 13813 RVA: 0x000BFBBB File Offset: 0x000BDDBB
		public bool EditComponent(object component)
		{
			return this.EditComponent(null, component);
		}

		/// <summary>Edits the component and returns a value indicating whether the component was modified based upon a given context.</summary>
		/// <returns>true if the component was modified; otherwise, false.</returns>
		/// <param name="context">An optional context object that can be used to obtain further information about the edit. </param>
		/// <param name="component">The component to be edited. </param>
		// Token: 0x060035F6 RID: 13814
		public abstract bool EditComponent(ITypeDescriptorContext context, object component);
	}
}
