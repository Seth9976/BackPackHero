using System;
using System.Collections.Generic;

namespace UnityEngine.InputSystem.Utilities
{
	// Token: 0x02000132 RID: 306
	internal static class MiscHelpers
	{
		// Token: 0x060010EF RID: 4335 RVA: 0x00050E10 File Offset: 0x0004F010
		public static TValue GetValueOrDefault<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key)
		{
			TValue tvalue;
			if (!dictionary.TryGetValue(key, out tvalue))
			{
				return default(TValue);
			}
			return tvalue;
		}

		// Token: 0x060010F0 RID: 4336 RVA: 0x00050E33 File Offset: 0x0004F033
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

		// Token: 0x060010F1 RID: 4337 RVA: 0x00050E54 File Offset: 0x0004F054
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
