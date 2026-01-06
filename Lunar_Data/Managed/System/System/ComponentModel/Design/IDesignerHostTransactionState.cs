using System;

namespace System.ComponentModel.Design
{
	/// <summary>Specifies methods for the designer host to report on the state of transactions.</summary>
	// Token: 0x02000772 RID: 1906
	public interface IDesignerHostTransactionState
	{
		/// <summary>Gets a value indicating whether the designer host is closing a transaction. </summary>
		/// <returns>true if the designer is closing a transaction; otherwise, false. </returns>
		// Token: 0x17000DD5 RID: 3541
		// (get) Token: 0x06003CC0 RID: 15552
		bool IsClosingTransaction { get; }
	}
}
