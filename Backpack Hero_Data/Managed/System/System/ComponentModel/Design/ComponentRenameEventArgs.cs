using System;

namespace System.ComponentModel.Design
{
	/// <summary>Provides data for the <see cref="E:System.ComponentModel.Design.IComponentChangeService.ComponentRename" /> event.</summary>
	// Token: 0x0200075B RID: 1883
	public class ComponentRenameEventArgs : EventArgs
	{
		/// <summary>Gets the component that is being renamed.</summary>
		/// <returns>The component that is being renamed.</returns>
		// Token: 0x17000DB8 RID: 3512
		// (get) Token: 0x06003C24 RID: 15396 RVA: 0x000D7D90 File Offset: 0x000D5F90
		public object Component { get; }

		/// <summary>Gets the name of the component before the rename event.</summary>
		/// <returns>The previous name of the component.</returns>
		// Token: 0x17000DB9 RID: 3513
		// (get) Token: 0x06003C25 RID: 15397 RVA: 0x000D7D98 File Offset: 0x000D5F98
		public virtual string OldName { get; }

		/// <summary>Gets the name of the component after the rename event.</summary>
		/// <returns>The name of the component after the rename event.</returns>
		// Token: 0x17000DBA RID: 3514
		// (get) Token: 0x06003C26 RID: 15398 RVA: 0x000D7DA0 File Offset: 0x000D5FA0
		public virtual string NewName { get; }

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.ComponentRenameEventArgs" /> class.</summary>
		/// <param name="component">The component to be renamed. </param>
		/// <param name="oldName">The old name of the component. </param>
		/// <param name="newName">The new name of the component. </param>
		// Token: 0x06003C27 RID: 15399 RVA: 0x000D7DA8 File Offset: 0x000D5FA8
		public ComponentRenameEventArgs(object component, string oldName, string newName)
		{
			this.OldName = oldName;
			this.NewName = newName;
			this.Component = component;
		}
	}
}
