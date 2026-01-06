using System;

namespace System.ComponentModel.Design
{
	/// <summary>Provides a way to group a series of design-time actions to improve performance and enable most types of changes to be undone.</summary>
	// Token: 0x0200075D RID: 1885
	public abstract class DesignerTransaction : IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.DesignerTransaction" /> class with no description.</summary>
		// Token: 0x06003C2C RID: 15404 RVA: 0x000D7DC5 File Offset: 0x000D5FC5
		protected DesignerTransaction()
			: this("")
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.DesignerTransaction" /> class using the specified transaction description.</summary>
		/// <param name="description">A description for this transaction. </param>
		// Token: 0x06003C2D RID: 15405 RVA: 0x000D7DD2 File Offset: 0x000D5FD2
		protected DesignerTransaction(string description)
		{
			this.Description = description;
		}

		/// <summary>Gets a value indicating whether the transaction was canceled.</summary>
		/// <returns>true if the transaction was canceled; otherwise, false.</returns>
		// Token: 0x17000DBB RID: 3515
		// (get) Token: 0x06003C2E RID: 15406 RVA: 0x000D7DE1 File Offset: 0x000D5FE1
		// (set) Token: 0x06003C2F RID: 15407 RVA: 0x000D7DE9 File Offset: 0x000D5FE9
		public bool Canceled { get; private set; }

		/// <summary>Gets a value indicating whether the transaction was committed.</summary>
		/// <returns>true if the transaction was committed; otherwise, false.</returns>
		// Token: 0x17000DBC RID: 3516
		// (get) Token: 0x06003C30 RID: 15408 RVA: 0x000D7DF2 File Offset: 0x000D5FF2
		// (set) Token: 0x06003C31 RID: 15409 RVA: 0x000D7DFA File Offset: 0x000D5FFA
		public bool Committed { get; private set; }

		/// <summary>Gets a description for the transaction.</summary>
		/// <returns>A description for the transaction.</returns>
		// Token: 0x17000DBD RID: 3517
		// (get) Token: 0x06003C32 RID: 15410 RVA: 0x000D7E03 File Offset: 0x000D6003
		public string Description { get; }

		/// <summary>Cancels the transaction and attempts to roll back the changes made by the events of the transaction.</summary>
		// Token: 0x06003C33 RID: 15411 RVA: 0x000D7E0B File Offset: 0x000D600B
		public void Cancel()
		{
			if (!this.Canceled && !this.Committed)
			{
				this.Canceled = true;
				GC.SuppressFinalize(this);
				this._suppressedFinalization = true;
				this.OnCancel();
			}
		}

		/// <summary>Commits this transaction.</summary>
		// Token: 0x06003C34 RID: 15412 RVA: 0x000D7E37 File Offset: 0x000D6037
		public void Commit()
		{
			if (!this.Committed && !this.Canceled)
			{
				this.Committed = true;
				GC.SuppressFinalize(this);
				this._suppressedFinalization = true;
				this.OnCommit();
			}
		}

		/// <summary>Raises the Cancel event.</summary>
		// Token: 0x06003C35 RID: 15413
		protected abstract void OnCancel();

		/// <summary>Performs the actual work of committing a transaction.</summary>
		// Token: 0x06003C36 RID: 15414
		protected abstract void OnCommit();

		// Token: 0x06003C37 RID: 15415 RVA: 0x000D7E64 File Offset: 0x000D6064
		~DesignerTransaction()
		{
			this.Dispose(false);
		}

		/// <summary>Releases all resources used by the <see cref="T:System.ComponentModel.Design.DesignerTransaction" />. </summary>
		// Token: 0x06003C38 RID: 15416 RVA: 0x000D7E94 File Offset: 0x000D6094
		void IDisposable.Dispose()
		{
			this.Dispose(true);
			if (!this._suppressedFinalization)
			{
				GC.SuppressFinalize(this);
			}
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.ComponentModel.Design.DesignerTransaction" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x06003C39 RID: 15417 RVA: 0x000D7EAB File Offset: 0x000D60AB
		protected virtual void Dispose(bool disposing)
		{
			this.Cancel();
		}

		// Token: 0x04002225 RID: 8741
		private bool _suppressedFinalization;
	}
}
