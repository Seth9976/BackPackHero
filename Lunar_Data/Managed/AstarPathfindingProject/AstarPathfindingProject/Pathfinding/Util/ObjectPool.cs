using System;

namespace Pathfinding.Util
{
	// Token: 0x02000264 RID: 612
	public static class ObjectPool<T> where T : class, IAstarPooledObject, new()
	{
		// Token: 0x06000E82 RID: 3714 RVA: 0x0005A90C File Offset: 0x00058B0C
		public static T Claim()
		{
			return ObjectPoolSimple<T>.Claim();
		}

		// Token: 0x06000E83 RID: 3715 RVA: 0x0005A913 File Offset: 0x00058B13
		public static void Release(ref T obj)
		{
			obj.OnEnterPool();
			ObjectPoolSimple<T>.Release(ref obj);
		}
	}
}
