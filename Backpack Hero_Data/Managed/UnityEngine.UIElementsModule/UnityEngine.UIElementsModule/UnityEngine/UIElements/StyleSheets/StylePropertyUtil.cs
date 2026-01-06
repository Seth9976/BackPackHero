using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements.StyleSheets
{
	// Token: 0x0200034F RID: 847
	internal static class StylePropertyUtil
	{
		// Token: 0x06001B10 RID: 6928 RVA: 0x0007A918 File Offset: 0x00078B18
		static StylePropertyUtil()
		{
			Dictionary<string, StylePropertyId> dictionary = new Dictionary<string, StylePropertyId>();
			dictionary.Add("align-content", StylePropertyId.AlignContent);
			dictionary.Add("align-items", StylePropertyId.AlignItems);
			dictionary.Add("align-self", StylePropertyId.AlignSelf);
			dictionary.Add("all", StylePropertyId.All);
			dictionary.Add("background-color", StylePropertyId.BackgroundColor);
			dictionary.Add("background-image", StylePropertyId.BackgroundImage);
			dictionary.Add("border-bottom-color", StylePropertyId.BorderBottomColor);
			dictionary.Add("border-bottom-left-radius", StylePropertyId.BorderBottomLeftRadius);
			dictionary.Add("border-bottom-right-radius", StylePropertyId.BorderBottomRightRadius);
			dictionary.Add("border-bottom-width", StylePropertyId.BorderBottomWidth);
			dictionary.Add("border-color", StylePropertyId.BorderColor);
			dictionary.Add("border-left-color", StylePropertyId.BorderLeftColor);
			dictionary.Add("border-left-width", StylePropertyId.BorderLeftWidth);
			dictionary.Add("border-radius", StylePropertyId.BorderRadius);
			dictionary.Add("border-right-color", StylePropertyId.BorderRightColor);
			dictionary.Add("border-right-width", StylePropertyId.BorderRightWidth);
			dictionary.Add("border-top-color", StylePropertyId.BorderTopColor);
			dictionary.Add("border-top-left-radius", StylePropertyId.BorderTopLeftRadius);
			dictionary.Add("border-top-right-radius", StylePropertyId.BorderTopRightRadius);
			dictionary.Add("border-top-width", StylePropertyId.BorderTopWidth);
			dictionary.Add("border-width", StylePropertyId.BorderWidth);
			dictionary.Add("bottom", StylePropertyId.Bottom);
			dictionary.Add("color", StylePropertyId.Color);
			dictionary.Add("cursor", StylePropertyId.Cursor);
			dictionary.Add("display", StylePropertyId.Display);
			dictionary.Add("flex", StylePropertyId.Flex);
			dictionary.Add("flex-basis", StylePropertyId.FlexBasis);
			dictionary.Add("flex-direction", StylePropertyId.FlexDirection);
			dictionary.Add("flex-grow", StylePropertyId.FlexGrow);
			dictionary.Add("flex-shrink", StylePropertyId.FlexShrink);
			dictionary.Add("flex-wrap", StylePropertyId.FlexWrap);
			dictionary.Add("font-size", StylePropertyId.FontSize);
			dictionary.Add("height", StylePropertyId.Height);
			dictionary.Add("justify-content", StylePropertyId.JustifyContent);
			dictionary.Add("left", StylePropertyId.Left);
			dictionary.Add("letter-spacing", StylePropertyId.LetterSpacing);
			dictionary.Add("margin", StylePropertyId.Margin);
			dictionary.Add("margin-bottom", StylePropertyId.MarginBottom);
			dictionary.Add("margin-left", StylePropertyId.MarginLeft);
			dictionary.Add("margin-right", StylePropertyId.MarginRight);
			dictionary.Add("margin-top", StylePropertyId.MarginTop);
			dictionary.Add("max-height", StylePropertyId.MaxHeight);
			dictionary.Add("max-width", StylePropertyId.MaxWidth);
			dictionary.Add("min-height", StylePropertyId.MinHeight);
			dictionary.Add("min-width", StylePropertyId.MinWidth);
			dictionary.Add("opacity", StylePropertyId.Opacity);
			dictionary.Add("overflow", StylePropertyId.Overflow);
			dictionary.Add("padding", StylePropertyId.Padding);
			dictionary.Add("padding-bottom", StylePropertyId.PaddingBottom);
			dictionary.Add("padding-left", StylePropertyId.PaddingLeft);
			dictionary.Add("padding-right", StylePropertyId.PaddingRight);
			dictionary.Add("padding-top", StylePropertyId.PaddingTop);
			dictionary.Add("position", StylePropertyId.Position);
			dictionary.Add("right", StylePropertyId.Right);
			dictionary.Add("rotate", StylePropertyId.Rotate);
			dictionary.Add("scale", StylePropertyId.Scale);
			dictionary.Add("text-overflow", StylePropertyId.TextOverflow);
			dictionary.Add("text-shadow", StylePropertyId.TextShadow);
			dictionary.Add("top", StylePropertyId.Top);
			dictionary.Add("transform-origin", StylePropertyId.TransformOrigin);
			dictionary.Add("transition", StylePropertyId.Transition);
			dictionary.Add("transition-delay", StylePropertyId.TransitionDelay);
			dictionary.Add("transition-duration", StylePropertyId.TransitionDuration);
			dictionary.Add("transition-property", StylePropertyId.TransitionProperty);
			dictionary.Add("transition-timing-function", StylePropertyId.TransitionTimingFunction);
			dictionary.Add("translate", StylePropertyId.Translate);
			dictionary.Add("-unity-background-image-tint-color", StylePropertyId.UnityBackgroundImageTintColor);
			dictionary.Add("-unity-background-scale-mode", StylePropertyId.UnityBackgroundScaleMode);
			dictionary.Add("-unity-font", StylePropertyId.UnityFont);
			dictionary.Add("-unity-font-definition", StylePropertyId.UnityFontDefinition);
			dictionary.Add("-unity-font-style", StylePropertyId.UnityFontStyleAndWeight);
			dictionary.Add("-unity-overflow-clip-box", StylePropertyId.UnityOverflowClipBox);
			dictionary.Add("-unity-paragraph-spacing", StylePropertyId.UnityParagraphSpacing);
			dictionary.Add("-unity-slice-bottom", StylePropertyId.UnitySliceBottom);
			dictionary.Add("-unity-slice-left", StylePropertyId.UnitySliceLeft);
			dictionary.Add("-unity-slice-right", StylePropertyId.UnitySliceRight);
			dictionary.Add("-unity-slice-top", StylePropertyId.UnitySliceTop);
			dictionary.Add("-unity-text-align", StylePropertyId.UnityTextAlign);
			dictionary.Add("-unity-text-outline", StylePropertyId.UnityTextOutline);
			dictionary.Add("-unity-text-outline-color", StylePropertyId.UnityTextOutlineColor);
			dictionary.Add("-unity-text-outline-width", StylePropertyId.UnityTextOutlineWidth);
			dictionary.Add("-unity-text-overflow-position", StylePropertyId.UnityTextOverflowPosition);
			dictionary.Add("visibility", StylePropertyId.Visibility);
			dictionary.Add("white-space", StylePropertyId.WhiteSpace);
			dictionary.Add("width", StylePropertyId.Width);
			dictionary.Add("word-spacing", StylePropertyId.WordSpacing);
			StylePropertyUtil.s_NameToId = dictionary;
			Dictionary<StylePropertyId, string> dictionary2 = new Dictionary<StylePropertyId, string>();
			dictionary2.Add(StylePropertyId.AlignContent, "align-content");
			dictionary2.Add(StylePropertyId.AlignItems, "align-items");
			dictionary2.Add(StylePropertyId.AlignSelf, "align-self");
			dictionary2.Add(StylePropertyId.All, "all");
			dictionary2.Add(StylePropertyId.BackgroundColor, "background-color");
			dictionary2.Add(StylePropertyId.BackgroundImage, "background-image");
			dictionary2.Add(StylePropertyId.BorderBottomColor, "border-bottom-color");
			dictionary2.Add(StylePropertyId.BorderBottomLeftRadius, "border-bottom-left-radius");
			dictionary2.Add(StylePropertyId.BorderBottomRightRadius, "border-bottom-right-radius");
			dictionary2.Add(StylePropertyId.BorderBottomWidth, "border-bottom-width");
			dictionary2.Add(StylePropertyId.BorderColor, "border-color");
			dictionary2.Add(StylePropertyId.BorderLeftColor, "border-left-color");
			dictionary2.Add(StylePropertyId.BorderLeftWidth, "border-left-width");
			dictionary2.Add(StylePropertyId.BorderRadius, "border-radius");
			dictionary2.Add(StylePropertyId.BorderRightColor, "border-right-color");
			dictionary2.Add(StylePropertyId.BorderRightWidth, "border-right-width");
			dictionary2.Add(StylePropertyId.BorderTopColor, "border-top-color");
			dictionary2.Add(StylePropertyId.BorderTopLeftRadius, "border-top-left-radius");
			dictionary2.Add(StylePropertyId.BorderTopRightRadius, "border-top-right-radius");
			dictionary2.Add(StylePropertyId.BorderTopWidth, "border-top-width");
			dictionary2.Add(StylePropertyId.BorderWidth, "border-width");
			dictionary2.Add(StylePropertyId.Bottom, "bottom");
			dictionary2.Add(StylePropertyId.Color, "color");
			dictionary2.Add(StylePropertyId.Cursor, "cursor");
			dictionary2.Add(StylePropertyId.Display, "display");
			dictionary2.Add(StylePropertyId.Flex, "flex");
			dictionary2.Add(StylePropertyId.FlexBasis, "flex-basis");
			dictionary2.Add(StylePropertyId.FlexDirection, "flex-direction");
			dictionary2.Add(StylePropertyId.FlexGrow, "flex-grow");
			dictionary2.Add(StylePropertyId.FlexShrink, "flex-shrink");
			dictionary2.Add(StylePropertyId.FlexWrap, "flex-wrap");
			dictionary2.Add(StylePropertyId.FontSize, "font-size");
			dictionary2.Add(StylePropertyId.Height, "height");
			dictionary2.Add(StylePropertyId.JustifyContent, "justify-content");
			dictionary2.Add(StylePropertyId.Left, "left");
			dictionary2.Add(StylePropertyId.LetterSpacing, "letter-spacing");
			dictionary2.Add(StylePropertyId.Margin, "margin");
			dictionary2.Add(StylePropertyId.MarginBottom, "margin-bottom");
			dictionary2.Add(StylePropertyId.MarginLeft, "margin-left");
			dictionary2.Add(StylePropertyId.MarginRight, "margin-right");
			dictionary2.Add(StylePropertyId.MarginTop, "margin-top");
			dictionary2.Add(StylePropertyId.MaxHeight, "max-height");
			dictionary2.Add(StylePropertyId.MaxWidth, "max-width");
			dictionary2.Add(StylePropertyId.MinHeight, "min-height");
			dictionary2.Add(StylePropertyId.MinWidth, "min-width");
			dictionary2.Add(StylePropertyId.Opacity, "opacity");
			dictionary2.Add(StylePropertyId.Overflow, "overflow");
			dictionary2.Add(StylePropertyId.Padding, "padding");
			dictionary2.Add(StylePropertyId.PaddingBottom, "padding-bottom");
			dictionary2.Add(StylePropertyId.PaddingLeft, "padding-left");
			dictionary2.Add(StylePropertyId.PaddingRight, "padding-right");
			dictionary2.Add(StylePropertyId.PaddingTop, "padding-top");
			dictionary2.Add(StylePropertyId.Position, "position");
			dictionary2.Add(StylePropertyId.Right, "right");
			dictionary2.Add(StylePropertyId.Rotate, "rotate");
			dictionary2.Add(StylePropertyId.Scale, "scale");
			dictionary2.Add(StylePropertyId.TextOverflow, "text-overflow");
			dictionary2.Add(StylePropertyId.TextShadow, "text-shadow");
			dictionary2.Add(StylePropertyId.Top, "top");
			dictionary2.Add(StylePropertyId.TransformOrigin, "transform-origin");
			dictionary2.Add(StylePropertyId.Transition, "transition");
			dictionary2.Add(StylePropertyId.TransitionDelay, "transition-delay");
			dictionary2.Add(StylePropertyId.TransitionDuration, "transition-duration");
			dictionary2.Add(StylePropertyId.TransitionProperty, "transition-property");
			dictionary2.Add(StylePropertyId.TransitionTimingFunction, "transition-timing-function");
			dictionary2.Add(StylePropertyId.Translate, "translate");
			dictionary2.Add(StylePropertyId.UnityBackgroundImageTintColor, "-unity-background-image-tint-color");
			dictionary2.Add(StylePropertyId.UnityBackgroundScaleMode, "-unity-background-scale-mode");
			dictionary2.Add(StylePropertyId.UnityFont, "-unity-font");
			dictionary2.Add(StylePropertyId.UnityFontDefinition, "-unity-font-definition");
			dictionary2.Add(StylePropertyId.UnityFontStyleAndWeight, "-unity-font-style");
			dictionary2.Add(StylePropertyId.UnityOverflowClipBox, "-unity-overflow-clip-box");
			dictionary2.Add(StylePropertyId.UnityParagraphSpacing, "-unity-paragraph-spacing");
			dictionary2.Add(StylePropertyId.UnitySliceBottom, "-unity-slice-bottom");
			dictionary2.Add(StylePropertyId.UnitySliceLeft, "-unity-slice-left");
			dictionary2.Add(StylePropertyId.UnitySliceRight, "-unity-slice-right");
			dictionary2.Add(StylePropertyId.UnitySliceTop, "-unity-slice-top");
			dictionary2.Add(StylePropertyId.UnityTextAlign, "-unity-text-align");
			dictionary2.Add(StylePropertyId.UnityTextOutline, "-unity-text-outline");
			dictionary2.Add(StylePropertyId.UnityTextOutlineColor, "-unity-text-outline-color");
			dictionary2.Add(StylePropertyId.UnityTextOutlineWidth, "-unity-text-outline-width");
			dictionary2.Add(StylePropertyId.UnityTextOverflowPosition, "-unity-text-overflow-position");
			dictionary2.Add(StylePropertyId.Visibility, "visibility");
			dictionary2.Add(StylePropertyId.WhiteSpace, "white-space");
			dictionary2.Add(StylePropertyId.Width, "width");
			dictionary2.Add(StylePropertyId.WordSpacing, "word-spacing");
			StylePropertyUtil.s_IdToName = dictionary2;
			StylePropertyUtil.s_AnimatableProperties = new StylePropertyId[]
			{
				StylePropertyId.AlignContent,
				StylePropertyId.AlignItems,
				StylePropertyId.AlignSelf,
				StylePropertyId.All,
				StylePropertyId.BackgroundColor,
				StylePropertyId.BackgroundImage,
				StylePropertyId.BorderBottomColor,
				StylePropertyId.BorderBottomLeftRadius,
				StylePropertyId.BorderBottomRightRadius,
				StylePropertyId.BorderBottomWidth,
				StylePropertyId.BorderColor,
				StylePropertyId.BorderLeftColor,
				StylePropertyId.BorderLeftWidth,
				StylePropertyId.BorderRadius,
				StylePropertyId.BorderRightColor,
				StylePropertyId.BorderRightWidth,
				StylePropertyId.BorderTopColor,
				StylePropertyId.BorderTopLeftRadius,
				StylePropertyId.BorderTopRightRadius,
				StylePropertyId.BorderTopWidth,
				StylePropertyId.BorderWidth,
				StylePropertyId.Bottom,
				StylePropertyId.Color,
				StylePropertyId.Display,
				StylePropertyId.Flex,
				StylePropertyId.FlexBasis,
				StylePropertyId.FlexDirection,
				StylePropertyId.FlexGrow,
				StylePropertyId.FlexShrink,
				StylePropertyId.FlexWrap,
				StylePropertyId.FontSize,
				StylePropertyId.Height,
				StylePropertyId.JustifyContent,
				StylePropertyId.Left,
				StylePropertyId.LetterSpacing,
				StylePropertyId.Margin,
				StylePropertyId.MarginBottom,
				StylePropertyId.MarginLeft,
				StylePropertyId.MarginRight,
				StylePropertyId.MarginTop,
				StylePropertyId.MaxHeight,
				StylePropertyId.MaxWidth,
				StylePropertyId.MinHeight,
				StylePropertyId.MinWidth,
				StylePropertyId.Opacity,
				StylePropertyId.Overflow,
				StylePropertyId.Padding,
				StylePropertyId.PaddingBottom,
				StylePropertyId.PaddingLeft,
				StylePropertyId.PaddingRight,
				StylePropertyId.PaddingTop,
				StylePropertyId.Position,
				StylePropertyId.Right,
				StylePropertyId.Rotate,
				StylePropertyId.Scale,
				StylePropertyId.TextOverflow,
				StylePropertyId.TextShadow,
				StylePropertyId.Top,
				StylePropertyId.TransformOrigin,
				StylePropertyId.Translate,
				StylePropertyId.UnityBackgroundImageTintColor,
				StylePropertyId.UnityBackgroundScaleMode,
				StylePropertyId.UnityFont,
				StylePropertyId.UnityFontDefinition,
				StylePropertyId.UnityFontStyleAndWeight,
				StylePropertyId.UnityOverflowClipBox,
				StylePropertyId.UnityParagraphSpacing,
				StylePropertyId.UnitySliceBottom,
				StylePropertyId.UnitySliceLeft,
				StylePropertyId.UnitySliceRight,
				StylePropertyId.UnitySliceTop,
				StylePropertyId.UnityTextAlign,
				StylePropertyId.UnityTextOutline,
				StylePropertyId.UnityTextOutlineColor,
				StylePropertyId.UnityTextOutlineWidth,
				StylePropertyId.UnityTextOverflowPosition,
				StylePropertyId.Visibility,
				StylePropertyId.WhiteSpace,
				StylePropertyId.Width,
				StylePropertyId.WordSpacing
			};
			StylePropertyUtil.s_AnimatablePropertiesHash = new HashSet<StylePropertyId>(StylePropertyUtil.s_AnimatableProperties);
		}

		// Token: 0x06001B11 RID: 6929 RVA: 0x0007B788 File Offset: 0x00079988
		public static bool IsAnimatable(StylePropertyId id)
		{
			return StylePropertyUtil.s_AnimatablePropertiesHash.Contains(id);
		}

		// Token: 0x06001B12 RID: 6930 RVA: 0x0007B7A8 File Offset: 0x000799A8
		public static bool TryGetEnumIntValue(StyleEnumType enumType, string value, out int intValue)
		{
			intValue = 0;
			switch (enumType)
			{
			case StyleEnumType.Align:
			{
				bool flag = string.Equals(value, "auto", 5);
				if (flag)
				{
					intValue = 0;
					return true;
				}
				bool flag2 = string.Equals(value, "flex-start", 5);
				if (flag2)
				{
					intValue = 1;
					return true;
				}
				bool flag3 = string.Equals(value, "center", 5);
				if (flag3)
				{
					intValue = 2;
					return true;
				}
				bool flag4 = string.Equals(value, "flex-end", 5);
				if (flag4)
				{
					intValue = 3;
					return true;
				}
				bool flag5 = string.Equals(value, "stretch", 5);
				if (flag5)
				{
					intValue = 4;
					return true;
				}
				break;
			}
			case StyleEnumType.DisplayStyle:
			{
				bool flag6 = string.Equals(value, "flex", 5);
				if (flag6)
				{
					intValue = 0;
					return true;
				}
				bool flag7 = string.Equals(value, "none", 5);
				if (flag7)
				{
					intValue = 1;
					return true;
				}
				break;
			}
			case StyleEnumType.EasingMode:
			{
				bool flag8 = string.Equals(value, "ease", 5);
				if (flag8)
				{
					intValue = 0;
					return true;
				}
				bool flag9 = string.Equals(value, "ease-in", 5);
				if (flag9)
				{
					intValue = 1;
					return true;
				}
				bool flag10 = string.Equals(value, "ease-out", 5);
				if (flag10)
				{
					intValue = 2;
					return true;
				}
				bool flag11 = string.Equals(value, "ease-in-out", 5);
				if (flag11)
				{
					intValue = 3;
					return true;
				}
				bool flag12 = string.Equals(value, "linear", 5);
				if (flag12)
				{
					intValue = 4;
					return true;
				}
				bool flag13 = string.Equals(value, "ease-in-sine", 5);
				if (flag13)
				{
					intValue = 5;
					return true;
				}
				bool flag14 = string.Equals(value, "ease-out-sine", 5);
				if (flag14)
				{
					intValue = 6;
					return true;
				}
				bool flag15 = string.Equals(value, "ease-in-out-sine", 5);
				if (flag15)
				{
					intValue = 7;
					return true;
				}
				bool flag16 = string.Equals(value, "ease-in-cubic", 5);
				if (flag16)
				{
					intValue = 8;
					return true;
				}
				bool flag17 = string.Equals(value, "ease-out-cubic", 5);
				if (flag17)
				{
					intValue = 9;
					return true;
				}
				bool flag18 = string.Equals(value, "ease-in-out-cubic", 5);
				if (flag18)
				{
					intValue = 10;
					return true;
				}
				bool flag19 = string.Equals(value, "ease-in-circ", 5);
				if (flag19)
				{
					intValue = 11;
					return true;
				}
				bool flag20 = string.Equals(value, "ease-out-circ", 5);
				if (flag20)
				{
					intValue = 12;
					return true;
				}
				bool flag21 = string.Equals(value, "ease-in-out-circ", 5);
				if (flag21)
				{
					intValue = 13;
					return true;
				}
				bool flag22 = string.Equals(value, "ease-in-elastic", 5);
				if (flag22)
				{
					intValue = 14;
					return true;
				}
				bool flag23 = string.Equals(value, "ease-out-elastic", 5);
				if (flag23)
				{
					intValue = 15;
					return true;
				}
				bool flag24 = string.Equals(value, "ease-in-out-elastic", 5);
				if (flag24)
				{
					intValue = 16;
					return true;
				}
				bool flag25 = string.Equals(value, "ease-in-back", 5);
				if (flag25)
				{
					intValue = 17;
					return true;
				}
				bool flag26 = string.Equals(value, "ease-out-back", 5);
				if (flag26)
				{
					intValue = 18;
					return true;
				}
				bool flag27 = string.Equals(value, "ease-in-out-back", 5);
				if (flag27)
				{
					intValue = 19;
					return true;
				}
				bool flag28 = string.Equals(value, "ease-in-bounce", 5);
				if (flag28)
				{
					intValue = 20;
					return true;
				}
				bool flag29 = string.Equals(value, "ease-out-bounce", 5);
				if (flag29)
				{
					intValue = 21;
					return true;
				}
				bool flag30 = string.Equals(value, "ease-in-out-bounce", 5);
				if (flag30)
				{
					intValue = 22;
					return true;
				}
				break;
			}
			case StyleEnumType.FlexDirection:
			{
				bool flag31 = string.Equals(value, "column", 5);
				if (flag31)
				{
					intValue = 0;
					return true;
				}
				bool flag32 = string.Equals(value, "column-reverse", 5);
				if (flag32)
				{
					intValue = 1;
					return true;
				}
				bool flag33 = string.Equals(value, "row", 5);
				if (flag33)
				{
					intValue = 2;
					return true;
				}
				bool flag34 = string.Equals(value, "row-reverse", 5);
				if (flag34)
				{
					intValue = 3;
					return true;
				}
				break;
			}
			case StyleEnumType.FontStyle:
			{
				bool flag35 = string.Equals(value, "normal", 5);
				if (flag35)
				{
					intValue = 0;
					return true;
				}
				bool flag36 = string.Equals(value, "bold", 5);
				if (flag36)
				{
					intValue = 1;
					return true;
				}
				bool flag37 = string.Equals(value, "italic", 5);
				if (flag37)
				{
					intValue = 2;
					return true;
				}
				bool flag38 = string.Equals(value, "bold-and-italic", 5);
				if (flag38)
				{
					intValue = 3;
					return true;
				}
				break;
			}
			case StyleEnumType.Justify:
			{
				bool flag39 = string.Equals(value, "flex-start", 5);
				if (flag39)
				{
					intValue = 0;
					return true;
				}
				bool flag40 = string.Equals(value, "center", 5);
				if (flag40)
				{
					intValue = 1;
					return true;
				}
				bool flag41 = string.Equals(value, "flex-end", 5);
				if (flag41)
				{
					intValue = 2;
					return true;
				}
				bool flag42 = string.Equals(value, "space-between", 5);
				if (flag42)
				{
					intValue = 3;
					return true;
				}
				bool flag43 = string.Equals(value, "space-around", 5);
				if (flag43)
				{
					intValue = 4;
					return true;
				}
				break;
			}
			case StyleEnumType.Overflow:
			{
				bool flag44 = string.Equals(value, "visible", 5);
				if (flag44)
				{
					intValue = 0;
					return true;
				}
				bool flag45 = string.Equals(value, "hidden", 5);
				if (flag45)
				{
					intValue = 1;
					return true;
				}
				break;
			}
			case StyleEnumType.OverflowClipBox:
			{
				bool flag46 = string.Equals(value, "padding-box", 5);
				if (flag46)
				{
					intValue = 0;
					return true;
				}
				bool flag47 = string.Equals(value, "content-box", 5);
				if (flag47)
				{
					intValue = 1;
					return true;
				}
				break;
			}
			case StyleEnumType.OverflowInternal:
			{
				bool flag48 = string.Equals(value, "visible", 5);
				if (flag48)
				{
					intValue = 0;
					return true;
				}
				bool flag49 = string.Equals(value, "hidden", 5);
				if (flag49)
				{
					intValue = 1;
					return true;
				}
				bool flag50 = string.Equals(value, "scroll", 5);
				if (flag50)
				{
					intValue = 2;
					return true;
				}
				break;
			}
			case StyleEnumType.Position:
			{
				bool flag51 = string.Equals(value, "relative", 5);
				if (flag51)
				{
					intValue = 0;
					return true;
				}
				bool flag52 = string.Equals(value, "absolute", 5);
				if (flag52)
				{
					intValue = 1;
					return true;
				}
				break;
			}
			case StyleEnumType.ScaleMode:
			{
				bool flag53 = string.Equals(value, "stretch-to-fill", 5);
				if (flag53)
				{
					intValue = 0;
					return true;
				}
				bool flag54 = string.Equals(value, "scale-and-crop", 5);
				if (flag54)
				{
					intValue = 1;
					return true;
				}
				bool flag55 = string.Equals(value, "scale-to-fit", 5);
				if (flag55)
				{
					intValue = 2;
					return true;
				}
				break;
			}
			case StyleEnumType.TextAnchor:
			{
				bool flag56 = string.Equals(value, "upper-left", 5);
				if (flag56)
				{
					intValue = 0;
					return true;
				}
				bool flag57 = string.Equals(value, "upper-center", 5);
				if (flag57)
				{
					intValue = 1;
					return true;
				}
				bool flag58 = string.Equals(value, "upper-right", 5);
				if (flag58)
				{
					intValue = 2;
					return true;
				}
				bool flag59 = string.Equals(value, "middle-left", 5);
				if (flag59)
				{
					intValue = 3;
					return true;
				}
				bool flag60 = string.Equals(value, "middle-center", 5);
				if (flag60)
				{
					intValue = 4;
					return true;
				}
				bool flag61 = string.Equals(value, "middle-right", 5);
				if (flag61)
				{
					intValue = 5;
					return true;
				}
				bool flag62 = string.Equals(value, "lower-left", 5);
				if (flag62)
				{
					intValue = 6;
					return true;
				}
				bool flag63 = string.Equals(value, "lower-center", 5);
				if (flag63)
				{
					intValue = 7;
					return true;
				}
				bool flag64 = string.Equals(value, "lower-right", 5);
				if (flag64)
				{
					intValue = 8;
					return true;
				}
				break;
			}
			case StyleEnumType.TextOverflow:
			{
				bool flag65 = string.Equals(value, "clip", 5);
				if (flag65)
				{
					intValue = 0;
					return true;
				}
				bool flag66 = string.Equals(value, "ellipsis", 5);
				if (flag66)
				{
					intValue = 1;
					return true;
				}
				break;
			}
			case StyleEnumType.TextOverflowPosition:
			{
				bool flag67 = string.Equals(value, "start", 5);
				if (flag67)
				{
					intValue = 1;
					return true;
				}
				bool flag68 = string.Equals(value, "middle", 5);
				if (flag68)
				{
					intValue = 2;
					return true;
				}
				bool flag69 = string.Equals(value, "end", 5);
				if (flag69)
				{
					intValue = 0;
					return true;
				}
				break;
			}
			case StyleEnumType.TransformOriginOffset:
			{
				bool flag70 = string.Equals(value, "left", 5);
				if (flag70)
				{
					intValue = 1;
					return true;
				}
				bool flag71 = string.Equals(value, "right", 5);
				if (flag71)
				{
					intValue = 2;
					return true;
				}
				bool flag72 = string.Equals(value, "top", 5);
				if (flag72)
				{
					intValue = 3;
					return true;
				}
				bool flag73 = string.Equals(value, "bottom", 5);
				if (flag73)
				{
					intValue = 4;
					return true;
				}
				bool flag74 = string.Equals(value, "center", 5);
				if (flag74)
				{
					intValue = 5;
					return true;
				}
				break;
			}
			case StyleEnumType.Visibility:
			{
				bool flag75 = string.Equals(value, "visible", 5);
				if (flag75)
				{
					intValue = 0;
					return true;
				}
				bool flag76 = string.Equals(value, "hidden", 5);
				if (flag76)
				{
					intValue = 1;
					return true;
				}
				break;
			}
			case StyleEnumType.WhiteSpace:
			{
				bool flag77 = string.Equals(value, "normal", 5);
				if (flag77)
				{
					intValue = 0;
					return true;
				}
				bool flag78 = string.Equals(value, "nowrap", 5);
				if (flag78)
				{
					intValue = 1;
					return true;
				}
				break;
			}
			case StyleEnumType.Wrap:
			{
				bool flag79 = string.Equals(value, "nowrap", 5);
				if (flag79)
				{
					intValue = 0;
					return true;
				}
				bool flag80 = string.Equals(value, "wrap", 5);
				if (flag80)
				{
					intValue = 1;
					return true;
				}
				bool flag81 = string.Equals(value, "wrap-reverse", 5);
				if (flag81)
				{
					intValue = 2;
					return true;
				}
				break;
			}
			}
			return false;
		}

		// Token: 0x06001B13 RID: 6931 RVA: 0x0007C194 File Offset: 0x0007A394
		public static bool IsMatchingShorthand(StylePropertyId shorthand, StylePropertyId id)
		{
			switch (shorthand)
			{
			case StylePropertyId.All:
				return true;
			case StylePropertyId.BorderColor:
				return id == StylePropertyId.BorderTopColor || id == StylePropertyId.BorderRightColor || id == StylePropertyId.BorderBottomColor || id == StylePropertyId.BorderLeftColor;
			case StylePropertyId.BorderRadius:
				return id == StylePropertyId.BorderTopLeftRadius || id == StylePropertyId.BorderTopRightRadius || id == StylePropertyId.BorderBottomRightRadius || id == StylePropertyId.BorderBottomLeftRadius;
			case StylePropertyId.BorderWidth:
				return id == StylePropertyId.BorderTopWidth || id == StylePropertyId.BorderRightWidth || id == StylePropertyId.BorderBottomWidth || id == StylePropertyId.BorderLeftWidth;
			case StylePropertyId.Flex:
				return id == StylePropertyId.FlexGrow || id == StylePropertyId.FlexShrink || id == StylePropertyId.FlexBasis;
			case StylePropertyId.Margin:
				return id == StylePropertyId.MarginTop || id == StylePropertyId.MarginRight || id == StylePropertyId.MarginBottom || id == StylePropertyId.MarginLeft;
			case StylePropertyId.Padding:
				return id == StylePropertyId.PaddingTop || id == StylePropertyId.PaddingRight || id == StylePropertyId.PaddingBottom || id == StylePropertyId.PaddingLeft;
			case StylePropertyId.UnityTextOutline:
				return id == StylePropertyId.UnityTextOutlineColor || id == StylePropertyId.UnityTextOutlineWidth;
			}
			return false;
		}

		// Token: 0x04000D17 RID: 3351
		private static readonly HashSet<StylePropertyId> s_AnimatablePropertiesHash;

		// Token: 0x04000D18 RID: 3352
		public const int k_GroupOffset = 16;

		// Token: 0x04000D19 RID: 3353
		internal static readonly Dictionary<string, StylePropertyId> s_NameToId;

		// Token: 0x04000D1A RID: 3354
		internal static readonly Dictionary<StylePropertyId, string> s_IdToName;

		// Token: 0x04000D1B RID: 3355
		internal static readonly StylePropertyId[] s_AnimatableProperties;
	}
}
