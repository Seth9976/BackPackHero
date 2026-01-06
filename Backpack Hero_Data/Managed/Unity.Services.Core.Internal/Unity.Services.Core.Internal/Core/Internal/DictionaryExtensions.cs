using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Unity.Services.Core.Internal
{
	// Token: 0x0200004A RID: 74
	internal static class DictionaryExtensions
	{
		// Token: 0x0600015A RID: 346 RVA: 0x00003D24 File Offset: 0x00001F24
		public static TDictionary MergeNoOverride<TDictionary, TKey, TValue>(this TDictionary self, [NotNull] IDictionary<TKey, TValue> dictionary) where TDictionary : IDictionary<TKey, TValue>
		{
			foreach (KeyValuePair<TKey, TValue> keyValuePair in dictionary)
			{
				if (!self.ContainsKey(keyValuePair.Key))
				{
					self[keyValuePair.Key] = keyValuePair.Value;
				}
			}
			return self;
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00003D98 File Offset: 0x00001F98
		public static TDictionary MergeAllowOverride<TDictionary, TKey, TValue>(this TDictionary self, [NotNull] IDictionary<TKey, TValue> dictionary) where TDictionary : IDictionary<TKey, TValue>
		{
			foreach (KeyValuePair<TKey, TValue> keyValuePair in dictionary)
			{
				self[keyValuePair.Key] = keyValuePair.Value;
			}
			return self;
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00003DF8 File Offset: 0x00001FF8
		public static bool ValueEquals<TKey, TValue>(this IDictionary<TKey, TValue> x, IDictionary<TKey, TValue> y)
		{
			return x.ValueEquals(y, EqualityComparer<TValue>.Default);
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00003E08 File Offset: 0x00002008
		public static bool ValueEquals<TKey, TValue, TComparer>(this IDictionary<TKey, TValue> x, IDictionary<TKey, TValue> y, TComparer valueComparer) where TComparer : IEqualityComparer<TValue>
		{
			if (x == y)
			{
				return true;
			}
			if (x == null || y == null || x.Count != y.Count)
			{
				return false;
			}
			foreach (KeyValuePair<TKey, TValue> keyValuePair in x)
			{
				TValue tvalue;
				if (!y.TryGetValue(keyValuePair.Key, out tvalue) || !valueComparer.Equals(keyValuePair.Value, tvalue))
				{
					return false;
				}
			}
			return true;
		}
	}
}
