using System;

namespace UnityEngine.Pool
{
	// Token: 0x0200037D RID: 893
	public interface IObjectPool<T> where T : class
	{
		// Token: 0x170005E7 RID: 1511
		// (get) Token: 0x06001E97 RID: 7831
		int CountInactive { get; }

		// Token: 0x06001E98 RID: 7832
		T Get();

		// Token: 0x06001E99 RID: 7833
		PooledObject<T> Get(out T v);

		// Token: 0x06001E9A RID: 7834
		void Release(T element);

		// Token: 0x06001E9B RID: 7835
		void Clear();
	}
}
