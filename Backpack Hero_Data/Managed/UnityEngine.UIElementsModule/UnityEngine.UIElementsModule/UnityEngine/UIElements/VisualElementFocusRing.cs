using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x020000E9 RID: 233
	public class VisualElementFocusRing : IFocusRing
	{
		// Token: 0x0600074F RID: 1871 RVA: 0x0001A67E File Offset: 0x0001887E
		public VisualElementFocusRing(VisualElement root, VisualElementFocusRing.DefaultFocusOrder dfo = VisualElementFocusRing.DefaultFocusOrder.ChildOrder)
		{
			this.defaultFocusOrder = dfo;
			this.root = root;
			this.m_FocusRing = new List<VisualElementFocusRing.FocusRingRecord>();
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x06000750 RID: 1872 RVA: 0x0001A6A2 File Offset: 0x000188A2
		private FocusController focusController
		{
			get
			{
				return this.root.focusController;
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x06000751 RID: 1873 RVA: 0x0001A6AF File Offset: 0x000188AF
		// (set) Token: 0x06000752 RID: 1874 RVA: 0x0001A6B7 File Offset: 0x000188B7
		public VisualElementFocusRing.DefaultFocusOrder defaultFocusOrder { get; set; }

		// Token: 0x06000753 RID: 1875 RVA: 0x0001A6C0 File Offset: 0x000188C0
		private int FocusRingAutoIndexSort(VisualElementFocusRing.FocusRingRecord a, VisualElementFocusRing.FocusRingRecord b)
		{
			int num;
			switch (this.defaultFocusOrder)
			{
			default:
				num = Comparer<int>.Default.Compare(a.m_AutoIndex, b.m_AutoIndex);
				break;
			case VisualElementFocusRing.DefaultFocusOrder.PositionXY:
			{
				VisualElement visualElement = a.m_Focusable as VisualElement;
				VisualElement visualElement2 = b.m_Focusable as VisualElement;
				bool flag = visualElement != null && visualElement2 != null;
				if (flag)
				{
					bool flag2 = visualElement.layout.position.x < visualElement2.layout.position.x;
					if (flag2)
					{
						num = -1;
						break;
					}
					bool flag3 = visualElement.layout.position.x > visualElement2.layout.position.x;
					if (flag3)
					{
						num = 1;
						break;
					}
					bool flag4 = visualElement.layout.position.y < visualElement2.layout.position.y;
					if (flag4)
					{
						num = -1;
						break;
					}
					bool flag5 = visualElement.layout.position.y > visualElement2.layout.position.y;
					if (flag5)
					{
						num = 1;
						break;
					}
				}
				num = Comparer<int>.Default.Compare(a.m_AutoIndex, b.m_AutoIndex);
				break;
			}
			case VisualElementFocusRing.DefaultFocusOrder.PositionYX:
			{
				VisualElement visualElement3 = a.m_Focusable as VisualElement;
				VisualElement visualElement4 = b.m_Focusable as VisualElement;
				bool flag6 = visualElement3 != null && visualElement4 != null;
				if (flag6)
				{
					bool flag7 = visualElement3.layout.position.y < visualElement4.layout.position.y;
					if (flag7)
					{
						num = -1;
						break;
					}
					bool flag8 = visualElement3.layout.position.y > visualElement4.layout.position.y;
					if (flag8)
					{
						num = 1;
						break;
					}
					bool flag9 = visualElement3.layout.position.x < visualElement4.layout.position.x;
					if (flag9)
					{
						num = -1;
						break;
					}
					bool flag10 = visualElement3.layout.position.x > visualElement4.layout.position.x;
					if (flag10)
					{
						num = 1;
						break;
					}
				}
				num = Comparer<int>.Default.Compare(a.m_AutoIndex, b.m_AutoIndex);
				break;
			}
			}
			return num;
		}

		// Token: 0x06000754 RID: 1876 RVA: 0x0001A96C File Offset: 0x00018B6C
		private int FocusRingSort(VisualElementFocusRing.FocusRingRecord a, VisualElementFocusRing.FocusRingRecord b)
		{
			bool flag = a.m_Focusable.tabIndex == 0 && b.m_Focusable.tabIndex == 0;
			int num;
			if (flag)
			{
				num = this.FocusRingAutoIndexSort(a, b);
			}
			else
			{
				bool flag2 = a.m_Focusable.tabIndex == 0;
				if (flag2)
				{
					num = 1;
				}
				else
				{
					bool flag3 = b.m_Focusable.tabIndex == 0;
					if (flag3)
					{
						num = -1;
					}
					else
					{
						int num2 = Comparer<int>.Default.Compare(a.m_Focusable.tabIndex, b.m_Focusable.tabIndex);
						bool flag4 = num2 == 0;
						if (flag4)
						{
							num2 = this.FocusRingAutoIndexSort(a, b);
						}
						num = num2;
					}
				}
			}
			return num;
		}

		// Token: 0x06000755 RID: 1877 RVA: 0x0001AA18 File Offset: 0x00018C18
		private void DoUpdate()
		{
			this.m_FocusRing.Clear();
			bool flag = this.root != null;
			if (flag)
			{
				List<VisualElementFocusRing.FocusRingRecord> list = new List<VisualElementFocusRing.FocusRingRecord>();
				int num = 0;
				this.BuildRingForScopeRecursive(this.root, ref num, list);
				this.SortAndFlattenScopeLists(list);
			}
		}

		// Token: 0x06000756 RID: 1878 RVA: 0x0001AA64 File Offset: 0x00018C64
		private void BuildRingForScopeRecursive(VisualElement ve, ref int scopeIndex, List<VisualElementFocusRing.FocusRingRecord> scopeList)
		{
			int childCount = ve.hierarchy.childCount;
			for (int i = 0; i < childCount; i++)
			{
				VisualElement visualElement = ve.hierarchy[i];
				bool flag = visualElement.parent != null && visualElement == visualElement.parent.contentContainer;
				bool flag2 = visualElement.isCompositeRoot || flag;
				if (flag2)
				{
					VisualElementFocusRing.FocusRingRecord focusRingRecord = new VisualElementFocusRing.FocusRingRecord();
					int num = scopeIndex;
					scopeIndex = num + 1;
					focusRingRecord.m_AutoIndex = num;
					focusRingRecord.m_Focusable = visualElement;
					focusRingRecord.m_IsSlot = flag;
					focusRingRecord.m_ScopeNavigationOrder = new List<VisualElementFocusRing.FocusRingRecord>();
					VisualElementFocusRing.FocusRingRecord focusRingRecord2 = focusRingRecord;
					scopeList.Add(focusRingRecord2);
					int num2 = 0;
					this.BuildRingForScopeRecursive(visualElement, ref num2, focusRingRecord2.m_ScopeNavigationOrder);
				}
				else
				{
					bool flag3 = visualElement.canGrabFocus && visualElement.isHierarchyDisplayed && visualElement.tabIndex >= 0;
					if (flag3)
					{
						VisualElementFocusRing.FocusRingRecord focusRingRecord3 = new VisualElementFocusRing.FocusRingRecord();
						int num = scopeIndex;
						scopeIndex = num + 1;
						focusRingRecord3.m_AutoIndex = num;
						focusRingRecord3.m_Focusable = visualElement;
						focusRingRecord3.m_IsSlot = false;
						focusRingRecord3.m_ScopeNavigationOrder = null;
						scopeList.Add(focusRingRecord3);
					}
					this.BuildRingForScopeRecursive(visualElement, ref scopeIndex, scopeList);
				}
			}
		}

		// Token: 0x06000757 RID: 1879 RVA: 0x0001AB90 File Offset: 0x00018D90
		private void SortAndFlattenScopeLists(List<VisualElementFocusRing.FocusRingRecord> rootScopeList)
		{
			bool flag = rootScopeList != null;
			if (flag)
			{
				rootScopeList.Sort(new Comparison<VisualElementFocusRing.FocusRingRecord>(this.FocusRingSort));
				foreach (VisualElementFocusRing.FocusRingRecord focusRingRecord in rootScopeList)
				{
					bool flag2 = focusRingRecord.m_Focusable.canGrabFocus && focusRingRecord.m_Focusable.tabIndex >= 0;
					if (flag2)
					{
						bool flag3 = !focusRingRecord.m_Focusable.excludeFromFocusRing;
						if (flag3)
						{
							this.m_FocusRing.Add(focusRingRecord);
						}
						this.SortAndFlattenScopeLists(focusRingRecord.m_ScopeNavigationOrder);
					}
					else
					{
						bool isSlot = focusRingRecord.m_IsSlot;
						if (isSlot)
						{
							this.SortAndFlattenScopeLists(focusRingRecord.m_ScopeNavigationOrder);
						}
					}
					focusRingRecord.m_ScopeNavigationOrder = null;
				}
			}
		}

		// Token: 0x06000758 RID: 1880 RVA: 0x0001AC80 File Offset: 0x00018E80
		private int GetFocusableInternalIndex(Focusable f)
		{
			bool flag = f != null;
			if (flag)
			{
				for (int i = 0; i < this.m_FocusRing.Count; i++)
				{
					bool flag2 = f == this.m_FocusRing[i].m_Focusable;
					if (flag2)
					{
						return i;
					}
				}
			}
			return -1;
		}

		// Token: 0x06000759 RID: 1881 RVA: 0x0001ACD8 File Offset: 0x00018ED8
		public FocusChangeDirection GetFocusChangeDirection(Focusable currentFocusable, EventBase e)
		{
			bool flag = e == null;
			if (flag)
			{
				throw new ArgumentNullException("e");
			}
			bool flag2 = e.eventTypeId == EventBase<PointerDownEvent>.TypeId();
			if (flag2)
			{
				Focusable focusable;
				bool focusableParentForPointerEvent = this.focusController.GetFocusableParentForPointerEvent(e.target as Focusable, out focusable);
				if (focusableParentForPointerEvent)
				{
					return VisualElementFocusChangeTarget.GetPooled(focusable);
				}
			}
			bool flag3 = currentFocusable is IMGUIContainer && e.imguiEvent != null;
			FocusChangeDirection focusChangeDirection;
			if (flag3)
			{
				focusChangeDirection = FocusChangeDirection.none;
			}
			else
			{
				focusChangeDirection = VisualElementFocusRing.GetKeyDownFocusChangeDirection(e);
			}
			return focusChangeDirection;
		}

		// Token: 0x0600075A RID: 1882 RVA: 0x0001AD64 File Offset: 0x00018F64
		internal static FocusChangeDirection GetKeyDownFocusChangeDirection(EventBase e)
		{
			bool flag = e.eventTypeId == EventBase<KeyDownEvent>.TypeId();
			if (flag)
			{
				KeyDownEvent keyDownEvent = e as KeyDownEvent;
				bool flag2 = keyDownEvent.character == '\u0019' || keyDownEvent.character == '\t';
				if (flag2)
				{
					bool flag3 = keyDownEvent.modifiers == EventModifiers.Shift;
					if (flag3)
					{
						return VisualElementFocusChangeDirection.left;
					}
					bool flag4 = keyDownEvent.modifiers == EventModifiers.None;
					if (flag4)
					{
						return VisualElementFocusChangeDirection.right;
					}
				}
			}
			return FocusChangeDirection.none;
		}

		// Token: 0x0600075B RID: 1883 RVA: 0x0001ADE4 File Offset: 0x00018FE4
		public Focusable GetNextFocusable(Focusable currentFocusable, FocusChangeDirection direction)
		{
			bool flag = direction == FocusChangeDirection.none || direction == FocusChangeDirection.unspecified;
			Focusable focusable;
			if (flag)
			{
				focusable = currentFocusable;
			}
			else
			{
				VisualElementFocusChangeTarget visualElementFocusChangeTarget = direction as VisualElementFocusChangeTarget;
				bool flag2 = visualElementFocusChangeTarget != null;
				if (flag2)
				{
					focusable = visualElementFocusChangeTarget.target;
				}
				else
				{
					this.DoUpdate();
					bool flag3 = this.m_FocusRing.Count == 0;
					if (flag3)
					{
						focusable = null;
					}
					else
					{
						int num = 0;
						bool flag4 = direction == VisualElementFocusChangeDirection.right;
						if (flag4)
						{
							num = this.GetFocusableInternalIndex(currentFocusable) + 1;
							bool flag5 = currentFocusable != null && num == 0;
							if (flag5)
							{
								return VisualElementFocusRing.GetNextFocusableInTree(currentFocusable as VisualElement);
							}
							bool flag6 = num == this.m_FocusRing.Count;
							if (flag6)
							{
								num = 0;
							}
							while (this.m_FocusRing[num].m_Focusable.delegatesFocus)
							{
								num++;
								bool flag7 = num == this.m_FocusRing.Count;
								if (flag7)
								{
									return null;
								}
							}
						}
						else
						{
							bool flag8 = direction == VisualElementFocusChangeDirection.left;
							if (flag8)
							{
								num = this.GetFocusableInternalIndex(currentFocusable) - 1;
								bool flag9 = currentFocusable != null && num == -2;
								if (flag9)
								{
									return VisualElementFocusRing.GetPreviousFocusableInTree(currentFocusable as VisualElement);
								}
								bool flag10 = num < 0;
								if (flag10)
								{
									num = this.m_FocusRing.Count - 1;
								}
								while (this.m_FocusRing[num].m_Focusable.delegatesFocus)
								{
									num--;
									bool flag11 = num == -1;
									if (flag11)
									{
										return null;
									}
								}
							}
						}
						focusable = this.m_FocusRing[num].m_Focusable;
					}
				}
			}
			return focusable;
		}

		// Token: 0x0600075C RID: 1884 RVA: 0x0001AF8C File Offset: 0x0001918C
		internal static Focusable GetNextFocusableInTree(VisualElement currentFocusable)
		{
			bool flag = currentFocusable == null;
			Focusable focusable;
			if (flag)
			{
				focusable = null;
			}
			else
			{
				VisualElement visualElement = currentFocusable.GetNextElementDepthFirst();
				while (!visualElement.canGrabFocus || visualElement.tabIndex < 0 || visualElement.excludeFromFocusRing)
				{
					visualElement = visualElement.GetNextElementDepthFirst();
					bool flag2 = visualElement == null;
					if (flag2)
					{
						visualElement = currentFocusable.GetRoot();
					}
					bool flag3 = visualElement == currentFocusable;
					if (flag3)
					{
						return currentFocusable;
					}
				}
				focusable = visualElement;
			}
			return focusable;
		}

		// Token: 0x0600075D RID: 1885 RVA: 0x0001B000 File Offset: 0x00019200
		internal static Focusable GetPreviousFocusableInTree(VisualElement currentFocusable)
		{
			bool flag = currentFocusable == null;
			Focusable focusable;
			if (flag)
			{
				focusable = null;
			}
			else
			{
				VisualElement visualElement = currentFocusable.GetPreviousElementDepthFirst();
				while (!visualElement.canGrabFocus || visualElement.tabIndex < 0 || visualElement.excludeFromFocusRing)
				{
					visualElement = visualElement.GetPreviousElementDepthFirst();
					bool flag2 = visualElement == null;
					if (flag2)
					{
						visualElement = currentFocusable.GetRoot();
						while (visualElement.childCount > 0)
						{
							visualElement = visualElement.hierarchy.ElementAt(visualElement.childCount - 1);
						}
					}
					bool flag3 = visualElement == currentFocusable;
					if (flag3)
					{
						return currentFocusable;
					}
				}
				focusable = visualElement;
			}
			return focusable;
		}

		// Token: 0x040002F5 RID: 757
		private readonly VisualElement root;

		// Token: 0x040002F7 RID: 759
		private List<VisualElementFocusRing.FocusRingRecord> m_FocusRing;

		// Token: 0x020000EA RID: 234
		public enum DefaultFocusOrder
		{
			// Token: 0x040002F9 RID: 761
			ChildOrder,
			// Token: 0x040002FA RID: 762
			PositionXY,
			// Token: 0x040002FB RID: 763
			PositionYX
		}

		// Token: 0x020000EB RID: 235
		private class FocusRingRecord
		{
			// Token: 0x040002FC RID: 764
			public int m_AutoIndex;

			// Token: 0x040002FD RID: 765
			public Focusable m_Focusable;

			// Token: 0x040002FE RID: 766
			public bool m_IsSlot;

			// Token: 0x040002FF RID: 767
			public List<VisualElementFocusRing.FocusRingRecord> m_ScopeNavigationOrder;
		}
	}
}
