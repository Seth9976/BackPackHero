using System;

namespace System.Collections.Specialized
{
	/// <summary>Notifies listeners of dynamic changes, such as when items get added and removed or the whole list is refreshed.</summary>
	// Token: 0x020007C8 RID: 1992
	public interface INotifyCollectionChanged
	{
		/// <summary>Occurs when the collection changes.</summary>
		// Token: 0x1400006A RID: 106
		// (add) Token: 0x06003F6B RID: 16235
		// (remove) Token: 0x06003F6C RID: 16236
		event NotifyCollectionChangedEventHandler CollectionChanged;
	}
}
