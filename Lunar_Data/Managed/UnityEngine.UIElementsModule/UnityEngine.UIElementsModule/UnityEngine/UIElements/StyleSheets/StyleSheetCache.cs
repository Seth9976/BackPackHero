using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements.StyleSheets
{
	// Token: 0x02000364 RID: 868
	internal static class StyleSheetCache
	{
		// Token: 0x06001BDB RID: 7131 RVA: 0x00081345 File Offset: 0x0007F545
		internal static void ClearCaches()
		{
			StyleSheetCache.s_RulePropertyIdsCache.Clear();
		}

		// Token: 0x06001BDC RID: 7132 RVA: 0x00081354 File Offset: 0x0007F554
		internal static StylePropertyId[] GetPropertyIds(StyleSheet sheet, int ruleIndex)
		{
			StyleSheetCache.SheetHandleKey sheetHandleKey = new StyleSheetCache.SheetHandleKey(sheet, ruleIndex);
			StylePropertyId[] array;
			bool flag = !StyleSheetCache.s_RulePropertyIdsCache.TryGetValue(sheetHandleKey, ref array);
			if (flag)
			{
				StyleRule styleRule = sheet.rules[ruleIndex];
				array = new StylePropertyId[styleRule.properties.Length];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = StyleSheetCache.GetPropertyId(styleRule, i);
				}
				StyleSheetCache.s_RulePropertyIdsCache.Add(sheetHandleKey, array);
			}
			return array;
		}

		// Token: 0x06001BDD RID: 7133 RVA: 0x000813D4 File Offset: 0x0007F5D4
		internal static StylePropertyId[] GetPropertyIds(StyleRule rule)
		{
			StylePropertyId[] array = new StylePropertyId[rule.properties.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = StyleSheetCache.GetPropertyId(rule, i);
			}
			return array;
		}

		// Token: 0x06001BDE RID: 7134 RVA: 0x00081414 File Offset: 0x0007F614
		private static StylePropertyId GetPropertyId(StyleRule rule, int index)
		{
			StyleProperty styleProperty = rule.properties[index];
			string name = styleProperty.name;
			StylePropertyId stylePropertyId;
			bool flag = !StylePropertyUtil.s_NameToId.TryGetValue(name, ref stylePropertyId);
			if (flag)
			{
				stylePropertyId = (styleProperty.isCustomProperty ? StylePropertyId.Custom : StylePropertyId.Unknown);
			}
			return stylePropertyId;
		}

		// Token: 0x04000DD8 RID: 3544
		private static StyleSheetCache.SheetHandleKeyComparer s_Comparer = new StyleSheetCache.SheetHandleKeyComparer();

		// Token: 0x04000DD9 RID: 3545
		private static Dictionary<StyleSheetCache.SheetHandleKey, StylePropertyId[]> s_RulePropertyIdsCache = new Dictionary<StyleSheetCache.SheetHandleKey, StylePropertyId[]>(StyleSheetCache.s_Comparer);

		// Token: 0x02000365 RID: 869
		private struct SheetHandleKey
		{
			// Token: 0x06001BE0 RID: 7136 RVA: 0x00081477 File Offset: 0x0007F677
			public SheetHandleKey(StyleSheet sheet, int index)
			{
				this.sheetInstanceID = sheet.GetInstanceID();
				this.index = index;
			}

			// Token: 0x04000DDA RID: 3546
			public readonly int sheetInstanceID;

			// Token: 0x04000DDB RID: 3547
			public readonly int index;
		}

		// Token: 0x02000366 RID: 870
		private class SheetHandleKeyComparer : IEqualityComparer<StyleSheetCache.SheetHandleKey>
		{
			// Token: 0x06001BE1 RID: 7137 RVA: 0x00081490 File Offset: 0x0007F690
			public bool Equals(StyleSheetCache.SheetHandleKey x, StyleSheetCache.SheetHandleKey y)
			{
				return x.sheetInstanceID == y.sheetInstanceID && x.index == y.index;
			}

			// Token: 0x06001BE2 RID: 7138 RVA: 0x000814C4 File Offset: 0x0007F6C4
			public int GetHashCode(StyleSheetCache.SheetHandleKey key)
			{
				return key.sheetInstanceID.GetHashCode() ^ key.index.GetHashCode();
			}
		}
	}
}
