using System;

namespace System.ComponentModel
{
	/// <summary>Specifies how the collection is changed.</summary>
	// Token: 0x0200069F RID: 1695
	public enum CollectionChangeAction
	{
		/// <summary>Specifies that an element was added to the collection.</summary>
		// Token: 0x04002062 RID: 8290
		Add = 1,
		/// <summary>Specifies that an element was removed from the collection.</summary>
		// Token: 0x04002063 RID: 8291
		Remove,
		/// <summary>Specifies that the entire collection has changed. This is caused by using methods that manipulate the entire collection, such as <see cref="M:System.Collections.CollectionBase.Clear" />.</summary>
		// Token: 0x04002064 RID: 8292
		Refresh
	}
}
