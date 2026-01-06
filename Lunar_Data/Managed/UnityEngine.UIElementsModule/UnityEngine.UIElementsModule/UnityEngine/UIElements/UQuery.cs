using System;
using System.Collections.Generic;
using UnityEngine.UIElements.StyleSheets;

namespace UnityEngine.UIElements
{
	// Token: 0x020000C5 RID: 197
	public static class UQuery
	{
		// Token: 0x020000C6 RID: 198
		internal interface IVisualPredicateWrapper
		{
			// Token: 0x06000697 RID: 1687
			bool Predicate(object e);
		}

		// Token: 0x020000C7 RID: 199
		internal class IsOfType<T> : UQuery.IVisualPredicateWrapper where T : VisualElement
		{
			// Token: 0x06000698 RID: 1688 RVA: 0x00018838 File Offset: 0x00016A38
			public bool Predicate(object e)
			{
				return e is T;
			}

			// Token: 0x04000299 RID: 665
			public static UQuery.IsOfType<T> s_Instance = new UQuery.IsOfType<T>();
		}

		// Token: 0x020000C8 RID: 200
		internal class PredicateWrapper<T> : UQuery.IVisualPredicateWrapper where T : VisualElement
		{
			// Token: 0x0600069B RID: 1691 RVA: 0x0001885F File Offset: 0x00016A5F
			public PredicateWrapper(Func<T, bool> p)
			{
				this.predicate = p;
			}

			// Token: 0x0600069C RID: 1692 RVA: 0x00018870 File Offset: 0x00016A70
			public bool Predicate(object e)
			{
				T t = e as T;
				bool flag = t != null;
				return flag && this.predicate.Invoke(t);
			}

			// Token: 0x0400029A RID: 666
			private Func<T, bool> predicate;
		}

		// Token: 0x020000C9 RID: 201
		internal abstract class UQueryMatcher : HierarchyTraversal
		{
			// Token: 0x0600069E RID: 1694 RVA: 0x000188B6 File Offset: 0x00016AB6
			public override void Traverse(VisualElement element)
			{
				base.Traverse(element);
			}

			// Token: 0x0600069F RID: 1695 RVA: 0x000188C4 File Offset: 0x00016AC4
			protected virtual bool OnRuleMatchedElement(RuleMatcher matcher, VisualElement element)
			{
				return false;
			}

			// Token: 0x060006A0 RID: 1696 RVA: 0x000020E6 File Offset: 0x000002E6
			private static void NoProcessResult(VisualElement e, MatchResultInfo i)
			{
			}

			// Token: 0x060006A1 RID: 1697 RVA: 0x000188D8 File Offset: 0x00016AD8
			public override void TraverseRecursive(VisualElement element, int depth)
			{
				int count = this.m_Matchers.Count;
				int count2 = this.m_Matchers.Count;
				for (int j = 0; j < count2; j++)
				{
					RuleMatcher ruleMatcher = this.m_Matchers[j];
					bool flag = StyleSelectorHelper.MatchRightToLeft(element, ruleMatcher.complexSelector, delegate(VisualElement e, MatchResultInfo i)
					{
						UQuery.UQueryMatcher.NoProcessResult(e, i);
					});
					if (flag)
					{
						bool flag2 = this.OnRuleMatchedElement(ruleMatcher, element);
						if (flag2)
						{
							return;
						}
					}
				}
				base.Recurse(element, depth);
				bool flag3 = this.m_Matchers.Count > count;
				if (flag3)
				{
					this.m_Matchers.RemoveRange(count, this.m_Matchers.Count - count);
					return;
				}
			}

			// Token: 0x060006A2 RID: 1698 RVA: 0x0001899C File Offset: 0x00016B9C
			public virtual void Run(VisualElement root, List<RuleMatcher> matchers)
			{
				this.m_Matchers = matchers;
				this.Traverse(root);
			}

			// Token: 0x0400029B RID: 667
			internal List<RuleMatcher> m_Matchers;
		}

		// Token: 0x020000CB RID: 203
		internal abstract class SingleQueryMatcher : UQuery.UQueryMatcher
		{
			// Token: 0x17000173 RID: 371
			// (get) Token: 0x060006A6 RID: 1702 RVA: 0x000189C4 File Offset: 0x00016BC4
			// (set) Token: 0x060006A7 RID: 1703 RVA: 0x000189CC File Offset: 0x00016BCC
			public VisualElement match { get; set; }

			// Token: 0x060006A8 RID: 1704 RVA: 0x000189D5 File Offset: 0x00016BD5
			public override void Run(VisualElement root, List<RuleMatcher> matchers)
			{
				this.match = null;
				base.Run(root, matchers);
				this.m_Matchers = null;
			}

			// Token: 0x060006A9 RID: 1705 RVA: 0x000189F0 File Offset: 0x00016BF0
			public bool IsInUse()
			{
				return this.m_Matchers != null;
			}

			// Token: 0x060006AA RID: 1706
			public abstract UQuery.SingleQueryMatcher CreateNew();
		}

		// Token: 0x020000CC RID: 204
		internal class FirstQueryMatcher : UQuery.SingleQueryMatcher
		{
			// Token: 0x060006AC RID: 1708 RVA: 0x00018A14 File Offset: 0x00016C14
			protected override bool OnRuleMatchedElement(RuleMatcher matcher, VisualElement element)
			{
				bool flag = base.match == null;
				if (flag)
				{
					base.match = element;
				}
				return true;
			}

			// Token: 0x060006AD RID: 1709 RVA: 0x00018A3C File Offset: 0x00016C3C
			public override UQuery.SingleQueryMatcher CreateNew()
			{
				return new UQuery.FirstQueryMatcher();
			}

			// Token: 0x0400029F RID: 671
			public static readonly UQuery.FirstQueryMatcher Instance = new UQuery.FirstQueryMatcher();
		}

		// Token: 0x020000CD RID: 205
		internal class LastQueryMatcher : UQuery.SingleQueryMatcher
		{
			// Token: 0x060006B0 RID: 1712 RVA: 0x00018A58 File Offset: 0x00016C58
			protected override bool OnRuleMatchedElement(RuleMatcher matcher, VisualElement element)
			{
				base.match = element;
				return false;
			}

			// Token: 0x060006B1 RID: 1713 RVA: 0x00018A73 File Offset: 0x00016C73
			public override UQuery.SingleQueryMatcher CreateNew()
			{
				return new UQuery.LastQueryMatcher();
			}

			// Token: 0x040002A0 RID: 672
			public static readonly UQuery.LastQueryMatcher Instance = new UQuery.LastQueryMatcher();
		}

		// Token: 0x020000CE RID: 206
		internal class IndexQueryMatcher : UQuery.SingleQueryMatcher
		{
			// Token: 0x17000174 RID: 372
			// (get) Token: 0x060006B4 RID: 1716 RVA: 0x00018A88 File Offset: 0x00016C88
			// (set) Token: 0x060006B5 RID: 1717 RVA: 0x00018AA0 File Offset: 0x00016CA0
			public int matchIndex
			{
				get
				{
					return this._matchIndex;
				}
				set
				{
					this.matchCount = -1;
					this._matchIndex = value;
				}
			}

			// Token: 0x060006B6 RID: 1718 RVA: 0x00018AB1 File Offset: 0x00016CB1
			public override void Run(VisualElement root, List<RuleMatcher> matchers)
			{
				this.matchCount = -1;
				base.Run(root, matchers);
			}

			// Token: 0x060006B7 RID: 1719 RVA: 0x00018AC4 File Offset: 0x00016CC4
			protected override bool OnRuleMatchedElement(RuleMatcher matcher, VisualElement element)
			{
				this.matchCount++;
				bool flag = this.matchCount == this._matchIndex;
				if (flag)
				{
					base.match = element;
				}
				return this.matchCount >= this._matchIndex;
			}

			// Token: 0x060006B8 RID: 1720 RVA: 0x00018B11 File Offset: 0x00016D11
			public override UQuery.SingleQueryMatcher CreateNew()
			{
				return new UQuery.IndexQueryMatcher();
			}

			// Token: 0x040002A1 RID: 673
			public static readonly UQuery.IndexQueryMatcher Instance = new UQuery.IndexQueryMatcher();

			// Token: 0x040002A2 RID: 674
			private int matchCount = -1;

			// Token: 0x040002A3 RID: 675
			private int _matchIndex;
		}
	}
}
