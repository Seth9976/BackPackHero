using System;

namespace System.ComponentModel.Design
{
	/// <summary>Provides event notifications when root designers are added and removed, when a selected component changes, and when the current root designer changes.</summary>
	// Token: 0x0200076F RID: 1903
	public interface IDesignerEventService
	{
		/// <summary>Gets the root designer for the currently active document.</summary>
		/// <returns>The currently active document, or null if there is no active document.</returns>
		// Token: 0x17000DCD RID: 3533
		// (get) Token: 0x06003C94 RID: 15508
		IDesignerHost ActiveDesigner { get; }

		/// <summary>Gets a collection of root designers for design documents that are currently active in the development environment.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.Design.DesignerCollection" /> containing the root designers that have been created and not yet disposed.</returns>
		// Token: 0x17000DCE RID: 3534
		// (get) Token: 0x06003C95 RID: 15509
		DesignerCollection Designers { get; }

		/// <summary>Occurs when the current root designer changes.</summary>
		// Token: 0x14000053 RID: 83
		// (add) Token: 0x06003C96 RID: 15510
		// (remove) Token: 0x06003C97 RID: 15511
		event ActiveDesignerEventHandler ActiveDesignerChanged;

		/// <summary>Occurs when a root designer is created.</summary>
		// Token: 0x14000054 RID: 84
		// (add) Token: 0x06003C98 RID: 15512
		// (remove) Token: 0x06003C99 RID: 15513
		event DesignerEventHandler DesignerCreated;

		/// <summary>Occurs when a root designer for a document is disposed.</summary>
		// Token: 0x14000055 RID: 85
		// (add) Token: 0x06003C9A RID: 15514
		// (remove) Token: 0x06003C9B RID: 15515
		event DesignerEventHandler DesignerDisposed;

		/// <summary>Occurs when the current design-view selection changes.</summary>
		// Token: 0x14000056 RID: 86
		// (add) Token: 0x06003C9C RID: 15516
		// (remove) Token: 0x06003C9D RID: 15517
		event EventHandler SelectionChanged;
	}
}
