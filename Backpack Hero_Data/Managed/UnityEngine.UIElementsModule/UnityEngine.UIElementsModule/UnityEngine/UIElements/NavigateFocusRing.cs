using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000240 RID: 576
	internal class NavigateFocusRing : IFocusRing
	{
		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x06001130 RID: 4400 RVA: 0x00042349 File Offset: 0x00040549
		private FocusController focusController
		{
			get
			{
				return this.m_Root.focusController;
			}
		}

		// Token: 0x06001131 RID: 4401 RVA: 0x00042356 File Offset: 0x00040556
		public NavigateFocusRing(VisualElement root)
		{
			this.m_Root = root;
			this.m_Ring = new VisualElementFocusRing(root, VisualElementFocusRing.DefaultFocusOrder.ChildOrder);
		}

		// Token: 0x06001132 RID: 4402 RVA: 0x00042374 File Offset: 0x00040574
		public FocusChangeDirection GetFocusChangeDirection(Focusable currentFocusable, EventBase e)
		{
			bool flag = e.eventTypeId == EventBase<PointerDownEvent>.TypeId();
			if (flag)
			{
				Focusable focusable;
				bool focusableParentForPointerEvent = this.focusController.GetFocusableParentForPointerEvent(e.target as Focusable, out focusable);
				if (focusableParentForPointerEvent)
				{
					return VisualElementFocusChangeTarget.GetPooled(focusable);
				}
			}
			bool flag2 = e.eventTypeId == EventBase<NavigationMoveEvent>.TypeId();
			if (flag2)
			{
				switch (((NavigationMoveEvent)e).direction)
				{
				case NavigationMoveEvent.Direction.Left:
					return NavigateFocusRing.Left;
				case NavigationMoveEvent.Direction.Up:
					return NavigateFocusRing.Up;
				case NavigationMoveEvent.Direction.Right:
					return NavigateFocusRing.Right;
				case NavigationMoveEvent.Direction.Down:
					return NavigateFocusRing.Down;
				}
			}
			else
			{
				bool flag3 = e.eventTypeId == EventBase<KeyDownEvent>.TypeId();
				if (flag3)
				{
					KeyDownEvent keyDownEvent = (KeyDownEvent)e;
					bool flag4 = keyDownEvent.character == '\u0019' || keyDownEvent.character == '\t';
					if (flag4)
					{
						return keyDownEvent.shiftKey ? NavigateFocusRing.Previous : NavigateFocusRing.Next;
					}
				}
			}
			return FocusChangeDirection.none;
		}

		// Token: 0x06001133 RID: 4403 RVA: 0x00042480 File Offset: 0x00040680
		public virtual Focusable GetNextFocusable(Focusable currentFocusable, FocusChangeDirection direction)
		{
			VisualElementFocusChangeTarget visualElementFocusChangeTarget = direction as VisualElementFocusChangeTarget;
			bool flag = visualElementFocusChangeTarget != null;
			Focusable focusable;
			if (flag)
			{
				focusable = visualElementFocusChangeTarget.target;
			}
			else
			{
				bool flag2 = direction == NavigateFocusRing.Next || direction == NavigateFocusRing.Previous;
				if (flag2)
				{
					focusable = this.m_Ring.GetNextFocusable(currentFocusable, (direction == NavigateFocusRing.Next) ? VisualElementFocusChangeDirection.right : VisualElementFocusChangeDirection.left);
				}
				else
				{
					bool flag3 = direction == NavigateFocusRing.Up || direction == NavigateFocusRing.Down || direction == NavigateFocusRing.Right || direction == NavigateFocusRing.Left;
					if (flag3)
					{
						focusable = this.GetNextFocusable2D(currentFocusable, (NavigateFocusRing.ChangeDirection)direction);
					}
					else
					{
						focusable = currentFocusable;
					}
				}
			}
			return focusable;
		}

		// Token: 0x06001134 RID: 4404 RVA: 0x00042524 File Offset: 0x00040724
		private Focusable GetNextFocusable2D(Focusable currentFocusable, NavigateFocusRing.ChangeDirection direction)
		{
			VisualElement visualElement = currentFocusable as VisualElement;
			bool flag = visualElement == null;
			if (flag)
			{
				visualElement = this.m_Root;
			}
			visualElement = NavigateFocusRing.GetRootFocusable(visualElement);
			Rect worldBoundingBox = this.m_Root.worldBoundingBox;
			Rect rect = new Rect(worldBoundingBox.position - Vector2.one, worldBoundingBox.size + Vector2.one * 2f);
			Rect worldBound = visualElement.worldBound;
			Rect rect2 = new Rect(worldBound.position - Vector2.one, worldBound.size + Vector2.one * 2f);
			bool flag2 = direction == NavigateFocusRing.Up;
			if (flag2)
			{
				rect2.yMin = rect.yMin;
			}
			else
			{
				bool flag3 = direction == NavigateFocusRing.Down;
				if (flag3)
				{
					rect2.yMax = rect.yMax;
				}
				else
				{
					bool flag4 = direction == NavigateFocusRing.Left;
					if (flag4)
					{
						rect2.xMin = rect.xMin;
					}
					else
					{
						bool flag5 = direction == NavigateFocusRing.Right;
						if (flag5)
						{
							rect2.xMax = rect.xMax;
						}
					}
				}
			}
			NavigateFocusRing.FocusableHierarchyTraversal focusableHierarchyTraversal = default(NavigateFocusRing.FocusableHierarchyTraversal);
			focusableHierarchyTraversal.currentFocusable = visualElement;
			focusableHierarchyTraversal.direction = direction;
			focusableHierarchyTraversal.validRect = rect2;
			focusableHierarchyTraversal.firstPass = true;
			VisualElement visualElement2 = focusableHierarchyTraversal.GetBestOverall(this.m_Root, null);
			bool flag6 = visualElement2 != null;
			Focusable focusable;
			if (flag6)
			{
				focusable = NavigateFocusRing.GetLeafFocusable(visualElement2);
			}
			else
			{
				rect2 = new Rect(worldBound.position - Vector2.one, worldBound.size + Vector2.one * 2f);
				bool flag7 = direction == NavigateFocusRing.Down;
				if (flag7)
				{
					rect2.yMin = rect.yMin;
				}
				else
				{
					bool flag8 = direction == NavigateFocusRing.Up;
					if (flag8)
					{
						rect2.yMax = rect.yMax;
					}
					else
					{
						bool flag9 = direction == NavigateFocusRing.Right;
						if (flag9)
						{
							rect2.xMin = rect.xMin;
						}
						else
						{
							bool flag10 = direction == NavigateFocusRing.Left;
							if (flag10)
							{
								rect2.xMax = rect.xMax;
							}
						}
					}
				}
				focusableHierarchyTraversal = default(NavigateFocusRing.FocusableHierarchyTraversal);
				focusableHierarchyTraversal.currentFocusable = visualElement;
				focusableHierarchyTraversal.direction = direction;
				focusableHierarchyTraversal.validRect = rect2;
				focusableHierarchyTraversal.firstPass = false;
				visualElement2 = focusableHierarchyTraversal.GetBestOverall(this.m_Root, null);
				bool flag11 = visualElement2 != null;
				if (flag11)
				{
					focusable = NavigateFocusRing.GetLeafFocusable(visualElement2);
				}
				else
				{
					focusable = currentFocusable;
				}
			}
			return focusable;
		}

		// Token: 0x06001135 RID: 4405 RVA: 0x0004279C File Offset: 0x0004099C
		private static bool IsActive(VisualElement v)
		{
			return v.resolvedStyle.display != DisplayStyle.None && v.enabledInHierarchy;
		}

		// Token: 0x06001136 RID: 4406 RVA: 0x000427C8 File Offset: 0x000409C8
		private static bool IsFocusable(Focusable focusable)
		{
			return focusable.canGrabFocus && focusable.tabIndex >= 0;
		}

		// Token: 0x06001137 RID: 4407 RVA: 0x000427F4 File Offset: 0x000409F4
		private static bool IsLeaf(Focusable focusable)
		{
			return !focusable.excludeFromFocusRing && !focusable.delegatesFocus;
		}

		// Token: 0x06001138 RID: 4408 RVA: 0x0004281C File Offset: 0x00040A1C
		private static bool IsFocusRoot(VisualElement focusable)
		{
			bool isCompositeRoot = focusable.isCompositeRoot;
			bool flag;
			if (isCompositeRoot)
			{
				flag = true;
			}
			else
			{
				VisualElement parent = focusable.hierarchy.parent;
				flag = parent == null || !NavigateFocusRing.IsFocusable(parent);
			}
			return flag;
		}

		// Token: 0x06001139 RID: 4409 RVA: 0x0004285C File Offset: 0x00040A5C
		private static VisualElement GetLeafFocusable(VisualElement v)
		{
			return NavigateFocusRing.GetLeafFocusableRecursive(v) ?? v;
		}

		// Token: 0x0600113A RID: 4410 RVA: 0x0004287C File Offset: 0x00040A7C
		private static VisualElement GetLeafFocusableRecursive(VisualElement v)
		{
			bool flag = NavigateFocusRing.IsLeaf(v);
			VisualElement visualElement;
			if (flag)
			{
				visualElement = v;
			}
			else
			{
				int childCount = v.childCount;
				for (int i = 0; i < childCount; i++)
				{
					VisualElement visualElement2 = v[i];
					bool flag2 = !NavigateFocusRing.IsFocusable(visualElement2);
					if (!flag2)
					{
						VisualElement leafFocusableRecursive = NavigateFocusRing.GetLeafFocusableRecursive(visualElement2);
						bool flag3 = leafFocusableRecursive != null;
						if (flag3)
						{
							return leafFocusableRecursive;
						}
					}
				}
				visualElement = null;
			}
			return visualElement;
		}

		// Token: 0x0600113B RID: 4411 RVA: 0x000428EC File Offset: 0x00040AEC
		private static VisualElement GetRootFocusable(VisualElement v)
		{
			for (;;)
			{
				bool flag = NavigateFocusRing.IsFocusRoot(v);
				if (flag)
				{
					break;
				}
				v = v.hierarchy.parent;
			}
			return v;
		}

		// Token: 0x04000790 RID: 1936
		public static readonly NavigateFocusRing.ChangeDirection Left = new NavigateFocusRing.ChangeDirection(1);

		// Token: 0x04000791 RID: 1937
		public static readonly NavigateFocusRing.ChangeDirection Right = new NavigateFocusRing.ChangeDirection(2);

		// Token: 0x04000792 RID: 1938
		public static readonly NavigateFocusRing.ChangeDirection Up = new NavigateFocusRing.ChangeDirection(3);

		// Token: 0x04000793 RID: 1939
		public static readonly NavigateFocusRing.ChangeDirection Down = new NavigateFocusRing.ChangeDirection(4);

		// Token: 0x04000794 RID: 1940
		public static readonly NavigateFocusRing.ChangeDirection Next = new NavigateFocusRing.ChangeDirection(5);

		// Token: 0x04000795 RID: 1941
		public static readonly NavigateFocusRing.ChangeDirection Previous = new NavigateFocusRing.ChangeDirection(6);

		// Token: 0x04000796 RID: 1942
		private readonly VisualElement m_Root;

		// Token: 0x04000797 RID: 1943
		private readonly VisualElementFocusRing m_Ring;

		// Token: 0x02000241 RID: 577
		public class ChangeDirection : FocusChangeDirection
		{
			// Token: 0x0600113D RID: 4413 RVA: 0x0001A5CB File Offset: 0x000187CB
			public ChangeDirection(int i)
				: base(i)
			{
			}
		}

		// Token: 0x02000242 RID: 578
		private struct FocusableHierarchyTraversal
		{
			// Token: 0x0600113E RID: 4414 RVA: 0x00042974 File Offset: 0x00040B74
			private bool ValidateHierarchyTraversal(VisualElement v)
			{
				return NavigateFocusRing.IsActive(v) && v.worldBoundingBox.Overlaps(this.validRect);
			}

			// Token: 0x0600113F RID: 4415 RVA: 0x000429A8 File Offset: 0x00040BA8
			private bool ValidateElement(VisualElement v)
			{
				return NavigateFocusRing.IsFocusable(v) && v.worldBound.Overlaps(this.validRect);
			}

			// Token: 0x06001140 RID: 4416 RVA: 0x000429DC File Offset: 0x00040BDC
			private int Order(VisualElement a, VisualElement b)
			{
				Rect worldBound = a.worldBound;
				Rect worldBound2 = b.worldBound;
				int num = this.StrictOrder(worldBound, worldBound2);
				return (num != 0) ? num : this.TieBreaker(worldBound, worldBound2);
			}

			// Token: 0x06001141 RID: 4417 RVA: 0x00042A14 File Offset: 0x00040C14
			private int StrictOrder(VisualElement a, VisualElement b)
			{
				return this.StrictOrder(a.worldBound, b.worldBound);
			}

			// Token: 0x06001142 RID: 4418 RVA: 0x00042A38 File Offset: 0x00040C38
			private int StrictOrder(Rect ra, Rect rb)
			{
				float num = 0f;
				bool flag = this.direction == NavigateFocusRing.Up;
				if (flag)
				{
					num = rb.yMax - ra.yMax;
				}
				else
				{
					bool flag2 = this.direction == NavigateFocusRing.Down;
					if (flag2)
					{
						num = ra.yMin - rb.yMin;
					}
					else
					{
						bool flag3 = this.direction == NavigateFocusRing.Left;
						if (flag3)
						{
							num = rb.xMax - ra.xMax;
						}
						else
						{
							bool flag4 = this.direction == NavigateFocusRing.Right;
							if (flag4)
							{
								num = ra.xMin - rb.xMin;
							}
						}
					}
				}
				bool flag5 = !Mathf.Approximately(num, 0f);
				int num2;
				if (flag5)
				{
					num2 = ((num > 0f) ? 1 : (-1));
				}
				else
				{
					num2 = 0;
				}
				return num2;
			}

			// Token: 0x06001143 RID: 4419 RVA: 0x00042B04 File Offset: 0x00040D04
			private int TieBreaker(Rect ra, Rect rb)
			{
				Rect worldBound = this.currentFocusable.worldBound;
				float num = (ra.min - worldBound.min).sqrMagnitude - (rb.min - worldBound.min).sqrMagnitude;
				bool flag = !Mathf.Approximately(num, 0f);
				int num2;
				if (flag)
				{
					num2 = ((num > 0f) ? 1 : (-1));
				}
				else
				{
					num2 = 0;
				}
				return num2;
			}

			// Token: 0x06001144 RID: 4420 RVA: 0x00042B80 File Offset: 0x00040D80
			public VisualElement GetBestOverall(VisualElement candidate, VisualElement bestSoFar = null)
			{
				bool flag = !this.ValidateHierarchyTraversal(candidate);
				VisualElement visualElement;
				if (flag)
				{
					visualElement = bestSoFar;
				}
				else
				{
					bool flag2 = this.ValidateElement(candidate);
					if (flag2)
					{
						bool flag3 = (!this.firstPass || this.StrictOrder(candidate, this.currentFocusable) > 0) && (bestSoFar == null || this.Order(bestSoFar, candidate) > 0);
						if (flag3)
						{
							bestSoFar = candidate;
						}
						bool flag4 = NavigateFocusRing.IsFocusRoot(candidate);
						if (flag4)
						{
							return bestSoFar;
						}
					}
					int childCount = candidate.childCount;
					for (int i = 0; i < childCount; i++)
					{
						VisualElement visualElement2 = candidate[i];
						bestSoFar = this.GetBestOverall(visualElement2, bestSoFar);
					}
					visualElement = bestSoFar;
				}
				return visualElement;
			}

			// Token: 0x04000798 RID: 1944
			public VisualElement currentFocusable;

			// Token: 0x04000799 RID: 1945
			public Rect validRect;

			// Token: 0x0400079A RID: 1946
			public bool firstPass;

			// Token: 0x0400079B RID: 1947
			public NavigateFocusRing.ChangeDirection direction;
		}
	}
}
