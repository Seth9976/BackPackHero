using System;

namespace System.ComponentModel
{
	/// <summary>Adds transactional capability when adding a new item to a collection.</summary>
	// Token: 0x020006C6 RID: 1734
	public interface ICancelAddNew
	{
		/// <summary>Discards a pending new item from the collection.</summary>
		/// <param name="itemIndex">The index of the item that was previously added to the collection. </param>
		// Token: 0x06003777 RID: 14199
		void CancelNew(int itemIndex);

		/// <summary>Commits a pending new item to the collection.</summary>
		/// <param name="itemIndex">The index of the item that was previously added to the collection. </param>
		// Token: 0x06003778 RID: 14200
		void EndNew(int itemIndex);
	}
}
