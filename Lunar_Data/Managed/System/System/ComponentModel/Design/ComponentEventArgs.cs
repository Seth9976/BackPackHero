using System;

namespace System.ComponentModel.Design
{
	/// <summary>Provides data for the <see cref="E:System.ComponentModel.Design.IComponentChangeService.ComponentAdded" />, <see cref="E:System.ComponentModel.Design.IComponentChangeService.ComponentAdding" />, <see cref="E:System.ComponentModel.Design.IComponentChangeService.ComponentRemoved" />, and <see cref="E:System.ComponentModel.Design.IComponentChangeService.ComponentRemoving" /> events.</summary>
	// Token: 0x02000759 RID: 1881
	public class ComponentEventArgs : EventArgs
	{
		/// <summary>Gets the component associated with the event.</summary>
		/// <returns>The component associated with the event.</returns>
		// Token: 0x17000DB7 RID: 3511
		// (get) Token: 0x06003C1E RID: 15390 RVA: 0x000D7D79 File Offset: 0x000D5F79
		public virtual IComponent Component { get; }

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.ComponentEventArgs" /> class.</summary>
		/// <param name="component">The component that is the source of the event. </param>
		// Token: 0x06003C1F RID: 15391 RVA: 0x000D7D81 File Offset: 0x000D5F81
		public ComponentEventArgs(IComponent component)
		{
			this.Component = component;
		}
	}
}
