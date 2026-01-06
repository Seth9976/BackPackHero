using System;
using UnityEngine.UIElements.Experimental;

namespace UnityEngine.UIElements
{
	// Token: 0x020001B4 RID: 436
	internal class ListViewDraggerAnimated : ListViewDragger
	{
		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x06000E00 RID: 3584 RVA: 0x0003937C File Offset: 0x0003757C
		public bool isDragging
		{
			get
			{
				return this.m_Item != null;
			}
		}

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x06000E01 RID: 3585 RVA: 0x00039387 File Offset: 0x00037587
		public ReusableCollectionItem draggedItem
		{
			get
			{
				return this.m_Item;
			}
		}

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x06000E02 RID: 3586 RVA: 0x00004D72 File Offset: 0x00002F72
		internal override bool supportsDragEvents
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000E03 RID: 3587 RVA: 0x0003938F File Offset: 0x0003758F
		public ListViewDraggerAnimated(BaseVerticalCollectionView listView)
			: base(listView)
		{
		}

		// Token: 0x06000E04 RID: 3588 RVA: 0x0003939C File Offset: 0x0003759C
		protected override StartDragArgs StartDrag(Vector3 pointerPosition)
		{
			base.targetListView.ClearSelection();
			ReusableCollectionItem recycledItem = base.GetRecycledItem(pointerPosition);
			bool flag = recycledItem == null;
			StartDragArgs startDragArgs;
			if (flag)
			{
				startDragArgs = null;
			}
			else
			{
				base.targetListView.SetSelection(recycledItem.index);
				this.m_Item = recycledItem;
				float y = this.m_Item.rootElement.layout.y;
				this.m_SelectionHeight = this.m_Item.rootElement.layout.height;
				this.m_Item.rootElement.style.position = Position.Absolute;
				this.m_Item.rootElement.style.height = this.m_Item.rootElement.layout.height;
				this.m_Item.rootElement.style.width = this.m_Item.rootElement.layout.width;
				this.m_Item.rootElement.style.top = y;
				this.m_DragStartIndex = this.m_Item.index;
				this.m_CurrentIndex = this.m_DragStartIndex;
				this.m_CurrentPointerPosition = pointerPosition;
				this.m_LocalOffsetOnStart = base.targetScrollView.contentContainer.WorldToLocal(pointerPosition).y - y;
				ReusableCollectionItem recycledItemFromIndex = base.targetListView.GetRecycledItemFromIndex(this.m_CurrentIndex + 1);
				bool flag2 = recycledItemFromIndex != null;
				if (flag2)
				{
					this.m_OffsetItem = recycledItemFromIndex;
					this.Animate(this.m_OffsetItem, this.m_SelectionHeight);
					this.m_OffsetItem.rootElement.style.paddingTop = this.m_SelectionHeight;
					bool flag3 = base.targetListView.virtualizationMethod == CollectionVirtualizationMethod.FixedHeight;
					if (flag3)
					{
						this.m_OffsetItem.rootElement.style.height = base.targetListView.fixedItemHeight + this.m_SelectionHeight;
					}
				}
				startDragArgs = base.dragAndDropController.SetupDragAndDrop(new int[] { this.m_Item.index }, true);
			}
			return startDragArgs;
		}

		// Token: 0x06000E05 RID: 3589 RVA: 0x000395CC File Offset: 0x000377CC
		protected override DragVisualMode UpdateDrag(Vector3 pointerPosition)
		{
			bool flag = this.m_Item == null;
			DragVisualMode dragVisualMode;
			if (flag)
			{
				dragVisualMode = DragVisualMode.Rejected;
			}
			else
			{
				base.HandleDragAndScroll(pointerPosition);
				this.m_CurrentPointerPosition = pointerPosition;
				Vector2 vector = base.targetScrollView.contentContainer.WorldToLocal(this.m_CurrentPointerPosition);
				Rect layout = this.m_Item.rootElement.layout;
				float height = base.targetScrollView.contentContainer.layout.height;
				layout.y = Mathf.Clamp(vector.y - this.m_LocalOffsetOnStart, 0f, height - this.m_SelectionHeight);
				float num = base.targetScrollView.contentContainer.resolvedStyle.paddingTop;
				this.m_CurrentIndex = -1;
				foreach (ReusableCollectionItem reusableCollectionItem in base.targetListView.activeItems)
				{
					bool flag2 = reusableCollectionItem.rootElement.style.display == DisplayStyle.None;
					if (!flag2)
					{
						ReusableCollectionItem reusableCollectionItem2 = reusableCollectionItem;
						bool flag3 = reusableCollectionItem2 == this.m_Item;
						if (!flag3)
						{
							float num2 = reusableCollectionItem2.rootElement.layout.height - reusableCollectionItem2.rootElement.resolvedStyle.paddingTop;
							bool flag4 = (!base.targetListView.sourceIncludesArraySize || reusableCollectionItem2.index != 0) && this.m_CurrentIndex == -1 && layout.y <= num + num2 * 0.5f;
							if (flag4)
							{
								this.m_CurrentIndex = reusableCollectionItem2.index;
								bool flag5 = this.m_OffsetItem == reusableCollectionItem2;
								if (flag5)
								{
									break;
								}
								this.Animate(this.m_OffsetItem, 0f);
								this.Animate(reusableCollectionItem2, this.m_SelectionHeight);
								this.m_OffsetItem = reusableCollectionItem2;
								break;
							}
							else
							{
								num += num2;
							}
						}
					}
				}
				bool flag6 = this.m_CurrentIndex == -1;
				if (flag6)
				{
					this.m_CurrentIndex = base.targetListView.itemsSource.Count;
					this.Animate(this.m_OffsetItem, 0f);
					this.m_OffsetItem = null;
				}
				this.m_Item.rootElement.layout = layout;
				this.m_Item.rootElement.BringToFront();
				dragVisualMode = DragVisualMode.Move;
			}
			return dragVisualMode;
		}

		// Token: 0x06000E06 RID: 3590 RVA: 0x00039858 File Offset: 0x00037A58
		private void Animate(ReusableCollectionItem element, float paddingTop)
		{
			bool flag = element == null;
			if (!flag)
			{
				bool flag2 = element.animator != null;
				if (flag2)
				{
					bool flag3 = (element.animator.isRunning && element.animator.to.paddingTop == paddingTop) || (!element.animator.isRunning && element.rootElement.style.paddingTop == paddingTop);
					if (flag3)
					{
						return;
					}
				}
				ValueAnimation<StyleValues> animator = element.animator;
				if (animator != null)
				{
					animator.Stop();
				}
				ValueAnimation<StyleValues> animator2 = element.animator;
				if (animator2 != null)
				{
					animator2.Recycle();
				}
				StyleValues styleValues = ((base.targetListView.virtualizationMethod == CollectionVirtualizationMethod.FixedHeight) ? new StyleValues
				{
					paddingTop = paddingTop,
					height = base.targetListView.ResolveItemHeight(-1f) + paddingTop
				} : new StyleValues
				{
					paddingTop = paddingTop
				});
				element.animator = element.rootElement.experimental.animation.Start(styleValues, 500);
				element.animator.KeepAlive();
			}
		}

		// Token: 0x06000E07 RID: 3591 RVA: 0x00039980 File Offset: 0x00037B80
		protected override void OnDrop(Vector3 pointerPosition)
		{
			bool flag = this.m_Item != null && base.targetListView.binding == null;
			if (flag)
			{
				base.targetListView.virtualizationController.ReplaceActiveItem(this.m_Item.index);
			}
			bool flag2 = this.m_OffsetItem != null;
			if (flag2)
			{
				ValueAnimation<StyleValues> animator = this.m_OffsetItem.animator;
				if (animator != null)
				{
					animator.Stop();
				}
				ValueAnimation<StyleValues> animator2 = this.m_OffsetItem.animator;
				if (animator2 != null)
				{
					animator2.Recycle();
				}
				this.m_OffsetItem.animator = null;
				this.m_OffsetItem.rootElement.style.paddingTop = 0f;
				bool flag3 = base.targetListView.virtualizationMethod == CollectionVirtualizationMethod.FixedHeight;
				if (flag3)
				{
					this.m_OffsetItem.rootElement.style.height = base.targetListView.ResolveItemHeight(-1f);
				}
			}
			base.OnDrop(pointerPosition);
			bool flag4 = this.m_Item != null && base.targetListView.binding != null;
			if (flag4)
			{
				base.targetListView.virtualizationController.ReplaceActiveItem(this.m_Item.index);
			}
			this.m_OffsetItem = null;
			this.m_Item = null;
		}

		// Token: 0x06000E08 RID: 3592 RVA: 0x000020E6 File Offset: 0x000002E6
		protected override void ClearDragAndDropUI()
		{
		}

		// Token: 0x06000E09 RID: 3593 RVA: 0x00039AC4 File Offset: 0x00037CC4
		protected override bool TryGetDragPosition(Vector2 pointerPosition, ref ListViewDragger.DragPosition dragPosition)
		{
			dragPosition.recycledItem = this.m_Item;
			dragPosition.insertAtIndex = this.m_CurrentIndex;
			dragPosition.dragAndDropPosition = DragAndDropPosition.BetweenItems;
			return true;
		}

		// Token: 0x04000671 RID: 1649
		private int m_DragStartIndex;

		// Token: 0x04000672 RID: 1650
		private int m_CurrentIndex;

		// Token: 0x04000673 RID: 1651
		private float m_SelectionHeight;

		// Token: 0x04000674 RID: 1652
		private float m_LocalOffsetOnStart;

		// Token: 0x04000675 RID: 1653
		private Vector3 m_CurrentPointerPosition;

		// Token: 0x04000676 RID: 1654
		private ReusableCollectionItem m_Item;

		// Token: 0x04000677 RID: 1655
		private ReusableCollectionItem m_OffsetItem;
	}
}
