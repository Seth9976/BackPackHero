using System;

namespace Unity.VisualScripting
{
	// Token: 0x02000017 RID: 23
	public interface INotifyCollectionChanged<T>
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000093 RID: 147
		// (remove) Token: 0x06000094 RID: 148
		event Action<T> ItemAdded;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000095 RID: 149
		// (remove) Token: 0x06000096 RID: 150
		event Action<T> ItemRemoved;

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000097 RID: 151
		// (remove) Token: 0x06000098 RID: 152
		event Action CollectionChanged;
	}
}
