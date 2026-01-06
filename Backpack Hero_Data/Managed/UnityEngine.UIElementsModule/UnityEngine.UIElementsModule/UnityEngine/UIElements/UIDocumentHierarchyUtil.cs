using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x0200024D RID: 589
	internal static class UIDocumentHierarchyUtil
	{
		// Token: 0x060011AD RID: 4525 RVA: 0x000443EC File Offset: 0x000425EC
		internal static int FindHierarchicalSortedIndex(SortedDictionary<UIDocumentHierarchicalIndex, UIDocument> children, UIDocument child)
		{
			int num = 0;
			foreach (UIDocument uidocument in children.Values)
			{
				bool flag = uidocument == child;
				if (flag)
				{
					return num;
				}
				bool flag2 = uidocument.rootVisualElement != null && uidocument.rootVisualElement.parent != null;
				if (flag2)
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x060011AE RID: 4526 RVA: 0x0004447C File Offset: 0x0004267C
		internal static void SetHierarchicalIndex(Transform childTransform, Transform directParentTransform, Transform mainParentTransform, out UIDocumentHierarchicalIndex hierarchicalIndex)
		{
			bool flag = mainParentTransform == null || childTransform == null;
			if (flag)
			{
				hierarchicalIndex.pathToParent = null;
			}
			else
			{
				bool flag2 = directParentTransform == mainParentTransform;
				if (flag2)
				{
					hierarchicalIndex.pathToParent = new int[] { childTransform.GetSiblingIndex() };
				}
				else
				{
					List<int> list = new List<int>();
					while (mainParentTransform != childTransform && childTransform != null)
					{
						list.Add(childTransform.GetSiblingIndex());
						childTransform = childTransform.parent;
					}
					list.Reverse();
					hierarchicalIndex.pathToParent = list.ToArray();
				}
			}
		}

		// Token: 0x060011AF RID: 4527 RVA: 0x0004451C File Offset: 0x0004271C
		internal static void SetGlobalIndex(Transform objectTransform, Transform directParentTransform, out UIDocumentHierarchicalIndex globalIndex)
		{
			bool flag = objectTransform == null;
			if (flag)
			{
				globalIndex.pathToParent = null;
			}
			else
			{
				bool flag2 = directParentTransform == null;
				if (flag2)
				{
					globalIndex.pathToParent = new int[] { objectTransform.GetSiblingIndex() };
				}
				else
				{
					List<int> list = new List<int>();
					list.Add(objectTransform.GetSiblingIndex());
					List<int> list2 = list;
					while (directParentTransform != null)
					{
						list2.Add(directParentTransform.GetSiblingIndex());
						directParentTransform = directParentTransform.parent;
					}
					list2.Reverse();
					globalIndex.pathToParent = list2.ToArray();
				}
			}
		}

		// Token: 0x040007D6 RID: 2006
		internal static UIDocumentHierarchicalIndexComparer indexComparer = new UIDocumentHierarchicalIndexComparer();
	}
}
