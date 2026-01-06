using System;

namespace Pathfinding.Util
{
	// Token: 0x020000C0 RID: 192
	public static class ObjectPool<T> where T : class, IAstarPooledObject, new()
	{
		// Token: 0x06000842 RID: 2114 RVA: 0x000373BA File Offset: 0x000355BA
		public static T Claim()
		{
			return ObjectPoolSimple<T>.Claim();
		}

		// Token: 0x06000843 RID: 2115 RVA: 0x000373C1 File Offset: 0x000355C1
		public static void Release(ref T obj)
		{
			obj.OnEnterPool();
			ObjectPoolSimple<T>.Release(ref obj);
		}
	}
}
