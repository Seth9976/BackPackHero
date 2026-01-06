using System;
using System.Collections.Generic;
using System.Linq;

namespace Unity.VisualScripting
{
	// Token: 0x020000BD RID: 189
	public static class XArrayPool
	{
		// Token: 0x060004B1 RID: 1201 RVA: 0x0000A5E8 File Offset: 0x000087E8
		public static T[] ToArrayPooled<T>(this IEnumerable<T> source)
		{
			T[] array = ArrayPool<T>.New(source.Count<T>());
			int num = 0;
			foreach (T t in source)
			{
				array[num++] = t;
			}
			return array;
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x0000A644 File Offset: 0x00008844
		public static void Free<T>(this T[] array)
		{
			ArrayPool<T>.Free(array);
		}
	}
}
