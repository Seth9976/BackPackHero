using System;

namespace UnityEngine.Pool
{
	// Token: 0x02000382 RID: 898
	public static class UnsafeGenericPool<T> where T : class, new()
	{
		// Token: 0x06001EB1 RID: 7857 RVA: 0x00031DAB File Offset: 0x0002FFAB
		public static T Get()
		{
			return UnsafeGenericPool<T>.s_Pool.Get();
		}

		// Token: 0x06001EB2 RID: 7858 RVA: 0x00031DB7 File Offset: 0x0002FFB7
		public static PooledObject<T> Get(out T value)
		{
			return UnsafeGenericPool<T>.s_Pool.Get(out value);
		}

		// Token: 0x06001EB3 RID: 7859 RVA: 0x00031DC4 File Offset: 0x0002FFC4
		public static void Release(T toRelease)
		{
			UnsafeGenericPool<T>.s_Pool.Release(toRelease);
		}

		// Token: 0x04000A14 RID: 2580
		internal static readonly ObjectPool<T> s_Pool = new ObjectPool<T>(() => new T(), null, null, null, false, 10, 10000);
	}
}
