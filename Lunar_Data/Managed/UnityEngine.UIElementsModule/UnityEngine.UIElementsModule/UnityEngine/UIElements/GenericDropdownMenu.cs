using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	// Token: 0x02000139 RID: 313
	public class GenericDropdownMenu : IGenericMenu
	{
		// Token: 0x170001FA RID: 506
		// (get) Token: 0x06000A2E RID: 2606 RVA: 0x00027A5C File Offset: 0x00025C5C
		internal List<GenericDropdownMenu.MenuItem> items
		{
			get
			{
				return this.m_Items;
			}
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x06000A2F RID: 2607 RVA: 0x00027A64 File Offset: 0x00025C64
		internal VisualElement menuContainer
		{
			get
			{
				return this.m_MenuContainer;
			}
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x06000A30 RID: 2608 RVA: 0x00027A6C File Offset: 0x00025C6C
		public VisualElement contentContainer
		{
			get
			{
				return this.m_ScrollView.contentContainer;
			}
		}

		// Token: 0x06000A31 RID: 2609 RVA: 0x00027A7C File Offset: 0x00025C7C
		public GenericDropdownMenu()
		{
			this.m_MenuContainer = new VisualElement();
			this.m_MenuContainer.AddToClassList(GenericDropdownMenu.ussClassName);
			this.m_OuterContainer = new VisualElement();
			this.m_OuterContainer.AddToClassList(GenericDropdownMenu.containerOuterUssClassName);
			this.m_MenuContainer.Add(this.m_OuterContainer);
			this.m_ScrollView = new ScrollView();
			this.m_ScrollView.AddToClassList(GenericDropdownMenu.containerInnerUssClassName);
			this.m_ScrollView.pickingMode = PickingMode.Position;
			this.m_ScrollView.contentContainer.focusable = true;
			this.m_ScrollView.touchScrollBehavior = ScrollView.TouchScrollBehavior.Clamped;
			this.m_OuterContainer.hierarchy.Add(this.m_ScrollView);
			this.m_MenuContainer.RegisterCallback<AttachToPanelEvent>(new EventCallback<AttachToPanelEvent>(this.OnAttachToPanel), TrickleDown.NoTrickleDown);
			this.m_MenuContainer.RegisterCallback<DetachFromPanelEvent>(new EventCallback<DetachFromPanelEvent>(this.OnDetachFromPanel), TrickleDown.NoTrickleDown);
		}

		// Token: 0x06000A32 RID: 2610 RVA: 0x00027B7C File Offset: 0x00025D7C
		private void OnAttachToPanel(AttachToPanelEvent evt)
		{
			bool flag = evt.destinationPanel == null;
			if (!flag)
			{
				this.contentContainer.AddManipulator(this.m_NavigationManipulator = new KeyboardNavigationManipulator(new Action<KeyboardNavigationOperation, EventBase>(this.Apply)));
				this.m_MenuContainer.RegisterCallback<PointerDownEvent>(new EventCallback<PointerDownEvent>(this.OnPointerDown), TrickleDown.NoTrickleDown);
				this.m_MenuContainer.RegisterCallback<PointerMoveEvent>(new EventCallback<PointerMoveEvent>(this.OnPointerMove), TrickleDown.NoTrickleDown);
				this.m_MenuContainer.RegisterCallback<PointerUpEvent>(new EventCallback<PointerUpEvent>(this.OnPointerUp), TrickleDown.NoTrickleDown);
				evt.destinationPanel.visualTree.RegisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.OnParentResized), TrickleDown.NoTrickleDown);
				this.m_ScrollView.RegisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.OnContainerGeometryChanged), TrickleDown.NoTrickleDown);
				this.m_ScrollView.RegisterCallback<FocusOutEvent>(new EventCallback<FocusOutEvent>(this.OnFocusOut), TrickleDown.NoTrickleDown);
			}
		}

		// Token: 0x06000A33 RID: 2611 RVA: 0x00027C60 File Offset: 0x00025E60
		private void OnDetachFromPanel(DetachFromPanelEvent evt)
		{
			bool flag = evt.originPanel == null;
			if (!flag)
			{
				this.contentContainer.RemoveManipulator(this.m_NavigationManipulator);
				this.m_MenuContainer.UnregisterCallback<PointerDownEvent>(new EventCallback<PointerDownEvent>(this.OnPointerDown), TrickleDown.NoTrickleDown);
				this.m_MenuContainer.UnregisterCallback<PointerMoveEvent>(new EventCallback<PointerMoveEvent>(this.OnPointerMove), TrickleDown.NoTrickleDown);
				this.m_MenuContainer.UnregisterCallback<PointerUpEvent>(new EventCallback<PointerUpEvent>(this.OnPointerUp), TrickleDown.NoTrickleDown);
				evt.originPanel.visualTree.UnregisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.OnParentResized), TrickleDown.NoTrickleDown);
				this.m_ScrollView.UnregisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.OnContainerGeometryChanged), TrickleDown.NoTrickleDown);
				this.m_ScrollView.UnregisterCallback<FocusOutEvent>(new EventCallback<FocusOutEvent>(this.OnFocusOut), TrickleDown.NoTrickleDown);
			}
		}

		// Token: 0x06000A34 RID: 2612 RVA: 0x00027D30 File Offset: 0x00025F30
		private void Hide()
		{
			this.m_MenuContainer.RemoveFromHierarchy();
			bool flag = this.m_TargetElement != null;
			if (flag)
			{
				this.m_TargetElement.UnregisterCallback<DetachFromPanelEvent>(new EventCallback<DetachFromPanelEvent>(this.OnTargetElementDetachFromPanel), TrickleDown.NoTrickleDown);
				this.m_TargetElement.pseudoStates ^= PseudoStates.Active;
			}
			this.m_TargetElement = null;
		}

		// Token: 0x06000A35 RID: 2613 RVA: 0x00027D90 File Offset: 0x00025F90
		private void Apply(KeyboardNavigationOperation op, EventBase sourceEvent)
		{
			bool flag = this.Apply(op);
			if (flag)
			{
				sourceEvent.StopPropagation();
				sourceEvent.PreventDefault();
			}
		}

		// Token: 0x06000A36 RID: 2614 RVA: 0x00027DBC File Offset: 0x00025FBC
		private bool Apply(KeyboardNavigationOperation op)
		{
			GenericDropdownMenu.<>c__DisplayClass27_0 CS$<>8__locals1;
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.selectedIndex = this.GetSelectedIndex();
			bool flag;
			switch (op)
			{
			case KeyboardNavigationOperation.Cancel:
				this.Hide();
				flag = true;
				break;
			case KeyboardNavigationOperation.Submit:
			{
				GenericDropdownMenu.MenuItem menuItem = this.m_Items[CS$<>8__locals1.selectedIndex];
				bool flag2 = CS$<>8__locals1.selectedIndex >= 0 && menuItem.element.enabledSelf;
				if (flag2)
				{
					Action action = menuItem.action;
					if (action != null)
					{
						action.Invoke();
					}
					Action<object> actionUserData = menuItem.actionUserData;
					if (actionUserData != null)
					{
						actionUserData.Invoke(menuItem.element.userData);
					}
				}
				this.Hide();
				flag = true;
				break;
			}
			case KeyboardNavigationOperation.Previous:
				this.<Apply>g__UpdateSelectionUp|27_1((CS$<>8__locals1.selectedIndex < 0) ? (this.m_Items.Count - 1) : (CS$<>8__locals1.selectedIndex - 1), ref CS$<>8__locals1);
				flag = true;
				break;
			case KeyboardNavigationOperation.Next:
				this.<Apply>g__UpdateSelectionDown|27_0(CS$<>8__locals1.selectedIndex + 1, ref CS$<>8__locals1);
				flag = true;
				break;
			case KeyboardNavigationOperation.PageUp:
			case KeyboardNavigationOperation.Begin:
				this.<Apply>g__UpdateSelectionDown|27_0(0, ref CS$<>8__locals1);
				flag = true;
				break;
			case KeyboardNavigationOperation.PageDown:
			case KeyboardNavigationOperation.End:
				this.<Apply>g__UpdateSelectionUp|27_1(this.m_Items.Count - 1, ref CS$<>8__locals1);
				flag = true;
				break;
			default:
				flag = false;
				break;
			}
			return flag;
		}

		// Token: 0x06000A37 RID: 2615 RVA: 0x00027F04 File Offset: 0x00026104
		private void OnPointerDown(PointerDownEvent evt)
		{
			this.m_MousePosition = this.m_ScrollView.WorldToLocal(evt.position);
			this.UpdateSelection(evt.target as VisualElement);
			bool flag = evt.pointerId != PointerId.mousePointerId;
			if (flag)
			{
				this.m_MenuContainer.panel.PreventCompatibilityMouseEvents(evt.pointerId);
			}
			evt.StopPropagation();
		}

		// Token: 0x06000A38 RID: 2616 RVA: 0x00027F74 File Offset: 0x00026174
		private void OnPointerMove(PointerMoveEvent evt)
		{
			this.m_MousePosition = this.m_ScrollView.WorldToLocal(evt.position);
			this.UpdateSelection(evt.target as VisualElement);
			bool flag = evt.pointerId != PointerId.mousePointerId;
			if (flag)
			{
				this.m_MenuContainer.panel.PreventCompatibilityMouseEvents(evt.pointerId);
			}
			evt.StopPropagation();
		}

		// Token: 0x06000A39 RID: 2617 RVA: 0x00027FE4 File Offset: 0x000261E4
		private void OnPointerUp(PointerUpEvent evt)
		{
			int selectedIndex = this.GetSelectedIndex();
			bool flag = selectedIndex != -1;
			if (flag)
			{
				GenericDropdownMenu.MenuItem menuItem = this.m_Items[selectedIndex];
				Action action = menuItem.action;
				if (action != null)
				{
					action.Invoke();
				}
				Action<object> actionUserData = menuItem.actionUserData;
				if (actionUserData != null)
				{
					actionUserData.Invoke(menuItem.element.userData);
				}
				this.Hide();
			}
			bool flag2 = evt.pointerId != PointerId.mousePointerId;
			if (flag2)
			{
				this.m_MenuContainer.panel.PreventCompatibilityMouseEvents(evt.pointerId);
			}
			evt.StopPropagation();
		}

		// Token: 0x06000A3A RID: 2618 RVA: 0x00028080 File Offset: 0x00026280
		private void OnFocusOut(FocusOutEvent evt)
		{
			bool flag = !this.m_ScrollView.ContainsPoint(this.m_MousePosition);
			if (flag)
			{
				this.Hide();
			}
			else
			{
				this.m_MenuContainer.schedule.Execute(new Action(this.contentContainer.Focus));
			}
		}

		// Token: 0x06000A3B RID: 2619 RVA: 0x000280D6 File Offset: 0x000262D6
		private void OnParentResized(GeometryChangedEvent evt)
		{
			this.Hide();
		}

		// Token: 0x06000A3C RID: 2620 RVA: 0x000280E0 File Offset: 0x000262E0
		private void UpdateSelection(VisualElement target)
		{
			bool flag = !this.m_ScrollView.ContainsPoint(this.m_MousePosition);
			if (flag)
			{
				int selectedIndex = this.GetSelectedIndex();
				bool flag2 = selectedIndex >= 0;
				if (flag2)
				{
					this.m_Items[selectedIndex].element.pseudoStates &= ~PseudoStates.Hover;
				}
			}
			else
			{
				bool flag3 = target == null;
				if (!flag3)
				{
					bool flag4 = (target.pseudoStates & PseudoStates.Hover) != PseudoStates.Hover;
					if (flag4)
					{
						int selectedIndex2 = this.GetSelectedIndex();
						bool flag5 = selectedIndex2 >= 0;
						if (flag5)
						{
							this.m_Items[selectedIndex2].element.pseudoStates &= ~PseudoStates.Hover;
						}
						target.pseudoStates |= PseudoStates.Hover;
					}
				}
			}
		}

		// Token: 0x06000A3D RID: 2621 RVA: 0x000281A4 File Offset: 0x000263A4
		private void ChangeSelectedIndex(int newIndex, int previousIndex)
		{
			bool flag = previousIndex >= 0 && previousIndex < this.m_Items.Count;
			if (flag)
			{
				this.m_Items[previousIndex].element.pseudoStates &= ~PseudoStates.Hover;
			}
			bool flag2 = newIndex >= 0 && newIndex < this.m_Items.Count;
			if (flag2)
			{
				this.m_Items[newIndex].element.pseudoStates |= PseudoStates.Hover;
				this.m_ScrollView.ScrollTo(this.m_Items[newIndex].element);
			}
		}

		// Token: 0x06000A3E RID: 2622 RVA: 0x00028244 File Offset: 0x00026444
		private int GetSelectedIndex()
		{
			for (int i = 0; i < this.m_Items.Count; i++)
			{
				bool flag = (this.m_Items[i].element.pseudoStates & PseudoStates.Hover) == PseudoStates.Hover;
				if (flag)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06000A3F RID: 2623 RVA: 0x00028298 File Offset: 0x00026498
		public void AddItem(string itemName, bool isChecked, Action action)
		{
			GenericDropdownMenu.MenuItem menuItem = this.AddItem(itemName, isChecked, true, null);
			bool flag = menuItem != null;
			if (flag)
			{
				menuItem.action = action;
			}
		}

		// Token: 0x06000A40 RID: 2624 RVA: 0x000282C4 File Offset: 0x000264C4
		public void AddItem(string itemName, bool isChecked, Action<object> action, object data)
		{
			GenericDropdownMenu.MenuItem menuItem = this.AddItem(itemName, isChecked, true, data);
			bool flag = menuItem != null;
			if (flag)
			{
				menuItem.actionUserData = action;
			}
		}

		// Token: 0x06000A41 RID: 2625 RVA: 0x000282EF File Offset: 0x000264EF
		public void AddDisabledItem(string itemName, bool isChecked)
		{
			this.AddItem(itemName, isChecked, false, null);
		}

		// Token: 0x06000A42 RID: 2626 RVA: 0x00028300 File Offset: 0x00026500
		public void AddSeparator(string path)
		{
			VisualElement visualElement = new VisualElement();
			visualElement.AddToClassList(GenericDropdownMenu.separatorUssClassName);
			visualElement.pickingMode = PickingMode.Ignore;
			this.m_ScrollView.Add(visualElement);
		}

		// Token: 0x06000A43 RID: 2627 RVA: 0x00028338 File Offset: 0x00026538
		private GenericDropdownMenu.MenuItem AddItem(string itemName, bool isChecked, bool isEnabled, object data = null)
		{
			bool flag = string.IsNullOrEmpty(itemName) || itemName.EndsWith("/");
			GenericDropdownMenu.MenuItem menuItem;
			if (flag)
			{
				this.AddSeparator(itemName);
				menuItem = null;
			}
			else
			{
				for (int i = 0; i < this.m_Items.Count; i++)
				{
					bool flag2 = itemName == this.m_Items[i].name;
					if (flag2)
					{
						return null;
					}
				}
				VisualElement visualElement = new VisualElement();
				visualElement.AddToClassList(GenericDropdownMenu.itemUssClassName);
				visualElement.SetEnabled(isEnabled);
				visualElement.userData = data;
				if (isChecked)
				{
					VisualElement visualElement2 = new VisualElement();
					visualElement2.AddToClassList(GenericDropdownMenu.checkmarkUssClassName);
					visualElement2.pickingMode = PickingMode.Ignore;
					visualElement.Add(visualElement2);
					visualElement.pseudoStates |= PseudoStates.Checked;
				}
				Label label = new Label(itemName);
				label.AddToClassList(GenericDropdownMenu.labelUssClassName);
				label.pickingMode = PickingMode.Ignore;
				visualElement.Add(label);
				this.m_ScrollView.Add(visualElement);
				GenericDropdownMenu.MenuItem menuItem2 = new GenericDropdownMenu.MenuItem
				{
					name = itemName,
					element = visualElement
				};
				this.m_Items.Add(menuItem2);
				menuItem = menuItem2;
			}
			return menuItem;
		}

		// Token: 0x06000A44 RID: 2628 RVA: 0x00028470 File Offset: 0x00026670
		public void DropDown(Rect position, VisualElement targetElement = null, bool anchored = false)
		{
			bool flag = targetElement == null;
			if (flag)
			{
				Debug.LogError("VisualElement Generic Menu needs a target to find a root to attach to.");
			}
			else
			{
				this.m_TargetElement = targetElement;
				this.m_TargetElement.RegisterCallback<DetachFromPanelEvent>(new EventCallback<DetachFromPanelEvent>(this.OnTargetElementDetachFromPanel), TrickleDown.NoTrickleDown);
				this.m_PanelRootVisualContainer = this.m_TargetElement.GetRootVisualContainer();
				bool flag2 = this.m_PanelRootVisualContainer == null;
				if (flag2)
				{
					Debug.LogError("Could not find rootVisualContainer...");
				}
				else
				{
					this.m_PanelRootVisualContainer.Add(this.m_MenuContainer);
					this.m_MenuContainer.style.left = this.m_PanelRootVisualContainer.layout.x;
					this.m_MenuContainer.style.top = this.m_PanelRootVisualContainer.layout.y;
					this.m_MenuContainer.style.width = this.m_PanelRootVisualContainer.layout.width;
					this.m_MenuContainer.style.height = this.m_PanelRootVisualContainer.layout.height;
					Rect rect = this.m_PanelRootVisualContainer.WorldToLocal(position);
					this.m_OuterContainer.style.left = rect.x - this.m_PanelRootVisualContainer.layout.x;
					this.m_OuterContainer.style.top = rect.y + position.height - this.m_PanelRootVisualContainer.layout.y;
					this.m_DesiredRect = (anchored ? position : Rect.zero);
					this.m_MenuContainer.schedule.Execute(new Action(this.contentContainer.Focus));
					this.EnsureVisibilityInParent();
					bool flag3 = targetElement != null;
					if (flag3)
					{
						targetElement.pseudoStates |= PseudoStates.Active;
					}
				}
			}
		}

		// Token: 0x06000A45 RID: 2629 RVA: 0x000280D6 File Offset: 0x000262D6
		private void OnTargetElementDetachFromPanel(DetachFromPanelEvent evt)
		{
			this.Hide();
		}

		// Token: 0x06000A46 RID: 2630 RVA: 0x00028667 File Offset: 0x00026867
		private void OnContainerGeometryChanged(GeometryChangedEvent evt)
		{
			this.EnsureVisibilityInParent();
		}

		// Token: 0x06000A47 RID: 2631 RVA: 0x00028674 File Offset: 0x00026874
		private void EnsureVisibilityInParent()
		{
			bool flag = this.m_PanelRootVisualContainer != null && !float.IsNaN(this.m_OuterContainer.layout.width) && !float.IsNaN(this.m_OuterContainer.layout.height);
			if (flag)
			{
				bool flag2 = this.m_DesiredRect == Rect.zero;
				if (flag2)
				{
					float num = Mathf.Min(this.m_OuterContainer.layout.x, this.m_PanelRootVisualContainer.layout.width - this.m_OuterContainer.layout.width);
					float num2 = Mathf.Min(this.m_OuterContainer.layout.y, Mathf.Max(0f, this.m_PanelRootVisualContainer.layout.height - this.m_OuterContainer.layout.height));
					this.m_OuterContainer.style.left = num;
					this.m_OuterContainer.style.top = num2;
				}
				this.m_OuterContainer.style.height = Mathf.Min(this.m_MenuContainer.layout.height - this.m_MenuContainer.layout.y - this.m_OuterContainer.layout.y, this.m_ScrollView.layout.height + this.m_OuterContainer.resolvedStyle.borderBottomWidth + this.m_OuterContainer.resolvedStyle.borderTopWidth);
				bool flag3 = this.m_DesiredRect != Rect.zero;
				if (flag3)
				{
					this.m_OuterContainer.style.width = this.m_DesiredRect.width;
				}
			}
		}

		// Token: 0x06000A49 RID: 2633 RVA: 0x000288F4 File Offset: 0x00026AF4
		[CompilerGenerated]
		private void <Apply>g__UpdateSelectionDown|27_0(int newIndex, ref GenericDropdownMenu.<>c__DisplayClass27_0 A_2)
		{
			while (newIndex < this.m_Items.Count)
			{
				bool enabledSelf = this.m_Items[newIndex].element.enabledSelf;
				if (enabledSelf)
				{
					this.ChangeSelectedIndex(newIndex, A_2.selectedIndex);
					break;
				}
				newIndex++;
			}
		}

		// Token: 0x06000A4A RID: 2634 RVA: 0x00028948 File Offset: 0x00026B48
		[CompilerGenerated]
		private void <Apply>g__UpdateSelectionUp|27_1(int newIndex, ref GenericDropdownMenu.<>c__DisplayClass27_0 A_2)
		{
			while (newIndex >= 0)
			{
				bool enabledSelf = this.m_Items[newIndex].element.enabledSelf;
				if (enabledSelf)
				{
					this.ChangeSelectedIndex(newIndex, A_2.selectedIndex);
					break;
				}
				newIndex--;
			}
		}

		// Token: 0x04000455 RID: 1109
		public static readonly string ussClassName = "unity-base-dropdown";

		// Token: 0x04000456 RID: 1110
		public static readonly string itemUssClassName = GenericDropdownMenu.ussClassName + "__item";

		// Token: 0x04000457 RID: 1111
		public static readonly string labelUssClassName = GenericDropdownMenu.ussClassName + "__label";

		// Token: 0x04000458 RID: 1112
		public static readonly string containerInnerUssClassName = GenericDropdownMenu.ussClassName + "__container-inner";

		// Token: 0x04000459 RID: 1113
		public static readonly string containerOuterUssClassName = GenericDropdownMenu.ussClassName + "__container-outer";

		// Token: 0x0400045A RID: 1114
		public static readonly string checkmarkUssClassName = GenericDropdownMenu.ussClassName + "__checkmark";

		// Token: 0x0400045B RID: 1115
		public static readonly string separatorUssClassName = GenericDropdownMenu.ussClassName + "__separator";

		// Token: 0x0400045C RID: 1116
		private List<GenericDropdownMenu.MenuItem> m_Items = new List<GenericDropdownMenu.MenuItem>();

		// Token: 0x0400045D RID: 1117
		private VisualElement m_MenuContainer;

		// Token: 0x0400045E RID: 1118
		private VisualElement m_OuterContainer;

		// Token: 0x0400045F RID: 1119
		private ScrollView m_ScrollView;

		// Token: 0x04000460 RID: 1120
		private VisualElement m_PanelRootVisualContainer;

		// Token: 0x04000461 RID: 1121
		private VisualElement m_TargetElement;

		// Token: 0x04000462 RID: 1122
		private Rect m_DesiredRect;

		// Token: 0x04000463 RID: 1123
		private KeyboardNavigationManipulator m_NavigationManipulator;

		// Token: 0x04000464 RID: 1124
		private Vector2 m_MousePosition;

		// Token: 0x0200013A RID: 314
		internal class MenuItem
		{
			// Token: 0x04000465 RID: 1125
			public string name;

			// Token: 0x04000466 RID: 1126
			public VisualElement element;

			// Token: 0x04000467 RID: 1127
			public Action action;

			// Token: 0x04000468 RID: 1128
			public Action<object> actionUserData;
		}
	}
}
