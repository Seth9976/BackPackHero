using System;
using UnityEngine.UIElements.StyleSheets;

namespace UnityEngine.UIElements
{
	// Token: 0x0200016E RID: 366
	public class ScrollView : VisualElement
	{
		// Token: 0x17000236 RID: 566
		// (get) Token: 0x06000B5E RID: 2910 RVA: 0x0002D810 File Offset: 0x0002BA10
		// (set) Token: 0x06000B5F RID: 2911 RVA: 0x0002D828 File Offset: 0x0002BA28
		public ScrollerVisibility horizontalScrollerVisibility
		{
			get
			{
				return this.m_HorizontalScrollerVisibility;
			}
			set
			{
				this.m_HorizontalScrollerVisibility = value;
				this.UpdateScrollers(this.needsHorizontal, this.needsVertical);
			}
		}

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x06000B60 RID: 2912 RVA: 0x0002D848 File Offset: 0x0002BA48
		// (set) Token: 0x06000B61 RID: 2913 RVA: 0x0002D860 File Offset: 0x0002BA60
		public ScrollerVisibility verticalScrollerVisibility
		{
			get
			{
				return this.m_VerticalScrollerVisibility;
			}
			set
			{
				this.m_VerticalScrollerVisibility = value;
				this.UpdateScrollers(this.needsHorizontal, this.needsVertical);
			}
		}

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x06000B62 RID: 2914 RVA: 0x0002D87D File Offset: 0x0002BA7D
		// (set) Token: 0x06000B63 RID: 2915 RVA: 0x0002D888 File Offset: 0x0002BA88
		[Obsolete("showHorizontal is obsolete. Use horizontalScrollerVisibility instead")]
		public bool showHorizontal
		{
			get
			{
				return this.horizontalScrollerVisibility == ScrollerVisibility.AlwaysVisible;
			}
			set
			{
				this.m_HorizontalScrollerVisibility = (value ? ScrollerVisibility.AlwaysVisible : ScrollerVisibility.Auto);
			}
		}

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x06000B64 RID: 2916 RVA: 0x0002D897 File Offset: 0x0002BA97
		// (set) Token: 0x06000B65 RID: 2917 RVA: 0x0002D8A2 File Offset: 0x0002BAA2
		[Obsolete("showVertical is obsolete. Use verticalScrollerVisibility instead")]
		public bool showVertical
		{
			get
			{
				return this.verticalScrollerVisibility == ScrollerVisibility.AlwaysVisible;
			}
			set
			{
				this.m_VerticalScrollerVisibility = (value ? ScrollerVisibility.AlwaysVisible : ScrollerVisibility.Auto);
			}
		}

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x06000B66 RID: 2918 RVA: 0x0002D8B4 File Offset: 0x0002BAB4
		internal bool needsHorizontal
		{
			get
			{
				return this.horizontalScrollerVisibility == ScrollerVisibility.AlwaysVisible || (this.horizontalScrollerVisibility == ScrollerVisibility.Auto && this.scrollableWidth > 0.001f);
			}
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x06000B67 RID: 2919 RVA: 0x0002D8EC File Offset: 0x0002BAEC
		internal bool needsVertical
		{
			get
			{
				return this.verticalScrollerVisibility == ScrollerVisibility.AlwaysVisible || (this.verticalScrollerVisibility == ScrollerVisibility.Auto && this.scrollableHeight > 0.001f);
			}
		}

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x06000B68 RID: 2920 RVA: 0x0002D924 File Offset: 0x0002BB24
		internal bool isVerticalScrollDisplayed
		{
			get
			{
				return this.verticalScroller.resolvedStyle.display == DisplayStyle.Flex;
			}
		}

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x06000B69 RID: 2921 RVA: 0x0002D94C File Offset: 0x0002BB4C
		internal bool isHorizontalScrollDisplayed
		{
			get
			{
				return this.horizontalScroller.resolvedStyle.display == DisplayStyle.Flex;
			}
		}

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x06000B6A RID: 2922 RVA: 0x0002D974 File Offset: 0x0002BB74
		// (set) Token: 0x06000B6B RID: 2923 RVA: 0x0002D9A4 File Offset: 0x0002BBA4
		public Vector2 scrollOffset
		{
			get
			{
				return new Vector2(this.horizontalScroller.value, this.verticalScroller.value);
			}
			set
			{
				bool flag = value != this.scrollOffset;
				if (flag)
				{
					this.horizontalScroller.value = value.x;
					this.verticalScroller.value = value.y;
					this.UpdateContentViewTransform();
				}
			}
		}

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x06000B6C RID: 2924 RVA: 0x0002D9F0 File Offset: 0x0002BBF0
		// (set) Token: 0x06000B6D RID: 2925 RVA: 0x0002DA08 File Offset: 0x0002BC08
		public float horizontalPageSize
		{
			get
			{
				return this.m_HorizontalPageSize;
			}
			set
			{
				this.m_HorizontalPageSize = value;
				this.UpdateHorizontalSliderPageSize();
			}
		}

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x06000B6E RID: 2926 RVA: 0x0002DA1C File Offset: 0x0002BC1C
		// (set) Token: 0x06000B6F RID: 2927 RVA: 0x0002DA34 File Offset: 0x0002BC34
		public float verticalPageSize
		{
			get
			{
				return this.m_VerticalPageSize;
			}
			set
			{
				this.m_VerticalPageSize = value;
				this.UpdateVerticalSliderPageSize();
			}
		}

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x06000B70 RID: 2928 RVA: 0x0002DA48 File Offset: 0x0002BC48
		// (set) Token: 0x06000B71 RID: 2929 RVA: 0x0002DA60 File Offset: 0x0002BC60
		public float mouseWheelScrollSize
		{
			get
			{
				return this.m_MouseWheelScrollSize;
			}
			set
			{
				float mouseWheelScrollSize = this.m_MouseWheelScrollSize;
				bool flag = Math.Abs(this.m_MouseWheelScrollSize - value) > float.Epsilon;
				if (flag)
				{
					this.m_MouseWheelScrollSizeIsInline = true;
					this.m_MouseWheelScrollSize = value;
				}
			}
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x06000B72 RID: 2930 RVA: 0x0002DAA0 File Offset: 0x0002BCA0
		internal float scrollableWidth
		{
			get
			{
				return this.contentContainer.boundingBox.width - this.contentViewport.layout.width;
			}
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x06000B73 RID: 2931 RVA: 0x0002DADC File Offset: 0x0002BCDC
		internal float scrollableHeight
		{
			get
			{
				return this.contentContainer.boundingBox.height - this.contentViewport.layout.height;
			}
		}

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x06000B74 RID: 2932 RVA: 0x0002DB15 File Offset: 0x0002BD15
		private bool hasInertia
		{
			get
			{
				return this.scrollDecelerationRate > 0f;
			}
		}

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x06000B75 RID: 2933 RVA: 0x0002DB24 File Offset: 0x0002BD24
		// (set) Token: 0x06000B76 RID: 2934 RVA: 0x0002DB3C File Offset: 0x0002BD3C
		public float scrollDecelerationRate
		{
			get
			{
				return this.m_ScrollDecelerationRate;
			}
			set
			{
				this.m_ScrollDecelerationRate = Mathf.Max(0f, value);
			}
		}

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x06000B77 RID: 2935 RVA: 0x0002DB50 File Offset: 0x0002BD50
		// (set) Token: 0x06000B78 RID: 2936 RVA: 0x0002DB68 File Offset: 0x0002BD68
		public float elasticity
		{
			get
			{
				return this.m_Elasticity;
			}
			set
			{
				this.m_Elasticity = Mathf.Max(0f, value);
			}
		}

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x06000B79 RID: 2937 RVA: 0x0002DB7C File Offset: 0x0002BD7C
		// (set) Token: 0x06000B7A RID: 2938 RVA: 0x0002DB94 File Offset: 0x0002BD94
		public ScrollView.TouchScrollBehavior touchScrollBehavior
		{
			get
			{
				return this.m_TouchScrollBehavior;
			}
			set
			{
				this.m_TouchScrollBehavior = value;
				bool flag = this.m_TouchScrollBehavior == ScrollView.TouchScrollBehavior.Clamped;
				if (flag)
				{
					this.horizontalScroller.slider.clamped = true;
					this.verticalScroller.slider.clamped = true;
				}
				else
				{
					this.horizontalScroller.slider.clamped = false;
					this.verticalScroller.slider.clamped = false;
				}
			}
		}

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x06000B7B RID: 2939 RVA: 0x0002DC04 File Offset: 0x0002BE04
		// (set) Token: 0x06000B7C RID: 2940 RVA: 0x0002DC0C File Offset: 0x0002BE0C
		public ScrollView.NestedInteractionKind nestedInteractionKind
		{
			get
			{
				return this.m_NestedInteractionKind;
			}
			set
			{
				this.m_NestedInteractionKind = value;
			}
		}

		// Token: 0x06000B7D RID: 2941 RVA: 0x0002DC18 File Offset: 0x0002BE18
		private void OnHorizontalScrollDragElementChanged(GeometryChangedEvent evt)
		{
			bool flag = evt.oldRect.size == evt.newRect.size;
			if (!flag)
			{
				this.UpdateHorizontalSliderPageSize();
			}
		}

		// Token: 0x06000B7E RID: 2942 RVA: 0x0002DC58 File Offset: 0x0002BE58
		private void OnVerticalScrollDragElementChanged(GeometryChangedEvent evt)
		{
			bool flag = evt.oldRect.size == evt.newRect.size;
			if (!flag)
			{
				this.UpdateVerticalSliderPageSize();
			}
		}

		// Token: 0x06000B7F RID: 2943 RVA: 0x0002DC98 File Offset: 0x0002BE98
		private void UpdateHorizontalSliderPageSize()
		{
			float width = this.horizontalScroller.resolvedStyle.width;
			float num = this.m_HorizontalPageSize;
			bool flag = width > 0f;
			if (flag)
			{
				bool flag2 = Mathf.Approximately(this.m_HorizontalPageSize, -1f);
				if (flag2)
				{
					float width2 = this.horizontalScroller.slider.dragElement.resolvedStyle.width;
					num = width2 * 0.9f;
				}
			}
			bool flag3 = num >= 0f;
			if (flag3)
			{
				this.horizontalScroller.slider.pageSize = num;
			}
		}

		// Token: 0x06000B80 RID: 2944 RVA: 0x0002DD2C File Offset: 0x0002BF2C
		private void UpdateVerticalSliderPageSize()
		{
			float height = this.verticalScroller.resolvedStyle.height;
			float num = this.m_VerticalPageSize;
			bool flag = height > 0f;
			if (flag)
			{
				bool flag2 = Mathf.Approximately(this.m_VerticalPageSize, -1f);
				if (flag2)
				{
					float height2 = this.verticalScroller.slider.dragElement.resolvedStyle.height;
					num = height2 * 0.9f;
				}
			}
			bool flag3 = num >= 0f;
			if (flag3)
			{
				this.verticalScroller.slider.pageSize = num;
			}
		}

		// Token: 0x06000B81 RID: 2945 RVA: 0x0002DDC0 File Offset: 0x0002BFC0
		private void UpdateContentViewTransform()
		{
			Vector3 position = this.contentContainer.transform.position;
			Vector2 scrollOffset = this.scrollOffset;
			bool needsVertical = this.needsVertical;
			if (needsVertical)
			{
				scrollOffset.y += this.contentContainer.resolvedStyle.top;
			}
			position.x = GUIUtility.RoundToPixelGrid(-scrollOffset.x);
			position.y = GUIUtility.RoundToPixelGrid(-scrollOffset.y);
			this.contentContainer.transform.position = position;
			base.IncrementVersion(VersionChangeType.Repaint);
		}

		// Token: 0x06000B82 RID: 2946 RVA: 0x0002DE50 File Offset: 0x0002C050
		public void ScrollTo(VisualElement child)
		{
			bool flag = child == null;
			if (flag)
			{
				throw new ArgumentNullException("child");
			}
			bool flag2 = !this.contentContainer.Contains(child);
			if (flag2)
			{
				throw new ArgumentException("Cannot scroll to a VisualElement that's not a child of the ScrollView content-container.");
			}
			this.m_Velocity = Vector2.zero;
			float num = 0f;
			float num2 = 0f;
			bool flag3 = this.scrollableHeight > 0f;
			if (flag3)
			{
				num = this.GetYDeltaOffset(child);
				this.verticalScroller.value = this.scrollOffset.y + num;
			}
			bool flag4 = this.scrollableWidth > 0f;
			if (flag4)
			{
				num2 = this.GetXDeltaOffset(child);
				this.horizontalScroller.value = this.scrollOffset.x + num2;
			}
			bool flag5 = num == 0f && num2 == 0f;
			if (!flag5)
			{
				this.UpdateContentViewTransform();
			}
		}

		// Token: 0x06000B83 RID: 2947 RVA: 0x0002DF34 File Offset: 0x0002C134
		private float GetXDeltaOffset(VisualElement child)
		{
			float num = this.contentContainer.transform.position.x * -1f;
			Rect worldBound = this.contentViewport.worldBound;
			float num2 = worldBound.xMin + num;
			float num3 = worldBound.xMax + num;
			Rect worldBound2 = child.worldBound;
			float num4 = worldBound2.xMin + num;
			float num5 = worldBound2.xMax + num;
			bool flag = (num4 >= num2 && num5 <= num3) || float.IsNaN(num4) || float.IsNaN(num5);
			float num6;
			if (flag)
			{
				num6 = 0f;
			}
			else
			{
				float deltaDistance = this.GetDeltaDistance(num2, num3, num4, num5);
				num6 = deltaDistance * this.horizontalScroller.highValue / this.scrollableWidth;
			}
			return num6;
		}

		// Token: 0x06000B84 RID: 2948 RVA: 0x0002DFF4 File Offset: 0x0002C1F4
		private float GetYDeltaOffset(VisualElement child)
		{
			float num = this.contentContainer.transform.position.y * -1f;
			Rect worldBound = this.contentViewport.worldBound;
			float num2 = worldBound.yMin + num;
			float num3 = worldBound.yMax + num;
			Rect worldBound2 = child.worldBound;
			float num4 = worldBound2.yMin + num;
			float num5 = worldBound2.yMax + num;
			bool flag = (num4 >= num2 && num5 <= num3) || float.IsNaN(num4) || float.IsNaN(num5);
			float num6;
			if (flag)
			{
				num6 = 0f;
			}
			else
			{
				float deltaDistance = this.GetDeltaDistance(num2, num3, num4, num5);
				num6 = deltaDistance * this.verticalScroller.highValue / this.scrollableHeight;
			}
			return num6;
		}

		// Token: 0x06000B85 RID: 2949 RVA: 0x0002E0B4 File Offset: 0x0002C2B4
		private float GetDeltaDistance(float viewMin, float viewMax, float childBoundaryMin, float childBoundaryMax)
		{
			float num = viewMax - viewMin;
			float num2 = childBoundaryMax - childBoundaryMin;
			bool flag = num2 > num;
			float num3;
			if (flag)
			{
				bool flag2 = viewMin > childBoundaryMin && childBoundaryMax > viewMax;
				if (flag2)
				{
					num3 = 0f;
				}
				else
				{
					num3 = ((childBoundaryMin > viewMin) ? (childBoundaryMin - viewMin) : (childBoundaryMax - viewMax));
				}
			}
			else
			{
				float num4 = childBoundaryMax - viewMax;
				bool flag3 = num4 < -1f;
				if (flag3)
				{
					num4 = childBoundaryMin - viewMin;
				}
				num3 = num4;
			}
			return num3;
		}

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x06000B86 RID: 2950 RVA: 0x0002E120 File Offset: 0x0002C320
		// (set) Token: 0x06000B87 RID: 2951 RVA: 0x0002E128 File Offset: 0x0002C328
		public VisualElement contentViewport { get; private set; }

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x06000B88 RID: 2952 RVA: 0x0002E131 File Offset: 0x0002C331
		// (set) Token: 0x06000B89 RID: 2953 RVA: 0x0002E139 File Offset: 0x0002C339
		public Scroller horizontalScroller { get; private set; }

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x06000B8A RID: 2954 RVA: 0x0002E142 File Offset: 0x0002C342
		// (set) Token: 0x06000B8B RID: 2955 RVA: 0x0002E14A File Offset: 0x0002C34A
		public Scroller verticalScroller { get; private set; }

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x06000B8C RID: 2956 RVA: 0x0002E154 File Offset: 0x0002C354
		public override VisualElement contentContainer
		{
			get
			{
				return this.m_ContentContainer;
			}
		}

		// Token: 0x06000B8D RID: 2957 RVA: 0x0002E16C File Offset: 0x0002C36C
		public ScrollView()
			: this(ScrollViewMode.Vertical)
		{
		}

		// Token: 0x06000B8E RID: 2958 RVA: 0x0002E178 File Offset: 0x0002C378
		public ScrollView(ScrollViewMode scrollViewMode)
		{
			base.AddToClassList(ScrollView.ussClassName);
			this.m_ContentAndVerticalScrollContainer = new VisualElement
			{
				name = "unity-content-and-vertical-scroll-container"
			};
			this.m_ContentAndVerticalScrollContainer.AddToClassList(ScrollView.contentAndVerticalScrollUssClassName);
			base.hierarchy.Add(this.m_ContentAndVerticalScrollContainer);
			this.contentViewport = new VisualElement
			{
				name = "unity-content-viewport"
			};
			this.contentViewport.AddToClassList(ScrollView.viewportUssClassName);
			this.contentViewport.RegisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.OnGeometryChanged), TrickleDown.NoTrickleDown);
			this.contentViewport.pickingMode = PickingMode.Ignore;
			this.m_ContentAndVerticalScrollContainer.RegisterCallback<AttachToPanelEvent>(new EventCallback<AttachToPanelEvent>(this.OnAttachToPanel), TrickleDown.NoTrickleDown);
			this.m_ContentAndVerticalScrollContainer.RegisterCallback<DetachFromPanelEvent>(new EventCallback<DetachFromPanelEvent>(this.OnDetachFromPanel), TrickleDown.NoTrickleDown);
			this.m_ContentAndVerticalScrollContainer.Add(this.contentViewport);
			this.m_ContentContainer = new VisualElement
			{
				name = "unity-content-container"
			};
			this.m_ContentContainer.disableClipping = true;
			this.m_ContentContainer.RegisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.OnGeometryChanged), TrickleDown.NoTrickleDown);
			this.m_ContentContainer.AddToClassList(ScrollView.contentUssClassName);
			this.m_ContentContainer.usageHints = UsageHints.GroupTransform;
			this.contentViewport.Add(this.m_ContentContainer);
			this.SetScrollViewMode(scrollViewMode);
			this.horizontalScroller = new Scroller(0f, 2.1474836E+09f, delegate(float value)
			{
				this.scrollOffset = new Vector2(value, this.scrollOffset.y);
				this.UpdateContentViewTransform();
			}, SliderDirection.Horizontal)
			{
				viewDataKey = "HorizontalScroller"
			};
			this.horizontalScroller.AddToClassList(ScrollView.hScrollerUssClassName);
			this.horizontalScroller.style.display = DisplayStyle.None;
			base.hierarchy.Add(this.horizontalScroller);
			this.verticalScroller = new Scroller(0f, 2.1474836E+09f, delegate(float value)
			{
				this.scrollOffset = new Vector2(this.scrollOffset.x, value);
				this.UpdateContentViewTransform();
			}, SliderDirection.Vertical)
			{
				viewDataKey = "VerticalScroller"
			};
			this.verticalScroller.AddToClassList(ScrollView.vScrollerUssClassName);
			this.verticalScroller.style.display = DisplayStyle.None;
			this.m_ContentAndVerticalScrollContainer.Add(this.verticalScroller);
			this.touchScrollBehavior = ScrollView.TouchScrollBehavior.Clamped;
			base.RegisterCallback<WheelEvent>(new EventCallback<WheelEvent>(this.OnScrollWheel), TrickleDown.NoTrickleDown);
			this.verticalScroller.RegisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.OnScrollersGeometryChanged), TrickleDown.NoTrickleDown);
			this.horizontalScroller.RegisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.OnScrollersGeometryChanged), TrickleDown.NoTrickleDown);
			this.horizontalPageSize = -1f;
			this.verticalPageSize = -1f;
			this.horizontalScroller.slider.dragElement.RegisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.OnHorizontalScrollDragElementChanged), TrickleDown.NoTrickleDown);
			this.verticalScroller.slider.dragElement.RegisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.OnVerticalScrollDragElementChanged), TrickleDown.NoTrickleDown);
			this.m_CapturedTargetPointerMoveCallback = new EventCallback<PointerMoveEvent>(this.OnPointerMove);
			this.m_CapturedTargetPointerUpCallback = new EventCallback<PointerUpEvent>(this.OnPointerUp);
			this.scrollOffset = Vector2.zero;
		}

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x06000B8F RID: 2959 RVA: 0x0002E4C6 File Offset: 0x0002C6C6
		// (set) Token: 0x06000B90 RID: 2960 RVA: 0x0002E4D0 File Offset: 0x0002C6D0
		public ScrollViewMode mode
		{
			get
			{
				return this.m_Mode;
			}
			set
			{
				bool flag = this.m_Mode == value;
				if (!flag)
				{
					this.SetScrollViewMode(value);
				}
			}
		}

		// Token: 0x06000B91 RID: 2961 RVA: 0x0002E4F8 File Offset: 0x0002C6F8
		private void SetScrollViewMode(ScrollViewMode mode)
		{
			this.m_Mode = mode;
			base.RemoveFromClassList(ScrollView.verticalVariantUssClassName);
			base.RemoveFromClassList(ScrollView.horizontalVariantUssClassName);
			base.RemoveFromClassList(ScrollView.verticalHorizontalVariantUssClassName);
			base.RemoveFromClassList(ScrollView.scrollVariantUssClassName);
			switch (mode)
			{
			case ScrollViewMode.Vertical:
				base.AddToClassList(ScrollView.verticalVariantUssClassName);
				base.AddToClassList(ScrollView.scrollVariantUssClassName);
				break;
			case ScrollViewMode.Horizontal:
				base.AddToClassList(ScrollView.horizontalVariantUssClassName);
				base.AddToClassList(ScrollView.scrollVariantUssClassName);
				break;
			case ScrollViewMode.VerticalAndHorizontal:
				base.AddToClassList(ScrollView.scrollVariantUssClassName);
				base.AddToClassList(ScrollView.verticalHorizontalVariantUssClassName);
				break;
			}
		}

		// Token: 0x06000B92 RID: 2962 RVA: 0x0002E5A4 File Offset: 0x0002C7A4
		private void OnAttachToPanel(AttachToPanelEvent evt)
		{
			bool flag = evt.destinationPanel == null;
			if (!flag)
			{
				this.m_AttachedRootVisualContainer = base.GetRootVisualContainer();
				VisualElement attachedRootVisualContainer = this.m_AttachedRootVisualContainer;
				if (attachedRootVisualContainer != null)
				{
					attachedRootVisualContainer.RegisterCallback<CustomStyleResolvedEvent>(new EventCallback<CustomStyleResolvedEvent>(this.OnRootCustomStyleResolved), TrickleDown.NoTrickleDown);
				}
				this.ReadSingleLineHeight();
				bool flag2 = evt.destinationPanel.contextType == ContextType.Player;
				if (flag2)
				{
					this.m_ContentAndVerticalScrollContainer.RegisterCallback<PointerMoveEvent>(new EventCallback<PointerMoveEvent>(this.OnPointerMove), TrickleDown.NoTrickleDown);
					this.contentContainer.RegisterCallback<PointerDownEvent>(new EventCallback<PointerDownEvent>(this.OnPointerDown), TrickleDown.TrickleDown);
					this.contentContainer.RegisterCallback<PointerCancelEvent>(new EventCallback<PointerCancelEvent>(this.OnPointerCancel), TrickleDown.NoTrickleDown);
					this.contentContainer.RegisterCallback<PointerUpEvent>(new EventCallback<PointerUpEvent>(this.OnPointerUp), TrickleDown.TrickleDown);
					this.contentContainer.RegisterCallback<PointerCaptureEvent>(new EventCallback<PointerCaptureEvent>(this.OnPointerCapture), TrickleDown.NoTrickleDown);
					this.contentContainer.RegisterCallback<PointerCaptureOutEvent>(new EventCallback<PointerCaptureOutEvent>(this.OnPointerCaptureOut), TrickleDown.NoTrickleDown);
				}
			}
		}

		// Token: 0x06000B93 RID: 2963 RVA: 0x0002E6A4 File Offset: 0x0002C8A4
		private void OnDetachFromPanel(DetachFromPanelEvent evt)
		{
			bool flag = evt.originPanel == null;
			if (!flag)
			{
				VisualElement attachedRootVisualContainer = this.m_AttachedRootVisualContainer;
				if (attachedRootVisualContainer != null)
				{
					attachedRootVisualContainer.UnregisterCallback<CustomStyleResolvedEvent>(new EventCallback<CustomStyleResolvedEvent>(this.OnRootCustomStyleResolved), TrickleDown.NoTrickleDown);
				}
				this.m_AttachedRootVisualContainer = null;
				bool flag2 = evt.originPanel.contextType == ContextType.Player;
				if (flag2)
				{
					this.m_ContentAndVerticalScrollContainer.UnregisterCallback<PointerDownEvent>(new EventCallback<PointerDownEvent>(this.OnPointerDown), TrickleDown.TrickleDown);
					this.m_ContentAndVerticalScrollContainer.UnregisterCallback<PointerMoveEvent>(new EventCallback<PointerMoveEvent>(this.OnPointerMove), TrickleDown.NoTrickleDown);
					this.m_ContentAndVerticalScrollContainer.UnregisterCallback<PointerCancelEvent>(new EventCallback<PointerCancelEvent>(this.OnPointerCancel), TrickleDown.NoTrickleDown);
					this.m_ContentAndVerticalScrollContainer.UnregisterCallback<PointerUpEvent>(new EventCallback<PointerUpEvent>(this.OnPointerUp), TrickleDown.TrickleDown);
					this.contentContainer.UnregisterCallback<PointerCaptureEvent>(new EventCallback<PointerCaptureEvent>(this.OnPointerCapture), TrickleDown.NoTrickleDown);
					this.contentContainer.UnregisterCallback<PointerCaptureOutEvent>(new EventCallback<PointerCaptureOutEvent>(this.OnPointerCaptureOut), TrickleDown.NoTrickleDown);
				}
			}
		}

		// Token: 0x06000B94 RID: 2964 RVA: 0x0002E798 File Offset: 0x0002C998
		private void OnPointerCapture(PointerCaptureEvent evt)
		{
			this.m_CapturedTarget = evt.target as VisualElement;
			bool flag = this.m_CapturedTarget == null;
			if (!flag)
			{
				this.m_ScrollingPointerId = evt.pointerId;
				this.m_CapturedTarget.RegisterCallback<PointerMoveEvent>(this.m_CapturedTargetPointerMoveCallback, TrickleDown.NoTrickleDown);
				this.m_CapturedTarget.RegisterCallback<PointerUpEvent>(this.m_CapturedTargetPointerUpCallback, TrickleDown.NoTrickleDown);
			}
		}

		// Token: 0x06000B95 RID: 2965 RVA: 0x0002E7F8 File Offset: 0x0002C9F8
		private void OnPointerCaptureOut(PointerCaptureOutEvent evt)
		{
			this.ReleaseScrolling(evt.pointerId, evt.target);
			bool flag = this.m_CapturedTarget == null;
			if (!flag)
			{
				this.m_CapturedTarget.UnregisterCallback<PointerMoveEvent>(this.m_CapturedTargetPointerMoveCallback, TrickleDown.NoTrickleDown);
				this.m_CapturedTarget.UnregisterCallback<PointerUpEvent>(this.m_CapturedTargetPointerUpCallback, TrickleDown.NoTrickleDown);
				this.m_CapturedTarget = null;
			}
		}

		// Token: 0x06000B96 RID: 2966 RVA: 0x0002E858 File Offset: 0x0002CA58
		private void OnGeometryChanged(GeometryChangedEvent evt)
		{
			bool flag = evt.oldRect.size == evt.newRect.size;
			if (!flag)
			{
				bool flag2 = this.needsVertical;
				bool flag3 = this.needsHorizontal;
				bool flag4 = evt.layoutPass > 0;
				if (flag4)
				{
					flag2 = flag2 || this.isVerticalScrollDisplayed;
					flag3 = flag3 || this.isHorizontalScrollDisplayed;
				}
				this.UpdateScrollers(flag3, flag2);
				this.UpdateContentViewTransform();
			}
		}

		// Token: 0x06000B97 RID: 2967 RVA: 0x0002E8D8 File Offset: 0x0002CAD8
		private static float ComputeElasticOffset(float deltaPointer, float initialScrollOffset, float lowLimit, float hardLowLimit, float highLimit, float hardHighLimit)
		{
			initialScrollOffset = Mathf.Max(initialScrollOffset, hardLowLimit * 0.95f);
			initialScrollOffset = Mathf.Min(initialScrollOffset, hardHighLimit * 0.95f);
			bool flag = initialScrollOffset < lowLimit && hardLowLimit < lowLimit;
			float num;
			float num3;
			if (flag)
			{
				num = lowLimit - hardLowLimit;
				float num2 = (lowLimit - initialScrollOffset) / num;
				num3 = num2 * num / (1f - num2);
				num3 += deltaPointer;
				initialScrollOffset = lowLimit;
			}
			else
			{
				bool flag2 = initialScrollOffset > highLimit && hardHighLimit > highLimit;
				if (flag2)
				{
					num = hardHighLimit - highLimit;
					float num4 = (initialScrollOffset - highLimit) / num;
					num3 = -1f * num4 * num / (1f - num4);
					num3 += deltaPointer;
					initialScrollOffset = highLimit;
				}
				else
				{
					num3 = deltaPointer;
				}
			}
			float num5 = initialScrollOffset - num3;
			bool flag3 = num5 < lowLimit;
			float num6;
			if (flag3)
			{
				num3 = lowLimit - num5;
				initialScrollOffset = lowLimit;
				num = lowLimit - hardLowLimit;
				num6 = 1f;
			}
			else
			{
				bool flag4 = num5 <= highLimit;
				if (flag4)
				{
					return num5;
				}
				num3 = num5 - highLimit;
				initialScrollOffset = highLimit;
				num = hardHighLimit - highLimit;
				num6 = -1f;
			}
			bool flag5 = Mathf.Abs(num3) < 1E-30f;
			float num7;
			if (flag5)
			{
				num7 = initialScrollOffset;
			}
			else
			{
				float num8 = num3 / (num3 + num);
				num8 *= num;
				num8 *= num6;
				num5 = initialScrollOffset - num8;
				num7 = num5;
			}
			return num7;
		}

		// Token: 0x06000B98 RID: 2968 RVA: 0x0002EA08 File Offset: 0x0002CC08
		private void ComputeInitialSpringBackVelocity()
		{
			bool flag = this.touchScrollBehavior != ScrollView.TouchScrollBehavior.Elastic;
			if (flag)
			{
				this.m_SpringBackVelocity = Vector2.zero;
			}
			else
			{
				bool flag2 = this.scrollOffset.x < this.m_LowBounds.x;
				if (flag2)
				{
					this.m_SpringBackVelocity.x = this.m_LowBounds.x - this.scrollOffset.x;
				}
				else
				{
					bool flag3 = this.scrollOffset.x > this.m_HighBounds.x;
					if (flag3)
					{
						this.m_SpringBackVelocity.x = this.m_HighBounds.x - this.scrollOffset.x;
					}
					else
					{
						this.m_SpringBackVelocity.x = 0f;
					}
				}
				bool flag4 = this.scrollOffset.y < this.m_LowBounds.y;
				if (flag4)
				{
					this.m_SpringBackVelocity.y = this.m_LowBounds.y - this.scrollOffset.y;
				}
				else
				{
					bool flag5 = this.scrollOffset.y > this.m_HighBounds.y;
					if (flag5)
					{
						this.m_SpringBackVelocity.y = this.m_HighBounds.y - this.scrollOffset.y;
					}
					else
					{
						this.m_SpringBackVelocity.y = 0f;
					}
				}
			}
		}

		// Token: 0x06000B99 RID: 2969 RVA: 0x0002EB68 File Offset: 0x0002CD68
		private void SpringBack()
		{
			bool flag = this.touchScrollBehavior != ScrollView.TouchScrollBehavior.Elastic;
			if (flag)
			{
				this.m_SpringBackVelocity = Vector2.zero;
			}
			else
			{
				Vector2 scrollOffset = this.scrollOffset;
				bool flag2 = scrollOffset.x < this.m_LowBounds.x;
				if (flag2)
				{
					scrollOffset.x = Mathf.SmoothDamp(scrollOffset.x, this.m_LowBounds.x, ref this.m_SpringBackVelocity.x, this.elasticity, float.PositiveInfinity, Time.unscaledDeltaTime);
					bool flag3 = Mathf.Abs(this.m_SpringBackVelocity.x) < 1f;
					if (flag3)
					{
						this.m_SpringBackVelocity.x = 0f;
					}
				}
				else
				{
					bool flag4 = scrollOffset.x > this.m_HighBounds.x;
					if (flag4)
					{
						scrollOffset.x = Mathf.SmoothDamp(scrollOffset.x, this.m_HighBounds.x, ref this.m_SpringBackVelocity.x, this.elasticity, float.PositiveInfinity, Time.unscaledDeltaTime);
						bool flag5 = Mathf.Abs(this.m_SpringBackVelocity.x) < 1f;
						if (flag5)
						{
							this.m_SpringBackVelocity.x = 0f;
						}
					}
					else
					{
						this.m_SpringBackVelocity.x = 0f;
					}
				}
				bool flag6 = scrollOffset.y < this.m_LowBounds.y;
				if (flag6)
				{
					scrollOffset.y = Mathf.SmoothDamp(scrollOffset.y, this.m_LowBounds.y, ref this.m_SpringBackVelocity.y, this.elasticity, float.PositiveInfinity, Time.unscaledDeltaTime);
					bool flag7 = Mathf.Abs(this.m_SpringBackVelocity.y) < 1f;
					if (flag7)
					{
						this.m_SpringBackVelocity.y = 0f;
					}
				}
				else
				{
					bool flag8 = scrollOffset.y > this.m_HighBounds.y;
					if (flag8)
					{
						scrollOffset.y = Mathf.SmoothDamp(scrollOffset.y, this.m_HighBounds.y, ref this.m_SpringBackVelocity.y, this.elasticity, float.PositiveInfinity, Time.unscaledDeltaTime);
						bool flag9 = Mathf.Abs(this.m_SpringBackVelocity.y) < 1f;
						if (flag9)
						{
							this.m_SpringBackVelocity.y = 0f;
						}
					}
					else
					{
						this.m_SpringBackVelocity.y = 0f;
					}
				}
				this.scrollOffset = scrollOffset;
			}
		}

		// Token: 0x06000B9A RID: 2970 RVA: 0x0002EDDC File Offset: 0x0002CFDC
		internal void ApplyScrollInertia()
		{
			bool flag = this.hasInertia && this.m_Velocity != Vector2.zero;
			if (flag)
			{
				this.m_Velocity *= Mathf.Pow(this.scrollDecelerationRate, Time.unscaledDeltaTime);
				bool flag2 = Mathf.Abs(this.m_Velocity.x) < 1f || (this.touchScrollBehavior == ScrollView.TouchScrollBehavior.Elastic && (this.scrollOffset.x < this.m_LowBounds.x || this.scrollOffset.x > this.m_HighBounds.x));
				if (flag2)
				{
					this.m_Velocity.x = 0f;
				}
				bool flag3 = Mathf.Abs(this.m_Velocity.y) < 1f || (this.touchScrollBehavior == ScrollView.TouchScrollBehavior.Elastic && (this.scrollOffset.y < this.m_LowBounds.y || this.scrollOffset.y > this.m_HighBounds.y));
				if (flag3)
				{
					this.m_Velocity.y = 0f;
				}
				this.scrollOffset += this.m_Velocity * Time.unscaledDeltaTime;
			}
			else
			{
				this.m_Velocity = Vector2.zero;
			}
		}

		// Token: 0x06000B9B RID: 2971 RVA: 0x0002EF40 File Offset: 0x0002D140
		private void PostPointerUpAnimation()
		{
			this.ApplyScrollInertia();
			this.SpringBack();
			bool flag = this.m_SpringBackVelocity == Vector2.zero && this.m_Velocity == Vector2.zero;
			if (flag)
			{
				this.m_PostPointerUpAnimation.Pause();
			}
		}

		// Token: 0x06000B9C RID: 2972 RVA: 0x0002EF94 File Offset: 0x0002D194
		private void OnPointerDown(PointerDownEvent evt)
		{
			bool flag = evt.pointerType == PointerType.mouse || !evt.isPrimary;
			if (!flag)
			{
				bool flag2 = this.m_ScrollingPointerId != PointerId.invalidPointerId;
				if (flag2)
				{
					this.ReleaseScrolling(this.m_ScrollingPointerId, evt.target);
				}
				IVisualElementScheduledItem postPointerUpAnimation = this.m_PostPointerUpAnimation;
				if (postPointerUpAnimation != null)
				{
					postPointerUpAnimation.Pause();
				}
				bool flag3 = Mathf.Abs(this.m_Velocity.x) > 10f || Mathf.Abs(this.m_Velocity.y) > 10f;
				this.m_ScrollingPointerId = evt.pointerId;
				this.m_StartedMoving = false;
				this.InitTouchScrolling(evt.position);
				bool flag4 = flag3;
				if (flag4)
				{
					this.contentContainer.CapturePointer(evt.pointerId);
					this.contentContainer.panel.PreventCompatibilityMouseEvents(evt.pointerId);
					evt.StopPropagation();
					this.m_TouchStoppedVelocity = true;
				}
			}
		}

		// Token: 0x06000B9D RID: 2973 RVA: 0x0002F09C File Offset: 0x0002D29C
		private void OnPointerMove(PointerMoveEvent evt)
		{
			bool flag = evt.pointerType == PointerType.mouse || !evt.isPrimary || evt.pointerId != this.m_ScrollingPointerId;
			if (!flag)
			{
				bool isHandledByDraggable = evt.isHandledByDraggable;
				if (isHandledByDraggable)
				{
					this.m_PointerStartPosition = evt.position;
					this.m_StartPosition = this.scrollOffset;
				}
				else
				{
					Vector2 vector = evt.position;
					Vector2 vector2 = vector - this.m_PointerStartPosition;
					bool flag2 = this.mode == ScrollViewMode.Horizontal;
					if (flag2)
					{
						vector2.y = 0f;
					}
					else
					{
						bool flag3 = this.mode == ScrollViewMode.Vertical;
						if (flag3)
						{
							vector2.x = 0f;
						}
					}
					bool flag4 = !this.m_TouchStoppedVelocity && !this.m_StartedMoving && vector2.sqrMagnitude < 100f;
					if (!flag4)
					{
						ScrollView.TouchScrollingResult touchScrollingResult = this.ComputeTouchScrolling(evt.position);
						bool flag5 = touchScrollingResult != ScrollView.TouchScrollingResult.Forward;
						if (flag5)
						{
							evt.isHandledByDraggable = true;
							evt.StopPropagation();
							bool flag6 = !this.contentContainer.HasPointerCapture(evt.pointerId);
							if (flag6)
							{
								this.contentContainer.CapturePointer(evt.pointerId);
							}
						}
						else
						{
							this.m_Velocity = Vector2.zero;
						}
					}
				}
			}
		}

		// Token: 0x06000B9E RID: 2974 RVA: 0x0002F1F5 File Offset: 0x0002D3F5
		private void OnPointerCancel(PointerCancelEvent evt)
		{
			this.ReleaseScrolling(evt.pointerId, evt.target);
		}

		// Token: 0x06000B9F RID: 2975 RVA: 0x0002F20C File Offset: 0x0002D40C
		private void OnPointerUp(PointerUpEvent evt)
		{
			bool flag = this.ReleaseScrolling(evt.pointerId, evt.target);
			if (flag)
			{
				this.contentContainer.panel.PreventCompatibilityMouseEvents(evt.pointerId);
				evt.StopPropagation();
			}
		}

		// Token: 0x06000BA0 RID: 2976 RVA: 0x0002F250 File Offset: 0x0002D450
		internal void InitTouchScrolling(Vector2 position)
		{
			this.m_PointerStartPosition = position;
			this.m_StartPosition = this.scrollOffset;
			this.m_Velocity = Vector2.zero;
			this.m_SpringBackVelocity = Vector2.zero;
			this.m_LowBounds = new Vector2(Mathf.Min(this.horizontalScroller.lowValue, this.horizontalScroller.highValue), Mathf.Min(this.verticalScroller.lowValue, this.verticalScroller.highValue));
			this.m_HighBounds = new Vector2(Mathf.Max(this.horizontalScroller.lowValue, this.horizontalScroller.highValue), Mathf.Max(this.verticalScroller.lowValue, this.verticalScroller.highValue));
		}

		// Token: 0x06000BA1 RID: 2977 RVA: 0x0002F30C File Offset: 0x0002D50C
		internal ScrollView.TouchScrollingResult ComputeTouchScrolling(Vector2 position)
		{
			bool flag = this.touchScrollBehavior == ScrollView.TouchScrollBehavior.Clamped;
			Vector2 vector;
			if (flag)
			{
				vector = this.m_StartPosition - (position - this.m_PointerStartPosition);
				vector = Vector2.Max(vector, this.m_LowBounds);
				vector = Vector2.Min(vector, this.m_HighBounds);
			}
			else
			{
				bool flag2 = this.touchScrollBehavior == ScrollView.TouchScrollBehavior.Elastic;
				if (flag2)
				{
					Vector2 vector2 = position - this.m_PointerStartPosition;
					vector.x = ScrollView.ComputeElasticOffset(vector2.x, this.m_StartPosition.x, this.m_LowBounds.x, this.m_LowBounds.x - this.contentViewport.resolvedStyle.width, this.m_HighBounds.x, this.m_HighBounds.x + this.contentViewport.resolvedStyle.width);
					vector.y = ScrollView.ComputeElasticOffset(vector2.y, this.m_StartPosition.y, this.m_LowBounds.y, this.m_LowBounds.y - this.contentViewport.resolvedStyle.height, this.m_HighBounds.y, this.m_HighBounds.y + this.contentViewport.resolvedStyle.height);
				}
				else
				{
					vector = this.m_StartPosition - (position - this.m_PointerStartPosition);
				}
			}
			bool flag3 = this.mode == ScrollViewMode.Vertical;
			if (flag3)
			{
				vector.x = this.m_LowBounds.x;
			}
			else
			{
				bool flag4 = this.mode == ScrollViewMode.Horizontal;
				if (flag4)
				{
					vector.y = this.m_LowBounds.y;
				}
			}
			bool flag5 = this.scrollOffset != vector;
			bool flag6 = flag5;
			ScrollView.TouchScrollingResult touchScrollingResult;
			if (flag6)
			{
				touchScrollingResult = (this.ApplyTouchScrolling(vector) ? ScrollView.TouchScrollingResult.Apply : ScrollView.TouchScrollingResult.Forward);
			}
			else
			{
				touchScrollingResult = ((this.m_StartedMoving && this.nestedInteractionKind != ScrollView.NestedInteractionKind.ForwardScrolling) ? ScrollView.TouchScrollingResult.Block : ScrollView.TouchScrollingResult.Forward);
			}
			return touchScrollingResult;
		}

		// Token: 0x06000BA2 RID: 2978 RVA: 0x0002F508 File Offset: 0x0002D708
		private bool ApplyTouchScrolling(Vector2 newScrollOffset)
		{
			this.m_StartedMoving = true;
			bool hasInertia = this.hasInertia;
			if (hasInertia)
			{
				bool flag = newScrollOffset == this.m_LowBounds || newScrollOffset == this.m_HighBounds;
				if (flag)
				{
					this.m_Velocity = Vector2.zero;
					this.scrollOffset = newScrollOffset;
					return false;
				}
				bool flag2 = this.m_LastVelocityLerpTime > 0f;
				if (flag2)
				{
					float num = Time.unscaledTime - this.m_LastVelocityLerpTime;
					this.m_Velocity = Vector2.Lerp(this.m_Velocity, Vector2.zero, num * 10f);
				}
				this.m_LastVelocityLerpTime = Time.unscaledTime;
				float unscaledDeltaTime = Time.unscaledDeltaTime;
				Vector2 vector = (newScrollOffset - this.scrollOffset) / unscaledDeltaTime;
				this.m_Velocity = Vector2.Lerp(this.m_Velocity, vector, unscaledDeltaTime * 10f);
			}
			bool flag3 = this.scrollOffset != newScrollOffset;
			this.scrollOffset = newScrollOffset;
			return flag3;
		}

		// Token: 0x06000BA3 RID: 2979 RVA: 0x0002F608 File Offset: 0x0002D808
		private bool ReleaseScrolling(int pointerId, IEventHandler target)
		{
			bool flag = pointerId != this.m_ScrollingPointerId;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				this.m_ScrollingPointerId = PointerId.invalidPointerId;
				this.m_TouchStoppedVelocity = false;
				this.m_StartedMoving = false;
				bool flag3 = target != this.contentContainer || !this.contentContainer.HasPointerCapture(pointerId);
				if (flag3)
				{
					flag2 = false;
				}
				else
				{
					bool flag4 = this.touchScrollBehavior == ScrollView.TouchScrollBehavior.Elastic || this.hasInertia;
					if (flag4)
					{
						this.ComputeInitialSpringBackVelocity();
						bool flag5 = this.m_PostPointerUpAnimation == null;
						if (flag5)
						{
							this.m_PostPointerUpAnimation = base.schedule.Execute(new Action(this.PostPointerUpAnimation)).Every(30L);
						}
						else
						{
							this.m_PostPointerUpAnimation.Resume();
						}
					}
					this.contentContainer.ReleasePointer(pointerId);
					flag2 = true;
				}
			}
			return flag2;
		}

		// Token: 0x06000BA4 RID: 2980 RVA: 0x0002F6E0 File Offset: 0x0002D8E0
		private void AdjustScrollers()
		{
			float num = ((this.contentContainer.boundingBox.width > 1E-30f) ? (this.contentViewport.layout.width / this.contentContainer.boundingBox.width) : 1f);
			float num2 = ((this.contentContainer.boundingBox.height > 1E-30f) ? (this.contentViewport.layout.height / this.contentContainer.boundingBox.height) : 1f);
			this.horizontalScroller.Adjust(num);
			this.verticalScroller.Adjust(num2);
		}

		// Token: 0x06000BA5 RID: 2981 RVA: 0x0002F79C File Offset: 0x0002D99C
		internal void UpdateScrollers(bool displayHorizontal, bool displayVertical)
		{
			this.AdjustScrollers();
			this.horizontalScroller.SetEnabled(this.contentContainer.boundingBox.width - this.contentViewport.layout.width > 0f);
			this.verticalScroller.SetEnabled(this.contentContainer.boundingBox.height - this.contentViewport.layout.height > 0f);
			bool flag = displayHorizontal && this.m_HorizontalScrollerVisibility != ScrollerVisibility.Hidden;
			bool flag2 = displayVertical && this.m_VerticalScrollerVisibility != ScrollerVisibility.Hidden;
			DisplayStyle displayStyle = (flag ? DisplayStyle.Flex : DisplayStyle.None);
			DisplayStyle displayStyle2 = (flag2 ? DisplayStyle.Flex : DisplayStyle.None);
			bool flag3 = displayStyle != this.horizontalScroller.style.display;
			if (flag3)
			{
				this.horizontalScroller.style.display = displayStyle;
			}
			bool flag4 = displayStyle2 != this.verticalScroller.style.display;
			if (flag4)
			{
				this.verticalScroller.style.display = displayStyle2;
			}
			this.verticalScroller.lowValue = 0f;
			this.verticalScroller.highValue = this.scrollableHeight;
			this.horizontalScroller.lowValue = 0f;
			this.horizontalScroller.highValue = this.scrollableWidth;
			bool flag5 = !this.needsVertical || this.scrollableHeight <= 0f;
			if (flag5)
			{
				this.verticalScroller.value = 0f;
			}
			bool flag6 = !this.needsHorizontal || this.scrollableWidth <= 0f;
			if (flag6)
			{
				this.horizontalScroller.value = 0f;
			}
		}

		// Token: 0x06000BA6 RID: 2982 RVA: 0x0002F984 File Offset: 0x0002DB84
		private void OnScrollersGeometryChanged(GeometryChangedEvent evt)
		{
			bool flag = evt.oldRect.size == evt.newRect.size;
			if (!flag)
			{
				bool flag2 = this.needsHorizontal && this.m_HorizontalScrollerVisibility != ScrollerVisibility.Hidden;
				bool flag3 = flag2;
				if (flag3)
				{
					this.horizontalScroller.style.marginRight = this.verticalScroller.layout.width;
				}
				this.AdjustScrollers();
			}
		}

		// Token: 0x06000BA7 RID: 2983 RVA: 0x0002FA0C File Offset: 0x0002DC0C
		private void OnScrollWheel(WheelEvent evt)
		{
			bool flag = false;
			bool flag2 = this.contentContainer.boundingBox.height - base.layout.height > 0f;
			bool flag3 = this.contentContainer.boundingBox.width - base.layout.width > 0f;
			float num = ((flag3 && !flag2) ? evt.delta.y : evt.delta.x);
			float num2 = (this.m_MouseWheelScrollSizeIsInline ? this.mouseWheelScrollSize : this.m_SingleLineHeight);
			bool flag4 = flag2;
			if (flag4)
			{
				float value = this.verticalScroller.value;
				this.verticalScroller.value += evt.delta.y * ((this.verticalScroller.lowValue < this.verticalScroller.highValue) ? 1f : (-1f)) * num2;
				bool flag5 = this.nestedInteractionKind == ScrollView.NestedInteractionKind.StopScrolling || !Mathf.Approximately(this.verticalScroller.value, value);
				if (flag5)
				{
					evt.StopPropagation();
					flag = true;
				}
			}
			bool flag6 = flag3;
			if (flag6)
			{
				float value2 = this.horizontalScroller.value;
				this.horizontalScroller.value += num * ((this.horizontalScroller.lowValue < this.horizontalScroller.highValue) ? 1f : (-1f)) * num2;
				bool flag7 = this.nestedInteractionKind == ScrollView.NestedInteractionKind.StopScrolling || !Mathf.Approximately(this.horizontalScroller.value, value2);
				if (flag7)
				{
					evt.StopPropagation();
					flag = true;
				}
			}
			bool flag8 = flag;
			if (flag8)
			{
				this.UpdateContentViewTransform();
			}
		}

		// Token: 0x06000BA8 RID: 2984 RVA: 0x0002FBCE File Offset: 0x0002DDCE
		private void OnRootCustomStyleResolved(CustomStyleResolvedEvent evt)
		{
			this.ReadSingleLineHeight();
		}

		// Token: 0x06000BA9 RID: 2985 RVA: 0x0002FBD8 File Offset: 0x0002DDD8
		private void ReadSingleLineHeight()
		{
			VisualElement attachedRootVisualContainer = this.m_AttachedRootVisualContainer;
			StylePropertyValue stylePropertyValue;
			bool flag = ((attachedRootVisualContainer != null) ? attachedRootVisualContainer.computedStyle.customProperties : null) != null && this.m_AttachedRootVisualContainer.computedStyle.customProperties.TryGetValue("--unity-metrics-single_line-height", ref stylePropertyValue);
			if (flag)
			{
				Dimension dimension;
				bool flag2 = stylePropertyValue.sheet.TryReadDimension(stylePropertyValue.handle, out dimension);
				if (flag2)
				{
					this.m_SingleLineHeight = dimension.value;
				}
			}
			else
			{
				this.m_SingleLineHeight = UIElementsUtility.singleLineHeight;
			}
		}

		// Token: 0x04000530 RID: 1328
		private ScrollerVisibility m_HorizontalScrollerVisibility;

		// Token: 0x04000531 RID: 1329
		private ScrollerVisibility m_VerticalScrollerVisibility;

		// Token: 0x04000532 RID: 1330
		private const float k_SizeThreshold = 0.001f;

		// Token: 0x04000533 RID: 1331
		private VisualElement m_AttachedRootVisualContainer;

		// Token: 0x04000534 RID: 1332
		private float m_SingleLineHeight = UIElementsUtility.singleLineHeight;

		// Token: 0x04000535 RID: 1333
		private const string k_SingleLineHeightPropertyName = "--unity-metrics-single_line-height";

		// Token: 0x04000536 RID: 1334
		private const float k_ScrollPageOverlapFactor = 0.1f;

		// Token: 0x04000537 RID: 1335
		internal const float k_UnsetPageSizeValue = -1f;

		// Token: 0x04000538 RID: 1336
		internal const float k_MouseWheelScrollSizeDefaultValue = 18f;

		// Token: 0x04000539 RID: 1337
		internal const float k_MouseWheelScrollSizeUnset = -1f;

		// Token: 0x0400053A RID: 1338
		internal bool m_MouseWheelScrollSizeIsInline;

		// Token: 0x0400053B RID: 1339
		private float m_HorizontalPageSize;

		// Token: 0x0400053C RID: 1340
		private float m_VerticalPageSize;

		// Token: 0x0400053D RID: 1341
		private float m_MouseWheelScrollSize = 18f;

		// Token: 0x0400053E RID: 1342
		private static readonly float k_DefaultScrollDecelerationRate = 0.135f;

		// Token: 0x0400053F RID: 1343
		private float m_ScrollDecelerationRate = ScrollView.k_DefaultScrollDecelerationRate;

		// Token: 0x04000540 RID: 1344
		private static readonly float k_DefaultElasticity = 0.1f;

		// Token: 0x04000541 RID: 1345
		private float m_Elasticity = ScrollView.k_DefaultElasticity;

		// Token: 0x04000542 RID: 1346
		private ScrollView.TouchScrollBehavior m_TouchScrollBehavior;

		// Token: 0x04000543 RID: 1347
		private ScrollView.NestedInteractionKind m_NestedInteractionKind;

		// Token: 0x04000547 RID: 1351
		private VisualElement m_ContentContainer;

		// Token: 0x04000548 RID: 1352
		private VisualElement m_ContentAndVerticalScrollContainer;

		// Token: 0x04000549 RID: 1353
		public static readonly string ussClassName = "unity-scroll-view";

		// Token: 0x0400054A RID: 1354
		public static readonly string viewportUssClassName = ScrollView.ussClassName + "__content-viewport";

		// Token: 0x0400054B RID: 1355
		public static readonly string contentAndVerticalScrollUssClassName = ScrollView.ussClassName + "__content-and-vertical-scroll-container";

		// Token: 0x0400054C RID: 1356
		public static readonly string contentUssClassName = ScrollView.ussClassName + "__content-container";

		// Token: 0x0400054D RID: 1357
		public static readonly string hScrollerUssClassName = ScrollView.ussClassName + "__horizontal-scroller";

		// Token: 0x0400054E RID: 1358
		public static readonly string vScrollerUssClassName = ScrollView.ussClassName + "__vertical-scroller";

		// Token: 0x0400054F RID: 1359
		public static readonly string horizontalVariantUssClassName = ScrollView.ussClassName + "--horizontal";

		// Token: 0x04000550 RID: 1360
		public static readonly string verticalVariantUssClassName = ScrollView.ussClassName + "--vertical";

		// Token: 0x04000551 RID: 1361
		public static readonly string verticalHorizontalVariantUssClassName = ScrollView.ussClassName + "--vertical-horizontal";

		// Token: 0x04000552 RID: 1362
		public static readonly string scrollVariantUssClassName = ScrollView.ussClassName + "--scroll";

		// Token: 0x04000553 RID: 1363
		private ScrollViewMode m_Mode;

		// Token: 0x04000554 RID: 1364
		private int m_ScrollingPointerId = PointerId.invalidPointerId;

		// Token: 0x04000555 RID: 1365
		private const float k_VelocityLerpTimeFactor = 10f;

		// Token: 0x04000556 RID: 1366
		internal const float ScrollThresholdSquared = 100f;

		// Token: 0x04000557 RID: 1367
		private Vector2 m_StartPosition;

		// Token: 0x04000558 RID: 1368
		private Vector2 m_PointerStartPosition;

		// Token: 0x04000559 RID: 1369
		private Vector2 m_Velocity;

		// Token: 0x0400055A RID: 1370
		private Vector2 m_SpringBackVelocity;

		// Token: 0x0400055B RID: 1371
		private Vector2 m_LowBounds;

		// Token: 0x0400055C RID: 1372
		private Vector2 m_HighBounds;

		// Token: 0x0400055D RID: 1373
		private float m_LastVelocityLerpTime;

		// Token: 0x0400055E RID: 1374
		private bool m_StartedMoving;

		// Token: 0x0400055F RID: 1375
		private bool m_TouchStoppedVelocity;

		// Token: 0x04000560 RID: 1376
		private VisualElement m_CapturedTarget;

		// Token: 0x04000561 RID: 1377
		private EventCallback<PointerMoveEvent> m_CapturedTargetPointerMoveCallback;

		// Token: 0x04000562 RID: 1378
		private EventCallback<PointerUpEvent> m_CapturedTargetPointerUpCallback;

		// Token: 0x04000563 RID: 1379
		private IVisualElementScheduledItem m_PostPointerUpAnimation;

		// Token: 0x0200016F RID: 367
		public new class UxmlFactory : UxmlFactory<ScrollView, ScrollView.UxmlTraits>
		{
		}

		// Token: 0x02000170 RID: 368
		public new class UxmlTraits : VisualElement.UxmlTraits
		{
			// Token: 0x06000BAE RID: 2990 RVA: 0x0002FD84 File Offset: 0x0002DF84
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				ScrollView scrollView = (ScrollView)ve;
				scrollView.mode = this.m_ScrollViewMode.GetValueFromBag(bag, cc);
				ScrollerVisibility scrollerVisibility = ScrollerVisibility.Auto;
				bool flag = this.m_HorizontalScrollerVisibility.TryGetValueFromBag(bag, cc, ref scrollerVisibility);
				if (flag)
				{
					scrollView.horizontalScrollerVisibility = scrollerVisibility;
				}
				else
				{
					scrollView.showHorizontal = this.m_ShowHorizontal.GetValueFromBag(bag, cc);
				}
				ScrollerVisibility scrollerVisibility2 = ScrollerVisibility.Auto;
				bool flag2 = this.m_VerticalScrollerVisibility.TryGetValueFromBag(bag, cc, ref scrollerVisibility2);
				if (flag2)
				{
					scrollView.verticalScrollerVisibility = scrollerVisibility2;
				}
				else
				{
					scrollView.showVertical = this.m_ShowVertical.GetValueFromBag(bag, cc);
				}
				scrollView.nestedInteractionKind = this.m_NestedInteractionKind.GetValueFromBag(bag, cc);
				scrollView.horizontalPageSize = this.m_HorizontalPageSize.GetValueFromBag(bag, cc);
				scrollView.verticalPageSize = this.m_VerticalPageSize.GetValueFromBag(bag, cc);
				scrollView.mouseWheelScrollSize = this.m_MouseWheelScrollSize.GetValueFromBag(bag, cc);
				scrollView.scrollDecelerationRate = this.m_ScrollDecelerationRate.GetValueFromBag(bag, cc);
				scrollView.touchScrollBehavior = this.m_TouchScrollBehavior.GetValueFromBag(bag, cc);
				scrollView.elasticity = this.m_Elasticity.GetValueFromBag(bag, cc);
			}

			// Token: 0x04000564 RID: 1380
			private UxmlEnumAttributeDescription<ScrollViewMode> m_ScrollViewMode = new UxmlEnumAttributeDescription<ScrollViewMode>
			{
				name = "mode",
				defaultValue = ScrollViewMode.Vertical
			};

			// Token: 0x04000565 RID: 1381
			private UxmlEnumAttributeDescription<ScrollView.NestedInteractionKind> m_NestedInteractionKind = new UxmlEnumAttributeDescription<ScrollView.NestedInteractionKind>
			{
				name = "nested-interaction-kind",
				defaultValue = ScrollView.NestedInteractionKind.Default
			};

			// Token: 0x04000566 RID: 1382
			private UxmlBoolAttributeDescription m_ShowHorizontal = new UxmlBoolAttributeDescription
			{
				name = "show-horizontal-scroller"
			};

			// Token: 0x04000567 RID: 1383
			private UxmlBoolAttributeDescription m_ShowVertical = new UxmlBoolAttributeDescription
			{
				name = "show-vertical-scroller"
			};

			// Token: 0x04000568 RID: 1384
			private UxmlEnumAttributeDescription<ScrollerVisibility> m_HorizontalScrollerVisibility = new UxmlEnumAttributeDescription<ScrollerVisibility>
			{
				name = "horizontal-scroller-visibility"
			};

			// Token: 0x04000569 RID: 1385
			private UxmlEnumAttributeDescription<ScrollerVisibility> m_VerticalScrollerVisibility = new UxmlEnumAttributeDescription<ScrollerVisibility>
			{
				name = "vertical-scroller-visibility"
			};

			// Token: 0x0400056A RID: 1386
			private UxmlFloatAttributeDescription m_HorizontalPageSize = new UxmlFloatAttributeDescription
			{
				name = "horizontal-page-size",
				defaultValue = -1f
			};

			// Token: 0x0400056B RID: 1387
			private UxmlFloatAttributeDescription m_VerticalPageSize = new UxmlFloatAttributeDescription
			{
				name = "vertical-page-size",
				defaultValue = -1f
			};

			// Token: 0x0400056C RID: 1388
			private UxmlFloatAttributeDescription m_MouseWheelScrollSize = new UxmlFloatAttributeDescription
			{
				name = "mouse-wheel-scroll-size",
				defaultValue = 18f
			};

			// Token: 0x0400056D RID: 1389
			private UxmlEnumAttributeDescription<ScrollView.TouchScrollBehavior> m_TouchScrollBehavior = new UxmlEnumAttributeDescription<ScrollView.TouchScrollBehavior>
			{
				name = "touch-scroll-type",
				defaultValue = ScrollView.TouchScrollBehavior.Clamped
			};

			// Token: 0x0400056E RID: 1390
			private UxmlFloatAttributeDescription m_ScrollDecelerationRate = new UxmlFloatAttributeDescription
			{
				name = "scroll-deceleration-rate",
				defaultValue = ScrollView.k_DefaultScrollDecelerationRate
			};

			// Token: 0x0400056F RID: 1391
			private UxmlFloatAttributeDescription m_Elasticity = new UxmlFloatAttributeDescription
			{
				name = "elasticity",
				defaultValue = ScrollView.k_DefaultElasticity
			};
		}

		// Token: 0x02000171 RID: 369
		public enum TouchScrollBehavior
		{
			// Token: 0x04000571 RID: 1393
			Unrestricted,
			// Token: 0x04000572 RID: 1394
			Elastic,
			// Token: 0x04000573 RID: 1395
			Clamped
		}

		// Token: 0x02000172 RID: 370
		public enum NestedInteractionKind
		{
			// Token: 0x04000575 RID: 1397
			Default,
			// Token: 0x04000576 RID: 1398
			StopScrolling,
			// Token: 0x04000577 RID: 1399
			ForwardScrolling
		}

		// Token: 0x02000173 RID: 371
		internal enum TouchScrollingResult
		{
			// Token: 0x04000579 RID: 1401
			Apply,
			// Token: 0x0400057A RID: 1402
			Forward,
			// Token: 0x0400057B RID: 1403
			Block
		}
	}
}
