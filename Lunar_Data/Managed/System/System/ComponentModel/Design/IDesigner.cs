using System;

namespace System.ComponentModel.Design
{
	/// <summary>Provides the basic framework for building a custom designer.</summary>
	// Token: 0x0200076E RID: 1902
	public interface IDesigner : IDisposable
	{
		/// <summary>Gets the base component that this designer is designing.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.IComponent" /> indicating the base component that this designer is designing.</returns>
		// Token: 0x17000DCB RID: 3531
		// (get) Token: 0x06003C90 RID: 15504
		IComponent Component { get; }

		/// <summary>Gets a collection of the design-time verbs supported by the designer.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.Design.DesignerVerbCollection" /> that contains the verbs supported by the designer, or null if the component has no verbs.</returns>
		// Token: 0x17000DCC RID: 3532
		// (get) Token: 0x06003C91 RID: 15505
		DesignerVerbCollection Verbs { get; }

		/// <summary>Performs the default action for this designer.</summary>
		// Token: 0x06003C92 RID: 15506
		void DoDefaultAction();

		/// <summary>Initializes the designer with the specified component.</summary>
		/// <param name="component">The component to associate with this designer. </param>
		// Token: 0x06003C93 RID: 15507
		void Initialize(IComponent component);
	}
}
