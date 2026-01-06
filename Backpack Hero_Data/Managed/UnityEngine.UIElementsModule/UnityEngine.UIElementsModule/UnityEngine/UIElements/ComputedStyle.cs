using System;
using System.Collections.Generic;
using UnityEngine.UIElements.StyleSheets;
using UnityEngine.Yoga;

namespace UnityEngine.UIElements
{
	// Token: 0x02000269 RID: 617
	internal struct ComputedStyle
	{
		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x0600128C RID: 4748 RVA: 0x00048C85 File Offset: 0x00046E85
		public int customPropertiesCount
		{
			get
			{
				Dictionary<string, StylePropertyValue> dictionary = this.customProperties;
				return (dictionary != null) ? dictionary.Count : 0;
			}
		}

		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x0600128D RID: 4749 RVA: 0x00048C99 File Offset: 0x00046E99
		public bool hasTransition
		{
			get
			{
				ComputedTransitionProperty[] array = this.computedTransitions;
				return array != null && array.Length != 0;
			}
		}

		// Token: 0x0600128E RID: 4750 RVA: 0x00048CAC File Offset: 0x00046EAC
		public static ComputedStyle Create()
		{
			return InitialStyle.Acquire();
		}

		// Token: 0x0600128F RID: 4751 RVA: 0x00048CC4 File Offset: 0x00046EC4
		public void FinalizeApply(ref ComputedStyle parentStyle)
		{
			bool flag = this.yogaNode == null;
			if (flag)
			{
				this.yogaNode = new YogaNode(null);
			}
			bool flag2 = this.fontSize.unit == LengthUnit.Percent;
			if (flag2)
			{
				float value = parentStyle.fontSize.value;
				float num = value * this.fontSize.value / 100f;
				this.inheritedData.Write().fontSize = new Length(num);
			}
			this.SyncWithLayout(this.yogaNode);
		}

		// Token: 0x06001290 RID: 4752 RVA: 0x00048D50 File Offset: 0x00046F50
		public void SyncWithLayout(YogaNode targetNode)
		{
			targetNode.Flex = float.NaN;
			targetNode.FlexGrow = this.flexGrow;
			targetNode.FlexShrink = this.flexShrink;
			targetNode.FlexBasis = this.flexBasis.ToYogaValue();
			targetNode.Left = this.left.ToYogaValue();
			targetNode.Top = this.top.ToYogaValue();
			targetNode.Right = this.right.ToYogaValue();
			targetNode.Bottom = this.bottom.ToYogaValue();
			targetNode.MarginLeft = this.marginLeft.ToYogaValue();
			targetNode.MarginTop = this.marginTop.ToYogaValue();
			targetNode.MarginRight = this.marginRight.ToYogaValue();
			targetNode.MarginBottom = this.marginBottom.ToYogaValue();
			targetNode.PaddingLeft = this.paddingLeft.ToYogaValue();
			targetNode.PaddingTop = this.paddingTop.ToYogaValue();
			targetNode.PaddingRight = this.paddingRight.ToYogaValue();
			targetNode.PaddingBottom = this.paddingBottom.ToYogaValue();
			targetNode.BorderLeftWidth = this.borderLeftWidth;
			targetNode.BorderTopWidth = this.borderTopWidth;
			targetNode.BorderRightWidth = this.borderRightWidth;
			targetNode.BorderBottomWidth = this.borderBottomWidth;
			targetNode.Width = this.width.ToYogaValue();
			targetNode.Height = this.height.ToYogaValue();
			targetNode.PositionType = (YogaPositionType)this.position;
			targetNode.Overflow = (YogaOverflow)this.overflow;
			targetNode.AlignSelf = (YogaAlign)this.alignSelf;
			targetNode.MaxWidth = this.maxWidth.ToYogaValue();
			targetNode.MaxHeight = this.maxHeight.ToYogaValue();
			targetNode.MinWidth = this.minWidth.ToYogaValue();
			targetNode.MinHeight = this.minHeight.ToYogaValue();
			targetNode.FlexDirection = (YogaFlexDirection)this.flexDirection;
			targetNode.AlignContent = (YogaAlign)this.alignContent;
			targetNode.AlignItems = (YogaAlign)this.alignItems;
			targetNode.JustifyContent = (YogaJustify)this.justifyContent;
			targetNode.Wrap = (YogaWrap)this.flexWrap;
			targetNode.Display = (YogaDisplay)this.display;
		}

		// Token: 0x06001291 RID: 4753 RVA: 0x00048F84 File Offset: 0x00047184
		private bool ApplyGlobalKeyword(StylePropertyReader reader, ref ComputedStyle parentStyle)
		{
			StyleValueHandle handle = reader.GetValue(0).handle;
			bool flag = handle.valueType == StyleValueType.Keyword;
			if (flag)
			{
				StyleValueKeyword valueIndex = (StyleValueKeyword)handle.valueIndex;
				StyleValueKeyword styleValueKeyword = valueIndex;
				if (styleValueKeyword == StyleValueKeyword.Initial)
				{
					this.ApplyInitialValue(reader);
					return true;
				}
				if (styleValueKeyword == StyleValueKeyword.Unset)
				{
					this.ApplyUnsetValue(reader, ref parentStyle);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001292 RID: 4754 RVA: 0x00048FE8 File Offset: 0x000471E8
		private bool ApplyGlobalKeyword(StylePropertyId id, StyleKeyword keyword, ref ComputedStyle parentStyle)
		{
			bool flag = keyword == StyleKeyword.Initial;
			bool flag2;
			if (flag)
			{
				this.ApplyInitialValue(id);
				flag2 = true;
			}
			else
			{
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x06001293 RID: 4755 RVA: 0x00049010 File Offset: 0x00047210
		private void RemoveCustomStyleProperty(StylePropertyReader reader)
		{
			string name = reader.property.name;
			bool flag = this.customProperties == null || !this.customProperties.ContainsKey(name);
			if (!flag)
			{
				this.customProperties.Remove(name);
			}
		}

		// Token: 0x06001294 RID: 4756 RVA: 0x00049058 File Offset: 0x00047258
		private void ApplyCustomStyleProperty(StylePropertyReader reader)
		{
			this.dpiScaling = reader.dpiScaling;
			bool flag = this.customProperties == null;
			if (flag)
			{
				this.customProperties = new Dictionary<string, StylePropertyValue>();
			}
			StyleProperty property = reader.property;
			StylePropertyValue value = reader.GetValue(0);
			this.customProperties[property.name] = value;
		}

		// Token: 0x06001295 RID: 4757 RVA: 0x000490AE File Offset: 0x000472AE
		private void ApplyAllPropertyInitial()
		{
			this.CopyFrom(InitialStyle.Get());
		}

		// Token: 0x06001296 RID: 4758 RVA: 0x000490BD File Offset: 0x000472BD
		private void ResetComputedTransitions()
		{
			this.computedTransitions = null;
		}

		// Token: 0x06001297 RID: 4759 RVA: 0x000490C8 File Offset: 0x000472C8
		public static VersionChangeType CompareChanges(ref ComputedStyle x, ref ComputedStyle y)
		{
			VersionChangeType versionChangeType = VersionChangeType.Layout | VersionChangeType.Styles | VersionChangeType.Repaint;
			bool flag = x.overflow != y.overflow;
			if (flag)
			{
				versionChangeType |= VersionChangeType.Overflow;
			}
			bool flag2 = x.borderBottomLeftRadius != y.borderBottomLeftRadius || x.borderBottomRightRadius != y.borderBottomRightRadius || x.borderTopLeftRadius != y.borderTopLeftRadius || x.borderTopRightRadius != y.borderTopRightRadius;
			if (flag2)
			{
				versionChangeType |= VersionChangeType.BorderRadius;
			}
			bool flag3 = x.borderLeftWidth != y.borderLeftWidth || x.borderTopWidth != y.borderTopWidth || x.borderRightWidth != y.borderRightWidth || x.borderBottomWidth != y.borderBottomWidth;
			if (flag3)
			{
				versionChangeType |= VersionChangeType.BorderWidth;
			}
			bool flag4 = x.opacity != y.opacity;
			if (flag4)
			{
				versionChangeType |= VersionChangeType.Opacity;
			}
			bool flag5 = !ComputedTransitionUtils.SameTransitionProperty(ref x, ref y);
			if (flag5)
			{
				versionChangeType |= VersionChangeType.TransitionProperty;
			}
			bool flag6 = x.transformOrigin != y.transformOrigin || x.translate != y.translate || x.scale != y.scale || x.rotate != y.rotate;
			if (flag6)
			{
				versionChangeType |= VersionChangeType.Transform;
			}
			return versionChangeType;
		}

		// Token: 0x06001298 RID: 4760 RVA: 0x00049238 File Offset: 0x00047438
		public static bool StartAnimationInlineTextShadow(VisualElement element, ref ComputedStyle computedStyle, StyleTextShadow textShadow, int durationMs, int delayMs, Func<float, float> easingCurve)
		{
			TextShadow textShadow2 = ((textShadow.keyword == StyleKeyword.Initial) ? InitialStyle.textShadow : textShadow.value);
			return element.styleAnimation.Start(StylePropertyId.TextShadow, computedStyle.inheritedData.Read().textShadow, textShadow2, durationMs, delayMs, easingCurve);
		}

		// Token: 0x06001299 RID: 4761 RVA: 0x0004928C File Offset: 0x0004748C
		public static bool StartAnimationInlineRotate(VisualElement element, ref ComputedStyle computedStyle, StyleRotate rotate, int durationMs, int delayMs, Func<float, float> easingCurve)
		{
			Rotate rotate2 = ((rotate.keyword == StyleKeyword.Initial) ? InitialStyle.rotate : rotate.value);
			return element.styleAnimation.Start(StylePropertyId.Rotate, computedStyle.transformData.Read().rotate, rotate2, durationMs, delayMs, easingCurve);
		}

		// Token: 0x0600129A RID: 4762 RVA: 0x000492E0 File Offset: 0x000474E0
		public static bool StartAnimationInlineTranslate(VisualElement element, ref ComputedStyle computedStyle, StyleTranslate translate, int durationMs, int delayMs, Func<float, float> easingCurve)
		{
			Translate translate2 = ((translate.keyword == StyleKeyword.Initial) ? InitialStyle.translate : translate.value);
			return element.styleAnimation.Start(StylePropertyId.Translate, computedStyle.transformData.Read().translate, translate2, durationMs, delayMs, easingCurve);
		}

		// Token: 0x0600129B RID: 4763 RVA: 0x00049334 File Offset: 0x00047534
		public static bool StartAnimationInlineScale(VisualElement element, ref ComputedStyle computedStyle, StyleScale scale, int durationMs, int delayMs, Func<float, float> easingCurve)
		{
			Scale scale2 = ((scale.keyword == StyleKeyword.Initial) ? InitialStyle.scale : scale.value);
			return element.styleAnimation.Start(StylePropertyId.Scale, computedStyle.transformData.Read().scale, scale2, durationMs, delayMs, easingCurve);
		}

		// Token: 0x0600129C RID: 4764 RVA: 0x00049388 File Offset: 0x00047588
		public static bool StartAnimationInlineTransformOrigin(VisualElement element, ref ComputedStyle computedStyle, StyleTransformOrigin transformOrigin, int durationMs, int delayMs, Func<float, float> easingCurve)
		{
			TransformOrigin transformOrigin2 = ((transformOrigin.keyword == StyleKeyword.Initial) ? InitialStyle.transformOrigin : transformOrigin.value);
			return element.styleAnimation.Start(StylePropertyId.TransformOrigin, computedStyle.transformData.Read().transformOrigin, transformOrigin2, durationMs, delayMs, easingCurve);
		}

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x0600129D RID: 4765 RVA: 0x000493D9 File Offset: 0x000475D9
		public Align alignContent
		{
			get
			{
				return this.layoutData.Read().alignContent;
			}
		}

		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x0600129E RID: 4766 RVA: 0x000493EB File Offset: 0x000475EB
		public Align alignItems
		{
			get
			{
				return this.layoutData.Read().alignItems;
			}
		}

		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x0600129F RID: 4767 RVA: 0x000493FD File Offset: 0x000475FD
		public Align alignSelf
		{
			get
			{
				return this.layoutData.Read().alignSelf;
			}
		}

		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x060012A0 RID: 4768 RVA: 0x0004940F File Offset: 0x0004760F
		public Color backgroundColor
		{
			get
			{
				return this.visualData.Read().backgroundColor;
			}
		}

		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x060012A1 RID: 4769 RVA: 0x00049421 File Offset: 0x00047621
		public Background backgroundImage
		{
			get
			{
				return this.visualData.Read().backgroundImage;
			}
		}

		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x060012A2 RID: 4770 RVA: 0x00049433 File Offset: 0x00047633
		public Color borderBottomColor
		{
			get
			{
				return this.visualData.Read().borderBottomColor;
			}
		}

		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x060012A3 RID: 4771 RVA: 0x00049445 File Offset: 0x00047645
		public Length borderBottomLeftRadius
		{
			get
			{
				return this.visualData.Read().borderBottomLeftRadius;
			}
		}

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x060012A4 RID: 4772 RVA: 0x00049457 File Offset: 0x00047657
		public Length borderBottomRightRadius
		{
			get
			{
				return this.visualData.Read().borderBottomRightRadius;
			}
		}

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x060012A5 RID: 4773 RVA: 0x00049469 File Offset: 0x00047669
		public float borderBottomWidth
		{
			get
			{
				return this.layoutData.Read().borderBottomWidth;
			}
		}

		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x060012A6 RID: 4774 RVA: 0x0004947B File Offset: 0x0004767B
		public Color borderLeftColor
		{
			get
			{
				return this.visualData.Read().borderLeftColor;
			}
		}

		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x060012A7 RID: 4775 RVA: 0x0004948D File Offset: 0x0004768D
		public float borderLeftWidth
		{
			get
			{
				return this.layoutData.Read().borderLeftWidth;
			}
		}

		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x060012A8 RID: 4776 RVA: 0x0004949F File Offset: 0x0004769F
		public Color borderRightColor
		{
			get
			{
				return this.visualData.Read().borderRightColor;
			}
		}

		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x060012A9 RID: 4777 RVA: 0x000494B1 File Offset: 0x000476B1
		public float borderRightWidth
		{
			get
			{
				return this.layoutData.Read().borderRightWidth;
			}
		}

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x060012AA RID: 4778 RVA: 0x000494C3 File Offset: 0x000476C3
		public Color borderTopColor
		{
			get
			{
				return this.visualData.Read().borderTopColor;
			}
		}

		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x060012AB RID: 4779 RVA: 0x000494D5 File Offset: 0x000476D5
		public Length borderTopLeftRadius
		{
			get
			{
				return this.visualData.Read().borderTopLeftRadius;
			}
		}

		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x060012AC RID: 4780 RVA: 0x000494E7 File Offset: 0x000476E7
		public Length borderTopRightRadius
		{
			get
			{
				return this.visualData.Read().borderTopRightRadius;
			}
		}

		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x060012AD RID: 4781 RVA: 0x000494F9 File Offset: 0x000476F9
		public float borderTopWidth
		{
			get
			{
				return this.layoutData.Read().borderTopWidth;
			}
		}

		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x060012AE RID: 4782 RVA: 0x0004950B File Offset: 0x0004770B
		public Length bottom
		{
			get
			{
				return this.layoutData.Read().bottom;
			}
		}

		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x060012AF RID: 4783 RVA: 0x0004951D File Offset: 0x0004771D
		public Color color
		{
			get
			{
				return this.inheritedData.Read().color;
			}
		}

		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x060012B0 RID: 4784 RVA: 0x0004952F File Offset: 0x0004772F
		public Cursor cursor
		{
			get
			{
				return this.rareData.Read().cursor;
			}
		}

		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x060012B1 RID: 4785 RVA: 0x00049541 File Offset: 0x00047741
		public DisplayStyle display
		{
			get
			{
				return this.layoutData.Read().display;
			}
		}

		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x060012B2 RID: 4786 RVA: 0x00049553 File Offset: 0x00047753
		public Length flexBasis
		{
			get
			{
				return this.layoutData.Read().flexBasis;
			}
		}

		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x060012B3 RID: 4787 RVA: 0x00049565 File Offset: 0x00047765
		public FlexDirection flexDirection
		{
			get
			{
				return this.layoutData.Read().flexDirection;
			}
		}

		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x060012B4 RID: 4788 RVA: 0x00049577 File Offset: 0x00047777
		public float flexGrow
		{
			get
			{
				return this.layoutData.Read().flexGrow;
			}
		}

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x060012B5 RID: 4789 RVA: 0x00049589 File Offset: 0x00047789
		public float flexShrink
		{
			get
			{
				return this.layoutData.Read().flexShrink;
			}
		}

		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x060012B6 RID: 4790 RVA: 0x0004959B File Offset: 0x0004779B
		public Wrap flexWrap
		{
			get
			{
				return this.layoutData.Read().flexWrap;
			}
		}

		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x060012B7 RID: 4791 RVA: 0x000495AD File Offset: 0x000477AD
		public Length fontSize
		{
			get
			{
				return this.inheritedData.Read().fontSize;
			}
		}

		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x060012B8 RID: 4792 RVA: 0x000495BF File Offset: 0x000477BF
		public Length height
		{
			get
			{
				return this.layoutData.Read().height;
			}
		}

		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x060012B9 RID: 4793 RVA: 0x000495D1 File Offset: 0x000477D1
		public Justify justifyContent
		{
			get
			{
				return this.layoutData.Read().justifyContent;
			}
		}

		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x060012BA RID: 4794 RVA: 0x000495E3 File Offset: 0x000477E3
		public Length left
		{
			get
			{
				return this.layoutData.Read().left;
			}
		}

		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x060012BB RID: 4795 RVA: 0x000495F5 File Offset: 0x000477F5
		public Length letterSpacing
		{
			get
			{
				return this.inheritedData.Read().letterSpacing;
			}
		}

		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x060012BC RID: 4796 RVA: 0x00049607 File Offset: 0x00047807
		public Length marginBottom
		{
			get
			{
				return this.layoutData.Read().marginBottom;
			}
		}

		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x060012BD RID: 4797 RVA: 0x00049619 File Offset: 0x00047819
		public Length marginLeft
		{
			get
			{
				return this.layoutData.Read().marginLeft;
			}
		}

		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x060012BE RID: 4798 RVA: 0x0004962B File Offset: 0x0004782B
		public Length marginRight
		{
			get
			{
				return this.layoutData.Read().marginRight;
			}
		}

		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x060012BF RID: 4799 RVA: 0x0004963D File Offset: 0x0004783D
		public Length marginTop
		{
			get
			{
				return this.layoutData.Read().marginTop;
			}
		}

		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x060012C0 RID: 4800 RVA: 0x0004964F File Offset: 0x0004784F
		public Length maxHeight
		{
			get
			{
				return this.layoutData.Read().maxHeight;
			}
		}

		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x060012C1 RID: 4801 RVA: 0x00049661 File Offset: 0x00047861
		public Length maxWidth
		{
			get
			{
				return this.layoutData.Read().maxWidth;
			}
		}

		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x060012C2 RID: 4802 RVA: 0x00049673 File Offset: 0x00047873
		public Length minHeight
		{
			get
			{
				return this.layoutData.Read().minHeight;
			}
		}

		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x060012C3 RID: 4803 RVA: 0x00049685 File Offset: 0x00047885
		public Length minWidth
		{
			get
			{
				return this.layoutData.Read().minWidth;
			}
		}

		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x060012C4 RID: 4804 RVA: 0x00049697 File Offset: 0x00047897
		public float opacity
		{
			get
			{
				return this.visualData.Read().opacity;
			}
		}

		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x060012C5 RID: 4805 RVA: 0x000496A9 File Offset: 0x000478A9
		public OverflowInternal overflow
		{
			get
			{
				return this.visualData.Read().overflow;
			}
		}

		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x060012C6 RID: 4806 RVA: 0x000496BB File Offset: 0x000478BB
		public Length paddingBottom
		{
			get
			{
				return this.layoutData.Read().paddingBottom;
			}
		}

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x060012C7 RID: 4807 RVA: 0x000496CD File Offset: 0x000478CD
		public Length paddingLeft
		{
			get
			{
				return this.layoutData.Read().paddingLeft;
			}
		}

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x060012C8 RID: 4808 RVA: 0x000496DF File Offset: 0x000478DF
		public Length paddingRight
		{
			get
			{
				return this.layoutData.Read().paddingRight;
			}
		}

		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x060012C9 RID: 4809 RVA: 0x000496F1 File Offset: 0x000478F1
		public Length paddingTop
		{
			get
			{
				return this.layoutData.Read().paddingTop;
			}
		}

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x060012CA RID: 4810 RVA: 0x00049703 File Offset: 0x00047903
		public Position position
		{
			get
			{
				return this.layoutData.Read().position;
			}
		}

		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x060012CB RID: 4811 RVA: 0x00049715 File Offset: 0x00047915
		public Length right
		{
			get
			{
				return this.layoutData.Read().right;
			}
		}

		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x060012CC RID: 4812 RVA: 0x00049727 File Offset: 0x00047927
		public Rotate rotate
		{
			get
			{
				return this.transformData.Read().rotate;
			}
		}

		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x060012CD RID: 4813 RVA: 0x00049739 File Offset: 0x00047939
		public Scale scale
		{
			get
			{
				return this.transformData.Read().scale;
			}
		}

		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x060012CE RID: 4814 RVA: 0x0004974B File Offset: 0x0004794B
		public TextOverflow textOverflow
		{
			get
			{
				return this.rareData.Read().textOverflow;
			}
		}

		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x060012CF RID: 4815 RVA: 0x0004975D File Offset: 0x0004795D
		public TextShadow textShadow
		{
			get
			{
				return this.inheritedData.Read().textShadow;
			}
		}

		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x060012D0 RID: 4816 RVA: 0x0004976F File Offset: 0x0004796F
		public Length top
		{
			get
			{
				return this.layoutData.Read().top;
			}
		}

		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x060012D1 RID: 4817 RVA: 0x00049781 File Offset: 0x00047981
		public TransformOrigin transformOrigin
		{
			get
			{
				return this.transformData.Read().transformOrigin;
			}
		}

		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x060012D2 RID: 4818 RVA: 0x00049793 File Offset: 0x00047993
		public List<TimeValue> transitionDelay
		{
			get
			{
				return this.transitionData.Read().transitionDelay;
			}
		}

		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x060012D3 RID: 4819 RVA: 0x000497A5 File Offset: 0x000479A5
		public List<TimeValue> transitionDuration
		{
			get
			{
				return this.transitionData.Read().transitionDuration;
			}
		}

		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x060012D4 RID: 4820 RVA: 0x000497B7 File Offset: 0x000479B7
		public List<StylePropertyName> transitionProperty
		{
			get
			{
				return this.transitionData.Read().transitionProperty;
			}
		}

		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x060012D5 RID: 4821 RVA: 0x000497C9 File Offset: 0x000479C9
		public List<EasingFunction> transitionTimingFunction
		{
			get
			{
				return this.transitionData.Read().transitionTimingFunction;
			}
		}

		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x060012D6 RID: 4822 RVA: 0x000497DB File Offset: 0x000479DB
		public Translate translate
		{
			get
			{
				return this.transformData.Read().translate;
			}
		}

		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x060012D7 RID: 4823 RVA: 0x000497ED File Offset: 0x000479ED
		public Color unityBackgroundImageTintColor
		{
			get
			{
				return this.rareData.Read().unityBackgroundImageTintColor;
			}
		}

		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x060012D8 RID: 4824 RVA: 0x000497FF File Offset: 0x000479FF
		public ScaleMode unityBackgroundScaleMode
		{
			get
			{
				return this.rareData.Read().unityBackgroundScaleMode;
			}
		}

		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x060012D9 RID: 4825 RVA: 0x00049811 File Offset: 0x00047A11
		public Font unityFont
		{
			get
			{
				return this.inheritedData.Read().unityFont;
			}
		}

		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x060012DA RID: 4826 RVA: 0x00049823 File Offset: 0x00047A23
		public FontDefinition unityFontDefinition
		{
			get
			{
				return this.inheritedData.Read().unityFontDefinition;
			}
		}

		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x060012DB RID: 4827 RVA: 0x00049835 File Offset: 0x00047A35
		public FontStyle unityFontStyleAndWeight
		{
			get
			{
				return this.inheritedData.Read().unityFontStyleAndWeight;
			}
		}

		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x060012DC RID: 4828 RVA: 0x00049847 File Offset: 0x00047A47
		public OverflowClipBox unityOverflowClipBox
		{
			get
			{
				return this.rareData.Read().unityOverflowClipBox;
			}
		}

		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x060012DD RID: 4829 RVA: 0x00049859 File Offset: 0x00047A59
		public Length unityParagraphSpacing
		{
			get
			{
				return this.inheritedData.Read().unityParagraphSpacing;
			}
		}

		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x060012DE RID: 4830 RVA: 0x0004986B File Offset: 0x00047A6B
		public int unitySliceBottom
		{
			get
			{
				return this.rareData.Read().unitySliceBottom;
			}
		}

		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x060012DF RID: 4831 RVA: 0x0004987D File Offset: 0x00047A7D
		public int unitySliceLeft
		{
			get
			{
				return this.rareData.Read().unitySliceLeft;
			}
		}

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x060012E0 RID: 4832 RVA: 0x0004988F File Offset: 0x00047A8F
		public int unitySliceRight
		{
			get
			{
				return this.rareData.Read().unitySliceRight;
			}
		}

		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x060012E1 RID: 4833 RVA: 0x000498A1 File Offset: 0x00047AA1
		public int unitySliceTop
		{
			get
			{
				return this.rareData.Read().unitySliceTop;
			}
		}

		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x060012E2 RID: 4834 RVA: 0x000498B3 File Offset: 0x00047AB3
		public TextAnchor unityTextAlign
		{
			get
			{
				return this.inheritedData.Read().unityTextAlign;
			}
		}

		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x060012E3 RID: 4835 RVA: 0x000498C5 File Offset: 0x00047AC5
		public Color unityTextOutlineColor
		{
			get
			{
				return this.inheritedData.Read().unityTextOutlineColor;
			}
		}

		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x060012E4 RID: 4836 RVA: 0x000498D7 File Offset: 0x00047AD7
		public float unityTextOutlineWidth
		{
			get
			{
				return this.inheritedData.Read().unityTextOutlineWidth;
			}
		}

		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x060012E5 RID: 4837 RVA: 0x000498E9 File Offset: 0x00047AE9
		public TextOverflowPosition unityTextOverflowPosition
		{
			get
			{
				return this.rareData.Read().unityTextOverflowPosition;
			}
		}

		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x060012E6 RID: 4838 RVA: 0x000498FB File Offset: 0x00047AFB
		public Visibility visibility
		{
			get
			{
				return this.inheritedData.Read().visibility;
			}
		}

		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x060012E7 RID: 4839 RVA: 0x0004990D File Offset: 0x00047B0D
		public WhiteSpace whiteSpace
		{
			get
			{
				return this.inheritedData.Read().whiteSpace;
			}
		}

		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x060012E8 RID: 4840 RVA: 0x0004991F File Offset: 0x00047B1F
		public Length width
		{
			get
			{
				return this.layoutData.Read().width;
			}
		}

		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x060012E9 RID: 4841 RVA: 0x00049931 File Offset: 0x00047B31
		public Length wordSpacing
		{
			get
			{
				return this.inheritedData.Read().wordSpacing;
			}
		}

		// Token: 0x060012EA RID: 4842 RVA: 0x00049944 File Offset: 0x00047B44
		public static ComputedStyle Create(ref ComputedStyle parentStyle)
		{
			ref ComputedStyle ptr = ref InitialStyle.Get();
			ComputedStyle computedStyle = new ComputedStyle
			{
				dpiScaling = 1f
			};
			computedStyle.inheritedData = parentStyle.inheritedData.Acquire();
			computedStyle.layoutData = ptr.layoutData.Acquire();
			computedStyle.rareData = ptr.rareData.Acquire();
			computedStyle.transformData = ptr.transformData.Acquire();
			computedStyle.transitionData = ptr.transitionData.Acquire();
			computedStyle.visualData = ptr.visualData.Acquire();
			return computedStyle;
		}

		// Token: 0x060012EB RID: 4843 RVA: 0x000499E0 File Offset: 0x00047BE0
		public static ComputedStyle CreateInitial()
		{
			ComputedStyle computedStyle = new ComputedStyle
			{
				dpiScaling = 1f
			};
			computedStyle.inheritedData = StyleDataRef<InheritedData>.Create();
			computedStyle.layoutData = StyleDataRef<LayoutData>.Create();
			computedStyle.rareData = StyleDataRef<RareData>.Create();
			computedStyle.transformData = StyleDataRef<TransformData>.Create();
			computedStyle.transitionData = StyleDataRef<TransitionData>.Create();
			computedStyle.visualData = StyleDataRef<VisualData>.Create();
			return computedStyle;
		}

		// Token: 0x060012EC RID: 4844 RVA: 0x00049A54 File Offset: 0x00047C54
		public ComputedStyle Acquire()
		{
			this.inheritedData.Acquire();
			this.layoutData.Acquire();
			this.rareData.Acquire();
			this.transformData.Acquire();
			this.transitionData.Acquire();
			this.visualData.Acquire();
			return this;
		}

		// Token: 0x060012ED RID: 4845 RVA: 0x00049AB4 File Offset: 0x00047CB4
		public void Release()
		{
			this.inheritedData.Release();
			this.layoutData.Release();
			this.rareData.Release();
			this.transformData.Release();
			this.transitionData.Release();
			this.visualData.Release();
		}

		// Token: 0x060012EE RID: 4846 RVA: 0x00049B0C File Offset: 0x00047D0C
		public void CopyFrom(ref ComputedStyle other)
		{
			this.inheritedData.CopyFrom(other.inheritedData);
			this.layoutData.CopyFrom(other.layoutData);
			this.rareData.CopyFrom(other.rareData);
			this.transformData.CopyFrom(other.transformData);
			this.transitionData.CopyFrom(other.transitionData);
			this.visualData.CopyFrom(other.visualData);
			this.yogaNode = other.yogaNode;
			this.customProperties = other.customProperties;
			this.matchingRulesHash = other.matchingRulesHash;
			this.dpiScaling = other.dpiScaling;
			this.computedTransitions = other.computedTransitions;
		}

		// Token: 0x060012EF RID: 4847 RVA: 0x00049BC4 File Offset: 0x00047DC4
		public void ApplyProperties(StylePropertyReader reader, ref ComputedStyle parentStyle)
		{
			StylePropertyId stylePropertyId = reader.propertyId;
			while (reader.property != null)
			{
				bool flag = this.ApplyGlobalKeyword(reader, ref parentStyle);
				if (!flag)
				{
					StylePropertyId stylePropertyId2 = stylePropertyId;
					StylePropertyId stylePropertyId3 = stylePropertyId2;
					if (stylePropertyId3 <= StylePropertyId.Width)
					{
						if (stylePropertyId3 <= StylePropertyId.Unknown)
						{
							if (stylePropertyId3 != StylePropertyId.Custom)
							{
								if (stylePropertyId3 != StylePropertyId.Unknown)
								{
									goto IL_0B23;
								}
							}
							else
							{
								this.ApplyCustomStyleProperty(reader);
							}
						}
						else
						{
							switch (stylePropertyId3)
							{
							case StylePropertyId.Color:
								this.inheritedData.Write().color = reader.ReadColor(0);
								break;
							case StylePropertyId.FontSize:
								this.inheritedData.Write().fontSize = reader.ReadLength(0);
								break;
							case StylePropertyId.LetterSpacing:
								this.inheritedData.Write().letterSpacing = reader.ReadLength(0);
								break;
							case StylePropertyId.TextShadow:
								this.inheritedData.Write().textShadow = reader.ReadTextShadow(0);
								break;
							case StylePropertyId.UnityFont:
								this.inheritedData.Write().unityFont = reader.ReadFont(0);
								break;
							case StylePropertyId.UnityFontDefinition:
								this.inheritedData.Write().unityFontDefinition = reader.ReadFontDefinition(0);
								break;
							case StylePropertyId.UnityFontStyleAndWeight:
								this.inheritedData.Write().unityFontStyleAndWeight = (FontStyle)reader.ReadEnum(StyleEnumType.FontStyle, 0);
								break;
							case StylePropertyId.UnityParagraphSpacing:
								this.inheritedData.Write().unityParagraphSpacing = reader.ReadLength(0);
								break;
							case StylePropertyId.UnityTextAlign:
								this.inheritedData.Write().unityTextAlign = (TextAnchor)reader.ReadEnum(StyleEnumType.TextAnchor, 0);
								break;
							case StylePropertyId.UnityTextOutlineColor:
								this.inheritedData.Write().unityTextOutlineColor = reader.ReadColor(0);
								break;
							case StylePropertyId.UnityTextOutlineWidth:
								this.inheritedData.Write().unityTextOutlineWidth = reader.ReadFloat(0);
								break;
							case StylePropertyId.Visibility:
								this.inheritedData.Write().visibility = (Visibility)reader.ReadEnum(StyleEnumType.Visibility, 0);
								break;
							case StylePropertyId.WhiteSpace:
								this.inheritedData.Write().whiteSpace = (WhiteSpace)reader.ReadEnum(StyleEnumType.WhiteSpace, 0);
								break;
							case StylePropertyId.WordSpacing:
								this.inheritedData.Write().wordSpacing = reader.ReadLength(0);
								break;
							default:
								switch (stylePropertyId3)
								{
								case StylePropertyId.AlignContent:
									this.layoutData.Write().alignContent = (Align)reader.ReadEnum(StyleEnumType.Align, 0);
									break;
								case StylePropertyId.AlignItems:
									this.layoutData.Write().alignItems = (Align)reader.ReadEnum(StyleEnumType.Align, 0);
									break;
								case StylePropertyId.AlignSelf:
									this.layoutData.Write().alignSelf = (Align)reader.ReadEnum(StyleEnumType.Align, 0);
									break;
								case StylePropertyId.BorderBottomWidth:
									this.layoutData.Write().borderBottomWidth = reader.ReadFloat(0);
									break;
								case StylePropertyId.BorderLeftWidth:
									this.layoutData.Write().borderLeftWidth = reader.ReadFloat(0);
									break;
								case StylePropertyId.BorderRightWidth:
									this.layoutData.Write().borderRightWidth = reader.ReadFloat(0);
									break;
								case StylePropertyId.BorderTopWidth:
									this.layoutData.Write().borderTopWidth = reader.ReadFloat(0);
									break;
								case StylePropertyId.Bottom:
									this.layoutData.Write().bottom = reader.ReadLength(0);
									break;
								case StylePropertyId.Display:
									this.layoutData.Write().display = (DisplayStyle)reader.ReadEnum(StyleEnumType.DisplayStyle, 0);
									break;
								case StylePropertyId.FlexBasis:
									this.layoutData.Write().flexBasis = reader.ReadLength(0);
									break;
								case StylePropertyId.FlexDirection:
									this.layoutData.Write().flexDirection = (FlexDirection)reader.ReadEnum(StyleEnumType.FlexDirection, 0);
									break;
								case StylePropertyId.FlexGrow:
									this.layoutData.Write().flexGrow = reader.ReadFloat(0);
									break;
								case StylePropertyId.FlexShrink:
									this.layoutData.Write().flexShrink = reader.ReadFloat(0);
									break;
								case StylePropertyId.FlexWrap:
									this.layoutData.Write().flexWrap = (Wrap)reader.ReadEnum(StyleEnumType.Wrap, 0);
									break;
								case StylePropertyId.Height:
									this.layoutData.Write().height = reader.ReadLength(0);
									break;
								case StylePropertyId.JustifyContent:
									this.layoutData.Write().justifyContent = (Justify)reader.ReadEnum(StyleEnumType.Justify, 0);
									break;
								case StylePropertyId.Left:
									this.layoutData.Write().left = reader.ReadLength(0);
									break;
								case StylePropertyId.MarginBottom:
									this.layoutData.Write().marginBottom = reader.ReadLength(0);
									break;
								case StylePropertyId.MarginLeft:
									this.layoutData.Write().marginLeft = reader.ReadLength(0);
									break;
								case StylePropertyId.MarginRight:
									this.layoutData.Write().marginRight = reader.ReadLength(0);
									break;
								case StylePropertyId.MarginTop:
									this.layoutData.Write().marginTop = reader.ReadLength(0);
									break;
								case StylePropertyId.MaxHeight:
									this.layoutData.Write().maxHeight = reader.ReadLength(0);
									break;
								case StylePropertyId.MaxWidth:
									this.layoutData.Write().maxWidth = reader.ReadLength(0);
									break;
								case StylePropertyId.MinHeight:
									this.layoutData.Write().minHeight = reader.ReadLength(0);
									break;
								case StylePropertyId.MinWidth:
									this.layoutData.Write().minWidth = reader.ReadLength(0);
									break;
								case StylePropertyId.PaddingBottom:
									this.layoutData.Write().paddingBottom = reader.ReadLength(0);
									break;
								case StylePropertyId.PaddingLeft:
									this.layoutData.Write().paddingLeft = reader.ReadLength(0);
									break;
								case StylePropertyId.PaddingRight:
									this.layoutData.Write().paddingRight = reader.ReadLength(0);
									break;
								case StylePropertyId.PaddingTop:
									this.layoutData.Write().paddingTop = reader.ReadLength(0);
									break;
								case StylePropertyId.Position:
									this.layoutData.Write().position = (Position)reader.ReadEnum(StyleEnumType.Position, 0);
									break;
								case StylePropertyId.Right:
									this.layoutData.Write().right = reader.ReadLength(0);
									break;
								case StylePropertyId.Top:
									this.layoutData.Write().top = reader.ReadLength(0);
									break;
								case StylePropertyId.Width:
									this.layoutData.Write().width = reader.ReadLength(0);
									break;
								default:
									goto IL_0B23;
								}
								break;
							}
						}
					}
					else if (stylePropertyId3 <= StylePropertyId.UnityTextOutline)
					{
						switch (stylePropertyId3)
						{
						case StylePropertyId.Cursor:
							this.rareData.Write().cursor = reader.ReadCursor(0);
							break;
						case StylePropertyId.TextOverflow:
							this.rareData.Write().textOverflow = (TextOverflow)reader.ReadEnum(StyleEnumType.TextOverflow, 0);
							break;
						case StylePropertyId.UnityBackgroundImageTintColor:
							this.rareData.Write().unityBackgroundImageTintColor = reader.ReadColor(0);
							break;
						case StylePropertyId.UnityBackgroundScaleMode:
							this.rareData.Write().unityBackgroundScaleMode = (ScaleMode)reader.ReadEnum(StyleEnumType.ScaleMode, 0);
							break;
						case StylePropertyId.UnityOverflowClipBox:
							this.rareData.Write().unityOverflowClipBox = (OverflowClipBox)reader.ReadEnum(StyleEnumType.OverflowClipBox, 0);
							break;
						case StylePropertyId.UnitySliceBottom:
							this.rareData.Write().unitySliceBottom = reader.ReadInt(0);
							break;
						case StylePropertyId.UnitySliceLeft:
							this.rareData.Write().unitySliceLeft = reader.ReadInt(0);
							break;
						case StylePropertyId.UnitySliceRight:
							this.rareData.Write().unitySliceRight = reader.ReadInt(0);
							break;
						case StylePropertyId.UnitySliceTop:
							this.rareData.Write().unitySliceTop = reader.ReadInt(0);
							break;
						case StylePropertyId.UnityTextOverflowPosition:
							this.rareData.Write().unityTextOverflowPosition = (TextOverflowPosition)reader.ReadEnum(StyleEnumType.TextOverflowPosition, 0);
							break;
						default:
							switch (stylePropertyId3)
							{
							case StylePropertyId.All:
								break;
							case StylePropertyId.BorderColor:
								ShorthandApplicator.ApplyBorderColor(reader, ref this);
								break;
							case StylePropertyId.BorderRadius:
								ShorthandApplicator.ApplyBorderRadius(reader, ref this);
								break;
							case StylePropertyId.BorderWidth:
								ShorthandApplicator.ApplyBorderWidth(reader, ref this);
								break;
							case StylePropertyId.Flex:
								ShorthandApplicator.ApplyFlex(reader, ref this);
								break;
							case StylePropertyId.Margin:
								ShorthandApplicator.ApplyMargin(reader, ref this);
								break;
							case StylePropertyId.Padding:
								ShorthandApplicator.ApplyPadding(reader, ref this);
								break;
							case StylePropertyId.Transition:
								ShorthandApplicator.ApplyTransition(reader, ref this);
								break;
							case StylePropertyId.UnityTextOutline:
								ShorthandApplicator.ApplyUnityTextOutline(reader, ref this);
								break;
							default:
								goto IL_0B23;
							}
							break;
						}
					}
					else
					{
						switch (stylePropertyId3)
						{
						case StylePropertyId.Rotate:
							this.transformData.Write().rotate = reader.ReadRotate(0);
							break;
						case StylePropertyId.Scale:
							this.transformData.Write().scale = reader.ReadScale(0);
							break;
						case StylePropertyId.TransformOrigin:
							this.transformData.Write().transformOrigin = reader.ReadTransformOrigin(0);
							break;
						case StylePropertyId.Translate:
							this.transformData.Write().translate = reader.ReadTranslate(0);
							break;
						default:
							switch (stylePropertyId3)
							{
							case StylePropertyId.TransitionDelay:
								reader.ReadListTimeValue(this.transitionData.Write().transitionDelay, 0);
								this.ResetComputedTransitions();
								break;
							case StylePropertyId.TransitionDuration:
								reader.ReadListTimeValue(this.transitionData.Write().transitionDuration, 0);
								this.ResetComputedTransitions();
								break;
							case StylePropertyId.TransitionProperty:
								reader.ReadListStylePropertyName(this.transitionData.Write().transitionProperty, 0);
								this.ResetComputedTransitions();
								break;
							case StylePropertyId.TransitionTimingFunction:
								reader.ReadListEasingFunction(this.transitionData.Write().transitionTimingFunction, 0);
								this.ResetComputedTransitions();
								break;
							default:
								switch (stylePropertyId3)
								{
								case StylePropertyId.BackgroundColor:
									this.visualData.Write().backgroundColor = reader.ReadColor(0);
									break;
								case StylePropertyId.BackgroundImage:
									this.visualData.Write().backgroundImage = reader.ReadBackground(0);
									break;
								case StylePropertyId.BorderBottomColor:
									this.visualData.Write().borderBottomColor = reader.ReadColor(0);
									break;
								case StylePropertyId.BorderBottomLeftRadius:
									this.visualData.Write().borderBottomLeftRadius = reader.ReadLength(0);
									break;
								case StylePropertyId.BorderBottomRightRadius:
									this.visualData.Write().borderBottomRightRadius = reader.ReadLength(0);
									break;
								case StylePropertyId.BorderLeftColor:
									this.visualData.Write().borderLeftColor = reader.ReadColor(0);
									break;
								case StylePropertyId.BorderRightColor:
									this.visualData.Write().borderRightColor = reader.ReadColor(0);
									break;
								case StylePropertyId.BorderTopColor:
									this.visualData.Write().borderTopColor = reader.ReadColor(0);
									break;
								case StylePropertyId.BorderTopLeftRadius:
									this.visualData.Write().borderTopLeftRadius = reader.ReadLength(0);
									break;
								case StylePropertyId.BorderTopRightRadius:
									this.visualData.Write().borderTopRightRadius = reader.ReadLength(0);
									break;
								case StylePropertyId.Opacity:
									this.visualData.Write().opacity = reader.ReadFloat(0);
									break;
								case StylePropertyId.Overflow:
									this.visualData.Write().overflow = (OverflowInternal)reader.ReadEnum(StyleEnumType.OverflowInternal, 0);
									break;
								default:
									goto IL_0B23;
								}
								break;
							}
							break;
						}
					}
					goto IL_0B3C;
					IL_0B23:
					Debug.LogAssertion(string.Format("Unknown property id {0}", stylePropertyId));
				}
				IL_0B3C:
				stylePropertyId = reader.MoveNextProperty();
			}
		}

		// Token: 0x060012F0 RID: 4848 RVA: 0x0004A728 File Offset: 0x00048928
		public void ApplyStyleValue(StyleValue sv, ref ComputedStyle parentStyle)
		{
			bool flag = this.ApplyGlobalKeyword(sv.id, sv.keyword, ref parentStyle);
			if (!flag)
			{
				StylePropertyId id = sv.id;
				StylePropertyId stylePropertyId = id;
				if (stylePropertyId <= StylePropertyId.Width)
				{
					switch (stylePropertyId)
					{
					case StylePropertyId.Color:
						this.inheritedData.Write().color = sv.color;
						return;
					case StylePropertyId.FontSize:
						this.inheritedData.Write().fontSize = sv.length;
						return;
					case StylePropertyId.LetterSpacing:
						this.inheritedData.Write().letterSpacing = sv.length;
						return;
					case StylePropertyId.TextShadow:
						break;
					case StylePropertyId.UnityFont:
						this.inheritedData.Write().unityFont = (sv.resource.IsAllocated ? (sv.resource.Target as Font) : null);
						return;
					case StylePropertyId.UnityFontDefinition:
						this.inheritedData.Write().unityFontDefinition = (sv.resource.IsAllocated ? FontDefinition.FromObject(sv.resource.Target) : default(FontDefinition));
						return;
					case StylePropertyId.UnityFontStyleAndWeight:
						this.inheritedData.Write().unityFontStyleAndWeight = (FontStyle)sv.number;
						return;
					case StylePropertyId.UnityParagraphSpacing:
						this.inheritedData.Write().unityParagraphSpacing = sv.length;
						return;
					case StylePropertyId.UnityTextAlign:
						this.inheritedData.Write().unityTextAlign = (TextAnchor)sv.number;
						return;
					case StylePropertyId.UnityTextOutlineColor:
						this.inheritedData.Write().unityTextOutlineColor = sv.color;
						return;
					case StylePropertyId.UnityTextOutlineWidth:
						this.inheritedData.Write().unityTextOutlineWidth = sv.number;
						return;
					case StylePropertyId.Visibility:
						this.inheritedData.Write().visibility = (Visibility)sv.number;
						return;
					case StylePropertyId.WhiteSpace:
						this.inheritedData.Write().whiteSpace = (WhiteSpace)sv.number;
						return;
					case StylePropertyId.WordSpacing:
						this.inheritedData.Write().wordSpacing = sv.length;
						return;
					default:
						switch (stylePropertyId)
						{
						case StylePropertyId.AlignContent:
						{
							this.layoutData.Write().alignContent = (Align)sv.number;
							bool flag2 = sv.keyword == StyleKeyword.Auto;
							if (flag2)
							{
								this.layoutData.Write().alignContent = Align.Auto;
							}
							return;
						}
						case StylePropertyId.AlignItems:
						{
							this.layoutData.Write().alignItems = (Align)sv.number;
							bool flag3 = sv.keyword == StyleKeyword.Auto;
							if (flag3)
							{
								this.layoutData.Write().alignItems = Align.Auto;
							}
							return;
						}
						case StylePropertyId.AlignSelf:
						{
							this.layoutData.Write().alignSelf = (Align)sv.number;
							bool flag4 = sv.keyword == StyleKeyword.Auto;
							if (flag4)
							{
								this.layoutData.Write().alignSelf = Align.Auto;
							}
							return;
						}
						case StylePropertyId.BorderBottomWidth:
							this.layoutData.Write().borderBottomWidth = sv.number;
							return;
						case StylePropertyId.BorderLeftWidth:
							this.layoutData.Write().borderLeftWidth = sv.number;
							return;
						case StylePropertyId.BorderRightWidth:
							this.layoutData.Write().borderRightWidth = sv.number;
							return;
						case StylePropertyId.BorderTopWidth:
							this.layoutData.Write().borderTopWidth = sv.number;
							return;
						case StylePropertyId.Bottom:
							this.layoutData.Write().bottom = sv.length;
							return;
						case StylePropertyId.Display:
						{
							this.layoutData.Write().display = (DisplayStyle)sv.number;
							bool flag5 = sv.keyword == StyleKeyword.None;
							if (flag5)
							{
								this.layoutData.Write().display = DisplayStyle.None;
							}
							return;
						}
						case StylePropertyId.FlexBasis:
							this.layoutData.Write().flexBasis = sv.length;
							return;
						case StylePropertyId.FlexDirection:
							this.layoutData.Write().flexDirection = (FlexDirection)sv.number;
							return;
						case StylePropertyId.FlexGrow:
							this.layoutData.Write().flexGrow = sv.number;
							return;
						case StylePropertyId.FlexShrink:
							this.layoutData.Write().flexShrink = sv.number;
							return;
						case StylePropertyId.FlexWrap:
							this.layoutData.Write().flexWrap = (Wrap)sv.number;
							return;
						case StylePropertyId.Height:
							this.layoutData.Write().height = sv.length;
							return;
						case StylePropertyId.JustifyContent:
							this.layoutData.Write().justifyContent = (Justify)sv.number;
							return;
						case StylePropertyId.Left:
							this.layoutData.Write().left = sv.length;
							return;
						case StylePropertyId.MarginBottom:
							this.layoutData.Write().marginBottom = sv.length;
							return;
						case StylePropertyId.MarginLeft:
							this.layoutData.Write().marginLeft = sv.length;
							return;
						case StylePropertyId.MarginRight:
							this.layoutData.Write().marginRight = sv.length;
							return;
						case StylePropertyId.MarginTop:
							this.layoutData.Write().marginTop = sv.length;
							return;
						case StylePropertyId.MaxHeight:
							this.layoutData.Write().maxHeight = sv.length;
							return;
						case StylePropertyId.MaxWidth:
							this.layoutData.Write().maxWidth = sv.length;
							return;
						case StylePropertyId.MinHeight:
							this.layoutData.Write().minHeight = sv.length;
							return;
						case StylePropertyId.MinWidth:
							this.layoutData.Write().minWidth = sv.length;
							return;
						case StylePropertyId.PaddingBottom:
							this.layoutData.Write().paddingBottom = sv.length;
							return;
						case StylePropertyId.PaddingLeft:
							this.layoutData.Write().paddingLeft = sv.length;
							return;
						case StylePropertyId.PaddingRight:
							this.layoutData.Write().paddingRight = sv.length;
							return;
						case StylePropertyId.PaddingTop:
							this.layoutData.Write().paddingTop = sv.length;
							return;
						case StylePropertyId.Position:
							this.layoutData.Write().position = (Position)sv.number;
							return;
						case StylePropertyId.Right:
							this.layoutData.Write().right = sv.length;
							return;
						case StylePropertyId.Top:
							this.layoutData.Write().top = sv.length;
							return;
						case StylePropertyId.Width:
							this.layoutData.Write().width = sv.length;
							return;
						}
						break;
					}
				}
				else
				{
					switch (stylePropertyId)
					{
					case StylePropertyId.TextOverflow:
						this.rareData.Write().textOverflow = (TextOverflow)sv.number;
						return;
					case StylePropertyId.UnityBackgroundImageTintColor:
						this.rareData.Write().unityBackgroundImageTintColor = sv.color;
						return;
					case StylePropertyId.UnityBackgroundScaleMode:
						this.rareData.Write().unityBackgroundScaleMode = (ScaleMode)sv.number;
						return;
					case StylePropertyId.UnityOverflowClipBox:
						this.rareData.Write().unityOverflowClipBox = (OverflowClipBox)sv.number;
						return;
					case StylePropertyId.UnitySliceBottom:
						this.rareData.Write().unitySliceBottom = (int)sv.number;
						return;
					case StylePropertyId.UnitySliceLeft:
						this.rareData.Write().unitySliceLeft = (int)sv.number;
						return;
					case StylePropertyId.UnitySliceRight:
						this.rareData.Write().unitySliceRight = (int)sv.number;
						return;
					case StylePropertyId.UnitySliceTop:
						this.rareData.Write().unitySliceTop = (int)sv.number;
						return;
					case StylePropertyId.UnityTextOverflowPosition:
						this.rareData.Write().unityTextOverflowPosition = (TextOverflowPosition)sv.number;
						return;
					default:
						switch (stylePropertyId)
						{
						case StylePropertyId.BackgroundColor:
							this.visualData.Write().backgroundColor = sv.color;
							return;
						case StylePropertyId.BackgroundImage:
							this.visualData.Write().backgroundImage = (sv.resource.IsAllocated ? Background.FromObject(sv.resource.Target) : default(Background));
							return;
						case StylePropertyId.BorderBottomColor:
							this.visualData.Write().borderBottomColor = sv.color;
							return;
						case StylePropertyId.BorderBottomLeftRadius:
							this.visualData.Write().borderBottomLeftRadius = sv.length;
							return;
						case StylePropertyId.BorderBottomRightRadius:
							this.visualData.Write().borderBottomRightRadius = sv.length;
							return;
						case StylePropertyId.BorderLeftColor:
							this.visualData.Write().borderLeftColor = sv.color;
							return;
						case StylePropertyId.BorderRightColor:
							this.visualData.Write().borderRightColor = sv.color;
							return;
						case StylePropertyId.BorderTopColor:
							this.visualData.Write().borderTopColor = sv.color;
							return;
						case StylePropertyId.BorderTopLeftRadius:
							this.visualData.Write().borderTopLeftRadius = sv.length;
							return;
						case StylePropertyId.BorderTopRightRadius:
							this.visualData.Write().borderTopRightRadius = sv.length;
							return;
						case StylePropertyId.Opacity:
							this.visualData.Write().opacity = sv.number;
							return;
						case StylePropertyId.Overflow:
							this.visualData.Write().overflow = (OverflowInternal)sv.number;
							return;
						}
						break;
					}
				}
				Debug.LogAssertion(string.Format("Unexpected property id {0}", sv.id));
			}
		}

		// Token: 0x060012F1 RID: 4849 RVA: 0x0004B0CC File Offset: 0x000492CC
		public void ApplyStyleValueManaged(StyleValueManaged sv, ref ComputedStyle parentStyle)
		{
			bool flag = this.ApplyGlobalKeyword(sv.id, sv.keyword, ref parentStyle);
			if (!flag)
			{
				switch (sv.id)
				{
				case StylePropertyId.TransitionDelay:
				{
					bool flag2 = sv.value == null;
					if (flag2)
					{
						this.transitionData.Write().transitionDelay.CopyFrom(InitialStyle.transitionDelay);
					}
					else
					{
						this.transitionData.Write().transitionDelay = sv.value as List<TimeValue>;
					}
					this.ResetComputedTransitions();
					break;
				}
				case StylePropertyId.TransitionDuration:
				{
					bool flag3 = sv.value == null;
					if (flag3)
					{
						this.transitionData.Write().transitionDuration.CopyFrom(InitialStyle.transitionDuration);
					}
					else
					{
						this.transitionData.Write().transitionDuration = sv.value as List<TimeValue>;
					}
					this.ResetComputedTransitions();
					break;
				}
				case StylePropertyId.TransitionProperty:
				{
					bool flag4 = sv.value == null;
					if (flag4)
					{
						this.transitionData.Write().transitionProperty.CopyFrom(InitialStyle.transitionProperty);
					}
					else
					{
						this.transitionData.Write().transitionProperty = sv.value as List<StylePropertyName>;
					}
					this.ResetComputedTransitions();
					break;
				}
				case StylePropertyId.TransitionTimingFunction:
				{
					bool flag5 = sv.value == null;
					if (flag5)
					{
						this.transitionData.Write().transitionTimingFunction.CopyFrom(InitialStyle.transitionTimingFunction);
					}
					else
					{
						this.transitionData.Write().transitionTimingFunction = sv.value as List<EasingFunction>;
					}
					this.ResetComputedTransitions();
					break;
				}
				default:
					Debug.LogAssertion(string.Format("Unexpected property id {0}", sv.id));
					break;
				}
			}
		}

		// Token: 0x060012F2 RID: 4850 RVA: 0x0004B281 File Offset: 0x00049481
		public void ApplyStyleCursor(Cursor cursor)
		{
			this.rareData.Write().cursor = cursor;
		}

		// Token: 0x060012F3 RID: 4851 RVA: 0x0004B295 File Offset: 0x00049495
		public void ApplyStyleTextShadow(TextShadow st)
		{
			this.inheritedData.Write().textShadow = st;
		}

		// Token: 0x060012F4 RID: 4852 RVA: 0x0004B2AC File Offset: 0x000494AC
		public void ApplyFromComputedStyle(StylePropertyId id, ref ComputedStyle other)
		{
			if (id <= StylePropertyId.UnityTextOverflowPosition)
			{
				switch (id)
				{
				case StylePropertyId.Color:
					this.inheritedData.Write().color = other.inheritedData.Read().color;
					return;
				case StylePropertyId.FontSize:
					this.inheritedData.Write().fontSize = other.inheritedData.Read().fontSize;
					return;
				case StylePropertyId.LetterSpacing:
					this.inheritedData.Write().letterSpacing = other.inheritedData.Read().letterSpacing;
					return;
				case StylePropertyId.TextShadow:
					this.inheritedData.Write().textShadow = other.inheritedData.Read().textShadow;
					return;
				case StylePropertyId.UnityFont:
					this.inheritedData.Write().unityFont = other.inheritedData.Read().unityFont;
					return;
				case StylePropertyId.UnityFontDefinition:
					this.inheritedData.Write().unityFontDefinition = other.inheritedData.Read().unityFontDefinition;
					return;
				case StylePropertyId.UnityFontStyleAndWeight:
					this.inheritedData.Write().unityFontStyleAndWeight = other.inheritedData.Read().unityFontStyleAndWeight;
					return;
				case StylePropertyId.UnityParagraphSpacing:
					this.inheritedData.Write().unityParagraphSpacing = other.inheritedData.Read().unityParagraphSpacing;
					return;
				case StylePropertyId.UnityTextAlign:
					this.inheritedData.Write().unityTextAlign = other.inheritedData.Read().unityTextAlign;
					return;
				case StylePropertyId.UnityTextOutlineColor:
					this.inheritedData.Write().unityTextOutlineColor = other.inheritedData.Read().unityTextOutlineColor;
					return;
				case StylePropertyId.UnityTextOutlineWidth:
					this.inheritedData.Write().unityTextOutlineWidth = other.inheritedData.Read().unityTextOutlineWidth;
					return;
				case StylePropertyId.Visibility:
					this.inheritedData.Write().visibility = other.inheritedData.Read().visibility;
					return;
				case StylePropertyId.WhiteSpace:
					this.inheritedData.Write().whiteSpace = other.inheritedData.Read().whiteSpace;
					return;
				case StylePropertyId.WordSpacing:
					this.inheritedData.Write().wordSpacing = other.inheritedData.Read().wordSpacing;
					return;
				default:
					switch (id)
					{
					case StylePropertyId.AlignContent:
						this.layoutData.Write().alignContent = other.layoutData.Read().alignContent;
						return;
					case StylePropertyId.AlignItems:
						this.layoutData.Write().alignItems = other.layoutData.Read().alignItems;
						return;
					case StylePropertyId.AlignSelf:
						this.layoutData.Write().alignSelf = other.layoutData.Read().alignSelf;
						return;
					case StylePropertyId.BorderBottomWidth:
						this.layoutData.Write().borderBottomWidth = other.layoutData.Read().borderBottomWidth;
						return;
					case StylePropertyId.BorderLeftWidth:
						this.layoutData.Write().borderLeftWidth = other.layoutData.Read().borderLeftWidth;
						return;
					case StylePropertyId.BorderRightWidth:
						this.layoutData.Write().borderRightWidth = other.layoutData.Read().borderRightWidth;
						return;
					case StylePropertyId.BorderTopWidth:
						this.layoutData.Write().borderTopWidth = other.layoutData.Read().borderTopWidth;
						return;
					case StylePropertyId.Bottom:
						this.layoutData.Write().bottom = other.layoutData.Read().bottom;
						return;
					case StylePropertyId.Display:
						this.layoutData.Write().display = other.layoutData.Read().display;
						return;
					case StylePropertyId.FlexBasis:
						this.layoutData.Write().flexBasis = other.layoutData.Read().flexBasis;
						return;
					case StylePropertyId.FlexDirection:
						this.layoutData.Write().flexDirection = other.layoutData.Read().flexDirection;
						return;
					case StylePropertyId.FlexGrow:
						this.layoutData.Write().flexGrow = other.layoutData.Read().flexGrow;
						return;
					case StylePropertyId.FlexShrink:
						this.layoutData.Write().flexShrink = other.layoutData.Read().flexShrink;
						return;
					case StylePropertyId.FlexWrap:
						this.layoutData.Write().flexWrap = other.layoutData.Read().flexWrap;
						return;
					case StylePropertyId.Height:
						this.layoutData.Write().height = other.layoutData.Read().height;
						return;
					case StylePropertyId.JustifyContent:
						this.layoutData.Write().justifyContent = other.layoutData.Read().justifyContent;
						return;
					case StylePropertyId.Left:
						this.layoutData.Write().left = other.layoutData.Read().left;
						return;
					case StylePropertyId.MarginBottom:
						this.layoutData.Write().marginBottom = other.layoutData.Read().marginBottom;
						return;
					case StylePropertyId.MarginLeft:
						this.layoutData.Write().marginLeft = other.layoutData.Read().marginLeft;
						return;
					case StylePropertyId.MarginRight:
						this.layoutData.Write().marginRight = other.layoutData.Read().marginRight;
						return;
					case StylePropertyId.MarginTop:
						this.layoutData.Write().marginTop = other.layoutData.Read().marginTop;
						return;
					case StylePropertyId.MaxHeight:
						this.layoutData.Write().maxHeight = other.layoutData.Read().maxHeight;
						return;
					case StylePropertyId.MaxWidth:
						this.layoutData.Write().maxWidth = other.layoutData.Read().maxWidth;
						return;
					case StylePropertyId.MinHeight:
						this.layoutData.Write().minHeight = other.layoutData.Read().minHeight;
						return;
					case StylePropertyId.MinWidth:
						this.layoutData.Write().minWidth = other.layoutData.Read().minWidth;
						return;
					case StylePropertyId.PaddingBottom:
						this.layoutData.Write().paddingBottom = other.layoutData.Read().paddingBottom;
						return;
					case StylePropertyId.PaddingLeft:
						this.layoutData.Write().paddingLeft = other.layoutData.Read().paddingLeft;
						return;
					case StylePropertyId.PaddingRight:
						this.layoutData.Write().paddingRight = other.layoutData.Read().paddingRight;
						return;
					case StylePropertyId.PaddingTop:
						this.layoutData.Write().paddingTop = other.layoutData.Read().paddingTop;
						return;
					case StylePropertyId.Position:
						this.layoutData.Write().position = other.layoutData.Read().position;
						return;
					case StylePropertyId.Right:
						this.layoutData.Write().right = other.layoutData.Read().right;
						return;
					case StylePropertyId.Top:
						this.layoutData.Write().top = other.layoutData.Read().top;
						return;
					case StylePropertyId.Width:
						this.layoutData.Write().width = other.layoutData.Read().width;
						return;
					default:
						switch (id)
						{
						case StylePropertyId.Cursor:
							this.rareData.Write().cursor = other.rareData.Read().cursor;
							return;
						case StylePropertyId.TextOverflow:
							this.rareData.Write().textOverflow = other.rareData.Read().textOverflow;
							return;
						case StylePropertyId.UnityBackgroundImageTintColor:
							this.rareData.Write().unityBackgroundImageTintColor = other.rareData.Read().unityBackgroundImageTintColor;
							return;
						case StylePropertyId.UnityBackgroundScaleMode:
							this.rareData.Write().unityBackgroundScaleMode = other.rareData.Read().unityBackgroundScaleMode;
							return;
						case StylePropertyId.UnityOverflowClipBox:
							this.rareData.Write().unityOverflowClipBox = other.rareData.Read().unityOverflowClipBox;
							return;
						case StylePropertyId.UnitySliceBottom:
							this.rareData.Write().unitySliceBottom = other.rareData.Read().unitySliceBottom;
							return;
						case StylePropertyId.UnitySliceLeft:
							this.rareData.Write().unitySliceLeft = other.rareData.Read().unitySliceLeft;
							return;
						case StylePropertyId.UnitySliceRight:
							this.rareData.Write().unitySliceRight = other.rareData.Read().unitySliceRight;
							return;
						case StylePropertyId.UnitySliceTop:
							this.rareData.Write().unitySliceTop = other.rareData.Read().unitySliceTop;
							return;
						case StylePropertyId.UnityTextOverflowPosition:
							this.rareData.Write().unityTextOverflowPosition = other.rareData.Read().unityTextOverflowPosition;
							return;
						}
						break;
					}
					break;
				}
			}
			else
			{
				switch (id)
				{
				case StylePropertyId.Rotate:
					this.transformData.Write().rotate = other.transformData.Read().rotate;
					return;
				case StylePropertyId.Scale:
					this.transformData.Write().scale = other.transformData.Read().scale;
					return;
				case StylePropertyId.TransformOrigin:
					this.transformData.Write().transformOrigin = other.transformData.Read().transformOrigin;
					return;
				case StylePropertyId.Translate:
					this.transformData.Write().translate = other.transformData.Read().translate;
					return;
				default:
					switch (id)
					{
					case StylePropertyId.TransitionDelay:
						this.transitionData.Write().transitionDelay.CopyFrom(other.transitionData.Read().transitionDelay);
						this.ResetComputedTransitions();
						return;
					case StylePropertyId.TransitionDuration:
						this.transitionData.Write().transitionDuration.CopyFrom(other.transitionData.Read().transitionDuration);
						this.ResetComputedTransitions();
						return;
					case StylePropertyId.TransitionProperty:
						this.transitionData.Write().transitionProperty.CopyFrom(other.transitionData.Read().transitionProperty);
						this.ResetComputedTransitions();
						return;
					case StylePropertyId.TransitionTimingFunction:
						this.transitionData.Write().transitionTimingFunction.CopyFrom(other.transitionData.Read().transitionTimingFunction);
						this.ResetComputedTransitions();
						return;
					default:
						switch (id)
						{
						case StylePropertyId.BackgroundColor:
							this.visualData.Write().backgroundColor = other.visualData.Read().backgroundColor;
							return;
						case StylePropertyId.BackgroundImage:
							this.visualData.Write().backgroundImage = other.visualData.Read().backgroundImage;
							return;
						case StylePropertyId.BorderBottomColor:
							this.visualData.Write().borderBottomColor = other.visualData.Read().borderBottomColor;
							return;
						case StylePropertyId.BorderBottomLeftRadius:
							this.visualData.Write().borderBottomLeftRadius = other.visualData.Read().borderBottomLeftRadius;
							return;
						case StylePropertyId.BorderBottomRightRadius:
							this.visualData.Write().borderBottomRightRadius = other.visualData.Read().borderBottomRightRadius;
							return;
						case StylePropertyId.BorderLeftColor:
							this.visualData.Write().borderLeftColor = other.visualData.Read().borderLeftColor;
							return;
						case StylePropertyId.BorderRightColor:
							this.visualData.Write().borderRightColor = other.visualData.Read().borderRightColor;
							return;
						case StylePropertyId.BorderTopColor:
							this.visualData.Write().borderTopColor = other.visualData.Read().borderTopColor;
							return;
						case StylePropertyId.BorderTopLeftRadius:
							this.visualData.Write().borderTopLeftRadius = other.visualData.Read().borderTopLeftRadius;
							return;
						case StylePropertyId.BorderTopRightRadius:
							this.visualData.Write().borderTopRightRadius = other.visualData.Read().borderTopRightRadius;
							return;
						case StylePropertyId.Opacity:
							this.visualData.Write().opacity = other.visualData.Read().opacity;
							return;
						case StylePropertyId.Overflow:
							this.visualData.Write().overflow = other.visualData.Read().overflow;
							return;
						}
						break;
					}
					break;
				}
			}
			Debug.LogAssertion(string.Format("Unexpected property id {0}", id));
		}

		// Token: 0x060012F5 RID: 4853 RVA: 0x0004BFB8 File Offset: 0x0004A1B8
		public void ApplyPropertyAnimation(VisualElement ve, StylePropertyId id, Length newValue)
		{
			StylePropertyId stylePropertyId = id;
			StylePropertyId stylePropertyId2 = stylePropertyId;
			if (stylePropertyId2 <= StylePropertyId.UnityParagraphSpacing)
			{
				if (stylePropertyId2 == StylePropertyId.FontSize)
				{
					this.inheritedData.Write().fontSize = newValue;
					ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.StyleSheet);
					return;
				}
				if (stylePropertyId2 == StylePropertyId.LetterSpacing)
				{
					this.inheritedData.Write().letterSpacing = newValue;
					ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.StyleSheet | VersionChangeType.Repaint);
					return;
				}
				if (stylePropertyId2 == StylePropertyId.UnityParagraphSpacing)
				{
					this.inheritedData.Write().unityParagraphSpacing = newValue;
					ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.StyleSheet | VersionChangeType.Repaint);
					return;
				}
			}
			else
			{
				if (stylePropertyId2 == StylePropertyId.WordSpacing)
				{
					this.inheritedData.Write().wordSpacing = newValue;
					ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.StyleSheet | VersionChangeType.Repaint);
					return;
				}
				switch (stylePropertyId2)
				{
				case StylePropertyId.Bottom:
					this.layoutData.Write().bottom = newValue;
					ve.yogaNode.Bottom = newValue.ToYogaValue();
					ve.IncrementVersion(VersionChangeType.Layout);
					return;
				case StylePropertyId.Display:
				case StylePropertyId.FlexDirection:
				case StylePropertyId.FlexGrow:
				case StylePropertyId.FlexShrink:
				case StylePropertyId.FlexWrap:
				case StylePropertyId.JustifyContent:
				case StylePropertyId.Position:
					break;
				case StylePropertyId.FlexBasis:
					this.layoutData.Write().flexBasis = newValue;
					ve.yogaNode.FlexBasis = newValue.ToYogaValue();
					ve.IncrementVersion(VersionChangeType.Layout);
					return;
				case StylePropertyId.Height:
					this.layoutData.Write().height = newValue;
					ve.yogaNode.Height = newValue.ToYogaValue();
					ve.IncrementVersion(VersionChangeType.Layout);
					return;
				case StylePropertyId.Left:
					this.layoutData.Write().left = newValue;
					ve.yogaNode.Left = newValue.ToYogaValue();
					ve.IncrementVersion(VersionChangeType.Layout);
					return;
				case StylePropertyId.MarginBottom:
					this.layoutData.Write().marginBottom = newValue;
					ve.yogaNode.MarginBottom = newValue.ToYogaValue();
					ve.IncrementVersion(VersionChangeType.Layout);
					return;
				case StylePropertyId.MarginLeft:
					this.layoutData.Write().marginLeft = newValue;
					ve.yogaNode.MarginLeft = newValue.ToYogaValue();
					ve.IncrementVersion(VersionChangeType.Layout);
					return;
				case StylePropertyId.MarginRight:
					this.layoutData.Write().marginRight = newValue;
					ve.yogaNode.MarginRight = newValue.ToYogaValue();
					ve.IncrementVersion(VersionChangeType.Layout);
					return;
				case StylePropertyId.MarginTop:
					this.layoutData.Write().marginTop = newValue;
					ve.yogaNode.MarginTop = newValue.ToYogaValue();
					ve.IncrementVersion(VersionChangeType.Layout);
					return;
				case StylePropertyId.MaxHeight:
					this.layoutData.Write().maxHeight = newValue;
					ve.yogaNode.MaxHeight = newValue.ToYogaValue();
					ve.IncrementVersion(VersionChangeType.Layout);
					return;
				case StylePropertyId.MaxWidth:
					this.layoutData.Write().maxWidth = newValue;
					ve.yogaNode.MaxWidth = newValue.ToYogaValue();
					ve.IncrementVersion(VersionChangeType.Layout);
					return;
				case StylePropertyId.MinHeight:
					this.layoutData.Write().minHeight = newValue;
					ve.yogaNode.MinHeight = newValue.ToYogaValue();
					ve.IncrementVersion(VersionChangeType.Layout);
					return;
				case StylePropertyId.MinWidth:
					this.layoutData.Write().minWidth = newValue;
					ve.yogaNode.MinWidth = newValue.ToYogaValue();
					ve.IncrementVersion(VersionChangeType.Layout);
					return;
				case StylePropertyId.PaddingBottom:
					this.layoutData.Write().paddingBottom = newValue;
					ve.yogaNode.PaddingBottom = newValue.ToYogaValue();
					ve.IncrementVersion(VersionChangeType.Layout);
					return;
				case StylePropertyId.PaddingLeft:
					this.layoutData.Write().paddingLeft = newValue;
					ve.yogaNode.PaddingLeft = newValue.ToYogaValue();
					ve.IncrementVersion(VersionChangeType.Layout);
					return;
				case StylePropertyId.PaddingRight:
					this.layoutData.Write().paddingRight = newValue;
					ve.yogaNode.PaddingRight = newValue.ToYogaValue();
					ve.IncrementVersion(VersionChangeType.Layout);
					return;
				case StylePropertyId.PaddingTop:
					this.layoutData.Write().paddingTop = newValue;
					ve.yogaNode.PaddingTop = newValue.ToYogaValue();
					ve.IncrementVersion(VersionChangeType.Layout);
					return;
				case StylePropertyId.Right:
					this.layoutData.Write().right = newValue;
					ve.yogaNode.Right = newValue.ToYogaValue();
					ve.IncrementVersion(VersionChangeType.Layout);
					return;
				case StylePropertyId.Top:
					this.layoutData.Write().top = newValue;
					ve.yogaNode.Top = newValue.ToYogaValue();
					ve.IncrementVersion(VersionChangeType.Layout);
					return;
				case StylePropertyId.Width:
					this.layoutData.Write().width = newValue;
					ve.yogaNode.Width = newValue.ToYogaValue();
					ve.IncrementVersion(VersionChangeType.Layout);
					return;
				default:
					switch (stylePropertyId2)
					{
					case StylePropertyId.BorderBottomLeftRadius:
						this.visualData.Write().borderBottomLeftRadius = newValue;
						ve.IncrementVersion(VersionChangeType.BorderRadius | VersionChangeType.Repaint);
						return;
					case StylePropertyId.BorderBottomRightRadius:
						this.visualData.Write().borderBottomRightRadius = newValue;
						ve.IncrementVersion(VersionChangeType.BorderRadius | VersionChangeType.Repaint);
						return;
					case StylePropertyId.BorderTopLeftRadius:
						this.visualData.Write().borderTopLeftRadius = newValue;
						ve.IncrementVersion(VersionChangeType.BorderRadius | VersionChangeType.Repaint);
						return;
					case StylePropertyId.BorderTopRightRadius:
						this.visualData.Write().borderTopRightRadius = newValue;
						ve.IncrementVersion(VersionChangeType.BorderRadius | VersionChangeType.Repaint);
						return;
					}
					break;
				}
			}
			throw new ArgumentException("Invalid animation property id. Can't apply value of type 'Length' to property '" + id.ToString() + "'. Please make sure that this property is animatable.", "id");
		}

		// Token: 0x060012F6 RID: 4854 RVA: 0x0004C568 File Offset: 0x0004A768
		public void ApplyPropertyAnimation(VisualElement ve, StylePropertyId id, float newValue)
		{
			StylePropertyId stylePropertyId = id;
			StylePropertyId stylePropertyId2 = stylePropertyId;
			if (stylePropertyId2 != StylePropertyId.UnityTextOutlineWidth)
			{
				switch (stylePropertyId2)
				{
				case StylePropertyId.BorderBottomWidth:
					this.layoutData.Write().borderBottomWidth = newValue;
					ve.yogaNode.BorderBottomWidth = newValue;
					ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.BorderWidth | VersionChangeType.Repaint);
					return;
				case StylePropertyId.BorderLeftWidth:
					this.layoutData.Write().borderLeftWidth = newValue;
					ve.yogaNode.BorderLeftWidth = newValue;
					ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.BorderWidth | VersionChangeType.Repaint);
					return;
				case StylePropertyId.BorderRightWidth:
					this.layoutData.Write().borderRightWidth = newValue;
					ve.yogaNode.BorderRightWidth = newValue;
					ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.BorderWidth | VersionChangeType.Repaint);
					return;
				case StylePropertyId.BorderTopWidth:
					this.layoutData.Write().borderTopWidth = newValue;
					ve.yogaNode.BorderTopWidth = newValue;
					ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.BorderWidth | VersionChangeType.Repaint);
					return;
				case StylePropertyId.Bottom:
				case StylePropertyId.Display:
				case StylePropertyId.FlexBasis:
				case StylePropertyId.FlexDirection:
					break;
				case StylePropertyId.FlexGrow:
					this.layoutData.Write().flexGrow = newValue;
					ve.yogaNode.FlexGrow = newValue;
					ve.IncrementVersion(VersionChangeType.Layout);
					return;
				case StylePropertyId.FlexShrink:
					this.layoutData.Write().flexShrink = newValue;
					ve.yogaNode.FlexShrink = newValue;
					ve.IncrementVersion(VersionChangeType.Layout);
					return;
				default:
					if (stylePropertyId2 == StylePropertyId.Opacity)
					{
						this.visualData.Write().opacity = newValue;
						ve.IncrementVersion(VersionChangeType.Opacity);
						return;
					}
					break;
				}
				throw new ArgumentException("Invalid animation property id. Can't apply value of type 'float' to property '" + id.ToString() + "'. Please make sure that this property is animatable.", "id");
			}
			this.inheritedData.Write().unityTextOutlineWidth = newValue;
			ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.StyleSheet | VersionChangeType.Repaint);
		}

		// Token: 0x060012F7 RID: 4855 RVA: 0x0004C744 File Offset: 0x0004A944
		public void ApplyPropertyAnimation(VisualElement ve, StylePropertyId id, int newValue)
		{
			StylePropertyId stylePropertyId = id;
			StylePropertyId stylePropertyId2 = stylePropertyId;
			if (stylePropertyId2 <= StylePropertyId.FlexDirection)
			{
				if (stylePropertyId2 <= StylePropertyId.AlignSelf)
				{
					switch (stylePropertyId2)
					{
					case StylePropertyId.UnityFontStyleAndWeight:
						this.inheritedData.Write().unityFontStyleAndWeight = (FontStyle)newValue;
						ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.StyleSheet | VersionChangeType.Repaint);
						return;
					case StylePropertyId.UnityParagraphSpacing:
					case StylePropertyId.UnityTextOutlineColor:
					case StylePropertyId.UnityTextOutlineWidth:
						break;
					case StylePropertyId.UnityTextAlign:
						this.inheritedData.Write().unityTextAlign = (TextAnchor)newValue;
						ve.IncrementVersion(VersionChangeType.StyleSheet | VersionChangeType.Repaint);
						return;
					case StylePropertyId.Visibility:
						this.inheritedData.Write().visibility = (Visibility)newValue;
						ve.IncrementVersion(VersionChangeType.StyleSheet | VersionChangeType.Repaint);
						return;
					case StylePropertyId.WhiteSpace:
						this.inheritedData.Write().whiteSpace = (WhiteSpace)newValue;
						ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.StyleSheet);
						return;
					default:
						switch (stylePropertyId2)
						{
						case StylePropertyId.AlignContent:
							this.layoutData.Write().alignContent = (Align)newValue;
							ve.yogaNode.AlignContent = (YogaAlign)newValue;
							ve.IncrementVersion(VersionChangeType.Layout);
							return;
						case StylePropertyId.AlignItems:
							this.layoutData.Write().alignItems = (Align)newValue;
							ve.yogaNode.AlignItems = (YogaAlign)newValue;
							ve.IncrementVersion(VersionChangeType.Layout);
							return;
						case StylePropertyId.AlignSelf:
							this.layoutData.Write().alignSelf = (Align)newValue;
							ve.yogaNode.AlignSelf = (YogaAlign)newValue;
							ve.IncrementVersion(VersionChangeType.Layout);
							return;
						}
						break;
					}
				}
				else
				{
					if (stylePropertyId2 == StylePropertyId.Display)
					{
						this.layoutData.Write().display = (DisplayStyle)newValue;
						ve.yogaNode.Display = (YogaDisplay)newValue;
						ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Repaint);
						return;
					}
					if (stylePropertyId2 == StylePropertyId.FlexDirection)
					{
						this.layoutData.Write().flexDirection = (FlexDirection)newValue;
						ve.yogaNode.FlexDirection = (YogaFlexDirection)newValue;
						ve.IncrementVersion(VersionChangeType.Layout);
						return;
					}
				}
			}
			else if (stylePropertyId2 <= StylePropertyId.JustifyContent)
			{
				if (stylePropertyId2 == StylePropertyId.FlexWrap)
				{
					this.layoutData.Write().flexWrap = (Wrap)newValue;
					ve.yogaNode.Wrap = (YogaWrap)newValue;
					ve.IncrementVersion(VersionChangeType.Layout);
					return;
				}
				if (stylePropertyId2 == StylePropertyId.JustifyContent)
				{
					this.layoutData.Write().justifyContent = (Justify)newValue;
					ve.yogaNode.JustifyContent = (YogaJustify)newValue;
					ve.IncrementVersion(VersionChangeType.Layout);
					return;
				}
			}
			else
			{
				if (stylePropertyId2 == StylePropertyId.Position)
				{
					this.layoutData.Write().position = (Position)newValue;
					ve.yogaNode.PositionType = (YogaPositionType)newValue;
					ve.IncrementVersion(VersionChangeType.Layout);
					return;
				}
				switch (stylePropertyId2)
				{
				case StylePropertyId.TextOverflow:
					this.rareData.Write().textOverflow = (TextOverflow)newValue;
					ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Repaint);
					return;
				case StylePropertyId.UnityBackgroundImageTintColor:
					break;
				case StylePropertyId.UnityBackgroundScaleMode:
					this.rareData.Write().unityBackgroundScaleMode = (ScaleMode)newValue;
					ve.IncrementVersion(VersionChangeType.Repaint);
					return;
				case StylePropertyId.UnityOverflowClipBox:
					this.rareData.Write().unityOverflowClipBox = (OverflowClipBox)newValue;
					ve.IncrementVersion(VersionChangeType.Repaint);
					return;
				case StylePropertyId.UnitySliceBottom:
					this.rareData.Write().unitySliceBottom = newValue;
					ve.IncrementVersion(VersionChangeType.Repaint);
					return;
				case StylePropertyId.UnitySliceLeft:
					this.rareData.Write().unitySliceLeft = newValue;
					ve.IncrementVersion(VersionChangeType.Repaint);
					return;
				case StylePropertyId.UnitySliceRight:
					this.rareData.Write().unitySliceRight = newValue;
					ve.IncrementVersion(VersionChangeType.Repaint);
					return;
				case StylePropertyId.UnitySliceTop:
					this.rareData.Write().unitySliceTop = newValue;
					ve.IncrementVersion(VersionChangeType.Repaint);
					return;
				case StylePropertyId.UnityTextOverflowPosition:
					this.rareData.Write().unityTextOverflowPosition = (TextOverflowPosition)newValue;
					ve.IncrementVersion(VersionChangeType.Repaint);
					return;
				default:
					if (stylePropertyId2 == StylePropertyId.Overflow)
					{
						this.visualData.Write().overflow = (OverflowInternal)newValue;
						ve.yogaNode.Overflow = (YogaOverflow)newValue;
						ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Overflow);
						return;
					}
					break;
				}
			}
			throw new ArgumentException("Invalid animation property id. Can't apply value of type 'int' to property '" + id.ToString() + "'. Please make sure that this property is animatable.", "id");
		}

		// Token: 0x060012F8 RID: 4856 RVA: 0x0004CB7C File Offset: 0x0004AD7C
		public void ApplyPropertyAnimation(VisualElement ve, StylePropertyId id, Color newValue)
		{
			StylePropertyId stylePropertyId = id;
			StylePropertyId stylePropertyId2 = stylePropertyId;
			if (stylePropertyId2 <= StylePropertyId.UnityTextOutlineColor)
			{
				if (stylePropertyId2 == StylePropertyId.Color)
				{
					this.inheritedData.Write().color = newValue;
					ve.IncrementVersion(VersionChangeType.StyleSheet | VersionChangeType.Repaint);
					return;
				}
				if (stylePropertyId2 == StylePropertyId.UnityTextOutlineColor)
				{
					this.inheritedData.Write().unityTextOutlineColor = newValue;
					ve.IncrementVersion(VersionChangeType.StyleSheet | VersionChangeType.Repaint);
					return;
				}
			}
			else
			{
				if (stylePropertyId2 == StylePropertyId.UnityBackgroundImageTintColor)
				{
					this.rareData.Write().unityBackgroundImageTintColor = newValue;
					ve.IncrementVersion(VersionChangeType.Color);
					return;
				}
				switch (stylePropertyId2)
				{
				case StylePropertyId.BackgroundColor:
					this.visualData.Write().backgroundColor = newValue;
					ve.IncrementVersion(VersionChangeType.Color);
					return;
				case StylePropertyId.BorderBottomColor:
					this.visualData.Write().borderBottomColor = newValue;
					ve.IncrementVersion(VersionChangeType.Color);
					return;
				case StylePropertyId.BorderLeftColor:
					this.visualData.Write().borderLeftColor = newValue;
					ve.IncrementVersion(VersionChangeType.Color);
					return;
				case StylePropertyId.BorderRightColor:
					this.visualData.Write().borderRightColor = newValue;
					ve.IncrementVersion(VersionChangeType.Color);
					return;
				case StylePropertyId.BorderTopColor:
					this.visualData.Write().borderTopColor = newValue;
					ve.IncrementVersion(VersionChangeType.Color);
					return;
				}
			}
			throw new ArgumentException("Invalid animation property id. Can't apply value of type 'Color' to property '" + id.ToString() + "'. Please make sure that this property is animatable.", "id");
		}

		// Token: 0x060012F9 RID: 4857 RVA: 0x0004CD20 File Offset: 0x0004AF20
		public void ApplyPropertyAnimation(VisualElement ve, StylePropertyId id, Background newValue)
		{
			StylePropertyId stylePropertyId = id;
			StylePropertyId stylePropertyId2 = stylePropertyId;
			if (stylePropertyId2 != StylePropertyId.BackgroundImage)
			{
				throw new ArgumentException("Invalid animation property id. Can't apply value of type 'Background' to property '" + id.ToString() + "'. Please make sure that this property is animatable.", "id");
			}
			this.visualData.Write().backgroundImage = newValue;
			ve.IncrementVersion(VersionChangeType.Repaint);
		}

		// Token: 0x060012FA RID: 4858 RVA: 0x0004CD84 File Offset: 0x0004AF84
		public void ApplyPropertyAnimation(VisualElement ve, StylePropertyId id, Font newValue)
		{
			StylePropertyId stylePropertyId = id;
			StylePropertyId stylePropertyId2 = stylePropertyId;
			if (stylePropertyId2 != StylePropertyId.UnityFont)
			{
				throw new ArgumentException("Invalid animation property id. Can't apply value of type 'Font' to property '" + id.ToString() + "'. Please make sure that this property is animatable.", "id");
			}
			this.inheritedData.Write().unityFont = newValue;
			ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.StyleSheet | VersionChangeType.Repaint);
		}

		// Token: 0x060012FB RID: 4859 RVA: 0x0004CDE8 File Offset: 0x0004AFE8
		public void ApplyPropertyAnimation(VisualElement ve, StylePropertyId id, FontDefinition newValue)
		{
			StylePropertyId stylePropertyId = id;
			StylePropertyId stylePropertyId2 = stylePropertyId;
			if (stylePropertyId2 != StylePropertyId.UnityFontDefinition)
			{
				throw new ArgumentException("Invalid animation property id. Can't apply value of type 'FontDefinition' to property '" + id.ToString() + "'. Please make sure that this property is animatable.", "id");
			}
			this.inheritedData.Write().unityFontDefinition = newValue;
			ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.StyleSheet | VersionChangeType.Repaint);
		}

		// Token: 0x060012FC RID: 4860 RVA: 0x0004CE4C File Offset: 0x0004B04C
		public void ApplyPropertyAnimation(VisualElement ve, StylePropertyId id, TextShadow newValue)
		{
			StylePropertyId stylePropertyId = id;
			StylePropertyId stylePropertyId2 = stylePropertyId;
			if (stylePropertyId2 != StylePropertyId.TextShadow)
			{
				throw new ArgumentException("Invalid animation property id. Can't apply value of type 'TextShadow' to property '" + id.ToString() + "'. Please make sure that this property is animatable.", "id");
			}
			this.inheritedData.Write().textShadow = newValue;
			ve.IncrementVersion(VersionChangeType.StyleSheet | VersionChangeType.Repaint);
		}

		// Token: 0x060012FD RID: 4861 RVA: 0x0004CEB0 File Offset: 0x0004B0B0
		public void ApplyPropertyAnimation(VisualElement ve, StylePropertyId id, Translate newValue)
		{
			StylePropertyId stylePropertyId = id;
			StylePropertyId stylePropertyId2 = stylePropertyId;
			if (stylePropertyId2 != StylePropertyId.Translate)
			{
				throw new ArgumentException("Invalid animation property id. Can't apply value of type 'Translate' to property '" + id.ToString() + "'. Please make sure that this property is animatable.", "id");
			}
			this.transformData.Write().translate = newValue;
			ve.IncrementVersion(VersionChangeType.Transform);
		}

		// Token: 0x060012FE RID: 4862 RVA: 0x0004CF14 File Offset: 0x0004B114
		public void ApplyPropertyAnimation(VisualElement ve, StylePropertyId id, TransformOrigin newValue)
		{
			StylePropertyId stylePropertyId = id;
			StylePropertyId stylePropertyId2 = stylePropertyId;
			if (stylePropertyId2 != StylePropertyId.TransformOrigin)
			{
				throw new ArgumentException("Invalid animation property id. Can't apply value of type 'TransformOrigin' to property '" + id.ToString() + "'. Please make sure that this property is animatable.", "id");
			}
			this.transformData.Write().transformOrigin = newValue;
			ve.IncrementVersion(VersionChangeType.Repaint);
		}

		// Token: 0x060012FF RID: 4863 RVA: 0x0004CF78 File Offset: 0x0004B178
		public void ApplyPropertyAnimation(VisualElement ve, StylePropertyId id, Rotate newValue)
		{
			StylePropertyId stylePropertyId = id;
			StylePropertyId stylePropertyId2 = stylePropertyId;
			if (stylePropertyId2 != StylePropertyId.Rotate)
			{
				throw new ArgumentException("Invalid animation property id. Can't apply value of type 'Rotate' to property '" + id.ToString() + "'. Please make sure that this property is animatable.", "id");
			}
			this.transformData.Write().rotate = newValue;
			ve.IncrementVersion(VersionChangeType.Transform);
		}

		// Token: 0x06001300 RID: 4864 RVA: 0x0004CFDC File Offset: 0x0004B1DC
		public void ApplyPropertyAnimation(VisualElement ve, StylePropertyId id, Scale newValue)
		{
			StylePropertyId stylePropertyId = id;
			StylePropertyId stylePropertyId2 = stylePropertyId;
			if (stylePropertyId2 != StylePropertyId.Scale)
			{
				throw new ArgumentException("Invalid animation property id. Can't apply value of type 'Scale' to property '" + id.ToString() + "'. Please make sure that this property is animatable.", "id");
			}
			this.transformData.Write().scale = newValue;
			ve.IncrementVersion(VersionChangeType.Transform);
		}

		// Token: 0x06001301 RID: 4865 RVA: 0x0004D040 File Offset: 0x0004B240
		public static bool StartAnimation(VisualElement element, StylePropertyId id, ref ComputedStyle oldStyle, ref ComputedStyle newStyle, int durationMs, int delayMs, Func<float, float> easingCurve)
		{
			if (id <= StylePropertyId.UnityTextOverflowPosition)
			{
				switch (id)
				{
				case StylePropertyId.Color:
					return element.styleAnimation.Start(StylePropertyId.Color, oldStyle.inheritedData.Read().color, newStyle.inheritedData.Read().color, durationMs, delayMs, easingCurve);
				case StylePropertyId.FontSize:
					return element.styleAnimation.Start(StylePropertyId.FontSize, oldStyle.inheritedData.Read().fontSize, newStyle.inheritedData.Read().fontSize, durationMs, delayMs, easingCurve);
				case StylePropertyId.LetterSpacing:
					return element.styleAnimation.Start(StylePropertyId.LetterSpacing, oldStyle.inheritedData.Read().letterSpacing, newStyle.inheritedData.Read().letterSpacing, durationMs, delayMs, easingCurve);
				case StylePropertyId.TextShadow:
					return element.styleAnimation.Start(StylePropertyId.TextShadow, oldStyle.inheritedData.Read().textShadow, newStyle.inheritedData.Read().textShadow, durationMs, delayMs, easingCurve);
				case StylePropertyId.UnityFont:
					return element.styleAnimation.Start(StylePropertyId.UnityFont, oldStyle.inheritedData.Read().unityFont, newStyle.inheritedData.Read().unityFont, durationMs, delayMs, easingCurve);
				case StylePropertyId.UnityFontDefinition:
					return element.styleAnimation.Start(StylePropertyId.UnityFontDefinition, oldStyle.inheritedData.Read().unityFontDefinition, newStyle.inheritedData.Read().unityFontDefinition, durationMs, delayMs, easingCurve);
				case StylePropertyId.UnityFontStyleAndWeight:
					return element.styleAnimation.StartEnum(StylePropertyId.UnityFontStyleAndWeight, (int)oldStyle.inheritedData.Read().unityFontStyleAndWeight, (int)newStyle.inheritedData.Read().unityFontStyleAndWeight, durationMs, delayMs, easingCurve);
				case StylePropertyId.UnityParagraphSpacing:
					return element.styleAnimation.Start(StylePropertyId.UnityParagraphSpacing, oldStyle.inheritedData.Read().unityParagraphSpacing, newStyle.inheritedData.Read().unityParagraphSpacing, durationMs, delayMs, easingCurve);
				case StylePropertyId.UnityTextAlign:
					return element.styleAnimation.StartEnum(StylePropertyId.UnityTextAlign, (int)oldStyle.inheritedData.Read().unityTextAlign, (int)newStyle.inheritedData.Read().unityTextAlign, durationMs, delayMs, easingCurve);
				case StylePropertyId.UnityTextOutlineColor:
					return element.styleAnimation.Start(StylePropertyId.UnityTextOutlineColor, oldStyle.inheritedData.Read().unityTextOutlineColor, newStyle.inheritedData.Read().unityTextOutlineColor, durationMs, delayMs, easingCurve);
				case StylePropertyId.UnityTextOutlineWidth:
					return element.styleAnimation.Start(StylePropertyId.UnityTextOutlineWidth, oldStyle.inheritedData.Read().unityTextOutlineWidth, newStyle.inheritedData.Read().unityTextOutlineWidth, durationMs, delayMs, easingCurve);
				case StylePropertyId.Visibility:
					return element.styleAnimation.StartEnum(StylePropertyId.Visibility, (int)oldStyle.inheritedData.Read().visibility, (int)newStyle.inheritedData.Read().visibility, durationMs, delayMs, easingCurve);
				case StylePropertyId.WhiteSpace:
					return element.styleAnimation.StartEnum(StylePropertyId.WhiteSpace, (int)oldStyle.inheritedData.Read().whiteSpace, (int)newStyle.inheritedData.Read().whiteSpace, durationMs, delayMs, easingCurve);
				case StylePropertyId.WordSpacing:
					return element.styleAnimation.Start(StylePropertyId.WordSpacing, oldStyle.inheritedData.Read().wordSpacing, newStyle.inheritedData.Read().wordSpacing, durationMs, delayMs, easingCurve);
				default:
					switch (id)
					{
					case StylePropertyId.AlignContent:
						return element.styleAnimation.StartEnum(StylePropertyId.AlignContent, (int)oldStyle.layoutData.Read().alignContent, (int)newStyle.layoutData.Read().alignContent, durationMs, delayMs, easingCurve);
					case StylePropertyId.AlignItems:
						return element.styleAnimation.StartEnum(StylePropertyId.AlignItems, (int)oldStyle.layoutData.Read().alignItems, (int)newStyle.layoutData.Read().alignItems, durationMs, delayMs, easingCurve);
					case StylePropertyId.AlignSelf:
						return element.styleAnimation.StartEnum(StylePropertyId.AlignSelf, (int)oldStyle.layoutData.Read().alignSelf, (int)newStyle.layoutData.Read().alignSelf, durationMs, delayMs, easingCurve);
					case StylePropertyId.BorderBottomWidth:
						return element.styleAnimation.Start(StylePropertyId.BorderBottomWidth, oldStyle.layoutData.Read().borderBottomWidth, newStyle.layoutData.Read().borderBottomWidth, durationMs, delayMs, easingCurve);
					case StylePropertyId.BorderLeftWidth:
						return element.styleAnimation.Start(StylePropertyId.BorderLeftWidth, oldStyle.layoutData.Read().borderLeftWidth, newStyle.layoutData.Read().borderLeftWidth, durationMs, delayMs, easingCurve);
					case StylePropertyId.BorderRightWidth:
						return element.styleAnimation.Start(StylePropertyId.BorderRightWidth, oldStyle.layoutData.Read().borderRightWidth, newStyle.layoutData.Read().borderRightWidth, durationMs, delayMs, easingCurve);
					case StylePropertyId.BorderTopWidth:
						return element.styleAnimation.Start(StylePropertyId.BorderTopWidth, oldStyle.layoutData.Read().borderTopWidth, newStyle.layoutData.Read().borderTopWidth, durationMs, delayMs, easingCurve);
					case StylePropertyId.Bottom:
						return element.styleAnimation.Start(StylePropertyId.Bottom, oldStyle.layoutData.Read().bottom, newStyle.layoutData.Read().bottom, durationMs, delayMs, easingCurve);
					case StylePropertyId.Display:
						return element.styleAnimation.StartEnum(StylePropertyId.Display, (int)oldStyle.layoutData.Read().display, (int)newStyle.layoutData.Read().display, durationMs, delayMs, easingCurve);
					case StylePropertyId.FlexBasis:
						return element.styleAnimation.Start(StylePropertyId.FlexBasis, oldStyle.layoutData.Read().flexBasis, newStyle.layoutData.Read().flexBasis, durationMs, delayMs, easingCurve);
					case StylePropertyId.FlexDirection:
						return element.styleAnimation.StartEnum(StylePropertyId.FlexDirection, (int)oldStyle.layoutData.Read().flexDirection, (int)newStyle.layoutData.Read().flexDirection, durationMs, delayMs, easingCurve);
					case StylePropertyId.FlexGrow:
						return element.styleAnimation.Start(StylePropertyId.FlexGrow, oldStyle.layoutData.Read().flexGrow, newStyle.layoutData.Read().flexGrow, durationMs, delayMs, easingCurve);
					case StylePropertyId.FlexShrink:
						return element.styleAnimation.Start(StylePropertyId.FlexShrink, oldStyle.layoutData.Read().flexShrink, newStyle.layoutData.Read().flexShrink, durationMs, delayMs, easingCurve);
					case StylePropertyId.FlexWrap:
						return element.styleAnimation.StartEnum(StylePropertyId.FlexWrap, (int)oldStyle.layoutData.Read().flexWrap, (int)newStyle.layoutData.Read().flexWrap, durationMs, delayMs, easingCurve);
					case StylePropertyId.Height:
						return element.styleAnimation.Start(StylePropertyId.Height, oldStyle.layoutData.Read().height, newStyle.layoutData.Read().height, durationMs, delayMs, easingCurve);
					case StylePropertyId.JustifyContent:
						return element.styleAnimation.StartEnum(StylePropertyId.JustifyContent, (int)oldStyle.layoutData.Read().justifyContent, (int)newStyle.layoutData.Read().justifyContent, durationMs, delayMs, easingCurve);
					case StylePropertyId.Left:
						return element.styleAnimation.Start(StylePropertyId.Left, oldStyle.layoutData.Read().left, newStyle.layoutData.Read().left, durationMs, delayMs, easingCurve);
					case StylePropertyId.MarginBottom:
						return element.styleAnimation.Start(StylePropertyId.MarginBottom, oldStyle.layoutData.Read().marginBottom, newStyle.layoutData.Read().marginBottom, durationMs, delayMs, easingCurve);
					case StylePropertyId.MarginLeft:
						return element.styleAnimation.Start(StylePropertyId.MarginLeft, oldStyle.layoutData.Read().marginLeft, newStyle.layoutData.Read().marginLeft, durationMs, delayMs, easingCurve);
					case StylePropertyId.MarginRight:
						return element.styleAnimation.Start(StylePropertyId.MarginRight, oldStyle.layoutData.Read().marginRight, newStyle.layoutData.Read().marginRight, durationMs, delayMs, easingCurve);
					case StylePropertyId.MarginTop:
						return element.styleAnimation.Start(StylePropertyId.MarginTop, oldStyle.layoutData.Read().marginTop, newStyle.layoutData.Read().marginTop, durationMs, delayMs, easingCurve);
					case StylePropertyId.MaxHeight:
						return element.styleAnimation.Start(StylePropertyId.MaxHeight, oldStyle.layoutData.Read().maxHeight, newStyle.layoutData.Read().maxHeight, durationMs, delayMs, easingCurve);
					case StylePropertyId.MaxWidth:
						return element.styleAnimation.Start(StylePropertyId.MaxWidth, oldStyle.layoutData.Read().maxWidth, newStyle.layoutData.Read().maxWidth, durationMs, delayMs, easingCurve);
					case StylePropertyId.MinHeight:
						return element.styleAnimation.Start(StylePropertyId.MinHeight, oldStyle.layoutData.Read().minHeight, newStyle.layoutData.Read().minHeight, durationMs, delayMs, easingCurve);
					case StylePropertyId.MinWidth:
						return element.styleAnimation.Start(StylePropertyId.MinWidth, oldStyle.layoutData.Read().minWidth, newStyle.layoutData.Read().minWidth, durationMs, delayMs, easingCurve);
					case StylePropertyId.PaddingBottom:
						return element.styleAnimation.Start(StylePropertyId.PaddingBottom, oldStyle.layoutData.Read().paddingBottom, newStyle.layoutData.Read().paddingBottom, durationMs, delayMs, easingCurve);
					case StylePropertyId.PaddingLeft:
						return element.styleAnimation.Start(StylePropertyId.PaddingLeft, oldStyle.layoutData.Read().paddingLeft, newStyle.layoutData.Read().paddingLeft, durationMs, delayMs, easingCurve);
					case StylePropertyId.PaddingRight:
						return element.styleAnimation.Start(StylePropertyId.PaddingRight, oldStyle.layoutData.Read().paddingRight, newStyle.layoutData.Read().paddingRight, durationMs, delayMs, easingCurve);
					case StylePropertyId.PaddingTop:
						return element.styleAnimation.Start(StylePropertyId.PaddingTop, oldStyle.layoutData.Read().paddingTop, newStyle.layoutData.Read().paddingTop, durationMs, delayMs, easingCurve);
					case StylePropertyId.Position:
						return element.styleAnimation.StartEnum(StylePropertyId.Position, (int)oldStyle.layoutData.Read().position, (int)newStyle.layoutData.Read().position, durationMs, delayMs, easingCurve);
					case StylePropertyId.Right:
						return element.styleAnimation.Start(StylePropertyId.Right, oldStyle.layoutData.Read().right, newStyle.layoutData.Read().right, durationMs, delayMs, easingCurve);
					case StylePropertyId.Top:
						return element.styleAnimation.Start(StylePropertyId.Top, oldStyle.layoutData.Read().top, newStyle.layoutData.Read().top, durationMs, delayMs, easingCurve);
					case StylePropertyId.Width:
						return element.styleAnimation.Start(StylePropertyId.Width, oldStyle.layoutData.Read().width, newStyle.layoutData.Read().width, durationMs, delayMs, easingCurve);
					default:
						switch (id)
						{
						case StylePropertyId.TextOverflow:
							return element.styleAnimation.StartEnum(StylePropertyId.TextOverflow, (int)oldStyle.rareData.Read().textOverflow, (int)newStyle.rareData.Read().textOverflow, durationMs, delayMs, easingCurve);
						case StylePropertyId.UnityBackgroundImageTintColor:
							return element.styleAnimation.Start(StylePropertyId.UnityBackgroundImageTintColor, oldStyle.rareData.Read().unityBackgroundImageTintColor, newStyle.rareData.Read().unityBackgroundImageTintColor, durationMs, delayMs, easingCurve);
						case StylePropertyId.UnityBackgroundScaleMode:
							return element.styleAnimation.StartEnum(StylePropertyId.UnityBackgroundScaleMode, (int)oldStyle.rareData.Read().unityBackgroundScaleMode, (int)newStyle.rareData.Read().unityBackgroundScaleMode, durationMs, delayMs, easingCurve);
						case StylePropertyId.UnityOverflowClipBox:
							return element.styleAnimation.StartEnum(StylePropertyId.UnityOverflowClipBox, (int)oldStyle.rareData.Read().unityOverflowClipBox, (int)newStyle.rareData.Read().unityOverflowClipBox, durationMs, delayMs, easingCurve);
						case StylePropertyId.UnitySliceBottom:
							return element.styleAnimation.Start(StylePropertyId.UnitySliceBottom, oldStyle.rareData.Read().unitySliceBottom, newStyle.rareData.Read().unitySliceBottom, durationMs, delayMs, easingCurve);
						case StylePropertyId.UnitySliceLeft:
							return element.styleAnimation.Start(StylePropertyId.UnitySliceLeft, oldStyle.rareData.Read().unitySliceLeft, newStyle.rareData.Read().unitySliceLeft, durationMs, delayMs, easingCurve);
						case StylePropertyId.UnitySliceRight:
							return element.styleAnimation.Start(StylePropertyId.UnitySliceRight, oldStyle.rareData.Read().unitySliceRight, newStyle.rareData.Read().unitySliceRight, durationMs, delayMs, easingCurve);
						case StylePropertyId.UnitySliceTop:
							return element.styleAnimation.Start(StylePropertyId.UnitySliceTop, oldStyle.rareData.Read().unitySliceTop, newStyle.rareData.Read().unitySliceTop, durationMs, delayMs, easingCurve);
						case StylePropertyId.UnityTextOverflowPosition:
							return element.styleAnimation.StartEnum(StylePropertyId.UnityTextOverflowPosition, (int)oldStyle.rareData.Read().unityTextOverflowPosition, (int)newStyle.rareData.Read().unityTextOverflowPosition, durationMs, delayMs, easingCurve);
						}
						break;
					}
					break;
				}
			}
			else
			{
				switch (id)
				{
				case StylePropertyId.All:
					return ComputedStyle.StartAnimationAllProperty(element, ref oldStyle, ref newStyle, durationMs, delayMs, easingCurve);
				case StylePropertyId.BorderColor:
				{
					bool flag = false;
					flag |= element.styleAnimation.Start(StylePropertyId.BorderTopColor, oldStyle.visualData.Read().borderTopColor, newStyle.visualData.Read().borderTopColor, durationMs, delayMs, easingCurve);
					flag |= element.styleAnimation.Start(StylePropertyId.BorderRightColor, oldStyle.visualData.Read().borderRightColor, newStyle.visualData.Read().borderRightColor, durationMs, delayMs, easingCurve);
					flag |= element.styleAnimation.Start(StylePropertyId.BorderBottomColor, oldStyle.visualData.Read().borderBottomColor, newStyle.visualData.Read().borderBottomColor, durationMs, delayMs, easingCurve);
					return flag | element.styleAnimation.Start(StylePropertyId.BorderLeftColor, oldStyle.visualData.Read().borderLeftColor, newStyle.visualData.Read().borderLeftColor, durationMs, delayMs, easingCurve);
				}
				case StylePropertyId.BorderRadius:
				{
					bool flag2 = false;
					flag2 |= element.styleAnimation.Start(StylePropertyId.BorderTopLeftRadius, oldStyle.visualData.Read().borderTopLeftRadius, newStyle.visualData.Read().borderTopLeftRadius, durationMs, delayMs, easingCurve);
					flag2 |= element.styleAnimation.Start(StylePropertyId.BorderTopRightRadius, oldStyle.visualData.Read().borderTopRightRadius, newStyle.visualData.Read().borderTopRightRadius, durationMs, delayMs, easingCurve);
					flag2 |= element.styleAnimation.Start(StylePropertyId.BorderBottomRightRadius, oldStyle.visualData.Read().borderBottomRightRadius, newStyle.visualData.Read().borderBottomRightRadius, durationMs, delayMs, easingCurve);
					return flag2 | element.styleAnimation.Start(StylePropertyId.BorderBottomLeftRadius, oldStyle.visualData.Read().borderBottomLeftRadius, newStyle.visualData.Read().borderBottomLeftRadius, durationMs, delayMs, easingCurve);
				}
				case StylePropertyId.BorderWidth:
				{
					bool flag3 = false;
					flag3 |= element.styleAnimation.Start(StylePropertyId.BorderTopWidth, oldStyle.layoutData.Read().borderTopWidth, newStyle.layoutData.Read().borderTopWidth, durationMs, delayMs, easingCurve);
					flag3 |= element.styleAnimation.Start(StylePropertyId.BorderRightWidth, oldStyle.layoutData.Read().borderRightWidth, newStyle.layoutData.Read().borderRightWidth, durationMs, delayMs, easingCurve);
					flag3 |= element.styleAnimation.Start(StylePropertyId.BorderBottomWidth, oldStyle.layoutData.Read().borderBottomWidth, newStyle.layoutData.Read().borderBottomWidth, durationMs, delayMs, easingCurve);
					return flag3 | element.styleAnimation.Start(StylePropertyId.BorderLeftWidth, oldStyle.layoutData.Read().borderLeftWidth, newStyle.layoutData.Read().borderLeftWidth, durationMs, delayMs, easingCurve);
				}
				case StylePropertyId.Flex:
				{
					bool flag4 = false;
					flag4 |= element.styleAnimation.Start(StylePropertyId.FlexGrow, oldStyle.layoutData.Read().flexGrow, newStyle.layoutData.Read().flexGrow, durationMs, delayMs, easingCurve);
					flag4 |= element.styleAnimation.Start(StylePropertyId.FlexShrink, oldStyle.layoutData.Read().flexShrink, newStyle.layoutData.Read().flexShrink, durationMs, delayMs, easingCurve);
					return flag4 | element.styleAnimation.Start(StylePropertyId.FlexBasis, oldStyle.layoutData.Read().flexBasis, newStyle.layoutData.Read().flexBasis, durationMs, delayMs, easingCurve);
				}
				case StylePropertyId.Margin:
				{
					bool flag5 = false;
					flag5 |= element.styleAnimation.Start(StylePropertyId.MarginTop, oldStyle.layoutData.Read().marginTop, newStyle.layoutData.Read().marginTop, durationMs, delayMs, easingCurve);
					flag5 |= element.styleAnimation.Start(StylePropertyId.MarginRight, oldStyle.layoutData.Read().marginRight, newStyle.layoutData.Read().marginRight, durationMs, delayMs, easingCurve);
					flag5 |= element.styleAnimation.Start(StylePropertyId.MarginBottom, oldStyle.layoutData.Read().marginBottom, newStyle.layoutData.Read().marginBottom, durationMs, delayMs, easingCurve);
					return flag5 | element.styleAnimation.Start(StylePropertyId.MarginLeft, oldStyle.layoutData.Read().marginLeft, newStyle.layoutData.Read().marginLeft, durationMs, delayMs, easingCurve);
				}
				case StylePropertyId.Padding:
				{
					bool flag6 = false;
					flag6 |= element.styleAnimation.Start(StylePropertyId.PaddingTop, oldStyle.layoutData.Read().paddingTop, newStyle.layoutData.Read().paddingTop, durationMs, delayMs, easingCurve);
					flag6 |= element.styleAnimation.Start(StylePropertyId.PaddingRight, oldStyle.layoutData.Read().paddingRight, newStyle.layoutData.Read().paddingRight, durationMs, delayMs, easingCurve);
					flag6 |= element.styleAnimation.Start(StylePropertyId.PaddingBottom, oldStyle.layoutData.Read().paddingBottom, newStyle.layoutData.Read().paddingBottom, durationMs, delayMs, easingCurve);
					return flag6 | element.styleAnimation.Start(StylePropertyId.PaddingLeft, oldStyle.layoutData.Read().paddingLeft, newStyle.layoutData.Read().paddingLeft, durationMs, delayMs, easingCurve);
				}
				case StylePropertyId.Transition:
					break;
				case StylePropertyId.UnityTextOutline:
				{
					bool flag7 = false;
					flag7 |= element.styleAnimation.Start(StylePropertyId.UnityTextOutlineColor, oldStyle.inheritedData.Read().unityTextOutlineColor, newStyle.inheritedData.Read().unityTextOutlineColor, durationMs, delayMs, easingCurve);
					return flag7 | element.styleAnimation.Start(StylePropertyId.UnityTextOutlineWidth, oldStyle.inheritedData.Read().unityTextOutlineWidth, newStyle.inheritedData.Read().unityTextOutlineWidth, durationMs, delayMs, easingCurve);
				}
				default:
					switch (id)
					{
					case StylePropertyId.Rotate:
						return element.styleAnimation.Start(StylePropertyId.Rotate, oldStyle.transformData.Read().rotate, newStyle.transformData.Read().rotate, durationMs, delayMs, easingCurve);
					case StylePropertyId.Scale:
						return element.styleAnimation.Start(StylePropertyId.Scale, oldStyle.transformData.Read().scale, newStyle.transformData.Read().scale, durationMs, delayMs, easingCurve);
					case StylePropertyId.TransformOrigin:
						return element.styleAnimation.Start(StylePropertyId.TransformOrigin, oldStyle.transformData.Read().transformOrigin, newStyle.transformData.Read().transformOrigin, durationMs, delayMs, easingCurve);
					case StylePropertyId.Translate:
						return element.styleAnimation.Start(StylePropertyId.Translate, oldStyle.transformData.Read().translate, newStyle.transformData.Read().translate, durationMs, delayMs, easingCurve);
					default:
						switch (id)
						{
						case StylePropertyId.BackgroundColor:
							return element.styleAnimation.Start(StylePropertyId.BackgroundColor, oldStyle.visualData.Read().backgroundColor, newStyle.visualData.Read().backgroundColor, durationMs, delayMs, easingCurve);
						case StylePropertyId.BackgroundImage:
							return element.styleAnimation.Start(StylePropertyId.BackgroundImage, oldStyle.visualData.Read().backgroundImage, newStyle.visualData.Read().backgroundImage, durationMs, delayMs, easingCurve);
						case StylePropertyId.BorderBottomColor:
							return element.styleAnimation.Start(StylePropertyId.BorderBottomColor, oldStyle.visualData.Read().borderBottomColor, newStyle.visualData.Read().borderBottomColor, durationMs, delayMs, easingCurve);
						case StylePropertyId.BorderBottomLeftRadius:
							return element.styleAnimation.Start(StylePropertyId.BorderBottomLeftRadius, oldStyle.visualData.Read().borderBottomLeftRadius, newStyle.visualData.Read().borderBottomLeftRadius, durationMs, delayMs, easingCurve);
						case StylePropertyId.BorderBottomRightRadius:
							return element.styleAnimation.Start(StylePropertyId.BorderBottomRightRadius, oldStyle.visualData.Read().borderBottomRightRadius, newStyle.visualData.Read().borderBottomRightRadius, durationMs, delayMs, easingCurve);
						case StylePropertyId.BorderLeftColor:
							return element.styleAnimation.Start(StylePropertyId.BorderLeftColor, oldStyle.visualData.Read().borderLeftColor, newStyle.visualData.Read().borderLeftColor, durationMs, delayMs, easingCurve);
						case StylePropertyId.BorderRightColor:
							return element.styleAnimation.Start(StylePropertyId.BorderRightColor, oldStyle.visualData.Read().borderRightColor, newStyle.visualData.Read().borderRightColor, durationMs, delayMs, easingCurve);
						case StylePropertyId.BorderTopColor:
							return element.styleAnimation.Start(StylePropertyId.BorderTopColor, oldStyle.visualData.Read().borderTopColor, newStyle.visualData.Read().borderTopColor, durationMs, delayMs, easingCurve);
						case StylePropertyId.BorderTopLeftRadius:
							return element.styleAnimation.Start(StylePropertyId.BorderTopLeftRadius, oldStyle.visualData.Read().borderTopLeftRadius, newStyle.visualData.Read().borderTopLeftRadius, durationMs, delayMs, easingCurve);
						case StylePropertyId.BorderTopRightRadius:
							return element.styleAnimation.Start(StylePropertyId.BorderTopRightRadius, oldStyle.visualData.Read().borderTopRightRadius, newStyle.visualData.Read().borderTopRightRadius, durationMs, delayMs, easingCurve);
						case StylePropertyId.Opacity:
							return element.styleAnimation.Start(StylePropertyId.Opacity, oldStyle.visualData.Read().opacity, newStyle.visualData.Read().opacity, durationMs, delayMs, easingCurve);
						case StylePropertyId.Overflow:
							return element.styleAnimation.StartEnum(StylePropertyId.Overflow, (int)oldStyle.visualData.Read().overflow, (int)newStyle.visualData.Read().overflow, durationMs, delayMs, easingCurve);
						}
						break;
					}
					break;
				}
			}
			return false;
		}

		// Token: 0x06001302 RID: 4866 RVA: 0x0004E8F8 File Offset: 0x0004CAF8
		public static bool StartAnimationAllProperty(VisualElement element, ref ComputedStyle oldStyle, ref ComputedStyle newStyle, int durationMs, int delayMs, Func<float, float> easingCurve)
		{
			bool flag = false;
			bool flag2 = !oldStyle.inheritedData.Equals(newStyle.inheritedData);
			if (flag2)
			{
				readonly ref InheritedData ptr = ref oldStyle.inheritedData.Read();
				readonly ref InheritedData ptr2 = ref newStyle.inheritedData.Read();
				bool flag3 = ptr.color != ptr2.color;
				if (flag3)
				{
					flag |= element.styleAnimation.Start(StylePropertyId.Color, ptr.color, ptr2.color, durationMs, delayMs, easingCurve);
				}
				bool flag4 = ptr.fontSize != ptr2.fontSize;
				if (flag4)
				{
					flag |= element.styleAnimation.Start(StylePropertyId.FontSize, ptr.fontSize, ptr2.fontSize, durationMs, delayMs, easingCurve);
				}
				bool flag5 = ptr.letterSpacing != ptr2.letterSpacing;
				if (flag5)
				{
					flag |= element.styleAnimation.Start(StylePropertyId.LetterSpacing, ptr.letterSpacing, ptr2.letterSpacing, durationMs, delayMs, easingCurve);
				}
				bool flag6 = ptr.textShadow != ptr2.textShadow;
				if (flag6)
				{
					flag |= element.styleAnimation.Start(StylePropertyId.TextShadow, ptr.textShadow, ptr2.textShadow, durationMs, delayMs, easingCurve);
				}
				bool flag7 = ptr.unityFont != ptr2.unityFont;
				if (flag7)
				{
					flag |= element.styleAnimation.Start(StylePropertyId.UnityFont, ptr.unityFont, ptr2.unityFont, durationMs, delayMs, easingCurve);
				}
				bool flag8 = ptr.unityFontDefinition != ptr2.unityFontDefinition;
				if (flag8)
				{
					flag |= element.styleAnimation.Start(StylePropertyId.UnityFontDefinition, ptr.unityFontDefinition, ptr2.unityFontDefinition, durationMs, delayMs, easingCurve);
				}
				bool flag9 = ptr.unityFontStyleAndWeight != ptr2.unityFontStyleAndWeight;
				if (flag9)
				{
					flag |= element.styleAnimation.StartEnum(StylePropertyId.UnityFontStyleAndWeight, (int)ptr.unityFontStyleAndWeight, (int)ptr2.unityFontStyleAndWeight, durationMs, delayMs, easingCurve);
				}
				bool flag10 = ptr.unityParagraphSpacing != ptr2.unityParagraphSpacing;
				if (flag10)
				{
					flag |= element.styleAnimation.Start(StylePropertyId.UnityParagraphSpacing, ptr.unityParagraphSpacing, ptr2.unityParagraphSpacing, durationMs, delayMs, easingCurve);
				}
				bool flag11 = ptr.unityTextAlign != ptr2.unityTextAlign;
				if (flag11)
				{
					flag |= element.styleAnimation.StartEnum(StylePropertyId.UnityTextAlign, (int)ptr.unityTextAlign, (int)ptr2.unityTextAlign, durationMs, delayMs, easingCurve);
				}
				bool flag12 = ptr.unityTextOutlineColor != ptr2.unityTextOutlineColor;
				if (flag12)
				{
					flag |= element.styleAnimation.Start(StylePropertyId.UnityTextOutlineColor, ptr.unityTextOutlineColor, ptr2.unityTextOutlineColor, durationMs, delayMs, easingCurve);
				}
				bool flag13 = ptr.unityTextOutlineWidth != ptr2.unityTextOutlineWidth;
				if (flag13)
				{
					flag |= element.styleAnimation.Start(StylePropertyId.UnityTextOutlineWidth, ptr.unityTextOutlineWidth, ptr2.unityTextOutlineWidth, durationMs, delayMs, easingCurve);
				}
				bool flag14 = ptr.visibility != ptr2.visibility;
				if (flag14)
				{
					flag |= element.styleAnimation.StartEnum(StylePropertyId.Visibility, (int)ptr.visibility, (int)ptr2.visibility, durationMs, delayMs, easingCurve);
				}
				bool flag15 = ptr.whiteSpace != ptr2.whiteSpace;
				if (flag15)
				{
					flag |= element.styleAnimation.StartEnum(StylePropertyId.WhiteSpace, (int)ptr.whiteSpace, (int)ptr2.whiteSpace, durationMs, delayMs, easingCurve);
				}
				bool flag16 = ptr.wordSpacing != ptr2.wordSpacing;
				if (flag16)
				{
					flag |= element.styleAnimation.Start(StylePropertyId.WordSpacing, ptr.wordSpacing, ptr2.wordSpacing, durationMs, delayMs, easingCurve);
				}
			}
			bool flag17 = !oldStyle.layoutData.Equals(newStyle.layoutData);
			if (flag17)
			{
				readonly ref LayoutData ptr3 = ref oldStyle.layoutData.Read();
				readonly ref LayoutData ptr4 = ref newStyle.layoutData.Read();
				bool flag18 = ptr3.alignContent != ptr4.alignContent;
				if (flag18)
				{
					flag |= element.styleAnimation.StartEnum(StylePropertyId.AlignContent, (int)ptr3.alignContent, (int)ptr4.alignContent, durationMs, delayMs, easingCurve);
				}
				bool flag19 = ptr3.alignItems != ptr4.alignItems;
				if (flag19)
				{
					flag |= element.styleAnimation.StartEnum(StylePropertyId.AlignItems, (int)ptr3.alignItems, (int)ptr4.alignItems, durationMs, delayMs, easingCurve);
				}
				bool flag20 = ptr3.alignSelf != ptr4.alignSelf;
				if (flag20)
				{
					flag |= element.styleAnimation.StartEnum(StylePropertyId.AlignSelf, (int)ptr3.alignSelf, (int)ptr4.alignSelf, durationMs, delayMs, easingCurve);
				}
				bool flag21 = ptr3.borderBottomWidth != ptr4.borderBottomWidth;
				if (flag21)
				{
					flag |= element.styleAnimation.Start(StylePropertyId.BorderBottomWidth, ptr3.borderBottomWidth, ptr4.borderBottomWidth, durationMs, delayMs, easingCurve);
				}
				bool flag22 = ptr3.borderLeftWidth != ptr4.borderLeftWidth;
				if (flag22)
				{
					flag |= element.styleAnimation.Start(StylePropertyId.BorderLeftWidth, ptr3.borderLeftWidth, ptr4.borderLeftWidth, durationMs, delayMs, easingCurve);
				}
				bool flag23 = ptr3.borderRightWidth != ptr4.borderRightWidth;
				if (flag23)
				{
					flag |= element.styleAnimation.Start(StylePropertyId.BorderRightWidth, ptr3.borderRightWidth, ptr4.borderRightWidth, durationMs, delayMs, easingCurve);
				}
				bool flag24 = ptr3.borderTopWidth != ptr4.borderTopWidth;
				if (flag24)
				{
					flag |= element.styleAnimation.Start(StylePropertyId.BorderTopWidth, ptr3.borderTopWidth, ptr4.borderTopWidth, durationMs, delayMs, easingCurve);
				}
				bool flag25 = ptr3.bottom != ptr4.bottom;
				if (flag25)
				{
					flag |= element.styleAnimation.Start(StylePropertyId.Bottom, ptr3.bottom, ptr4.bottom, durationMs, delayMs, easingCurve);
				}
				bool flag26 = ptr3.display != ptr4.display;
				if (flag26)
				{
					flag |= element.styleAnimation.StartEnum(StylePropertyId.Display, (int)ptr3.display, (int)ptr4.display, durationMs, delayMs, easingCurve);
				}
				bool flag27 = ptr3.flexBasis != ptr4.flexBasis;
				if (flag27)
				{
					flag |= element.styleAnimation.Start(StylePropertyId.FlexBasis, ptr3.flexBasis, ptr4.flexBasis, durationMs, delayMs, easingCurve);
				}
				bool flag28 = ptr3.flexDirection != ptr4.flexDirection;
				if (flag28)
				{
					flag |= element.styleAnimation.StartEnum(StylePropertyId.FlexDirection, (int)ptr3.flexDirection, (int)ptr4.flexDirection, durationMs, delayMs, easingCurve);
				}
				bool flag29 = ptr3.flexGrow != ptr4.flexGrow;
				if (flag29)
				{
					flag |= element.styleAnimation.Start(StylePropertyId.FlexGrow, ptr3.flexGrow, ptr4.flexGrow, durationMs, delayMs, easingCurve);
				}
				bool flag30 = ptr3.flexShrink != ptr4.flexShrink;
				if (flag30)
				{
					flag |= element.styleAnimation.Start(StylePropertyId.FlexShrink, ptr3.flexShrink, ptr4.flexShrink, durationMs, delayMs, easingCurve);
				}
				bool flag31 = ptr3.flexWrap != ptr4.flexWrap;
				if (flag31)
				{
					flag |= element.styleAnimation.StartEnum(StylePropertyId.FlexWrap, (int)ptr3.flexWrap, (int)ptr4.flexWrap, durationMs, delayMs, easingCurve);
				}
				bool flag32 = ptr3.height != ptr4.height;
				if (flag32)
				{
					flag |= element.styleAnimation.Start(StylePropertyId.Height, ptr3.height, ptr4.height, durationMs, delayMs, easingCurve);
				}
				bool flag33 = ptr3.justifyContent != ptr4.justifyContent;
				if (flag33)
				{
					flag |= element.styleAnimation.StartEnum(StylePropertyId.JustifyContent, (int)ptr3.justifyContent, (int)ptr4.justifyContent, durationMs, delayMs, easingCurve);
				}
				bool flag34 = ptr3.left != ptr4.left;
				if (flag34)
				{
					flag |= element.styleAnimation.Start(StylePropertyId.Left, ptr3.left, ptr4.left, durationMs, delayMs, easingCurve);
				}
				bool flag35 = ptr3.marginBottom != ptr4.marginBottom;
				if (flag35)
				{
					flag |= element.styleAnimation.Start(StylePropertyId.MarginBottom, ptr3.marginBottom, ptr4.marginBottom, durationMs, delayMs, easingCurve);
				}
				bool flag36 = ptr3.marginLeft != ptr4.marginLeft;
				if (flag36)
				{
					flag |= element.styleAnimation.Start(StylePropertyId.MarginLeft, ptr3.marginLeft, ptr4.marginLeft, durationMs, delayMs, easingCurve);
				}
				bool flag37 = ptr3.marginRight != ptr4.marginRight;
				if (flag37)
				{
					flag |= element.styleAnimation.Start(StylePropertyId.MarginRight, ptr3.marginRight, ptr4.marginRight, durationMs, delayMs, easingCurve);
				}
				bool flag38 = ptr3.marginTop != ptr4.marginTop;
				if (flag38)
				{
					flag |= element.styleAnimation.Start(StylePropertyId.MarginTop, ptr3.marginTop, ptr4.marginTop, durationMs, delayMs, easingCurve);
				}
				bool flag39 = ptr3.maxHeight != ptr4.maxHeight;
				if (flag39)
				{
					flag |= element.styleAnimation.Start(StylePropertyId.MaxHeight, ptr3.maxHeight, ptr4.maxHeight, durationMs, delayMs, easingCurve);
				}
				bool flag40 = ptr3.maxWidth != ptr4.maxWidth;
				if (flag40)
				{
					flag |= element.styleAnimation.Start(StylePropertyId.MaxWidth, ptr3.maxWidth, ptr4.maxWidth, durationMs, delayMs, easingCurve);
				}
				bool flag41 = ptr3.minHeight != ptr4.minHeight;
				if (flag41)
				{
					flag |= element.styleAnimation.Start(StylePropertyId.MinHeight, ptr3.minHeight, ptr4.minHeight, durationMs, delayMs, easingCurve);
				}
				bool flag42 = ptr3.minWidth != ptr4.minWidth;
				if (flag42)
				{
					flag |= element.styleAnimation.Start(StylePropertyId.MinWidth, ptr3.minWidth, ptr4.minWidth, durationMs, delayMs, easingCurve);
				}
				bool flag43 = ptr3.paddingBottom != ptr4.paddingBottom;
				if (flag43)
				{
					flag |= element.styleAnimation.Start(StylePropertyId.PaddingBottom, ptr3.paddingBottom, ptr4.paddingBottom, durationMs, delayMs, easingCurve);
				}
				bool flag44 = ptr3.paddingLeft != ptr4.paddingLeft;
				if (flag44)
				{
					flag |= element.styleAnimation.Start(StylePropertyId.PaddingLeft, ptr3.paddingLeft, ptr4.paddingLeft, durationMs, delayMs, easingCurve);
				}
				bool flag45 = ptr3.paddingRight != ptr4.paddingRight;
				if (flag45)
				{
					flag |= element.styleAnimation.Start(StylePropertyId.PaddingRight, ptr3.paddingRight, ptr4.paddingRight, durationMs, delayMs, easingCurve);
				}
				bool flag46 = ptr3.paddingTop != ptr4.paddingTop;
				if (flag46)
				{
					flag |= element.styleAnimation.Start(StylePropertyId.PaddingTop, ptr3.paddingTop, ptr4.paddingTop, durationMs, delayMs, easingCurve);
				}
				bool flag47 = ptr3.position != ptr4.position;
				if (flag47)
				{
					flag |= element.styleAnimation.StartEnum(StylePropertyId.Position, (int)ptr3.position, (int)ptr4.position, durationMs, delayMs, easingCurve);
				}
				bool flag48 = ptr3.right != ptr4.right;
				if (flag48)
				{
					flag |= element.styleAnimation.Start(StylePropertyId.Right, ptr3.right, ptr4.right, durationMs, delayMs, easingCurve);
				}
				bool flag49 = ptr3.top != ptr4.top;
				if (flag49)
				{
					flag |= element.styleAnimation.Start(StylePropertyId.Top, ptr3.top, ptr4.top, durationMs, delayMs, easingCurve);
				}
				bool flag50 = ptr3.width != ptr4.width;
				if (flag50)
				{
					flag |= element.styleAnimation.Start(StylePropertyId.Width, ptr3.width, ptr4.width, durationMs, delayMs, easingCurve);
				}
			}
			bool flag51 = !oldStyle.rareData.Equals(newStyle.rareData);
			if (flag51)
			{
				readonly ref RareData ptr5 = ref oldStyle.rareData.Read();
				readonly ref RareData ptr6 = ref newStyle.rareData.Read();
				bool flag52 = ptr5.textOverflow != ptr6.textOverflow;
				if (flag52)
				{
					flag |= element.styleAnimation.StartEnum(StylePropertyId.TextOverflow, (int)ptr5.textOverflow, (int)ptr6.textOverflow, durationMs, delayMs, easingCurve);
				}
				bool flag53 = ptr5.unityBackgroundImageTintColor != ptr6.unityBackgroundImageTintColor;
				if (flag53)
				{
					flag |= element.styleAnimation.Start(StylePropertyId.UnityBackgroundImageTintColor, ptr5.unityBackgroundImageTintColor, ptr6.unityBackgroundImageTintColor, durationMs, delayMs, easingCurve);
				}
				bool flag54 = ptr5.unityBackgroundScaleMode != ptr6.unityBackgroundScaleMode;
				if (flag54)
				{
					flag |= element.styleAnimation.StartEnum(StylePropertyId.UnityBackgroundScaleMode, (int)ptr5.unityBackgroundScaleMode, (int)ptr6.unityBackgroundScaleMode, durationMs, delayMs, easingCurve);
				}
				bool flag55 = ptr5.unityOverflowClipBox != ptr6.unityOverflowClipBox;
				if (flag55)
				{
					flag |= element.styleAnimation.StartEnum(StylePropertyId.UnityOverflowClipBox, (int)ptr5.unityOverflowClipBox, (int)ptr6.unityOverflowClipBox, durationMs, delayMs, easingCurve);
				}
				bool flag56 = ptr5.unitySliceBottom != ptr6.unitySliceBottom;
				if (flag56)
				{
					flag |= element.styleAnimation.Start(StylePropertyId.UnitySliceBottom, ptr5.unitySliceBottom, ptr6.unitySliceBottom, durationMs, delayMs, easingCurve);
				}
				bool flag57 = ptr5.unitySliceLeft != ptr6.unitySliceLeft;
				if (flag57)
				{
					flag |= element.styleAnimation.Start(StylePropertyId.UnitySliceLeft, ptr5.unitySliceLeft, ptr6.unitySliceLeft, durationMs, delayMs, easingCurve);
				}
				bool flag58 = ptr5.unitySliceRight != ptr6.unitySliceRight;
				if (flag58)
				{
					flag |= element.styleAnimation.Start(StylePropertyId.UnitySliceRight, ptr5.unitySliceRight, ptr6.unitySliceRight, durationMs, delayMs, easingCurve);
				}
				bool flag59 = ptr5.unitySliceTop != ptr6.unitySliceTop;
				if (flag59)
				{
					flag |= element.styleAnimation.Start(StylePropertyId.UnitySliceTop, ptr5.unitySliceTop, ptr6.unitySliceTop, durationMs, delayMs, easingCurve);
				}
				bool flag60 = ptr5.unityTextOverflowPosition != ptr6.unityTextOverflowPosition;
				if (flag60)
				{
					flag |= element.styleAnimation.StartEnum(StylePropertyId.UnityTextOverflowPosition, (int)ptr5.unityTextOverflowPosition, (int)ptr6.unityTextOverflowPosition, durationMs, delayMs, easingCurve);
				}
			}
			bool flag61 = !oldStyle.transformData.Equals(newStyle.transformData);
			if (flag61)
			{
				readonly ref TransformData ptr7 = ref oldStyle.transformData.Read();
				readonly ref TransformData ptr8 = ref newStyle.transformData.Read();
				bool flag62 = ptr7.rotate != ptr8.rotate;
				if (flag62)
				{
					flag |= element.styleAnimation.Start(StylePropertyId.Rotate, ptr7.rotate, ptr8.rotate, durationMs, delayMs, easingCurve);
				}
				bool flag63 = ptr7.scale != ptr8.scale;
				if (flag63)
				{
					flag |= element.styleAnimation.Start(StylePropertyId.Scale, ptr7.scale, ptr8.scale, durationMs, delayMs, easingCurve);
				}
				bool flag64 = ptr7.transformOrigin != ptr8.transformOrigin;
				if (flag64)
				{
					flag |= element.styleAnimation.Start(StylePropertyId.TransformOrigin, ptr7.transformOrigin, ptr8.transformOrigin, durationMs, delayMs, easingCurve);
				}
				bool flag65 = ptr7.translate != ptr8.translate;
				if (flag65)
				{
					flag |= element.styleAnimation.Start(StylePropertyId.Translate, ptr7.translate, ptr8.translate, durationMs, delayMs, easingCurve);
				}
			}
			bool flag66 = !oldStyle.visualData.Equals(newStyle.visualData);
			if (flag66)
			{
				readonly ref VisualData ptr9 = ref oldStyle.visualData.Read();
				readonly ref VisualData ptr10 = ref newStyle.visualData.Read();
				bool flag67 = ptr9.backgroundColor != ptr10.backgroundColor;
				if (flag67)
				{
					flag |= element.styleAnimation.Start(StylePropertyId.BackgroundColor, ptr9.backgroundColor, ptr10.backgroundColor, durationMs, delayMs, easingCurve);
				}
				bool flag68 = ptr9.backgroundImage != ptr10.backgroundImage;
				if (flag68)
				{
					flag |= element.styleAnimation.Start(StylePropertyId.BackgroundImage, ptr9.backgroundImage, ptr10.backgroundImage, durationMs, delayMs, easingCurve);
				}
				bool flag69 = ptr9.borderBottomColor != ptr10.borderBottomColor;
				if (flag69)
				{
					flag |= element.styleAnimation.Start(StylePropertyId.BorderBottomColor, ptr9.borderBottomColor, ptr10.borderBottomColor, durationMs, delayMs, easingCurve);
				}
				bool flag70 = ptr9.borderBottomLeftRadius != ptr10.borderBottomLeftRadius;
				if (flag70)
				{
					flag |= element.styleAnimation.Start(StylePropertyId.BorderBottomLeftRadius, ptr9.borderBottomLeftRadius, ptr10.borderBottomLeftRadius, durationMs, delayMs, easingCurve);
				}
				bool flag71 = ptr9.borderBottomRightRadius != ptr10.borderBottomRightRadius;
				if (flag71)
				{
					flag |= element.styleAnimation.Start(StylePropertyId.BorderBottomRightRadius, ptr9.borderBottomRightRadius, ptr10.borderBottomRightRadius, durationMs, delayMs, easingCurve);
				}
				bool flag72 = ptr9.borderLeftColor != ptr10.borderLeftColor;
				if (flag72)
				{
					flag |= element.styleAnimation.Start(StylePropertyId.BorderLeftColor, ptr9.borderLeftColor, ptr10.borderLeftColor, durationMs, delayMs, easingCurve);
				}
				bool flag73 = ptr9.borderRightColor != ptr10.borderRightColor;
				if (flag73)
				{
					flag |= element.styleAnimation.Start(StylePropertyId.BorderRightColor, ptr9.borderRightColor, ptr10.borderRightColor, durationMs, delayMs, easingCurve);
				}
				bool flag74 = ptr9.borderTopColor != ptr10.borderTopColor;
				if (flag74)
				{
					flag |= element.styleAnimation.Start(StylePropertyId.BorderTopColor, ptr9.borderTopColor, ptr10.borderTopColor, durationMs, delayMs, easingCurve);
				}
				bool flag75 = ptr9.borderTopLeftRadius != ptr10.borderTopLeftRadius;
				if (flag75)
				{
					flag |= element.styleAnimation.Start(StylePropertyId.BorderTopLeftRadius, ptr9.borderTopLeftRadius, ptr10.borderTopLeftRadius, durationMs, delayMs, easingCurve);
				}
				bool flag76 = ptr9.borderTopRightRadius != ptr10.borderTopRightRadius;
				if (flag76)
				{
					flag |= element.styleAnimation.Start(StylePropertyId.BorderTopRightRadius, ptr9.borderTopRightRadius, ptr10.borderTopRightRadius, durationMs, delayMs, easingCurve);
				}
				bool flag77 = ptr9.opacity != ptr10.opacity;
				if (flag77)
				{
					flag |= element.styleAnimation.Start(StylePropertyId.Opacity, ptr9.opacity, ptr10.opacity, durationMs, delayMs, easingCurve);
				}
				bool flag78 = ptr9.overflow != ptr10.overflow;
				if (flag78)
				{
					flag |= element.styleAnimation.StartEnum(StylePropertyId.Overflow, (int)ptr9.overflow, (int)ptr10.overflow, durationMs, delayMs, easingCurve);
				}
			}
			return flag;
		}

		// Token: 0x06001303 RID: 4867 RVA: 0x0004FC38 File Offset: 0x0004DE38
		public static bool StartAnimationInline(VisualElement element, StylePropertyId id, ref ComputedStyle computedStyle, StyleValue sv, int durationMs, int delayMs, Func<float, float> easingCurve)
		{
			if (id <= StylePropertyId.Width)
			{
				switch (id)
				{
				case StylePropertyId.Color:
				{
					Color color = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.color : sv.color);
					return element.styleAnimation.Start(StylePropertyId.Color, computedStyle.inheritedData.Read().color, color, durationMs, delayMs, easingCurve);
				}
				case StylePropertyId.FontSize:
				{
					Length length = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.fontSize : sv.length);
					return element.styleAnimation.Start(StylePropertyId.FontSize, computedStyle.inheritedData.Read().fontSize, length, durationMs, delayMs, easingCurve);
				}
				case StylePropertyId.LetterSpacing:
				{
					Length length2 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.letterSpacing : sv.length);
					return element.styleAnimation.Start(StylePropertyId.LetterSpacing, computedStyle.inheritedData.Read().letterSpacing, length2, durationMs, delayMs, easingCurve);
				}
				case StylePropertyId.TextShadow:
					break;
				case StylePropertyId.UnityFont:
				{
					Font font = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.unityFont : (sv.resource.IsAllocated ? (sv.resource.Target as Font) : null));
					return element.styleAnimation.Start(StylePropertyId.UnityFont, computedStyle.inheritedData.Read().unityFont, font, durationMs, delayMs, easingCurve);
				}
				case StylePropertyId.UnityFontDefinition:
				{
					FontDefinition fontDefinition = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.unityFontDefinition : (sv.resource.IsAllocated ? FontDefinition.FromObject(sv.resource.Target) : default(FontDefinition)));
					return element.styleAnimation.Start(StylePropertyId.UnityFontDefinition, computedStyle.inheritedData.Read().unityFontDefinition, fontDefinition, durationMs, delayMs, easingCurve);
				}
				case StylePropertyId.UnityFontStyleAndWeight:
				{
					FontStyle fontStyle = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.unityFontStyleAndWeight : ((FontStyle)sv.number));
					return element.styleAnimation.StartEnum(StylePropertyId.UnityFontStyleAndWeight, (int)computedStyle.inheritedData.Read().unityFontStyleAndWeight, (int)fontStyle, durationMs, delayMs, easingCurve);
				}
				case StylePropertyId.UnityParagraphSpacing:
				{
					Length length3 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.unityParagraphSpacing : sv.length);
					return element.styleAnimation.Start(StylePropertyId.UnityParagraphSpacing, computedStyle.inheritedData.Read().unityParagraphSpacing, length3, durationMs, delayMs, easingCurve);
				}
				case StylePropertyId.UnityTextAlign:
				{
					TextAnchor textAnchor = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.unityTextAlign : ((TextAnchor)sv.number));
					return element.styleAnimation.StartEnum(StylePropertyId.UnityTextAlign, (int)computedStyle.inheritedData.Read().unityTextAlign, (int)textAnchor, durationMs, delayMs, easingCurve);
				}
				case StylePropertyId.UnityTextOutlineColor:
				{
					Color color2 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.unityTextOutlineColor : sv.color);
					return element.styleAnimation.Start(StylePropertyId.UnityTextOutlineColor, computedStyle.inheritedData.Read().unityTextOutlineColor, color2, durationMs, delayMs, easingCurve);
				}
				case StylePropertyId.UnityTextOutlineWidth:
				{
					float num = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.unityTextOutlineWidth : sv.number);
					return element.styleAnimation.Start(StylePropertyId.UnityTextOutlineWidth, computedStyle.inheritedData.Read().unityTextOutlineWidth, num, durationMs, delayMs, easingCurve);
				}
				case StylePropertyId.Visibility:
				{
					Visibility visibility = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.visibility : ((Visibility)sv.number));
					return element.styleAnimation.StartEnum(StylePropertyId.Visibility, (int)computedStyle.inheritedData.Read().visibility, (int)visibility, durationMs, delayMs, easingCurve);
				}
				case StylePropertyId.WhiteSpace:
				{
					WhiteSpace whiteSpace = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.whiteSpace : ((WhiteSpace)sv.number));
					return element.styleAnimation.StartEnum(StylePropertyId.WhiteSpace, (int)computedStyle.inheritedData.Read().whiteSpace, (int)whiteSpace, durationMs, delayMs, easingCurve);
				}
				case StylePropertyId.WordSpacing:
				{
					Length length4 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.wordSpacing : sv.length);
					return element.styleAnimation.Start(StylePropertyId.WordSpacing, computedStyle.inheritedData.Read().wordSpacing, length4, durationMs, delayMs, easingCurve);
				}
				default:
					switch (id)
					{
					case StylePropertyId.AlignContent:
					{
						Align align = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.alignContent : ((Align)sv.number));
						bool flag = sv.keyword == StyleKeyword.Auto;
						if (flag)
						{
							align = Align.Auto;
						}
						return element.styleAnimation.StartEnum(StylePropertyId.AlignContent, (int)computedStyle.layoutData.Read().alignContent, (int)align, durationMs, delayMs, easingCurve);
					}
					case StylePropertyId.AlignItems:
					{
						Align align2 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.alignItems : ((Align)sv.number));
						bool flag2 = sv.keyword == StyleKeyword.Auto;
						if (flag2)
						{
							align2 = Align.Auto;
						}
						return element.styleAnimation.StartEnum(StylePropertyId.AlignItems, (int)computedStyle.layoutData.Read().alignItems, (int)align2, durationMs, delayMs, easingCurve);
					}
					case StylePropertyId.AlignSelf:
					{
						Align align3 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.alignSelf : ((Align)sv.number));
						bool flag3 = sv.keyword == StyleKeyword.Auto;
						if (flag3)
						{
							align3 = Align.Auto;
						}
						return element.styleAnimation.StartEnum(StylePropertyId.AlignSelf, (int)computedStyle.layoutData.Read().alignSelf, (int)align3, durationMs, delayMs, easingCurve);
					}
					case StylePropertyId.BorderBottomWidth:
					{
						float num2 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.borderBottomWidth : sv.number);
						return element.styleAnimation.Start(StylePropertyId.BorderBottomWidth, computedStyle.layoutData.Read().borderBottomWidth, num2, durationMs, delayMs, easingCurve);
					}
					case StylePropertyId.BorderLeftWidth:
					{
						float num3 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.borderLeftWidth : sv.number);
						return element.styleAnimation.Start(StylePropertyId.BorderLeftWidth, computedStyle.layoutData.Read().borderLeftWidth, num3, durationMs, delayMs, easingCurve);
					}
					case StylePropertyId.BorderRightWidth:
					{
						float num4 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.borderRightWidth : sv.number);
						return element.styleAnimation.Start(StylePropertyId.BorderRightWidth, computedStyle.layoutData.Read().borderRightWidth, num4, durationMs, delayMs, easingCurve);
					}
					case StylePropertyId.BorderTopWidth:
					{
						float num5 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.borderTopWidth : sv.number);
						return element.styleAnimation.Start(StylePropertyId.BorderTopWidth, computedStyle.layoutData.Read().borderTopWidth, num5, durationMs, delayMs, easingCurve);
					}
					case StylePropertyId.Bottom:
					{
						Length length5 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.bottom : sv.length);
						return element.styleAnimation.Start(StylePropertyId.Bottom, computedStyle.layoutData.Read().bottom, length5, durationMs, delayMs, easingCurve);
					}
					case StylePropertyId.Display:
					{
						DisplayStyle displayStyle = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.display : ((DisplayStyle)sv.number));
						bool flag4 = sv.keyword == StyleKeyword.None;
						if (flag4)
						{
							displayStyle = DisplayStyle.None;
						}
						return element.styleAnimation.StartEnum(StylePropertyId.Display, (int)computedStyle.layoutData.Read().display, (int)displayStyle, durationMs, delayMs, easingCurve);
					}
					case StylePropertyId.FlexBasis:
					{
						Length length6 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.flexBasis : sv.length);
						return element.styleAnimation.Start(StylePropertyId.FlexBasis, computedStyle.layoutData.Read().flexBasis, length6, durationMs, delayMs, easingCurve);
					}
					case StylePropertyId.FlexDirection:
					{
						FlexDirection flexDirection = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.flexDirection : ((FlexDirection)sv.number));
						return element.styleAnimation.StartEnum(StylePropertyId.FlexDirection, (int)computedStyle.layoutData.Read().flexDirection, (int)flexDirection, durationMs, delayMs, easingCurve);
					}
					case StylePropertyId.FlexGrow:
					{
						float num6 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.flexGrow : sv.number);
						return element.styleAnimation.Start(StylePropertyId.FlexGrow, computedStyle.layoutData.Read().flexGrow, num6, durationMs, delayMs, easingCurve);
					}
					case StylePropertyId.FlexShrink:
					{
						float num7 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.flexShrink : sv.number);
						return element.styleAnimation.Start(StylePropertyId.FlexShrink, computedStyle.layoutData.Read().flexShrink, num7, durationMs, delayMs, easingCurve);
					}
					case StylePropertyId.FlexWrap:
					{
						Wrap wrap = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.flexWrap : ((Wrap)sv.number));
						return element.styleAnimation.StartEnum(StylePropertyId.FlexWrap, (int)computedStyle.layoutData.Read().flexWrap, (int)wrap, durationMs, delayMs, easingCurve);
					}
					case StylePropertyId.Height:
					{
						Length length7 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.height : sv.length);
						return element.styleAnimation.Start(StylePropertyId.Height, computedStyle.layoutData.Read().height, length7, durationMs, delayMs, easingCurve);
					}
					case StylePropertyId.JustifyContent:
					{
						Justify justify = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.justifyContent : ((Justify)sv.number));
						return element.styleAnimation.StartEnum(StylePropertyId.JustifyContent, (int)computedStyle.layoutData.Read().justifyContent, (int)justify, durationMs, delayMs, easingCurve);
					}
					case StylePropertyId.Left:
					{
						Length length8 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.left : sv.length);
						return element.styleAnimation.Start(StylePropertyId.Left, computedStyle.layoutData.Read().left, length8, durationMs, delayMs, easingCurve);
					}
					case StylePropertyId.MarginBottom:
					{
						Length length9 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.marginBottom : sv.length);
						return element.styleAnimation.Start(StylePropertyId.MarginBottom, computedStyle.layoutData.Read().marginBottom, length9, durationMs, delayMs, easingCurve);
					}
					case StylePropertyId.MarginLeft:
					{
						Length length10 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.marginLeft : sv.length);
						return element.styleAnimation.Start(StylePropertyId.MarginLeft, computedStyle.layoutData.Read().marginLeft, length10, durationMs, delayMs, easingCurve);
					}
					case StylePropertyId.MarginRight:
					{
						Length length11 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.marginRight : sv.length);
						return element.styleAnimation.Start(StylePropertyId.MarginRight, computedStyle.layoutData.Read().marginRight, length11, durationMs, delayMs, easingCurve);
					}
					case StylePropertyId.MarginTop:
					{
						Length length12 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.marginTop : sv.length);
						return element.styleAnimation.Start(StylePropertyId.MarginTop, computedStyle.layoutData.Read().marginTop, length12, durationMs, delayMs, easingCurve);
					}
					case StylePropertyId.MaxHeight:
					{
						Length length13 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.maxHeight : sv.length);
						return element.styleAnimation.Start(StylePropertyId.MaxHeight, computedStyle.layoutData.Read().maxHeight, length13, durationMs, delayMs, easingCurve);
					}
					case StylePropertyId.MaxWidth:
					{
						Length length14 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.maxWidth : sv.length);
						return element.styleAnimation.Start(StylePropertyId.MaxWidth, computedStyle.layoutData.Read().maxWidth, length14, durationMs, delayMs, easingCurve);
					}
					case StylePropertyId.MinHeight:
					{
						Length length15 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.minHeight : sv.length);
						return element.styleAnimation.Start(StylePropertyId.MinHeight, computedStyle.layoutData.Read().minHeight, length15, durationMs, delayMs, easingCurve);
					}
					case StylePropertyId.MinWidth:
					{
						Length length16 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.minWidth : sv.length);
						return element.styleAnimation.Start(StylePropertyId.MinWidth, computedStyle.layoutData.Read().minWidth, length16, durationMs, delayMs, easingCurve);
					}
					case StylePropertyId.PaddingBottom:
					{
						Length length17 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.paddingBottom : sv.length);
						return element.styleAnimation.Start(StylePropertyId.PaddingBottom, computedStyle.layoutData.Read().paddingBottom, length17, durationMs, delayMs, easingCurve);
					}
					case StylePropertyId.PaddingLeft:
					{
						Length length18 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.paddingLeft : sv.length);
						return element.styleAnimation.Start(StylePropertyId.PaddingLeft, computedStyle.layoutData.Read().paddingLeft, length18, durationMs, delayMs, easingCurve);
					}
					case StylePropertyId.PaddingRight:
					{
						Length length19 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.paddingRight : sv.length);
						return element.styleAnimation.Start(StylePropertyId.PaddingRight, computedStyle.layoutData.Read().paddingRight, length19, durationMs, delayMs, easingCurve);
					}
					case StylePropertyId.PaddingTop:
					{
						Length length20 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.paddingTop : sv.length);
						return element.styleAnimation.Start(StylePropertyId.PaddingTop, computedStyle.layoutData.Read().paddingTop, length20, durationMs, delayMs, easingCurve);
					}
					case StylePropertyId.Position:
					{
						Position position = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.position : ((Position)sv.number));
						return element.styleAnimation.StartEnum(StylePropertyId.Position, (int)computedStyle.layoutData.Read().position, (int)position, durationMs, delayMs, easingCurve);
					}
					case StylePropertyId.Right:
					{
						Length length21 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.right : sv.length);
						return element.styleAnimation.Start(StylePropertyId.Right, computedStyle.layoutData.Read().right, length21, durationMs, delayMs, easingCurve);
					}
					case StylePropertyId.Top:
					{
						Length length22 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.top : sv.length);
						return element.styleAnimation.Start(StylePropertyId.Top, computedStyle.layoutData.Read().top, length22, durationMs, delayMs, easingCurve);
					}
					case StylePropertyId.Width:
					{
						Length length23 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.width : sv.length);
						return element.styleAnimation.Start(StylePropertyId.Width, computedStyle.layoutData.Read().width, length23, durationMs, delayMs, easingCurve);
					}
					}
					break;
				}
			}
			else
			{
				switch (id)
				{
				case StylePropertyId.TextOverflow:
				{
					TextOverflow textOverflow = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.textOverflow : ((TextOverflow)sv.number));
					return element.styleAnimation.StartEnum(StylePropertyId.TextOverflow, (int)computedStyle.rareData.Read().textOverflow, (int)textOverflow, durationMs, delayMs, easingCurve);
				}
				case StylePropertyId.UnityBackgroundImageTintColor:
				{
					Color color3 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.unityBackgroundImageTintColor : sv.color);
					return element.styleAnimation.Start(StylePropertyId.UnityBackgroundImageTintColor, computedStyle.rareData.Read().unityBackgroundImageTintColor, color3, durationMs, delayMs, easingCurve);
				}
				case StylePropertyId.UnityBackgroundScaleMode:
				{
					ScaleMode scaleMode = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.unityBackgroundScaleMode : ((ScaleMode)sv.number));
					return element.styleAnimation.StartEnum(StylePropertyId.UnityBackgroundScaleMode, (int)computedStyle.rareData.Read().unityBackgroundScaleMode, (int)scaleMode, durationMs, delayMs, easingCurve);
				}
				case StylePropertyId.UnityOverflowClipBox:
				{
					OverflowClipBox overflowClipBox = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.unityOverflowClipBox : ((OverflowClipBox)sv.number));
					return element.styleAnimation.StartEnum(StylePropertyId.UnityOverflowClipBox, (int)computedStyle.rareData.Read().unityOverflowClipBox, (int)overflowClipBox, durationMs, delayMs, easingCurve);
				}
				case StylePropertyId.UnitySliceBottom:
				{
					int num8 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.unitySliceBottom : ((int)sv.number));
					return element.styleAnimation.Start(StylePropertyId.UnitySliceBottom, computedStyle.rareData.Read().unitySliceBottom, num8, durationMs, delayMs, easingCurve);
				}
				case StylePropertyId.UnitySliceLeft:
				{
					int num9 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.unitySliceLeft : ((int)sv.number));
					return element.styleAnimation.Start(StylePropertyId.UnitySliceLeft, computedStyle.rareData.Read().unitySliceLeft, num9, durationMs, delayMs, easingCurve);
				}
				case StylePropertyId.UnitySliceRight:
				{
					int num10 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.unitySliceRight : ((int)sv.number));
					return element.styleAnimation.Start(StylePropertyId.UnitySliceRight, computedStyle.rareData.Read().unitySliceRight, num10, durationMs, delayMs, easingCurve);
				}
				case StylePropertyId.UnitySliceTop:
				{
					int num11 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.unitySliceTop : ((int)sv.number));
					return element.styleAnimation.Start(StylePropertyId.UnitySliceTop, computedStyle.rareData.Read().unitySliceTop, num11, durationMs, delayMs, easingCurve);
				}
				case StylePropertyId.UnityTextOverflowPosition:
				{
					TextOverflowPosition textOverflowPosition = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.unityTextOverflowPosition : ((TextOverflowPosition)sv.number));
					return element.styleAnimation.StartEnum(StylePropertyId.UnityTextOverflowPosition, (int)computedStyle.rareData.Read().unityTextOverflowPosition, (int)textOverflowPosition, durationMs, delayMs, easingCurve);
				}
				default:
					switch (id)
					{
					case StylePropertyId.BackgroundColor:
					{
						Color color4 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.backgroundColor : sv.color);
						return element.styleAnimation.Start(StylePropertyId.BackgroundColor, computedStyle.visualData.Read().backgroundColor, color4, durationMs, delayMs, easingCurve);
					}
					case StylePropertyId.BackgroundImage:
					{
						Background background = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.backgroundImage : (sv.resource.IsAllocated ? Background.FromObject(sv.resource.Target) : default(Background)));
						return element.styleAnimation.Start(StylePropertyId.BackgroundImage, computedStyle.visualData.Read().backgroundImage, background, durationMs, delayMs, easingCurve);
					}
					case StylePropertyId.BorderBottomColor:
					{
						Color color5 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.borderBottomColor : sv.color);
						return element.styleAnimation.Start(StylePropertyId.BorderBottomColor, computedStyle.visualData.Read().borderBottomColor, color5, durationMs, delayMs, easingCurve);
					}
					case StylePropertyId.BorderBottomLeftRadius:
					{
						Length length24 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.borderBottomLeftRadius : sv.length);
						return element.styleAnimation.Start(StylePropertyId.BorderBottomLeftRadius, computedStyle.visualData.Read().borderBottomLeftRadius, length24, durationMs, delayMs, easingCurve);
					}
					case StylePropertyId.BorderBottomRightRadius:
					{
						Length length25 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.borderBottomRightRadius : sv.length);
						return element.styleAnimation.Start(StylePropertyId.BorderBottomRightRadius, computedStyle.visualData.Read().borderBottomRightRadius, length25, durationMs, delayMs, easingCurve);
					}
					case StylePropertyId.BorderLeftColor:
					{
						Color color6 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.borderLeftColor : sv.color);
						return element.styleAnimation.Start(StylePropertyId.BorderLeftColor, computedStyle.visualData.Read().borderLeftColor, color6, durationMs, delayMs, easingCurve);
					}
					case StylePropertyId.BorderRightColor:
					{
						Color color7 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.borderRightColor : sv.color);
						return element.styleAnimation.Start(StylePropertyId.BorderRightColor, computedStyle.visualData.Read().borderRightColor, color7, durationMs, delayMs, easingCurve);
					}
					case StylePropertyId.BorderTopColor:
					{
						Color color8 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.borderTopColor : sv.color);
						return element.styleAnimation.Start(StylePropertyId.BorderTopColor, computedStyle.visualData.Read().borderTopColor, color8, durationMs, delayMs, easingCurve);
					}
					case StylePropertyId.BorderTopLeftRadius:
					{
						Length length26 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.borderTopLeftRadius : sv.length);
						return element.styleAnimation.Start(StylePropertyId.BorderTopLeftRadius, computedStyle.visualData.Read().borderTopLeftRadius, length26, durationMs, delayMs, easingCurve);
					}
					case StylePropertyId.BorderTopRightRadius:
					{
						Length length27 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.borderTopRightRadius : sv.length);
						return element.styleAnimation.Start(StylePropertyId.BorderTopRightRadius, computedStyle.visualData.Read().borderTopRightRadius, length27, durationMs, delayMs, easingCurve);
					}
					case StylePropertyId.Opacity:
					{
						float num12 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.opacity : sv.number);
						return element.styleAnimation.Start(StylePropertyId.Opacity, computedStyle.visualData.Read().opacity, num12, durationMs, delayMs, easingCurve);
					}
					case StylePropertyId.Overflow:
					{
						OverflowInternal overflowInternal = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.overflow : ((OverflowInternal)sv.number));
						return element.styleAnimation.StartEnum(StylePropertyId.Overflow, (int)computedStyle.visualData.Read().overflow, (int)overflowInternal, durationMs, delayMs, easingCurve);
					}
					}
					break;
				}
			}
			return false;
		}

		// Token: 0x06001304 RID: 4868 RVA: 0x0005113A File Offset: 0x0004F33A
		public void ApplyStyleTransformOrigin(TransformOrigin st)
		{
			this.transformData.Write().transformOrigin = st;
		}

		// Token: 0x06001305 RID: 4869 RVA: 0x0005114E File Offset: 0x0004F34E
		public void ApplyStyleTranslate(Translate translateValue)
		{
			this.transformData.Write().translate = translateValue;
		}

		// Token: 0x06001306 RID: 4870 RVA: 0x00051162 File Offset: 0x0004F362
		public void ApplyStyleRotate(Rotate rotateValue)
		{
			this.transformData.Write().rotate = rotateValue;
		}

		// Token: 0x06001307 RID: 4871 RVA: 0x00051176 File Offset: 0x0004F376
		public void ApplyStyleScale(Scale scaleValue)
		{
			this.transformData.Write().scale = scaleValue;
		}

		// Token: 0x06001308 RID: 4872 RVA: 0x0005118C File Offset: 0x0004F38C
		public void ApplyInitialValue(StylePropertyReader reader)
		{
			StylePropertyId propertyId = reader.propertyId;
			StylePropertyId stylePropertyId = propertyId;
			if (stylePropertyId != StylePropertyId.Custom)
			{
				if (stylePropertyId != StylePropertyId.All)
				{
					this.ApplyInitialValue(reader.propertyId);
				}
				else
				{
					this.ApplyAllPropertyInitial();
				}
			}
			else
			{
				this.RemoveCustomStyleProperty(reader);
			}
		}

		// Token: 0x06001309 RID: 4873 RVA: 0x000511D8 File Offset: 0x0004F3D8
		public void ApplyInitialValue(StylePropertyId id)
		{
			if (id <= StylePropertyId.UnityTextOverflowPosition)
			{
				switch (id)
				{
				case StylePropertyId.Color:
					this.inheritedData.Write().color = InitialStyle.color;
					return;
				case StylePropertyId.FontSize:
					this.inheritedData.Write().fontSize = InitialStyle.fontSize;
					return;
				case StylePropertyId.LetterSpacing:
					this.inheritedData.Write().letterSpacing = InitialStyle.letterSpacing;
					return;
				case StylePropertyId.TextShadow:
					this.inheritedData.Write().textShadow = InitialStyle.textShadow;
					return;
				case StylePropertyId.UnityFont:
					this.inheritedData.Write().unityFont = InitialStyle.unityFont;
					return;
				case StylePropertyId.UnityFontDefinition:
					this.inheritedData.Write().unityFontDefinition = InitialStyle.unityFontDefinition;
					return;
				case StylePropertyId.UnityFontStyleAndWeight:
					this.inheritedData.Write().unityFontStyleAndWeight = InitialStyle.unityFontStyleAndWeight;
					return;
				case StylePropertyId.UnityParagraphSpacing:
					this.inheritedData.Write().unityParagraphSpacing = InitialStyle.unityParagraphSpacing;
					return;
				case StylePropertyId.UnityTextAlign:
					this.inheritedData.Write().unityTextAlign = InitialStyle.unityTextAlign;
					return;
				case StylePropertyId.UnityTextOutlineColor:
					this.inheritedData.Write().unityTextOutlineColor = InitialStyle.unityTextOutlineColor;
					return;
				case StylePropertyId.UnityTextOutlineWidth:
					this.inheritedData.Write().unityTextOutlineWidth = InitialStyle.unityTextOutlineWidth;
					return;
				case StylePropertyId.Visibility:
					this.inheritedData.Write().visibility = InitialStyle.visibility;
					return;
				case StylePropertyId.WhiteSpace:
					this.inheritedData.Write().whiteSpace = InitialStyle.whiteSpace;
					return;
				case StylePropertyId.WordSpacing:
					this.inheritedData.Write().wordSpacing = InitialStyle.wordSpacing;
					return;
				default:
					switch (id)
					{
					case StylePropertyId.AlignContent:
						this.layoutData.Write().alignContent = InitialStyle.alignContent;
						return;
					case StylePropertyId.AlignItems:
						this.layoutData.Write().alignItems = InitialStyle.alignItems;
						return;
					case StylePropertyId.AlignSelf:
						this.layoutData.Write().alignSelf = InitialStyle.alignSelf;
						return;
					case StylePropertyId.BorderBottomWidth:
						this.layoutData.Write().borderBottomWidth = InitialStyle.borderBottomWidth;
						return;
					case StylePropertyId.BorderLeftWidth:
						this.layoutData.Write().borderLeftWidth = InitialStyle.borderLeftWidth;
						return;
					case StylePropertyId.BorderRightWidth:
						this.layoutData.Write().borderRightWidth = InitialStyle.borderRightWidth;
						return;
					case StylePropertyId.BorderTopWidth:
						this.layoutData.Write().borderTopWidth = InitialStyle.borderTopWidth;
						return;
					case StylePropertyId.Bottom:
						this.layoutData.Write().bottom = InitialStyle.bottom;
						return;
					case StylePropertyId.Display:
						this.layoutData.Write().display = InitialStyle.display;
						return;
					case StylePropertyId.FlexBasis:
						this.layoutData.Write().flexBasis = InitialStyle.flexBasis;
						return;
					case StylePropertyId.FlexDirection:
						this.layoutData.Write().flexDirection = InitialStyle.flexDirection;
						return;
					case StylePropertyId.FlexGrow:
						this.layoutData.Write().flexGrow = InitialStyle.flexGrow;
						return;
					case StylePropertyId.FlexShrink:
						this.layoutData.Write().flexShrink = InitialStyle.flexShrink;
						return;
					case StylePropertyId.FlexWrap:
						this.layoutData.Write().flexWrap = InitialStyle.flexWrap;
						return;
					case StylePropertyId.Height:
						this.layoutData.Write().height = InitialStyle.height;
						return;
					case StylePropertyId.JustifyContent:
						this.layoutData.Write().justifyContent = InitialStyle.justifyContent;
						return;
					case StylePropertyId.Left:
						this.layoutData.Write().left = InitialStyle.left;
						return;
					case StylePropertyId.MarginBottom:
						this.layoutData.Write().marginBottom = InitialStyle.marginBottom;
						return;
					case StylePropertyId.MarginLeft:
						this.layoutData.Write().marginLeft = InitialStyle.marginLeft;
						return;
					case StylePropertyId.MarginRight:
						this.layoutData.Write().marginRight = InitialStyle.marginRight;
						return;
					case StylePropertyId.MarginTop:
						this.layoutData.Write().marginTop = InitialStyle.marginTop;
						return;
					case StylePropertyId.MaxHeight:
						this.layoutData.Write().maxHeight = InitialStyle.maxHeight;
						return;
					case StylePropertyId.MaxWidth:
						this.layoutData.Write().maxWidth = InitialStyle.maxWidth;
						return;
					case StylePropertyId.MinHeight:
						this.layoutData.Write().minHeight = InitialStyle.minHeight;
						return;
					case StylePropertyId.MinWidth:
						this.layoutData.Write().minWidth = InitialStyle.minWidth;
						return;
					case StylePropertyId.PaddingBottom:
						this.layoutData.Write().paddingBottom = InitialStyle.paddingBottom;
						return;
					case StylePropertyId.PaddingLeft:
						this.layoutData.Write().paddingLeft = InitialStyle.paddingLeft;
						return;
					case StylePropertyId.PaddingRight:
						this.layoutData.Write().paddingRight = InitialStyle.paddingRight;
						return;
					case StylePropertyId.PaddingTop:
						this.layoutData.Write().paddingTop = InitialStyle.paddingTop;
						return;
					case StylePropertyId.Position:
						this.layoutData.Write().position = InitialStyle.position;
						return;
					case StylePropertyId.Right:
						this.layoutData.Write().right = InitialStyle.right;
						return;
					case StylePropertyId.Top:
						this.layoutData.Write().top = InitialStyle.top;
						return;
					case StylePropertyId.Width:
						this.layoutData.Write().width = InitialStyle.width;
						return;
					default:
						switch (id)
						{
						case StylePropertyId.Cursor:
							this.rareData.Write().cursor = InitialStyle.cursor;
							return;
						case StylePropertyId.TextOverflow:
							this.rareData.Write().textOverflow = InitialStyle.textOverflow;
							return;
						case StylePropertyId.UnityBackgroundImageTintColor:
							this.rareData.Write().unityBackgroundImageTintColor = InitialStyle.unityBackgroundImageTintColor;
							return;
						case StylePropertyId.UnityBackgroundScaleMode:
							this.rareData.Write().unityBackgroundScaleMode = InitialStyle.unityBackgroundScaleMode;
							return;
						case StylePropertyId.UnityOverflowClipBox:
							this.rareData.Write().unityOverflowClipBox = InitialStyle.unityOverflowClipBox;
							return;
						case StylePropertyId.UnitySliceBottom:
							this.rareData.Write().unitySliceBottom = InitialStyle.unitySliceBottom;
							return;
						case StylePropertyId.UnitySliceLeft:
							this.rareData.Write().unitySliceLeft = InitialStyle.unitySliceLeft;
							return;
						case StylePropertyId.UnitySliceRight:
							this.rareData.Write().unitySliceRight = InitialStyle.unitySliceRight;
							return;
						case StylePropertyId.UnitySliceTop:
							this.rareData.Write().unitySliceTop = InitialStyle.unitySliceTop;
							return;
						case StylePropertyId.UnityTextOverflowPosition:
							this.rareData.Write().unityTextOverflowPosition = InitialStyle.unityTextOverflowPosition;
							return;
						}
						break;
					}
					break;
				}
			}
			else if (id <= StylePropertyId.Translate)
			{
				switch (id)
				{
				case StylePropertyId.All:
					return;
				case StylePropertyId.BorderColor:
					this.visualData.Write().borderTopColor = InitialStyle.borderTopColor;
					this.visualData.Write().borderRightColor = InitialStyle.borderRightColor;
					this.visualData.Write().borderBottomColor = InitialStyle.borderBottomColor;
					this.visualData.Write().borderLeftColor = InitialStyle.borderLeftColor;
					return;
				case StylePropertyId.BorderRadius:
					this.visualData.Write().borderTopLeftRadius = InitialStyle.borderTopLeftRadius;
					this.visualData.Write().borderTopRightRadius = InitialStyle.borderTopRightRadius;
					this.visualData.Write().borderBottomRightRadius = InitialStyle.borderBottomRightRadius;
					this.visualData.Write().borderBottomLeftRadius = InitialStyle.borderBottomLeftRadius;
					return;
				case StylePropertyId.BorderWidth:
					this.layoutData.Write().borderTopWidth = InitialStyle.borderTopWidth;
					this.layoutData.Write().borderRightWidth = InitialStyle.borderRightWidth;
					this.layoutData.Write().borderBottomWidth = InitialStyle.borderBottomWidth;
					this.layoutData.Write().borderLeftWidth = InitialStyle.borderLeftWidth;
					return;
				case StylePropertyId.Flex:
					this.layoutData.Write().flexGrow = InitialStyle.flexGrow;
					this.layoutData.Write().flexShrink = InitialStyle.flexShrink;
					this.layoutData.Write().flexBasis = InitialStyle.flexBasis;
					return;
				case StylePropertyId.Margin:
					this.layoutData.Write().marginTop = InitialStyle.marginTop;
					this.layoutData.Write().marginRight = InitialStyle.marginRight;
					this.layoutData.Write().marginBottom = InitialStyle.marginBottom;
					this.layoutData.Write().marginLeft = InitialStyle.marginLeft;
					return;
				case StylePropertyId.Padding:
					this.layoutData.Write().paddingTop = InitialStyle.paddingTop;
					this.layoutData.Write().paddingRight = InitialStyle.paddingRight;
					this.layoutData.Write().paddingBottom = InitialStyle.paddingBottom;
					this.layoutData.Write().paddingLeft = InitialStyle.paddingLeft;
					return;
				case StylePropertyId.Transition:
					this.transitionData.Write().transitionDelay.CopyFrom(InitialStyle.transitionDelay);
					this.transitionData.Write().transitionDuration.CopyFrom(InitialStyle.transitionDuration);
					this.transitionData.Write().transitionProperty.CopyFrom(InitialStyle.transitionProperty);
					this.transitionData.Write().transitionTimingFunction.CopyFrom(InitialStyle.transitionTimingFunction);
					this.ResetComputedTransitions();
					return;
				case StylePropertyId.UnityTextOutline:
					this.inheritedData.Write().unityTextOutlineColor = InitialStyle.unityTextOutlineColor;
					this.inheritedData.Write().unityTextOutlineWidth = InitialStyle.unityTextOutlineWidth;
					return;
				default:
					switch (id)
					{
					case StylePropertyId.Rotate:
						this.transformData.Write().rotate = InitialStyle.rotate;
						return;
					case StylePropertyId.Scale:
						this.transformData.Write().scale = InitialStyle.scale;
						return;
					case StylePropertyId.TransformOrigin:
						this.transformData.Write().transformOrigin = InitialStyle.transformOrigin;
						return;
					case StylePropertyId.Translate:
						this.transformData.Write().translate = InitialStyle.translate;
						return;
					}
					break;
				}
			}
			else
			{
				switch (id)
				{
				case StylePropertyId.TransitionDelay:
					this.transitionData.Write().transitionDelay.CopyFrom(InitialStyle.transitionDelay);
					this.ResetComputedTransitions();
					return;
				case StylePropertyId.TransitionDuration:
					this.transitionData.Write().transitionDuration.CopyFrom(InitialStyle.transitionDuration);
					this.ResetComputedTransitions();
					return;
				case StylePropertyId.TransitionProperty:
					this.transitionData.Write().transitionProperty.CopyFrom(InitialStyle.transitionProperty);
					this.ResetComputedTransitions();
					return;
				case StylePropertyId.TransitionTimingFunction:
					this.transitionData.Write().transitionTimingFunction.CopyFrom(InitialStyle.transitionTimingFunction);
					this.ResetComputedTransitions();
					return;
				default:
					switch (id)
					{
					case StylePropertyId.BackgroundColor:
						this.visualData.Write().backgroundColor = InitialStyle.backgroundColor;
						return;
					case StylePropertyId.BackgroundImage:
						this.visualData.Write().backgroundImage = InitialStyle.backgroundImage;
						return;
					case StylePropertyId.BorderBottomColor:
						this.visualData.Write().borderBottomColor = InitialStyle.borderBottomColor;
						return;
					case StylePropertyId.BorderBottomLeftRadius:
						this.visualData.Write().borderBottomLeftRadius = InitialStyle.borderBottomLeftRadius;
						return;
					case StylePropertyId.BorderBottomRightRadius:
						this.visualData.Write().borderBottomRightRadius = InitialStyle.borderBottomRightRadius;
						return;
					case StylePropertyId.BorderLeftColor:
						this.visualData.Write().borderLeftColor = InitialStyle.borderLeftColor;
						return;
					case StylePropertyId.BorderRightColor:
						this.visualData.Write().borderRightColor = InitialStyle.borderRightColor;
						return;
					case StylePropertyId.BorderTopColor:
						this.visualData.Write().borderTopColor = InitialStyle.borderTopColor;
						return;
					case StylePropertyId.BorderTopLeftRadius:
						this.visualData.Write().borderTopLeftRadius = InitialStyle.borderTopLeftRadius;
						return;
					case StylePropertyId.BorderTopRightRadius:
						this.visualData.Write().borderTopRightRadius = InitialStyle.borderTopRightRadius;
						return;
					case StylePropertyId.Opacity:
						this.visualData.Write().opacity = InitialStyle.opacity;
						return;
					case StylePropertyId.Overflow:
						this.visualData.Write().overflow = InitialStyle.overflow;
						return;
					}
					break;
				}
			}
			Debug.LogAssertion(string.Format("Unexpected property id {0}", id));
		}

		// Token: 0x0600130A RID: 4874 RVA: 0x00051E7C File Offset: 0x0005007C
		public void ApplyUnsetValue(StylePropertyReader reader, ref ComputedStyle parentStyle)
		{
			StylePropertyId propertyId = reader.propertyId;
			StylePropertyId stylePropertyId = propertyId;
			if (stylePropertyId != StylePropertyId.Custom)
			{
				this.ApplyUnsetValue(reader.propertyId, ref parentStyle);
			}
			else
			{
				this.RemoveCustomStyleProperty(reader);
			}
		}

		// Token: 0x0600130B RID: 4875 RVA: 0x00051EB4 File Offset: 0x000500B4
		public void ApplyUnsetValue(StylePropertyId id, ref ComputedStyle parentStyle)
		{
			switch (id)
			{
			case StylePropertyId.Color:
				this.inheritedData.Write().color = parentStyle.color;
				break;
			case StylePropertyId.FontSize:
				this.inheritedData.Write().fontSize = parentStyle.fontSize;
				break;
			case StylePropertyId.LetterSpacing:
				this.inheritedData.Write().letterSpacing = parentStyle.letterSpacing;
				break;
			case StylePropertyId.TextShadow:
				this.inheritedData.Write().textShadow = parentStyle.textShadow;
				break;
			case StylePropertyId.UnityFont:
				this.inheritedData.Write().unityFont = parentStyle.unityFont;
				break;
			case StylePropertyId.UnityFontDefinition:
				this.inheritedData.Write().unityFontDefinition = parentStyle.unityFontDefinition;
				break;
			case StylePropertyId.UnityFontStyleAndWeight:
				this.inheritedData.Write().unityFontStyleAndWeight = parentStyle.unityFontStyleAndWeight;
				break;
			case StylePropertyId.UnityParagraphSpacing:
				this.inheritedData.Write().unityParagraphSpacing = parentStyle.unityParagraphSpacing;
				break;
			case StylePropertyId.UnityTextAlign:
				this.inheritedData.Write().unityTextAlign = parentStyle.unityTextAlign;
				break;
			case StylePropertyId.UnityTextOutlineColor:
				this.inheritedData.Write().unityTextOutlineColor = parentStyle.unityTextOutlineColor;
				break;
			case StylePropertyId.UnityTextOutlineWidth:
				this.inheritedData.Write().unityTextOutlineWidth = parentStyle.unityTextOutlineWidth;
				break;
			case StylePropertyId.Visibility:
				this.inheritedData.Write().visibility = parentStyle.visibility;
				break;
			case StylePropertyId.WhiteSpace:
				this.inheritedData.Write().whiteSpace = parentStyle.whiteSpace;
				break;
			case StylePropertyId.WordSpacing:
				this.inheritedData.Write().wordSpacing = parentStyle.wordSpacing;
				break;
			default:
				this.ApplyInitialValue(id);
				break;
			}
		}

		// Token: 0x0400088F RID: 2191
		public StyleDataRef<InheritedData> inheritedData;

		// Token: 0x04000890 RID: 2192
		public StyleDataRef<LayoutData> layoutData;

		// Token: 0x04000891 RID: 2193
		public StyleDataRef<RareData> rareData;

		// Token: 0x04000892 RID: 2194
		public StyleDataRef<TransformData> transformData;

		// Token: 0x04000893 RID: 2195
		public StyleDataRef<TransitionData> transitionData;

		// Token: 0x04000894 RID: 2196
		public StyleDataRef<VisualData> visualData;

		// Token: 0x04000895 RID: 2197
		public YogaNode yogaNode;

		// Token: 0x04000896 RID: 2198
		public Dictionary<string, StylePropertyValue> customProperties;

		// Token: 0x04000897 RID: 2199
		public long matchingRulesHash;

		// Token: 0x04000898 RID: 2200
		public float dpiScaling;

		// Token: 0x04000899 RID: 2201
		public ComputedTransitionProperty[] computedTransitions;
	}
}
