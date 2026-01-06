using System;
using System.Collections.Generic;

namespace UnityEngine.InputSystem.Utilities
{
	// Token: 0x02000132 RID: 306
	internal static class MiscHelpers
	{
		// Token: 0x060010E8 RID: 4328 RVA: 0x00050BFC File Offset: 0x0004EDFC
		public static TValue GetValueOrDefault<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key)
		{
			TValue tvalue;
			if (!dictionary.TryGetValue(key, out tvalue))
			{
				return default(TValue);
			}
			return tvalue;
		}

		// Token: 0x060010E9 RID: 4329 RVA: 0x00050C1F File Offset: 0x0004EE1F
		public static IEnumerable<TValue> EveryNth<TValue>(this IEnumerable<TValue> enumerable, int n, int start = 0)
		{
			int index = 0;
			foreach (TValue tvalue in enumerable)
			{
				if (index < start)
				{
					int num = index + 1;
					index = num;
				}
				else
				{
					if ((index - start) % n == 0)
					{
						yield return tvalue;
					}
					int num = index + 1;
					index = num;
				}
			}
			IEnumerator<TValue> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x060010EA RID: 4330 RVA: 0x00050C40 File Offset: 0x0004EE40
		public static int IndexOf<TValue>(this IEnumerable<TValue> enumerable, TValue value)
		{
			int num = 0;
			foreach (TValue tvalue in enumerable)
			{
				if (EqualityComparer<TValue>.Default.Equals(tvalue, value))
				{
					return num;
				}
				num++;
			}
			return -1;
		}
	}
}
