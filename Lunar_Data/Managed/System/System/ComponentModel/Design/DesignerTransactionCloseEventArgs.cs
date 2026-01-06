using System;

namespace System.ComponentModel.Design
{
	/// <summary>Provides data for the <see cref="E:System.ComponentModel.Design.IDesignerHost.TransactionClosed" /> and <see cref="E:System.ComponentModel.Design.IDesignerHost.TransactionClosing" /> events.</summary>
	// Token: 0x0200075E RID: 1886
	public class DesignerTransactionCloseEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.DesignerTransactionCloseEventArgs" /> class, using the specified value that indicates whether the designer called <see cref="M:System.ComponentModel.Design.DesignerTransaction.Commit" /> on the transaction.</summary>
		/// <param name="commit">A value indicating whether the transaction was committed.</param>
		// Token: 0x06003C3A RID: 15418 RVA: 0x000D7EB3 File Offset: 0x000D60B3
		[Obsolete("This constructor is obsolete. Use DesignerTransactionCloseEventArgs(bool, bool) instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
		public DesignerTransactionCloseEventArgs(bool commit)
			: this(commit, true)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.DesignerTransactionCloseEventArgs" /> class. </summary>
		/// <param name="commit">A value indicating whether the transaction was committed.</param>
		/// <param name="lastTransaction">true if this is the last transaction to close; otherwise, false.</param>
		// Token: 0x06003C3B RID: 15419 RVA: 0x000D7EBD File Offset: 0x000D60BD
		public DesignerTransactionCloseEventArgs(bool commit, bool lastTransaction)
		{
			this.TransactionCommitted = commit;
			this.LastTransaction = lastTransaction;
		}

		/// <summary>Indicates whether the designer called <see cref="M:System.ComponentModel.Design.DesignerTransaction.Commit" /> on the transaction.</summary>
		/// <returns>true if the designer called <see cref="M:System.ComponentModel.Design.DesignerTransaction.Commit" /> on the transaction; otherwise, false.</returns>
		// Token: 0x17000DBE RID: 3518
		// (get) Token: 0x06003C3C RID: 15420 RVA: 0x000D7ED3 File Offset: 0x000D60D3
		public bool TransactionCommitted { get; }

		/// <summary>Gets a value indicating whether this is the last transaction to close.</summary>
		/// <returns>true, if this is the last transaction to close; otherwise, false. </returns>
		// Token: 0x17000DBF RID: 3519
		// (get) Token: 0x06003C3D RID: 15421 RVA: 0x000D7EDB File Offset: 0x000D60DB
		public bool LastTransaction { get; }
	}
}
