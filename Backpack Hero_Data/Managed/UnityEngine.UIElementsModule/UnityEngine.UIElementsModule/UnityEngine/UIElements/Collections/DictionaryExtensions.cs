using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements.Collections
{
	// Token: 0x0200034D RID: 845
	internal static class DictionaryExtensions
	{
		// Token: 0x06001B0B RID: 6923 RVA: 0x0007A884 File Offset: 0x00078A84
		public static TValue Get<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key, TValue fallbackValue = default(TValue))
		{
			TValue tvalue;
			return dict.TryGetValue(key, ref tvalue) ? tvalue : fallbackValue;
		}
	}
}
