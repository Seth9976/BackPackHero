using System;

namespace System.Collections
{
	// Token: 0x020007A9 RID: 1961
	internal static class HashtableExtensions
	{
		// Token: 0x06003DE5 RID: 15845 RVA: 0x000D9F9B File Offset: 0x000D819B
		public static bool TryGetValue<T>(this Hashtable table, object key, out T value)
		{
			if (table.ContainsKey(key))
			{
				value = (T)((object)table[key]);
				return true;
			}
			value = default(T);
			return false;
		}
	}
}
