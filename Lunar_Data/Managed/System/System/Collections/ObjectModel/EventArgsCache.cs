using System;
using System.Collections.Specialized;
using System.ComponentModel;

namespace System.Collections.ObjectModel
{
	// Token: 0x020007AC RID: 1964
	internal static class EventArgsCache
	{
		// Token: 0x04002618 RID: 9752
		internal static readonly PropertyChangedEventArgs CountPropertyChanged = new PropertyChangedEventArgs("Count");

		// Token: 0x04002619 RID: 9753
		internal static readonly PropertyChangedEventArgs IndexerPropertyChanged = new PropertyChangedEventArgs("Item[]");

		// Token: 0x0400261A RID: 9754
		internal static readonly NotifyCollectionChangedEventArgs ResetCollectionChanged = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset);
	}
}
