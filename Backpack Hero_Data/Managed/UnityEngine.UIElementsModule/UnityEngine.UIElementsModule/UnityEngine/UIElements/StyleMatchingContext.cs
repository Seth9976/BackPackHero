using System;
using System.Collections.Generic;
using UnityEngine.UIElements.StyleSheets;

namespace UnityEngine.UIElements
{
	// Token: 0x020000F8 RID: 248
	internal class StyleMatchingContext
	{
		// Token: 0x1700018F RID: 399
		// (get) Token: 0x060007AB RID: 1963 RVA: 0x0001BF27 File Offset: 0x0001A127
		public int styleSheetCount
		{
			get
			{
				return this.m_StyleSheetStack.Count;
			}
		}

		// Token: 0x060007AC RID: 1964 RVA: 0x0001BF34 File Offset: 0x0001A134
		public StyleMatchingContext(Action<VisualElement, MatchResultInfo> processResult)
		{
			this.m_StyleSheetStack = new List<StyleSheet>();
			this.variableContext = StyleVariableContext.none;
			this.currentElement = null;
			this.processResult = processResult;
		}

		// Token: 0x060007AD RID: 1965 RVA: 0x0001BF64 File Offset: 0x0001A164
		public void AddStyleSheet(StyleSheet sheet)
		{
			bool flag = sheet == null;
			if (!flag)
			{
				this.m_StyleSheetStack.Add(sheet);
			}
		}

		// Token: 0x060007AE RID: 1966 RVA: 0x0001BF8C File Offset: 0x0001A18C
		public void RemoveStyleSheetRange(int index, int count)
		{
			this.m_StyleSheetStack.RemoveRange(index, count);
		}

		// Token: 0x060007AF RID: 1967 RVA: 0x0001BFA0 File Offset: 0x0001A1A0
		public StyleSheet GetStyleSheetAt(int index)
		{
			return this.m_StyleSheetStack[index];
		}

		// Token: 0x04000325 RID: 805
		private List<StyleSheet> m_StyleSheetStack;

		// Token: 0x04000326 RID: 806
		public StyleVariableContext variableContext;

		// Token: 0x04000327 RID: 807
		public VisualElement currentElement;

		// Token: 0x04000328 RID: 808
		public Action<VisualElement, MatchResultInfo> processResult;
	}
}
