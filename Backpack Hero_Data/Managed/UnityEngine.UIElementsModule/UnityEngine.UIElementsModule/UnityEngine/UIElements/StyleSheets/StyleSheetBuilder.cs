using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements.StyleSheets
{
	// Token: 0x02000361 RID: 865
	internal class StyleSheetBuilder
	{
		// Token: 0x170006B2 RID: 1714
		// (get) Token: 0x06001BC2 RID: 7106 RVA: 0x00080D08 File Offset: 0x0007EF08
		public StyleProperty currentProperty
		{
			get
			{
				return this.m_CurrentProperty;
			}
		}

		// Token: 0x06001BC3 RID: 7107 RVA: 0x00080D10 File Offset: 0x0007EF10
		public StyleRule BeginRule(int ruleLine)
		{
			StyleSheetBuilder.Log("Beginning rule");
			Debug.Assert(this.m_BuilderState == StyleSheetBuilder.BuilderState.Init);
			this.m_BuilderState = StyleSheetBuilder.BuilderState.Rule;
			this.m_CurrentRule = new StyleRule
			{
				line = ruleLine
			};
			return this.m_CurrentRule;
		}

		// Token: 0x06001BC4 RID: 7108 RVA: 0x00080D5C File Offset: 0x0007EF5C
		public StyleSheetBuilder.ComplexSelectorScope BeginComplexSelector(int specificity)
		{
			StyleSheetBuilder.Log("Begin complex selector with specificity " + specificity.ToString());
			Debug.Assert(this.m_BuilderState == StyleSheetBuilder.BuilderState.Rule);
			this.m_BuilderState = StyleSheetBuilder.BuilderState.ComplexSelector;
			this.m_CurrentComplexSelector = new StyleComplexSelector();
			this.m_CurrentComplexSelector.specificity = specificity;
			this.m_CurrentComplexSelector.ruleIndex = this.m_Rules.Count;
			return new StyleSheetBuilder.ComplexSelectorScope(this);
		}

		// Token: 0x06001BC5 RID: 7109 RVA: 0x00080DD0 File Offset: 0x0007EFD0
		public void AddSimpleSelector(StyleSelectorPart[] parts, StyleSelectorRelationship previousRelationsip)
		{
			Debug.Assert(this.m_BuilderState == StyleSheetBuilder.BuilderState.ComplexSelector);
			StyleSelector styleSelector = new StyleSelector();
			styleSelector.parts = parts;
			styleSelector.previousRelationship = previousRelationsip;
			string text = "Add simple selector ";
			StyleSelector styleSelector2 = styleSelector;
			StyleSheetBuilder.Log(text + ((styleSelector2 != null) ? styleSelector2.ToString() : null));
			this.m_CurrentSelectors.Add(styleSelector);
		}

		// Token: 0x06001BC6 RID: 7110 RVA: 0x00080E30 File Offset: 0x0007F030
		public void EndComplexSelector()
		{
			StyleSheetBuilder.Log("Ending complex selector");
			Debug.Assert(this.m_BuilderState == StyleSheetBuilder.BuilderState.ComplexSelector);
			this.m_BuilderState = StyleSheetBuilder.BuilderState.Rule;
			bool flag = this.m_CurrentSelectors.Count > 0;
			if (flag)
			{
				this.m_CurrentComplexSelector.selectors = this.m_CurrentSelectors.ToArray();
				this.m_ComplexSelectors.Add(this.m_CurrentComplexSelector);
				this.m_CurrentSelectors.Clear();
			}
			this.m_CurrentComplexSelector = null;
		}

		// Token: 0x06001BC7 RID: 7111 RVA: 0x00080EB0 File Offset: 0x0007F0B0
		public StyleProperty BeginProperty(string name, int line = -1)
		{
			StyleSheetBuilder.Log("Begin property named " + name);
			Debug.Assert(this.m_BuilderState == StyleSheetBuilder.BuilderState.Rule);
			this.m_BuilderState = StyleSheetBuilder.BuilderState.Property;
			this.m_CurrentProperty = new StyleProperty
			{
				name = name,
				line = line
			};
			this.m_CurrentProperties.Add(this.m_CurrentProperty);
			return this.m_CurrentProperty;
		}

		// Token: 0x06001BC8 RID: 7112 RVA: 0x00080F1C File Offset: 0x0007F11C
		public void AddImport(StyleSheet.ImportStruct importStruct)
		{
			this.m_Imports.Add(importStruct);
		}

		// Token: 0x06001BC9 RID: 7113 RVA: 0x00080F2C File Offset: 0x0007F12C
		public void AddValue(float value)
		{
			this.RegisterValue<float>(this.m_Floats, StyleValueType.Float, value);
		}

		// Token: 0x06001BCA RID: 7114 RVA: 0x00080F3E File Offset: 0x0007F13E
		public void AddValue(Dimension value)
		{
			this.RegisterValue<Dimension>(this.m_Dimensions, StyleValueType.Dimension, value);
		}

		// Token: 0x06001BCB RID: 7115 RVA: 0x00080F50 File Offset: 0x0007F150
		public void AddValue(StyleValueKeyword keyword)
		{
			this.m_CurrentValues.Add(new StyleValueHandle((int)keyword, StyleValueType.Keyword));
		}

		// Token: 0x06001BCC RID: 7116 RVA: 0x00080F66 File Offset: 0x0007F166
		public void AddValue(StyleValueFunction function)
		{
			this.m_CurrentValues.Add(new StyleValueHandle((int)function, StyleValueType.Function));
		}

		// Token: 0x06001BCD RID: 7117 RVA: 0x00080F7D File Offset: 0x0007F17D
		public void AddCommaSeparator()
		{
			this.m_CurrentValues.Add(new StyleValueHandle(0, StyleValueType.CommaSeparator));
		}

		// Token: 0x06001BCE RID: 7118 RVA: 0x00080F94 File Offset: 0x0007F194
		public void AddValue(string value, StyleValueType type)
		{
			bool flag = type == StyleValueType.Variable;
			if (flag)
			{
				this.RegisterVariable(value);
			}
			else
			{
				this.RegisterValue<string>(this.m_Strings, type, value);
			}
		}

		// Token: 0x06001BCF RID: 7119 RVA: 0x00080FC3 File Offset: 0x0007F1C3
		public void AddValue(Color value)
		{
			this.RegisterValue<Color>(this.m_Colors, StyleValueType.Color, value);
		}

		// Token: 0x06001BD0 RID: 7120 RVA: 0x00080FD5 File Offset: 0x0007F1D5
		public void AddValue(Object value)
		{
			this.RegisterValue<Object>(this.m_Assets, StyleValueType.AssetReference, value);
		}

		// Token: 0x06001BD1 RID: 7121 RVA: 0x00080FE7 File Offset: 0x0007F1E7
		public void AddValue(ScalableImage value)
		{
			this.RegisterValue<ScalableImage>(this.m_ScalableImages, StyleValueType.ScalableImage, value);
		}

		// Token: 0x06001BD2 RID: 7122 RVA: 0x00080FFC File Offset: 0x0007F1FC
		public void EndProperty()
		{
			StyleSheetBuilder.Log("Ending property");
			Debug.Assert(this.m_BuilderState == StyleSheetBuilder.BuilderState.Property);
			this.m_BuilderState = StyleSheetBuilder.BuilderState.Rule;
			this.m_CurrentProperty.values = this.m_CurrentValues.ToArray();
			this.m_CurrentProperty = null;
			this.m_CurrentValues.Clear();
		}

		// Token: 0x06001BD3 RID: 7123 RVA: 0x00081058 File Offset: 0x0007F258
		public int EndRule()
		{
			StyleSheetBuilder.Log("Ending rule");
			Debug.Assert(this.m_BuilderState == StyleSheetBuilder.BuilderState.Rule);
			this.m_BuilderState = StyleSheetBuilder.BuilderState.Init;
			this.m_CurrentRule.properties = this.m_CurrentProperties.ToArray();
			this.m_Rules.Add(this.m_CurrentRule);
			this.m_CurrentRule = null;
			this.m_CurrentProperties.Clear();
			return this.m_Rules.Count - 1;
		}

		// Token: 0x06001BD4 RID: 7124 RVA: 0x000810D4 File Offset: 0x0007F2D4
		public void BuildTo(StyleSheet writeTo)
		{
			Debug.Assert(this.m_BuilderState == StyleSheetBuilder.BuilderState.Init);
			writeTo.floats = this.m_Floats.ToArray();
			writeTo.dimensions = this.m_Dimensions.ToArray();
			writeTo.colors = this.m_Colors.ToArray();
			writeTo.strings = this.m_Strings.ToArray();
			writeTo.rules = this.m_Rules.ToArray();
			writeTo.assets = this.m_Assets.ToArray();
			writeTo.scalableImages = this.m_ScalableImages.ToArray();
			writeTo.complexSelectors = this.m_ComplexSelectors.ToArray();
			writeTo.imports = this.m_Imports.ToArray();
			bool flag = writeTo.imports.Length != 0;
			if (flag)
			{
				writeTo.FlattenImportedStyleSheetsRecursive();
			}
		}

		// Token: 0x06001BD5 RID: 7125 RVA: 0x000811A4 File Offset: 0x0007F3A4
		private void RegisterVariable(string value)
		{
			StyleSheetBuilder.Log("Add variable : " + value);
			Debug.Assert(this.m_BuilderState == StyleSheetBuilder.BuilderState.Property);
			int num = this.m_Strings.IndexOf(value);
			bool flag = num < 0;
			if (flag)
			{
				this.m_Strings.Add(value);
				num = this.m_Strings.Count - 1;
			}
			this.m_CurrentValues.Add(new StyleValueHandle(num, StyleValueType.Variable));
		}

		// Token: 0x06001BD6 RID: 7126 RVA: 0x00081218 File Offset: 0x0007F418
		private void RegisterValue<T>(List<T> list, StyleValueType type, T value)
		{
			string text = "Add value of type ";
			string text2 = type.ToString();
			string text3 = " : ";
			T t = value;
			StyleSheetBuilder.Log(text + text2 + text3 + ((t != null) ? t.ToString() : null));
			Debug.Assert(this.m_BuilderState == StyleSheetBuilder.BuilderState.Property);
			list.Add(value);
			this.m_CurrentValues.Add(new StyleValueHandle(list.Count - 1, type));
		}

		// Token: 0x06001BD7 RID: 7127 RVA: 0x000020E6 File Offset: 0x000002E6
		private static void Log(string msg)
		{
		}

		// Token: 0x04000DC2 RID: 3522
		private StyleSheetBuilder.BuilderState m_BuilderState;

		// Token: 0x04000DC3 RID: 3523
		private List<float> m_Floats = new List<float>();

		// Token: 0x04000DC4 RID: 3524
		private List<Dimension> m_Dimensions = new List<Dimension>();

		// Token: 0x04000DC5 RID: 3525
		private List<Color> m_Colors = new List<Color>();

		// Token: 0x04000DC6 RID: 3526
		private List<string> m_Strings = new List<string>();

		// Token: 0x04000DC7 RID: 3527
		private List<StyleRule> m_Rules = new List<StyleRule>();

		// Token: 0x04000DC8 RID: 3528
		private List<Object> m_Assets = new List<Object>();

		// Token: 0x04000DC9 RID: 3529
		private List<ScalableImage> m_ScalableImages = new List<ScalableImage>();

		// Token: 0x04000DCA RID: 3530
		private List<StyleComplexSelector> m_ComplexSelectors = new List<StyleComplexSelector>();

		// Token: 0x04000DCB RID: 3531
		private List<StyleProperty> m_CurrentProperties = new List<StyleProperty>();

		// Token: 0x04000DCC RID: 3532
		private List<StyleValueHandle> m_CurrentValues = new List<StyleValueHandle>();

		// Token: 0x04000DCD RID: 3533
		private StyleComplexSelector m_CurrentComplexSelector;

		// Token: 0x04000DCE RID: 3534
		private List<StyleSelector> m_CurrentSelectors = new List<StyleSelector>();

		// Token: 0x04000DCF RID: 3535
		private StyleProperty m_CurrentProperty;

		// Token: 0x04000DD0 RID: 3536
		private StyleRule m_CurrentRule;

		// Token: 0x04000DD1 RID: 3537
		private List<StyleSheet.ImportStruct> m_Imports = new List<StyleSheet.ImportStruct>();

		// Token: 0x02000362 RID: 866
		public struct ComplexSelectorScope : IDisposable
		{
			// Token: 0x06001BD9 RID: 7129 RVA: 0x0008132C File Offset: 0x0007F52C
			public ComplexSelectorScope(StyleSheetBuilder builder)
			{
				this.m_Builder = builder;
			}

			// Token: 0x06001BDA RID: 7130 RVA: 0x00081336 File Offset: 0x0007F536
			public void Dispose()
			{
				this.m_Builder.EndComplexSelector();
			}

			// Token: 0x04000DD2 RID: 3538
			private StyleSheetBuilder m_Builder;
		}

		// Token: 0x02000363 RID: 867
		private enum BuilderState
		{
			// Token: 0x04000DD4 RID: 3540
			Init,
			// Token: 0x04000DD5 RID: 3541
			Rule,
			// Token: 0x04000DD6 RID: 3542
			ComplexSelector,
			// Token: 0x04000DD7 RID: 3543
			Property
		}
	}
}
