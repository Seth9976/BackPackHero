using System;
using System.Collections.Generic;

namespace UnityEngine.Rendering
{
	// Token: 0x0200005D RID: 93
	public static class HashSetPool<T>
	{
		// Token: 0x060002F8 RID: 760 RVA: 0x0000EDE2 File Offset: 0x0000CFE2
		public static HashSet<T> Get()
		{
			return HashSetPool<T>.s_Pool.Get();
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x0000EDEE File Offset: 0x0000CFEE
		public static ObjectPool<HashSet<T>>.PooledObject Get(out HashSet<T> value)
		{
			return HashSetPool<T>.s_Pool.Get(out value);
		}

		// Token: 0x060002FA RID: 762 RVA: 0x0000EDFB File Offset: 0x0000CFFB
		public static void Release(HashSet<T> toRelease)
		{
			HashSetPool<T>.s_Pool.Release(toRelease);
		}

		// Token: 0x040001FE RID: 510
		private static readonly ObjectPool<HashSet<T>> s_Pool = new ObjectPool<HashSet<T>>(null, delegate(HashSet<T> l)
		{
			l.Clear();
		}, true);
	}
}
