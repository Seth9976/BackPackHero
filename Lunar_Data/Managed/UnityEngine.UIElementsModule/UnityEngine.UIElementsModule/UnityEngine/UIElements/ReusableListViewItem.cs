using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000111 RID: 273
	internal class ReusableListViewItem : ReusableCollectionItem
	{
		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x060008C2 RID: 2242 RVA: 0x0002150E File Offset: 0x0001F70E
		public override VisualElement rootElement
		{
			get
			{
				return this.m_Container ?? base.bindableElement;
			}
		}

		// Token: 0x060008C3 RID: 2243 RVA: 0x00021520 File Offset: 0x0001F720
		public void Init(VisualElement item, bool usesAnimatedDragger)
		{
			base.Init(item);
			this.UpdateHierarchy(usesAnimatedDragger);
		}

		// Token: 0x060008C4 RID: 2244 RVA: 0x00021534 File Offset: 0x0001F734
		private void UpdateHierarchy(bool usesAnimatedDragger)
		{
			if (usesAnimatedDragger)
			{
				bool flag = this.m_Container != null;
				if (!flag)
				{
					this.m_Container = new VisualElement
					{
						name = ListView.reorderableItemUssClassName
					};
					this.m_Container.AddToClassList(ListView.reorderableItemUssClassName);
					this.m_DragHandle = new VisualElement
					{
						name = ListView.reorderableItemHandleUssClassName
					};
					this.m_DragHandle.AddToClassList(ListView.reorderableItemHandleUssClassName);
					VisualElement visualElement = new VisualElement
					{
						name = ListView.reorderableItemHandleBarUssClassName
					};
					visualElement.AddToClassList(ListView.reorderableItemHandleBarUssClassName);
					this.m_DragHandle.Add(visualElement);
					VisualElement visualElement2 = new VisualElement
					{
						name = ListView.reorderableItemHandleBarUssClassName
					};
					visualElement2.AddToClassList(ListView.reorderableItemHandleBarUssClassName);
					this.m_DragHandle.Add(visualElement2);
					this.m_ItemContainer = new VisualElement
					{
						name = ListView.reorderableItemContainerUssClassName
					};
					this.m_ItemContainer.AddToClassList(ListView.reorderableItemContainerUssClassName);
					this.m_ItemContainer.Add(base.bindableElement);
					this.m_Container.Add(this.m_DragHandle);
					this.m_Container.Add(this.m_ItemContainer);
				}
			}
			else
			{
				bool flag2 = this.m_Container == null;
				if (!flag2)
				{
					this.m_Container.RemoveFromHierarchy();
					this.m_Container = null;
				}
			}
		}

		// Token: 0x060008C5 RID: 2245 RVA: 0x0002168C File Offset: 0x0001F88C
		public void UpdateDragHandle(bool needsDragHandle)
		{
			if (needsDragHandle)
			{
				bool flag = this.m_DragHandle.parent == null;
				if (flag)
				{
					this.rootElement.Insert(0, this.m_DragHandle);
					this.rootElement.AddToClassList(ListView.reorderableItemUssClassName);
				}
			}
			else
			{
				VisualElement dragHandle = this.m_DragHandle;
				bool flag2 = ((dragHandle != null) ? dragHandle.parent : null) != null;
				if (flag2)
				{
					this.m_DragHandle.RemoveFromHierarchy();
					this.rootElement.RemoveFromClassList(ListView.reorderableItemUssClassName);
				}
			}
		}

		// Token: 0x060008C6 RID: 2246 RVA: 0x00021715 File Offset: 0x0001F915
		public override void PreAttachElement()
		{
			base.PreAttachElement();
			this.rootElement.AddToClassList(ListView.itemUssClassName);
		}

		// Token: 0x060008C7 RID: 2247 RVA: 0x00021730 File Offset: 0x0001F930
		public override void DetachElement()
		{
			base.DetachElement();
			this.rootElement.RemoveFromClassList(ListView.itemUssClassName);
		}

		// Token: 0x04000392 RID: 914
		private VisualElement m_Container;

		// Token: 0x04000393 RID: 915
		private VisualElement m_DragHandle;

		// Token: 0x04000394 RID: 916
		private VisualElement m_ItemContainer;
	}
}
