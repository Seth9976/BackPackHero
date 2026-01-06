using System;
using UnityEngine.Assertions;

namespace UnityEngine.UIElements
{
	// Token: 0x020001EE RID: 494
	internal class MouseEventDispatchingStrategy : IEventDispatchingStrategy
	{
		// Token: 0x06000F38 RID: 3896 RVA: 0x0003CE74 File Offset: 0x0003B074
		public bool CanDispatchEvent(EventBase evt)
		{
			return evt is IMouseEvent;
		}

		// Token: 0x06000F39 RID: 3897 RVA: 0x0003CE90 File Offset: 0x0003B090
		public void DispatchEvent(EventBase evt, IPanel iPanel)
		{
			bool flag = iPanel != null;
			if (flag)
			{
				Assert.IsTrue(iPanel is BaseVisualElementPanel);
				BaseVisualElementPanel baseVisualElementPanel = (BaseVisualElementPanel)iPanel;
				MouseEventDispatchingStrategy.SetBestTargetForEvent(evt, baseVisualElementPanel);
				MouseEventDispatchingStrategy.SendEventToTarget(evt, baseVisualElementPanel);
			}
			evt.stopDispatch = true;
		}

		// Token: 0x06000F3A RID: 3898 RVA: 0x0003CED8 File Offset: 0x0003B0D8
		private static bool SendEventToTarget(EventBase evt, BaseVisualElementPanel panel)
		{
			return MouseEventDispatchingStrategy.SendEventToRegularTarget(evt, panel) || MouseEventDispatchingStrategy.SendEventToIMGUIContainer(evt, panel);
		}

		// Token: 0x06000F3B RID: 3899 RVA: 0x0003CF00 File Offset: 0x0003B100
		private static bool SendEventToRegularTarget(EventBase evt, BaseVisualElementPanel panel)
		{
			bool flag = evt.target == null;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				EventDispatchUtilities.PropagateEvent(evt);
				flag2 = MouseEventDispatchingStrategy.IsDone(evt);
			}
			return flag2;
		}

		// Token: 0x06000F3C RID: 3900 RVA: 0x0003CF30 File Offset: 0x0003B130
		private static bool SendEventToIMGUIContainer(EventBase evt, BaseVisualElementPanel panel)
		{
			bool flag = evt.imguiEvent == null;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				IMGUIContainer rootIMGUIContainer = panel.rootIMGUIContainer;
				bool flag3 = rootIMGUIContainer == null;
				if (flag3)
				{
					flag2 = false;
				}
				else
				{
					bool flag4 = evt.propagateToIMGUI || evt.eventTypeId == EventBase<MouseEnterWindowEvent>.TypeId() || evt.eventTypeId == EventBase<MouseLeaveWindowEvent>.TypeId();
					if (flag4)
					{
						evt.skipElements.Add(evt.target);
						EventDispatchUtilities.PropagateToIMGUIContainer(panel.visualTree, evt);
					}
					flag2 = MouseEventDispatchingStrategy.IsDone(evt);
				}
			}
			return flag2;
		}

		// Token: 0x06000F3D RID: 3901 RVA: 0x0003CFB8 File Offset: 0x0003B1B8
		private static void SetBestTargetForEvent(EventBase evt, BaseVisualElementPanel panel)
		{
			VisualElement visualElement;
			MouseEventDispatchingStrategy.UpdateElementUnderMouse(evt, panel, out visualElement);
			bool flag = evt.target != null;
			if (flag)
			{
				evt.propagateToIMGUI = false;
			}
			else
			{
				bool flag2 = visualElement != null;
				if (flag2)
				{
					evt.propagateToIMGUI = false;
					evt.target = visualElement;
				}
				else
				{
					evt.target = ((panel != null) ? panel.visualTree : null);
				}
			}
		}

		// Token: 0x06000F3E RID: 3902 RVA: 0x0003D01C File Offset: 0x0003B21C
		private static void UpdateElementUnderMouse(EventBase evt, BaseVisualElementPanel panel, out VisualElement elementUnderMouse)
		{
			IMouseEventInternal mouseEventInternal = evt as IMouseEventInternal;
			elementUnderMouse = ((mouseEventInternal == null || mouseEventInternal.recomputeTopElementUnderMouse) ? panel.RecomputeTopElementUnderPointer(PointerId.mousePointerId, ((IMouseEvent)evt).mousePosition, evt) : panel.GetTopElementUnderPointer(PointerId.mousePointerId));
			bool flag = evt.eventTypeId == EventBase<MouseLeaveWindowEvent>.TypeId() && (evt as MouseLeaveWindowEvent).pressedButtons == 0;
			if (flag)
			{
				panel.ClearCachedElementUnderPointer(PointerId.mousePointerId, evt);
			}
		}

		// Token: 0x06000F3F RID: 3903 RVA: 0x0003D098 File Offset: 0x0003B298
		private static bool IsDone(EventBase evt)
		{
			Event imguiEvent = evt.imguiEvent;
			bool flag = imguiEvent != null && imguiEvent.rawType == EventType.Used;
			if (flag)
			{
				evt.StopPropagation();
			}
			return evt.isPropagationStopped;
		}
	}
}
