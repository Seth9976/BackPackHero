using System;
using System.Collections.Generic;

namespace Unity.VisualScripting
{
	// Token: 0x020000BE RID: 190
	public static class DictionaryPool<TKey, TValue>
	{
		// Token: 0x060004B3 RID: 1203 RVA: 0x0000A64C File Offset: 0x0000884C
		public static Dictionary<TKey, TValue> New(Dictionary<TKey, TValue> source = null)
		{
			object obj = DictionaryPool<TKey, TValue>.@lock;
			Dictionary<TKey, TValue> dictionary2;
			lock (obj)
			{
				if (DictionaryPool<TKey, TValue>.free.Count == 0)
				{
					DictionaryPool<TKey, TValue>.free.Push(new Dictionary<TKey, TValue>());
				}
				Dictionary<TKey, TValue> dictionary = DictionaryPool<TKey, TValue>.free.Pop();
				DictionaryPool<TKey, TValue>.busy.Add(dictionary);
				if (source != null)
				{
					foreach (KeyValuePair<TKey, TValue> keyValuePair in source)
					{
						dictionary.Add(keyValuePair.Key, keyValuePair.Value);
					}
				}
				dictionary2 = dictionary;
			}
			return dictionary2;
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x0000A70C File Offset: 0x0000890C
		public static void Free(Dictionary<TKey, TValue> dictionary)
		{
			object obj = DictionaryPool<TKey, TValue>.@lock;
			lock (obj)
			{
				if (!DictionaryPool<TKey, TValue>.busy.Contains(dictionary))
				{
					throw new ArgumentException("The dictionary to free is not in use by the pool.", "dictionary");
				}
				dictionary.Clear();
				DictionaryPool<TKey, TValue>.busy.Remove(dictionary);
				DictionaryPool<TKey, TValue>.free.Push(dictionary);
			}
		}

		// Token: 0x04000105 RID: 261
		private static readonly object @lock = new object();

		// Token: 0x04000106 RID: 262
		private static readonly Stack<Dictionary<TKey, TValue>> free = new Stack<Dictionary<TKey, TValue>>();

		// Token: 0x04000107 RID: 263
		private static readonly HashSet<Dictionary<TKey, TValue>> busy = new HashSet<Dictionary<TKey, TValue>>();
	}
}
