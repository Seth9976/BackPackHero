using System;

namespace UnityEngine.UIElements.StyleSheets
{
	// Token: 0x0200035F RID: 863
	internal struct SelectorMatchRecord
	{
		// Token: 0x06001BBB RID: 7099 RVA: 0x0008073E File Offset: 0x0007E93E
		public SelectorMatchRecord(StyleSheet sheet, int styleSheetIndexInStack)
		{
			this = default(SelectorMatchRecord);
			this.sheet = sheet;
			this.styleSheetIndexInStack = styleSheetIndexInStack;
		}

		// Token: 0x06001BBC RID: 7100 RVA: 0x00080758 File Offset: 0x0007E958
		public static int Compare(SelectorMatchRecord a, SelectorMatchRecord b)
		{
			bool flag = a.sheet.isDefaultStyleSheet != b.sheet.isDefaultStyleSheet;
			int num;
			if (flag)
			{
				num = (a.sheet.isDefaultStyleSheet ? (-1) : 1);
			}
			else
			{
				int num2 = a.complexSelector.specificity.CompareTo(b.complexSelector.specificity);
				bool flag2 = num2 == 0;
				if (flag2)
				{
					num2 = a.styleSheetIndexInStack.CompareTo(b.styleSheetIndexInStack);
				}
				bool flag3 = num2 == 0;
				if (flag3)
				{
					num2 = a.complexSelector.orderInStyleSheet.CompareTo(b.complexSelector.orderInStyleSheet);
				}
				num = num2;
			}
			return num;
		}

		// Token: 0x04000DBF RID: 3519
		public StyleSheet sheet;

		// Token: 0x04000DC0 RID: 3520
		public int styleSheetIndexInStack;

		// Token: 0x04000DC1 RID: 3521
		public StyleComplexSelector complexSelector;
	}
}
