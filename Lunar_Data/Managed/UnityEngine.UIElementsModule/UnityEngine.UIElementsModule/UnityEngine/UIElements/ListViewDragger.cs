using System;
using System.Linq;

namespace UnityEngine.UIElements
{
	// Token: 0x020001B1 RID: 433
	internal class ListViewDragger : DragEventsProcessor
	{
		// Token: 0x170002ED RID: 749
		// (get) Token: 0x06000DE9 RID: 3561 RVA: 0x000388BC File Offset: 0x00036ABC
		protected BaseVerticalCollectionView targetListView
		{
			get
			{
				return this.m_Target as BaseVerticalCollectionView;
			}
		}

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x06000DEA RID: 3562 RVA: 0x000388DC File Offset: 0x00036ADC
		protected ScrollView targetScrollView
		{
			get
			{
				return this.targetListView.scrollView;
			}
		}

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x06000DEB RID: 3563 RVA: 0x000388F9 File Offset: 0x00036AF9
		// (set) Token: 0x06000DEC RID: 3564 RVA: 0x00038901 File Offset: 0x00036B01
		public ICollectionDragAndDropController dragAndDropController { get; set; }

		// Token: 0x06000DED RID: 3565 RVA: 0x0003890A File Offset: 0x00036B0A
		public ListViewDragger(BaseVerticalCollectionView listView)
			: base(listView)
		{
		}

		// Token: 0x06000DEE RID: 3566 RVA: 0x00038918 File Offset: 0x00036B18
		protected override bool CanStartDrag(Vector3 pointerPosition)
		{
			bool flag = this.dragAndDropController == null;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				bool flag3 = !this.targetScrollView.contentContainer.worldBound.Contains(pointerPosition);
				if (flag3)
				{
					flag2 = false;
				}
				else
				{
					bool flag4 = Enumerable.Any<int>(this.targetListView.selectedIndices);
					if (flag4)
					{
						flag2 = this.dragAndDropController.CanStartDrag(this.targetListView.selectedIndices);
					}
					else
					{
						ReusableCollectionItem recycledItem = this.GetRecycledItem(pointerPosition);
						flag2 = recycledItem != null && this.dragAndDropController.CanStartDrag(new int[] { recycledItem.index });
					}
				}
			}
			return flag2;
		}

		// Token: 0x06000DEF RID: 3567 RVA: 0x000389B8 File Offset: 0x00036BB8
		protected override StartDragArgs StartDrag(Vector3 pointerPosition)
		{
			bool flag = Enumerable.Any<int>(this.targetListView.selectedIndices);
			StartDragArgs startDragArgs;
			if (flag)
			{
				startDragArgs = this.dragAndDropController.SetupDragAndDrop(this.targetListView.selectedIndices, false);
			}
			else
			{
				ReusableCollectionItem recycledItem = this.GetRecycledItem(pointerPosition);
				bool flag2 = recycledItem == null;
				if (flag2)
				{
					startDragArgs = null;
				}
				else
				{
					startDragArgs = this.dragAndDropController.SetupDragAndDrop(new int[] { recycledItem.index }, false);
				}
			}
			return startDragArgs;
		}

		// Token: 0x06000DF0 RID: 3568 RVA: 0x00038A28 File Offset: 0x00036C28
		protected override DragVisualMode UpdateDrag(Vector3 pointerPosition)
		{
			ListViewDragger.DragPosition dragPosition = default(ListViewDragger.DragPosition);
			DragVisualMode visualMode = this.GetVisualMode(pointerPosition, ref dragPosition);
			bool flag = visualMode == DragVisualMode.Rejected;
			if (flag)
			{
				this.ClearDragAndDropUI();
			}
			else
			{
				this.ApplyDragAndDropUI(dragPosition);
			}
			return visualMode;
		}

		// Token: 0x06000DF1 RID: 3569 RVA: 0x00038A68 File Offset: 0x00036C68
		private DragVisualMode GetVisualMode(Vector3 pointerPosition, ref ListViewDragger.DragPosition dragPosition)
		{
			bool flag = this.dragAndDropController == null;
			DragVisualMode dragVisualMode;
			if (flag)
			{
				dragVisualMode = DragVisualMode.Rejected;
			}
			else
			{
				this.HandleDragAndScroll(pointerPosition);
				bool flag2 = !this.TryGetDragPosition(pointerPosition, ref dragPosition);
				if (flag2)
				{
					dragVisualMode = DragVisualMode.Rejected;
				}
				else
				{
					ListDragAndDropArgs listDragAndDropArgs = this.MakeDragAndDropArgs(dragPosition);
					dragVisualMode = this.dragAndDropController.HandleDragAndDrop(listDragAndDropArgs);
				}
			}
			return dragVisualMode;
		}

		// Token: 0x06000DF2 RID: 3570 RVA: 0x00038AD0 File Offset: 0x00036CD0
		protected override void OnDrop(Vector3 pointerPosition)
		{
			ListViewDragger.DragPosition dragPosition = default(ListViewDragger.DragPosition);
			bool flag = !this.TryGetDragPosition(pointerPosition, ref dragPosition);
			if (!flag)
			{
				ListDragAndDropArgs listDragAndDropArgs = this.MakeDragAndDropArgs(dragPosition);
				bool flag2 = this.dragAndDropController.HandleDragAndDrop(listDragAndDropArgs) != DragVisualMode.Rejected;
				if (flag2)
				{
					this.dragAndDropController.OnDrop(listDragAndDropArgs);
				}
			}
		}

		// Token: 0x06000DF3 RID: 3571 RVA: 0x00038B34 File Offset: 0x00036D34
		internal void HandleDragAndScroll(Vector2 pointerPosition)
		{
			bool flag = pointerPosition.y < this.targetScrollView.worldBound.yMin + 5f;
			bool flag2 = pointerPosition.y > this.targetScrollView.worldBound.yMax - 5f;
			bool flag3 = flag || flag2;
			if (flag3)
			{
				Vector2 vector = this.targetScrollView.scrollOffset + (flag ? Vector2.down : Vector2.up) * 20f;
				vector.y = Mathf.Clamp(vector.y, 0f, Mathf.Max(0f, this.targetScrollView.contentContainer.worldBound.height - this.targetScrollView.contentViewport.worldBound.height));
				this.targetScrollView.scrollOffset = vector;
			}
		}

		// Token: 0x06000DF4 RID: 3572 RVA: 0x00038C20 File Offset: 0x00036E20
		protected void ApplyDragAndDropUI(ListViewDragger.DragPosition dragPosition)
		{
			bool flag = this.m_LastDragPosition.Equals(dragPosition);
			if (!flag)
			{
				bool flag2 = this.m_DragHoverBar == null;
				if (flag2)
				{
					this.m_DragHoverBar = new VisualElement();
					this.m_DragHoverBar.AddToClassList(BaseVerticalCollectionView.dragHoverBarUssClassName);
					this.m_DragHoverBar.style.width = this.targetListView.localBound.width;
					this.m_DragHoverBar.style.visibility = Visibility.Hidden;
					this.m_DragHoverBar.pickingMode = PickingMode.Ignore;
					this.targetListView.RegisterCallback<GeometryChangedEvent>(delegate(GeometryChangedEvent e)
					{
						this.m_DragHoverBar.style.width = this.targetListView.localBound.width;
					}, TrickleDown.NoTrickleDown);
					this.targetScrollView.contentViewport.Add(this.m_DragHoverBar);
				}
				this.ClearDragAndDropUI();
				this.m_LastDragPosition = dragPosition;
				switch (dragPosition.dragAndDropPosition)
				{
				case DragAndDropPosition.OverItem:
					dragPosition.recycledItem.rootElement.AddToClassList(BaseVerticalCollectionView.itemDragHoverUssClassName);
					break;
				case DragAndDropPosition.BetweenItems:
				{
					bool flag3 = dragPosition.insertAtIndex == 0;
					if (flag3)
					{
						this.PlaceHoverBarAt(0f);
					}
					else
					{
						ReusableCollectionItem reusableCollectionItem = this.targetListView.GetRecycledItemFromIndex(dragPosition.insertAtIndex - 1);
						if (reusableCollectionItem == null)
						{
							reusableCollectionItem = this.targetListView.GetRecycledItemFromIndex(dragPosition.insertAtIndex);
						}
						this.PlaceHoverBarAtElement(reusableCollectionItem.rootElement);
					}
					break;
				}
				case DragAndDropPosition.OutsideItems:
				{
					ReusableCollectionItem recycledItemFromIndex = this.targetListView.GetRecycledItemFromIndex(this.targetListView.itemsSource.Count - 1);
					bool flag4 = recycledItemFromIndex != null;
					if (flag4)
					{
						this.PlaceHoverBarAtElement(recycledItemFromIndex.rootElement);
					}
					else
					{
						bool flag5 = this.targetListView.sourceIncludesArraySize && this.targetListView.itemsSource.Count > 0;
						if (flag5)
						{
							this.PlaceHoverBarAtElement(this.targetListView.GetRecycledItemFromIndex(0).rootElement);
						}
						else
						{
							this.PlaceHoverBarAt(0f);
						}
					}
					break;
				}
				default:
					throw new ArgumentOutOfRangeException("dragAndDropPosition", dragPosition.dragAndDropPosition, "Unsupported dragAndDropPosition value");
				}
			}
		}

		// Token: 0x06000DF5 RID: 3573 RVA: 0x00038E3C File Offset: 0x0003703C
		protected virtual bool TryGetDragPosition(Vector2 pointerPosition, ref ListViewDragger.DragPosition dragPosition)
		{
			ReusableCollectionItem recycledItem = this.GetRecycledItem(pointerPosition);
			bool flag = recycledItem != null;
			bool flag3;
			if (flag)
			{
				bool flag2 = this.targetListView.sourceIncludesArraySize && recycledItem.index == 0;
				if (flag2)
				{
					dragPosition.insertAtIndex = recycledItem.index + 1;
					dragPosition.dragAndDropPosition = DragAndDropPosition.BetweenItems;
					flag3 = true;
				}
				else
				{
					bool flag4 = recycledItem.rootElement.worldBound.yMax - pointerPosition.y < 5f;
					if (flag4)
					{
						dragPosition.insertAtIndex = recycledItem.index + 1;
						dragPosition.dragAndDropPosition = DragAndDropPosition.BetweenItems;
						flag3 = true;
					}
					else
					{
						bool flag5 = pointerPosition.y - recycledItem.rootElement.worldBound.yMin > 5f;
						if (flag5)
						{
							Vector2 scrollOffset = this.targetScrollView.scrollOffset;
							this.targetScrollView.ScrollTo(recycledItem.rootElement);
							bool flag6 = scrollOffset != this.targetScrollView.scrollOffset;
							if (flag6)
							{
								flag3 = this.TryGetDragPosition(pointerPosition, ref dragPosition);
							}
							else
							{
								dragPosition.recycledItem = recycledItem;
								dragPosition.insertAtIndex = recycledItem.index;
								dragPosition.dragAndDropPosition = DragAndDropPosition.OverItem;
								flag3 = true;
							}
						}
						else
						{
							dragPosition.insertAtIndex = recycledItem.index;
							dragPosition.dragAndDropPosition = DragAndDropPosition.BetweenItems;
							flag3 = true;
						}
					}
				}
			}
			else
			{
				bool flag7 = !this.targetListView.worldBound.Contains(pointerPosition);
				if (flag7)
				{
					flag3 = false;
				}
				else
				{
					dragPosition.dragAndDropPosition = DragAndDropPosition.OutsideItems;
					bool flag8 = pointerPosition.y >= this.targetScrollView.contentContainer.worldBound.yMax;
					if (flag8)
					{
						dragPosition.insertAtIndex = this.targetListView.itemsSource.Count;
					}
					else
					{
						dragPosition.insertAtIndex = 0;
					}
					flag3 = true;
				}
			}
			return flag3;
		}

		// Token: 0x06000DF6 RID: 3574 RVA: 0x00039000 File Offset: 0x00037200
		private ListDragAndDropArgs MakeDragAndDropArgs(ListViewDragger.DragPosition dragPosition)
		{
			object obj = null;
			ReusableCollectionItem recycledItem = dragPosition.recycledItem;
			bool flag = recycledItem != null;
			if (flag)
			{
				obj = this.targetListView.viewController.GetItemForIndex(recycledItem.index);
			}
			return new ListDragAndDropArgs
			{
				target = obj,
				insertAtIndex = dragPosition.insertAtIndex,
				dragAndDropPosition = dragPosition.dragAndDropPosition,
				dragAndDropData = (base.useDragEvents ? DragAndDropUtility.dragAndDrop.data : this.dragAndDropClient.data)
			};
		}

		// Token: 0x06000DF7 RID: 3575 RVA: 0x00039094 File Offset: 0x00037294
		private void PlaceHoverBarAtElement(VisualElement element)
		{
			VisualElement contentViewport = this.targetScrollView.contentViewport;
			this.PlaceHoverBarAt(Mathf.Min(contentViewport.WorldToLocal(element.worldBound).yMax, contentViewport.localBound.yMax - 2f));
		}

		// Token: 0x06000DF8 RID: 3576 RVA: 0x000390E2 File Offset: 0x000372E2
		private void PlaceHoverBarAt(float top)
		{
			this.m_DragHoverBar.style.top = top;
			this.m_DragHoverBar.style.visibility = Visibility.Visible;
		}

		// Token: 0x06000DF9 RID: 3577 RVA: 0x00039114 File Offset: 0x00037314
		protected override void ClearDragAndDropUI()
		{
			this.m_LastDragPosition = default(ListViewDragger.DragPosition);
			foreach (ReusableCollectionItem reusableCollectionItem in this.targetListView.activeItems)
			{
				reusableCollectionItem.rootElement.RemoveFromClassList(BaseVerticalCollectionView.itemDragHoverUssClassName);
			}
			bool flag = this.m_DragHoverBar != null;
			if (flag)
			{
				this.m_DragHoverBar.style.visibility = Visibility.Hidden;
			}
		}

		// Token: 0x06000DFA RID: 3578 RVA: 0x000391A8 File Offset: 0x000373A8
		protected ReusableCollectionItem GetRecycledItem(Vector3 pointerPosition)
		{
			foreach (ReusableCollectionItem reusableCollectionItem in this.targetListView.activeItems)
			{
				bool flag = reusableCollectionItem.rootElement.worldBound.Contains(pointerPosition);
				if (flag)
				{
					return reusableCollectionItem;
				}
			}
			return null;
		}

		// Token: 0x04000667 RID: 1639
		private ListViewDragger.DragPosition m_LastDragPosition;

		// Token: 0x04000668 RID: 1640
		private VisualElement m_DragHoverBar;

		// Token: 0x04000669 RID: 1641
		private const int k_AutoScrollAreaSize = 5;

		// Token: 0x0400066A RID: 1642
		private const int k_BetweenElementsAreaSize = 5;

		// Token: 0x0400066B RID: 1643
		private const int k_PanSpeed = 20;

		// Token: 0x0400066C RID: 1644
		private const int k_DragHoverBarHeight = 2;

		// Token: 0x020001B2 RID: 434
		internal struct DragPosition : IEquatable<ListViewDragger.DragPosition>
		{
			// Token: 0x06000DFC RID: 3580 RVA: 0x00039254 File Offset: 0x00037454
			public bool Equals(ListViewDragger.DragPosition other)
			{
				return this.insertAtIndex == other.insertAtIndex && object.Equals(this.recycledItem, other.recycledItem) && this.dragAndDropPosition == other.dragAndDropPosition;
			}

			// Token: 0x06000DFD RID: 3581 RVA: 0x00039298 File Offset: 0x00037498
			public override bool Equals(object obj)
			{
				return obj is ListViewDragger.DragPosition && this.Equals((ListViewDragger.DragPosition)obj);
			}

			// Token: 0x06000DFE RID: 3582 RVA: 0x000392C4 File Offset: 0x000374C4
			public override int GetHashCode()
			{
				int num = this.insertAtIndex;
				num = (num * 397) ^ ((this.recycledItem != null) ? this.recycledItem.GetHashCode() : 0);
				return (num * 397) ^ (int)this.dragAndDropPosition;
			}

			// Token: 0x0400066E RID: 1646
			public int insertAtIndex;

			// Token: 0x0400066F RID: 1647
			public ReusableCollectionItem recycledItem;

			// Token: 0x04000670 RID: 1648
			public DragAndDropPosition dragAndDropPosition;
		}
	}
}
