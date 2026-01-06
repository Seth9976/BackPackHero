using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements.StyleSheets
{
	// Token: 0x02000350 RID: 848
	internal static class InitialStyle
	{
		// Token: 0x06001B14 RID: 6932 RVA: 0x0007C2E4 File Offset: 0x0007A4E4
		public static ref ComputedStyle Get()
		{
			return ref InitialStyle.s_InitialStyle;
		}

		// Token: 0x06001B15 RID: 6933 RVA: 0x0007C2FC File Offset: 0x0007A4FC
		public static ComputedStyle Acquire()
		{
			return InitialStyle.s_InitialStyle.Acquire();
		}

		// Token: 0x06001B16 RID: 6934 RVA: 0x0007C318 File Offset: 0x0007A518
		static InitialStyle()
		{
			InitialStyle.s_InitialStyle.layoutData.Write().alignContent = Align.FlexStart;
			InitialStyle.s_InitialStyle.layoutData.Write().alignItems = Align.Stretch;
			InitialStyle.s_InitialStyle.layoutData.Write().alignSelf = Align.Auto;
			InitialStyle.s_InitialStyle.visualData.Write().backgroundColor = Color.clear;
			InitialStyle.s_InitialStyle.visualData.Write().backgroundImage = default(Background);
			InitialStyle.s_InitialStyle.visualData.Write().borderBottomColor = Color.clear;
			InitialStyle.s_InitialStyle.visualData.Write().borderBottomLeftRadius = 0f;
			InitialStyle.s_InitialStyle.visualData.Write().borderBottomRightRadius = 0f;
			InitialStyle.s_InitialStyle.layoutData.Write().borderBottomWidth = 0f;
			InitialStyle.s_InitialStyle.visualData.Write().borderLeftColor = Color.clear;
			InitialStyle.s_InitialStyle.layoutData.Write().borderLeftWidth = 0f;
			InitialStyle.s_InitialStyle.visualData.Write().borderRightColor = Color.clear;
			InitialStyle.s_InitialStyle.layoutData.Write().borderRightWidth = 0f;
			InitialStyle.s_InitialStyle.visualData.Write().borderTopColor = Color.clear;
			InitialStyle.s_InitialStyle.visualData.Write().borderTopLeftRadius = 0f;
			InitialStyle.s_InitialStyle.visualData.Write().borderTopRightRadius = 0f;
			InitialStyle.s_InitialStyle.layoutData.Write().borderTopWidth = 0f;
			InitialStyle.s_InitialStyle.layoutData.Write().bottom = StyleKeyword.Auto.ToLength();
			InitialStyle.s_InitialStyle.inheritedData.Write().color = Color.black;
			InitialStyle.s_InitialStyle.rareData.Write().cursor = default(Cursor);
			InitialStyle.s_InitialStyle.layoutData.Write().display = DisplayStyle.Flex;
			InitialStyle.s_InitialStyle.layoutData.Write().flexBasis = StyleKeyword.Auto.ToLength();
			InitialStyle.s_InitialStyle.layoutData.Write().flexDirection = FlexDirection.Column;
			InitialStyle.s_InitialStyle.layoutData.Write().flexGrow = 0f;
			InitialStyle.s_InitialStyle.layoutData.Write().flexShrink = 1f;
			InitialStyle.s_InitialStyle.layoutData.Write().flexWrap = Wrap.NoWrap;
			InitialStyle.s_InitialStyle.inheritedData.Write().fontSize = 0f;
			InitialStyle.s_InitialStyle.layoutData.Write().height = StyleKeyword.Auto.ToLength();
			InitialStyle.s_InitialStyle.layoutData.Write().justifyContent = Justify.FlexStart;
			InitialStyle.s_InitialStyle.layoutData.Write().left = StyleKeyword.Auto.ToLength();
			InitialStyle.s_InitialStyle.inheritedData.Write().letterSpacing = 0f;
			InitialStyle.s_InitialStyle.layoutData.Write().marginBottom = 0f;
			InitialStyle.s_InitialStyle.layoutData.Write().marginLeft = 0f;
			InitialStyle.s_InitialStyle.layoutData.Write().marginRight = 0f;
			InitialStyle.s_InitialStyle.layoutData.Write().marginTop = 0f;
			InitialStyle.s_InitialStyle.layoutData.Write().maxHeight = StyleKeyword.None.ToLength();
			InitialStyle.s_InitialStyle.layoutData.Write().maxWidth = StyleKeyword.None.ToLength();
			InitialStyle.s_InitialStyle.layoutData.Write().minHeight = StyleKeyword.Auto.ToLength();
			InitialStyle.s_InitialStyle.layoutData.Write().minWidth = StyleKeyword.Auto.ToLength();
			InitialStyle.s_InitialStyle.visualData.Write().opacity = 1f;
			InitialStyle.s_InitialStyle.visualData.Write().overflow = OverflowInternal.Visible;
			InitialStyle.s_InitialStyle.layoutData.Write().paddingBottom = 0f;
			InitialStyle.s_InitialStyle.layoutData.Write().paddingLeft = 0f;
			InitialStyle.s_InitialStyle.layoutData.Write().paddingRight = 0f;
			InitialStyle.s_InitialStyle.layoutData.Write().paddingTop = 0f;
			InitialStyle.s_InitialStyle.layoutData.Write().position = Position.Relative;
			InitialStyle.s_InitialStyle.layoutData.Write().right = StyleKeyword.Auto.ToLength();
			InitialStyle.s_InitialStyle.transformData.Write().rotate = StyleKeyword.None.ToRotate();
			InitialStyle.s_InitialStyle.transformData.Write().scale = StyleKeyword.None.ToScale();
			InitialStyle.s_InitialStyle.rareData.Write().textOverflow = TextOverflow.Clip;
			InitialStyle.s_InitialStyle.inheritedData.Write().textShadow = default(TextShadow);
			InitialStyle.s_InitialStyle.layoutData.Write().top = StyleKeyword.Auto.ToLength();
			InitialStyle.s_InitialStyle.transformData.Write().transformOrigin = TransformOrigin.Initial();
			ref TransitionData ptr = ref InitialStyle.s_InitialStyle.transitionData.Write();
			List<TimeValue> list = new List<TimeValue>();
			list.Add(0f);
			ptr.transitionDelay = list;
			ref TransitionData ptr2 = ref InitialStyle.s_InitialStyle.transitionData.Write();
			List<TimeValue> list2 = new List<TimeValue>();
			list2.Add(0f);
			ptr2.transitionDuration = list2;
			ref TransitionData ptr3 = ref InitialStyle.s_InitialStyle.transitionData.Write();
			List<StylePropertyName> list3 = new List<StylePropertyName>();
			list3.Add("all");
			ptr3.transitionProperty = list3;
			ref TransitionData ptr4 = ref InitialStyle.s_InitialStyle.transitionData.Write();
			List<EasingFunction> list4 = new List<EasingFunction>();
			list4.Add(EasingMode.Ease);
			ptr4.transitionTimingFunction = list4;
			InitialStyle.s_InitialStyle.transformData.Write().translate = StyleKeyword.None.ToTranslate();
			InitialStyle.s_InitialStyle.rareData.Write().unityBackgroundImageTintColor = Color.white;
			InitialStyle.s_InitialStyle.rareData.Write().unityBackgroundScaleMode = ScaleMode.StretchToFill;
			InitialStyle.s_InitialStyle.inheritedData.Write().unityFont = null;
			InitialStyle.s_InitialStyle.inheritedData.Write().unityFontDefinition = default(FontDefinition);
			InitialStyle.s_InitialStyle.inheritedData.Write().unityFontStyleAndWeight = FontStyle.Normal;
			InitialStyle.s_InitialStyle.rareData.Write().unityOverflowClipBox = OverflowClipBox.PaddingBox;
			InitialStyle.s_InitialStyle.inheritedData.Write().unityParagraphSpacing = 0f;
			InitialStyle.s_InitialStyle.rareData.Write().unitySliceBottom = 0;
			InitialStyle.s_InitialStyle.rareData.Write().unitySliceLeft = 0;
			InitialStyle.s_InitialStyle.rareData.Write().unitySliceRight = 0;
			InitialStyle.s_InitialStyle.rareData.Write().unitySliceTop = 0;
			InitialStyle.s_InitialStyle.inheritedData.Write().unityTextAlign = TextAnchor.UpperLeft;
			InitialStyle.s_InitialStyle.inheritedData.Write().unityTextOutlineColor = Color.clear;
			InitialStyle.s_InitialStyle.inheritedData.Write().unityTextOutlineWidth = 0f;
			InitialStyle.s_InitialStyle.rareData.Write().unityTextOverflowPosition = TextOverflowPosition.End;
			InitialStyle.s_InitialStyle.inheritedData.Write().visibility = Visibility.Visible;
			InitialStyle.s_InitialStyle.inheritedData.Write().whiteSpace = WhiteSpace.Normal;
			InitialStyle.s_InitialStyle.layoutData.Write().width = StyleKeyword.Auto.ToLength();
			InitialStyle.s_InitialStyle.inheritedData.Write().wordSpacing = 0f;
		}

		// Token: 0x17000661 RID: 1633
		// (get) Token: 0x06001B17 RID: 6935 RVA: 0x0007CAFF File Offset: 0x0007ACFF
		public static Align alignContent
		{
			get
			{
				return InitialStyle.s_InitialStyle.layoutData.Read().alignContent;
			}
		}

		// Token: 0x17000662 RID: 1634
		// (get) Token: 0x06001B18 RID: 6936 RVA: 0x0007CB15 File Offset: 0x0007AD15
		public static Align alignItems
		{
			get
			{
				return InitialStyle.s_InitialStyle.layoutData.Read().alignItems;
			}
		}

		// Token: 0x17000663 RID: 1635
		// (get) Token: 0x06001B19 RID: 6937 RVA: 0x0007CB2B File Offset: 0x0007AD2B
		public static Align alignSelf
		{
			get
			{
				return InitialStyle.s_InitialStyle.layoutData.Read().alignSelf;
			}
		}

		// Token: 0x17000664 RID: 1636
		// (get) Token: 0x06001B1A RID: 6938 RVA: 0x0007CB41 File Offset: 0x0007AD41
		public static Color backgroundColor
		{
			get
			{
				return InitialStyle.s_InitialStyle.visualData.Read().backgroundColor;
			}
		}

		// Token: 0x17000665 RID: 1637
		// (get) Token: 0x06001B1B RID: 6939 RVA: 0x0007CB57 File Offset: 0x0007AD57
		public static Background backgroundImage
		{
			get
			{
				return InitialStyle.s_InitialStyle.visualData.Read().backgroundImage;
			}
		}

		// Token: 0x17000666 RID: 1638
		// (get) Token: 0x06001B1C RID: 6940 RVA: 0x0007CB6D File Offset: 0x0007AD6D
		public static Color borderBottomColor
		{
			get
			{
				return InitialStyle.s_InitialStyle.visualData.Read().borderBottomColor;
			}
		}

		// Token: 0x17000667 RID: 1639
		// (get) Token: 0x06001B1D RID: 6941 RVA: 0x0007CB83 File Offset: 0x0007AD83
		public static Length borderBottomLeftRadius
		{
			get
			{
				return InitialStyle.s_InitialStyle.visualData.Read().borderBottomLeftRadius;
			}
		}

		// Token: 0x17000668 RID: 1640
		// (get) Token: 0x06001B1E RID: 6942 RVA: 0x0007CB99 File Offset: 0x0007AD99
		public static Length borderBottomRightRadius
		{
			get
			{
				return InitialStyle.s_InitialStyle.visualData.Read().borderBottomRightRadius;
			}
		}

		// Token: 0x17000669 RID: 1641
		// (get) Token: 0x06001B1F RID: 6943 RVA: 0x0007CBAF File Offset: 0x0007ADAF
		public static float borderBottomWidth
		{
			get
			{
				return InitialStyle.s_InitialStyle.layoutData.Read().borderBottomWidth;
			}
		}

		// Token: 0x1700066A RID: 1642
		// (get) Token: 0x06001B20 RID: 6944 RVA: 0x0007CBC5 File Offset: 0x0007ADC5
		public static Color borderLeftColor
		{
			get
			{
				return InitialStyle.s_InitialStyle.visualData.Read().borderLeftColor;
			}
		}

		// Token: 0x1700066B RID: 1643
		// (get) Token: 0x06001B21 RID: 6945 RVA: 0x0007CBDB File Offset: 0x0007ADDB
		public static float borderLeftWidth
		{
			get
			{
				return InitialStyle.s_InitialStyle.layoutData.Read().borderLeftWidth;
			}
		}

		// Token: 0x1700066C RID: 1644
		// (get) Token: 0x06001B22 RID: 6946 RVA: 0x0007CBF1 File Offset: 0x0007ADF1
		public static Color borderRightColor
		{
			get
			{
				return InitialStyle.s_InitialStyle.visualData.Read().borderRightColor;
			}
		}

		// Token: 0x1700066D RID: 1645
		// (get) Token: 0x06001B23 RID: 6947 RVA: 0x0007CC07 File Offset: 0x0007AE07
		public static float borderRightWidth
		{
			get
			{
				return InitialStyle.s_InitialStyle.layoutData.Read().borderRightWidth;
			}
		}

		// Token: 0x1700066E RID: 1646
		// (get) Token: 0x06001B24 RID: 6948 RVA: 0x0007CC1D File Offset: 0x0007AE1D
		public static Color borderTopColor
		{
			get
			{
				return InitialStyle.s_InitialStyle.visualData.Read().borderTopColor;
			}
		}

		// Token: 0x1700066F RID: 1647
		// (get) Token: 0x06001B25 RID: 6949 RVA: 0x0007CC33 File Offset: 0x0007AE33
		public static Length borderTopLeftRadius
		{
			get
			{
				return InitialStyle.s_InitialStyle.visualData.Read().borderTopLeftRadius;
			}
		}

		// Token: 0x17000670 RID: 1648
		// (get) Token: 0x06001B26 RID: 6950 RVA: 0x0007CC49 File Offset: 0x0007AE49
		public static Length borderTopRightRadius
		{
			get
			{
				return InitialStyle.s_InitialStyle.visualData.Read().borderTopRightRadius;
			}
		}

		// Token: 0x17000671 RID: 1649
		// (get) Token: 0x06001B27 RID: 6951 RVA: 0x0007CC5F File Offset: 0x0007AE5F
		public static float borderTopWidth
		{
			get
			{
				return InitialStyle.s_InitialStyle.layoutData.Read().borderTopWidth;
			}
		}

		// Token: 0x17000672 RID: 1650
		// (get) Token: 0x06001B28 RID: 6952 RVA: 0x0007CC75 File Offset: 0x0007AE75
		public static Length bottom
		{
			get
			{
				return InitialStyle.s_InitialStyle.layoutData.Read().bottom;
			}
		}

		// Token: 0x17000673 RID: 1651
		// (get) Token: 0x06001B29 RID: 6953 RVA: 0x0007CC8B File Offset: 0x0007AE8B
		public static Color color
		{
			get
			{
				return InitialStyle.s_InitialStyle.inheritedData.Read().color;
			}
		}

		// Token: 0x17000674 RID: 1652
		// (get) Token: 0x06001B2A RID: 6954 RVA: 0x0007CCA1 File Offset: 0x0007AEA1
		public static Cursor cursor
		{
			get
			{
				return InitialStyle.s_InitialStyle.rareData.Read().cursor;
			}
		}

		// Token: 0x17000675 RID: 1653
		// (get) Token: 0x06001B2B RID: 6955 RVA: 0x0007CCB7 File Offset: 0x0007AEB7
		public static DisplayStyle display
		{
			get
			{
				return InitialStyle.s_InitialStyle.layoutData.Read().display;
			}
		}

		// Token: 0x17000676 RID: 1654
		// (get) Token: 0x06001B2C RID: 6956 RVA: 0x0007CCCD File Offset: 0x0007AECD
		public static Length flexBasis
		{
			get
			{
				return InitialStyle.s_InitialStyle.layoutData.Read().flexBasis;
			}
		}

		// Token: 0x17000677 RID: 1655
		// (get) Token: 0x06001B2D RID: 6957 RVA: 0x0007CCE3 File Offset: 0x0007AEE3
		public static FlexDirection flexDirection
		{
			get
			{
				return InitialStyle.s_InitialStyle.layoutData.Read().flexDirection;
			}
		}

		// Token: 0x17000678 RID: 1656
		// (get) Token: 0x06001B2E RID: 6958 RVA: 0x0007CCF9 File Offset: 0x0007AEF9
		public static float flexGrow
		{
			get
			{
				return InitialStyle.s_InitialStyle.layoutData.Read().flexGrow;
			}
		}

		// Token: 0x17000679 RID: 1657
		// (get) Token: 0x06001B2F RID: 6959 RVA: 0x0007CD0F File Offset: 0x0007AF0F
		public static float flexShrink
		{
			get
			{
				return InitialStyle.s_InitialStyle.layoutData.Read().flexShrink;
			}
		}

		// Token: 0x1700067A RID: 1658
		// (get) Token: 0x06001B30 RID: 6960 RVA: 0x0007CD25 File Offset: 0x0007AF25
		public static Wrap flexWrap
		{
			get
			{
				return InitialStyle.s_InitialStyle.layoutData.Read().flexWrap;
			}
		}

		// Token: 0x1700067B RID: 1659
		// (get) Token: 0x06001B31 RID: 6961 RVA: 0x0007CD3B File Offset: 0x0007AF3B
		public static Length fontSize
		{
			get
			{
				return InitialStyle.s_InitialStyle.inheritedData.Read().fontSize;
			}
		}

		// Token: 0x1700067C RID: 1660
		// (get) Token: 0x06001B32 RID: 6962 RVA: 0x0007CD51 File Offset: 0x0007AF51
		public static Length height
		{
			get
			{
				return InitialStyle.s_InitialStyle.layoutData.Read().height;
			}
		}

		// Token: 0x1700067D RID: 1661
		// (get) Token: 0x06001B33 RID: 6963 RVA: 0x0007CD67 File Offset: 0x0007AF67
		public static Justify justifyContent
		{
			get
			{
				return InitialStyle.s_InitialStyle.layoutData.Read().justifyContent;
			}
		}

		// Token: 0x1700067E RID: 1662
		// (get) Token: 0x06001B34 RID: 6964 RVA: 0x0007CD7D File Offset: 0x0007AF7D
		public static Length left
		{
			get
			{
				return InitialStyle.s_InitialStyle.layoutData.Read().left;
			}
		}

		// Token: 0x1700067F RID: 1663
		// (get) Token: 0x06001B35 RID: 6965 RVA: 0x0007CD93 File Offset: 0x0007AF93
		public static Length letterSpacing
		{
			get
			{
				return InitialStyle.s_InitialStyle.inheritedData.Read().letterSpacing;
			}
		}

		// Token: 0x17000680 RID: 1664
		// (get) Token: 0x06001B36 RID: 6966 RVA: 0x0007CDA9 File Offset: 0x0007AFA9
		public static Length marginBottom
		{
			get
			{
				return InitialStyle.s_InitialStyle.layoutData.Read().marginBottom;
			}
		}

		// Token: 0x17000681 RID: 1665
		// (get) Token: 0x06001B37 RID: 6967 RVA: 0x0007CDBF File Offset: 0x0007AFBF
		public static Length marginLeft
		{
			get
			{
				return InitialStyle.s_InitialStyle.layoutData.Read().marginLeft;
			}
		}

		// Token: 0x17000682 RID: 1666
		// (get) Token: 0x06001B38 RID: 6968 RVA: 0x0007CDD5 File Offset: 0x0007AFD5
		public static Length marginRight
		{
			get
			{
				return InitialStyle.s_InitialStyle.layoutData.Read().marginRight;
			}
		}

		// Token: 0x17000683 RID: 1667
		// (get) Token: 0x06001B39 RID: 6969 RVA: 0x0007CDEB File Offset: 0x0007AFEB
		public static Length marginTop
		{
			get
			{
				return InitialStyle.s_InitialStyle.layoutData.Read().marginTop;
			}
		}

		// Token: 0x17000684 RID: 1668
		// (get) Token: 0x06001B3A RID: 6970 RVA: 0x0007CE01 File Offset: 0x0007B001
		public static Length maxHeight
		{
			get
			{
				return InitialStyle.s_InitialStyle.layoutData.Read().maxHeight;
			}
		}

		// Token: 0x17000685 RID: 1669
		// (get) Token: 0x06001B3B RID: 6971 RVA: 0x0007CE17 File Offset: 0x0007B017
		public static Length maxWidth
		{
			get
			{
				return InitialStyle.s_InitialStyle.layoutData.Read().maxWidth;
			}
		}

		// Token: 0x17000686 RID: 1670
		// (get) Token: 0x06001B3C RID: 6972 RVA: 0x0007CE2D File Offset: 0x0007B02D
		public static Length minHeight
		{
			get
			{
				return InitialStyle.s_InitialStyle.layoutData.Read().minHeight;
			}
		}

		// Token: 0x17000687 RID: 1671
		// (get) Token: 0x06001B3D RID: 6973 RVA: 0x0007CE43 File Offset: 0x0007B043
		public static Length minWidth
		{
			get
			{
				return InitialStyle.s_InitialStyle.layoutData.Read().minWidth;
			}
		}

		// Token: 0x17000688 RID: 1672
		// (get) Token: 0x06001B3E RID: 6974 RVA: 0x0007CE59 File Offset: 0x0007B059
		public static float opacity
		{
			get
			{
				return InitialStyle.s_InitialStyle.visualData.Read().opacity;
			}
		}

		// Token: 0x17000689 RID: 1673
		// (get) Token: 0x06001B3F RID: 6975 RVA: 0x0007CE6F File Offset: 0x0007B06F
		public static OverflowInternal overflow
		{
			get
			{
				return InitialStyle.s_InitialStyle.visualData.Read().overflow;
			}
		}

		// Token: 0x1700068A RID: 1674
		// (get) Token: 0x06001B40 RID: 6976 RVA: 0x0007CE85 File Offset: 0x0007B085
		public static Length paddingBottom
		{
			get
			{
				return InitialStyle.s_InitialStyle.layoutData.Read().paddingBottom;
			}
		}

		// Token: 0x1700068B RID: 1675
		// (get) Token: 0x06001B41 RID: 6977 RVA: 0x0007CE9B File Offset: 0x0007B09B
		public static Length paddingLeft
		{
			get
			{
				return InitialStyle.s_InitialStyle.layoutData.Read().paddingLeft;
			}
		}

		// Token: 0x1700068C RID: 1676
		// (get) Token: 0x06001B42 RID: 6978 RVA: 0x0007CEB1 File Offset: 0x0007B0B1
		public static Length paddingRight
		{
			get
			{
				return InitialStyle.s_InitialStyle.layoutData.Read().paddingRight;
			}
		}

		// Token: 0x1700068D RID: 1677
		// (get) Token: 0x06001B43 RID: 6979 RVA: 0x0007CEC7 File Offset: 0x0007B0C7
		public static Length paddingTop
		{
			get
			{
				return InitialStyle.s_InitialStyle.layoutData.Read().paddingTop;
			}
		}

		// Token: 0x1700068E RID: 1678
		// (get) Token: 0x06001B44 RID: 6980 RVA: 0x0007CEDD File Offset: 0x0007B0DD
		public static Position position
		{
			get
			{
				return InitialStyle.s_InitialStyle.layoutData.Read().position;
			}
		}

		// Token: 0x1700068F RID: 1679
		// (get) Token: 0x06001B45 RID: 6981 RVA: 0x0007CEF3 File Offset: 0x0007B0F3
		public static Length right
		{
			get
			{
				return InitialStyle.s_InitialStyle.layoutData.Read().right;
			}
		}

		// Token: 0x17000690 RID: 1680
		// (get) Token: 0x06001B46 RID: 6982 RVA: 0x0007CF09 File Offset: 0x0007B109
		public static Rotate rotate
		{
			get
			{
				return InitialStyle.s_InitialStyle.transformData.Read().rotate;
			}
		}

		// Token: 0x17000691 RID: 1681
		// (get) Token: 0x06001B47 RID: 6983 RVA: 0x0007CF1F File Offset: 0x0007B11F
		public static Scale scale
		{
			get
			{
				return InitialStyle.s_InitialStyle.transformData.Read().scale;
			}
		}

		// Token: 0x17000692 RID: 1682
		// (get) Token: 0x06001B48 RID: 6984 RVA: 0x0007CF35 File Offset: 0x0007B135
		public static TextOverflow textOverflow
		{
			get
			{
				return InitialStyle.s_InitialStyle.rareData.Read().textOverflow;
			}
		}

		// Token: 0x17000693 RID: 1683
		// (get) Token: 0x06001B49 RID: 6985 RVA: 0x0007CF4B File Offset: 0x0007B14B
		public static TextShadow textShadow
		{
			get
			{
				return InitialStyle.s_InitialStyle.inheritedData.Read().textShadow;
			}
		}

		// Token: 0x17000694 RID: 1684
		// (get) Token: 0x06001B4A RID: 6986 RVA: 0x0007CF61 File Offset: 0x0007B161
		public static Length top
		{
			get
			{
				return InitialStyle.s_InitialStyle.layoutData.Read().top;
			}
		}

		// Token: 0x17000695 RID: 1685
		// (get) Token: 0x06001B4B RID: 6987 RVA: 0x0007CF77 File Offset: 0x0007B177
		public static TransformOrigin transformOrigin
		{
			get
			{
				return InitialStyle.s_InitialStyle.transformData.Read().transformOrigin;
			}
		}

		// Token: 0x17000696 RID: 1686
		// (get) Token: 0x06001B4C RID: 6988 RVA: 0x0007CF8D File Offset: 0x0007B18D
		public static List<TimeValue> transitionDelay
		{
			get
			{
				return InitialStyle.s_InitialStyle.transitionData.Read().transitionDelay;
			}
		}

		// Token: 0x17000697 RID: 1687
		// (get) Token: 0x06001B4D RID: 6989 RVA: 0x0007CFA3 File Offset: 0x0007B1A3
		public static List<TimeValue> transitionDuration
		{
			get
			{
				return InitialStyle.s_InitialStyle.transitionData.Read().transitionDuration;
			}
		}

		// Token: 0x17000698 RID: 1688
		// (get) Token: 0x06001B4E RID: 6990 RVA: 0x0007CFB9 File Offset: 0x0007B1B9
		public static List<StylePropertyName> transitionProperty
		{
			get
			{
				return InitialStyle.s_InitialStyle.transitionData.Read().transitionProperty;
			}
		}

		// Token: 0x17000699 RID: 1689
		// (get) Token: 0x06001B4F RID: 6991 RVA: 0x0007CFCF File Offset: 0x0007B1CF
		public static List<EasingFunction> transitionTimingFunction
		{
			get
			{
				return InitialStyle.s_InitialStyle.transitionData.Read().transitionTimingFunction;
			}
		}

		// Token: 0x1700069A RID: 1690
		// (get) Token: 0x06001B50 RID: 6992 RVA: 0x0007CFE5 File Offset: 0x0007B1E5
		public static Translate translate
		{
			get
			{
				return InitialStyle.s_InitialStyle.transformData.Read().translate;
			}
		}

		// Token: 0x1700069B RID: 1691
		// (get) Token: 0x06001B51 RID: 6993 RVA: 0x0007CFFB File Offset: 0x0007B1FB
		public static Color unityBackgroundImageTintColor
		{
			get
			{
				return InitialStyle.s_InitialStyle.rareData.Read().unityBackgroundImageTintColor;
			}
		}

		// Token: 0x1700069C RID: 1692
		// (get) Token: 0x06001B52 RID: 6994 RVA: 0x0007D011 File Offset: 0x0007B211
		public static ScaleMode unityBackgroundScaleMode
		{
			get
			{
				return InitialStyle.s_InitialStyle.rareData.Read().unityBackgroundScaleMode;
			}
		}

		// Token: 0x1700069D RID: 1693
		// (get) Token: 0x06001B53 RID: 6995 RVA: 0x0007D027 File Offset: 0x0007B227
		public static Font unityFont
		{
			get
			{
				return InitialStyle.s_InitialStyle.inheritedData.Read().unityFont;
			}
		}

		// Token: 0x1700069E RID: 1694
		// (get) Token: 0x06001B54 RID: 6996 RVA: 0x0007D03D File Offset: 0x0007B23D
		public static FontDefinition unityFontDefinition
		{
			get
			{
				return InitialStyle.s_InitialStyle.inheritedData.Read().unityFontDefinition;
			}
		}

		// Token: 0x1700069F RID: 1695
		// (get) Token: 0x06001B55 RID: 6997 RVA: 0x0007D053 File Offset: 0x0007B253
		public static FontStyle unityFontStyleAndWeight
		{
			get
			{
				return InitialStyle.s_InitialStyle.inheritedData.Read().unityFontStyleAndWeight;
			}
		}

		// Token: 0x170006A0 RID: 1696
		// (get) Token: 0x06001B56 RID: 6998 RVA: 0x0007D069 File Offset: 0x0007B269
		public static OverflowClipBox unityOverflowClipBox
		{
			get
			{
				return InitialStyle.s_InitialStyle.rareData.Read().unityOverflowClipBox;
			}
		}

		// Token: 0x170006A1 RID: 1697
		// (get) Token: 0x06001B57 RID: 6999 RVA: 0x0007D07F File Offset: 0x0007B27F
		public static Length unityParagraphSpacing
		{
			get
			{
				return InitialStyle.s_InitialStyle.inheritedData.Read().unityParagraphSpacing;
			}
		}

		// Token: 0x170006A2 RID: 1698
		// (get) Token: 0x06001B58 RID: 7000 RVA: 0x0007D095 File Offset: 0x0007B295
		public static int unitySliceBottom
		{
			get
			{
				return InitialStyle.s_InitialStyle.rareData.Read().unitySliceBottom;
			}
		}

		// Token: 0x170006A3 RID: 1699
		// (get) Token: 0x06001B59 RID: 7001 RVA: 0x0007D0AB File Offset: 0x0007B2AB
		public static int unitySliceLeft
		{
			get
			{
				return InitialStyle.s_InitialStyle.rareData.Read().unitySliceLeft;
			}
		}

		// Token: 0x170006A4 RID: 1700
		// (get) Token: 0x06001B5A RID: 7002 RVA: 0x0007D0C1 File Offset: 0x0007B2C1
		public static int unitySliceRight
		{
			get
			{
				return InitialStyle.s_InitialStyle.rareData.Read().unitySliceRight;
			}
		}

		// Token: 0x170006A5 RID: 1701
		// (get) Token: 0x06001B5B RID: 7003 RVA: 0x0007D0D7 File Offset: 0x0007B2D7
		public static int unitySliceTop
		{
			get
			{
				return InitialStyle.s_InitialStyle.rareData.Read().unitySliceTop;
			}
		}

		// Token: 0x170006A6 RID: 1702
		// (get) Token: 0x06001B5C RID: 7004 RVA: 0x0007D0ED File Offset: 0x0007B2ED
		public static TextAnchor unityTextAlign
		{
			get
			{
				return InitialStyle.s_InitialStyle.inheritedData.Read().unityTextAlign;
			}
		}

		// Token: 0x170006A7 RID: 1703
		// (get) Token: 0x06001B5D RID: 7005 RVA: 0x0007D103 File Offset: 0x0007B303
		public static Color unityTextOutlineColor
		{
			get
			{
				return InitialStyle.s_InitialStyle.inheritedData.Read().unityTextOutlineColor;
			}
		}

		// Token: 0x170006A8 RID: 1704
		// (get) Token: 0x06001B5E RID: 7006 RVA: 0x0007D119 File Offset: 0x0007B319
		public static float unityTextOutlineWidth
		{
			get
			{
				return InitialStyle.s_InitialStyle.inheritedData.Read().unityTextOutlineWidth;
			}
		}

		// Token: 0x170006A9 RID: 1705
		// (get) Token: 0x06001B5F RID: 7007 RVA: 0x0007D12F File Offset: 0x0007B32F
		public static TextOverflowPosition unityTextOverflowPosition
		{
			get
			{
				return InitialStyle.s_InitialStyle.rareData.Read().unityTextOverflowPosition;
			}
		}

		// Token: 0x170006AA RID: 1706
		// (get) Token: 0x06001B60 RID: 7008 RVA: 0x0007D145 File Offset: 0x0007B345
		public static Visibility visibility
		{
			get
			{
				return InitialStyle.s_InitialStyle.inheritedData.Read().visibility;
			}
		}

		// Token: 0x170006AB RID: 1707
		// (get) Token: 0x06001B61 RID: 7009 RVA: 0x0007D15B File Offset: 0x0007B35B
		public static WhiteSpace whiteSpace
		{
			get
			{
				return InitialStyle.s_InitialStyle.inheritedData.Read().whiteSpace;
			}
		}

		// Token: 0x170006AC RID: 1708
		// (get) Token: 0x06001B62 RID: 7010 RVA: 0x0007D171 File Offset: 0x0007B371
		public static Length width
		{
			get
			{
				return InitialStyle.s_InitialStyle.layoutData.Read().width;
			}
		}

		// Token: 0x170006AD RID: 1709
		// (get) Token: 0x06001B63 RID: 7011 RVA: 0x0007D187 File Offset: 0x0007B387
		public static Length wordSpacing
		{
			get
			{
				return InitialStyle.s_InitialStyle.inheritedData.Read().wordSpacing;
			}
		}

		// Token: 0x04000D1C RID: 3356
		private static ComputedStyle s_InitialStyle = ComputedStyle.CreateInitial();
	}
}
