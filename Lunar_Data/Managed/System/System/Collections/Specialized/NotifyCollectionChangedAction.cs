using System;

namespace System.Collections.Specialized
{
	/// <summary>Describes the action that caused a <see cref="E:System.Collections.Specialized.INotifyCollectionChanged.CollectionChanged" /> event. </summary>
	// Token: 0x020007C9 RID: 1993
	public enum NotifyCollectionChangedAction
	{
		/// <summary>One or more items were added to the collection.</summary>
		// Token: 0x04002681 RID: 9857
		Add,
		/// <summary>One or more items were removed from the collection.</summary>
		// Token: 0x04002682 RID: 9858
		Remove,
		/// <summary>One or more items were replaced in the collection.</summary>
		// Token: 0x04002683 RID: 9859
		Replace,
		/// <summary>One or more items were moved within the collection.</summary>
		// Token: 0x04002684 RID: 9860
		Move,
		/// <summary>The content of the collection changed dramatically.</summary>
		// Token: 0x04002685 RID: 9861
		Reset
	}
}
