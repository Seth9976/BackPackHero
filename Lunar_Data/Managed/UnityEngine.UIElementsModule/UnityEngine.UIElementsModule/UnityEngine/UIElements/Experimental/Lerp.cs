using System;
using UnityEngine.UIElements.StyleSheets;

namespace UnityEngine.UIElements.Experimental
{
	// Token: 0x02000382 RID: 898
	internal static class Lerp
	{
		// Token: 0x06001CCD RID: 7373 RVA: 0x00085BB4 File Offset: 0x00083DB4
		public static float Interpolate(float start, float end, float ratio)
		{
			return Mathf.LerpUnclamped(start, end, ratio);
		}

		// Token: 0x06001CCE RID: 7374 RVA: 0x00085BD0 File Offset: 0x00083DD0
		public static int Interpolate(int start, int end, float ratio)
		{
			return Mathf.RoundToInt(Mathf.LerpUnclamped((float)start, (float)end, ratio));
		}

		// Token: 0x06001CCF RID: 7375 RVA: 0x00085BF4 File Offset: 0x00083DF4
		public static Rect Interpolate(Rect r1, Rect r2, float ratio)
		{
			return new Rect(Mathf.LerpUnclamped(r1.x, r2.x, ratio), Mathf.LerpUnclamped(r1.y, r2.y, ratio), Mathf.LerpUnclamped(r1.width, r2.width, ratio), Mathf.LerpUnclamped(r1.height, r2.height, ratio));
		}

		// Token: 0x06001CD0 RID: 7376 RVA: 0x00085C5C File Offset: 0x00083E5C
		public static Color Interpolate(Color start, Color end, float ratio)
		{
			return Color.LerpUnclamped(start, end, ratio);
		}

		// Token: 0x06001CD1 RID: 7377 RVA: 0x00085C78 File Offset: 0x00083E78
		public static Vector2 Interpolate(Vector2 start, Vector2 end, float ratio)
		{
			return Vector2.LerpUnclamped(start, end, ratio);
		}

		// Token: 0x06001CD2 RID: 7378 RVA: 0x00085C94 File Offset: 0x00083E94
		public static Vector3 Interpolate(Vector3 start, Vector3 end, float ratio)
		{
			return Vector3.LerpUnclamped(start, end, ratio);
		}

		// Token: 0x06001CD3 RID: 7379 RVA: 0x00085CB0 File Offset: 0x00083EB0
		public static Quaternion Interpolate(Quaternion start, Quaternion end, float ratio)
		{
			return Quaternion.SlerpUnclamped(start, end, ratio);
		}

		// Token: 0x06001CD4 RID: 7380 RVA: 0x00085CCC File Offset: 0x00083ECC
		internal static StyleValues Interpolate(StyleValues start, StyleValues end, float ratio)
		{
			StyleValues styleValues = default(StyleValues);
			foreach (StyleValue styleValue in end.m_StyleValues.m_Values)
			{
				StyleValue styleValue2 = default(StyleValue);
				bool flag = !start.m_StyleValues.TryGetStyleValue(styleValue.id, ref styleValue2);
				if (flag)
				{
					throw new ArgumentException("Start StyleValues must contain the same values as end values. Missing property:" + styleValue.id.ToString());
				}
				StylePropertyId id = styleValue.id;
				StylePropertyId stylePropertyId = id;
				if (stylePropertyId <= StylePropertyId.Width)
				{
					if (stylePropertyId - StylePropertyId.Custom <= 1)
					{
						goto IL_0252;
					}
					switch (stylePropertyId)
					{
					case StylePropertyId.Color:
						goto IL_022E;
					case StylePropertyId.FontSize:
						goto IL_020A;
					case StylePropertyId.LetterSpacing:
					case StylePropertyId.TextShadow:
					case StylePropertyId.UnityFont:
					case StylePropertyId.UnityFontDefinition:
					case StylePropertyId.UnityFontStyleAndWeight:
					case StylePropertyId.UnityParagraphSpacing:
					case StylePropertyId.UnityTextAlign:
					case StylePropertyId.UnityTextOutlineColor:
					case StylePropertyId.UnityTextOutlineWidth:
					case StylePropertyId.Visibility:
					case StylePropertyId.WhiteSpace:
						goto IL_0252;
					default:
						switch (stylePropertyId)
						{
						case StylePropertyId.AlignContent:
						case StylePropertyId.AlignItems:
						case StylePropertyId.AlignSelf:
						case StylePropertyId.Display:
						case StylePropertyId.FlexDirection:
						case StylePropertyId.FlexWrap:
						case StylePropertyId.JustifyContent:
						case StylePropertyId.Position:
							goto IL_0252;
						case StylePropertyId.BorderBottomWidth:
						case StylePropertyId.BorderLeftWidth:
						case StylePropertyId.BorderRightWidth:
						case StylePropertyId.BorderTopWidth:
						case StylePropertyId.Bottom:
						case StylePropertyId.FlexBasis:
						case StylePropertyId.FlexGrow:
						case StylePropertyId.FlexShrink:
						case StylePropertyId.Height:
						case StylePropertyId.Left:
						case StylePropertyId.MarginBottom:
						case StylePropertyId.MarginLeft:
						case StylePropertyId.MarginRight:
						case StylePropertyId.MarginTop:
						case StylePropertyId.MaxHeight:
						case StylePropertyId.MaxWidth:
						case StylePropertyId.MinHeight:
						case StylePropertyId.MinWidth:
						case StylePropertyId.PaddingBottom:
						case StylePropertyId.PaddingLeft:
						case StylePropertyId.PaddingRight:
						case StylePropertyId.PaddingTop:
						case StylePropertyId.Right:
						case StylePropertyId.Top:
						case StylePropertyId.Width:
							goto IL_020A;
						default:
							goto IL_0252;
						}
						break;
					}
				}
				else if (stylePropertyId <= StylePropertyId.BorderColor)
				{
					switch (stylePropertyId)
					{
					case StylePropertyId.Cursor:
					case StylePropertyId.TextOverflow:
					case StylePropertyId.UnityBackgroundScaleMode:
					case StylePropertyId.UnityOverflowClipBox:
					case StylePropertyId.UnitySliceBottom:
					case StylePropertyId.UnitySliceLeft:
					case StylePropertyId.UnitySliceRight:
					case StylePropertyId.UnitySliceTop:
					case StylePropertyId.UnityTextOverflowPosition:
						goto IL_0252;
					case StylePropertyId.UnityBackgroundImageTintColor:
						goto IL_022E;
					default:
						if (stylePropertyId != StylePropertyId.BorderColor)
						{
							goto IL_0252;
						}
						goto IL_022E;
					}
				}
				else
				{
					if (stylePropertyId - StylePropertyId.BorderRadius <= 4)
					{
						goto IL_0252;
					}
					switch (stylePropertyId)
					{
					case StylePropertyId.BackgroundColor:
						goto IL_022E;
					case StylePropertyId.BackgroundImage:
					case StylePropertyId.BorderBottomColor:
					case StylePropertyId.BorderLeftColor:
					case StylePropertyId.BorderRightColor:
					case StylePropertyId.BorderTopColor:
					case StylePropertyId.Overflow:
						goto IL_0252;
					case StylePropertyId.BorderBottomLeftRadius:
					case StylePropertyId.BorderBottomRightRadius:
					case StylePropertyId.BorderTopLeftRadius:
					case StylePropertyId.BorderTopRightRadius:
					case StylePropertyId.Opacity:
						goto IL_020A;
					default:
						goto IL_0252;
					}
				}
				continue;
				IL_020A:
				styleValues.SetValue(styleValue.id, Lerp.Interpolate(styleValue2.number, styleValue.number, ratio));
				continue;
				IL_022E:
				styleValues.SetValue(styleValue.id, Lerp.Interpolate(styleValue2.color, styleValue.color, ratio));
				continue;
				IL_0252:
				throw new ArgumentException("Style Value can't be animated");
			}
			return styleValues;
		}
	}
}
