using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x020001A2 RID: 418
	internal abstract class BaseReorderableDragAndDropController : ICollectionDragAndDropController, IDragAndDropController<IListDragAndDropArgs>, IReorderable
	{
		// Token: 0x06000DA3 RID: 3491 RVA: 0x00038103 File Offset: 0x00036303
		public BaseReorderableDragAndDropController(BaseVerticalCollectionView view)
		{
			this.m_View = view;
			this.enableReordering = true;
		}

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x06000DA4 RID: 3492 RVA: 0x0003811C File Offset: 0x0003631C
		// (set) Token: 0x06000DA5 RID: 3493 RVA: 0x00038124 File Offset: 0x00036324
		public bool enableReordering { get; set; }

		// Token: 0x06000DA6 RID: 3494 RVA: 0x00038130 File Offset: 0x00036330
		public virtual bool CanStartDrag(IEnumerable<int> itemIndices)
		{
			return this.enableReordering;
		}

		// Token: 0x06000DA7 RID: 3495 RVA: 0x00038148 File Offset: 0x00036348
		public virtual StartDragArgs SetupDragAndDrop(IEnumerable<int> itemIndices, bool skipText = false)
		{
			if (this.m_SelectedIndices == null)
			{
				this.m_SelectedIndices = new List<int>();
			}
			this.m_SelectedIndices.Clear();
			string text = string.Empty;
			bool flag = itemIndices != null;
			if (flag)
			{
				foreach (int num in itemIndices)
				{
					this.m_SelectedIndices.Add(num);
					bool flag2 = skipText;
					if (!flag2)
					{
						bool flag3 = string.IsNullOrEmpty(text);
						if (flag3)
						{
							ReusableCollectionItem recycledItemFromIndex = this.m_View.GetRecycledItemFromIndex(num);
							Label label = ((recycledItemFromIndex != null) ? recycledItemFromIndex.rootElement.Q(null, null) : null);
							text = ((label != null) ? label.text : string.Format("Item {0}", num));
						}
						else
						{
							text = "<Multiple>";
							skipText = true;
						}
					}
				}
			}
			this.m_SelectedIndices.Sort();
			return new StartDragArgs(text, this.m_View);
		}

		// Token: 0x06000DA8 RID: 3496
		public abstract DragVisualMode HandleDragAndDrop(IListDragAndDropArgs args);

		// Token: 0x06000DA9 RID: 3497
		public abstract void OnDrop(IListDragAndDropArgs args);

		// Token: 0x04000646 RID: 1606
		protected readonly BaseVerticalCollectionView m_View;

		// Token: 0x04000647 RID: 1607
		protected List<int> m_SelectedIndices;
	}
}
