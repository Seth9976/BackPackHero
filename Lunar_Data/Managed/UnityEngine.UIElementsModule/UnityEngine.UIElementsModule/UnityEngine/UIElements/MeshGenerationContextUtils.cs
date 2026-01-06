using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000259 RID: 601
	internal static class MeshGenerationContextUtils
	{
		// Token: 0x06001216 RID: 4630 RVA: 0x00046200 File Offset: 0x00044400
		public static void Rectangle(this MeshGenerationContext mgc, MeshGenerationContextUtils.RectangleParams rectParams)
		{
			mgc.painter.DrawRectangle(rectParams);
		}

		// Token: 0x06001217 RID: 4631 RVA: 0x00046210 File Offset: 0x00044410
		public static void Border(this MeshGenerationContext mgc, MeshGenerationContextUtils.BorderParams borderParams)
		{
			mgc.painter.DrawBorder(borderParams);
		}

		// Token: 0x06001218 RID: 4632 RVA: 0x00046220 File Offset: 0x00044420
		public static void Text(this MeshGenerationContext mgc, MeshGenerationContextUtils.TextParams textParams, ITextHandle handle, float pixelsPerPoint)
		{
			bool flag = TextUtilities.IsFontAssigned(textParams);
			if (flag)
			{
				mgc.painter.DrawText(textParams, handle, pixelsPerPoint);
			}
		}

		// Token: 0x06001219 RID: 4633 RVA: 0x00046248 File Offset: 0x00044448
		private static Vector2 ConvertBorderRadiusPercentToPoints(Vector2 borderRectSize, Length length)
		{
			float num = length.value;
			float num2 = length.value;
			bool flag = length.unit == LengthUnit.Percent;
			if (flag)
			{
				num = borderRectSize.x * length.value / 100f;
				num2 = borderRectSize.y * length.value / 100f;
			}
			num = Mathf.Max(num, 0f);
			num2 = Mathf.Max(num2, 0f);
			return new Vector2(num, num2);
		}

		// Token: 0x0600121A RID: 4634 RVA: 0x000462C4 File Offset: 0x000444C4
		public unsafe static void GetVisualElementRadii(VisualElement ve, out Vector2 topLeft, out Vector2 bottomLeft, out Vector2 topRight, out Vector2 bottomRight)
		{
			IResolvedStyle resolvedStyle = ve.resolvedStyle;
			Vector2 vector = new Vector2(resolvedStyle.width, resolvedStyle.height);
			ComputedStyle computedStyle = *ve.computedStyle;
			topLeft = MeshGenerationContextUtils.ConvertBorderRadiusPercentToPoints(vector, computedStyle.borderTopLeftRadius);
			bottomLeft = MeshGenerationContextUtils.ConvertBorderRadiusPercentToPoints(vector, computedStyle.borderBottomLeftRadius);
			topRight = MeshGenerationContextUtils.ConvertBorderRadiusPercentToPoints(vector, computedStyle.borderTopRightRadius);
			bottomRight = MeshGenerationContextUtils.ConvertBorderRadiusPercentToPoints(vector, computedStyle.borderBottomRightRadius);
		}

		// Token: 0x0600121B RID: 4635 RVA: 0x00046348 File Offset: 0x00044548
		public static void AdjustBackgroundSizeForBorders(VisualElement visualElement, ref Rect rect)
		{
			IResolvedStyle resolvedStyle = visualElement.resolvedStyle;
			bool flag = resolvedStyle.borderLeftWidth >= 1f && resolvedStyle.borderLeftColor.a >= 1f;
			if (flag)
			{
				rect.x += 0.5f;
				rect.width -= 0.5f;
			}
			bool flag2 = resolvedStyle.borderTopWidth >= 1f && resolvedStyle.borderTopColor.a >= 1f;
			if (flag2)
			{
				rect.y += 0.5f;
				rect.height -= 0.5f;
			}
			bool flag3 = resolvedStyle.borderRightWidth >= 1f && resolvedStyle.borderRightColor.a >= 1f;
			if (flag3)
			{
				rect.width -= 0.5f;
			}
			bool flag4 = resolvedStyle.borderBottomWidth >= 1f && resolvedStyle.borderBottomColor.a >= 1f;
			if (flag4)
			{
				rect.height -= 0.5f;
			}
		}

		// Token: 0x0200025A RID: 602
		public struct BorderParams
		{
			// Token: 0x04000820 RID: 2080
			public Rect rect;

			// Token: 0x04000821 RID: 2081
			public Color playmodeTintColor;

			// Token: 0x04000822 RID: 2082
			public Color leftColor;

			// Token: 0x04000823 RID: 2083
			public Color topColor;

			// Token: 0x04000824 RID: 2084
			public Color rightColor;

			// Token: 0x04000825 RID: 2085
			public Color bottomColor;

			// Token: 0x04000826 RID: 2086
			public float leftWidth;

			// Token: 0x04000827 RID: 2087
			public float topWidth;

			// Token: 0x04000828 RID: 2088
			public float rightWidth;

			// Token: 0x04000829 RID: 2089
			public float bottomWidth;

			// Token: 0x0400082A RID: 2090
			public Vector2 topLeftRadius;

			// Token: 0x0400082B RID: 2091
			public Vector2 topRightRadius;

			// Token: 0x0400082C RID: 2092
			public Vector2 bottomRightRadius;

			// Token: 0x0400082D RID: 2093
			public Vector2 bottomLeftRadius;

			// Token: 0x0400082E RID: 2094
			public Material material;

			// Token: 0x0400082F RID: 2095
			internal ColorPage leftColorPage;

			// Token: 0x04000830 RID: 2096
			internal ColorPage topColorPage;

			// Token: 0x04000831 RID: 2097
			internal ColorPage rightColorPage;

			// Token: 0x04000832 RID: 2098
			internal ColorPage bottomColorPage;
		}

		// Token: 0x0200025B RID: 603
		public struct RectangleParams
		{
			// Token: 0x0600121C RID: 4636 RVA: 0x00046480 File Offset: 0x00044680
			public static MeshGenerationContextUtils.RectangleParams MakeSolid(Rect rect, Color color, ContextType panelContext)
			{
				Color color2 = ((panelContext == ContextType.Editor) ? UIElementsUtility.editorPlayModeTintColor : Color.white);
				return new MeshGenerationContextUtils.RectangleParams
				{
					rect = rect,
					color = color,
					uv = new Rect(0f, 0f, 1f, 1f),
					playmodeTintColor = color2
				};
			}

			// Token: 0x0600121D RID: 4637 RVA: 0x000464E4 File Offset: 0x000446E4
			private static void AdjustUVsForScaleMode(Rect rect, Rect uv, Texture texture, ScaleMode scaleMode, out Rect rectOut, out Rect uvOut)
			{
				float num = Mathf.Abs((float)texture.width * uv.width / ((float)texture.height * uv.height));
				float num2 = rect.width / rect.height;
				switch (scaleMode)
				{
				case ScaleMode.StretchToFill:
					break;
				case ScaleMode.ScaleAndCrop:
				{
					bool flag = num2 > num;
					if (flag)
					{
						float num3 = uv.height * (num / num2);
						float num4 = (uv.height - num3) * 0.5f;
						uv = new Rect(uv.x, uv.y + num4, uv.width, num3);
					}
					else
					{
						float num5 = uv.width * (num2 / num);
						float num6 = (uv.width - num5) * 0.5f;
						uv = new Rect(uv.x + num6, uv.y, num5, uv.height);
					}
					break;
				}
				case ScaleMode.ScaleToFit:
				{
					bool flag2 = num2 > num;
					if (flag2)
					{
						float num7 = num / num2;
						rect = new Rect(rect.xMin + rect.width * (1f - num7) * 0.5f, rect.yMin, num7 * rect.width, rect.height);
					}
					else
					{
						float num8 = num2 / num;
						rect = new Rect(rect.xMin, rect.yMin + rect.height * (1f - num8) * 0.5f, rect.width, num8 * rect.height);
					}
					break;
				}
				default:
					throw new NotImplementedException();
				}
				rectOut = rect;
				uvOut = uv;
			}

			// Token: 0x0600121E RID: 4638 RVA: 0x00046688 File Offset: 0x00044888
			private static void AdjustSpriteUVsForScaleMode(Rect containerRect, Rect srcRect, Rect spriteGeomRect, Sprite sprite, ScaleMode scaleMode, out Rect rectOut, out Rect uvOut)
			{
				float num = sprite.rect.width / sprite.rect.height;
				float num2 = containerRect.width / containerRect.height;
				Rect rect = spriteGeomRect;
				rect.position -= sprite.bounds.min;
				rect.position /= sprite.bounds.size;
				rect.size /= sprite.bounds.size;
				Vector2 position = rect.position;
				position.y = 1f - rect.size.y - position.y;
				rect.position = position;
				switch (scaleMode)
				{
				case ScaleMode.StretchToFill:
				{
					Vector2 size = containerRect.size;
					containerRect.position = rect.position * size;
					containerRect.size = rect.size * size;
					break;
				}
				case ScaleMode.ScaleAndCrop:
				{
					Rect rect2 = containerRect;
					bool flag = num2 > num;
					if (flag)
					{
						rect2.height = rect2.width / num;
						rect2.position = new Vector2(rect2.position.x, -(rect2.height - containerRect.height) / 2f);
					}
					else
					{
						rect2.width = rect2.height * num;
						rect2.position = new Vector2(-(rect2.width - containerRect.width) / 2f, rect2.position.y);
					}
					Vector2 size2 = rect2.size;
					rect2.position += rect.position * size2;
					rect2.size = rect.size * size2;
					Rect rect3 = MeshGenerationContextUtils.RectangleParams.RectIntersection(containerRect, rect2);
					bool flag2 = rect3.width < 1E-30f || rect3.height < 1E-30f;
					if (flag2)
					{
						rect3 = Rect.zero;
					}
					else
					{
						Rect rect4 = rect3;
						rect4.position -= rect2.position;
						rect4.position /= rect2.size;
						rect4.size /= rect2.size;
						Vector2 position2 = rect4.position;
						position2.y = 1f - rect4.size.y - position2.y;
						rect4.position = position2;
						srcRect.position += rect4.position * srcRect.size;
						srcRect.size *= rect4.size;
					}
					containerRect = rect3;
					break;
				}
				case ScaleMode.ScaleToFit:
				{
					bool flag3 = num2 > num;
					if (flag3)
					{
						float num3 = num / num2;
						containerRect = new Rect(containerRect.xMin + containerRect.width * (1f - num3) * 0.5f, containerRect.yMin, num3 * containerRect.width, containerRect.height);
					}
					else
					{
						float num4 = num2 / num;
						containerRect = new Rect(containerRect.xMin, containerRect.yMin + containerRect.height * (1f - num4) * 0.5f, containerRect.width, num4 * containerRect.height);
					}
					containerRect.position += rect.position * containerRect.size;
					containerRect.size *= rect.size;
					break;
				}
				default:
					throw new NotImplementedException();
				}
				rectOut = containerRect;
				uvOut = srcRect;
			}

			// Token: 0x0600121F RID: 4639 RVA: 0x00046A9C File Offset: 0x00044C9C
			private static Rect RectIntersection(Rect a, Rect b)
			{
				Rect zero = Rect.zero;
				zero.min = Vector2.Max(a.min, b.min);
				zero.max = Vector2.Min(a.max, b.max);
				zero.size = Vector2.Max(zero.size, Vector2.zero);
				return zero;
			}

			// Token: 0x06001220 RID: 4640 RVA: 0x00046B04 File Offset: 0x00044D04
			private static Rect ComputeGeomRect(Sprite sprite)
			{
				Vector2 vector = new Vector2(float.MaxValue, float.MaxValue);
				Vector2 vector2 = new Vector2(float.MinValue, float.MinValue);
				foreach (Vector2 vector3 in sprite.vertices)
				{
					vector = Vector2.Min(vector, vector3);
					vector2 = Vector2.Max(vector2, vector3);
				}
				return new Rect(vector, vector2 - vector);
			}

			// Token: 0x06001221 RID: 4641 RVA: 0x00046B7C File Offset: 0x00044D7C
			private static Rect ComputeUVRect(Sprite sprite)
			{
				Vector2 vector = new Vector2(float.MaxValue, float.MaxValue);
				Vector2 vector2 = new Vector2(float.MinValue, float.MinValue);
				foreach (Vector2 vector3 in sprite.uv)
				{
					vector = Vector2.Min(vector, vector3);
					vector2 = Vector2.Max(vector2, vector3);
				}
				return new Rect(vector, vector2 - vector);
			}

			// Token: 0x06001222 RID: 4642 RVA: 0x00046BF4 File Offset: 0x00044DF4
			private static Rect ApplyPackingRotation(Rect uv, SpritePackingRotation rotation)
			{
				switch (rotation)
				{
				case SpritePackingRotation.FlipHorizontal:
				{
					uv.position += new Vector2(uv.size.x, 0f);
					Vector2 size = uv.size;
					size.x = -size.x;
					uv.size = size;
					break;
				}
				case SpritePackingRotation.FlipVertical:
				{
					uv.position += new Vector2(0f, uv.size.y);
					Vector2 size2 = uv.size;
					size2.y = -size2.y;
					uv.size = size2;
					break;
				}
				case SpritePackingRotation.Rotate180:
					uv.position += uv.size;
					uv.size = -uv.size;
					break;
				}
				return uv;
			}

			// Token: 0x06001223 RID: 4643 RVA: 0x00046CF8 File Offset: 0x00044EF8
			public static MeshGenerationContextUtils.RectangleParams MakeTextured(Rect rect, Rect uv, Texture texture, ScaleMode scaleMode, ContextType panelContext)
			{
				Color color = ((panelContext == ContextType.Editor) ? UIElementsUtility.editorPlayModeTintColor : Color.white);
				MeshGenerationContextUtils.RectangleParams.AdjustUVsForScaleMode(rect, uv, texture, scaleMode, out rect, out uv);
				return new MeshGenerationContextUtils.RectangleParams
				{
					rect = rect,
					uv = uv,
					color = Color.white,
					texture = texture,
					scaleMode = scaleMode,
					playmodeTintColor = color
				};
			}

			// Token: 0x06001224 RID: 4644 RVA: 0x00046D6C File Offset: 0x00044F6C
			public static MeshGenerationContextUtils.RectangleParams MakeSprite(Rect containerRect, Rect subRect, Sprite sprite, ScaleMode scaleMode, ContextType panelContext, bool hasRadius, ref Vector4 slices)
			{
				bool flag = sprite.texture == null;
				MeshGenerationContextUtils.RectangleParams rectangleParams2;
				if (flag)
				{
					Debug.LogWarning("Ignoring textureless sprite named \"" + sprite.name + "\", please import as a VectorImage instead");
					MeshGenerationContextUtils.RectangleParams rectangleParams = default(MeshGenerationContextUtils.RectangleParams);
					rectangleParams2 = rectangleParams;
				}
				else
				{
					Color color = ((panelContext == ContextType.Editor) ? UIElementsUtility.editorPlayModeTintColor : Color.white);
					Rect rect = MeshGenerationContextUtils.RectangleParams.ComputeGeomRect(sprite);
					Rect rect2 = MeshGenerationContextUtils.RectangleParams.ComputeUVRect(sprite);
					Vector4 border = sprite.border;
					bool flag2 = border != Vector4.zero || slices != Vector4.zero;
					bool flag3 = subRect != new Rect(0f, 0f, 1f, 1f);
					bool flag4 = scaleMode == ScaleMode.ScaleAndCrop || flag2 || hasRadius || flag3;
					bool flag5 = flag4 && sprite.packed && sprite.packingRotation > SpritePackingRotation.None;
					if (flag5)
					{
						rect2 = MeshGenerationContextUtils.RectangleParams.ApplyPackingRotation(rect2, sprite.packingRotation);
					}
					bool flag6 = flag3;
					Rect rect3;
					if (flag6)
					{
						rect3 = subRect;
						rect3.position *= rect2.size;
						rect3.position += rect2.position;
						rect3.size *= rect2.size;
					}
					else
					{
						rect3 = rect2;
					}
					Rect rect4;
					Rect rect5;
					MeshGenerationContextUtils.RectangleParams.AdjustSpriteUVsForScaleMode(containerRect, rect3, rect, sprite, scaleMode, out rect4, out rect5);
					MeshGenerationContextUtils.RectangleParams rectangleParams = new MeshGenerationContextUtils.RectangleParams
					{
						rect = rect4,
						uv = rect5,
						color = Color.white,
						texture = (flag4 ? sprite.texture : null),
						sprite = (flag4 ? null : sprite),
						spriteGeomRect = rect,
						scaleMode = scaleMode,
						playmodeTintColor = color,
						meshFlags = (sprite.packed ? MeshGenerationContext.MeshFlags.SkipDynamicAtlas : MeshGenerationContext.MeshFlags.None)
					};
					MeshGenerationContextUtils.RectangleParams rectangleParams3 = rectangleParams;
					Vector4 vector = new Vector4(border.x, border.w, border.z, border.y);
					bool flag7 = slices != Vector4.zero && vector != Vector4.zero && vector != slices;
					if (flag7)
					{
						Debug.LogWarning(string.Format("Sprite \"{0}\" borders {1} are overridden by style slices {2}", sprite.name, vector, slices));
					}
					else
					{
						bool flag8 = slices == Vector4.zero;
						if (flag8)
						{
							slices = vector;
						}
					}
					rectangleParams2 = rectangleParams3;
				}
				return rectangleParams2;
			}

			// Token: 0x06001225 RID: 4645 RVA: 0x00046FF0 File Offset: 0x000451F0
			public static MeshGenerationContextUtils.RectangleParams MakeVectorTextured(Rect rect, Rect uv, VectorImage vectorImage, ScaleMode scaleMode, ContextType panelContext)
			{
				Color color = ((panelContext == ContextType.Editor) ? UIElementsUtility.editorPlayModeTintColor : Color.white);
				return new MeshGenerationContextUtils.RectangleParams
				{
					rect = rect,
					uv = uv,
					color = Color.white,
					vectorImage = vectorImage,
					scaleMode = scaleMode,
					playmodeTintColor = color
				};
			}

			// Token: 0x06001226 RID: 4646 RVA: 0x00047054 File Offset: 0x00045254
			internal bool HasRadius(float epsilon)
			{
				return (this.topLeftRadius.x > epsilon && this.topLeftRadius.y > epsilon) || (this.topRightRadius.x > epsilon && this.topRightRadius.y > epsilon) || (this.bottomRightRadius.x > epsilon && this.bottomRightRadius.y > epsilon) || (this.bottomLeftRadius.x > epsilon && this.bottomLeftRadius.y > epsilon);
			}

			// Token: 0x04000833 RID: 2099
			public Rect rect;

			// Token: 0x04000834 RID: 2100
			public Rect uv;

			// Token: 0x04000835 RID: 2101
			public Color color;

			// Token: 0x04000836 RID: 2102
			public Texture texture;

			// Token: 0x04000837 RID: 2103
			public Sprite sprite;

			// Token: 0x04000838 RID: 2104
			public VectorImage vectorImage;

			// Token: 0x04000839 RID: 2105
			public Material material;

			// Token: 0x0400083A RID: 2106
			public ScaleMode scaleMode;

			// Token: 0x0400083B RID: 2107
			public Color playmodeTintColor;

			// Token: 0x0400083C RID: 2108
			public Vector2 topLeftRadius;

			// Token: 0x0400083D RID: 2109
			public Vector2 topRightRadius;

			// Token: 0x0400083E RID: 2110
			public Vector2 bottomRightRadius;

			// Token: 0x0400083F RID: 2111
			public Vector2 bottomLeftRadius;

			// Token: 0x04000840 RID: 2112
			public int leftSlice;

			// Token: 0x04000841 RID: 2113
			public int topSlice;

			// Token: 0x04000842 RID: 2114
			public int rightSlice;

			// Token: 0x04000843 RID: 2115
			public int bottomSlice;

			// Token: 0x04000844 RID: 2116
			public float sliceScale;

			// Token: 0x04000845 RID: 2117
			internal Rect spriteGeomRect;

			// Token: 0x04000846 RID: 2118
			internal ColorPage colorPage;

			// Token: 0x04000847 RID: 2119
			internal MeshGenerationContext.MeshFlags meshFlags;
		}

		// Token: 0x0200025C RID: 604
		public struct TextParams
		{
			// Token: 0x06001227 RID: 4647 RVA: 0x000470DC File Offset: 0x000452DC
			public override int GetHashCode()
			{
				int num = this.rect.GetHashCode();
				num = (num * 397) ^ ((this.text != null) ? this.text.GetHashCode() : 0);
				num = (num * 397) ^ ((this.font != null) ? this.font.GetHashCode() : 0);
				num = (num * 397) ^ this.fontDefinition.GetHashCode();
				num = (num * 397) ^ this.fontSize;
				num = (num * 397) ^ (int)this.fontStyle;
				num = (num * 397) ^ this.fontColor.GetHashCode();
				num = (num * 397) ^ (int)this.anchor;
				num = (num * 397) ^ this.wordWrap.GetHashCode();
				num = (num * 397) ^ this.wordWrapWidth.GetHashCode();
				num = (num * 397) ^ this.richText.GetHashCode();
				num = (num * 397) ^ this.playmodeTintColor.GetHashCode();
				num = (num * 397) ^ this.textOverflow.GetHashCode();
				num = (num * 397) ^ this.textOverflowPosition.GetHashCode();
				num = (num * 397) ^ this.overflow.GetHashCode();
				num = (num * 397) ^ this.letterSpacing.GetHashCode();
				num = (num * 397) ^ this.wordSpacing.GetHashCode();
				return (num * 397) ^ this.paragraphSpacing.GetHashCode();
			}

			// Token: 0x06001228 RID: 4648 RVA: 0x0004729C File Offset: 0x0004549C
			internal unsafe static MeshGenerationContextUtils.TextParams MakeStyleBased(VisualElement ve, string text)
			{
				ComputedStyle computedStyle = *ve.computedStyle;
				TextElement textElement = ve as TextElement;
				bool flag = textElement == null;
				MeshGenerationContextUtils.TextParams textParams = default(MeshGenerationContextUtils.TextParams);
				textParams.rect = ve.contentRect;
				textParams.text = text;
				textParams.fontDefinition = computedStyle.unityFontDefinition;
				textParams.font = TextUtilities.GetFont(ve);
				textParams.fontSize = (int)computedStyle.fontSize.value;
				textParams.fontStyle = computedStyle.unityFontStyleAndWeight;
				textParams.fontColor = computedStyle.color;
				textParams.anchor = computedStyle.unityTextAlign;
				textParams.wordWrap = computedStyle.whiteSpace == WhiteSpace.Normal;
				textParams.wordWrapWidth = ((computedStyle.whiteSpace == WhiteSpace.Normal) ? ve.contentRect.width : 0f);
				textParams.richText = textElement != null && textElement.enableRichText;
				IPanel panel = ve.panel;
				textParams.playmodeTintColor = ((panel != null && panel.contextType == ContextType.Editor) ? UIElementsUtility.editorPlayModeTintColor : Color.white);
				textParams.textOverflow = computedStyle.textOverflow;
				textParams.textOverflowPosition = computedStyle.unityTextOverflowPosition;
				textParams.overflow = computedStyle.overflow;
				textParams.letterSpacing = (flag ? 0f : computedStyle.letterSpacing);
				textParams.wordSpacing = (flag ? 0f : computedStyle.wordSpacing);
				textParams.paragraphSpacing = (flag ? 0f : computedStyle.unityParagraphSpacing);
				textParams.panel = ve.panel;
				return textParams;
			}

			// Token: 0x06001229 RID: 4649 RVA: 0x00047448 File Offset: 0x00045648
			internal static TextNativeSettings GetTextNativeSettings(MeshGenerationContextUtils.TextParams textParams, float scaling)
			{
				return new TextNativeSettings
				{
					text = textParams.text,
					font = TextUtilities.GetFont(textParams),
					size = textParams.fontSize,
					scaling = scaling,
					style = textParams.fontStyle,
					color = textParams.fontColor,
					anchor = textParams.anchor,
					wordWrap = textParams.wordWrap,
					wordWrapWidth = textParams.wordWrapWidth,
					richText = textParams.richText
				};
			}

			// Token: 0x04000848 RID: 2120
			public Rect rect;

			// Token: 0x04000849 RID: 2121
			public string text;

			// Token: 0x0400084A RID: 2122
			public Font font;

			// Token: 0x0400084B RID: 2123
			public FontDefinition fontDefinition;

			// Token: 0x0400084C RID: 2124
			public int fontSize;

			// Token: 0x0400084D RID: 2125
			public Length letterSpacing;

			// Token: 0x0400084E RID: 2126
			public Length wordSpacing;

			// Token: 0x0400084F RID: 2127
			public Length paragraphSpacing;

			// Token: 0x04000850 RID: 2128
			public FontStyle fontStyle;

			// Token: 0x04000851 RID: 2129
			public Color fontColor;

			// Token: 0x04000852 RID: 2130
			public TextAnchor anchor;

			// Token: 0x04000853 RID: 2131
			public bool wordWrap;

			// Token: 0x04000854 RID: 2132
			public float wordWrapWidth;

			// Token: 0x04000855 RID: 2133
			public bool richText;

			// Token: 0x04000856 RID: 2134
			public Color playmodeTintColor;

			// Token: 0x04000857 RID: 2135
			public TextOverflow textOverflow;

			// Token: 0x04000858 RID: 2136
			public TextOverflowPosition textOverflowPosition;

			// Token: 0x04000859 RID: 2137
			public OverflowInternal overflow;

			// Token: 0x0400085A RID: 2138
			public IPanel panel;
		}
	}
}
