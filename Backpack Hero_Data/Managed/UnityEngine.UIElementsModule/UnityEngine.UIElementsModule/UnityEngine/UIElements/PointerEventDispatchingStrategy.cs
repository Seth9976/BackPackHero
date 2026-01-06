using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000211 RID: 529
	internal class PointerEventDispatchingStrategy : IEventDispatchingStrategy
	{
		// Token: 0x06000FED RID: 4077 RVA: 0x0003EC2C File Offset: 0x0003CE2C
		public bool CanDispatchEvent(EventBase evt)
		{
			return evt is IPointerEvent;
		}

		// Token: 0x06000FEE RID: 4078 RVA: 0x0003EC47 File Offset: 0x0003CE47
		public virtual void DispatchEvent(EventBase evt, IPanel panel)
		{
			PointerEventDispatchingStrategy.SetBestTargetForEvent(evt, panel);
			PointerEventDispatchingStrategy.SendEventToTarget(evt);
			evt.stopDispatch = true;
		}

		// Token: 0x06000FEF RID: 4079 RVA: 0x0003EC64 File Offset: 0x0003CE64
		private static void SendEventToTarget(EventBase evt)
		{
			bool flag = evt.target != null;
			if (flag)
			{
				EventDispatchUtilities.PropagateEvent(evt);
			}
		}

		// Token: 0x06000FF0 RID: 4080 RVA: 0x0003EC88 File Offset: 0x0003CE88
		private static void SetBestTargetForEvent(EventBase evt, IPanel panel)
		{
			VisualElement visualElement;
			PointerEventDispatchingStrategy.UpdateElementUnderPointer(evt, panel, out visualElement);
			bool flag = evt.target == null && visualElement != null;
			if (flag)
			{
				evt.propagateToIMGUI = false;
				evt.target = visualElement;
			}
			else
			{
				bool flag2 = evt.target == null && visualElement == null;
				if (flag2)
				{
					bool flag3 = panel != null && panel.contextType == ContextType.Editor && evt.eventTypeId == EventBase<PointerUpEvent>.TypeId();
					if (flag3)
					{
						Panel panel2 = panel as Panel;
						evt.target = ((panel2 != null) ? panel2.rootIMGUIContainer : null);
					}
					else
					{
						evt.target = ((panel != null) ? panel.visualTree : null);
					}
				}
				else
				{
					bool flag4 = evt.target != null;
					if (flag4)
					{
						evt.propagateToIMGUI = false;
					}
				}
			}
		}

		// Token: 0x06000FF1 RID: 4081 RVA: 0x0003ED48 File Offset: 0x0003CF48
		private static void UpdateElementUnderPointer(EventBase evt, IPanel panel, out VisualElement elementUnderPointer)
		{
			IPointerEvent pointerEvent = evt as IPointerEvent;
			BaseVisualElementPanel baseVisualElementPanel = panel as BaseVisualElementPanel;
			IPointerEventInternal pointerEventInternal = evt as IPointerEventInternal;
			elementUnderPointer = ((pointerEventInternal == null || pointerEventInternal.recomputeTopElementUnderPointer) ? ((baseVisualElementPanel != null) ? baseVisualElementPanel.RecomputeTopElementUnderPointer(pointerEvent.pointerId, pointerEvent.position, evt) : null) : ((baseVisualElementPanel != null) ? baseVisualElementPanel.GetTopElementUnderPointer(pointerEvent.pointerId) : null));
		}
	}
}
