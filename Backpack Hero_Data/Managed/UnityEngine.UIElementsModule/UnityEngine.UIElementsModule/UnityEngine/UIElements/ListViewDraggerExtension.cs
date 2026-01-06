using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001B3 RID: 435
	internal static class ListViewDraggerExtension
	{
		// Token: 0x06000DFF RID: 3583 RVA: 0x00039310 File Offset: 0x00037510
		public static ReusableCollectionItem GetRecycledItemFromIndex(this BaseVerticalCollectionView listView, int index)
		{
			foreach (ReusableCollectionItem reusableCollectionItem in listView.activeItems)
			{
				bool flag = reusableCollectionItem.index.Equals(index);
				if (flag)
				{
					return reusableCollectionItem;
				}
			}
			return null;
		}
	}
}
