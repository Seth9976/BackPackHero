using System;

namespace System.ComponentModel.Design
{
	/// <summary>Provides data for the <see cref="P:System.ComponentModel.Design.IDesignerEventService.ActiveDesigner" /> event.</summary>
	// Token: 0x02000751 RID: 1873
	public class ActiveDesignerEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.ActiveDesignerEventArgs" /> class.</summary>
		/// <param name="oldDesigner">The document that is losing activation. </param>
		/// <param name="newDesigner">The document that is gaining activation. </param>
		// Token: 0x06003BFB RID: 15355 RVA: 0x000D7BCD File Offset: 0x000D5DCD
		public ActiveDesignerEventArgs(IDesignerHost oldDesigner, IDesignerHost newDesigner)
		{
			this.OldDesigner = oldDesigner;
			this.NewDesigner = newDesigner;
		}

		/// <summary>Gets the document that is losing activation.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.Design.IDesignerHost" /> that represents the document losing activation.</returns>
		// Token: 0x17000DAD RID: 3501
		// (get) Token: 0x06003BFC RID: 15356 RVA: 0x000D7BE3 File Offset: 0x000D5DE3
		public IDesignerHost OldDesigner { get; }

		/// <summary>Gets the document that is gaining activation.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.Design.IDesignerHost" /> that represents the document gaining activation.</returns>
		// Token: 0x17000DAE RID: 3502
		// (get) Token: 0x06003BFD RID: 15357 RVA: 0x000D7BEB File Offset: 0x000D5DEB
		public IDesignerHost NewDesigner { get; }
	}
}
