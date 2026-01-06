using System;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements.UIR;

namespace UnityEngine.UIElements
{
	// Token: 0x020002B9 RID: 697
	internal static class TextUtilities
	{
		// Token: 0x0600177C RID: 6012 RVA: 0x0005F798 File Offset: 0x0005D998
		public static float ComputeTextScaling(Matrix4x4 worldMatrix, float pixelsPerPoint)
		{
			Vector3 vector = new Vector3(worldMatrix.m00, worldMatrix.m10, worldMatrix.m20);
			Vector3 vector2 = new Vector3(worldMatrix.m01, worldMatrix.m11, worldMatrix.m21);
			float num = (vector.magnitude + vector2.magnitude) / 2f;
			return num * pixelsPerPoint;
		}

		// Token: 0x0600177D RID: 6013 RVA: 0x0005F7F8 File Offset: 0x0005D9F8
		internal static Vector2 MeasureVisualElementTextSize(VisualElement ve, string textToMeasure, float width, VisualElement.MeasureMode widthMode, float height, VisualElement.MeasureMode heightMode, ITextHandle textHandle)
		{
			float num = float.NaN;
			float num2 = float.NaN;
			bool flag = textToMeasure == null || !TextUtilities.IsFontAssigned(ve);
			Vector2 vector;
			if (flag)
			{
				vector = new Vector2(num, num2);
			}
			else
			{
				float scaledPixelsPerPoint = ve.scaledPixelsPerPoint;
				bool flag2 = widthMode == VisualElement.MeasureMode.Exactly;
				if (flag2)
				{
					num = width;
				}
				else
				{
					MeshGenerationContextUtils.TextParams textParams = MeshGenerationContextUtils.TextParams.MakeStyleBased(ve, textToMeasure);
					textParams.wordWrap = false;
					textParams.rect = new Rect(textParams.rect.x, textParams.rect.y, width, height);
					num = textHandle.ComputeTextWidth(textParams, scaledPixelsPerPoint);
					bool flag3 = widthMode == VisualElement.MeasureMode.AtMost;
					if (flag3)
					{
						num = Mathf.Min(num, width);
					}
				}
				bool flag4 = heightMode == VisualElement.MeasureMode.Exactly;
				if (flag4)
				{
					num2 = height;
				}
				else
				{
					MeshGenerationContextUtils.TextParams textParams2 = MeshGenerationContextUtils.TextParams.MakeStyleBased(ve, textToMeasure);
					textParams2.wordWrapWidth = num;
					textParams2.rect = new Rect(textParams2.rect.x, textParams2.rect.y, width, height);
					num2 = textHandle.ComputeTextHeight(textParams2, scaledPixelsPerPoint);
					bool flag5 = heightMode == VisualElement.MeasureMode.AtMost;
					if (flag5)
					{
						num2 = Mathf.Min(num2, height);
					}
				}
				float num3 = AlignmentUtils.CeilToPixelGrid(num, scaledPixelsPerPoint, 0f);
				float num4 = AlignmentUtils.CeilToPixelGrid(num2, scaledPixelsPerPoint, 0f);
				Vector2 vector2 = new Vector2(num3, num4);
				textHandle.MeasuredSizes = new Vector2(num, num2);
				textHandle.RoundedSizes = vector2;
				vector = vector2;
			}
			return vector;
		}

		// Token: 0x0600177E RID: 6014 RVA: 0x0005F958 File Offset: 0x0005DB58
		internal static FontAsset GetFontAsset(MeshGenerationContextUtils.TextParams textParam)
		{
			PanelTextSettings textSettingsFrom = TextUtilities.GetTextSettingsFrom(textParam);
			bool flag = textParam.fontDefinition.fontAsset != null;
			FontAsset fontAsset;
			if (flag)
			{
				fontAsset = textParam.fontDefinition.fontAsset;
			}
			else
			{
				bool flag2 = textParam.fontDefinition.font != null;
				if (flag2)
				{
					fontAsset = textSettingsFrom.GetCachedFontAsset(textParam.fontDefinition.font);
				}
				else
				{
					fontAsset = textSettingsFrom.GetCachedFontAsset(textParam.font);
				}
			}
			return fontAsset;
		}

		// Token: 0x0600177F RID: 6015 RVA: 0x0005F9D0 File Offset: 0x0005DBD0
		internal static FontAsset GetFontAsset(VisualElement ve)
		{
			bool flag = ve.computedStyle.unityFontDefinition.fontAsset != null;
			FontAsset fontAsset;
			if (flag)
			{
				fontAsset = ve.computedStyle.unityFontDefinition.fontAsset;
			}
			else
			{
				PanelTextSettings textSettingsFrom = TextUtilities.GetTextSettingsFrom(ve);
				bool flag2 = ve.computedStyle.unityFontDefinition.font != null;
				if (flag2)
				{
					fontAsset = textSettingsFrom.GetCachedFontAsset(ve.computedStyle.unityFontDefinition.font);
				}
				else
				{
					fontAsset = textSettingsFrom.GetCachedFontAsset(ve.computedStyle.unityFont);
				}
			}
			return fontAsset;
		}

		// Token: 0x06001780 RID: 6016 RVA: 0x0005FA68 File Offset: 0x0005DC68
		internal static Font GetFont(MeshGenerationContextUtils.TextParams textParam)
		{
			bool flag = textParam.fontDefinition.font != null;
			Font font;
			if (flag)
			{
				font = textParam.fontDefinition.font;
			}
			else
			{
				bool flag2 = textParam.font != null;
				if (flag2)
				{
					font = textParam.font;
				}
				else
				{
					FontAsset fontAsset = textParam.fontDefinition.fontAsset;
					font = ((fontAsset != null) ? fontAsset.sourceFontFile : null);
				}
			}
			return font;
		}

		// Token: 0x06001781 RID: 6017 RVA: 0x0005FAD0 File Offset: 0x0005DCD0
		internal unsafe static Font GetFont(VisualElement ve)
		{
			ComputedStyle computedStyle = *ve.computedStyle;
			bool flag = computedStyle.unityFontDefinition.font != null;
			Font font;
			if (flag)
			{
				font = computedStyle.unityFontDefinition.font;
			}
			else
			{
				bool flag2 = computedStyle.unityFont != null;
				if (flag2)
				{
					font = computedStyle.unityFont;
				}
				else
				{
					FontAsset fontAsset = computedStyle.unityFontDefinition.fontAsset;
					font = ((fontAsset != null) ? fontAsset.sourceFontFile : null);
				}
			}
			return font;
		}

		// Token: 0x06001782 RID: 6018 RVA: 0x0005FB54 File Offset: 0x0005DD54
		internal static bool IsFontAssigned(VisualElement ve)
		{
			return ve.computedStyle.unityFont != null || !ve.computedStyle.unityFontDefinition.IsEmpty();
		}

		// Token: 0x06001783 RID: 6019 RVA: 0x0005FB94 File Offset: 0x0005DD94
		internal static bool IsFontAssigned(MeshGenerationContextUtils.TextParams textParams)
		{
			return textParams.font != null || !textParams.fontDefinition.IsEmpty();
		}

		// Token: 0x06001784 RID: 6020 RVA: 0x0005FBC8 File Offset: 0x0005DDC8
		internal static PanelTextSettings GetTextSettingsFrom(VisualElement ve)
		{
			RuntimePanel runtimePanel = ve.panel as RuntimePanel;
			bool flag = runtimePanel != null;
			PanelTextSettings panelTextSettings;
			if (flag)
			{
				panelTextSettings = runtimePanel.panelSettings.textSettings ?? PanelTextSettings.defaultPanelTextSettings;
			}
			else
			{
				panelTextSettings = PanelTextSettings.defaultPanelTextSettings;
			}
			return panelTextSettings;
		}

		// Token: 0x06001785 RID: 6021 RVA: 0x0005FC0C File Offset: 0x0005DE0C
		internal static PanelTextSettings GetTextSettingsFrom(MeshGenerationContextUtils.TextParams textParam)
		{
			RuntimePanel runtimePanel = textParam.panel as RuntimePanel;
			bool flag = runtimePanel != null;
			PanelTextSettings panelTextSettings;
			if (flag)
			{
				panelTextSettings = runtimePanel.panelSettings.textSettings ?? PanelTextSettings.defaultPanelTextSettings;
			}
			else
			{
				panelTextSettings = PanelTextSettings.defaultPanelTextSettings;
			}
			return panelTextSettings;
		}

		// Token: 0x06001786 RID: 6022 RVA: 0x0005FC50 File Offset: 0x0005DE50
		internal unsafe static TextCoreSettings GetTextCoreSettingsForElement(VisualElement ve)
		{
			FontAsset fontAsset = TextUtilities.GetFontAsset(ve);
			bool flag = fontAsset == null;
			TextCoreSettings textCoreSettings;
			if (flag)
			{
				textCoreSettings = default(TextCoreSettings);
			}
			else
			{
				IResolvedStyle resolvedStyle = ve.resolvedStyle;
				ComputedStyle computedStyle = *ve.computedStyle;
				float num = 1f / (float)fontAsset.atlasPadding;
				float num2 = (float)fontAsset.faceInfo.pointSize / ve.computedStyle.fontSize.value;
				float num3 = num * num2;
				float num4 = Mathf.Max(0f, resolvedStyle.unityTextOutlineWidth * num3);
				float num5 = Mathf.Max(0f, computedStyle.textShadow.blurRadius * num3);
				Vector2 vector = computedStyle.textShadow.offset * num3;
				Color color = resolvedStyle.color;
				Color unityTextOutlineColor = resolvedStyle.unityTextOutlineColor;
				bool flag2 = num4 < 1E-30f;
				if (flag2)
				{
					unityTextOutlineColor.a = 0f;
				}
				textCoreSettings = new TextCoreSettings
				{
					faceColor = color,
					outlineColor = unityTextOutlineColor,
					outlineWidth = num4,
					underlayColor = computedStyle.textShadow.color,
					underlayOffset = vector,
					underlaySoftness = num5
				};
			}
			return textCoreSettings;
		}
	}
}
