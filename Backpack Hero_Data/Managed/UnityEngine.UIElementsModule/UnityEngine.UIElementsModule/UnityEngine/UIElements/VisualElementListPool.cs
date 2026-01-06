using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x020000E0 RID: 224
	internal static class VisualElementListPool
	{
		// Token: 0x0600072D RID: 1837 RVA: 0x0001A228 File Offset: 0x00018428
		public static List<VisualElement> Copy(List<VisualElement> elements)
		{
			List<VisualElement> list = VisualElementListPool.pool.Get();
			list.AddRange(elements);
			return list;
		}

		// Token: 0x0600072E RID: 1838 RVA: 0x0001A250 File Offset: 0x00018450
		public static List<VisualElement> Get(int initialCapacity = 0)
		{
			List<VisualElement> list = VisualElementListPool.pool.Get();
			bool flag = initialCapacity > 0 && list.Capacity < initialCapacity;
			if (flag)
			{
				list.Capacity = initialCapacity;
			}
			return list;
		}

		// Token: 0x0600072F RID: 1839 RVA: 0x0001A28C File Offset: 0x0001848C
		public static void Release(List<VisualElement> elements)
		{
			elements.Clear();
			VisualElementListPool.pool.Release(elements);
		}

		// Token: 0x040002EF RID: 751
		private static ObjectPool<List<VisualElement>> pool = new ObjectPool<List<VisualElement>>(20);
	}
}
