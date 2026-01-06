using System;
using UnityEngine.UIElements.Experimental;

namespace UnityEngine.UIElements
{
	// Token: 0x020001B6 RID: 438
	internal class TreeViewReorderableDragAndDropController : BaseReorderableDragAndDropController
	{
		// Token: 0x06000E0D RID: 3597 RVA: 0x00039C5D File Offset: 0x00037E5D
		public TreeViewReorderableDragAndDropController(TreeView view)
			: base(view)
		{
			this.m_TreeView = view;
			base.enableReordering = true;
		}

		// Token: 0x06000E0E RID: 3598 RVA: 0x00039C78 File Offset: 0x00037E78
		public override DragVisualMode HandleDragAndDrop(IListDragAndDropArgs args)
		{
			bool flag = !base.enableReordering;
			DragVisualMode dragVisualMode;
			if (flag)
			{
				dragVisualMode = DragVisualMode.Rejected;
			}
			else
			{
				dragVisualMode = ((args.dragAndDropData.userData == this.m_TreeView) ? DragVisualMode.Move : DragVisualMode.Rejected);
			}
			return dragVisualMode;
		}

		// Token: 0x06000E0F RID: 3599 RVA: 0x00039CB4 File Offset: 0x00037EB4
		public override void OnDrop(IListDragAndDropArgs args)
		{
			int idForIndex = this.m_TreeView.GetIdForIndex(args.insertAtIndex);
			int parentIdForIndex = this.m_TreeView.GetParentIdForIndex(args.insertAtIndex);
			int childIndexForId = this.m_TreeView.viewController.GetChildIndexForId(idForIndex);
			bool flag = args.dragAndDropPosition == DragAndDropPosition.OverItem || (idForIndex == -1 && parentIdForIndex == -1 && childIndexForId == -1);
			if (flag)
			{
				for (int i = 0; i < this.m_SelectedIndices.Count; i++)
				{
					int num = this.m_SelectedIndices[i];
					int idForIndex2 = this.m_TreeView.GetIdForIndex(num);
					int num2 = idForIndex;
					int num3 = -1;
					this.m_TreeView.viewController.Move(idForIndex2, num2, num3);
				}
			}
			else
			{
				for (int j = this.m_SelectedIndices.Count - 1; j >= 0; j--)
				{
					int num4 = this.m_SelectedIndices[j];
					int idForIndex3 = this.m_TreeView.GetIdForIndex(num4);
					int num5 = parentIdForIndex;
					int num6 = childIndexForId;
					this.m_TreeView.viewController.Move(idForIndex3, num5, num6);
				}
			}
			this.m_TreeView.viewController.RebuildTree();
			this.m_TreeView.RefreshItems();
		}

		// Token: 0x04000679 RID: 1657
		protected readonly TreeView m_TreeView;
	}
}
