using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x02000194 RID: 404
	public class TwoPaneSplitView : VisualElement
	{
		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x06000D08 RID: 3336 RVA: 0x000354D4 File Offset: 0x000336D4
		public VisualElement fixedPane
		{
			get
			{
				return this.m_FixedPane;
			}
		}

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x06000D09 RID: 3337 RVA: 0x000354DC File Offset: 0x000336DC
		public VisualElement flexedPane
		{
			get
			{
				return this.m_FlexedPane;
			}
		}

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x06000D0A RID: 3338 RVA: 0x000354E4 File Offset: 0x000336E4
		// (set) Token: 0x06000D0B RID: 3339 RVA: 0x000354EC File Offset: 0x000336EC
		public int fixedPaneIndex
		{
			get
			{
				return this.m_FixedPaneIndex;
			}
			set
			{
				bool flag = value == this.m_FixedPaneIndex;
				if (!flag)
				{
					this.Init(value, this.m_FixedPaneInitialDimension, this.m_Orientation);
				}
			}
		}

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x06000D0C RID: 3340 RVA: 0x0003551D File Offset: 0x0003371D
		// (set) Token: 0x06000D0D RID: 3341 RVA: 0x00035528 File Offset: 0x00033728
		public float fixedPaneInitialDimension
		{
			get
			{
				return this.m_FixedPaneInitialDimension;
			}
			set
			{
				bool flag = value == this.m_FixedPaneInitialDimension;
				if (!flag)
				{
					this.Init(this.m_FixedPaneIndex, value, this.m_Orientation);
				}
			}
		}

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x06000D0E RID: 3342 RVA: 0x00035559 File Offset: 0x00033759
		// (set) Token: 0x06000D0F RID: 3343 RVA: 0x00035564 File Offset: 0x00033764
		public TwoPaneSplitViewOrientation orientation
		{
			get
			{
				return this.m_Orientation;
			}
			set
			{
				bool flag = value == this.m_Orientation;
				if (!flag)
				{
					this.Init(this.m_FixedPaneIndex, this.m_FixedPaneInitialDimension, value);
				}
			}
		}

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x06000D10 RID: 3344 RVA: 0x00035595 File Offset: 0x00033795
		// (set) Token: 0x06000D11 RID: 3345 RVA: 0x000355B4 File Offset: 0x000337B4
		internal float fixedPaneDimension
		{
			get
			{
				return string.IsNullOrEmpty(base.viewDataKey) ? this.m_FixedPaneInitialDimension : this.m_FixedPaneDimension;
			}
			set
			{
				bool flag = value == this.m_FixedPaneDimension;
				if (!flag)
				{
					this.m_FixedPaneDimension = value;
					base.SaveViewData();
				}
			}
		}

		// Token: 0x06000D12 RID: 3346 RVA: 0x000355E0 File Offset: 0x000337E0
		public TwoPaneSplitView()
		{
			base.AddToClassList(TwoPaneSplitView.s_UssClassName);
			this.m_Content = new VisualElement();
			this.m_Content.name = "unity-content-container";
			this.m_Content.AddToClassList(TwoPaneSplitView.s_ContentContainerClassName);
			base.hierarchy.Add(this.m_Content);
			this.m_DragLineAnchor = new VisualElement();
			this.m_DragLineAnchor.name = "unity-dragline-anchor";
			this.m_DragLineAnchor.AddToClassList(TwoPaneSplitView.s_HandleDragLineAnchorClassName);
			base.hierarchy.Add(this.m_DragLineAnchor);
			this.m_DragLine = new VisualElement();
			this.m_DragLine.name = "unity-dragline";
			this.m_DragLine.AddToClassList(TwoPaneSplitView.s_HandleDragLineClassName);
			this.m_DragLineAnchor.Add(this.m_DragLine);
		}

		// Token: 0x06000D13 RID: 3347 RVA: 0x000356CF File Offset: 0x000338CF
		public TwoPaneSplitView(int fixedPaneIndex, float fixedPaneStartDimension, TwoPaneSplitViewOrientation orientation)
			: this()
		{
			this.Init(fixedPaneIndex, fixedPaneStartDimension, orientation);
		}

		// Token: 0x06000D14 RID: 3348 RVA: 0x000356E4 File Offset: 0x000338E4
		public void CollapseChild(int index)
		{
			bool flag = this.m_LeftPane == null;
			if (!flag)
			{
				this.m_DragLine.style.display = DisplayStyle.None;
				this.m_DragLineAnchor.style.display = DisplayStyle.None;
				bool flag2 = index == 0;
				if (flag2)
				{
					this.m_RightPane.style.width = StyleKeyword.Initial;
					this.m_RightPane.style.height = StyleKeyword.Initial;
					this.m_RightPane.style.flexGrow = 1f;
					this.m_LeftPane.style.display = DisplayStyle.None;
				}
				else
				{
					this.m_LeftPane.style.width = StyleKeyword.Initial;
					this.m_LeftPane.style.height = StyleKeyword.Initial;
					this.m_LeftPane.style.flexGrow = 1f;
					this.m_RightPane.style.display = DisplayStyle.None;
				}
				this.m_CollapseMode = true;
			}
		}

		// Token: 0x06000D15 RID: 3349 RVA: 0x00035808 File Offset: 0x00033A08
		public void UnCollapse()
		{
			bool flag = this.m_LeftPane == null;
			if (!flag)
			{
				this.m_LeftPane.style.display = DisplayStyle.Flex;
				this.m_RightPane.style.display = DisplayStyle.Flex;
				this.m_DragLine.style.display = DisplayStyle.Flex;
				this.m_DragLineAnchor.style.display = DisplayStyle.Flex;
				this.m_LeftPane.style.flexGrow = 0f;
				this.m_RightPane.style.flexGrow = 0f;
				this.m_CollapseMode = false;
				this.Init(this.m_FixedPaneIndex, this.m_FixedPaneInitialDimension, this.m_Orientation);
			}
		}

		// Token: 0x06000D16 RID: 3350 RVA: 0x000358DC File Offset: 0x00033ADC
		internal void Init(int fixedPaneIndex, float fixedPaneInitialDimension, TwoPaneSplitViewOrientation orientation)
		{
			this.m_Orientation = orientation;
			this.m_FixedPaneIndex = fixedPaneIndex;
			this.m_FixedPaneInitialDimension = fixedPaneInitialDimension;
			this.m_Content.RemoveFromClassList(TwoPaneSplitView.s_HorizontalClassName);
			this.m_Content.RemoveFromClassList(TwoPaneSplitView.s_VerticalClassName);
			bool flag = this.m_Orientation == TwoPaneSplitViewOrientation.Horizontal;
			if (flag)
			{
				this.m_Content.AddToClassList(TwoPaneSplitView.s_HorizontalClassName);
			}
			else
			{
				this.m_Content.AddToClassList(TwoPaneSplitView.s_VerticalClassName);
			}
			this.m_DragLineAnchor.RemoveFromClassList(TwoPaneSplitView.s_HandleDragLineAnchorHorizontalClassName);
			this.m_DragLineAnchor.RemoveFromClassList(TwoPaneSplitView.s_HandleDragLineAnchorVerticalClassName);
			bool flag2 = this.m_Orientation == TwoPaneSplitViewOrientation.Horizontal;
			if (flag2)
			{
				this.m_DragLineAnchor.AddToClassList(TwoPaneSplitView.s_HandleDragLineAnchorHorizontalClassName);
			}
			else
			{
				this.m_DragLineAnchor.AddToClassList(TwoPaneSplitView.s_HandleDragLineAnchorVerticalClassName);
			}
			this.m_DragLine.RemoveFromClassList(TwoPaneSplitView.s_HandleDragLineHorizontalClassName);
			this.m_DragLine.RemoveFromClassList(TwoPaneSplitView.s_HandleDragLineVerticalClassName);
			bool flag3 = this.m_Orientation == TwoPaneSplitViewOrientation.Horizontal;
			if (flag3)
			{
				this.m_DragLine.AddToClassList(TwoPaneSplitView.s_HandleDragLineHorizontalClassName);
			}
			else
			{
				this.m_DragLine.AddToClassList(TwoPaneSplitView.s_HandleDragLineVerticalClassName);
			}
			bool flag4 = this.m_Resizer != null;
			if (flag4)
			{
				this.m_DragLineAnchor.RemoveManipulator(this.m_Resizer);
				this.m_Resizer = null;
			}
			bool flag5 = this.m_Content.childCount != 2;
			if (flag5)
			{
				base.RegisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.OnPostDisplaySetup), TrickleDown.NoTrickleDown);
			}
			else
			{
				this.PostDisplaySetup();
			}
		}

		// Token: 0x06000D17 RID: 3351 RVA: 0x00035A54 File Offset: 0x00033C54
		private void OnPostDisplaySetup(GeometryChangedEvent evt)
		{
			bool flag = this.m_Content.childCount != 2;
			if (flag)
			{
				Debug.LogError("TwoPaneSplitView needs exactly 2 children.");
			}
			else
			{
				this.PostDisplaySetup();
				base.UnregisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.OnPostDisplaySetup), TrickleDown.NoTrickleDown);
			}
		}

		// Token: 0x06000D18 RID: 3352 RVA: 0x00035AA0 File Offset: 0x00033CA0
		private void PostDisplaySetup()
		{
			bool flag = this.m_Content.childCount != 2;
			if (flag)
			{
				Debug.LogError("TwoPaneSplitView needs exactly 2 children.");
			}
			else
			{
				bool flag2 = this.fixedPaneDimension < 0f;
				if (flag2)
				{
					this.fixedPaneDimension = this.m_FixedPaneInitialDimension;
				}
				float fixedPaneDimension = this.fixedPaneDimension;
				this.m_LeftPane = this.m_Content[0];
				bool flag3 = this.m_FixedPaneIndex == 0;
				if (flag3)
				{
					this.m_FixedPane = this.m_LeftPane;
				}
				else
				{
					this.m_FlexedPane = this.m_LeftPane;
				}
				this.m_RightPane = this.m_Content[1];
				bool flag4 = this.m_FixedPaneIndex == 1;
				if (flag4)
				{
					this.m_FixedPane = this.m_RightPane;
				}
				else
				{
					this.m_FlexedPane = this.m_RightPane;
				}
				this.m_FixedPane.style.flexBasis = StyleKeyword.Null;
				this.m_FixedPane.style.flexShrink = StyleKeyword.Null;
				this.m_FixedPane.style.flexGrow = StyleKeyword.Null;
				this.m_FlexedPane.style.flexGrow = StyleKeyword.Null;
				this.m_FlexedPane.style.flexShrink = StyleKeyword.Null;
				this.m_FlexedPane.style.flexBasis = StyleKeyword.Null;
				this.m_FixedPane.style.width = StyleKeyword.Null;
				this.m_FixedPane.style.height = StyleKeyword.Null;
				this.m_FlexedPane.style.width = StyleKeyword.Null;
				this.m_FlexedPane.style.height = StyleKeyword.Null;
				bool flag5 = this.m_Orientation == TwoPaneSplitViewOrientation.Horizontal;
				if (flag5)
				{
					this.m_FixedPane.style.width = fixedPaneDimension;
					this.m_FixedPane.style.height = StyleKeyword.Null;
				}
				else
				{
					this.m_FixedPane.style.width = StyleKeyword.Null;
					this.m_FixedPane.style.height = fixedPaneDimension;
				}
				this.m_FixedPane.style.flexShrink = 0f;
				this.m_FixedPane.style.flexGrow = 0f;
				this.m_FlexedPane.style.flexGrow = 1f;
				this.m_FlexedPane.style.flexShrink = 0f;
				this.m_FlexedPane.style.flexBasis = 0f;
				bool flag6 = this.m_Orientation == TwoPaneSplitViewOrientation.Horizontal;
				if (flag6)
				{
					bool flag7 = this.m_FixedPaneIndex == 0;
					if (flag7)
					{
						this.m_DragLineAnchor.style.left = fixedPaneDimension;
					}
					else
					{
						this.m_DragLineAnchor.style.left = base.resolvedStyle.width - fixedPaneDimension;
					}
				}
				else
				{
					bool flag8 = this.m_FixedPaneIndex == 0;
					if (flag8)
					{
						this.m_DragLineAnchor.style.top = fixedPaneDimension;
					}
					else
					{
						this.m_DragLineAnchor.style.top = base.resolvedStyle.height - fixedPaneDimension;
					}
				}
				bool flag9 = this.m_FixedPaneIndex == 0;
				int num;
				if (flag9)
				{
					num = 1;
				}
				else
				{
					num = -1;
				}
				bool flag10 = this.m_FixedPaneIndex == 0;
				if (flag10)
				{
					this.m_Resizer = new TwoPaneSplitViewResizer(this, num, this.m_Orientation);
				}
				else
				{
					this.m_Resizer = new TwoPaneSplitViewResizer(this, num, this.m_Orientation);
				}
				this.m_DragLineAnchor.AddManipulator(this.m_Resizer);
				base.RegisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.OnSizeChange), TrickleDown.NoTrickleDown);
			}
		}

		// Token: 0x06000D19 RID: 3353 RVA: 0x00035E6C File Offset: 0x0003406C
		private void OnSizeChange(GeometryChangedEvent evt)
		{
			this.OnSizeChange();
		}

		// Token: 0x06000D1A RID: 3354 RVA: 0x00035E78 File Offset: 0x00034078
		private void OnSizeChange()
		{
			bool collapseMode = this.m_CollapseMode;
			if (!collapseMode)
			{
				bool flag = base.resolvedStyle.display == DisplayStyle.None || base.resolvedStyle.visibility == Visibility.Hidden;
				if (!flag)
				{
					float num = base.resolvedStyle.width;
					float num2 = this.m_FixedPane.resolvedStyle.width;
					float num3 = this.m_FixedPane.resolvedStyle.minWidth.value;
					float num4 = this.m_FlexedPane.resolvedStyle.minWidth.value;
					bool flag2 = this.m_Orientation == TwoPaneSplitViewOrientation.Vertical;
					if (flag2)
					{
						num = base.resolvedStyle.height;
						num2 = this.m_FixedPane.resolvedStyle.height;
						num3 = this.m_FixedPane.resolvedStyle.minHeight.value;
						num4 = this.m_FlexedPane.resolvedStyle.minHeight.value;
					}
					bool flag3 = num >= num2 + num4;
					if (flag3)
					{
						this.SetDragLineOffset((this.m_FixedPaneIndex == 0) ? num2 : (num - num2));
					}
					else
					{
						bool flag4 = num >= num3 + num4;
						if (flag4)
						{
							float num5 = num - num4;
							this.SetFixedPaneDimension(num5);
							this.SetDragLineOffset((this.m_FixedPaneIndex == 0) ? num5 : num4);
						}
						else
						{
							this.SetFixedPaneDimension(num3);
							this.SetDragLineOffset((this.m_FixedPaneIndex == 0) ? num3 : num4);
						}
					}
				}
			}
		}

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x06000D1B RID: 3355 RVA: 0x00035FF0 File Offset: 0x000341F0
		public override VisualElement contentContainer
		{
			get
			{
				return this.m_Content;
			}
		}

		// Token: 0x06000D1C RID: 3356 RVA: 0x00036008 File Offset: 0x00034208
		internal override void OnViewDataReady()
		{
			base.OnViewDataReady();
			string fullHierarchicalViewDataKey = base.GetFullHierarchicalViewDataKey();
			base.OverwriteFromViewData(this, fullHierarchicalViewDataKey);
			this.PostDisplaySetup();
		}

		// Token: 0x06000D1D RID: 3357 RVA: 0x00036034 File Offset: 0x00034234
		private void SetDragLineOffset(float offset)
		{
			bool flag = this.m_Orientation == TwoPaneSplitViewOrientation.Horizontal;
			if (flag)
			{
				this.m_DragLineAnchor.style.left = offset;
			}
			else
			{
				this.m_DragLineAnchor.style.top = offset;
			}
		}

		// Token: 0x06000D1E RID: 3358 RVA: 0x00036080 File Offset: 0x00034280
		private void SetFixedPaneDimension(float dimension)
		{
			bool flag = this.m_Orientation == TwoPaneSplitViewOrientation.Horizontal;
			if (flag)
			{
				this.m_FixedPane.style.width = dimension;
			}
			else
			{
				this.m_FixedPane.style.height = dimension;
			}
		}

		// Token: 0x040005F8 RID: 1528
		private static readonly string s_UssClassName = "unity-two-pane-split-view";

		// Token: 0x040005F9 RID: 1529
		private static readonly string s_ContentContainerClassName = "unity-two-pane-split-view__content-container";

		// Token: 0x040005FA RID: 1530
		private static readonly string s_HandleDragLineClassName = "unity-two-pane-split-view__dragline";

		// Token: 0x040005FB RID: 1531
		private static readonly string s_HandleDragLineVerticalClassName = TwoPaneSplitView.s_HandleDragLineClassName + "--vertical";

		// Token: 0x040005FC RID: 1532
		private static readonly string s_HandleDragLineHorizontalClassName = TwoPaneSplitView.s_HandleDragLineClassName + "--horizontal";

		// Token: 0x040005FD RID: 1533
		private static readonly string s_HandleDragLineAnchorClassName = "unity-two-pane-split-view__dragline-anchor";

		// Token: 0x040005FE RID: 1534
		private static readonly string s_HandleDragLineAnchorVerticalClassName = TwoPaneSplitView.s_HandleDragLineAnchorClassName + "--vertical";

		// Token: 0x040005FF RID: 1535
		private static readonly string s_HandleDragLineAnchorHorizontalClassName = TwoPaneSplitView.s_HandleDragLineAnchorClassName + "--horizontal";

		// Token: 0x04000600 RID: 1536
		private static readonly string s_VerticalClassName = "unity-two-pane-split-view--vertical";

		// Token: 0x04000601 RID: 1537
		private static readonly string s_HorizontalClassName = "unity-two-pane-split-view--horizontal";

		// Token: 0x04000602 RID: 1538
		private VisualElement m_LeftPane;

		// Token: 0x04000603 RID: 1539
		private VisualElement m_RightPane;

		// Token: 0x04000604 RID: 1540
		private VisualElement m_FixedPane;

		// Token: 0x04000605 RID: 1541
		private VisualElement m_FlexedPane;

		// Token: 0x04000606 RID: 1542
		[SerializeField]
		private float m_FixedPaneDimension = -1f;

		// Token: 0x04000607 RID: 1543
		private VisualElement m_DragLine;

		// Token: 0x04000608 RID: 1544
		private VisualElement m_DragLineAnchor;

		// Token: 0x04000609 RID: 1545
		private bool m_CollapseMode;

		// Token: 0x0400060A RID: 1546
		private VisualElement m_Content;

		// Token: 0x0400060B RID: 1547
		private TwoPaneSplitViewOrientation m_Orientation;

		// Token: 0x0400060C RID: 1548
		private int m_FixedPaneIndex;

		// Token: 0x0400060D RID: 1549
		private float m_FixedPaneInitialDimension;

		// Token: 0x0400060E RID: 1550
		internal TwoPaneSplitViewResizer m_Resizer;

		// Token: 0x02000195 RID: 405
		public new class UxmlFactory : UxmlFactory<TwoPaneSplitView, TwoPaneSplitView.UxmlTraits>
		{
		}

		// Token: 0x02000196 RID: 406
		public new class UxmlTraits : VisualElement.UxmlTraits
		{
			// Token: 0x170002B0 RID: 688
			// (get) Token: 0x06000D21 RID: 3361 RVA: 0x00036170 File Offset: 0x00034370
			public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription
			{
				get
				{
					yield break;
				}
			}

			// Token: 0x06000D22 RID: 3362 RVA: 0x00036190 File Offset: 0x00034390
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				int valueFromBag = this.m_FixedPaneIndex.GetValueFromBag(bag, cc);
				int valueFromBag2 = this.m_FixedPaneInitialDimension.GetValueFromBag(bag, cc);
				TwoPaneSplitViewOrientation valueFromBag3 = this.m_Orientation.GetValueFromBag(bag, cc);
				((TwoPaneSplitView)ve).Init(valueFromBag, (float)valueFromBag2, valueFromBag3);
			}

			// Token: 0x0400060F RID: 1551
			private UxmlIntAttributeDescription m_FixedPaneIndex = new UxmlIntAttributeDescription
			{
				name = "fixed-pane-index",
				defaultValue = 0
			};

			// Token: 0x04000610 RID: 1552
			private UxmlIntAttributeDescription m_FixedPaneInitialDimension = new UxmlIntAttributeDescription
			{
				name = "fixed-pane-initial-dimension",
				defaultValue = 100
			};

			// Token: 0x04000611 RID: 1553
			private UxmlEnumAttributeDescription<TwoPaneSplitViewOrientation> m_Orientation = new UxmlEnumAttributeDescription<TwoPaneSplitViewOrientation>
			{
				name = "orientation",
				defaultValue = TwoPaneSplitViewOrientation.Horizontal
			};
		}
	}
}
