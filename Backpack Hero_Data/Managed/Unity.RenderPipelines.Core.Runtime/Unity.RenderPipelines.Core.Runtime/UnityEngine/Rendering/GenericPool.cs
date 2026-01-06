using System;

namespace UnityEngine.Rendering
{
	// Token: 0x0200005A RID: 90
	public static class GenericPool<T> where T : new()
	{
		// Token: 0x060002EC RID: 748 RVA: 0x0000ED34 File Offset: 0x0000CF34
		public static T Get()
		{
			return GenericPool<T>.s_Pool.Get();
		}

		// Token: 0x060002ED RID: 749 RVA: 0x0000ED40 File Offset: 0x0000CF40
		public static ObjectPool<T>.PooledObject Get(out T value)
		{
			return GenericPool<T>.s_Pool.Get(out value);
		}

		// Token: 0x060002EE RID: 750 RVA: 0x0000ED4D File Offset: 0x0000CF4D
		public static void Release(T toRelease)
		{
			GenericPool<T>.s_Pool.Release(toRelease);
		}

		// Token: 0x040001FB RID: 507
		private static readonly ObjectPool<T> s_Pool = new ObjectPool<T>(null, null, true);
	}
}
