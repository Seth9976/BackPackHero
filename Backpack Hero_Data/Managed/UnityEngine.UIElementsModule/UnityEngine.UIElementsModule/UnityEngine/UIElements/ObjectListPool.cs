using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x020000E1 RID: 225
	internal class ObjectListPool<T>
	{
		// Token: 0x06000731 RID: 1841 RVA: 0x0001A2B0 File Offset: 0x000184B0
		public static List<T> Get()
		{
			return ObjectListPool<T>.pool.Get();
		}

		// Token: 0x06000732 RID: 1842 RVA: 0x0001A2CC File Offset: 0x000184CC
		public static void Release(List<T> elements)
		{
			elements.Clear();
			ObjectListPool<T>.pool.Release(elements);
		}

		// Token: 0x040002F0 RID: 752
		private static ObjectPool<List<T>> pool = new ObjectPool<List<T>>(20);
	}
}
