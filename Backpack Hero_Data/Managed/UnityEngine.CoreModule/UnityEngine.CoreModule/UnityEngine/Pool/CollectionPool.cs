using System;
using System.Collections.Generic;

namespace UnityEngine.Pool
{
	// Token: 0x02000376 RID: 886
	public class CollectionPool<TCollection, TItem> where TCollection : class, ICollection<TItem>, new()
	{
		// Token: 0x06001E83 RID: 7811 RVA: 0x000317D9 File Offset: 0x0002F9D9
		public static TCollection Get()
		{
			return CollectionPool<TCollection, TItem>.s_Pool.Get();
		}

		// Token: 0x06001E84 RID: 7812 RVA: 0x000317E5 File Offset: 0x0002F9E5
		public static PooledObject<TCollection> Get(out TCollection value)
		{
			return CollectionPool<TCollection, TItem>.s_Pool.Get(out value);
		}

		// Token: 0x06001E85 RID: 7813 RVA: 0x000317F2 File Offset: 0x0002F9F2
		public static void Release(TCollection toRelease)
		{
			CollectionPool<TCollection, TItem>.s_Pool.Release(toRelease);
		}

		// Token: 0x040009FB RID: 2555
		internal static readonly ObjectPool<TCollection> s_Pool = new ObjectPool<TCollection>(() => new TCollection(), null, delegate(TCollection l)
		{
			l.Clear();
		}, null, true, 10, 10000);
	}
}
