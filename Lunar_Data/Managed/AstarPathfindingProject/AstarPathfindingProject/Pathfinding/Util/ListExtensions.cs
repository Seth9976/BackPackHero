using System;
using System.Collections.Generic;

namespace Pathfinding.Util
{
	// Token: 0x02000261 RID: 609
	public static class ListExtensions
	{
		// Token: 0x06000E76 RID: 3702 RVA: 0x0005A590 File Offset: 0x00058790
		public static T[] ToArrayFromPool<T>(this List<T> list)
		{
			T[] array = ArrayPool<T>.ClaimWithExactLength(list.Count);
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = list[i];
			}
			return array;
		}

		// Token: 0x06000E77 RID: 3703 RVA: 0x0005A5C6 File Offset: 0x000587C6
		public static void ClearFast<T>(this List<T> list)
		{
			if (list.Count * 2 < list.Capacity)
			{
				list.RemoveRange(0, list.Count);
				return;
			}
			list.Clear();
		}
	}
}
