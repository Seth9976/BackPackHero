using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x020000F1 RID: 241
	internal static class VisualElementUtils
	{
		// Token: 0x06000785 RID: 1925 RVA: 0x0001B5CC File Offset: 0x000197CC
		public static string GetUniqueName(string nameBase)
		{
			string text = nameBase;
			int num = 2;
			while (VisualElementUtils.s_usedNames.Contains(text))
			{
				text = nameBase + num.ToString();
				num++;
			}
			VisualElementUtils.s_usedNames.Add(text);
			return text;
		}

		// Token: 0x06000786 RID: 1926 RVA: 0x0001B614 File Offset: 0x00019814
		internal static int GetFoldoutDepth(this VisualElement element)
		{
			int num = 0;
			bool flag = element.parent != null;
			if (flag)
			{
				for (VisualElement visualElement = element.parent; visualElement != null; visualElement = visualElement.parent)
				{
					bool flag2 = VisualElementUtils.s_FoldoutType.IsAssignableFrom(visualElement.GetType());
					if (flag2)
					{
						num++;
					}
				}
			}
			return num;
		}

		// Token: 0x06000787 RID: 1927 RVA: 0x0001B670 File Offset: 0x00019870
		internal static int GetListAndFoldoutDepth(this VisualElement element)
		{
			int num = 0;
			bool flag = element.hierarchy.parent != null;
			if (flag)
			{
				for (VisualElement visualElement = element.hierarchy.parent; visualElement != null; visualElement = visualElement.hierarchy.parent)
				{
					bool flag2 = visualElement is Foldout || visualElement is ListView;
					if (flag2)
					{
						num++;
					}
				}
			}
			return num;
		}

		// Token: 0x04000304 RID: 772
		private static readonly HashSet<string> s_usedNames = new HashSet<string>();

		// Token: 0x04000305 RID: 773
		private static readonly Type s_FoldoutType = typeof(Foldout);
	}
}
