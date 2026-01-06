using System;
using System.Collections;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x020000CF RID: 207
	public struct UQueryState<T> : IEnumerable<T>, IEnumerable, IEquatable<UQueryState<T>> where T : VisualElement
	{
		// Token: 0x060006BB RID: 1723 RVA: 0x00018B34 File Offset: 0x00016D34
		internal UQueryState(VisualElement element, List<RuleMatcher> matchers)
		{
			this.m_Element = element;
			this.m_Matchers = matchers;
		}

		// Token: 0x060006BC RID: 1724 RVA: 0x00018B48 File Offset: 0x00016D48
		public UQueryState<T> RebuildOn(VisualElement element)
		{
			return new UQueryState<T>(element, this.m_Matchers);
		}

		// Token: 0x060006BD RID: 1725 RVA: 0x00018B68 File Offset: 0x00016D68
		private T Single(UQuery.SingleQueryMatcher matcher)
		{
			bool flag = matcher.IsInUse();
			if (flag)
			{
				matcher = matcher.CreateNew();
			}
			matcher.Run(this.m_Element, this.m_Matchers);
			T t = matcher.match as T;
			matcher.match = null;
			return t;
		}

		// Token: 0x060006BE RID: 1726 RVA: 0x00018BBB File Offset: 0x00016DBB
		public T First()
		{
			return this.Single(UQuery.FirstQueryMatcher.Instance);
		}

		// Token: 0x060006BF RID: 1727 RVA: 0x00018BC8 File Offset: 0x00016DC8
		public T Last()
		{
			return this.Single(UQuery.LastQueryMatcher.Instance);
		}

		// Token: 0x060006C0 RID: 1728 RVA: 0x00018BD5 File Offset: 0x00016DD5
		public void ToList(List<T> results)
		{
			UQueryState<T>.s_List.matches = results;
			UQueryState<T>.s_List.Run(this.m_Element, this.m_Matchers);
			UQueryState<T>.s_List.Reset();
		}

		// Token: 0x060006C1 RID: 1729 RVA: 0x00018C08 File Offset: 0x00016E08
		public List<T> ToList()
		{
			List<T> list = new List<T>();
			this.ToList(list);
			return list;
		}

		// Token: 0x060006C2 RID: 1730 RVA: 0x00018C2C File Offset: 0x00016E2C
		public T AtIndex(int index)
		{
			UQuery.IndexQueryMatcher instance = UQuery.IndexQueryMatcher.Instance;
			instance.matchIndex = index;
			return this.Single(instance);
		}

		// Token: 0x060006C3 RID: 1731 RVA: 0x00018C54 File Offset: 0x00016E54
		public void ForEach(Action<T> funcCall)
		{
			UQueryState<T>.ActionQueryMatcher actionQueryMatcher = UQueryState<T>.s_Action;
			bool flag = actionQueryMatcher.callBack != null;
			if (flag)
			{
				actionQueryMatcher = new UQueryState<T>.ActionQueryMatcher();
			}
			try
			{
				actionQueryMatcher.callBack = funcCall;
				actionQueryMatcher.Run(this.m_Element, this.m_Matchers);
			}
			finally
			{
				actionQueryMatcher.callBack = null;
			}
		}

		// Token: 0x060006C4 RID: 1732 RVA: 0x00018CB8 File Offset: 0x00016EB8
		public void ForEach<T2>(List<T2> result, Func<T, T2> funcCall)
		{
			UQueryState<T>.DelegateQueryMatcher<T2> delegateQueryMatcher = UQueryState<T>.DelegateQueryMatcher<T2>.s_Instance;
			bool flag = delegateQueryMatcher.callBack != null;
			if (flag)
			{
				delegateQueryMatcher = new UQueryState<T>.DelegateQueryMatcher<T2>();
			}
			try
			{
				delegateQueryMatcher.callBack = funcCall;
				delegateQueryMatcher.result = result;
				delegateQueryMatcher.Run(this.m_Element, this.m_Matchers);
			}
			finally
			{
				delegateQueryMatcher.callBack = null;
				delegateQueryMatcher.result = null;
			}
		}

		// Token: 0x060006C5 RID: 1733 RVA: 0x00018D2C File Offset: 0x00016F2C
		public List<T2> ForEach<T2>(Func<T, T2> funcCall)
		{
			List<T2> list = new List<T2>();
			this.ForEach<T2>(list, funcCall);
			return list;
		}

		// Token: 0x060006C6 RID: 1734 RVA: 0x00018D4E File Offset: 0x00016F4E
		public UQueryState<T>.Enumerator GetEnumerator()
		{
			return new UQueryState<T>.Enumerator(this);
		}

		// Token: 0x060006C7 RID: 1735 RVA: 0x00018D5B File Offset: 0x00016F5B
		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x060006C8 RID: 1736 RVA: 0x00018D5B File Offset: 0x00016F5B
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x060006C9 RID: 1737 RVA: 0x00018D68 File Offset: 0x00016F68
		public bool Equals(UQueryState<T> other)
		{
			return this.m_Element == other.m_Element && EqualityComparer<List<RuleMatcher>>.Default.Equals(this.m_Matchers, other.m_Matchers);
		}

		// Token: 0x060006CA RID: 1738 RVA: 0x00018DA4 File Offset: 0x00016FA4
		public override bool Equals(object obj)
		{
			bool flag = !(obj is UQueryState<T>);
			return !flag && this.Equals((UQueryState<T>)obj);
		}

		// Token: 0x060006CB RID: 1739 RVA: 0x00018DD8 File Offset: 0x00016FD8
		public override int GetHashCode()
		{
			int num = 488160421;
			num = num * -1521134295 + EqualityComparer<VisualElement>.Default.GetHashCode(this.m_Element);
			return num * -1521134295 + EqualityComparer<List<RuleMatcher>>.Default.GetHashCode(this.m_Matchers);
		}

		// Token: 0x060006CC RID: 1740 RVA: 0x00018E24 File Offset: 0x00017024
		public static bool operator ==(UQueryState<T> state1, UQueryState<T> state2)
		{
			return state1.Equals(state2);
		}

		// Token: 0x060006CD RID: 1741 RVA: 0x00018E40 File Offset: 0x00017040
		public static bool operator !=(UQueryState<T> state1, UQueryState<T> state2)
		{
			return !(state1 == state2);
		}

		// Token: 0x040002A4 RID: 676
		private static UQueryState<T>.ActionQueryMatcher s_Action = new UQueryState<T>.ActionQueryMatcher();

		// Token: 0x040002A5 RID: 677
		private readonly VisualElement m_Element;

		// Token: 0x040002A6 RID: 678
		internal readonly List<RuleMatcher> m_Matchers;

		// Token: 0x040002A7 RID: 679
		private static readonly UQueryState<T>.ListQueryMatcher<T> s_List = new UQueryState<T>.ListQueryMatcher<T>();

		// Token: 0x040002A8 RID: 680
		private static readonly UQueryState<T>.ListQueryMatcher<VisualElement> s_EnumerationList = new UQueryState<T>.ListQueryMatcher<VisualElement>();

		// Token: 0x020000D0 RID: 208
		private class ListQueryMatcher<TElement> : UQuery.UQueryMatcher where TElement : VisualElement
		{
			// Token: 0x17000175 RID: 373
			// (get) Token: 0x060006CF RID: 1743 RVA: 0x00018E7C File Offset: 0x0001707C
			// (set) Token: 0x060006D0 RID: 1744 RVA: 0x00018E84 File Offset: 0x00017084
			public List<TElement> matches { get; set; }

			// Token: 0x060006D1 RID: 1745 RVA: 0x00018E90 File Offset: 0x00017090
			protected override bool OnRuleMatchedElement(RuleMatcher matcher, VisualElement element)
			{
				this.matches.Add(element as TElement);
				return false;
			}

			// Token: 0x060006D2 RID: 1746 RVA: 0x00018EBA File Offset: 0x000170BA
			public void Reset()
			{
				this.matches = null;
			}
		}

		// Token: 0x020000D1 RID: 209
		private class ActionQueryMatcher : UQuery.UQueryMatcher
		{
			// Token: 0x17000176 RID: 374
			// (get) Token: 0x060006D4 RID: 1748 RVA: 0x00018EC5 File Offset: 0x000170C5
			// (set) Token: 0x060006D5 RID: 1749 RVA: 0x00018ECD File Offset: 0x000170CD
			internal Action<T> callBack { get; set; }

			// Token: 0x060006D6 RID: 1750 RVA: 0x00018ED8 File Offset: 0x000170D8
			protected override bool OnRuleMatchedElement(RuleMatcher matcher, VisualElement element)
			{
				T t = element as T;
				bool flag = t != null;
				if (flag)
				{
					this.callBack.Invoke(t);
				}
				return false;
			}
		}

		// Token: 0x020000D2 RID: 210
		private class DelegateQueryMatcher<TReturnType> : UQuery.UQueryMatcher
		{
			// Token: 0x17000177 RID: 375
			// (get) Token: 0x060006D8 RID: 1752 RVA: 0x00018F13 File Offset: 0x00017113
			// (set) Token: 0x060006D9 RID: 1753 RVA: 0x00018F1B File Offset: 0x0001711B
			public Func<T, TReturnType> callBack { get; set; }

			// Token: 0x17000178 RID: 376
			// (get) Token: 0x060006DA RID: 1754 RVA: 0x00018F24 File Offset: 0x00017124
			// (set) Token: 0x060006DB RID: 1755 RVA: 0x00018F2C File Offset: 0x0001712C
			public List<TReturnType> result { get; set; }

			// Token: 0x060006DC RID: 1756 RVA: 0x00018F38 File Offset: 0x00017138
			protected override bool OnRuleMatchedElement(RuleMatcher matcher, VisualElement element)
			{
				T t = element as T;
				bool flag = t != null;
				if (flag)
				{
					this.result.Add(this.callBack.Invoke(t));
				}
				return false;
			}

			// Token: 0x040002AD RID: 685
			public static UQueryState<T>.DelegateQueryMatcher<TReturnType> s_Instance = new UQueryState<T>.DelegateQueryMatcher<TReturnType>();
		}

		// Token: 0x020000D3 RID: 211
		public struct Enumerator : IEnumerator<T>, IEnumerator, IDisposable
		{
			// Token: 0x060006DF RID: 1759 RVA: 0x00018F8C File Offset: 0x0001718C
			internal Enumerator(UQueryState<T> queryState)
			{
				this.iterationList = VisualElementListPool.Get(0);
				UQueryState<T>.s_EnumerationList.matches = this.iterationList;
				UQueryState<T>.s_EnumerationList.Run(queryState.m_Element, queryState.m_Matchers);
				UQueryState<T>.s_EnumerationList.Reset();
				this.currentIndex = -1;
			}

			// Token: 0x17000179 RID: 377
			// (get) Token: 0x060006E0 RID: 1760 RVA: 0x00018FE0 File Offset: 0x000171E0
			public T Current
			{
				get
				{
					return (T)((object)this.iterationList[this.currentIndex]);
				}
			}

			// Token: 0x1700017A RID: 378
			// (get) Token: 0x060006E1 RID: 1761 RVA: 0x00018FF8 File Offset: 0x000171F8
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x060006E2 RID: 1762 RVA: 0x00019008 File Offset: 0x00017208
			public bool MoveNext()
			{
				int num = this.currentIndex + 1;
				this.currentIndex = num;
				return num < this.iterationList.Count;
			}

			// Token: 0x060006E3 RID: 1763 RVA: 0x00019038 File Offset: 0x00017238
			public void Reset()
			{
				this.currentIndex = -1;
			}

			// Token: 0x060006E4 RID: 1764 RVA: 0x00019042 File Offset: 0x00017242
			public void Dispose()
			{
				VisualElementListPool.Release(this.iterationList);
				this.iterationList = null;
			}

			// Token: 0x040002AE RID: 686
			private List<VisualElement> iterationList;

			// Token: 0x040002AF RID: 687
			private int currentIndex;
		}
	}
}
