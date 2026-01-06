using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine.UIElements.StyleSheets.Syntax;

namespace UnityEngine.UIElements.StyleSheets
{
	// Token: 0x02000370 RID: 880
	internal class StylePropertyValueMatcher : BaseStyleMatcher
	{
		// Token: 0x170006BE RID: 1726
		// (get) Token: 0x06001C23 RID: 7203 RVA: 0x0008309C File Offset: 0x0008129C
		private StylePropertyValue current
		{
			get
			{
				return base.hasCurrent ? this.m_Values[base.currentIndex] : default(StylePropertyValue);
			}
		}

		// Token: 0x170006BF RID: 1727
		// (get) Token: 0x06001C24 RID: 7204 RVA: 0x000830CD File Offset: 0x000812CD
		public override int valueCount
		{
			get
			{
				return this.m_Values.Count;
			}
		}

		// Token: 0x170006C0 RID: 1728
		// (get) Token: 0x06001C25 RID: 7205 RVA: 0x00004D72 File Offset: 0x00002F72
		public override bool isCurrentVariable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170006C1 RID: 1729
		// (get) Token: 0x06001C26 RID: 7206 RVA: 0x000830DC File Offset: 0x000812DC
		public override bool isCurrentComma
		{
			get
			{
				return base.hasCurrent && this.m_Values[base.currentIndex].handle.valueType == StyleValueType.CommaSeparator;
			}
		}

		// Token: 0x06001C27 RID: 7207 RVA: 0x00083118 File Offset: 0x00081318
		public MatchResult Match(Expression exp, List<StylePropertyValue> values)
		{
			MatchResult matchResult = new MatchResult
			{
				errorCode = MatchResultErrorCode.None
			};
			bool flag = values == null || values.Count == 0;
			MatchResult matchResult2;
			if (flag)
			{
				matchResult.errorCode = MatchResultErrorCode.EmptyValue;
				matchResult2 = matchResult;
			}
			else
			{
				base.Initialize();
				this.m_Values = values;
				StyleValueHandle handle = this.m_Values[0].handle;
				bool flag2 = handle.valueType == StyleValueType.Keyword && handle.valueIndex == 1;
				bool flag3;
				if (flag2)
				{
					base.MoveNext();
					flag3 = true;
				}
				else
				{
					flag3 = base.Match(exp);
				}
				bool flag4 = !flag3;
				if (flag4)
				{
					StyleSheet sheet = this.current.sheet;
					matchResult.errorCode = MatchResultErrorCode.Syntax;
					matchResult.errorValue = sheet.ReadAsString(this.current.handle);
				}
				else
				{
					bool hasCurrent = base.hasCurrent;
					if (hasCurrent)
					{
						StyleSheet sheet2 = this.current.sheet;
						matchResult.errorCode = MatchResultErrorCode.ExpectedEndOfValue;
						matchResult.errorValue = sheet2.ReadAsString(this.current.handle);
					}
				}
				matchResult2 = matchResult;
			}
			return matchResult2;
		}

		// Token: 0x06001C28 RID: 7208 RVA: 0x00083234 File Offset: 0x00081434
		protected override bool MatchKeyword(string keyword)
		{
			StylePropertyValue current = this.current;
			bool flag = current.handle.valueType == StyleValueType.Keyword;
			bool flag2;
			if (flag)
			{
				StyleValueKeyword valueIndex = (StyleValueKeyword)current.handle.valueIndex;
				flag2 = valueIndex.ToUssString() == keyword.ToLower();
			}
			else
			{
				bool flag3 = current.handle.valueType == StyleValueType.Enum;
				if (flag3)
				{
					string text = current.sheet.ReadEnum(current.handle);
					flag2 = text == keyword.ToLower();
				}
				else
				{
					flag2 = false;
				}
			}
			return flag2;
		}

		// Token: 0x06001C29 RID: 7209 RVA: 0x000832BC File Offset: 0x000814BC
		protected override bool MatchNumber()
		{
			return this.current.handle.valueType == StyleValueType.Float;
		}

		// Token: 0x06001C2A RID: 7210 RVA: 0x000832E4 File Offset: 0x000814E4
		protected override bool MatchInteger()
		{
			return this.current.handle.valueType == StyleValueType.Float;
		}

		// Token: 0x06001C2B RID: 7211 RVA: 0x0008330C File Offset: 0x0008150C
		protected override bool MatchLength()
		{
			StylePropertyValue current = this.current;
			bool flag = current.handle.valueType == StyleValueType.Dimension;
			bool flag2;
			if (flag)
			{
				Dimension dimension = current.sheet.ReadDimension(current.handle);
				flag2 = dimension.unit == Dimension.Unit.Pixel;
			}
			else
			{
				bool flag3 = current.handle.valueType == StyleValueType.Float;
				if (flag3)
				{
					float num = current.sheet.ReadFloat(current.handle);
					flag2 = Mathf.Approximately(0f, num);
				}
				else
				{
					flag2 = false;
				}
			}
			return flag2;
		}

		// Token: 0x06001C2C RID: 7212 RVA: 0x00083390 File Offset: 0x00081590
		protected override bool MatchPercentage()
		{
			StylePropertyValue current = this.current;
			bool flag = current.handle.valueType == StyleValueType.Dimension;
			bool flag2;
			if (flag)
			{
				Dimension dimension = current.sheet.ReadDimension(current.handle);
				flag2 = dimension.unit == Dimension.Unit.Percent;
			}
			else
			{
				bool flag3 = current.handle.valueType == StyleValueType.Float;
				if (flag3)
				{
					float num = current.sheet.ReadFloat(current.handle);
					flag2 = Mathf.Approximately(0f, num);
				}
				else
				{
					flag2 = false;
				}
			}
			return flag2;
		}

		// Token: 0x06001C2D RID: 7213 RVA: 0x00083414 File Offset: 0x00081614
		protected override bool MatchColor()
		{
			StylePropertyValue current = this.current;
			bool flag = current.handle.valueType == StyleValueType.Color;
			bool flag2;
			if (flag)
			{
				flag2 = true;
			}
			else
			{
				bool flag3 = current.handle.valueType == StyleValueType.Enum;
				if (flag3)
				{
					Color clear = Color.clear;
					string text = current.sheet.ReadAsString(current.handle);
					bool flag4 = StyleSheetColor.TryGetColor(text.ToLower(), out clear);
					if (flag4)
					{
						return true;
					}
				}
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x06001C2E RID: 7214 RVA: 0x0008348C File Offset: 0x0008168C
		protected override bool MatchResource()
		{
			return this.current.handle.valueType == StyleValueType.ResourcePath;
		}

		// Token: 0x06001C2F RID: 7215 RVA: 0x000834B4 File Offset: 0x000816B4
		protected override bool MatchUrl()
		{
			StyleValueType valueType = this.current.handle.valueType;
			return valueType == StyleValueType.AssetReference || valueType == StyleValueType.ScalableImage;
		}

		// Token: 0x06001C30 RID: 7216 RVA: 0x000834EC File Offset: 0x000816EC
		protected override bool MatchTime()
		{
			StylePropertyValue current = this.current;
			bool flag = current.handle.valueType == StyleValueType.Dimension;
			bool flag2;
			if (flag)
			{
				Dimension dimension = current.sheet.ReadDimension(current.handle);
				flag2 = dimension.unit == Dimension.Unit.Second || dimension.unit == Dimension.Unit.Millisecond;
			}
			else
			{
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x06001C31 RID: 7217 RVA: 0x00083544 File Offset: 0x00081744
		protected override bool MatchCustomIdent()
		{
			StylePropertyValue current = this.current;
			bool flag = current.handle.valueType == StyleValueType.Enum;
			bool flag2;
			if (flag)
			{
				string text = current.sheet.ReadAsString(current.handle);
				Match match = BaseStyleMatcher.s_CustomIdentRegex.Match(text);
				flag2 = match.Success && match.Length == text.Length;
			}
			else
			{
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x06001C32 RID: 7218 RVA: 0x000835B0 File Offset: 0x000817B0
		protected override bool MatchAngle()
		{
			StylePropertyValue current = this.current;
			bool flag = current.handle.valueType == StyleValueType.Dimension;
			if (flag)
			{
				Dimension dimension = current.sheet.ReadDimension(current.handle);
				Dimension.Unit unit = dimension.unit;
				Dimension.Unit unit2 = unit;
				if (unit2 - Dimension.Unit.Degree <= 3)
				{
					return true;
				}
			}
			bool flag2 = current.handle.valueType == StyleValueType.Float;
			bool flag3;
			if (flag2)
			{
				float num = current.sheet.ReadFloat(current.handle);
				flag3 = Mathf.Approximately(0f, num);
			}
			else
			{
				flag3 = false;
			}
			return flag3;
		}

		// Token: 0x04000E01 RID: 3585
		private List<StylePropertyValue> m_Values;
	}
}
