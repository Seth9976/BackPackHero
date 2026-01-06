using System;

namespace UnityEngine.Pool
{
	// Token: 0x0200037B RID: 891
	public class GenericPool<T> where T : class, new()
	{
		// Token: 0x06001E8F RID: 7823 RVA: 0x00031872 File Offset: 0x0002FA72
		public static T Get()
		{
			return GenericPool<T>.s_Pool.Get();
		}

		// Token: 0x06001E90 RID: 7824 RVA: 0x0003187E File Offset: 0x0002FA7E
		public static PooledObject<T> Get(out T value)
		{
			return GenericPool<T>.s_Pool.Get(out value);
		}

		// Token: 0x06001E91 RID: 7825 RVA: 0x0003188B File Offset: 0x0002FA8B
		public static void Release(T toRelease)
		{
			GenericPool<T>.s_Pool.Release(toRelease);
		}

		// Token: 0x040009FD RID: 2557
		internal static readonly ObjectPool<T> s_Pool = new ObjectPool<T>(() => new T(), null, null, null, true, 10, 10000);
	}
}
