using System;

namespace System.ComponentModel
{
	/// <summary>Provides data for the <see cref="E:System.ComponentModel.TypeDescriptor.Refreshed" /> event.</summary>
	// Token: 0x020006FC RID: 1788
	public class RefreshEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.RefreshEventArgs" /> class with the component that has changed.</summary>
		/// <param name="componentChanged">The component that changed. </param>
		// Token: 0x0600394A RID: 14666 RVA: 0x000C834C File Offset: 0x000C654C
		public RefreshEventArgs(object componentChanged)
		{
			this.ComponentChanged = componentChanged;
			this.TypeChanged = componentChanged.GetType();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.RefreshEventArgs" /> class with the type of component that has changed.</summary>
		/// <param name="typeChanged">The <see cref="T:System.Type" /> that changed. </param>
		// Token: 0x0600394B RID: 14667 RVA: 0x000C8367 File Offset: 0x000C6567
		public RefreshEventArgs(Type typeChanged)
		{
			this.TypeChanged = typeChanged;
		}

		/// <summary>Gets the component that changed its properties, events, or extenders.</summary>
		/// <returns>The component that changed its properties, events, or extenders, or null if all components of the same type have changed.</returns>
		// Token: 0x17000D3D RID: 3389
		// (get) Token: 0x0600394C RID: 14668 RVA: 0x000C8376 File Offset: 0x000C6576
		public object ComponentChanged { get; }

		/// <summary>Gets the <see cref="T:System.Type" /> that changed its properties or events.</summary>
		/// <returns>The <see cref="T:System.Type" /> that changed its properties or events.</returns>
		// Token: 0x17000D3E RID: 3390
		// (get) Token: 0x0600394D RID: 14669 RVA: 0x000C837E File Offset: 0x000C657E
		public Type TypeChanged { get; }
	}
}
