using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x020001B5 RID: 437
	internal class ListViewReorderableDragAndDropController : BaseReorderableDragAndDropController
	{
		// Token: 0x06000E0A RID: 3594 RVA: 0x00039AF6 File Offset: 0x00037CF6
		public ListViewReorderableDragAndDropController(ListView view)
			: base(view)
		{
			this.m_ListView = view;
		}

		// Token: 0x06000E0B RID: 3595 RVA: 0x00039B08 File Offset: 0x00037D08
		public override DragVisualMode HandleDragAndDrop(IListDragAndDropArgs args)
		{
			bool flag = args.dragAndDropPosition == DragAndDropPosition.OverItem || !base.enableReordering;
			DragVisualMode dragVisualMode;
			if (flag)
			{
				dragVisualMode = DragVisualMode.Rejected;
			}
			else
			{
				dragVisualMode = ((args.dragAndDropData.userData == this.m_ListView) ? DragVisualMode.Move : DragVisualMode.Rejected);
			}
			return dragVisualMode;
		}

		// Token: 0x06000E0C RID: 3596 RVA: 0x00039B50 File Offset: 0x00037D50
		public override void OnDrop(IListDragAndDropArgs args)
		{
			int insertAtIndex = args.insertAtIndex;
			int num = 0;
			int num2 = 0;
			for (int i = this.m_SelectedIndices.Count - 1; i >= 0; i--)
			{
				int num3 = this.m_SelectedIndices[i];
				bool flag = num3 < 0;
				if (!flag)
				{
					int num4 = insertAtIndex - num;
					bool flag2 = num3 > insertAtIndex;
					if (flag2)
					{
						num3 += num2;
						num2++;
					}
					else
					{
						bool flag3 = num3 < num4;
						if (flag3)
						{
							num++;
							num4--;
						}
					}
					this.m_ListView.viewController.Move(num3, num4);
				}
			}
			bool flag4 = this.m_ListView.selectionType > SelectionType.None;
			if (flag4)
			{
				List<int> list = new List<int>();
				for (int j = 0; j < this.m_SelectedIndices.Count; j++)
				{
					list.Add(insertAtIndex - num + j);
				}
				this.m_ListView.SetSelectionWithoutNotify(list);
			}
			else
			{
				this.m_ListView.ClearSelection();
			}
		}

		// Token: 0x04000678 RID: 1656
		protected readonly ListView m_ListView;
	}
}
