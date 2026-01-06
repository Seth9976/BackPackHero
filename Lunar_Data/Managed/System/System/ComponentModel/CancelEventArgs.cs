using System;

namespace System.ComponentModel
{
	/// <summary>Provides data for a cancelable event.</summary>
	// Token: 0x02000710 RID: 1808
	public class CancelEventArgs : EventArgs
	{
		/// <summary>Gets or sets a value indicating whether the event should be canceled.</summary>
		/// <returns>true if the event should be canceled; otherwise, false.</returns>
		// Token: 0x17000D4E RID: 3406
		// (get) Token: 0x060039B6 RID: 14774 RVA: 0x000C8D6D File Offset: 0x000C6F6D
		// (set) Token: 0x060039B7 RID: 14775 RVA: 0x000C8D75 File Offset: 0x000C6F75
		public bool Cancel { get; set; }

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.CancelEventArgs" /> class with the <see cref="P:System.ComponentModel.CancelEventArgs.Cancel" /> property set to false.</summary>
		// Token: 0x060039B8 RID: 14776 RVA: 0x0000C6B5 File Offset: 0x0000A8B5
		public CancelEventArgs()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.CancelEventArgs" /> class with the <see cref="P:System.ComponentModel.CancelEventArgs.Cancel" /> property set to the given value.</summary>
		/// <param name="cancel">true to cancel the event; otherwise, false. </param>
		// Token: 0x060039B9 RID: 14777 RVA: 0x000C8D7E File Offset: 0x000C6F7E
		public CancelEventArgs(bool cancel)
		{
			this.Cancel = cancel;
		}
	}
}
