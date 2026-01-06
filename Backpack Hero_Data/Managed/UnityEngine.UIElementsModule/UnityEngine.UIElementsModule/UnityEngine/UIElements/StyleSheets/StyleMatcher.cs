using System;
using System.Text.RegularExpressions;
using UnityEngine.UIElements.StyleSheets.Syntax;

namespace UnityEngine.UIElements.StyleSheets
{
	// Token: 0x0200036F RID: 879
	internal class StyleMatcher : BaseStyleMatcher
	{
		// Token: 0x170006BA RID: 1722
		// (get) Token: 0x06001C10 RID: 7184 RVA: 0x00082B32 File Offset: 0x00080D32
		private string current
		{
			get
			{
				return base.hasCurrent ? this.m_PropertyParts[base.currentIndex] : null;
			}
		}

		// Token: 0x170006BB RID: 1723
		// (get) Token: 0x06001C11 RID: 7185 RVA: 0x00082B4C File Offset: 0x00080D4C
		public override int valueCount
		{
			get
			{
				return this.m_PropertyParts.Length;
			}
		}

		// Token: 0x170006BC RID: 1724
		// (get) Token: 0x06001C12 RID: 7186 RVA: 0x00082B56 File Offset: 0x00080D56
		public override bool isCurrentVariable
		{
			get
			{
				return base.hasCurrent && this.current.StartsWith("var(");
			}
		}

		// Token: 0x170006BD RID: 1725
		// (get) Token: 0x06001C13 RID: 7187 RVA: 0x00082B73 File Offset: 0x00080D73
		public override bool isCurrentComma
		{
			get
			{
				return base.hasCurrent && this.current == ",";
			}
		}

		// Token: 0x06001C14 RID: 7188 RVA: 0x00082B90 File Offset: 0x00080D90
		private void Initialize(string propertyValue)
		{
			base.Initialize();
			this.m_PropertyParts = this.m_Parser.Parse(propertyValue);
		}

		// Token: 0x06001C15 RID: 7189 RVA: 0x00082BAC File Offset: 0x00080DAC
		public MatchResult Match(Expression exp, string propertyValue)
		{
			MatchResult matchResult = new MatchResult
			{
				errorCode = MatchResultErrorCode.None
			};
			bool flag = string.IsNullOrEmpty(propertyValue);
			MatchResult matchResult2;
			if (flag)
			{
				matchResult.errorCode = MatchResultErrorCode.EmptyValue;
				matchResult2 = matchResult;
			}
			else
			{
				this.Initialize(propertyValue);
				string current = this.current;
				bool flag2 = current == "initial" || current.StartsWith("env(");
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
					matchResult.errorCode = MatchResultErrorCode.Syntax;
					matchResult.errorValue = this.current;
				}
				else
				{
					bool hasCurrent = base.hasCurrent;
					if (hasCurrent)
					{
						matchResult.errorCode = MatchResultErrorCode.ExpectedEndOfValue;
						matchResult.errorValue = this.current;
					}
				}
				matchResult2 = matchResult;
			}
			return matchResult2;
		}

		// Token: 0x06001C16 RID: 7190 RVA: 0x00082C80 File Offset: 0x00080E80
		protected override bool MatchKeyword(string keyword)
		{
			return this.current != null && keyword == this.current.ToLower();
		}

		// Token: 0x06001C17 RID: 7191 RVA: 0x00082CB0 File Offset: 0x00080EB0
		protected override bool MatchNumber()
		{
			string current = this.current;
			Match match = StyleMatcher.s_NumberRegex.Match(current);
			return match.Success;
		}

		// Token: 0x06001C18 RID: 7192 RVA: 0x00082CDC File Offset: 0x00080EDC
		protected override bool MatchInteger()
		{
			string current = this.current;
			Match match = StyleMatcher.s_IntegerRegex.Match(current);
			return match.Success;
		}

		// Token: 0x06001C19 RID: 7193 RVA: 0x00082D08 File Offset: 0x00080F08
		protected override bool MatchLength()
		{
			string current = this.current;
			Match match = StyleMatcher.s_LengthRegex.Match(current);
			bool success = match.Success;
			bool flag;
			if (success)
			{
				flag = true;
			}
			else
			{
				match = StyleMatcher.s_ZeroRegex.Match(current);
				flag = match.Success;
			}
			return flag;
		}

		// Token: 0x06001C1A RID: 7194 RVA: 0x00082D50 File Offset: 0x00080F50
		protected override bool MatchPercentage()
		{
			string current = this.current;
			Match match = StyleMatcher.s_PercentRegex.Match(current);
			bool success = match.Success;
			bool flag;
			if (success)
			{
				flag = true;
			}
			else
			{
				match = StyleMatcher.s_ZeroRegex.Match(current);
				flag = match.Success;
			}
			return flag;
		}

		// Token: 0x06001C1B RID: 7195 RVA: 0x00082D98 File Offset: 0x00080F98
		protected override bool MatchColor()
		{
			string current = this.current;
			Match match = StyleMatcher.s_HexColorRegex.Match(current);
			bool success = match.Success;
			bool flag;
			if (success)
			{
				flag = true;
			}
			else
			{
				match = StyleMatcher.s_RgbRegex.Match(current);
				bool success2 = match.Success;
				if (success2)
				{
					flag = true;
				}
				else
				{
					match = StyleMatcher.s_RgbaRegex.Match(current);
					bool success3 = match.Success;
					if (success3)
					{
						flag = true;
					}
					else
					{
						Color clear = Color.clear;
						bool flag2 = StyleSheetColor.TryGetColor(current, out clear);
						flag = flag2;
					}
				}
			}
			return flag;
		}

		// Token: 0x06001C1C RID: 7196 RVA: 0x00082E24 File Offset: 0x00081024
		protected override bool MatchResource()
		{
			string current = this.current;
			Match match = StyleMatcher.s_ResourceRegex.Match(current);
			bool flag = !match.Success;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				string text = match.Groups[1].Value.Trim();
				match = StyleMatcher.s_VarFunctionRegex.Match(text);
				flag2 = !match.Success;
			}
			return flag2;
		}

		// Token: 0x06001C1D RID: 7197 RVA: 0x00082E8C File Offset: 0x0008108C
		protected override bool MatchUrl()
		{
			string current = this.current;
			Match match = StyleMatcher.s_UrlRegex.Match(current);
			bool flag = !match.Success;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				string text = match.Groups[1].Value.Trim();
				match = StyleMatcher.s_VarFunctionRegex.Match(text);
				flag2 = !match.Success;
			}
			return flag2;
		}

		// Token: 0x06001C1E RID: 7198 RVA: 0x00082EF4 File Offset: 0x000810F4
		protected override bool MatchTime()
		{
			string current = this.current;
			Match match = StyleMatcher.s_TimeRegex.Match(current);
			return match.Success;
		}

		// Token: 0x06001C1F RID: 7199 RVA: 0x00082F20 File Offset: 0x00081120
		protected override bool MatchAngle()
		{
			string current = this.current;
			Match match = StyleMatcher.s_AngleRegex.Match(current);
			bool success = match.Success;
			bool flag;
			if (success)
			{
				flag = true;
			}
			else
			{
				match = StyleMatcher.s_ZeroRegex.Match(current);
				flag = match.Success;
			}
			return flag;
		}

		// Token: 0x06001C20 RID: 7200 RVA: 0x00082F68 File Offset: 0x00081168
		protected override bool MatchCustomIdent()
		{
			string current = this.current;
			Match match = BaseStyleMatcher.s_CustomIdentRegex.Match(current);
			return match.Success && match.Length == current.Length;
		}

		// Token: 0x04000DF2 RID: 3570
		private StylePropertyValueParser m_Parser = new StylePropertyValueParser();

		// Token: 0x04000DF3 RID: 3571
		private string[] m_PropertyParts;

		// Token: 0x04000DF4 RID: 3572
		private static readonly Regex s_NumberRegex = new Regex("^[+-]?\\d+(?:\\.\\d+)?$", 8);

		// Token: 0x04000DF5 RID: 3573
		private static readonly Regex s_IntegerRegex = new Regex("^[+-]?\\d+$", 8);

		// Token: 0x04000DF6 RID: 3574
		private static readonly Regex s_ZeroRegex = new Regex("^0(?:\\.0+)?$", 8);

		// Token: 0x04000DF7 RID: 3575
		private static readonly Regex s_LengthRegex = new Regex("^[+-]?\\d+(?:\\.\\d+)?(?:px)$", 8);

		// Token: 0x04000DF8 RID: 3576
		private static readonly Regex s_PercentRegex = new Regex("^[+-]?\\d+(?:\\.\\d+)?(?:%)$", 8);

		// Token: 0x04000DF9 RID: 3577
		private static readonly Regex s_HexColorRegex = new Regex("^#[a-fA-F0-9]{3}(?:[a-fA-F0-9]{3})?$", 8);

		// Token: 0x04000DFA RID: 3578
		private static readonly Regex s_RgbRegex = new Regex("^rgb\\(\\s*(\\d+)\\s*,\\s*(\\d+)\\s*,\\s*(\\d+)\\s*\\)$", 8);

		// Token: 0x04000DFB RID: 3579
		private static readonly Regex s_RgbaRegex = new Regex("rgba\\(\\s*(\\d+)\\s*,\\s*(\\d+)\\s*,\\s*(\\d+)\\s*,\\s*([\\d.]+)\\s*\\)$", 8);

		// Token: 0x04000DFC RID: 3580
		private static readonly Regex s_VarFunctionRegex = new Regex("^var\\(.+\\)$", 8);

		// Token: 0x04000DFD RID: 3581
		private static readonly Regex s_ResourceRegex = new Regex("^resource\\((.+)\\)$", 8);

		// Token: 0x04000DFE RID: 3582
		private static readonly Regex s_UrlRegex = new Regex("^url\\((.+)\\)$", 8);

		// Token: 0x04000DFF RID: 3583
		private static readonly Regex s_TimeRegex = new Regex("^[+-]?\\.?\\d+(?:\\.\\d+)?(?:s|ms)$", 8);

		// Token: 0x04000E00 RID: 3584
		private static readonly Regex s_AngleRegex = new Regex("^[+-]?\\d+(?:\\.\\d+)?(?:deg|grad|rad|turn)$", 8);
	}
}
