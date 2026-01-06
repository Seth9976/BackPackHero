using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x0200010F RID: 271
	internal class FixedHeightVirtualizationController<T> : VerticalVirtualizationController<T> where T : ReusableCollectionItem, new()
	{
		// Token: 0x170001AA RID: 426
		// (get) Token: 0x060008A7 RID: 2215 RVA: 0x00020C98 File Offset: 0x0001EE98
		private float resolvedItemHeight
		{
			get
			{
				return this.m_ListView.ResolveItemHeight(-1f);
			}
		}

		// Token: 0x060008A8 RID: 2216 RVA: 0x00020CAC File Offset: 0x0001EEAC
		protected override bool VisibleItemPredicate(T i)
		{
			return true;
		}

		// Token: 0x060008A9 RID: 2217 RVA: 0x00020CBF File Offset: 0x0001EEBF
		public FixedHeightVirtualizationController(BaseVerticalCollectionView collectionView)
			: base(collectionView)
		{
		}

		// Token: 0x060008AA RID: 2218 RVA: 0x00020CCC File Offset: 0x0001EECC
		public override int GetIndexFromPosition(Vector2 position)
		{
			return (int)(position.y / this.resolvedItemHeight);
		}

		// Token: 0x060008AB RID: 2219 RVA: 0x00020CEC File Offset: 0x0001EEEC
		public override float GetItemHeight(int index)
		{
			return this.resolvedItemHeight;
		}

		// Token: 0x060008AC RID: 2220 RVA: 0x00020D04 File Offset: 0x0001EF04
		public override void ScrollToItem(int index)
		{
			bool flag = this.visibleItemCount == 0 || index < -1;
			if (!flag)
			{
				float resolvedItemHeight = this.resolvedItemHeight;
				bool flag2 = index == -1;
				if (flag2)
				{
					int num = (int)(base.lastHeight / resolvedItemHeight);
					bool flag3 = this.m_ListView.itemsSource.Count < num;
					if (flag3)
					{
						this.m_ScrollView.scrollOffset = new Vector2(0f, 0f);
					}
					else
					{
						this.m_ScrollView.scrollOffset = new Vector2(0f, (float)(this.m_ListView.itemsSource.Count + 1) * resolvedItemHeight);
					}
				}
				else
				{
					bool flag4 = this.m_FirstVisibleIndex >= index;
					if (flag4)
					{
						this.m_ScrollView.scrollOffset = Vector2.up * (resolvedItemHeight * (float)index);
					}
					else
					{
						int num2 = (int)(base.lastHeight / resolvedItemHeight);
						bool flag5 = index < this.m_FirstVisibleIndex + num2;
						if (!flag5)
						{
							int num3 = index - num2 + 1;
							float num4 = resolvedItemHeight - (base.lastHeight - (float)num2 * resolvedItemHeight);
							float num5 = resolvedItemHeight * (float)num3 + num4;
							this.m_ScrollView.scrollOffset = new Vector2(this.m_ScrollView.scrollOffset.x, num5);
						}
					}
				}
			}
		}

		// Token: 0x060008AD RID: 2221 RVA: 0x00020E44 File Offset: 0x0001F044
		public override void Resize(Vector2 size, int layoutPass)
		{
			float resolvedItemHeight = this.resolvedItemHeight;
			float num = (float)this.m_ListView.itemsSource.Count * resolvedItemHeight;
			this.m_ScrollView.contentContainer.style.height = num;
			float num2 = Mathf.Max(0f, num - this.m_ScrollView.contentViewport.layout.height);
			float num3 = Mathf.Min(this.m_ListView.m_ScrollOffset.y, num2);
			this.m_ScrollView.verticalScroller.slider.SetHighValueWithoutNotify(num2);
			this.m_ScrollView.verticalScroller.slider.SetValueWithoutNotify(num3);
			int num4 = (int)(this.m_ListView.ResolveItemHeight(size.y) / resolvedItemHeight);
			bool flag = num4 > 0;
			if (flag)
			{
				num4 += 2;
			}
			int num5 = Mathf.Min(num4, this.m_ListView.itemsSource.Count);
			bool flag2 = this.visibleItemCount != num5;
			if (flag2)
			{
				int visibleItemCount = this.visibleItemCount;
				bool flag3 = this.visibleItemCount > num5;
				if (flag3)
				{
					int num6 = visibleItemCount - num5;
					for (int i = 0; i < num6; i++)
					{
						int num7 = this.m_ActiveItems.Count - 1;
						this.ReleaseItem(num7);
					}
				}
				else
				{
					int num8 = num5 - this.visibleItemCount;
					for (int j = 0; j < num8; j++)
					{
						int num9 = j + this.m_FirstVisibleIndex + visibleItemCount;
						T orMakeItem = this.GetOrMakeItem();
						this.m_ActiveItems.Add(orMakeItem);
						this.m_ScrollView.Add(orMakeItem.rootElement);
						base.Setup(orMakeItem, num9);
					}
				}
			}
			this.OnScroll(new Vector2(0f, num3));
		}

		// Token: 0x060008AE RID: 2222 RVA: 0x00021020 File Offset: 0x0001F220
		public override void OnScroll(Vector2 scrollOffset)
		{
			float y = scrollOffset.y;
			float resolvedItemHeight = this.resolvedItemHeight;
			int num = (int)(y / resolvedItemHeight);
			this.m_ScrollView.contentContainer.style.paddingTop = (float)num * resolvedItemHeight;
			this.m_ScrollView.contentContainer.style.height = (float)this.m_ListView.itemsSource.Count * resolvedItemHeight;
			this.m_ListView.m_ScrollOffset.y = scrollOffset.y;
			bool flag = num != this.m_FirstVisibleIndex;
			if (flag)
			{
				this.m_FirstVisibleIndex = num;
				bool flag2 = this.m_ActiveItems.Count > 0;
				if (flag2)
				{
					bool flag3 = this.m_FirstVisibleIndex < this.m_ActiveItems[0].index;
					if (flag3)
					{
						int num2 = this.m_ActiveItems[0].index - this.m_FirstVisibleIndex;
						List<T> scrollInsertionList = this.m_ScrollInsertionList;
						int num3 = 0;
						while (num3 < num2 && this.m_ActiveItems.Count > 0)
						{
							T t = this.m_ActiveItems[this.m_ActiveItems.Count - 1];
							scrollInsertionList.Add(t);
							this.m_ActiveItems.RemoveAt(this.m_ActiveItems.Count - 1);
							t.rootElement.SendToBack();
							num3++;
						}
						this.m_ActiveItems.InsertRange(0, scrollInsertionList);
						this.m_ScrollInsertionList.Clear();
					}
					else
					{
						bool flag4 = this.m_FirstVisibleIndex < this.m_ActiveItems[this.m_ActiveItems.Count - 1].index;
						if (flag4)
						{
							List<T> scrollInsertionList2 = this.m_ScrollInsertionList;
							int num4 = 0;
							while (this.m_FirstVisibleIndex > this.m_ActiveItems[num4].index)
							{
								T t2 = this.m_ActiveItems[num4];
								scrollInsertionList2.Add(t2);
								num4++;
								t2.rootElement.BringToFront();
							}
							this.m_ActiveItems.RemoveRange(0, num4);
							this.m_ActiveItems.AddRange(scrollInsertionList2);
							scrollInsertionList2.Clear();
						}
					}
					for (int i = 0; i < this.m_ActiveItems.Count; i++)
					{
						int num5 = i + this.m_FirstVisibleIndex;
						base.Setup(this.m_ActiveItems[i], num5);
					}
				}
			}
		}

		// Token: 0x060008AF RID: 2223 RVA: 0x000212C8 File Offset: 0x0001F4C8
		internal override T GetOrMakeItem()
		{
			T orMakeItem = base.GetOrMakeItem();
			orMakeItem.rootElement.style.height = this.resolvedItemHeight;
			return orMakeItem;
		}
	}
}
