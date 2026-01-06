using System;
using TMPro;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x02000221 RID: 545
	[Preserve]
	[ES3Properties(new string[]
	{
		"_SortingLayer", "_SortingLayerID", "_SortingOrder", "m_hasFontAssetChanged", "m_renderer", "m_maskType", "m_text", "m_TextPreprocessor", "m_isRightToLeft", "m_fontAsset",
		"m_sharedMaterial", "m_fontSharedMaterials", "m_fontMaterial", "m_fontMaterials", "m_fontColor32", "m_fontColor", "m_enableVertexGradient", "m_colorMode", "m_fontColorGradient", "m_fontColorGradientPreset",
		"m_spriteAsset", "m_tintAllSprites", "m_StyleSheet", "m_TextStyleHashCode", "m_overrideHtmlColors", "m_faceColor", "m_fontSize", "m_fontSizeBase", "m_fontWeight", "m_enableAutoSizing",
		"m_fontSizeMin", "m_fontSizeMax", "m_fontStyle", "m_HorizontalAlignment", "m_VerticalAlignment", "m_textAlignment", "m_characterSpacing", "m_wordSpacing", "m_lineSpacing", "m_lineSpacingMax",
		"m_paragraphSpacing", "m_charWidthMaxAdj", "m_enableWordWrapping", "m_wordWrappingRatios", "m_overflowMode", "m_linkedTextComponent", "parentLinkedComponent", "m_enableKerning", "m_enableExtraPadding", "checkPaddingRequired",
		"m_isRichText", "m_parseCtrlCharacters", "m_isOrthographic", "m_isCullingEnabled", "m_horizontalMapping", "m_verticalMapping", "m_uvLineOffset", "m_geometrySortingOrder", "m_IsTextObjectScaleStatic", "m_VertexBufferAutoSizeReduction",
		"m_useMaxVisibleDescender", "m_pageToDisplay", "m_margin", "m_isUsingLegacyAnimationComponent", "m_isVolumetricText"
	})]
	public class ES3UserType_TextMeshPro : ES3ComponentType
	{
		// Token: 0x06001209 RID: 4617 RVA: 0x000AA19D File Offset: 0x000A839D
		public ES3UserType_TextMeshPro()
			: base(typeof(TextMeshPro))
		{
			ES3UserType_TextMeshPro.Instance = this;
			this.priority = 1;
		}

		// Token: 0x0600120A RID: 4618 RVA: 0x000AA1BC File Offset: 0x000A83BC
		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			TextMeshPro textMeshPro = (TextMeshPro)obj;
			writer.WritePrivateField("_SortingLayer", textMeshPro);
			writer.WritePrivateField("_SortingLayerID", textMeshPro);
			writer.WritePrivateField("_SortingOrder", textMeshPro);
			writer.WritePrivateField("m_hasFontAssetChanged", textMeshPro);
			writer.WritePrivateFieldByRef("m_renderer", textMeshPro);
			writer.WritePrivateField("m_maskType", textMeshPro);
			writer.WritePrivateField("m_text", textMeshPro);
			writer.WritePrivateField("m_TextPreprocessor", textMeshPro);
			writer.WritePrivateField("m_isRightToLeft", textMeshPro);
			writer.WritePrivateFieldByRef("m_fontAsset", textMeshPro);
			writer.WritePrivateFieldByRef("m_sharedMaterial", textMeshPro);
			writer.WritePrivateField("m_fontSharedMaterials", textMeshPro);
			writer.WritePrivateFieldByRef("m_fontMaterial", textMeshPro);
			writer.WritePrivateField("m_fontMaterials", textMeshPro);
			writer.WritePrivateField("m_fontColor32", textMeshPro);
			writer.WritePrivateField("m_fontColor", textMeshPro);
			writer.WritePrivateField("m_enableVertexGradient", textMeshPro);
			writer.WritePrivateField("m_colorMode", textMeshPro);
			writer.WritePrivateField("m_fontColorGradient", textMeshPro);
			writer.WritePrivateFieldByRef("m_fontColorGradientPreset", textMeshPro);
			writer.WritePrivateFieldByRef("m_spriteAsset", textMeshPro);
			writer.WritePrivateField("m_tintAllSprites", textMeshPro);
			writer.WritePrivateFieldByRef("m_StyleSheet", textMeshPro);
			writer.WritePrivateField("m_TextStyleHashCode", textMeshPro);
			writer.WritePrivateField("m_overrideHtmlColors", textMeshPro);
			writer.WritePrivateField("m_faceColor", textMeshPro);
			writer.WritePrivateField("m_fontSize", textMeshPro);
			writer.WritePrivateField("m_fontSizeBase", textMeshPro);
			writer.WritePrivateField("m_fontWeight", textMeshPro);
			writer.WritePrivateField("m_enableAutoSizing", textMeshPro);
			writer.WritePrivateField("m_fontSizeMin", textMeshPro);
			writer.WritePrivateField("m_fontSizeMax", textMeshPro);
			writer.WritePrivateField("m_fontStyle", textMeshPro);
			writer.WritePrivateField("m_HorizontalAlignment", textMeshPro);
			writer.WritePrivateField("m_VerticalAlignment", textMeshPro);
			writer.WritePrivateField("m_textAlignment", textMeshPro);
			writer.WritePrivateField("m_characterSpacing", textMeshPro);
			writer.WritePrivateField("m_wordSpacing", textMeshPro);
			writer.WritePrivateField("m_lineSpacing", textMeshPro);
			writer.WritePrivateField("m_lineSpacingMax", textMeshPro);
			writer.WritePrivateField("m_paragraphSpacing", textMeshPro);
			writer.WritePrivateField("m_charWidthMaxAdj", textMeshPro);
			writer.WritePrivateField("m_enableWordWrapping", textMeshPro);
			writer.WritePrivateField("m_wordWrappingRatios", textMeshPro);
			writer.WritePrivateField("m_overflowMode", textMeshPro);
			writer.WritePrivateFieldByRef("m_linkedTextComponent", textMeshPro);
			writer.WritePrivateFieldByRef("parentLinkedComponent", textMeshPro);
			writer.WritePrivateField("m_enableKerning", textMeshPro);
			writer.WritePrivateField("m_enableExtraPadding", textMeshPro);
			writer.WritePrivateField("checkPaddingRequired", textMeshPro);
			writer.WritePrivateField("m_isRichText", textMeshPro);
			writer.WritePrivateField("m_parseCtrlCharacters", textMeshPro);
			writer.WritePrivateField("m_isOrthographic", textMeshPro);
			writer.WritePrivateField("m_isCullingEnabled", textMeshPro);
			writer.WritePrivateField("m_horizontalMapping", textMeshPro);
			writer.WritePrivateField("m_verticalMapping", textMeshPro);
			writer.WritePrivateField("m_uvLineOffset", textMeshPro);
			writer.WritePrivateField("m_geometrySortingOrder", textMeshPro);
			writer.WritePrivateField("m_IsTextObjectScaleStatic", textMeshPro);
			writer.WritePrivateField("m_VertexBufferAutoSizeReduction", textMeshPro);
			writer.WritePrivateField("m_useMaxVisibleDescender", textMeshPro);
			writer.WritePrivateField("m_pageToDisplay", textMeshPro);
			writer.WritePrivateField("m_margin", textMeshPro);
			writer.WritePrivateField("m_isUsingLegacyAnimationComponent", textMeshPro);
			writer.WritePrivateField("m_isVolumetricText", textMeshPro);
		}

		// Token: 0x0600120B RID: 4619 RVA: 0x000AA4DC File Offset: 0x000A86DC
		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			TextMeshPro textMeshPro = (TextMeshPro)obj;
			foreach (object obj2 in reader.Properties)
			{
				string text = (string)obj2;
				uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
				if (num <= 2208521222U)
				{
					if (num <= 1385732490U)
					{
						if (num <= 699362427U)
						{
							if (num <= 247108000U)
							{
								if (num <= 81402979U)
								{
									if (num != 37021642U)
									{
										if (num == 81402979U)
										{
											if (text == "m_faceColor")
											{
												reader.SetPrivateField("m_faceColor", reader.Read<Color32>(), textMeshPro);
												continue;
											}
										}
									}
									else if (text == "m_fontColor32")
									{
										reader.SetPrivateField("m_fontColor32", reader.Read<Color32>(), textMeshPro);
										continue;
									}
								}
								else if (num != 188936545U)
								{
									if (num == 247108000U)
									{
										if (text == "m_fontAsset")
										{
											reader.SetPrivateField("m_fontAsset", reader.Read<TMP_FontAsset>(), textMeshPro);
											continue;
										}
									}
								}
								else if (text == "m_fontSize")
								{
									reader.SetPrivateField("m_fontSize", reader.Read<float>(), textMeshPro);
									continue;
								}
							}
							else if (num <= 614001638U)
							{
								if (num != 365345841U)
								{
									if (num == 614001638U)
									{
										if (text == "m_enableWordWrapping")
										{
											reader.SetPrivateField("m_enableWordWrapping", reader.Read<bool>(), textMeshPro);
											continue;
										}
									}
								}
								else if (text == "m_enableExtraPadding")
								{
									reader.SetPrivateField("m_enableExtraPadding", reader.Read<bool>(), textMeshPro);
									continue;
								}
							}
							else if (num != 636529635U)
							{
								if (num == 699362427U)
								{
									if (text == "m_pageToDisplay")
									{
										reader.SetPrivateField("m_pageToDisplay", reader.Read<int>(), textMeshPro);
										continue;
									}
								}
							}
							else if (text == "m_textAlignment")
							{
								reader.SetPrivateField("m_textAlignment", reader.Read<TextAlignmentOptions>(), textMeshPro);
								continue;
							}
						}
						else if (num <= 1141308439U)
						{
							if (num <= 862794267U)
							{
								if (num != 847005842U)
								{
									if (num == 862794267U)
									{
										if (text == "m_margin")
										{
											reader.SetPrivateField("m_margin", reader.Read<Vector4>(), textMeshPro);
											continue;
										}
									}
								}
								else if (text == "_SortingLayerID")
								{
									reader.SetPrivateField("_SortingLayerID", reader.Read<int>(), textMeshPro);
									continue;
								}
							}
							else if (num != 862813087U)
							{
								if (num == 1141308439U)
								{
									if (text == "m_tintAllSprites")
									{
										reader.SetPrivateField("m_tintAllSprites", reader.Read<bool>(), textMeshPro);
										continue;
									}
								}
							}
							else if (text == "m_verticalMapping")
							{
								reader.SetPrivateField("m_verticalMapping", reader.Read<TextureMappingOptions>(), textMeshPro);
								continue;
							}
						}
						else if (num <= 1236944107U)
						{
							if (num != 1142200021U)
							{
								if (num == 1236944107U)
								{
									if (text == "m_fontColorGradient")
									{
										reader.SetPrivateField("m_fontColorGradient", reader.Read<VertexGradient>(), textMeshPro);
										continue;
									}
								}
							}
							else if (text == "m_isRightToLeft")
							{
								reader.SetPrivateField("m_isRightToLeft", reader.Read<bool>(), textMeshPro);
								continue;
							}
						}
						else if (num != 1261608999U)
						{
							if (num == 1385732490U)
							{
								if (text == "_SortingOrder")
								{
									reader.SetPrivateField("_SortingOrder", reader.Read<int>(), textMeshPro);
									continue;
								}
							}
						}
						else if (text == "m_fontColor")
						{
							reader.SetPrivateField("m_fontColor", reader.Read<Color>(), textMeshPro);
							continue;
						}
					}
					else if (num <= 1747901007U)
					{
						if (num <= 1449138772U)
						{
							if (num <= 1436383186U)
							{
								if (num != 1426237186U)
								{
									if (num == 1436383186U)
									{
										if (text == "m_lineSpacingMax")
										{
											reader.SetPrivateField("m_lineSpacingMax", reader.Read<float>(), textMeshPro);
											continue;
										}
									}
								}
								else if (text == "m_enableKerning")
								{
									reader.SetPrivateField("m_enableKerning", reader.Read<bool>(), textMeshPro);
									continue;
								}
							}
							else if (num != 1439643097U)
							{
								if (num == 1449138772U)
								{
									if (text == "m_overflowMode")
									{
										reader.SetPrivateField("m_overflowMode", reader.Read<TextOverflowModes>(), textMeshPro);
										continue;
									}
								}
							}
							else if (text == "m_TextPreprocessor")
							{
								reader.SetPrivateField("m_TextPreprocessor", reader.Read<ITextPreprocessor>(), textMeshPro);
								continue;
							}
						}
						else if (num <= 1483198924U)
						{
							if (num != 1477154859U)
							{
								if (num == 1483198924U)
								{
									if (text == "m_fontColorGradientPreset")
									{
										reader.SetPrivateField("m_fontColorGradientPreset", reader.Read<TMP_ColorGradient>(), textMeshPro);
										continue;
									}
								}
							}
							else if (text == "m_colorMode")
							{
								reader.SetPrivateField("m_colorMode", reader.Read<ColorMode>(), textMeshPro);
								continue;
							}
						}
						else if (num != 1631370625U)
						{
							if (num == 1747901007U)
							{
								if (text == "m_IsTextObjectScaleStatic")
								{
									reader.SetPrivateField("m_IsTextObjectScaleStatic", reader.Read<bool>(), textMeshPro);
									continue;
								}
							}
						}
						else if (text == "m_wordWrappingRatios")
						{
							reader.SetPrivateField("m_wordWrappingRatios", reader.Read<float>(), textMeshPro);
							continue;
						}
					}
					else if (num <= 1933043696U)
					{
						if (num <= 1819618128U)
						{
							if (num != 1781754868U)
							{
								if (num == 1819618128U)
								{
									if (text == "m_fontWeight")
									{
										reader.SetPrivateField("m_fontWeight", reader.Read<FontWeight>(), textMeshPro);
										continue;
									}
								}
							}
							else if (text == "m_spriteAsset")
							{
								reader.SetPrivateField("m_spriteAsset", reader.Read<TMP_SpriteAsset>(), textMeshPro);
								continue;
							}
						}
						else if (num != 1901757663U)
						{
							if (num == 1933043696U)
							{
								if (text == "m_isCullingEnabled")
								{
									reader.SetPrivateField("m_isCullingEnabled", reader.Read<bool>(), textMeshPro);
									continue;
								}
							}
						}
						else if (text == "_SortingLayer")
						{
							reader.SetPrivateField("_SortingLayer", reader.Read<int>(), textMeshPro);
							continue;
						}
					}
					else if (num <= 2076381662U)
					{
						if (num != 2022204714U)
						{
							if (num == 2076381662U)
							{
								if (text == "m_text")
								{
									reader.SetPrivateField("m_text", reader.Read<string>(), textMeshPro);
									continue;
								}
							}
						}
						else if (text == "m_TextStyleHashCode")
						{
							reader.SetPrivateField("m_TextStyleHashCode", reader.Read<int>(), textMeshPro);
							continue;
						}
					}
					else if (num != 2147380368U)
					{
						if (num == 2208521222U)
						{
							if (text == "m_wordSpacing")
							{
								reader.SetPrivateField("m_wordSpacing", reader.Read<float>(), textMeshPro);
								continue;
							}
						}
					}
					else if (text == "m_isRichText")
					{
						reader.SetPrivateField("m_isRichText", reader.Read<bool>(), textMeshPro);
						continue;
					}
				}
				else if (num <= 3257066910U)
				{
					if (num <= 2724489833U)
					{
						if (num <= 2402581114U)
						{
							if (num <= 2306163712U)
							{
								if (num != 2241965298U)
								{
									if (num == 2306163712U)
									{
										if (text == "m_charWidthMaxAdj")
										{
											reader.SetPrivateField("m_charWidthMaxAdj", reader.Read<float>(), textMeshPro);
											continue;
										}
									}
								}
								else if (text == "m_HorizontalAlignment")
								{
									reader.SetPrivateField("m_HorizontalAlignment", reader.Read<HorizontalAlignmentOptions>(), textMeshPro);
									continue;
								}
							}
							else if (num != 2326257965U)
							{
								if (num == 2402581114U)
								{
									if (text == "m_paragraphSpacing")
									{
										reader.SetPrivateField("m_paragraphSpacing", reader.Read<float>(), textMeshPro);
										continue;
									}
								}
							}
							else if (text == "m_isUsingLegacyAnimationComponent")
							{
								reader.SetPrivateField("m_isUsingLegacyAnimationComponent", reader.Read<bool>(), textMeshPro);
								continue;
							}
						}
						else if (num <= 2625120445U)
						{
							if (num != 2530008059U)
							{
								if (num == 2625120445U)
								{
									if (text == "m_useMaxVisibleDescender")
									{
										reader.SetPrivateField("m_useMaxVisibleDescender", reader.Read<bool>(), textMeshPro);
										continue;
									}
								}
							}
							else if (text == "m_enableAutoSizing")
							{
								reader.SetPrivateField("m_enableAutoSizing", reader.Read<bool>(), textMeshPro);
								continue;
							}
						}
						else if (num != 2645718657U)
						{
							if (num == 2724489833U)
							{
								if (text == "m_horizontalMapping")
								{
									reader.SetPrivateField("m_horizontalMapping", reader.Read<TextureMappingOptions>(), textMeshPro);
									continue;
								}
							}
						}
						else if (text == "m_StyleSheet")
						{
							reader.SetPrivateField("m_StyleSheet", reader.Read<TMP_StyleSheet>(), textMeshPro);
							continue;
						}
					}
					else if (num <= 3047963954U)
					{
						if (num <= 2943731845U)
						{
							if (num != 2764538980U)
							{
								if (num == 2943731845U)
								{
									if (text == "m_geometrySortingOrder")
									{
										reader.SetPrivateField("m_geometrySortingOrder", reader.Read<VertexSortingOrder>(), textMeshPro);
										continue;
									}
								}
							}
							else if (text == "m_lineSpacing")
							{
								reader.SetPrivateField("m_lineSpacing", reader.Read<float>(), textMeshPro);
								continue;
							}
						}
						else if (num != 3006139473U)
						{
							if (num == 3047963954U)
							{
								if (text == "m_linkedTextComponent")
								{
									reader.SetPrivateField("m_linkedTextComponent", reader.Read<TMP_Text>(), textMeshPro);
									continue;
								}
							}
						}
						else if (text == "m_fontSharedMaterials")
						{
							reader.SetPrivateField("m_fontSharedMaterials", reader.Read<Material[]>(), textMeshPro);
							continue;
						}
					}
					else if (num <= 3156427971U)
					{
						if (num != 3080284033U)
						{
							if (num == 3156427971U)
							{
								if (text == "m_uvLineOffset")
								{
									reader.SetPrivateField("m_uvLineOffset", reader.Read<float>(), textMeshPro);
									continue;
								}
							}
						}
						else if (text == "m_maskType")
						{
							reader.SetPrivateField("m_maskType", reader.Read<MaskingTypes>(), textMeshPro);
							continue;
						}
					}
					else if (num != 3253844233U)
					{
						if (num == 3257066910U)
						{
							if (text == "m_renderer")
							{
								reader.SetPrivateField("m_renderer", reader.Read<Renderer>(), textMeshPro);
								continue;
							}
						}
					}
					else if (text == "m_fontMaterial")
					{
						reader.SetPrivateField("m_fontMaterial", reader.Read<Material>(), textMeshPro);
						continue;
					}
				}
				else if (num <= 3587979869U)
				{
					if (num <= 3381066510U)
					{
						if (num <= 3332403178U)
						{
							if (num != 3331929908U)
							{
								if (num == 3332403178U)
								{
									if (text == "m_hasFontAssetChanged")
									{
										reader.SetPrivateField("m_hasFontAssetChanged", reader.Read<bool>(), textMeshPro);
										continue;
									}
								}
							}
							else if (text == "m_overrideHtmlColors")
							{
								reader.SetPrivateField("m_overrideHtmlColors", reader.Read<bool>(), textMeshPro);
								continue;
							}
						}
						else if (num != 3367137467U)
						{
							if (num == 3381066510U)
							{
								if (text == "m_fontMaterials")
								{
									reader.SetPrivateField("m_fontMaterials", reader.Read<Material[]>(), textMeshPro);
									continue;
								}
							}
						}
						else if (text == "m_isOrthographic")
						{
							reader.SetPrivateField("m_isOrthographic", reader.Read<bool>(), textMeshPro);
							continue;
						}
					}
					else if (num <= 3557512490U)
					{
						if (num != 3430356977U)
						{
							if (num == 3557512490U)
							{
								if (text == "m_isVolumetricText")
								{
									reader.SetPrivateField("m_isVolumetricText", reader.Read<bool>(), textMeshPro);
									continue;
								}
							}
						}
						else if (text == "m_characterSpacing")
						{
							reader.SetPrivateField("m_characterSpacing", reader.Read<float>(), textMeshPro);
							continue;
						}
					}
					else if (num != 3578519965U)
					{
						if (num == 3587979869U)
						{
							if (text == "checkPaddingRequired")
							{
								reader.SetPrivateField("checkPaddingRequired", reader.Read<bool>(), textMeshPro);
								continue;
							}
						}
					}
					else if (text == "m_sharedMaterial")
					{
						reader.SetPrivateField("m_sharedMaterial", reader.Read<Material>(), textMeshPro);
						continue;
					}
				}
				else if (num <= 3873945321U)
				{
					if (num <= 3815822160U)
					{
						if (num != 3732514245U)
						{
							if (num == 3815822160U)
							{
								if (text == "m_VertexBufferAutoSizeReduction")
								{
									reader.SetPrivateField("m_VertexBufferAutoSizeReduction", reader.Read<bool>(), textMeshPro);
									continue;
								}
							}
						}
						else if (text == "m_fontSizeMax")
						{
							reader.SetPrivateField("m_fontSizeMax", reader.Read<float>(), textMeshPro);
							continue;
						}
					}
					else if (num != 3870028652U)
					{
						if (num == 3873945321U)
						{
							if (text == "m_parseCtrlCharacters")
							{
								reader.SetPrivateField("m_parseCtrlCharacters", reader.Read<bool>(), textMeshPro);
								continue;
							}
						}
					}
					else if (text == "m_fontSizeBase")
					{
						reader.SetPrivateField("m_fontSizeBase", reader.Read<float>(), textMeshPro);
						continue;
					}
				}
				else if (num <= 4061545044U)
				{
					if (num != 3901570363U)
					{
						if (num == 4061545044U)
						{
							if (text == "m_enableVertexGradient")
							{
								reader.SetPrivateField("m_enableVertexGradient", reader.Read<bool>(), textMeshPro);
								continue;
							}
						}
					}
					else if (text == "m_fontSizeMin")
					{
						reader.SetPrivateField("m_fontSizeMin", reader.Read<float>(), textMeshPro);
						continue;
					}
				}
				else if (num != 4081783047U)
				{
					if (num != 4180532021U)
					{
						if (num == 4182020596U)
						{
							if (text == "m_VerticalAlignment")
							{
								reader.SetPrivateField("m_VerticalAlignment", reader.Read<VerticalAlignmentOptions>(), textMeshPro);
								continue;
							}
						}
					}
					else if (text == "m_fontStyle")
					{
						reader.SetPrivateField("m_fontStyle", reader.Read<FontStyles>(), textMeshPro);
						continue;
					}
				}
				else if (text == "parentLinkedComponent")
				{
					reader.SetPrivateField("parentLinkedComponent", reader.Read<TMP_Text>(), textMeshPro);
					continue;
				}
				reader.Skip();
			}
		}

		// Token: 0x04000E4D RID: 3661
		public static ES3Type Instance;
	}
}
