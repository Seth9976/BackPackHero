using System;
using System.Collections.Generic;

namespace Pathfinding.Util
{
	// Token: 0x020000BB RID: 187
	public static class ListExtensions
	{
		// Token: 0x0600082B RID: 2091 RVA: 0x00036890 File Offset: 0x00034A90
		public static T[] ToArrayFromPool<T>(this List<T> list)
		{
			T[] array = ArrayPool<T>.ClaimWithExactLength(list.Count);
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = list[i];
			}
			return array;
		}

		// Token: 0x0600082C RID: 2092 RVA: 0x000368C6 File Offset: 0x00034AC6
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
