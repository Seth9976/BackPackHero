using System;
using System.Collections.Generic;

namespace UnityEngine.Rendering
{
	// Token: 0x0200005C RID: 92
	public static class ListPool<T>
	{
		// Token: 0x060002F4 RID: 756 RVA: 0x0000ED9E File Offset: 0x0000CF9E
		public static List<T> Get()
		{
			return ListPool<T>.s_Pool.Get();
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x0000EDAA File Offset: 0x0000CFAA
		public static ObjectPool<List<T>>.PooledObject Get(out List<T> value)
		{
			return ListPool<T>.s_Pool.Get(out value);
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x0000EDB7 File Offset: 0x0000CFB7
		public static void Release(List<T> toRelease)
		{
			ListPool<T>.s_Pool.Release(toRelease);
		}

		// Token: 0x040001FD RID: 509
		private static readonly ObjectPool<List<T>> s_Pool = new ObjectPool<List<T>>(null, delegate(List<T> l)
		{
			l.Clear();
		}, true);
	}
}
