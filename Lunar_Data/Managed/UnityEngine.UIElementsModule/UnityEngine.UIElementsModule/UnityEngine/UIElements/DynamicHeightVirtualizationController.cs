using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x0200010E RID: 270
	internal class DynamicHeightVirtualizationController<T> : VerticalVirtualizationController<T> where T : ReusableCollectionItem, new()
	{
		// Token: 0x0600088F RID: 2191 RVA: 0x0001F840 File Offset: 0x0001DA40
		public DynamicHeightVirtualizationController(BaseVerticalCollectionView collectionView)
			: base(collectionView)
		{
			this.m_FillCallback = new Action(this.Fill);
			this.m_GeometryChangedCallback = new Action<ReusableCollectionItem>(this.OnRecycledItemGeometryChanged);
			this.m_IndexOutOfBoundsPredicate = new Predicate<int>(this.IsIndexOutOfBounds);
			this.m_ScrollView.contentViewport.RegisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.OnViewportGeometryChanged), TrickleDown.NoTrickleDown);
			collectionView.destroyItem = (Action<VisualElement>)Delegate.Combine(collectionView.destroyItem, delegate(VisualElement element)
			{
				foreach (ReusableCollectionItem reusableCollectionItem in this.m_ListView.activeItems)
				{
					bool flag = reusableCollectionItem.rootElement == element;
					if (flag)
					{
						this.UnregisterItemHeight(reusableCollectionItem.index, element.layout.height);
						break;
					}
				}
			});
		}

		// Token: 0x06000890 RID: 2192 RVA: 0x0001F900 File Offset: 0x0001DB00
		public override void Refresh(bool rebuild)
		{
			base.Refresh(rebuild);
			if (rebuild)
			{
				this.m_WaitingCache.Clear();
			}
			else
			{
				this.m_WaitingCache.RemoveWhere(this.m_IndexOutOfBoundsPredicate);
			}
			bool flag = this.m_ListView.HasValidDataAndBindings();
			if (flag)
			{
				if (this.m_ScheduledItem == null)
				{
					this.m_ScheduledItem = this.m_ListView.schedule.Execute(this.m_FillCallback);
				}
			}
		}

		// Token: 0x06000891 RID: 2193 RVA: 0x0001F970 File Offset: 0x0001DB70
		public override void ScrollToItem(int index)
		{
			bool flag = this.visibleItemCount == 0 || index < -1;
			if (!flag)
			{
				float height = this.m_ScrollView.contentContainer.layout.height;
				float height2 = this.m_ScrollView.contentViewport.layout.height;
				bool flag2 = index == -1;
				if (flag2)
				{
					this.m_ForcedLastVisibleItem = this.m_ListView.viewController.itemsSource.Count - 1;
					this.m_StickToBottom = true;
					this.m_ScrollView.scrollOffset = new Vector2(0f, (height2 >= height) ? 0f : height);
				}
				else
				{
					bool flag3 = this.m_FirstVisibleIndex >= index;
					if (flag3)
					{
						this.m_ForcedFirstVisibleItem = index;
						this.m_ScrollView.scrollOffset = new Vector2(0f, this.GetContentHeightForIndex(index - 1));
					}
					else
					{
						float contentHeightForIndex = this.GetContentHeightForIndex(index);
						bool flag4 = contentHeightForIndex < this.m_StoredPadding + height2;
						if (!flag4)
						{
							this.m_ForcedLastVisibleItem = index;
							float num = contentHeightForIndex - height2 + (float)DynamicHeightVirtualizationController<T>.InitialAverageHeight;
							this.m_ScrollView.scrollOffset = new Vector2(0f, num);
						}
					}
				}
			}
		}

		// Token: 0x06000892 RID: 2194 RVA: 0x0001FAA8 File Offset: 0x0001DCA8
		public override void Resize(Vector2 size, int layoutPass)
		{
			float contentHeight = this.GetContentHeight();
			this.m_ScrollView.contentContainer.style.height = contentHeight;
			float contentHeightForIndex = this.GetContentHeightForIndex(this.m_FirstVisibleIndex - 1);
			float y = this.m_ListView.m_ScrollOffset.y;
			float storedPadding = this.m_StoredPadding;
			float num = y - storedPadding;
			float num2 = contentHeightForIndex + num;
			float num3 = Mathf.Max(0f, contentHeight - this.m_ScrollView.contentViewport.layout.height);
			float num4 = Mathf.Min(num2, num3);
			this.m_ScrollView.verticalScroller.slider.SetHighValueWithoutNotify(num3);
			this.m_ScrollView.verticalScroller.value = num4;
			this.m_ListView.m_ScrollOffset.y = this.m_ScrollView.verticalScroller.value;
			this.m_ScrollView.contentContainer.style.paddingTop = contentHeightForIndex;
			this.m_StoredPadding = contentHeightForIndex;
			bool flag = layoutPass == 0;
			if (flag)
			{
				this.Fill();
				this.OnScroll(new Vector2(0f, num4));
			}
			else
			{
				bool flag2 = this.m_ScheduledItem == null;
				if (flag2)
				{
					this.m_ScheduledItem = this.m_ListView.schedule.Execute(this.m_FillCallback);
				}
			}
		}

		// Token: 0x06000893 RID: 2195 RVA: 0x0001FC00 File Offset: 0x0001DE00
		public override void OnScroll(Vector2 scrollOffset)
		{
			bool flag = float.IsNaN(base.lastHeight);
			if (!flag)
			{
				float y = scrollOffset.y;
				float contentHeight = this.GetContentHeight();
				float num = Mathf.Max(0f, contentHeight - this.m_ScrollView.contentViewport.layout.height);
				float num2 = this.m_ScrollView.contentContainer.boundingBox.height - this.m_ScrollView.contentViewport.layout.height;
				this.m_ListView.m_ScrollOffset.y = Mathf.Min(y, num);
				bool flag2 = scrollOffset.y == 0f;
				if (flag2)
				{
					this.m_ForcedFirstVisibleItem = 0;
				}
				else
				{
					this.m_StickToBottom = num2 > 0f && Math.Abs(scrollOffset.y - this.m_ScrollView.verticalScroller.highValue) < float.Epsilon;
				}
				int num3 = ((this.m_ForcedFirstVisibleItem != -1) ? this.m_ForcedFirstVisibleItem : this.GetFirstVisibleItem(this.m_ListView.m_ScrollOffset.y));
				float contentHeightForIndex = this.GetContentHeightForIndex(num3 - 1);
				this.m_ForcedFirstVisibleItem = -1;
				bool flag3 = num3 != this.m_FirstVisibleIndex;
				if (flag3)
				{
					this.m_FirstVisibleIndex = num3;
					bool flag4 = this.m_ActiveItems.Count > 0;
					if (flag4)
					{
						T firstVisibleItem = base.firstVisibleItem;
						bool stickToBottom = this.m_StickToBottom;
						if (!stickToBottom)
						{
							bool flag5 = this.m_FirstVisibleIndex < firstVisibleItem.index;
							if (flag5)
							{
								int num4 = firstVisibleItem.index - this.m_FirstVisibleIndex;
								List<T> scrollInsertionList = this.m_ScrollInsertionList;
								int num5 = 0;
								while (num5 < num4 && this.m_ActiveItems.Count > 0)
								{
									T t = this.m_ActiveItems[this.m_ActiveItems.Count - 1];
									bool flag6 = t.rootElement.layout.y < this.m_ListView.m_ScrollOffset.y + this.m_ScrollView.contentViewport.layout.height;
									if (flag6)
									{
										break;
									}
									scrollInsertionList.Add(t);
									this.m_ActiveItems.RemoveAt(this.m_ActiveItems.Count - 1);
									t.rootElement.SendToBack();
									num5++;
								}
								this.m_ActiveItems.InsertRange(0, scrollInsertionList);
								this.m_ScrollInsertionList.Clear();
							}
							else
							{
								T lastVisibleItem = base.lastVisibleItem;
								bool flag7 = this.m_FirstVisibleIndex < lastVisibleItem.index && lastVisibleItem.index < this.m_ListView.itemsSource.Count - 1;
								if (flag7)
								{
									List<T> scrollInsertionList2 = this.m_ScrollInsertionList;
									int num6 = 0;
									while (this.m_FirstVisibleIndex > this.m_ActiveItems[num6].index)
									{
										T t2 = this.m_ActiveItems[num6];
										scrollInsertionList2.Add(t2);
										num6++;
										t2.rootElement.BringToFront();
									}
									this.m_ActiveItems.RemoveRange(0, num6);
									this.m_ActiveItems.AddRange(scrollInsertionList2);
									this.m_ScrollInsertionList.Clear();
								}
							}
						}
						float num7 = contentHeightForIndex;
						for (int i = 0; i < this.m_ActiveItems.Count; i++)
						{
							int num8 = this.m_FirstVisibleIndex + i;
							bool flag8 = num8 >= this.m_ListView.itemsSource.Count;
							if (flag8)
							{
								this.m_StickToBottom = true;
								this.m_ForcedLastVisibleItem = -1;
								this.ReleaseItem(i--);
							}
							else
							{
								bool flag9 = num7 - this.m_ListView.m_ScrollOffset.y <= this.m_ScrollView.contentViewport.layout.height;
								int index = this.m_ActiveItems[i].index;
								this.m_WaitingCache.Remove(index);
								base.Setup(this.m_ActiveItems[i], num8);
								bool flag10 = flag9;
								if (flag10)
								{
									bool flag11 = num8 != index;
									if (flag11)
									{
										this.m_WaitingCache.Add(num8);
									}
								}
								else
								{
									this.ReleaseItem(i--);
								}
								num7 += this.GetItemHeight(num8);
							}
						}
					}
				}
				this.m_StoredPadding = contentHeightForIndex;
				this.m_ScrollView.contentContainer.style.paddingTop = contentHeightForIndex;
				if (this.m_ScheduledItem == null)
				{
					this.m_ScheduledItem = this.m_ListView.schedule.Execute(this.m_FillCallback);
				}
			}
		}

		// Token: 0x06000894 RID: 2196 RVA: 0x00020104 File Offset: 0x0001E304
		private bool NeedsFill()
		{
			T t = base.lastVisibleItem;
			int num = ((t != null) ? t.index : (-1));
			float num2 = this.m_StoredPadding;
			bool flag = num2 > this.m_ListView.m_ScrollOffset.y;
			bool flag2;
			if (flag)
			{
				flag2 = true;
			}
			else
			{
				for (int i = this.m_FirstVisibleIndex; i < this.m_ListView.itemsSource.Count; i++)
				{
					bool flag3 = num2 - this.m_ListView.m_ScrollOffset.y > this.m_ScrollView.contentViewport.layout.height;
					if (flag3)
					{
						break;
					}
					num2 += this.GetItemHeight(i);
					bool flag4 = i > num;
					if (flag4)
					{
						return true;
					}
				}
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x06000895 RID: 2197 RVA: 0x000201CC File Offset: 0x0001E3CC
		private void Fill()
		{
			bool flag = !this.m_ListView.HasValidDataAndBindings();
			if (!flag)
			{
				int num = 0;
				T lastVisibleItem = base.lastVisibleItem;
				T t = lastVisibleItem;
				int num2 = ((t != null) ? t.index : (-1));
				float num3 = this.m_StoredPadding;
				float num4 = num3;
				ListViewDraggerAnimated listViewDraggerAnimated = this.m_ListView.dragger as ListViewDraggerAnimated;
				bool flag2 = listViewDraggerAnimated != null && listViewDraggerAnimated.draggedItem != null;
				if (flag2)
				{
					num4 -= listViewDraggerAnimated.draggedItem.rootElement.style.height.value.value;
				}
				for (int i = this.m_FirstVisibleIndex; i < this.m_ListView.itemsSource.Count; i++)
				{
					bool flag3 = num4 - this.m_ListView.m_ScrollOffset.y > this.m_ScrollView.contentViewport.layout.height;
					if (flag3)
					{
						break;
					}
					num4 += this.GetItemHeight(i);
					bool flag4 = i > num2;
					if (flag4)
					{
						num++;
					}
				}
				int visibleItemCount = this.visibleItemCount;
				for (int j = 0; j < num; j++)
				{
					int num5 = j + this.m_FirstVisibleIndex + visibleItemCount;
					T orMakeItem = this.GetOrMakeItem();
					this.m_ActiveItems.Add(orMakeItem);
					this.m_ScrollView.Add(orMakeItem.rootElement);
					this.m_WaitingCache.Add(num5);
					base.Setup(orMakeItem, num5);
				}
				while (num3 > this.m_ListView.m_ScrollOffset.y)
				{
					int num6 = this.m_FirstVisibleIndex - 1;
					bool flag5 = num6 < 0;
					if (flag5)
					{
						break;
					}
					T orMakeItem2 = this.GetOrMakeItem();
					this.m_ActiveItems.Insert(0, orMakeItem2);
					this.m_ScrollView.Insert(0, orMakeItem2.rootElement);
					this.m_WaitingCache.Add(num6);
					base.Setup(orMakeItem2, num6);
					num3 -= this.GetItemHeight(num6);
					this.m_FirstVisibleIndex = num6;
				}
				this.m_ScrollView.contentContainer.style.paddingTop = num3;
				this.m_StoredPadding = num3;
				this.m_ScheduledItem = null;
			}
		}

		// Token: 0x06000896 RID: 2198 RVA: 0x00020424 File Offset: 0x0001E624
		public override int GetIndexFromPosition(Vector2 position)
		{
			int num = 0;
			for (float num2 = 0f; num2 < position.y; num2 += this.GetItemHeight(num++))
			{
			}
			return num - 1;
		}

		// Token: 0x06000897 RID: 2199 RVA: 0x00020460 File Offset: 0x0001E660
		public override float GetItemHeight(int index)
		{
			float num;
			return this.m_ItemHeightCache.TryGetValue(index, ref num) ? num : this.m_AverageHeight;
		}

		// Token: 0x06000898 RID: 2200 RVA: 0x0002048C File Offset: 0x0001E68C
		private int GetFirstVisibleItem(float offset)
		{
			bool flag = offset <= 0f;
			int num;
			if (flag)
			{
				num = 0;
			}
			else
			{
				int num2 = -1;
				while (offset > 0f)
				{
					num2++;
					float itemHeight = this.GetItemHeight(num2);
					offset -= itemHeight;
				}
				num = num2;
			}
			return num;
		}

		// Token: 0x06000899 RID: 2201 RVA: 0x000204D8 File Offset: 0x0001E6D8
		private void UpdateScrollViewContainer(int index, float previousHeight, float newHeight)
		{
			float y = this.m_ListView.m_ScrollOffset.y;
			float storedPadding = this.m_StoredPadding;
			this.m_StoredPadding = this.GetContentHeightForIndex(this.m_FirstVisibleIndex - 1);
			bool stickToBottom = this.m_StickToBottom;
			if (!stickToBottom)
			{
				bool flag = this.m_ForcedLastVisibleItem >= 0;
				if (flag)
				{
					float contentHeightForIndex = this.GetContentHeightForIndex(this.m_ForcedLastVisibleItem);
					this.m_ListView.m_ScrollOffset.y = contentHeightForIndex + (float)DynamicHeightVirtualizationController<T>.InitialAverageHeight - this.m_ScrollView.contentViewport.layout.height;
				}
				else
				{
					float num = y - storedPadding;
					bool flag2 = index == this.m_FirstVisibleIndex && num != 0f;
					if (flag2)
					{
						num += newHeight - previousHeight;
					}
					this.m_ListView.m_ScrollOffset.y = this.m_StoredPadding + num;
				}
			}
		}

		// Token: 0x0600089A RID: 2202 RVA: 0x000205C0 File Offset: 0x0001E7C0
		private void ApplyScrollViewUpdate()
		{
			float contentHeight = this.GetContentHeight();
			this.m_StoredPadding = this.GetContentHeightForIndex(this.m_FirstVisibleIndex - 1);
			this.m_ScrollView.contentContainer.style.paddingTop = this.m_StoredPadding;
			this.m_ScrollView.contentContainer.style.height = contentHeight;
			float num = Mathf.Max(0f, contentHeight - this.m_ScrollView.contentViewport.layout.height);
			bool stickToBottom = this.m_StickToBottom;
			if (stickToBottom)
			{
				this.m_ListView.m_ScrollOffset.y = num;
			}
			this.m_ScrollView.verticalScroller.slider.SetHighValueWithoutNotify(num);
			this.m_ScrollView.verticalScroller.slider.SetValueWithoutNotify(this.m_ListView.m_ScrollOffset.y);
			this.m_ListView.m_ScrollOffset.y = this.m_ScrollView.verticalScroller.slider.value;
			bool flag = !this.NeedsFill();
			if (flag)
			{
				float num2 = this.m_StoredPadding;
				int firstVisibleIndex = this.m_FirstVisibleIndex;
				for (int i = 0; i < this.m_ActiveItems.Count; i++)
				{
					int num3 = this.m_FirstVisibleIndex + i;
					bool flag2 = num2 - this.m_ListView.m_ScrollOffset.y < this.m_ScrollView.contentViewport.layout.height;
					bool flag3 = !flag2;
					if (flag3)
					{
						bool flag4 = this.m_FirstVisibleIndex == num3;
						if (flag4)
						{
							this.m_FirstVisibleIndex = num3 + 1;
						}
					}
					num2 += this.GetItemHeight(num3);
				}
				bool flag5 = this.m_FirstVisibleIndex != firstVisibleIndex;
				if (flag5)
				{
					this.m_StoredPadding = this.GetContentHeightForIndex(this.m_FirstVisibleIndex - 1);
				}
				this.m_StickToBottom = false;
				this.m_ForcedLastVisibleItem = -1;
				IVisualElementScheduledItem scheduledItem = this.m_ScheduledItem;
				if (scheduledItem != null)
				{
					scheduledItem.Pause();
				}
				this.m_ScheduledItem = null;
			}
			else if (this.m_ScheduledItem == null)
			{
				this.m_ScheduledItem = this.m_ListView.schedule.Execute(this.m_FillCallback);
			}
		}

		// Token: 0x0600089B RID: 2203 RVA: 0x000207FC File Offset: 0x0001E9FC
		private void OnViewportGeometryChanged(GeometryChangedEvent evt)
		{
			bool flag = evt.oldRect.size == evt.newRect.size;
			if (!flag)
			{
				this.m_ScrollView.UpdateScrollers(this.m_ScrollView.needsHorizontal, this.m_ScrollView.needsVertical);
			}
		}

		// Token: 0x0600089C RID: 2204 RVA: 0x00020854 File Offset: 0x0001EA54
		private float GetContentHeight()
		{
			int count = this.m_ListView.viewController.itemsSource.Count;
			return this.GetContentHeightForIndex(count - 1);
		}

		// Token: 0x0600089D RID: 2205 RVA: 0x00020888 File Offset: 0x0001EA88
		private float GetContentHeightForIndex(int lastIndex)
		{
			bool flag = lastIndex < 0;
			float num;
			if (flag)
			{
				num = 0f;
			}
			else
			{
				float num2 = 0f;
				for (int i = 0; i <= lastIndex; i++)
				{
					num2 += this.GetItemHeight(i);
				}
				num = num2;
			}
			return num;
		}

		// Token: 0x0600089E RID: 2206 RVA: 0x000208D4 File Offset: 0x0001EAD4
		private void RegisterItemHeight(int index, float height)
		{
			bool flag = height <= 0f;
			if (!flag)
			{
				float num = this.m_ListView.ResolveItemHeight(height);
				float num2;
				bool flag2 = this.m_ItemHeightCache.TryGetValue(index, ref num2);
				if (flag2)
				{
					this.m_AccumulatedHeight -= num2;
				}
				this.m_AccumulatedHeight += num;
				int count = this.m_ItemHeightCache.Count;
				this.m_AverageHeight = this.m_ListView.ResolveItemHeight((count > 0) ? (this.m_AccumulatedHeight / (float)count) : this.m_AccumulatedHeight);
				this.m_ItemHeightCache[index] = num;
			}
		}

		// Token: 0x0600089F RID: 2207 RVA: 0x00020970 File Offset: 0x0001EB70
		private void UnregisterItemHeight(int index, float height)
		{
			bool flag = height <= 0f;
			if (!flag)
			{
				float num;
				bool flag2 = !this.m_ItemHeightCache.TryGetValue(index, ref num);
				if (!flag2)
				{
					this.m_AccumulatedHeight -= num;
					this.m_ItemHeightCache.Remove(index);
					int count = this.m_ItemHeightCache.Count;
					this.m_AverageHeight = this.m_ListView.ResolveItemHeight((count > 0) ? (this.m_AccumulatedHeight / (float)count) : this.m_AccumulatedHeight);
				}
			}
		}

		// Token: 0x060008A0 RID: 2208 RVA: 0x000209F4 File Offset: 0x0001EBF4
		private void OnRecycledItemGeometryChanged(ReusableCollectionItem item)
		{
			bool flag = item.index == -1 || float.IsNaN(item.rootElement.layout.height) || item.rootElement.layout.height == 0f;
			if (!flag)
			{
				bool flag2 = item.animator != null && item.animator.isRunning;
				if (!flag2)
				{
					float itemHeight = this.GetItemHeight(item.index);
					float num;
					bool flag3 = !this.m_ItemHeightCache.TryGetValue(item.index, ref num) || !item.rootElement.layout.height.Equals(num);
					if (flag3)
					{
						this.RegisterItemHeight(item.index, item.rootElement.layout.height);
						this.UpdateScrollViewContainer(item.index, itemHeight, item.rootElement.layout.height);
						bool flag4 = this.m_WaitingCache.Count == 0;
						if (flag4)
						{
							this.ApplyScrollViewUpdate();
						}
					}
					bool flag5 = this.m_WaitingCache.Remove(item.index) && this.m_WaitingCache.Count == 0;
					if (flag5)
					{
						this.ApplyScrollViewUpdate();
					}
				}
			}
		}

		// Token: 0x060008A1 RID: 2209 RVA: 0x00020B54 File Offset: 0x0001ED54
		internal override T GetOrMakeItem()
		{
			T orMakeItem = base.GetOrMakeItem();
			orMakeItem.onGeometryChanged += this.m_GeometryChangedCallback;
			return orMakeItem;
		}

		// Token: 0x060008A2 RID: 2210 RVA: 0x00020B80 File Offset: 0x0001ED80
		public override void ReplaceActiveItem(int index)
		{
			base.ReplaceActiveItem(index);
			this.m_WaitingCache.Remove(index);
		}

		// Token: 0x060008A3 RID: 2211 RVA: 0x00020B98 File Offset: 0x0001ED98
		internal override void ReleaseItem(int activeItemsIndex)
		{
			T t = this.m_ActiveItems[activeItemsIndex];
			t.onGeometryChanged -= this.m_GeometryChangedCallback;
			int index = t.index;
			base.ReleaseItem(activeItemsIndex);
			this.m_WaitingCache.Remove(index);
		}

		// Token: 0x060008A4 RID: 2212 RVA: 0x00020BE8 File Offset: 0x0001EDE8
		private bool IsIndexOutOfBounds(int i)
		{
			return i >= this.m_ListView.itemsSource.Count;
		}

		// Token: 0x0400037E RID: 894
		internal static readonly int InitialAverageHeight = 20;

		// Token: 0x0400037F RID: 895
		private Dictionary<int, float> m_ItemHeightCache = new Dictionary<int, float>();

		// Token: 0x04000380 RID: 896
		private HashSet<int> m_WaitingCache = new HashSet<int>();

		// Token: 0x04000381 RID: 897
		private int m_ForcedFirstVisibleItem = -1;

		// Token: 0x04000382 RID: 898
		private int m_ForcedLastVisibleItem = -1;

		// Token: 0x04000383 RID: 899
		private bool m_StickToBottom;

		// Token: 0x04000384 RID: 900
		private float m_AverageHeight = (float)DynamicHeightVirtualizationController<T>.InitialAverageHeight;

		// Token: 0x04000385 RID: 901
		private float m_AccumulatedHeight;

		// Token: 0x04000386 RID: 902
		private float m_StoredPadding;

		// Token: 0x04000387 RID: 903
		private Action m_FillCallback;

		// Token: 0x04000388 RID: 904
		private Action<ReusableCollectionItem> m_GeometryChangedCallback;

		// Token: 0x04000389 RID: 905
		private IVisualElementScheduledItem m_ScheduledItem;

		// Token: 0x0400038A RID: 906
		private Predicate<int> m_IndexOutOfBoundsPredicate;
	}
}
