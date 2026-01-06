using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine.UIElements.StyleSheets.Syntax;

namespace UnityEngine.UIElements.StyleSheets
{
	// Token: 0x0200036D RID: 877
	internal abstract class BaseStyleMatcher
	{
		// Token: 0x06001BEA RID: 7146
		protected abstract bool MatchKeyword(string keyword);

		// Token: 0x06001BEB RID: 7147
		protected abstract bool MatchNumber();

		// Token: 0x06001BEC RID: 7148
		protected abstract bool MatchInteger();

		// Token: 0x06001BED RID: 7149
		protected abstract bool MatchLength();

		// Token: 0x06001BEE RID: 7150
		protected abstract bool MatchPercentage();

		// Token: 0x06001BEF RID: 7151
		protected abstract bool MatchColor();

		// Token: 0x06001BF0 RID: 7152
		protected abstract bool MatchResource();

		// Token: 0x06001BF1 RID: 7153
		protected abstract bool MatchUrl();

		// Token: 0x06001BF2 RID: 7154
		protected abstract bool MatchTime();

		// Token: 0x06001BF3 RID: 7155
		protected abstract bool MatchAngle();

		// Token: 0x06001BF4 RID: 7156
		protected abstract bool MatchCustomIdent();

		// Token: 0x170006B4 RID: 1716
		// (get) Token: 0x06001BF5 RID: 7157
		public abstract int valueCount { get; }

		// Token: 0x170006B5 RID: 1717
		// (get) Token: 0x06001BF6 RID: 7158
		public abstract bool isCurrentVariable { get; }

		// Token: 0x170006B6 RID: 1718
		// (get) Token: 0x06001BF7 RID: 7159
		public abstract bool isCurrentComma { get; }

		// Token: 0x170006B7 RID: 1719
		// (get) Token: 0x06001BF8 RID: 7160 RVA: 0x000823F3 File Offset: 0x000805F3
		public bool hasCurrent
		{
			get
			{
				return this.m_CurrentContext.valueIndex < this.valueCount;
			}
		}

		// Token: 0x170006B8 RID: 1720
		// (get) Token: 0x06001BF9 RID: 7161 RVA: 0x00082408 File Offset: 0x00080608
		// (set) Token: 0x06001BFA RID: 7162 RVA: 0x00082415 File Offset: 0x00080615
		public int currentIndex
		{
			get
			{
				return this.m_CurrentContext.valueIndex;
			}
			set
			{
				this.m_CurrentContext.valueIndex = value;
			}
		}

		// Token: 0x170006B9 RID: 1721
		// (get) Token: 0x06001BFB RID: 7163 RVA: 0x00082423 File Offset: 0x00080623
		// (set) Token: 0x06001BFC RID: 7164 RVA: 0x00082430 File Offset: 0x00080630
		public int matchedVariableCount
		{
			get
			{
				return this.m_CurrentContext.matchedVariableCount;
			}
			set
			{
				this.m_CurrentContext.matchedVariableCount = value;
			}
		}

		// Token: 0x06001BFD RID: 7165 RVA: 0x0008243E File Offset: 0x0008063E
		protected void Initialize()
		{
			this.m_CurrentContext = default(BaseStyleMatcher.MatchContext);
			this.m_ContextStack.Clear();
		}

		// Token: 0x06001BFE RID: 7166 RVA: 0x0008245C File Offset: 0x0008065C
		public void MoveNext()
		{
			bool flag = this.currentIndex + 1 <= this.valueCount;
			if (flag)
			{
				int currentIndex = this.currentIndex;
				this.currentIndex = currentIndex + 1;
			}
		}

		// Token: 0x06001BFF RID: 7167 RVA: 0x00082494 File Offset: 0x00080694
		public void SaveContext()
		{
			this.m_ContextStack.Push(this.m_CurrentContext);
		}

		// Token: 0x06001C00 RID: 7168 RVA: 0x000824A9 File Offset: 0x000806A9
		public void RestoreContext()
		{
			this.m_CurrentContext = this.m_ContextStack.Pop();
		}

		// Token: 0x06001C01 RID: 7169 RVA: 0x000824BD File Offset: 0x000806BD
		public void DropContext()
		{
			this.m_ContextStack.Pop();
		}

		// Token: 0x06001C02 RID: 7170 RVA: 0x000824CC File Offset: 0x000806CC
		protected bool Match(Expression exp)
		{
			bool flag = exp.multiplier.type == ExpressionMultiplierType.None;
			bool flag2;
			if (flag)
			{
				flag2 = this.MatchExpression(exp);
			}
			else
			{
				Debug.Assert(exp.multiplier.type != ExpressionMultiplierType.GroupAtLeastOne, "'!' multiplier in syntax expression is not supported");
				flag2 = this.MatchExpressionWithMultiplier(exp);
			}
			return flag2;
		}

		// Token: 0x06001C03 RID: 7171 RVA: 0x00082528 File Offset: 0x00080728
		private bool MatchExpression(Expression exp)
		{
			bool flag = false;
			bool flag2 = exp.type == ExpressionType.Combinator;
			if (flag2)
			{
				flag = this.MatchCombinator(exp);
			}
			else
			{
				bool isCurrentVariable = this.isCurrentVariable;
				if (isCurrentVariable)
				{
					flag = true;
					int matchedVariableCount = this.matchedVariableCount;
					this.matchedVariableCount = matchedVariableCount + 1;
				}
				else
				{
					bool flag3 = exp.type == ExpressionType.Data;
					if (flag3)
					{
						flag = this.MatchDataType(exp);
					}
					else
					{
						bool flag4 = exp.type == ExpressionType.Keyword;
						if (flag4)
						{
							flag = this.MatchKeyword(exp.keyword);
						}
					}
				}
				bool flag5 = flag;
				if (flag5)
				{
					this.MoveNext();
				}
			}
			bool flag6 = !flag && !this.hasCurrent && this.matchedVariableCount > 0;
			if (flag6)
			{
				flag = true;
			}
			return flag;
		}

		// Token: 0x06001C04 RID: 7172 RVA: 0x000825E4 File Offset: 0x000807E4
		private bool MatchExpressionWithMultiplier(Expression exp)
		{
			bool flag = exp.multiplier.type == ExpressionMultiplierType.OneOrMoreComma;
			bool flag2 = true;
			int min = exp.multiplier.min;
			int max = exp.multiplier.max;
			int num = 0;
			int num2 = 0;
			while (flag2 && this.hasCurrent && num2 < max)
			{
				flag2 = this.MatchExpression(exp);
				bool flag3 = flag2;
				if (flag3)
				{
					num++;
					bool flag4 = flag;
					if (flag4)
					{
						bool flag5 = !this.isCurrentComma;
						if (flag5)
						{
							break;
						}
						this.MoveNext();
					}
				}
				num2++;
			}
			flag2 = num >= min && num <= max;
			bool flag6 = !flag2 && num <= max && this.matchedVariableCount > 0;
			if (flag6)
			{
				flag2 = true;
			}
			return flag2;
		}

		// Token: 0x06001C05 RID: 7173 RVA: 0x000826B0 File Offset: 0x000808B0
		private bool MatchGroup(Expression exp)
		{
			Debug.Assert(exp.subExpressions.Length == 1, "Group has invalid number of sub expressions");
			Expression expression = exp.subExpressions[0];
			return this.Match(expression);
		}

		// Token: 0x06001C06 RID: 7174 RVA: 0x000826E8 File Offset: 0x000808E8
		private bool MatchCombinator(Expression exp)
		{
			this.SaveContext();
			bool flag = false;
			switch (exp.combinator)
			{
			case ExpressionCombinator.Or:
				flag = this.MatchOr(exp);
				break;
			case ExpressionCombinator.OrOr:
				flag = this.MatchOrOr(exp);
				break;
			case ExpressionCombinator.AndAnd:
				flag = this.MatchAndAnd(exp);
				break;
			case ExpressionCombinator.Juxtaposition:
				flag = this.MatchJuxtaposition(exp);
				break;
			case ExpressionCombinator.Group:
				flag = this.MatchGroup(exp);
				break;
			}
			bool flag2 = flag;
			if (flag2)
			{
				this.DropContext();
			}
			else
			{
				this.RestoreContext();
			}
			return flag;
		}

		// Token: 0x06001C07 RID: 7175 RVA: 0x00082778 File Offset: 0x00080978
		private bool MatchOr(Expression exp)
		{
			BaseStyleMatcher.MatchContext matchContext = default(BaseStyleMatcher.MatchContext);
			int num = 0;
			for (int i = 0; i < exp.subExpressions.Length; i++)
			{
				this.SaveContext();
				int currentIndex = this.currentIndex;
				bool flag = this.Match(exp.subExpressions[i]);
				int num2 = this.currentIndex - currentIndex;
				bool flag2 = flag && num2 > num;
				if (flag2)
				{
					num = num2;
					matchContext = this.m_CurrentContext;
				}
				this.RestoreContext();
			}
			bool flag3 = num > 0;
			bool flag4;
			if (flag3)
			{
				this.m_CurrentContext = matchContext;
				flag4 = true;
			}
			else
			{
				flag4 = false;
			}
			return flag4;
		}

		// Token: 0x06001C08 RID: 7176 RVA: 0x00082818 File Offset: 0x00080A18
		private bool MatchOrOr(Expression exp)
		{
			int num = this.MatchMany(exp);
			return num > 0;
		}

		// Token: 0x06001C09 RID: 7177 RVA: 0x00082838 File Offset: 0x00080A38
		private bool MatchAndAnd(Expression exp)
		{
			int num = this.MatchMany(exp);
			int num2 = exp.subExpressions.Length;
			return num == num2;
		}

		// Token: 0x06001C0A RID: 7178 RVA: 0x00082860 File Offset: 0x00080A60
		private unsafe int MatchMany(Expression exp)
		{
			BaseStyleMatcher.MatchContext matchContext = default(BaseStyleMatcher.MatchContext);
			int num = 0;
			int num2 = -1;
			int num3 = exp.subExpressions.Length;
			int* ptr;
			checked
			{
				ptr = stackalloc int[unchecked((UIntPtr)num3) * 4];
			}
			do
			{
				this.SaveContext();
				num2++;
				for (int i = 0; i < num3; i++)
				{
					int num4 = ((num2 > 0) ? ((num2 + i) % num3) : i);
					ptr[i] = num4;
				}
				int num5 = this.MatchManyByOrder(exp, ptr);
				bool flag = num5 > num;
				if (flag)
				{
					num = num5;
					matchContext = this.m_CurrentContext;
				}
				this.RestoreContext();
			}
			while (num < num3 && num2 < num3);
			bool flag2 = num > 0;
			if (flag2)
			{
				this.m_CurrentContext = matchContext;
			}
			return num;
		}

		// Token: 0x06001C0B RID: 7179 RVA: 0x00082920 File Offset: 0x00080B20
		private unsafe int MatchManyByOrder(Expression exp, int* matchOrder)
		{
			int num = exp.subExpressions.Length;
			int* ptr;
			int num2;
			int num3;
			int num4;
			checked
			{
				ptr = stackalloc int[unchecked((UIntPtr)num) * 4];
				num2 = 0;
				num3 = 0;
				num4 = 0;
			}
			while (num4 < num && num2 + num3 < num)
			{
				int num5 = matchOrder[num4];
				bool flag = false;
				for (int i = 0; i < num2; i++)
				{
					bool flag2 = ptr[i] == num5;
					if (flag2)
					{
						flag = true;
						break;
					}
				}
				bool flag3 = false;
				bool flag4 = !flag;
				if (flag4)
				{
					flag3 = this.Match(exp.subExpressions[num5]);
				}
				bool flag5 = flag3;
				if (flag5)
				{
					bool flag6 = num3 == this.matchedVariableCount;
					if (flag6)
					{
						ptr[num2] = num5;
						num2++;
					}
					else
					{
						num3 = this.matchedVariableCount;
					}
					num4 = 0;
				}
				else
				{
					num4++;
				}
			}
			return num2 + num3;
		}

		// Token: 0x06001C0C RID: 7180 RVA: 0x00082A0C File Offset: 0x00080C0C
		private bool MatchJuxtaposition(Expression exp)
		{
			bool flag = true;
			int num = 0;
			while (flag && num < exp.subExpressions.Length)
			{
				flag = this.Match(exp.subExpressions[num]);
				num++;
			}
			return flag;
		}

		// Token: 0x06001C0D RID: 7181 RVA: 0x00082A50 File Offset: 0x00080C50
		private bool MatchDataType(Expression exp)
		{
			bool flag = false;
			bool hasCurrent = this.hasCurrent;
			if (hasCurrent)
			{
				switch (exp.dataType)
				{
				case DataType.Number:
					flag = this.MatchNumber();
					break;
				case DataType.Integer:
					flag = this.MatchInteger();
					break;
				case DataType.Length:
					flag = this.MatchLength();
					break;
				case DataType.Percentage:
					flag = this.MatchPercentage();
					break;
				case DataType.Color:
					flag = this.MatchColor();
					break;
				case DataType.Resource:
					flag = this.MatchResource();
					break;
				case DataType.Url:
					flag = this.MatchUrl();
					break;
				case DataType.Time:
					flag = this.MatchTime();
					break;
				case DataType.Angle:
					flag = this.MatchAngle();
					break;
				case DataType.CustomIdent:
					flag = this.MatchCustomIdent();
					break;
				}
			}
			return flag;
		}

		// Token: 0x04000DED RID: 3565
		protected static readonly Regex s_CustomIdentRegex = new Regex("^-?[_a-z][_a-z0-9-]*", 9);

		// Token: 0x04000DEE RID: 3566
		private Stack<BaseStyleMatcher.MatchContext> m_ContextStack = new Stack<BaseStyleMatcher.MatchContext>();

		// Token: 0x04000DEF RID: 3567
		private BaseStyleMatcher.MatchContext m_CurrentContext;

		// Token: 0x0200036E RID: 878
		private struct MatchContext
		{
			// Token: 0x04000DF0 RID: 3568
			public int valueIndex;

			// Token: 0x04000DF1 RID: 3569
			public int matchedVariableCount;
		}
	}
}
