using System;

namespace System.ComponentModel.Design
{
	/// <summary>Provides data for the <see cref="E:System.ComponentModel.Design.IDesignerEventService.DesignerCreated" /> and <see cref="E:System.ComponentModel.Design.IDesignerEventService.DesignerDisposed" /> events.</summary>
	// Token: 0x02000766 RID: 1894
	public class DesignerEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.DesignerEventArgs" /> class.</summary>
		/// <param name="host">The <see cref="T:System.ComponentModel.Design.IDesignerHost" /> of the document. </param>
		// Token: 0x06003C6F RID: 15471 RVA: 0x000D841E File Offset: 0x000D661E
		public DesignerEventArgs(IDesignerHost host)
		{
			this.Designer = host;
		}

		/// <summary>Gets the host of the document.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.Design.IDesignerHost" /> of the document.</returns>
		// Token: 0x17000DC9 RID: 3529
		// (get) Token: 0x06003C70 RID: 15472 RVA: 0x000D842D File Offset: 0x000D662D
		public IDesignerHost Designer { get; }
	}
}
