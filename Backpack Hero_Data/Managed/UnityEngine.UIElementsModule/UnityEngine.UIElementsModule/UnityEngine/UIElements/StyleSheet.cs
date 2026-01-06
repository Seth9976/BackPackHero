using System;
using System.Collections.Generic;
using UnityEngine.UIElements.StyleSheets;

namespace UnityEngine.UIElements
{
	// Token: 0x020002A6 RID: 678
	[Serializable]
	public class StyleSheet : ScriptableObject
	{
		// Token: 0x17000597 RID: 1431
		// (get) Token: 0x060016E6 RID: 5862 RVA: 0x0005D8A8 File Offset: 0x0005BAA8
		// (set) Token: 0x060016E7 RID: 5863 RVA: 0x0005D8C0 File Offset: 0x0005BAC0
		public bool importedWithErrors
		{
			get
			{
				return this.m_ImportedWithErrors;
			}
			internal set
			{
				this.m_ImportedWithErrors = value;
			}
		}

		// Token: 0x17000598 RID: 1432
		// (get) Token: 0x060016E8 RID: 5864 RVA: 0x0005D8CC File Offset: 0x0005BACC
		// (set) Token: 0x060016E9 RID: 5865 RVA: 0x0005D8E4 File Offset: 0x0005BAE4
		public bool importedWithWarnings
		{
			get
			{
				return this.m_ImportedWithWarnings;
			}
			internal set
			{
				this.m_ImportedWithWarnings = value;
			}
		}

		// Token: 0x17000599 RID: 1433
		// (get) Token: 0x060016EA RID: 5866 RVA: 0x0005D8F0 File Offset: 0x0005BAF0
		// (set) Token: 0x060016EB RID: 5867 RVA: 0x0005D908 File Offset: 0x0005BB08
		internal StyleRule[] rules
		{
			get
			{
				return this.m_Rules;
			}
			set
			{
				this.m_Rules = value;
				this.SetupReferences();
			}
		}

		// Token: 0x1700059A RID: 1434
		// (get) Token: 0x060016EC RID: 5868 RVA: 0x0005D91C File Offset: 0x0005BB1C
		// (set) Token: 0x060016ED RID: 5869 RVA: 0x0005D934 File Offset: 0x0005BB34
		internal StyleComplexSelector[] complexSelectors
		{
			get
			{
				return this.m_ComplexSelectors;
			}
			set
			{
				this.m_ComplexSelectors = value;
				this.SetupReferences();
			}
		}

		// Token: 0x1700059B RID: 1435
		// (get) Token: 0x060016EE RID: 5870 RVA: 0x0005D948 File Offset: 0x0005BB48
		internal List<StyleSheet> flattenedRecursiveImports
		{
			get
			{
				return this.m_FlattenedImportedStyleSheets;
			}
		}

		// Token: 0x1700059C RID: 1436
		// (get) Token: 0x060016EF RID: 5871 RVA: 0x0005D960 File Offset: 0x0005BB60
		// (set) Token: 0x060016F0 RID: 5872 RVA: 0x0005D978 File Offset: 0x0005BB78
		public int contentHash
		{
			get
			{
				return this.m_ContentHash;
			}
			set
			{
				this.m_ContentHash = value;
			}
		}

		// Token: 0x1700059D RID: 1437
		// (get) Token: 0x060016F1 RID: 5873 RVA: 0x0005D984 File Offset: 0x0005BB84
		// (set) Token: 0x060016F2 RID: 5874 RVA: 0x0005D99C File Offset: 0x0005BB9C
		internal bool isDefaultStyleSheet
		{
			get
			{
				return this.m_IsDefaultStyleSheet;
			}
			set
			{
				this.m_IsDefaultStyleSheet = value;
				bool flag = this.flattenedRecursiveImports != null;
				if (flag)
				{
					foreach (StyleSheet styleSheet in this.flattenedRecursiveImports)
					{
						styleSheet.isDefaultStyleSheet = value;
					}
				}
			}
		}

		// Token: 0x060016F3 RID: 5875 RVA: 0x0005DA0C File Offset: 0x0005BC0C
		private static bool TryCheckAccess<T>(T[] list, StyleValueType type, StyleValueHandle handle, out T value)
		{
			bool flag = false;
			value = default(T);
			bool flag2 = handle.valueType == type && handle.valueIndex >= 0 && handle.valueIndex < list.Length;
			if (flag2)
			{
				value = list[handle.valueIndex];
				flag = true;
			}
			else
			{
				Debug.LogErrorFormat("Trying to read value of type {0} while reading a value of type {1}", new object[] { type, handle.valueType });
			}
			return flag;
		}

		// Token: 0x060016F4 RID: 5876 RVA: 0x0005DA90 File Offset: 0x0005BC90
		private static T CheckAccess<T>(T[] list, StyleValueType type, StyleValueHandle handle)
		{
			T t = default(T);
			bool flag = handle.valueType != type;
			if (flag)
			{
				Debug.LogErrorFormat("Trying to read value of type {0} while reading a value of type {1}", new object[] { type, handle.valueType });
			}
			else
			{
				bool flag2 = list == null || handle.valueIndex < 0 || handle.valueIndex >= list.Length;
				if (flag2)
				{
					Debug.LogError("Accessing invalid property");
				}
				else
				{
					t = list[handle.valueIndex];
				}
			}
			return t;
		}

		// Token: 0x060016F5 RID: 5877 RVA: 0x0005DB28 File Offset: 0x0005BD28
		internal virtual void OnEnable()
		{
			this.SetupReferences();
		}

		// Token: 0x060016F6 RID: 5878 RVA: 0x0005DB32 File Offset: 0x0005BD32
		internal void FlattenImportedStyleSheetsRecursive()
		{
			this.m_FlattenedImportedStyleSheets = new List<StyleSheet>();
			this.FlattenImportedStyleSheetsRecursive(this);
		}

		// Token: 0x060016F7 RID: 5879 RVA: 0x0005DB48 File Offset: 0x0005BD48
		private void FlattenImportedStyleSheetsRecursive(StyleSheet sheet)
		{
			bool flag = sheet.imports == null;
			if (!flag)
			{
				for (int i = 0; i < sheet.imports.Length; i++)
				{
					StyleSheet styleSheet = sheet.imports[i].styleSheet;
					bool flag2 = styleSheet == null;
					if (!flag2)
					{
						styleSheet.isDefaultStyleSheet = this.isDefaultStyleSheet;
						this.FlattenImportedStyleSheetsRecursive(styleSheet);
						this.m_FlattenedImportedStyleSheets.Add(styleSheet);
					}
				}
			}
		}

		// Token: 0x060016F8 RID: 5880 RVA: 0x0005DBC4 File Offset: 0x0005BDC4
		private void SetupReferences()
		{
			bool flag = this.complexSelectors == null || this.rules == null;
			if (!flag)
			{
				foreach (StyleRule styleRule in this.rules)
				{
					foreach (StyleProperty styleProperty in styleRule.properties)
					{
						bool flag2 = StyleSheet.CustomStartsWith(styleProperty.name, StyleSheet.kCustomPropertyMarker);
						if (flag2)
						{
							styleRule.customPropertiesCount++;
							styleProperty.isCustomProperty = true;
						}
						foreach (StyleValueHandle styleValueHandle in styleProperty.values)
						{
							bool flag3 = styleValueHandle.IsVarFunction();
							if (flag3)
							{
								styleProperty.requireVariableResolve = true;
								break;
							}
						}
					}
				}
				int l = 0;
				int num = this.complexSelectors.Length;
				while (l < num)
				{
					this.complexSelectors[l].CachePseudoStateMasks();
					l++;
				}
				this.orderedClassSelectors = new Dictionary<string, StyleComplexSelector>(StringComparer.Ordinal);
				this.orderedNameSelectors = new Dictionary<string, StyleComplexSelector>(StringComparer.Ordinal);
				this.orderedTypeSelectors = new Dictionary<string, StyleComplexSelector>(StringComparer.Ordinal);
				int m = 0;
				while (m < this.complexSelectors.Length)
				{
					StyleComplexSelector styleComplexSelector = this.complexSelectors[m];
					bool flag4 = styleComplexSelector.ruleIndex < this.rules.Length;
					if (flag4)
					{
						styleComplexSelector.rule = this.rules[styleComplexSelector.ruleIndex];
					}
					styleComplexSelector.orderInStyleSheet = m;
					StyleSelector styleSelector = styleComplexSelector.selectors[styleComplexSelector.selectors.Length - 1];
					StyleSelectorPart styleSelectorPart = styleSelector.parts[0];
					string text = styleSelectorPart.value;
					Dictionary<string, StyleComplexSelector> dictionary = null;
					switch (styleSelectorPart.type)
					{
					case StyleSelectorType.Wildcard:
					case StyleSelectorType.Type:
						text = styleSelectorPart.value ?? "*";
						dictionary = this.orderedTypeSelectors;
						break;
					case StyleSelectorType.Class:
						dictionary = this.orderedClassSelectors;
						break;
					case StyleSelectorType.PseudoClass:
						text = "*";
						dictionary = this.orderedTypeSelectors;
						break;
					case StyleSelectorType.RecursivePseudoClass:
						goto IL_022B;
					case StyleSelectorType.ID:
						dictionary = this.orderedNameSelectors;
						break;
					default:
						goto IL_022B;
					}
					IL_0249:
					bool flag5 = dictionary != null;
					if (flag5)
					{
						StyleComplexSelector styleComplexSelector2;
						bool flag6 = dictionary.TryGetValue(text, ref styleComplexSelector2);
						if (flag6)
						{
							styleComplexSelector.nextInTable = styleComplexSelector2;
						}
						dictionary[text] = styleComplexSelector;
					}
					m++;
					continue;
					IL_022B:
					Debug.LogError(string.Format("Invalid first part type {0}", styleSelectorPart.type));
					goto IL_0249;
				}
			}
		}

		// Token: 0x060016F9 RID: 5881 RVA: 0x0005DE6C File Offset: 0x0005C06C
		internal StyleValueKeyword ReadKeyword(StyleValueHandle handle)
		{
			return (StyleValueKeyword)handle.valueIndex;
		}

		// Token: 0x060016FA RID: 5882 RVA: 0x0005DE84 File Offset: 0x0005C084
		internal float ReadFloat(StyleValueHandle handle)
		{
			bool flag = handle.valueType == StyleValueType.Dimension;
			float num;
			if (flag)
			{
				Dimension dimension = StyleSheet.CheckAccess<Dimension>(this.dimensions, StyleValueType.Dimension, handle);
				num = dimension.value;
			}
			else
			{
				num = StyleSheet.CheckAccess<float>(this.floats, StyleValueType.Float, handle);
			}
			return num;
		}

		// Token: 0x060016FB RID: 5883 RVA: 0x0005DECC File Offset: 0x0005C0CC
		internal bool TryReadFloat(StyleValueHandle handle, out float value)
		{
			bool flag = StyleSheet.TryCheckAccess<float>(this.floats, StyleValueType.Float, handle, out value);
			bool flag2;
			if (flag)
			{
				flag2 = true;
			}
			else
			{
				Dimension dimension;
				bool flag3 = StyleSheet.TryCheckAccess<Dimension>(this.dimensions, StyleValueType.Float, handle, out dimension);
				value = dimension.value;
				flag2 = flag3;
			}
			return flag2;
		}

		// Token: 0x060016FC RID: 5884 RVA: 0x0005DF10 File Offset: 0x0005C110
		internal Dimension ReadDimension(StyleValueHandle handle)
		{
			bool flag = handle.valueType == StyleValueType.Float;
			Dimension dimension;
			if (flag)
			{
				float num = StyleSheet.CheckAccess<float>(this.floats, StyleValueType.Float, handle);
				dimension = new Dimension(num, Dimension.Unit.Unitless);
			}
			else
			{
				dimension = StyleSheet.CheckAccess<Dimension>(this.dimensions, StyleValueType.Dimension, handle);
			}
			return dimension;
		}

		// Token: 0x060016FD RID: 5885 RVA: 0x0005DF58 File Offset: 0x0005C158
		internal bool TryReadDimension(StyleValueHandle handle, out Dimension value)
		{
			bool flag = StyleSheet.TryCheckAccess<Dimension>(this.dimensions, StyleValueType.Dimension, handle, out value);
			bool flag2;
			if (flag)
			{
				flag2 = true;
			}
			else
			{
				float num = 0f;
				bool flag3 = StyleSheet.TryCheckAccess<float>(this.floats, StyleValueType.Float, handle, out num);
				value = new Dimension(num, Dimension.Unit.Unitless);
				flag2 = flag3;
			}
			return flag2;
		}

		// Token: 0x060016FE RID: 5886 RVA: 0x0005DFA4 File Offset: 0x0005C1A4
		internal Color ReadColor(StyleValueHandle handle)
		{
			return StyleSheet.CheckAccess<Color>(this.colors, StyleValueType.Color, handle);
		}

		// Token: 0x060016FF RID: 5887 RVA: 0x0005DFC4 File Offset: 0x0005C1C4
		internal bool TryReadColor(StyleValueHandle handle, out Color value)
		{
			return StyleSheet.TryCheckAccess<Color>(this.colors, StyleValueType.Color, handle, out value);
		}

		// Token: 0x06001700 RID: 5888 RVA: 0x0005DFE4 File Offset: 0x0005C1E4
		internal string ReadString(StyleValueHandle handle)
		{
			return StyleSheet.CheckAccess<string>(this.strings, StyleValueType.String, handle);
		}

		// Token: 0x06001701 RID: 5889 RVA: 0x0005E004 File Offset: 0x0005C204
		internal bool TryReadString(StyleValueHandle handle, out string value)
		{
			return StyleSheet.TryCheckAccess<string>(this.strings, StyleValueType.String, handle, out value);
		}

		// Token: 0x06001702 RID: 5890 RVA: 0x0005E028 File Offset: 0x0005C228
		internal string ReadEnum(StyleValueHandle handle)
		{
			return StyleSheet.CheckAccess<string>(this.strings, StyleValueType.Enum, handle);
		}

		// Token: 0x06001703 RID: 5891 RVA: 0x0005E048 File Offset: 0x0005C248
		internal bool TryReadEnum(StyleValueHandle handle, out string value)
		{
			return StyleSheet.TryCheckAccess<string>(this.strings, StyleValueType.Enum, handle, out value);
		}

		// Token: 0x06001704 RID: 5892 RVA: 0x0005E068 File Offset: 0x0005C268
		internal string ReadVariable(StyleValueHandle handle)
		{
			return StyleSheet.CheckAccess<string>(this.strings, StyleValueType.Variable, handle);
		}

		// Token: 0x06001705 RID: 5893 RVA: 0x0005E088 File Offset: 0x0005C288
		internal bool TryReadVariable(StyleValueHandle handle, out string value)
		{
			return StyleSheet.TryCheckAccess<string>(this.strings, StyleValueType.Variable, handle, out value);
		}

		// Token: 0x06001706 RID: 5894 RVA: 0x0005E0A8 File Offset: 0x0005C2A8
		internal string ReadResourcePath(StyleValueHandle handle)
		{
			return StyleSheet.CheckAccess<string>(this.strings, StyleValueType.ResourcePath, handle);
		}

		// Token: 0x06001707 RID: 5895 RVA: 0x0005E0C8 File Offset: 0x0005C2C8
		internal bool TryReadResourcePath(StyleValueHandle handle, out string value)
		{
			return StyleSheet.TryCheckAccess<string>(this.strings, StyleValueType.ResourcePath, handle, out value);
		}

		// Token: 0x06001708 RID: 5896 RVA: 0x0005E0E8 File Offset: 0x0005C2E8
		internal Object ReadAssetReference(StyleValueHandle handle)
		{
			return StyleSheet.CheckAccess<Object>(this.assets, StyleValueType.AssetReference, handle);
		}

		// Token: 0x06001709 RID: 5897 RVA: 0x0005E108 File Offset: 0x0005C308
		internal string ReadMissingAssetReferenceUrl(StyleValueHandle handle)
		{
			return StyleSheet.CheckAccess<string>(this.strings, StyleValueType.MissingAssetReference, handle);
		}

		// Token: 0x0600170A RID: 5898 RVA: 0x0005E128 File Offset: 0x0005C328
		internal bool TryReadAssetReference(StyleValueHandle handle, out Object value)
		{
			return StyleSheet.TryCheckAccess<Object>(this.assets, StyleValueType.AssetReference, handle, out value);
		}

		// Token: 0x0600170B RID: 5899 RVA: 0x0005E148 File Offset: 0x0005C348
		internal StyleValueFunction ReadFunction(StyleValueHandle handle)
		{
			return (StyleValueFunction)handle.valueIndex;
		}

		// Token: 0x0600170C RID: 5900 RVA: 0x0005E160 File Offset: 0x0005C360
		internal string ReadFunctionName(StyleValueHandle handle)
		{
			bool flag = handle.valueType != StyleValueType.Function;
			string text;
			if (flag)
			{
				Debug.LogErrorFormat(string.Format("Trying to read value of type {0} while reading a value of type {1}", StyleValueType.Function, handle.valueType), new object[0]);
				text = string.Empty;
			}
			else
			{
				StyleValueFunction valueIndex = (StyleValueFunction)handle.valueIndex;
				text = valueIndex.ToUssString();
			}
			return text;
		}

		// Token: 0x0600170D RID: 5901 RVA: 0x0005E1C4 File Offset: 0x0005C3C4
		internal ScalableImage ReadScalableImage(StyleValueHandle handle)
		{
			return StyleSheet.CheckAccess<ScalableImage>(this.scalableImages, StyleValueType.ScalableImage, handle);
		}

		// Token: 0x0600170E RID: 5902 RVA: 0x0005E1E4 File Offset: 0x0005C3E4
		private static bool CustomStartsWith(string originalString, string pattern)
		{
			int length = originalString.Length;
			int length2 = pattern.Length;
			int num = 0;
			int num2 = 0;
			while (num < length && num2 < length2 && originalString.get_Chars(num) == pattern.get_Chars(num2))
			{
				num++;
				num2++;
			}
			return (num2 == length2 && length >= length2) || (num == length && length2 >= length);
		}

		// Token: 0x040009A6 RID: 2470
		[SerializeField]
		private bool m_ImportedWithErrors;

		// Token: 0x040009A7 RID: 2471
		[SerializeField]
		private bool m_ImportedWithWarnings;

		// Token: 0x040009A8 RID: 2472
		[SerializeField]
		private StyleRule[] m_Rules;

		// Token: 0x040009A9 RID: 2473
		[SerializeField]
		private StyleComplexSelector[] m_ComplexSelectors;

		// Token: 0x040009AA RID: 2474
		[SerializeField]
		internal float[] floats;

		// Token: 0x040009AB RID: 2475
		[SerializeField]
		internal Dimension[] dimensions;

		// Token: 0x040009AC RID: 2476
		[SerializeField]
		internal Color[] colors;

		// Token: 0x040009AD RID: 2477
		[SerializeField]
		internal string[] strings;

		// Token: 0x040009AE RID: 2478
		[SerializeField]
		internal Object[] assets;

		// Token: 0x040009AF RID: 2479
		[SerializeField]
		internal StyleSheet.ImportStruct[] imports;

		// Token: 0x040009B0 RID: 2480
		[SerializeField]
		private List<StyleSheet> m_FlattenedImportedStyleSheets;

		// Token: 0x040009B1 RID: 2481
		[SerializeField]
		private int m_ContentHash;

		// Token: 0x040009B2 RID: 2482
		[SerializeField]
		internal ScalableImage[] scalableImages;

		// Token: 0x040009B3 RID: 2483
		[NonSerialized]
		internal Dictionary<string, StyleComplexSelector> orderedNameSelectors;

		// Token: 0x040009B4 RID: 2484
		[NonSerialized]
		internal Dictionary<string, StyleComplexSelector> orderedTypeSelectors;

		// Token: 0x040009B5 RID: 2485
		[NonSerialized]
		internal Dictionary<string, StyleComplexSelector> orderedClassSelectors;

		// Token: 0x040009B6 RID: 2486
		[NonSerialized]
		private bool m_IsDefaultStyleSheet;

		// Token: 0x040009B7 RID: 2487
		private static string kCustomPropertyMarker = "--";

		// Token: 0x020002A7 RID: 679
		[Serializable]
		internal struct ImportStruct
		{
			// Token: 0x040009B8 RID: 2488
			public StyleSheet styleSheet;

			// Token: 0x040009B9 RID: 2489
			public string[] mediaQueries;
		}
	}
}
