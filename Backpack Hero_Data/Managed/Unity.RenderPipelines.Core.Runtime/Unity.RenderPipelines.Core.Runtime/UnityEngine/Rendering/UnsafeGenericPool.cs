using System;

namespace UnityEngine.Rendering
{
	// Token: 0x0200005B RID: 91
	public static class UnsafeGenericPool<T> where T : new()
	{
		// Token: 0x060002F0 RID: 752 RVA: 0x0000ED69 File Offset: 0x0000CF69
		public static T Get()
		{
			return UnsafeGenericPool<T>.s_Pool.Get();
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x0000ED75 File Offset: 0x0000CF75
		public static ObjectPool<T>.PooledObject Get(out T value)
		{
			return UnsafeGenericPool<T>.s_Pool.Get(out value);
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x0000ED82 File Offset: 0x0000CF82
		public static void Release(T toRelease)
		{
			UnsafeGenericPool<T>.s_Pool.Release(toRelease);
		}

		// Token: 0x040001FC RID: 508
		private static readonly ObjectPool<T> s_Pool = new ObjectPool<T>(null, null, false);
	}
}
