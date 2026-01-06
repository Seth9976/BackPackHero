using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x020000F6 RID: 246
	internal static class StyleCache
	{
		// Token: 0x0600079B RID: 1947 RVA: 0x0001BB58 File Offset: 0x00019D58
		public static bool TryGetValue(long hash, out ComputedStyle data)
		{
			return StyleCache.s_ComputedStyleCache.TryGetValue(hash, ref data);
		}

		// Token: 0x0600079C RID: 1948 RVA: 0x0001BB76 File Offset: 0x00019D76
		public static void SetValue(long hash, ref ComputedStyle data)
		{
			StyleCache.s_ComputedStyleCache[hash] = data;
		}

		// Token: 0x0600079D RID: 1949 RVA: 0x0001BB8C File Offset: 0x00019D8C
		public static bool TryGetValue(int hash, out StyleVariableContext data)
		{
			return StyleCache.s_StyleVariableContextCache.TryGetValue(hash, ref data);
		}

		// Token: 0x0600079E RID: 1950 RVA: 0x0001BBAA File Offset: 0x00019DAA
		public static void SetValue(int hash, StyleVariableContext data)
		{
			StyleCache.s_StyleVariableContextCache[hash] = data;
		}

		// Token: 0x0600079F RID: 1951 RVA: 0x0001BBBC File Offset: 0x00019DBC
		public static bool TryGetValue(int hash, out ComputedTransitionProperty[] data)
		{
			return StyleCache.s_ComputedTransitionsCache.TryGetValue(hash, ref data);
		}

		// Token: 0x060007A0 RID: 1952 RVA: 0x0001BBDA File Offset: 0x00019DDA
		public static void SetValue(int hash, ComputedTransitionProperty[] data)
		{
			StyleCache.s_ComputedTransitionsCache[hash] = data;
		}

		// Token: 0x060007A1 RID: 1953 RVA: 0x0001BBEC File Offset: 0x00019DEC
		public static void ClearStyleCache()
		{
			foreach (KeyValuePair<long, ComputedStyle> keyValuePair in StyleCache.s_ComputedStyleCache)
			{
				keyValuePair.Value.Release();
			}
			StyleCache.s_ComputedStyleCache.Clear();
			StyleCache.s_StyleVariableContextCache.Clear();
			StyleCache.s_ComputedTransitionsCache.Clear();
		}

		// Token: 0x0400031A RID: 794
		private static Dictionary<long, ComputedStyle> s_ComputedStyleCache = new Dictionary<long, ComputedStyle>();

		// Token: 0x0400031B RID: 795
		private static Dictionary<int, StyleVariableContext> s_StyleVariableContextCache = new Dictionary<int, StyleVariableContext>();

		// Token: 0x0400031C RID: 796
		private static Dictionary<int, ComputedTransitionProperty[]> s_ComputedTransitionsCache = new Dictionary<int, ComputedTransitionProperty[]>();
	}
}
