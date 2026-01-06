using System;
using System.Collections.Generic;
using UnityEngine.TextCore.Text;

namespace UnityEngine.UIElements.StyleSheets
{
	// Token: 0x0200035C RID: 860
	internal class StylePropertyReader
	{
		// Token: 0x170006AE RID: 1710
		// (get) Token: 0x06001B88 RID: 7048 RVA: 0x0007E9E3 File Offset: 0x0007CBE3
		// (set) Token: 0x06001B89 RID: 7049 RVA: 0x0007E9EB File Offset: 0x0007CBEB
		public StyleProperty property { get; private set; }

		// Token: 0x170006AF RID: 1711
		// (get) Token: 0x06001B8A RID: 7050 RVA: 0x0007E9F4 File Offset: 0x0007CBF4
		// (set) Token: 0x06001B8B RID: 7051 RVA: 0x0007E9FC File Offset: 0x0007CBFC
		public StylePropertyId propertyId { get; private set; }

		// Token: 0x170006B0 RID: 1712
		// (get) Token: 0x06001B8C RID: 7052 RVA: 0x0007EA05 File Offset: 0x0007CC05
		// (set) Token: 0x06001B8D RID: 7053 RVA: 0x0007EA0D File Offset: 0x0007CC0D
		public int valueCount { get; private set; }

		// Token: 0x170006B1 RID: 1713
		// (get) Token: 0x06001B8E RID: 7054 RVA: 0x0007EA16 File Offset: 0x0007CC16
		// (set) Token: 0x06001B8F RID: 7055 RVA: 0x0007EA1E File Offset: 0x0007CC1E
		public float dpiScaling { get; private set; }

		// Token: 0x06001B90 RID: 7056 RVA: 0x0007EA28 File Offset: 0x0007CC28
		public void SetContext(StyleSheet sheet, StyleComplexSelector selector, StyleVariableContext varContext, float dpiScaling = 1f)
		{
			this.m_Sheet = sheet;
			this.m_Properties = selector.rule.properties;
			this.m_PropertyIds = StyleSheetCache.GetPropertyIds(sheet, selector.ruleIndex);
			this.m_Resolver.variableContext = varContext;
			this.dpiScaling = dpiScaling;
			this.LoadProperties();
		}

		// Token: 0x06001B91 RID: 7057 RVA: 0x0007EA7D File Offset: 0x0007CC7D
		public void SetInlineContext(StyleSheet sheet, StyleProperty[] properties, StylePropertyId[] propertyIds, float dpiScaling = 1f)
		{
			this.m_Sheet = sheet;
			this.m_Properties = properties;
			this.m_PropertyIds = propertyIds;
			this.dpiScaling = dpiScaling;
			this.LoadProperties();
		}

		// Token: 0x06001B92 RID: 7058 RVA: 0x0007EAA8 File Offset: 0x0007CCA8
		public StylePropertyId MoveNextProperty()
		{
			this.m_CurrentPropertyIndex++;
			this.m_CurrentValueIndex += this.valueCount;
			this.SetCurrentProperty();
			return this.propertyId;
		}

		// Token: 0x06001B93 RID: 7059 RVA: 0x0007EAE8 File Offset: 0x0007CCE8
		public StylePropertyValue GetValue(int index)
		{
			return this.m_Values[this.m_CurrentValueIndex + index];
		}

		// Token: 0x06001B94 RID: 7060 RVA: 0x0007EB10 File Offset: 0x0007CD10
		public StyleValueType GetValueType(int index)
		{
			return this.m_Values[this.m_CurrentValueIndex + index].handle.valueType;
		}

		// Token: 0x06001B95 RID: 7061 RVA: 0x0007EB44 File Offset: 0x0007CD44
		public bool IsValueType(int index, StyleValueType type)
		{
			return this.m_Values[this.m_CurrentValueIndex + index].handle.valueType == type;
		}

		// Token: 0x06001B96 RID: 7062 RVA: 0x0007EB7C File Offset: 0x0007CD7C
		public bool IsKeyword(int index, StyleValueKeyword keyword)
		{
			StylePropertyValue stylePropertyValue = this.m_Values[this.m_CurrentValueIndex + index];
			return stylePropertyValue.handle.valueType == StyleValueType.Keyword && stylePropertyValue.handle.valueIndex == (int)keyword;
		}

		// Token: 0x06001B97 RID: 7063 RVA: 0x0007EBC4 File Offset: 0x0007CDC4
		public string ReadAsString(int index)
		{
			StylePropertyValue stylePropertyValue = this.m_Values[this.m_CurrentValueIndex + index];
			return stylePropertyValue.sheet.ReadAsString(stylePropertyValue.handle);
		}

		// Token: 0x06001B98 RID: 7064 RVA: 0x0007EBFC File Offset: 0x0007CDFC
		public Length ReadLength(int index)
		{
			StylePropertyValue stylePropertyValue = this.m_Values[this.m_CurrentValueIndex + index];
			bool flag = stylePropertyValue.handle.valueType == StyleValueType.Keyword;
			Length length;
			if (flag)
			{
				StyleValueKeyword valueIndex = (StyleValueKeyword)stylePropertyValue.handle.valueIndex;
				StyleValueKeyword styleValueKeyword = valueIndex;
				StyleValueKeyword styleValueKeyword2 = styleValueKeyword;
				if (styleValueKeyword2 != StyleValueKeyword.Auto)
				{
					if (styleValueKeyword2 != StyleValueKeyword.None)
					{
						length = default(Length);
					}
					else
					{
						length = Length.None();
					}
				}
				else
				{
					length = Length.Auto();
				}
			}
			else
			{
				length = stylePropertyValue.sheet.ReadDimension(stylePropertyValue.handle).ToLength();
			}
			return length;
		}

		// Token: 0x06001B99 RID: 7065 RVA: 0x0007EC94 File Offset: 0x0007CE94
		public TimeValue ReadTimeValue(int index)
		{
			StylePropertyValue stylePropertyValue = this.m_Values[this.m_CurrentValueIndex + index];
			return stylePropertyValue.sheet.ReadDimension(stylePropertyValue.handle).ToTime();
		}

		// Token: 0x06001B9A RID: 7066 RVA: 0x0007ECD4 File Offset: 0x0007CED4
		public Translate ReadTranslate(int index)
		{
			StylePropertyValue stylePropertyValue = this.m_Values[this.m_CurrentValueIndex + index];
			StylePropertyValue stylePropertyValue2 = ((this.valueCount > 1) ? this.m_Values[this.m_CurrentValueIndex + index + 1] : default(StylePropertyValue));
			StylePropertyValue stylePropertyValue3 = ((this.valueCount > 2) ? this.m_Values[this.m_CurrentValueIndex + index + 2] : default(StylePropertyValue));
			return StylePropertyReader.ReadTranslate(this.valueCount, stylePropertyValue, stylePropertyValue2, stylePropertyValue3);
		}

		// Token: 0x06001B9B RID: 7067 RVA: 0x0007ED60 File Offset: 0x0007CF60
		public TransformOrigin ReadTransformOrigin(int index)
		{
			StylePropertyValue stylePropertyValue = this.m_Values[this.m_CurrentValueIndex + index];
			StylePropertyValue stylePropertyValue2 = ((this.valueCount > 1) ? this.m_Values[this.m_CurrentValueIndex + index + 1] : default(StylePropertyValue));
			StylePropertyValue stylePropertyValue3 = ((this.valueCount > 2) ? this.m_Values[this.m_CurrentValueIndex + index + 2] : default(StylePropertyValue));
			return StylePropertyReader.ReadTransformOrigin(this.valueCount, stylePropertyValue, stylePropertyValue2, stylePropertyValue3);
		}

		// Token: 0x06001B9C RID: 7068 RVA: 0x0007EDEC File Offset: 0x0007CFEC
		public Rotate ReadRotate(int index)
		{
			StylePropertyValue stylePropertyValue = this.m_Values[this.m_CurrentValueIndex + index];
			StylePropertyValue stylePropertyValue2 = ((this.valueCount > 1) ? this.m_Values[this.m_CurrentValueIndex + index + 1] : default(StylePropertyValue));
			StylePropertyValue stylePropertyValue3 = ((this.valueCount > 2) ? this.m_Values[this.m_CurrentValueIndex + index + 2] : default(StylePropertyValue));
			StylePropertyValue stylePropertyValue4 = ((this.valueCount > 3) ? this.m_Values[this.m_CurrentValueIndex + index + 3] : default(StylePropertyValue));
			return StylePropertyReader.ReadRotate(this.valueCount, stylePropertyValue, stylePropertyValue2, stylePropertyValue3, stylePropertyValue4);
		}

		// Token: 0x06001B9D RID: 7069 RVA: 0x0007EEA4 File Offset: 0x0007D0A4
		public Scale ReadScale(int index)
		{
			StylePropertyValue stylePropertyValue = this.m_Values[this.m_CurrentValueIndex + index];
			StylePropertyValue stylePropertyValue2 = ((this.valueCount > 1) ? this.m_Values[this.m_CurrentValueIndex + index + 1] : default(StylePropertyValue));
			StylePropertyValue stylePropertyValue3 = ((this.valueCount > 2) ? this.m_Values[this.m_CurrentValueIndex + index + 2] : default(StylePropertyValue));
			return StylePropertyReader.ReadScale(this.valueCount, stylePropertyValue, stylePropertyValue2, stylePropertyValue3);
		}

		// Token: 0x06001B9E RID: 7070 RVA: 0x0007EF30 File Offset: 0x0007D130
		public float ReadFloat(int index)
		{
			StylePropertyValue stylePropertyValue = this.m_Values[this.m_CurrentValueIndex + index];
			return stylePropertyValue.sheet.ReadFloat(stylePropertyValue.handle);
		}

		// Token: 0x06001B9F RID: 7071 RVA: 0x0007EF68 File Offset: 0x0007D168
		public int ReadInt(int index)
		{
			StylePropertyValue stylePropertyValue = this.m_Values[this.m_CurrentValueIndex + index];
			return (int)stylePropertyValue.sheet.ReadFloat(stylePropertyValue.handle);
		}

		// Token: 0x06001BA0 RID: 7072 RVA: 0x0007EFA0 File Offset: 0x0007D1A0
		public Color ReadColor(int index)
		{
			StylePropertyValue stylePropertyValue = this.m_Values[this.m_CurrentValueIndex + index];
			Color color = Color.clear;
			bool flag = stylePropertyValue.handle.valueType == StyleValueType.Enum;
			if (flag)
			{
				string text = stylePropertyValue.sheet.ReadAsString(stylePropertyValue.handle);
				StyleSheetColor.TryGetColor(text.ToLower(), out color);
			}
			else
			{
				color = stylePropertyValue.sheet.ReadColor(stylePropertyValue.handle);
			}
			return color;
		}

		// Token: 0x06001BA1 RID: 7073 RVA: 0x0007F01C File Offset: 0x0007D21C
		public int ReadEnum(StyleEnumType enumType, int index)
		{
			StylePropertyValue stylePropertyValue = this.m_Values[this.m_CurrentValueIndex + index];
			StyleValueHandle handle = stylePropertyValue.handle;
			bool flag = handle.valueType == StyleValueType.Keyword;
			string text;
			if (flag)
			{
				StyleValueKeyword styleValueKeyword = stylePropertyValue.sheet.ReadKeyword(handle);
				text = styleValueKeyword.ToUssString();
			}
			else
			{
				text = stylePropertyValue.sheet.ReadEnum(handle);
			}
			int num;
			StylePropertyUtil.TryGetEnumIntValue(enumType, text, out num);
			return num;
		}

		// Token: 0x06001BA2 RID: 7074 RVA: 0x0007F094 File Offset: 0x0007D294
		public FontDefinition ReadFontDefinition(int index)
		{
			FontAsset fontAsset = null;
			Font font = null;
			StylePropertyValue stylePropertyValue = this.m_Values[this.m_CurrentValueIndex + index];
			StyleValueType valueType = stylePropertyValue.handle.valueType;
			StyleValueType styleValueType = valueType;
			if (styleValueType != StyleValueType.Keyword)
			{
				if (styleValueType != StyleValueType.ResourcePath)
				{
					if (styleValueType != StyleValueType.AssetReference)
					{
						Debug.LogWarning("Invalid value for font " + stylePropertyValue.handle.valueType.ToString());
					}
					else
					{
						font = stylePropertyValue.sheet.ReadAssetReference(stylePropertyValue.handle) as Font;
						bool flag = font == null;
						if (flag)
						{
							fontAsset = stylePropertyValue.sheet.ReadAssetReference(stylePropertyValue.handle) as FontAsset;
						}
					}
				}
				else
				{
					string text = stylePropertyValue.sheet.ReadResourcePath(stylePropertyValue.handle);
					bool flag2 = !string.IsNullOrEmpty(text);
					if (flag2)
					{
						font = Panel.LoadResource(text, typeof(Font), this.dpiScaling) as Font;
						bool flag3 = font == null;
						if (flag3)
						{
							fontAsset = Panel.LoadResource(text, typeof(FontAsset), this.dpiScaling) as FontAsset;
						}
					}
					bool flag4 = fontAsset == null && font == null;
					if (flag4)
					{
						Debug.LogWarning(string.Format("Font not found for path: {0}", text));
					}
				}
			}
			else
			{
				bool flag5 = stylePropertyValue.handle.valueIndex != 6;
				if (flag5)
				{
					string text2 = "Invalid keyword for font ";
					StyleValueKeyword valueIndex = (StyleValueKeyword)stylePropertyValue.handle.valueIndex;
					Debug.LogWarning(text2 + valueIndex.ToString());
				}
			}
			bool flag6 = font != null;
			FontDefinition fontDefinition;
			if (flag6)
			{
				fontDefinition = FontDefinition.FromFont(font);
			}
			else
			{
				bool flag7 = fontAsset != null;
				if (flag7)
				{
					fontDefinition = FontDefinition.FromSDFFont(fontAsset);
				}
				else
				{
					fontDefinition = default(FontDefinition);
				}
			}
			return fontDefinition;
		}

		// Token: 0x06001BA3 RID: 7075 RVA: 0x0007F274 File Offset: 0x0007D474
		public Font ReadFont(int index)
		{
			Font font = null;
			StylePropertyValue stylePropertyValue = this.m_Values[this.m_CurrentValueIndex + index];
			StyleValueType valueType = stylePropertyValue.handle.valueType;
			StyleValueType styleValueType = valueType;
			if (styleValueType != StyleValueType.Keyword)
			{
				if (styleValueType != StyleValueType.ResourcePath)
				{
					if (styleValueType != StyleValueType.AssetReference)
					{
						Debug.LogWarning("Invalid value for font " + stylePropertyValue.handle.valueType.ToString());
					}
					else
					{
						font = stylePropertyValue.sheet.ReadAssetReference(stylePropertyValue.handle) as Font;
					}
				}
				else
				{
					string text = stylePropertyValue.sheet.ReadResourcePath(stylePropertyValue.handle);
					bool flag = !string.IsNullOrEmpty(text);
					if (flag)
					{
						font = Panel.LoadResource(text, typeof(Font), this.dpiScaling) as Font;
					}
					bool flag2 = font == null;
					if (flag2)
					{
						Debug.LogWarning(string.Format("Font not found for path: {0}", text));
					}
				}
			}
			else
			{
				bool flag3 = stylePropertyValue.handle.valueIndex != 6;
				if (flag3)
				{
					string text2 = "Invalid keyword for font ";
					StyleValueKeyword valueIndex = (StyleValueKeyword)stylePropertyValue.handle.valueIndex;
					Debug.LogWarning(text2 + valueIndex.ToString());
				}
			}
			return font;
		}

		// Token: 0x06001BA4 RID: 7076 RVA: 0x0007F3B8 File Offset: 0x0007D5B8
		public Background ReadBackground(int index)
		{
			ImageSource imageSource = default(ImageSource);
			StylePropertyValue stylePropertyValue = this.m_Values[this.m_CurrentValueIndex + index];
			bool flag = stylePropertyValue.handle.valueType == StyleValueType.Keyword;
			if (flag)
			{
				bool flag2 = stylePropertyValue.handle.valueIndex != 6;
				if (flag2)
				{
					string text = "Invalid keyword for image source ";
					StyleValueKeyword valueIndex = (StyleValueKeyword)stylePropertyValue.handle.valueIndex;
					Debug.LogWarning(text + valueIndex.ToString());
				}
			}
			else
			{
				bool flag3 = !StylePropertyReader.TryGetImageSourceFromValue(stylePropertyValue, this.dpiScaling, out imageSource);
				if (flag3)
				{
				}
			}
			bool flag4 = imageSource.texture != null;
			Background background;
			if (flag4)
			{
				background = Background.FromTexture2D(imageSource.texture);
			}
			else
			{
				bool flag5 = imageSource.sprite != null;
				if (flag5)
				{
					background = Background.FromSprite(imageSource.sprite);
				}
				else
				{
					bool flag6 = imageSource.vectorImage != null;
					if (flag6)
					{
						background = Background.FromVectorImage(imageSource.vectorImage);
					}
					else
					{
						bool flag7 = imageSource.renderTexture != null;
						if (flag7)
						{
							background = Background.FromRenderTexture(imageSource.renderTexture);
						}
						else
						{
							background = default(Background);
						}
					}
				}
			}
			return background;
		}

		// Token: 0x06001BA5 RID: 7077 RVA: 0x0007F4E8 File Offset: 0x0007D6E8
		public Cursor ReadCursor(int index)
		{
			float num = 0f;
			float num2 = 0f;
			int num3 = 0;
			Texture2D texture2D = null;
			StyleValueType valueType = this.GetValueType(index);
			bool flag = valueType == StyleValueType.ResourcePath || valueType == StyleValueType.AssetReference || valueType == StyleValueType.ScalableImage || valueType == StyleValueType.MissingAssetReference;
			bool flag2 = flag;
			if (flag2)
			{
				bool flag3 = this.valueCount < 1;
				if (flag3)
				{
					Debug.LogWarning(string.Format("USS 'cursor' has invalid value at {0}.", index));
				}
				else
				{
					ImageSource imageSource = default(ImageSource);
					StylePropertyValue value = this.GetValue(index);
					bool flag4 = StylePropertyReader.TryGetImageSourceFromValue(value, this.dpiScaling, out imageSource);
					if (flag4)
					{
						texture2D = imageSource.texture;
						bool flag5 = this.valueCount >= 3;
						if (flag5)
						{
							StylePropertyValue value2 = this.GetValue(index + 1);
							StylePropertyValue value3 = this.GetValue(index + 2);
							bool flag6 = value2.handle.valueType != StyleValueType.Float || value3.handle.valueType != StyleValueType.Float;
							if (flag6)
							{
								Debug.LogWarning("USS 'cursor' property requires two integers for the hot spot value.");
							}
							else
							{
								num = value2.sheet.ReadFloat(value2.handle);
								num2 = value3.sheet.ReadFloat(value3.handle);
							}
						}
					}
				}
			}
			else
			{
				bool flag7 = StylePropertyReader.getCursorIdFunc != null;
				if (flag7)
				{
					StylePropertyValue value4 = this.GetValue(index);
					num3 = StylePropertyReader.getCursorIdFunc(value4.sheet, value4.handle);
				}
			}
			return new Cursor
			{
				texture = texture2D,
				hotspot = new Vector2(num, num2),
				defaultCursorId = num3
			};
		}

		// Token: 0x06001BA6 RID: 7078 RVA: 0x0007F68C File Offset: 0x0007D88C
		public TextShadow ReadTextShadow(int index)
		{
			float num = 0f;
			float num2 = 0f;
			float num3 = 0f;
			Color color = Color.clear;
			bool flag = this.valueCount >= 2;
			if (flag)
			{
				int num4 = index;
				StyleValueType styleValueType = this.GetValueType(num4);
				bool flag2 = false;
				bool flag3 = styleValueType == StyleValueType.Color || styleValueType == StyleValueType.Enum;
				if (flag3)
				{
					color = this.ReadColor(num4++);
					flag2 = true;
				}
				bool flag4 = num4 + 1 < this.valueCount;
				if (flag4)
				{
					styleValueType = this.GetValueType(num4);
					StyleValueType valueType = this.GetValueType(num4 + 1);
					bool flag5 = (styleValueType == StyleValueType.Dimension || styleValueType == StyleValueType.Float) && (valueType == StyleValueType.Dimension || valueType == StyleValueType.Float);
					if (flag5)
					{
						StylePropertyValue value = this.GetValue(num4++);
						StylePropertyValue value2 = this.GetValue(num4++);
						num = value.sheet.ReadDimension(value.handle).value;
						num2 = value2.sheet.ReadDimension(value2.handle).value;
					}
				}
				bool flag6 = num4 < this.valueCount;
				if (flag6)
				{
					styleValueType = this.GetValueType(num4);
					bool flag7 = styleValueType == StyleValueType.Dimension || styleValueType == StyleValueType.Float;
					if (flag7)
					{
						StylePropertyValue value3 = this.GetValue(num4++);
						num3 = value3.sheet.ReadDimension(value3.handle).value;
					}
					else
					{
						bool flag8 = styleValueType == StyleValueType.Color || styleValueType == StyleValueType.Enum;
						if (flag8)
						{
							bool flag9 = !flag2;
							if (flag9)
							{
								color = this.ReadColor(num4);
							}
						}
					}
				}
				bool flag10 = num4 < this.valueCount;
				if (flag10)
				{
					styleValueType = this.GetValueType(num4);
					bool flag11 = styleValueType == StyleValueType.Color || styleValueType == StyleValueType.Enum;
					if (flag11)
					{
						bool flag12 = !flag2;
						if (flag12)
						{
							color = this.ReadColor(num4);
						}
					}
				}
			}
			return new TextShadow
			{
				offset = new Vector2(num, num2),
				blurRadius = num3,
				color = color
			};
		}

		// Token: 0x06001BA7 RID: 7079 RVA: 0x0007F89C File Offset: 0x0007DA9C
		public void ReadListEasingFunction(List<EasingFunction> list, int index)
		{
			list.Clear();
			do
			{
				StylePropertyValue stylePropertyValue = this.m_Values[this.m_CurrentValueIndex + index];
				StyleValueHandle handle = stylePropertyValue.handle;
				bool flag = handle.valueType == StyleValueType.Enum;
				if (flag)
				{
					string text = stylePropertyValue.sheet.ReadEnum(handle);
					int num;
					StylePropertyUtil.TryGetEnumIntValue(StyleEnumType.EasingMode, text, out num);
					list.Add(new EasingFunction((EasingMode)num));
					index++;
				}
				bool flag2 = index < this.valueCount;
				if (flag2)
				{
					bool flag3 = this.m_Values[this.m_CurrentValueIndex + index].handle.valueType == StyleValueType.CommaSeparator;
					if (flag3)
					{
						index++;
					}
				}
			}
			while (index < this.valueCount);
		}

		// Token: 0x06001BA8 RID: 7080 RVA: 0x0007F95C File Offset: 0x0007DB5C
		public void ReadListTimeValue(List<TimeValue> list, int index)
		{
			list.Clear();
			do
			{
				StylePropertyValue stylePropertyValue = this.m_Values[this.m_CurrentValueIndex + index];
				TimeValue timeValue = stylePropertyValue.sheet.ReadDimension(stylePropertyValue.handle).ToTime();
				list.Add(timeValue);
				index++;
				bool flag = index < this.valueCount;
				if (flag)
				{
					bool flag2 = this.m_Values[this.m_CurrentValueIndex + index].handle.valueType == StyleValueType.CommaSeparator;
					if (flag2)
					{
						index++;
					}
				}
			}
			while (index < this.valueCount);
		}

		// Token: 0x06001BA9 RID: 7081 RVA: 0x0007FA00 File Offset: 0x0007DC00
		public void ReadListStylePropertyName(List<StylePropertyName> list, int index)
		{
			list.Clear();
			do
			{
				StylePropertyValue stylePropertyValue = this.m_Values[this.m_CurrentValueIndex + index];
				string text = stylePropertyValue.sheet.ReadAsString(stylePropertyValue.handle);
				list.Add(new StylePropertyName(text));
				index++;
				bool flag = index < this.valueCount;
				if (flag)
				{
					bool flag2 = this.m_Values[this.m_CurrentValueIndex + index].handle.valueType == StyleValueType.CommaSeparator;
					if (flag2)
					{
						index++;
					}
				}
			}
			while (index < this.valueCount);
		}

		// Token: 0x06001BAA RID: 7082 RVA: 0x0007FAA0 File Offset: 0x0007DCA0
		public void ReadListString(List<string> list, int index)
		{
			list.Clear();
			do
			{
				StylePropertyValue stylePropertyValue = this.m_Values[this.m_CurrentValueIndex + index];
				string text = stylePropertyValue.sheet.ReadAsString(stylePropertyValue.handle);
				list.Add(text);
				index++;
				bool flag = index < this.valueCount;
				if (flag)
				{
					bool flag2 = this.m_Values[this.m_CurrentValueIndex + index].handle.valueType == StyleValueType.CommaSeparator;
					if (flag2)
					{
						index++;
					}
				}
			}
			while (index < this.valueCount);
		}

		// Token: 0x06001BAB RID: 7083 RVA: 0x0007FB3C File Offset: 0x0007DD3C
		private void LoadProperties()
		{
			this.m_CurrentPropertyIndex = 0;
			this.m_CurrentValueIndex = 0;
			this.m_Values.Clear();
			this.m_ValueCount.Clear();
			foreach (StyleProperty styleProperty in this.m_Properties)
			{
				int num = 0;
				bool flag = true;
				bool requireVariableResolve = styleProperty.requireVariableResolve;
				if (requireVariableResolve)
				{
					this.m_Resolver.Init(styleProperty, this.m_Sheet, styleProperty.values);
					int num2 = 0;
					while (num2 < styleProperty.values.Length && flag)
					{
						StyleValueHandle styleValueHandle = styleProperty.values[num2];
						bool flag2 = styleValueHandle.IsVarFunction();
						if (flag2)
						{
							flag = this.m_Resolver.ResolveVarFunction(ref num2);
						}
						else
						{
							this.m_Resolver.AddValue(styleValueHandle);
						}
						num2++;
					}
					bool flag3 = flag && this.m_Resolver.ValidateResolvedValues();
					if (flag3)
					{
						this.m_Values.AddRange(this.m_Resolver.resolvedValues);
						num += this.m_Resolver.resolvedValues.Count;
					}
					else
					{
						StyleValueHandle styleValueHandle2 = new StyleValueHandle
						{
							valueType = StyleValueType.Keyword,
							valueIndex = 3
						};
						this.m_Values.Add(new StylePropertyValue
						{
							sheet = this.m_Sheet,
							handle = styleValueHandle2
						});
						num++;
					}
				}
				else
				{
					num = styleProperty.values.Length;
					for (int j = 0; j < num; j++)
					{
						this.m_Values.Add(new StylePropertyValue
						{
							sheet = this.m_Sheet,
							handle = styleProperty.values[j]
						});
					}
				}
				this.m_ValueCount.Add(num);
			}
			this.SetCurrentProperty();
		}

		// Token: 0x06001BAC RID: 7084 RVA: 0x0007FD28 File Offset: 0x0007DF28
		private void SetCurrentProperty()
		{
			bool flag = this.m_CurrentPropertyIndex < this.m_PropertyIds.Length;
			if (flag)
			{
				this.property = this.m_Properties[this.m_CurrentPropertyIndex];
				this.propertyId = this.m_PropertyIds[this.m_CurrentPropertyIndex];
				this.valueCount = this.m_ValueCount[this.m_CurrentPropertyIndex];
			}
			else
			{
				this.property = null;
				this.propertyId = StylePropertyId.Unknown;
				this.valueCount = 0;
			}
		}

		// Token: 0x06001BAD RID: 7085 RVA: 0x0007FDA8 File Offset: 0x0007DFA8
		public static TransformOrigin ReadTransformOrigin(int valCount, StylePropertyValue val1, StylePropertyValue val2, StylePropertyValue zVvalue)
		{
			Length length = Length.Percent(50f);
			Length length2 = Length.Percent(50f);
			float num = 0f;
			switch (valCount)
			{
			case 1:
			{
				bool flag;
				bool flag2;
				Length length3 = StylePropertyReader.ReadTransformOriginEnum(val1, out flag, out flag2);
				bool flag3 = flag2;
				if (flag3)
				{
					length = length3;
				}
				else
				{
					length2 = length3;
				}
				goto IL_00F3;
			}
			case 2:
				break;
			case 3:
			{
				bool flag4 = zVvalue.handle.valueType == StyleValueType.Dimension || zVvalue.handle.valueType == StyleValueType.Float;
				if (flag4)
				{
					Dimension dimension = zVvalue.sheet.ReadDimension(zVvalue.handle);
					num = dimension.value;
				}
				break;
			}
			default:
				goto IL_00F3;
			}
			bool flag5;
			bool flag6;
			Length length4 = StylePropertyReader.ReadTransformOriginEnum(val1, out flag5, out flag6);
			bool flag7;
			bool flag8;
			Length length5 = StylePropertyReader.ReadTransformOriginEnum(val2, out flag7, out flag8);
			bool flag9 = !flag6 || !flag7;
			if (flag9)
			{
				bool flag10 = flag8 && flag5;
				if (flag10)
				{
					length = length5;
					length2 = length4;
				}
			}
			else
			{
				length = length4;
				length2 = length5;
			}
			IL_00F3:
			return new TransformOrigin(length, length2, num);
		}

		// Token: 0x06001BAE RID: 7086 RVA: 0x0007FEB8 File Offset: 0x0007E0B8
		private static Length ReadTransformOriginEnum(StylePropertyValue value, out bool isVertical, out bool isHorizontal)
		{
			bool flag = value.handle.valueType == StyleValueType.Enum;
			if (flag)
			{
				switch (StylePropertyReader.ReadEnum(StyleEnumType.TransformOriginOffset, value))
				{
				case 1:
					isVertical = false;
					isHorizontal = true;
					return Length.Percent(0f);
				case 2:
					isVertical = false;
					isHorizontal = true;
					return Length.Percent(100f);
				case 3:
					isVertical = true;
					isHorizontal = false;
					return Length.Percent(0f);
				case 4:
					isVertical = true;
					isHorizontal = false;
					return Length.Percent(100f);
				case 5:
					isVertical = true;
					isHorizontal = true;
					return Length.Percent(50f);
				}
			}
			else
			{
				bool flag2 = value.handle.valueType == StyleValueType.Dimension || value.handle.valueType == StyleValueType.Float;
				if (flag2)
				{
					isVertical = true;
					isHorizontal = true;
					return value.sheet.ReadDimension(value.handle).ToLength();
				}
			}
			isVertical = false;
			isHorizontal = false;
			return Length.Percent(50f);
		}

		// Token: 0x06001BAF RID: 7087 RVA: 0x0007FFDC File Offset: 0x0007E1DC
		public static Translate ReadTranslate(int valCount, StylePropertyValue val1, StylePropertyValue val2, StylePropertyValue val3)
		{
			bool flag = val1.handle.valueType == StyleValueType.Keyword && val1.handle.valueIndex == 6;
			Translate translate;
			if (flag)
			{
				translate = Translate.None();
			}
			else
			{
				Length length = 0f;
				Length length2 = 0f;
				float num = 0f;
				switch (valCount)
				{
				case 1:
				{
					bool flag2 = val1.handle.valueType == StyleValueType.Dimension || val1.handle.valueType == StyleValueType.Float;
					if (flag2)
					{
						length = val1.sheet.ReadDimension(val1.handle).ToLength();
						length2 = val1.sheet.ReadDimension(val1.handle).ToLength();
					}
					goto IL_01C3;
				}
				case 2:
					break;
				case 3:
				{
					bool flag3 = val3.handle.valueType == StyleValueType.Dimension || val3.handle.valueType == StyleValueType.Float;
					if (flag3)
					{
						Dimension dimension = val3.sheet.ReadDimension(val3.handle);
						bool flag4 = dimension.unit != Dimension.Unit.Pixel && dimension.unit > Dimension.Unit.Unitless;
						if (flag4)
						{
							num = dimension.value;
						}
					}
					break;
				}
				default:
					goto IL_01C3;
				}
				bool flag5 = val1.handle.valueType == StyleValueType.Dimension || val1.handle.valueType == StyleValueType.Float;
				if (flag5)
				{
					length = val1.sheet.ReadDimension(val1.handle).ToLength();
				}
				bool flag6 = val2.handle.valueType == StyleValueType.Dimension || val2.handle.valueType == StyleValueType.Float;
				if (flag6)
				{
					length2 = val2.sheet.ReadDimension(val2.handle).ToLength();
				}
				IL_01C3:
				translate = new Translate(length, length2, num);
			}
			return translate;
		}

		// Token: 0x06001BB0 RID: 7088 RVA: 0x000801BC File Offset: 0x0007E3BC
		public static Scale ReadScale(int valCount, StylePropertyValue val1, StylePropertyValue val2, StylePropertyValue val3)
		{
			bool flag = val1.handle.valueType == StyleValueType.Keyword && val1.handle.valueIndex == 6;
			Scale scale;
			if (flag)
			{
				scale = Scale.None();
			}
			else
			{
				Vector3 one = Vector3.one;
				switch (valCount)
				{
				case 1:
				{
					bool flag2 = val1.handle.valueType == StyleValueType.Dimension || val1.handle.valueType == StyleValueType.Float;
					if (flag2)
					{
						one.x = val1.sheet.ReadFloat(val1.handle);
						one.y = one.x;
					}
					goto IL_0173;
				}
				case 2:
					break;
				case 3:
				{
					bool flag3 = val3.handle.valueType == StyleValueType.Dimension || val3.handle.valueType == StyleValueType.Float;
					if (flag3)
					{
						one.z = val3.sheet.ReadFloat(val3.handle);
					}
					break;
				}
				default:
					goto IL_0173;
				}
				bool flag4 = val1.handle.valueType == StyleValueType.Dimension || val1.handle.valueType == StyleValueType.Float;
				if (flag4)
				{
					one.x = val1.sheet.ReadFloat(val1.handle);
				}
				bool flag5 = val2.handle.valueType == StyleValueType.Dimension || val2.handle.valueType == StyleValueType.Float;
				if (flag5)
				{
					one.y = val2.sheet.ReadFloat(val2.handle);
				}
				IL_0173:
				scale = new Scale(one);
			}
			return scale;
		}

		// Token: 0x06001BB1 RID: 7089 RVA: 0x00080348 File Offset: 0x0007E548
		public static Rotate ReadRotate(int valCount, StylePropertyValue val1, StylePropertyValue val2, StylePropertyValue val3, StylePropertyValue val4)
		{
			bool flag = val1.handle.valueType == StyleValueType.Keyword && val1.handle.valueIndex == 6;
			Rotate rotate;
			if (flag)
			{
				rotate = Rotate.None();
			}
			else
			{
				Rotate rotate2 = Rotate.Initial();
				if (valCount == 1)
				{
					bool flag2 = val1.handle.valueType == StyleValueType.Dimension;
					if (flag2)
					{
						rotate2.angle = StylePropertyReader.ReadAngle(val1);
					}
				}
				rotate = rotate2;
			}
			return rotate;
		}

		// Token: 0x06001BB2 RID: 7090 RVA: 0x000803C4 File Offset: 0x0007E5C4
		private static int ReadEnum(StyleEnumType enumType, StylePropertyValue value)
		{
			StyleValueHandle handle = value.handle;
			bool flag = handle.valueType == StyleValueType.Keyword;
			string text;
			if (flag)
			{
				StyleValueKeyword styleValueKeyword = value.sheet.ReadKeyword(handle);
				text = styleValueKeyword.ToUssString();
			}
			else
			{
				text = value.sheet.ReadEnum(handle);
			}
			int num;
			StylePropertyUtil.TryGetEnumIntValue(enumType, text, out num);
			return num;
		}

		// Token: 0x06001BB3 RID: 7091 RVA: 0x00080424 File Offset: 0x0007E624
		public static Angle ReadAngle(StylePropertyValue value)
		{
			bool flag = value.handle.valueType == StyleValueType.Keyword;
			Angle angle;
			if (flag)
			{
				StyleValueKeyword valueIndex = (StyleValueKeyword)value.handle.valueIndex;
				StyleValueKeyword styleValueKeyword = valueIndex;
				StyleValueKeyword styleValueKeyword2 = styleValueKeyword;
				if (styleValueKeyword2 != StyleValueKeyword.None)
				{
					angle = default(Angle);
				}
				else
				{
					angle = Angle.None();
				}
			}
			else
			{
				angle = value.sheet.ReadDimension(value.handle).ToAngle();
			}
			return angle;
		}

		// Token: 0x06001BB4 RID: 7092 RVA: 0x00080494 File Offset: 0x0007E694
		internal static bool TryGetImageSourceFromValue(StylePropertyValue propertyValue, float dpiScaling, out ImageSource source)
		{
			source = default(ImageSource);
			StyleValueType valueType = propertyValue.handle.valueType;
			StyleValueType styleValueType = valueType;
			if (styleValueType <= StyleValueType.AssetReference)
			{
				if (styleValueType != StyleValueType.ResourcePath)
				{
					if (styleValueType == StyleValueType.AssetReference)
					{
						Object @object = propertyValue.sheet.ReadAssetReference(propertyValue.handle);
						source.texture = @object as Texture2D;
						source.sprite = @object as Sprite;
						source.vectorImage = @object as VectorImage;
						source.renderTexture = @object as RenderTexture;
						bool flag = source.IsNull();
						if (flag)
						{
							Debug.LogWarning("Invalid image specified");
							return false;
						}
						goto IL_0254;
					}
				}
				else
				{
					string text = propertyValue.sheet.ReadResourcePath(propertyValue.handle);
					bool flag2 = !string.IsNullOrEmpty(text);
					if (flag2)
					{
						source.sprite = Panel.LoadResource(text, typeof(Sprite), dpiScaling) as Sprite;
						bool flag3 = source.IsNull();
						if (flag3)
						{
							source.texture = Panel.LoadResource(text, typeof(Texture2D), dpiScaling) as Texture2D;
						}
						bool flag4 = source.IsNull();
						if (flag4)
						{
							source.vectorImage = Panel.LoadResource(text, typeof(VectorImage), dpiScaling) as VectorImage;
						}
						bool flag5 = source.IsNull();
						if (flag5)
						{
							source.renderTexture = Panel.LoadResource(text, typeof(RenderTexture), dpiScaling) as RenderTexture;
						}
					}
					bool flag6 = source.IsNull();
					if (flag6)
					{
						Debug.LogWarning(string.Format("Image not found for path: {0}", text));
						return false;
					}
					goto IL_0254;
				}
			}
			else if (styleValueType != StyleValueType.ScalableImage)
			{
				if (styleValueType == StyleValueType.MissingAssetReference)
				{
					return false;
				}
			}
			else
			{
				ScalableImage scalableImage = propertyValue.sheet.ReadScalableImage(propertyValue.handle);
				bool flag7 = scalableImage.normalImage == null && scalableImage.highResolutionImage == null;
				if (flag7)
				{
					Debug.LogWarning("Invalid scalable image specified");
					return false;
				}
				source.texture = scalableImage.normalImage;
				bool flag8 = !Mathf.Approximately(dpiScaling % 1f, 0f);
				if (flag8)
				{
					source.texture.filterMode = FilterMode.Bilinear;
				}
				goto IL_0254;
			}
			Debug.LogWarning("Invalid value for image texture " + propertyValue.handle.valueType.ToString());
			return false;
			IL_0254:
			return true;
		}

		// Token: 0x04000DAF RID: 3503
		internal static StylePropertyReader.GetCursorIdFunction getCursorIdFunc;

		// Token: 0x04000DB0 RID: 3504
		private List<StylePropertyValue> m_Values = new List<StylePropertyValue>();

		// Token: 0x04000DB1 RID: 3505
		private List<int> m_ValueCount = new List<int>();

		// Token: 0x04000DB2 RID: 3506
		private StyleVariableResolver m_Resolver = new StyleVariableResolver();

		// Token: 0x04000DB3 RID: 3507
		private StyleSheet m_Sheet;

		// Token: 0x04000DB4 RID: 3508
		private StyleProperty[] m_Properties;

		// Token: 0x04000DB5 RID: 3509
		private StylePropertyId[] m_PropertyIds;

		// Token: 0x04000DB6 RID: 3510
		private int m_CurrentValueIndex;

		// Token: 0x04000DB7 RID: 3511
		private int m_CurrentPropertyIndex;

		// Token: 0x0200035D RID: 861
		// (Invoke) Token: 0x06001BB7 RID: 7095
		internal delegate int GetCursorIdFunction(StyleSheet sheet, StyleValueHandle handle);
	}
}
