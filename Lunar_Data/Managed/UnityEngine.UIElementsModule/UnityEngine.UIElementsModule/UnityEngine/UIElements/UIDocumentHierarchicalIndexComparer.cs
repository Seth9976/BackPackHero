using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x0200024E RID: 590
	internal class UIDocumentHierarchicalIndexComparer : IComparer<UIDocumentHierarchicalIndex>
	{
		// Token: 0x060011B1 RID: 4529 RVA: 0x000445BC File Offset: 0x000427BC
		public int Compare(UIDocumentHierarchicalIndex x, UIDocumentHierarchicalIndex y)
		{
			return x.CompareTo(y);
		}
	}
}
