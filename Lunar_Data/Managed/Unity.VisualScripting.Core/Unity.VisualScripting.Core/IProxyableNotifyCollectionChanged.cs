using System;

namespace Unity.VisualScripting
{
	// Token: 0x02000018 RID: 24
	public interface IProxyableNotifyCollectionChanged<T>
	{
		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000099 RID: 153
		// (set) Token: 0x0600009A RID: 154
		bool ProxyCollectionChange { get; set; }

		// Token: 0x0600009B RID: 155
		void BeforeAdd(T item);

		// Token: 0x0600009C RID: 156
		void AfterAdd(T item);

		// Token: 0x0600009D RID: 157
		void BeforeRemove(T item);

		// Token: 0x0600009E RID: 158
		void AfterRemove(T item);
	}
}
