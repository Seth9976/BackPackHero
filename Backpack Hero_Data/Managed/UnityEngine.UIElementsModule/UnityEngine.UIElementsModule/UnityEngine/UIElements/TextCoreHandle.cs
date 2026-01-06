using System;
using UnityEngine.TextCore.Text;

namespace UnityEngine.UIElements
{
	// Token: 0x020002B7 RID: 695
	internal struct TextCoreHandle : ITextHandle
	{
		// Token: 0x170005A8 RID: 1448
		// (get) Token: 0x06001755 RID: 5973 RVA: 0x0005EF11 File Offset: 0x0005D111
		// (set) Token: 0x06001756 RID: 5974 RVA: 0x0005EF19 File Offset: 0x0005D119
		public Vector2 MeasuredSizes { readonly get; set; }

		// Token: 0x170005A9 RID: 1449
		// (get) Token: 0x06001757 RID: 5975 RVA: 0x0005EF22 File Offset: 0x0005D122
		// (set) Token: 0x06001758 RID: 5976 RVA: 0x0005EF2A File Offset: 0x0005D12A
		public Vector2 RoundedSizes { readonly get; set; }

		// Token: 0x06001759 RID: 5977 RVA: 0x0005EF34 File Offset: 0x0005D134
		public static ITextHandle New()
		{
			return new TextCoreHandle
			{
				m_CurrentGenerationSettings = new TextGenerationSettings()
			};
		}

		// Token: 0x170005AA RID: 1450
		// (get) Token: 0x0600175A RID: 5978 RVA: 0x0005EF60 File Offset: 0x0005D160
		internal TextInfo textInfoMesh
		{
			get
			{
				bool flag = this.m_TextInfoMesh == null;
				if (flag)
				{
					this.m_TextInfoMesh = new TextInfo();
				}
				return this.m_TextInfoMesh;
			}
		}

		// Token: 0x170005AB RID: 1451
		// (get) Token: 0x0600175B RID: 5979 RVA: 0x0005EF94 File Offset: 0x0005D194
		internal static TextInfo textInfoLayout
		{
			get
			{
				bool flag = TextCoreHandle.s_TextInfoLayout == null;
				if (flag)
				{
					TextCoreHandle.s_TextInfoLayout = new TextInfo();
				}
				return TextCoreHandle.s_TextInfoLayout;
			}
		}

		// Token: 0x0600175C RID: 5980 RVA: 0x0005EFC4 File Offset: 0x0005D1C4
		internal bool IsTextInfoAllocated()
		{
			return this.m_TextInfoMesh != null;
		}

		// Token: 0x0600175D RID: 5981 RVA: 0x0005EFE0 File Offset: 0x0005D1E0
		public bool IsLegacy()
		{
			return false;
		}

		// Token: 0x0600175E RID: 5982 RVA: 0x0005EFF3 File Offset: 0x0005D1F3
		public void SetDirty()
		{
			this.isDirty = true;
		}

		// Token: 0x0600175F RID: 5983 RVA: 0x0005F000 File Offset: 0x0005D200
		public bool IsDirty(MeshGenerationContextUtils.TextParams parms)
		{
			int hashCode = parms.GetHashCode();
			bool flag = this.m_PreviousGenerationSettingsHash == hashCode && !this.isDirty;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				this.m_PreviousGenerationSettingsHash = hashCode;
				this.isDirty = false;
				flag2 = true;
			}
			return flag2;
		}

		// Token: 0x06001760 RID: 5984 RVA: 0x0005F04C File Offset: 0x0005D24C
		public Vector2 GetCursorPosition(CursorPositionStylePainterParameters parms, float scaling)
		{
			return TextGenerator.GetCursorPosition(this.textInfoMesh, parms.rect, parms.cursorIndex, true);
		}

		// Token: 0x06001761 RID: 5985 RVA: 0x0005F078 File Offset: 0x0005D278
		public float ComputeTextWidth(MeshGenerationContextUtils.TextParams parms, float scaling)
		{
			this.UpdatePreferredValues(parms);
			return this.m_PreferredSize.x;
		}

		// Token: 0x06001762 RID: 5986 RVA: 0x0005F0A0 File Offset: 0x0005D2A0
		public float ComputeTextHeight(MeshGenerationContextUtils.TextParams parms, float scaling)
		{
			this.UpdatePreferredValues(parms);
			return this.m_PreferredSize.y;
		}

		// Token: 0x06001763 RID: 5987 RVA: 0x0005F0C8 File Offset: 0x0005D2C8
		public float GetLineHeight(int characterIndex, MeshGenerationContextUtils.TextParams textParams, float textScaling, float pixelPerPoint)
		{
			bool flag = this.m_TextInfoMesh == null || this.m_TextInfoMesh.characterCount == 0;
			if (flag)
			{
				this.Update(textParams, pixelPerPoint);
			}
			return this.m_TextInfoMesh.lineInfo[0].lineHeight;
		}

		// Token: 0x06001764 RID: 5988 RVA: 0x0005F118 File Offset: 0x0005D318
		public int VerticesCount(MeshGenerationContextUtils.TextParams parms, float pixelPerPoint)
		{
			this.Update(parms, pixelPerPoint);
			int num = 0;
			foreach (MeshInfo meshInfo2 in this.textInfoMesh.meshInfo)
			{
				num += meshInfo2.vertexCount;
			}
			return num;
		}

		// Token: 0x06001765 RID: 5989 RVA: 0x0005F164 File Offset: 0x0005D364
		ITextHandle ITextHandle.New()
		{
			return TextCoreHandle.New();
		}

		// Token: 0x06001766 RID: 5990 RVA: 0x0005F17C File Offset: 0x0005D37C
		public TextInfo Update(MeshGenerationContextUtils.TextParams parms, float pixelsPerPoint)
		{
			Vector2 vector = parms.rect.size;
			bool flag = Mathf.Abs(parms.rect.size.x - this.RoundedSizes.x) < 0.01f && Mathf.Abs(parms.rect.size.y - this.RoundedSizes.y) < 0.01f;
			if (flag)
			{
				vector = this.MeasuredSizes;
				parms.wordWrapWidth = vector.x;
			}
			else
			{
				this.RoundedSizes = vector;
				this.MeasuredSizes = vector;
			}
			parms.rect = new Rect(Vector2.zero, vector);
			bool flag2 = !this.IsDirty(parms);
			TextInfo textInfo;
			if (flag2)
			{
				textInfo = this.textInfoMesh;
			}
			else
			{
				TextCoreHandle.UpdateGenerationSettingsCommon(parms, this.m_CurrentGenerationSettings);
				this.m_CurrentGenerationSettings.color = parms.fontColor;
				this.m_CurrentGenerationSettings.inverseYAxis = true;
				this.textInfoMesh.isDirty = true;
				TextGenerator.GenerateText(this.m_CurrentGenerationSettings, this.textInfoMesh);
				textInfo = this.textInfoMesh;
			}
			return textInfo;
		}

		// Token: 0x06001767 RID: 5991 RVA: 0x0005F298 File Offset: 0x0005D498
		private void UpdatePreferredValues(MeshGenerationContextUtils.TextParams parms)
		{
			Vector2 size = parms.rect.size;
			parms.rect = new Rect(Vector2.zero, size);
			TextCoreHandle.UpdateGenerationSettingsCommon(parms, TextCoreHandle.s_LayoutSettings);
			this.m_PreferredSize = TextGenerator.GetPreferredValues(TextCoreHandle.s_LayoutSettings, TextCoreHandle.textInfoLayout);
		}

		// Token: 0x06001768 RID: 5992 RVA: 0x0005F2E8 File Offset: 0x0005D4E8
		private static TextOverflowMode GetTextOverflowMode(MeshGenerationContextUtils.TextParams textParams)
		{
			bool flag = textParams.textOverflow == TextOverflow.Clip;
			TextOverflowMode textOverflowMode;
			if (flag)
			{
				textOverflowMode = TextOverflowMode.Masking;
			}
			else
			{
				bool flag2 = textParams.textOverflow != TextOverflow.Ellipsis;
				if (flag2)
				{
					textOverflowMode = TextOverflowMode.Overflow;
				}
				else
				{
					bool flag3 = !textParams.wordWrap && textParams.overflow == OverflowInternal.Hidden;
					if (flag3)
					{
						textOverflowMode = TextOverflowMode.Ellipsis;
					}
					else
					{
						textOverflowMode = TextOverflowMode.Overflow;
					}
				}
			}
			return textOverflowMode;
		}

		// Token: 0x06001769 RID: 5993 RVA: 0x0005F33C File Offset: 0x0005D53C
		private static void UpdateGenerationSettingsCommon(MeshGenerationContextUtils.TextParams painterParams, TextGenerationSettings settings)
		{
			settings.textSettings = TextUtilities.GetTextSettingsFrom(painterParams);
			bool flag = settings.textSettings == null;
			if (!flag)
			{
				settings.fontAsset = TextUtilities.GetFontAsset(painterParams);
				bool flag2 = settings.fontAsset == null;
				if (!flag2)
				{
					settings.material = settings.fontAsset.material;
					settings.screenRect = painterParams.rect;
					settings.text = (string.IsNullOrEmpty(painterParams.text) ? "\u200b" : (painterParams.text + "\u200b"));
					settings.fontSize = (float)((painterParams.fontSize > 0) ? painterParams.fontSize : settings.fontAsset.faceInfo.pointSize);
					settings.fontStyle = TextGeneratorUtilities.LegacyStyleToNewStyle(painterParams.fontStyle);
					settings.textAlignment = TextGeneratorUtilities.LegacyAlignmentToNewAlignment(painterParams.anchor);
					settings.wordWrap = painterParams.wordWrap;
					settings.wordWrappingRatio = 0.4f;
					settings.richText = painterParams.richText;
					settings.overflowMode = TextCoreHandle.GetTextOverflowMode(painterParams);
					settings.characterSpacing = painterParams.letterSpacing.value;
					settings.wordSpacing = painterParams.wordSpacing.value;
					settings.paragraphSpacing = painterParams.paragraphSpacing.value;
				}
			}
		}

		// Token: 0x0600176A RID: 5994 RVA: 0x0005F488 File Offset: 0x0005D688
		public bool IsElided()
		{
			bool flag = this.m_TextInfoMesh == null;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				bool flag3 = this.m_TextInfoMesh.characterCount == 0;
				flag2 = flag3 || this.m_TextInfoMesh.textElementInfo[this.m_TextInfoMesh.characterCount - 1].character == '…';
			}
			return flag2;
		}

		// Token: 0x040009F8 RID: 2552
		private Vector2 m_PreferredSize;

		// Token: 0x040009F9 RID: 2553
		private int m_PreviousGenerationSettingsHash;

		// Token: 0x040009FA RID: 2554
		private TextGenerationSettings m_CurrentGenerationSettings;

		// Token: 0x040009FB RID: 2555
		private static TextGenerationSettings s_LayoutSettings = new TextGenerationSettings();

		// Token: 0x040009FC RID: 2556
		private TextInfo m_TextInfoMesh;

		// Token: 0x040009FD RID: 2557
		private static TextInfo s_TextInfoLayout;

		// Token: 0x040009FE RID: 2558
		private bool isDirty;
	}
}
