using System;

namespace UnityEngine.UIElements.StyleSheets
{
	// Token: 0x0200034E RID: 846
	internal abstract class HierarchyTraversal
	{
		// Token: 0x06001B0C RID: 6924 RVA: 0x0007A8A5 File Offset: 0x00078AA5
		public virtual void Traverse(VisualElement element)
		{
			this.TraverseRecursive(element, 0);
		}

		// Token: 0x06001B0D RID: 6925
		public abstract void TraverseRecursive(VisualElement element, int depth);

		// Token: 0x06001B0E RID: 6926 RVA: 0x0007A8B4 File Offset: 0x00078AB4
		protected void Recurse(VisualElement element, int depth)
		{
			int i = 0;
			while (i < element.hierarchy.childCount)
			{
				VisualElement visualElement = element.hierarchy[i];
				this.TraverseRecursive(visualElement, depth + 1);
				bool flag = visualElement.hierarchy.parent != element;
				if (!flag)
				{
					i++;
				}
			}
		}
	}
}
