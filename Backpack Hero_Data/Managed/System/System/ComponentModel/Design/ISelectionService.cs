using System;
using System.Collections;

namespace System.ComponentModel.Design
{
	/// <summary>Provides an interface for a designer to select components.</summary>
	// Token: 0x0200077E RID: 1918
	public interface ISelectionService
	{
		/// <summary>Gets the object that is currently the primary selected object.</summary>
		/// <returns>The object that is currently the primary selected object.</returns>
		// Token: 0x17000DD8 RID: 3544
		// (get) Token: 0x06003CEB RID: 15595
		object PrimarySelection { get; }

		/// <summary>Gets the count of selected objects.</summary>
		/// <returns>The number of selected objects.</returns>
		// Token: 0x17000DD9 RID: 3545
		// (get) Token: 0x06003CEC RID: 15596
		int SelectionCount { get; }

		/// <summary>Occurs when the current selection changes.</summary>
		// Token: 0x1400005E RID: 94
		// (add) Token: 0x06003CED RID: 15597
		// (remove) Token: 0x06003CEE RID: 15598
		event EventHandler SelectionChanged;

		/// <summary>Occurs when the current selection is about to change.</summary>
		// Token: 0x1400005F RID: 95
		// (add) Token: 0x06003CEF RID: 15599
		// (remove) Token: 0x06003CF0 RID: 15600
		event EventHandler SelectionChanging;

		/// <summary>Gets a value indicating whether the specified component is currently selected.</summary>
		/// <returns>true if the component is part of the user's current selection; otherwise, false.</returns>
		/// <param name="component">The component to test. </param>
		// Token: 0x06003CF1 RID: 15601
		bool GetComponentSelected(object component);

		/// <summary>Gets a collection of components that are currently selected.</summary>
		/// <returns>A collection that represents the current set of components that are selected.</returns>
		// Token: 0x06003CF2 RID: 15602
		ICollection GetSelectedComponents();

		/// <summary>Selects the specified collection of components.</summary>
		/// <param name="components">The collection of components to select. </param>
		// Token: 0x06003CF3 RID: 15603
		void SetSelectedComponents(ICollection components);

		/// <summary>Selects the components from within the specified collection of components that match the specified selection type.</summary>
		/// <param name="components">The collection of components to select. </param>
		/// <param name="selectionType">A value from the <see cref="T:System.ComponentModel.Design.SelectionTypes" /> enumeration. The default is <see cref="F:System.ComponentModel.Design.SelectionTypes.Normal" />. </param>
		// Token: 0x06003CF4 RID: 15604
		void SetSelectedComponents(ICollection components, SelectionTypes selectionType);
	}
}
