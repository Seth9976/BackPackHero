using System;
using System.Collections.Generic;
using UnityEngine.UIElements.StyleSheets;
using UnityEngine.UIElements.StyleSheets.Syntax;

namespace UnityEngine.UIElements
{
	// Token: 0x020002B0 RID: 688
	internal class StyleVariableResolver
	{
		// Token: 0x1700059F RID: 1439
		// (get) Token: 0x06001721 RID: 5921 RVA: 0x0005E66C File Offset: 0x0005C86C
		private StyleSheet currentSheet
		{
			get
			{
				return this.m_CurrentContext.sheet;
			}
		}

		// Token: 0x170005A0 RID: 1440
		// (get) Token: 0x06001722 RID: 5922 RVA: 0x0005E679 File Offset: 0x0005C879
		private StyleValueHandle[] currentHandles
		{
			get
			{
				return this.m_CurrentContext.handles;
			}
		}

		// Token: 0x170005A1 RID: 1441
		// (get) Token: 0x06001723 RID: 5923 RVA: 0x0005E686 File Offset: 0x0005C886
		public List<StylePropertyValue> resolvedValues
		{
			get
			{
				return this.m_ResolvedValues;
			}
		}

		// Token: 0x170005A2 RID: 1442
		// (get) Token: 0x06001724 RID: 5924 RVA: 0x0005E68E File Offset: 0x0005C88E
		// (set) Token: 0x06001725 RID: 5925 RVA: 0x0005E696 File Offset: 0x0005C896
		public StyleVariableContext variableContext { get; set; }

		// Token: 0x06001726 RID: 5926 RVA: 0x0005E69F File Offset: 0x0005C89F
		public void Init(StyleProperty property, StyleSheet sheet, StyleValueHandle[] handles)
		{
			this.m_ResolvedValues.Clear();
			this.m_ContextStack.Clear();
			this.m_Property = property;
			this.PushContext(sheet, handles);
		}

		// Token: 0x06001727 RID: 5927 RVA: 0x0005E6CC File Offset: 0x0005C8CC
		private void PushContext(StyleSheet sheet, StyleValueHandle[] handles)
		{
			this.m_CurrentContext = new StyleVariableResolver.ResolveContext
			{
				sheet = sheet,
				handles = handles
			};
			this.m_ContextStack.Push(this.m_CurrentContext);
		}

		// Token: 0x06001728 RID: 5928 RVA: 0x0005E70B File Offset: 0x0005C90B
		private void PopContext()
		{
			this.m_ContextStack.Pop();
			this.m_CurrentContext = this.m_ContextStack.Peek();
		}

		// Token: 0x06001729 RID: 5929 RVA: 0x0005E72C File Offset: 0x0005C92C
		public void AddValue(StyleValueHandle handle)
		{
			this.m_ResolvedValues.Add(new StylePropertyValue
			{
				sheet = this.currentSheet,
				handle = handle
			});
		}

		// Token: 0x0600172A RID: 5930 RVA: 0x0005E764 File Offset: 0x0005C964
		public bool ResolveVarFunction(ref int index)
		{
			this.m_ResolvedVarStack.Clear();
			int num;
			string text;
			StyleVariableResolver.ParseVarFunction(this.currentSheet, this.currentHandles, ref index, out num, out text);
			StyleVariableResolver.Result result = this.ResolveVarFunction(ref index, num, text);
			return result == StyleVariableResolver.Result.Valid;
		}

		// Token: 0x0600172B RID: 5931 RVA: 0x0005E7A8 File Offset: 0x0005C9A8
		private StyleVariableResolver.Result ResolveVarFunction(ref int index, int argc, string varName)
		{
			StyleVariableResolver.Result result = this.ResolveVariable(varName);
			bool flag = result == StyleVariableResolver.Result.NotFound && argc > 1;
			if (flag)
			{
				StyleValueHandle[] currentHandles = this.currentHandles;
				int num = index + 1;
				index = num;
				StyleValueHandle styleValueHandle = currentHandles[num];
				Debug.Assert(styleValueHandle.valueType == StyleValueType.CommaSeparator, string.Format("Unexpected value type {0} in var function", styleValueHandle.valueType));
				bool flag2 = styleValueHandle.valueType == StyleValueType.CommaSeparator && index + 1 < this.currentHandles.Length;
				if (flag2)
				{
					index++;
					result = this.ResolveFallback(ref index);
				}
			}
			return result;
		}

		// Token: 0x0600172C RID: 5932 RVA: 0x0005E844 File Offset: 0x0005CA44
		public bool ValidateResolvedValues()
		{
			bool isCustomProperty = this.m_Property.isCustomProperty;
			bool flag;
			if (isCustomProperty)
			{
				flag = true;
			}
			else
			{
				string text;
				bool flag2 = !StylePropertyCache.TryGetSyntax(this.m_Property.name, out text);
				if (flag2)
				{
					Debug.LogAssertion("Unknown style property " + this.m_Property.name);
					flag = false;
				}
				else
				{
					Expression expression = StyleVariableResolver.s_SyntaxParser.Parse(text);
					flag = this.m_Matcher.Match(expression, this.m_ResolvedValues).success;
				}
			}
			return flag;
		}

		// Token: 0x0600172D RID: 5933 RVA: 0x0005E8D0 File Offset: 0x0005CAD0
		private StyleVariableResolver.Result ResolveVariable(string variableName)
		{
			StyleVariable styleVariable;
			bool flag = !this.variableContext.TryFindVariable(variableName, out styleVariable);
			StyleVariableResolver.Result result;
			if (flag)
			{
				result = StyleVariableResolver.Result.NotFound;
			}
			else
			{
				bool flag2 = this.m_ResolvedVarStack.Contains(styleVariable.name);
				if (flag2)
				{
					result = StyleVariableResolver.Result.NotFound;
				}
				else
				{
					this.m_ResolvedVarStack.Push(styleVariable.name);
					StyleVariableResolver.Result result2 = StyleVariableResolver.Result.Valid;
					int num = 0;
					while (num < styleVariable.handles.Length && result2 == StyleVariableResolver.Result.Valid)
					{
						bool flag3 = this.m_ResolvedValues.Count + 1 > 100;
						if (flag3)
						{
							return StyleVariableResolver.Result.Invalid;
						}
						StyleValueHandle styleValueHandle = styleVariable.handles[num];
						bool flag4 = styleValueHandle.IsVarFunction();
						if (flag4)
						{
							this.PushContext(styleVariable.sheet, styleVariable.handles);
							int num2;
							string text;
							StyleVariableResolver.ParseVarFunction(styleVariable.sheet, styleVariable.handles, ref num, out num2, out text);
							result2 = this.ResolveVarFunction(ref num, num2, text);
							this.PopContext();
						}
						else
						{
							this.m_ResolvedValues.Add(new StylePropertyValue
							{
								sheet = styleVariable.sheet,
								handle = styleValueHandle
							});
						}
						num++;
					}
					this.m_ResolvedVarStack.Pop();
					result = result2;
				}
			}
			return result;
		}

		// Token: 0x0600172E RID: 5934 RVA: 0x0005EA18 File Offset: 0x0005CC18
		private StyleVariableResolver.Result ResolveFallback(ref int index)
		{
			StyleVariableResolver.Result result = StyleVariableResolver.Result.Valid;
			while (index < this.currentHandles.Length && result == StyleVariableResolver.Result.Valid)
			{
				StyleValueHandle styleValueHandle = this.currentHandles[index];
				bool flag = styleValueHandle.IsVarFunction();
				if (flag)
				{
					int num;
					string text;
					StyleVariableResolver.ParseVarFunction(this.currentSheet, this.currentHandles, ref index, out num, out text);
					result = this.ResolveVariable(text);
					bool flag2 = result == StyleVariableResolver.Result.NotFound;
					if (flag2)
					{
						bool flag3 = num > 1;
						if (flag3)
						{
							StyleValueHandle[] currentHandles = this.currentHandles;
							int num2 = index + 1;
							index = num2;
							styleValueHandle = currentHandles[num2];
							Debug.Assert(styleValueHandle.valueType == StyleValueType.CommaSeparator, string.Format("Unexpected value type {0} in var function", styleValueHandle.valueType));
							bool flag4 = styleValueHandle.valueType == StyleValueType.CommaSeparator && index + 1 < this.currentHandles.Length;
							if (flag4)
							{
								index++;
								result = this.ResolveFallback(ref index);
							}
						}
					}
				}
				else
				{
					this.m_ResolvedValues.Add(new StylePropertyValue
					{
						sheet = this.currentSheet,
						handle = styleValueHandle
					});
				}
				index++;
			}
			return result;
		}

		// Token: 0x0600172F RID: 5935 RVA: 0x0005EB50 File Offset: 0x0005CD50
		private static void ParseVarFunction(StyleSheet sheet, StyleValueHandle[] handles, ref int index, out int argCount, out string variableName)
		{
			int num = index + 1;
			index = num;
			argCount = (int)sheet.ReadFloat(handles[num]);
			num = index + 1;
			index = num;
			variableName = sheet.ReadVariable(handles[num]);
		}

		// Token: 0x040009E2 RID: 2530
		internal const int kMaxResolves = 100;

		// Token: 0x040009E3 RID: 2531
		private static StyleSyntaxParser s_SyntaxParser = new StyleSyntaxParser();

		// Token: 0x040009E4 RID: 2532
		private StylePropertyValueMatcher m_Matcher = new StylePropertyValueMatcher();

		// Token: 0x040009E5 RID: 2533
		private List<StylePropertyValue> m_ResolvedValues = new List<StylePropertyValue>();

		// Token: 0x040009E6 RID: 2534
		private Stack<string> m_ResolvedVarStack = new Stack<string>();

		// Token: 0x040009E7 RID: 2535
		private StyleProperty m_Property;

		// Token: 0x040009E8 RID: 2536
		private Stack<StyleVariableResolver.ResolveContext> m_ContextStack = new Stack<StyleVariableResolver.ResolveContext>();

		// Token: 0x040009E9 RID: 2537
		private StyleVariableResolver.ResolveContext m_CurrentContext;

		// Token: 0x020002B1 RID: 689
		private enum Result
		{
			// Token: 0x040009EC RID: 2540
			Valid,
			// Token: 0x040009ED RID: 2541
			Invalid,
			// Token: 0x040009EE RID: 2542
			NotFound
		}

		// Token: 0x020002B2 RID: 690
		private struct ResolveContext
		{
			// Token: 0x040009EF RID: 2543
			public StyleSheet sheet;

			// Token: 0x040009F0 RID: 2544
			public StyleValueHandle[] handles;
		}
	}
}
