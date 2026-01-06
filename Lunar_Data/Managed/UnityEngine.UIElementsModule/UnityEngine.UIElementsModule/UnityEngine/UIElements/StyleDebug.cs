using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UIElements.StyleSheets;

namespace UnityEngine.UIElements
{
	// Token: 0x0200027F RID: 639
	internal static class StyleDebug
	{
		// Token: 0x060014A4 RID: 5284 RVA: 0x00057E8C File Offset: 0x0005608C
		public static string[] GetStylePropertyNames()
		{
			List<string> list = Enumerable.ToList<string>(StylePropertyUtil.s_NameToId.Keys);
			list.Sort();
			return list.ToArray();
		}

		// Token: 0x060014A5 RID: 5285 RVA: 0x00057EBC File Offset: 0x000560BC
		public static string[] GetLonghandPropertyNames(string shorthandName)
		{
			StylePropertyId stylePropertyId;
			bool flag = StylePropertyUtil.s_NameToId.TryGetValue(shorthandName, ref stylePropertyId);
			if (flag)
			{
				bool flag2 = StyleDebug.IsShorthandProperty(stylePropertyId);
				if (flag2)
				{
					return StyleDebug.GetLonghandPropertyNames(stylePropertyId);
				}
			}
			return null;
		}

		// Token: 0x060014A6 RID: 5286 RVA: 0x00057EF8 File Offset: 0x000560F8
		public static StylePropertyId GetStylePropertyIdFromName(string name)
		{
			StylePropertyId stylePropertyId;
			bool flag = StylePropertyUtil.s_NameToId.TryGetValue(name, ref stylePropertyId);
			StylePropertyId stylePropertyId2;
			if (flag)
			{
				stylePropertyId2 = stylePropertyId;
			}
			else
			{
				stylePropertyId2 = StylePropertyId.Unknown;
			}
			return stylePropertyId2;
		}

		// Token: 0x060014A7 RID: 5287 RVA: 0x00057F20 File Offset: 0x00056120
		public static object GetComputedStyleValue(in ComputedStyle computedStyle, string name)
		{
			StylePropertyId stylePropertyId;
			bool flag = StylePropertyUtil.s_NameToId.TryGetValue(name, ref stylePropertyId);
			object obj;
			if (flag)
			{
				obj = StyleDebug.GetComputedStyleValue(in computedStyle, stylePropertyId);
			}
			else
			{
				obj = null;
			}
			return obj;
		}

		// Token: 0x060014A8 RID: 5288 RVA: 0x00057F50 File Offset: 0x00056150
		public static object GetInlineStyleValue(IStyle style, string name)
		{
			StylePropertyId stylePropertyId;
			bool flag = StylePropertyUtil.s_NameToId.TryGetValue(name, ref stylePropertyId);
			object obj;
			if (flag)
			{
				obj = StyleDebug.GetInlineStyleValue(style, stylePropertyId);
			}
			else
			{
				obj = null;
			}
			return obj;
		}

		// Token: 0x060014A9 RID: 5289 RVA: 0x00057F80 File Offset: 0x00056180
		public static void SetInlineStyleValue(IStyle style, string name, object value)
		{
			StylePropertyId stylePropertyId;
			bool flag = StylePropertyUtil.s_NameToId.TryGetValue(name, ref stylePropertyId);
			if (flag)
			{
				StyleDebug.SetInlineStyleValue(style, stylePropertyId, value);
			}
		}

		// Token: 0x060014AA RID: 5290 RVA: 0x00057FAC File Offset: 0x000561AC
		public static Type GetInlineStyleType(string name)
		{
			StylePropertyId stylePropertyId;
			bool flag = StylePropertyUtil.s_NameToId.TryGetValue(name, ref stylePropertyId);
			if (flag)
			{
				bool flag2 = !StyleDebug.IsShorthandProperty(stylePropertyId);
				if (flag2)
				{
					return StyleDebug.GetInlineStyleType(stylePropertyId);
				}
			}
			return null;
		}

		// Token: 0x060014AB RID: 5291 RVA: 0x00057FE8 File Offset: 0x000561E8
		public static Type GetComputedStyleType(string name)
		{
			StylePropertyId stylePropertyId;
			bool flag = StylePropertyUtil.s_NameToId.TryGetValue(name, ref stylePropertyId);
			if (flag)
			{
				bool flag2 = !StyleDebug.IsShorthandProperty(stylePropertyId);
				if (flag2)
				{
					return StyleDebug.GetInlineStyleType(stylePropertyId);
				}
			}
			return null;
		}

		// Token: 0x060014AC RID: 5292 RVA: 0x00058024 File Offset: 0x00056224
		public static void FindSpecifiedStyles(in ComputedStyle computedStyle, IEnumerable<SelectorMatchRecord> matchRecords, Dictionary<StylePropertyId, int> result)
		{
			result.Clear();
			foreach (SelectorMatchRecord selectorMatchRecord in matchRecords)
			{
				int num = selectorMatchRecord.complexSelector.specificity;
				bool isDefaultStyleSheet = selectorMatchRecord.sheet.isDefaultStyleSheet;
				if (isDefaultStyleSheet)
				{
					num = -1;
				}
				StyleProperty[] properties = selectorMatchRecord.complexSelector.rule.properties;
				foreach (StyleProperty styleProperty in properties)
				{
					StylePropertyId stylePropertyId;
					bool flag = StylePropertyUtil.s_NameToId.TryGetValue(styleProperty.name, ref stylePropertyId);
					if (flag)
					{
						bool flag2 = StyleDebug.IsShorthandProperty(stylePropertyId);
						if (flag2)
						{
							string[] longhandPropertyNames = StyleDebug.GetLonghandPropertyNames(stylePropertyId);
							foreach (string text in longhandPropertyNames)
							{
								StylePropertyId stylePropertyIdFromName = StyleDebug.GetStylePropertyIdFromName(text);
								result[stylePropertyIdFromName] = num;
							}
						}
						else
						{
							result[stylePropertyId] = num;
						}
					}
				}
			}
			StylePropertyId[] inheritedProperties = StyleDebug.GetInheritedProperties();
			foreach (StylePropertyId stylePropertyId2 in inheritedProperties)
			{
				bool flag3 = result.ContainsKey(stylePropertyId2);
				if (!flag3)
				{
					object computedStyleValue = StyleDebug.GetComputedStyleValue(in computedStyle, stylePropertyId2);
					object computedStyleValue2 = StyleDebug.GetComputedStyleValue(InitialStyle.Get(), stylePropertyId2);
					bool flag4 = computedStyleValue != null && !computedStyleValue.Equals(computedStyleValue2);
					if (flag4)
					{
						result[stylePropertyId2] = 2147483646;
					}
				}
			}
		}

		// Token: 0x060014AD RID: 5293 RVA: 0x000581BC File Offset: 0x000563BC
		public static object GetComputedStyleValue(in ComputedStyle computedStyle, StylePropertyId id)
		{
			if (id <= StylePropertyId.UnityTextOverflowPosition)
			{
				switch (id)
				{
				case StylePropertyId.Color:
				{
					ComputedStyle computedStyle2 = computedStyle;
					return computedStyle2.color;
				}
				case StylePropertyId.FontSize:
				{
					ComputedStyle computedStyle2 = computedStyle;
					return computedStyle2.fontSize;
				}
				case StylePropertyId.LetterSpacing:
				{
					ComputedStyle computedStyle2 = computedStyle;
					return computedStyle2.letterSpacing;
				}
				case StylePropertyId.TextShadow:
				{
					ComputedStyle computedStyle2 = computedStyle;
					return computedStyle2.textShadow;
				}
				case StylePropertyId.UnityFont:
				{
					ComputedStyle computedStyle2 = computedStyle;
					return computedStyle2.unityFont;
				}
				case StylePropertyId.UnityFontDefinition:
				{
					ComputedStyle computedStyle2 = computedStyle;
					return computedStyle2.unityFontDefinition;
				}
				case StylePropertyId.UnityFontStyleAndWeight:
				{
					ComputedStyle computedStyle2 = computedStyle;
					return computedStyle2.unityFontStyleAndWeight;
				}
				case StylePropertyId.UnityParagraphSpacing:
				{
					ComputedStyle computedStyle2 = computedStyle;
					return computedStyle2.unityParagraphSpacing;
				}
				case StylePropertyId.UnityTextAlign:
				{
					ComputedStyle computedStyle2 = computedStyle;
					return computedStyle2.unityTextAlign;
				}
				case StylePropertyId.UnityTextOutlineColor:
				{
					ComputedStyle computedStyle2 = computedStyle;
					return computedStyle2.unityTextOutlineColor;
				}
				case StylePropertyId.UnityTextOutlineWidth:
				{
					ComputedStyle computedStyle2 = computedStyle;
					return computedStyle2.unityTextOutlineWidth;
				}
				case StylePropertyId.Visibility:
				{
					ComputedStyle computedStyle2 = computedStyle;
					return computedStyle2.visibility;
				}
				case StylePropertyId.WhiteSpace:
				{
					ComputedStyle computedStyle2 = computedStyle;
					return computedStyle2.whiteSpace;
				}
				case StylePropertyId.WordSpacing:
				{
					ComputedStyle computedStyle2 = computedStyle;
					return computedStyle2.wordSpacing;
				}
				default:
					switch (id)
					{
					case StylePropertyId.AlignContent:
					{
						ComputedStyle computedStyle2 = computedStyle;
						return computedStyle2.alignContent;
					}
					case StylePropertyId.AlignItems:
					{
						ComputedStyle computedStyle2 = computedStyle;
						return computedStyle2.alignItems;
					}
					case StylePropertyId.AlignSelf:
					{
						ComputedStyle computedStyle2 = computedStyle;
						return computedStyle2.alignSelf;
					}
					case StylePropertyId.BorderBottomWidth:
					{
						ComputedStyle computedStyle2 = computedStyle;
						return computedStyle2.borderBottomWidth;
					}
					case StylePropertyId.BorderLeftWidth:
					{
						ComputedStyle computedStyle2 = computedStyle;
						return computedStyle2.borderLeftWidth;
					}
					case StylePropertyId.BorderRightWidth:
					{
						ComputedStyle computedStyle2 = computedStyle;
						return computedStyle2.borderRightWidth;
					}
					case StylePropertyId.BorderTopWidth:
					{
						ComputedStyle computedStyle2 = computedStyle;
						return computedStyle2.borderTopWidth;
					}
					case StylePropertyId.Bottom:
					{
						ComputedStyle computedStyle2 = computedStyle;
						return computedStyle2.bottom;
					}
					case StylePropertyId.Display:
					{
						ComputedStyle computedStyle2 = computedStyle;
						return computedStyle2.display;
					}
					case StylePropertyId.FlexBasis:
					{
						ComputedStyle computedStyle2 = computedStyle;
						return computedStyle2.flexBasis;
					}
					case StylePropertyId.FlexDirection:
					{
						ComputedStyle computedStyle2 = computedStyle;
						return computedStyle2.flexDirection;
					}
					case StylePropertyId.FlexGrow:
					{
						ComputedStyle computedStyle2 = computedStyle;
						return computedStyle2.flexGrow;
					}
					case StylePropertyId.FlexShrink:
					{
						ComputedStyle computedStyle2 = computedStyle;
						return computedStyle2.flexShrink;
					}
					case StylePropertyId.FlexWrap:
					{
						ComputedStyle computedStyle2 = computedStyle;
						return computedStyle2.flexWrap;
					}
					case StylePropertyId.Height:
					{
						ComputedStyle computedStyle2 = computedStyle;
						return computedStyle2.height;
					}
					case StylePropertyId.JustifyContent:
					{
						ComputedStyle computedStyle2 = computedStyle;
						return computedStyle2.justifyContent;
					}
					case StylePropertyId.Left:
					{
						ComputedStyle computedStyle2 = computedStyle;
						return computedStyle2.left;
					}
					case StylePropertyId.MarginBottom:
					{
						ComputedStyle computedStyle2 = computedStyle;
						return computedStyle2.marginBottom;
					}
					case StylePropertyId.MarginLeft:
					{
						ComputedStyle computedStyle2 = computedStyle;
						return computedStyle2.marginLeft;
					}
					case StylePropertyId.MarginRight:
					{
						ComputedStyle computedStyle2 = computedStyle;
						return computedStyle2.marginRight;
					}
					case StylePropertyId.MarginTop:
					{
						ComputedStyle computedStyle2 = computedStyle;
						return computedStyle2.marginTop;
					}
					case StylePropertyId.MaxHeight:
					{
						ComputedStyle computedStyle2 = computedStyle;
						return computedStyle2.maxHeight;
					}
					case StylePropertyId.MaxWidth:
					{
						ComputedStyle computedStyle2 = computedStyle;
						return computedStyle2.maxWidth;
					}
					case StylePropertyId.MinHeight:
					{
						ComputedStyle computedStyle2 = computedStyle;
						return computedStyle2.minHeight;
					}
					case StylePropertyId.MinWidth:
					{
						ComputedStyle computedStyle2 = computedStyle;
						return computedStyle2.minWidth;
					}
					case StylePropertyId.PaddingBottom:
					{
						ComputedStyle computedStyle2 = computedStyle;
						return computedStyle2.paddingBottom;
					}
					case StylePropertyId.PaddingLeft:
					{
						ComputedStyle computedStyle2 = computedStyle;
						return computedStyle2.paddingLeft;
					}
					case StylePropertyId.PaddingRight:
					{
						ComputedStyle computedStyle2 = computedStyle;
						return computedStyle2.paddingRight;
					}
					case StylePropertyId.PaddingTop:
					{
						ComputedStyle computedStyle2 = computedStyle;
						return computedStyle2.paddingTop;
					}
					case StylePropertyId.Position:
					{
						ComputedStyle computedStyle2 = computedStyle;
						return computedStyle2.position;
					}
					case StylePropertyId.Right:
					{
						ComputedStyle computedStyle2 = computedStyle;
						return computedStyle2.right;
					}
					case StylePropertyId.Top:
					{
						ComputedStyle computedStyle2 = computedStyle;
						return computedStyle2.top;
					}
					case StylePropertyId.Width:
					{
						ComputedStyle computedStyle2 = computedStyle;
						return computedStyle2.width;
					}
					default:
						switch (id)
						{
						case StylePropertyId.Cursor:
						{
							ComputedStyle computedStyle2 = computedStyle;
							return computedStyle2.cursor;
						}
						case StylePropertyId.TextOverflow:
						{
							ComputedStyle computedStyle2 = computedStyle;
							return computedStyle2.textOverflow;
						}
						case StylePropertyId.UnityBackgroundImageTintColor:
						{
							ComputedStyle computedStyle2 = computedStyle;
							return computedStyle2.unityBackgroundImageTintColor;
						}
						case StylePropertyId.UnityBackgroundScaleMode:
						{
							ComputedStyle computedStyle2 = computedStyle;
							return computedStyle2.unityBackgroundScaleMode;
						}
						case StylePropertyId.UnityOverflowClipBox:
						{
							ComputedStyle computedStyle2 = computedStyle;
							return computedStyle2.unityOverflowClipBox;
						}
						case StylePropertyId.UnitySliceBottom:
						{
							ComputedStyle computedStyle2 = computedStyle;
							return computedStyle2.unitySliceBottom;
						}
						case StylePropertyId.UnitySliceLeft:
						{
							ComputedStyle computedStyle2 = computedStyle;
							return computedStyle2.unitySliceLeft;
						}
						case StylePropertyId.UnitySliceRight:
						{
							ComputedStyle computedStyle2 = computedStyle;
							return computedStyle2.unitySliceRight;
						}
						case StylePropertyId.UnitySliceTop:
						{
							ComputedStyle computedStyle2 = computedStyle;
							return computedStyle2.unitySliceTop;
						}
						case StylePropertyId.UnityTextOverflowPosition:
						{
							ComputedStyle computedStyle2 = computedStyle;
							return computedStyle2.unityTextOverflowPosition;
						}
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
				{
					ComputedStyle computedStyle2 = computedStyle;
					return computedStyle2.rotate;
				}
				case StylePropertyId.Scale:
				{
					ComputedStyle computedStyle2 = computedStyle;
					return computedStyle2.scale;
				}
				case StylePropertyId.TransformOrigin:
				{
					ComputedStyle computedStyle2 = computedStyle;
					return computedStyle2.transformOrigin;
				}
				case StylePropertyId.Translate:
				{
					ComputedStyle computedStyle2 = computedStyle;
					return computedStyle2.translate;
				}
				default:
					switch (id)
					{
					case StylePropertyId.TransitionDelay:
					{
						ComputedStyle computedStyle2 = computedStyle;
						return computedStyle2.transitionDelay;
					}
					case StylePropertyId.TransitionDuration:
					{
						ComputedStyle computedStyle2 = computedStyle;
						return computedStyle2.transitionDuration;
					}
					case StylePropertyId.TransitionProperty:
					{
						ComputedStyle computedStyle2 = computedStyle;
						return computedStyle2.transitionProperty;
					}
					case StylePropertyId.TransitionTimingFunction:
					{
						ComputedStyle computedStyle2 = computedStyle;
						return computedStyle2.transitionTimingFunction;
					}
					default:
						switch (id)
						{
						case StylePropertyId.BackgroundColor:
						{
							ComputedStyle computedStyle2 = computedStyle;
							return computedStyle2.backgroundColor;
						}
						case StylePropertyId.BackgroundImage:
						{
							ComputedStyle computedStyle2 = computedStyle;
							return computedStyle2.backgroundImage;
						}
						case StylePropertyId.BorderBottomColor:
						{
							ComputedStyle computedStyle2 = computedStyle;
							return computedStyle2.borderBottomColor;
						}
						case StylePropertyId.BorderBottomLeftRadius:
						{
							ComputedStyle computedStyle2 = computedStyle;
							return computedStyle2.borderBottomLeftRadius;
						}
						case StylePropertyId.BorderBottomRightRadius:
						{
							ComputedStyle computedStyle2 = computedStyle;
							return computedStyle2.borderBottomRightRadius;
						}
						case StylePropertyId.BorderLeftColor:
						{
							ComputedStyle computedStyle2 = computedStyle;
							return computedStyle2.borderLeftColor;
						}
						case StylePropertyId.BorderRightColor:
						{
							ComputedStyle computedStyle2 = computedStyle;
							return computedStyle2.borderRightColor;
						}
						case StylePropertyId.BorderTopColor:
						{
							ComputedStyle computedStyle2 = computedStyle;
							return computedStyle2.borderTopColor;
						}
						case StylePropertyId.BorderTopLeftRadius:
						{
							ComputedStyle computedStyle2 = computedStyle;
							return computedStyle2.borderTopLeftRadius;
						}
						case StylePropertyId.BorderTopRightRadius:
						{
							ComputedStyle computedStyle2 = computedStyle;
							return computedStyle2.borderTopRightRadius;
						}
						case StylePropertyId.Opacity:
						{
							ComputedStyle computedStyle2 = computedStyle;
							return computedStyle2.opacity;
						}
						case StylePropertyId.Overflow:
						{
							ComputedStyle computedStyle2 = computedStyle;
							return computedStyle2.overflow;
						}
						}
						break;
					}
					break;
				}
			}
			Debug.LogAssertion(string.Format("Cannot get computed style value for property id {0}", id));
			return null;
		}

		// Token: 0x060014AE RID: 5294 RVA: 0x00058AE0 File Offset: 0x00056CE0
		public static object GetInlineStyleValue(IStyle style, StylePropertyId id)
		{
			if (id <= StylePropertyId.UnityTextOverflowPosition)
			{
				switch (id)
				{
				case StylePropertyId.Color:
					return style.color;
				case StylePropertyId.FontSize:
					return style.fontSize;
				case StylePropertyId.LetterSpacing:
					return style.letterSpacing;
				case StylePropertyId.TextShadow:
					return style.textShadow;
				case StylePropertyId.UnityFont:
					return style.unityFont;
				case StylePropertyId.UnityFontDefinition:
					return style.unityFontDefinition;
				case StylePropertyId.UnityFontStyleAndWeight:
					return style.unityFontStyleAndWeight;
				case StylePropertyId.UnityParagraphSpacing:
					return style.unityParagraphSpacing;
				case StylePropertyId.UnityTextAlign:
					return style.unityTextAlign;
				case StylePropertyId.UnityTextOutlineColor:
					return style.unityTextOutlineColor;
				case StylePropertyId.UnityTextOutlineWidth:
					return style.unityTextOutlineWidth;
				case StylePropertyId.Visibility:
					return style.visibility;
				case StylePropertyId.WhiteSpace:
					return style.whiteSpace;
				case StylePropertyId.WordSpacing:
					return style.wordSpacing;
				default:
					switch (id)
					{
					case StylePropertyId.AlignContent:
						return style.alignContent;
					case StylePropertyId.AlignItems:
						return style.alignItems;
					case StylePropertyId.AlignSelf:
						return style.alignSelf;
					case StylePropertyId.BorderBottomWidth:
						return style.borderBottomWidth;
					case StylePropertyId.BorderLeftWidth:
						return style.borderLeftWidth;
					case StylePropertyId.BorderRightWidth:
						return style.borderRightWidth;
					case StylePropertyId.BorderTopWidth:
						return style.borderTopWidth;
					case StylePropertyId.Bottom:
						return style.bottom;
					case StylePropertyId.Display:
						return style.display;
					case StylePropertyId.FlexBasis:
						return style.flexBasis;
					case StylePropertyId.FlexDirection:
						return style.flexDirection;
					case StylePropertyId.FlexGrow:
						return style.flexGrow;
					case StylePropertyId.FlexShrink:
						return style.flexShrink;
					case StylePropertyId.FlexWrap:
						return style.flexWrap;
					case StylePropertyId.Height:
						return style.height;
					case StylePropertyId.JustifyContent:
						return style.justifyContent;
					case StylePropertyId.Left:
						return style.left;
					case StylePropertyId.MarginBottom:
						return style.marginBottom;
					case StylePropertyId.MarginLeft:
						return style.marginLeft;
					case StylePropertyId.MarginRight:
						return style.marginRight;
					case StylePropertyId.MarginTop:
						return style.marginTop;
					case StylePropertyId.MaxHeight:
						return style.maxHeight;
					case StylePropertyId.MaxWidth:
						return style.maxWidth;
					case StylePropertyId.MinHeight:
						return style.minHeight;
					case StylePropertyId.MinWidth:
						return style.minWidth;
					case StylePropertyId.PaddingBottom:
						return style.paddingBottom;
					case StylePropertyId.PaddingLeft:
						return style.paddingLeft;
					case StylePropertyId.PaddingRight:
						return style.paddingRight;
					case StylePropertyId.PaddingTop:
						return style.paddingTop;
					case StylePropertyId.Position:
						return style.position;
					case StylePropertyId.Right:
						return style.right;
					case StylePropertyId.Top:
						return style.top;
					case StylePropertyId.Width:
						return style.width;
					default:
						switch (id)
						{
						case StylePropertyId.Cursor:
							return style.cursor;
						case StylePropertyId.TextOverflow:
							return style.textOverflow;
						case StylePropertyId.UnityBackgroundImageTintColor:
							return style.unityBackgroundImageTintColor;
						case StylePropertyId.UnityBackgroundScaleMode:
							return style.unityBackgroundScaleMode;
						case StylePropertyId.UnityOverflowClipBox:
							return style.unityOverflowClipBox;
						case StylePropertyId.UnitySliceBottom:
							return style.unitySliceBottom;
						case StylePropertyId.UnitySliceLeft:
							return style.unitySliceLeft;
						case StylePropertyId.UnitySliceRight:
							return style.unitySliceRight;
						case StylePropertyId.UnitySliceTop:
							return style.unitySliceTop;
						case StylePropertyId.UnityTextOverflowPosition:
							return style.unityTextOverflowPosition;
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
					return style.rotate;
				case StylePropertyId.Scale:
					return style.scale;
				case StylePropertyId.TransformOrigin:
					return style.transformOrigin;
				case StylePropertyId.Translate:
					return style.translate;
				default:
					switch (id)
					{
					case StylePropertyId.TransitionDelay:
						return style.transitionDelay;
					case StylePropertyId.TransitionDuration:
						return style.transitionDuration;
					case StylePropertyId.TransitionProperty:
						return style.transitionProperty;
					case StylePropertyId.TransitionTimingFunction:
						return style.transitionTimingFunction;
					default:
						switch (id)
						{
						case StylePropertyId.BackgroundColor:
							return style.backgroundColor;
						case StylePropertyId.BackgroundImage:
							return style.backgroundImage;
						case StylePropertyId.BorderBottomColor:
							return style.borderBottomColor;
						case StylePropertyId.BorderBottomLeftRadius:
							return style.borderBottomLeftRadius;
						case StylePropertyId.BorderBottomRightRadius:
							return style.borderBottomRightRadius;
						case StylePropertyId.BorderLeftColor:
							return style.borderLeftColor;
						case StylePropertyId.BorderRightColor:
							return style.borderRightColor;
						case StylePropertyId.BorderTopColor:
							return style.borderTopColor;
						case StylePropertyId.BorderTopLeftRadius:
							return style.borderTopLeftRadius;
						case StylePropertyId.BorderTopRightRadius:
							return style.borderTopRightRadius;
						case StylePropertyId.Opacity:
							return style.opacity;
						case StylePropertyId.Overflow:
							return style.overflow;
						}
						break;
					}
					break;
				}
			}
			Debug.LogAssertion(string.Format("Cannot get inline style value for property id {0}", id));
			return null;
		}

		// Token: 0x060014AF RID: 5295 RVA: 0x000591AC File Offset: 0x000573AC
		public static void SetInlineStyleValue(IStyle style, StylePropertyId id, object value)
		{
			if (id <= StylePropertyId.UnityTextOverflowPosition)
			{
				switch (id)
				{
				case StylePropertyId.Color:
					style.color = (StyleColor)value;
					return;
				case StylePropertyId.FontSize:
					style.fontSize = (StyleLength)value;
					return;
				case StylePropertyId.LetterSpacing:
					style.letterSpacing = (StyleLength)value;
					return;
				case StylePropertyId.TextShadow:
					style.textShadow = (StyleTextShadow)value;
					return;
				case StylePropertyId.UnityFont:
					style.unityFont = (StyleFont)value;
					return;
				case StylePropertyId.UnityFontDefinition:
					style.unityFontDefinition = (StyleFontDefinition)value;
					return;
				case StylePropertyId.UnityFontStyleAndWeight:
					style.unityFontStyleAndWeight = (StyleEnum<FontStyle>)value;
					return;
				case StylePropertyId.UnityParagraphSpacing:
					style.unityParagraphSpacing = (StyleLength)value;
					return;
				case StylePropertyId.UnityTextAlign:
					style.unityTextAlign = (StyleEnum<TextAnchor>)value;
					return;
				case StylePropertyId.UnityTextOutlineColor:
					style.unityTextOutlineColor = (StyleColor)value;
					return;
				case StylePropertyId.UnityTextOutlineWidth:
					style.unityTextOutlineWidth = (StyleFloat)value;
					return;
				case StylePropertyId.Visibility:
					style.visibility = (StyleEnum<Visibility>)value;
					return;
				case StylePropertyId.WhiteSpace:
					style.whiteSpace = (StyleEnum<WhiteSpace>)value;
					return;
				case StylePropertyId.WordSpacing:
					style.wordSpacing = (StyleLength)value;
					return;
				default:
					switch (id)
					{
					case StylePropertyId.AlignContent:
						style.alignContent = (StyleEnum<Align>)value;
						return;
					case StylePropertyId.AlignItems:
						style.alignItems = (StyleEnum<Align>)value;
						return;
					case StylePropertyId.AlignSelf:
						style.alignSelf = (StyleEnum<Align>)value;
						return;
					case StylePropertyId.BorderBottomWidth:
						style.borderBottomWidth = (StyleFloat)value;
						return;
					case StylePropertyId.BorderLeftWidth:
						style.borderLeftWidth = (StyleFloat)value;
						return;
					case StylePropertyId.BorderRightWidth:
						style.borderRightWidth = (StyleFloat)value;
						return;
					case StylePropertyId.BorderTopWidth:
						style.borderTopWidth = (StyleFloat)value;
						return;
					case StylePropertyId.Bottom:
						style.bottom = (StyleLength)value;
						return;
					case StylePropertyId.Display:
						style.display = (StyleEnum<DisplayStyle>)value;
						return;
					case StylePropertyId.FlexBasis:
						style.flexBasis = (StyleLength)value;
						return;
					case StylePropertyId.FlexDirection:
						style.flexDirection = (StyleEnum<FlexDirection>)value;
						return;
					case StylePropertyId.FlexGrow:
						style.flexGrow = (StyleFloat)value;
						return;
					case StylePropertyId.FlexShrink:
						style.flexShrink = (StyleFloat)value;
						return;
					case StylePropertyId.FlexWrap:
						style.flexWrap = (StyleEnum<Wrap>)value;
						return;
					case StylePropertyId.Height:
						style.height = (StyleLength)value;
						return;
					case StylePropertyId.JustifyContent:
						style.justifyContent = (StyleEnum<Justify>)value;
						return;
					case StylePropertyId.Left:
						style.left = (StyleLength)value;
						return;
					case StylePropertyId.MarginBottom:
						style.marginBottom = (StyleLength)value;
						return;
					case StylePropertyId.MarginLeft:
						style.marginLeft = (StyleLength)value;
						return;
					case StylePropertyId.MarginRight:
						style.marginRight = (StyleLength)value;
						return;
					case StylePropertyId.MarginTop:
						style.marginTop = (StyleLength)value;
						return;
					case StylePropertyId.MaxHeight:
						style.maxHeight = (StyleLength)value;
						return;
					case StylePropertyId.MaxWidth:
						style.maxWidth = (StyleLength)value;
						return;
					case StylePropertyId.MinHeight:
						style.minHeight = (StyleLength)value;
						return;
					case StylePropertyId.MinWidth:
						style.minWidth = (StyleLength)value;
						return;
					case StylePropertyId.PaddingBottom:
						style.paddingBottom = (StyleLength)value;
						return;
					case StylePropertyId.PaddingLeft:
						style.paddingLeft = (StyleLength)value;
						return;
					case StylePropertyId.PaddingRight:
						style.paddingRight = (StyleLength)value;
						return;
					case StylePropertyId.PaddingTop:
						style.paddingTop = (StyleLength)value;
						return;
					case StylePropertyId.Position:
						style.position = (StyleEnum<Position>)value;
						return;
					case StylePropertyId.Right:
						style.right = (StyleLength)value;
						return;
					case StylePropertyId.Top:
						style.top = (StyleLength)value;
						return;
					case StylePropertyId.Width:
						style.width = (StyleLength)value;
						return;
					default:
						switch (id)
						{
						case StylePropertyId.Cursor:
							style.cursor = (StyleCursor)value;
							return;
						case StylePropertyId.TextOverflow:
							style.textOverflow = (StyleEnum<TextOverflow>)value;
							return;
						case StylePropertyId.UnityBackgroundImageTintColor:
							style.unityBackgroundImageTintColor = (StyleColor)value;
							return;
						case StylePropertyId.UnityBackgroundScaleMode:
							style.unityBackgroundScaleMode = (StyleEnum<ScaleMode>)value;
							return;
						case StylePropertyId.UnityOverflowClipBox:
							style.unityOverflowClipBox = (StyleEnum<OverflowClipBox>)value;
							return;
						case StylePropertyId.UnitySliceBottom:
							style.unitySliceBottom = (StyleInt)value;
							return;
						case StylePropertyId.UnitySliceLeft:
							style.unitySliceLeft = (StyleInt)value;
							return;
						case StylePropertyId.UnitySliceRight:
							style.unitySliceRight = (StyleInt)value;
							return;
						case StylePropertyId.UnitySliceTop:
							style.unitySliceTop = (StyleInt)value;
							return;
						case StylePropertyId.UnityTextOverflowPosition:
							style.unityTextOverflowPosition = (StyleEnum<TextOverflowPosition>)value;
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
					style.rotate = (StyleRotate)value;
					return;
				case StylePropertyId.Scale:
					style.scale = (StyleScale)value;
					return;
				case StylePropertyId.TransformOrigin:
					style.transformOrigin = (StyleTransformOrigin)value;
					return;
				case StylePropertyId.Translate:
					style.translate = (StyleTranslate)value;
					return;
				default:
					switch (id)
					{
					case StylePropertyId.TransitionDelay:
						style.transitionDelay = (StyleList<TimeValue>)value;
						return;
					case StylePropertyId.TransitionDuration:
						style.transitionDuration = (StyleList<TimeValue>)value;
						return;
					case StylePropertyId.TransitionProperty:
						style.transitionProperty = (StyleList<StylePropertyName>)value;
						return;
					case StylePropertyId.TransitionTimingFunction:
						style.transitionTimingFunction = (StyleList<EasingFunction>)value;
						return;
					default:
						switch (id)
						{
						case StylePropertyId.BackgroundColor:
							style.backgroundColor = (StyleColor)value;
							return;
						case StylePropertyId.BackgroundImage:
							style.backgroundImage = (StyleBackground)value;
							return;
						case StylePropertyId.BorderBottomColor:
							style.borderBottomColor = (StyleColor)value;
							return;
						case StylePropertyId.BorderBottomLeftRadius:
							style.borderBottomLeftRadius = (StyleLength)value;
							return;
						case StylePropertyId.BorderBottomRightRadius:
							style.borderBottomRightRadius = (StyleLength)value;
							return;
						case StylePropertyId.BorderLeftColor:
							style.borderLeftColor = (StyleColor)value;
							return;
						case StylePropertyId.BorderRightColor:
							style.borderRightColor = (StyleColor)value;
							return;
						case StylePropertyId.BorderTopColor:
							style.borderTopColor = (StyleColor)value;
							return;
						case StylePropertyId.BorderTopLeftRadius:
							style.borderTopLeftRadius = (StyleLength)value;
							return;
						case StylePropertyId.BorderTopRightRadius:
							style.borderTopRightRadius = (StyleLength)value;
							return;
						case StylePropertyId.Opacity:
							style.opacity = (StyleFloat)value;
							return;
						case StylePropertyId.Overflow:
							style.overflow = (StyleEnum<Overflow>)value;
							return;
						}
						break;
					}
					break;
				}
			}
			Debug.LogAssertion(string.Format("Cannot set inline style value for property id {0}", id));
		}

		// Token: 0x060014B0 RID: 5296 RVA: 0x000598C4 File Offset: 0x00057AC4
		public static Type GetInlineStyleType(StylePropertyId id)
		{
			if (id <= StylePropertyId.UnityTextOverflowPosition)
			{
				switch (id)
				{
				case StylePropertyId.Color:
					return typeof(StyleColor);
				case StylePropertyId.FontSize:
					return typeof(StyleLength);
				case StylePropertyId.LetterSpacing:
					return typeof(StyleLength);
				case StylePropertyId.TextShadow:
					return typeof(StyleTextShadow);
				case StylePropertyId.UnityFont:
					return typeof(StyleFont);
				case StylePropertyId.UnityFontDefinition:
					return typeof(StyleFontDefinition);
				case StylePropertyId.UnityFontStyleAndWeight:
					return typeof(StyleEnum<FontStyle>);
				case StylePropertyId.UnityParagraphSpacing:
					return typeof(StyleLength);
				case StylePropertyId.UnityTextAlign:
					return typeof(StyleEnum<TextAnchor>);
				case StylePropertyId.UnityTextOutlineColor:
					return typeof(StyleColor);
				case StylePropertyId.UnityTextOutlineWidth:
					return typeof(StyleFloat);
				case StylePropertyId.Visibility:
					return typeof(StyleEnum<Visibility>);
				case StylePropertyId.WhiteSpace:
					return typeof(StyleEnum<WhiteSpace>);
				case StylePropertyId.WordSpacing:
					return typeof(StyleLength);
				default:
					switch (id)
					{
					case StylePropertyId.AlignContent:
						return typeof(StyleEnum<Align>);
					case StylePropertyId.AlignItems:
						return typeof(StyleEnum<Align>);
					case StylePropertyId.AlignSelf:
						return typeof(StyleEnum<Align>);
					case StylePropertyId.BorderBottomWidth:
						return typeof(StyleFloat);
					case StylePropertyId.BorderLeftWidth:
						return typeof(StyleFloat);
					case StylePropertyId.BorderRightWidth:
						return typeof(StyleFloat);
					case StylePropertyId.BorderTopWidth:
						return typeof(StyleFloat);
					case StylePropertyId.Bottom:
						return typeof(StyleLength);
					case StylePropertyId.Display:
						return typeof(StyleEnum<DisplayStyle>);
					case StylePropertyId.FlexBasis:
						return typeof(StyleLength);
					case StylePropertyId.FlexDirection:
						return typeof(StyleEnum<FlexDirection>);
					case StylePropertyId.FlexGrow:
						return typeof(StyleFloat);
					case StylePropertyId.FlexShrink:
						return typeof(StyleFloat);
					case StylePropertyId.FlexWrap:
						return typeof(StyleEnum<Wrap>);
					case StylePropertyId.Height:
						return typeof(StyleLength);
					case StylePropertyId.JustifyContent:
						return typeof(StyleEnum<Justify>);
					case StylePropertyId.Left:
						return typeof(StyleLength);
					case StylePropertyId.MarginBottom:
						return typeof(StyleLength);
					case StylePropertyId.MarginLeft:
						return typeof(StyleLength);
					case StylePropertyId.MarginRight:
						return typeof(StyleLength);
					case StylePropertyId.MarginTop:
						return typeof(StyleLength);
					case StylePropertyId.MaxHeight:
						return typeof(StyleLength);
					case StylePropertyId.MaxWidth:
						return typeof(StyleLength);
					case StylePropertyId.MinHeight:
						return typeof(StyleLength);
					case StylePropertyId.MinWidth:
						return typeof(StyleLength);
					case StylePropertyId.PaddingBottom:
						return typeof(StyleLength);
					case StylePropertyId.PaddingLeft:
						return typeof(StyleLength);
					case StylePropertyId.PaddingRight:
						return typeof(StyleLength);
					case StylePropertyId.PaddingTop:
						return typeof(StyleLength);
					case StylePropertyId.Position:
						return typeof(StyleEnum<Position>);
					case StylePropertyId.Right:
						return typeof(StyleLength);
					case StylePropertyId.Top:
						return typeof(StyleLength);
					case StylePropertyId.Width:
						return typeof(StyleLength);
					default:
						switch (id)
						{
						case StylePropertyId.Cursor:
							return typeof(StyleCursor);
						case StylePropertyId.TextOverflow:
							return typeof(StyleEnum<TextOverflow>);
						case StylePropertyId.UnityBackgroundImageTintColor:
							return typeof(StyleColor);
						case StylePropertyId.UnityBackgroundScaleMode:
							return typeof(StyleEnum<ScaleMode>);
						case StylePropertyId.UnityOverflowClipBox:
							return typeof(StyleEnum<OverflowClipBox>);
						case StylePropertyId.UnitySliceBottom:
							return typeof(StyleInt);
						case StylePropertyId.UnitySliceLeft:
							return typeof(StyleInt);
						case StylePropertyId.UnitySliceRight:
							return typeof(StyleInt);
						case StylePropertyId.UnitySliceTop:
							return typeof(StyleInt);
						case StylePropertyId.UnityTextOverflowPosition:
							return typeof(StyleEnum<TextOverflowPosition>);
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
					return typeof(StyleRotate);
				case StylePropertyId.Scale:
					return typeof(StyleScale);
				case StylePropertyId.TransformOrigin:
					return typeof(StyleTransformOrigin);
				case StylePropertyId.Translate:
					return typeof(StyleTranslate);
				default:
					switch (id)
					{
					case StylePropertyId.TransitionDelay:
						return typeof(StyleList<TimeValue>);
					case StylePropertyId.TransitionDuration:
						return typeof(StyleList<TimeValue>);
					case StylePropertyId.TransitionProperty:
						return typeof(StyleList<StylePropertyName>);
					case StylePropertyId.TransitionTimingFunction:
						return typeof(StyleList<EasingFunction>);
					default:
						switch (id)
						{
						case StylePropertyId.BackgroundColor:
							return typeof(StyleColor);
						case StylePropertyId.BackgroundImage:
							return typeof(StyleBackground);
						case StylePropertyId.BorderBottomColor:
							return typeof(StyleColor);
						case StylePropertyId.BorderBottomLeftRadius:
							return typeof(StyleLength);
						case StylePropertyId.BorderBottomRightRadius:
							return typeof(StyleLength);
						case StylePropertyId.BorderLeftColor:
							return typeof(StyleColor);
						case StylePropertyId.BorderRightColor:
							return typeof(StyleColor);
						case StylePropertyId.BorderTopColor:
							return typeof(StyleColor);
						case StylePropertyId.BorderTopLeftRadius:
							return typeof(StyleLength);
						case StylePropertyId.BorderTopRightRadius:
							return typeof(StyleLength);
						case StylePropertyId.Opacity:
							return typeof(StyleFloat);
						case StylePropertyId.Overflow:
							return typeof(StyleEnum<Overflow>);
						}
						break;
					}
					break;
				}
			}
			Debug.LogAssertion(string.Format("Cannot get computed style type for property id {0}", id));
			return null;
		}

		// Token: 0x060014B1 RID: 5297 RVA: 0x00059F44 File Offset: 0x00058144
		public static string[] GetLonghandPropertyNames(StylePropertyId id)
		{
			string[] array;
			switch (id)
			{
			case StylePropertyId.All:
				array = new string[0];
				break;
			case StylePropertyId.BorderColor:
				array = new string[] { "border-top-color", "border-right-color", "border-bottom-color", "border-left-color" };
				break;
			case StylePropertyId.BorderRadius:
				array = new string[] { "border-top-left-radius", "border-top-right-radius", "border-bottom-right-radius", "border-bottom-left-radius" };
				break;
			case StylePropertyId.BorderWidth:
				array = new string[] { "border-top-width", "border-right-width", "border-bottom-width", "border-left-width" };
				break;
			case StylePropertyId.Flex:
				array = new string[] { "flex-grow", "flex-shrink", "flex-basis" };
				break;
			case StylePropertyId.Margin:
				array = new string[] { "margin-top", "margin-right", "margin-bottom", "margin-left" };
				break;
			case StylePropertyId.Padding:
				array = new string[] { "padding-top", "padding-right", "padding-bottom", "padding-left" };
				break;
			case StylePropertyId.Transition:
				array = new string[] { "transition-delay", "transition-duration", "transition-property", "transition-timing-function" };
				break;
			case StylePropertyId.UnityTextOutline:
				array = new string[] { "-unity-text-outline-color", "-unity-text-outline-width" };
				break;
			default:
				Debug.LogAssertion(string.Format("Cannot get longhand property names for property id {0}", id));
				array = null;
				break;
			}
			return array;
		}

		// Token: 0x060014B2 RID: 5298 RVA: 0x0005A0F4 File Offset: 0x000582F4
		public static bool IsShorthandProperty(StylePropertyId id)
		{
			bool flag;
			switch (id)
			{
			case StylePropertyId.All:
				flag = true;
				break;
			case StylePropertyId.BorderColor:
				flag = true;
				break;
			case StylePropertyId.BorderRadius:
				flag = true;
				break;
			case StylePropertyId.BorderWidth:
				flag = true;
				break;
			case StylePropertyId.Flex:
				flag = true;
				break;
			case StylePropertyId.Margin:
				flag = true;
				break;
			case StylePropertyId.Padding:
				flag = true;
				break;
			case StylePropertyId.Transition:
				flag = true;
				break;
			case StylePropertyId.UnityTextOutline:
				flag = true;
				break;
			default:
				flag = false;
				break;
			}
			return flag;
		}

		// Token: 0x060014B3 RID: 5299 RVA: 0x0005A164 File Offset: 0x00058364
		public static bool IsInheritedProperty(StylePropertyId id)
		{
			bool flag;
			switch (id)
			{
			case StylePropertyId.Color:
				flag = true;
				break;
			case StylePropertyId.FontSize:
				flag = true;
				break;
			case StylePropertyId.LetterSpacing:
				flag = true;
				break;
			case StylePropertyId.TextShadow:
				flag = true;
				break;
			case StylePropertyId.UnityFont:
				flag = true;
				break;
			case StylePropertyId.UnityFontDefinition:
				flag = true;
				break;
			case StylePropertyId.UnityFontStyleAndWeight:
				flag = true;
				break;
			case StylePropertyId.UnityParagraphSpacing:
				flag = true;
				break;
			case StylePropertyId.UnityTextAlign:
				flag = true;
				break;
			case StylePropertyId.UnityTextOutlineColor:
				flag = true;
				break;
			case StylePropertyId.UnityTextOutlineWidth:
				flag = true;
				break;
			case StylePropertyId.Visibility:
				flag = true;
				break;
			case StylePropertyId.WhiteSpace:
				flag = true;
				break;
			case StylePropertyId.WordSpacing:
				flag = true;
				break;
			default:
				flag = false;
				break;
			}
			return flag;
		}

		// Token: 0x060014B4 RID: 5300 RVA: 0x0005A1FC File Offset: 0x000583FC
		public static StylePropertyId[] GetInheritedProperties()
		{
			return new StylePropertyId[]
			{
				StylePropertyId.Color,
				StylePropertyId.FontSize,
				StylePropertyId.LetterSpacing,
				StylePropertyId.TextShadow,
				StylePropertyId.UnityFont,
				StylePropertyId.UnityFontDefinition,
				StylePropertyId.UnityFontStyleAndWeight,
				StylePropertyId.UnityParagraphSpacing,
				StylePropertyId.UnityTextAlign,
				StylePropertyId.UnityTextOutlineColor,
				StylePropertyId.UnityTextOutlineWidth,
				StylePropertyId.Visibility,
				StylePropertyId.WhiteSpace,
				StylePropertyId.WordSpacing
			};
		}

		// Token: 0x04000902 RID: 2306
		internal const int UnitySpecificity = -1;

		// Token: 0x04000903 RID: 2307
		internal const int UndefinedSpecificity = 0;

		// Token: 0x04000904 RID: 2308
		internal const int InheritedSpecificity = 2147483646;

		// Token: 0x04000905 RID: 2309
		internal const int InlineSpecificity = 2147483647;
	}
}
