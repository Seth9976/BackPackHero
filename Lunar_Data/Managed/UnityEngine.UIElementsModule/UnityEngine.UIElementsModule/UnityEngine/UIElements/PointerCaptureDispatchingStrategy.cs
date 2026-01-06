using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200020D RID: 525
	internal class PointerCaptureDispatchingStrategy : IEventDispatchingStrategy
	{
		// Token: 0x06000FD4 RID: 4052 RVA: 0x0003E764 File Offset: 0x0003C964
		public bool CanDispatchEvent(EventBase evt)
		{
			return evt is IPointerEvent;
		}

		// Token: 0x06000FD5 RID: 4053 RVA: 0x0003E780 File Offset: 0x0003C980
		public void DispatchEvent(EventBase evt, IPanel panel)
		{
			IPointerEvent pointerEvent = evt as IPointerEvent;
			bool flag = pointerEvent == null;
			if (!flag)
			{
				IEventHandler capturingElement = panel.GetCapturingElement(pointerEvent.pointerId);
				bool flag2 = capturingElement == null;
				if (!flag2)
				{
					VisualElement visualElement = capturingElement as VisualElement;
					bool flag3 = evt.eventTypeId != EventBase<PointerCaptureOutEvent>.TypeId() && visualElement != null && visualElement.panel == null;
					if (flag3)
					{
						panel.ReleasePointer(pointerEvent.pointerId);
					}
					else
					{
						bool flag4 = evt.target != null && evt.target != capturingElement;
						if (!flag4)
						{
							bool flag5 = panel != null && visualElement != null && visualElement.panel != panel;
							if (!flag5)
							{
								bool flag6 = evt.eventTypeId != EventBase<PointerCaptureEvent>.TypeId() && evt.eventTypeId != EventBase<PointerCaptureOutEvent>.TypeId();
								if (flag6)
								{
									panel.ProcessPointerCapture(pointerEvent.pointerId);
								}
								BaseVisualElementPanel baseVisualElementPanel = panel as BaseVisualElementPanel;
								bool flag7 = baseVisualElementPanel != null;
								if (flag7)
								{
									IPointerEventInternal pointerEventInternal = pointerEvent as IPointerEventInternal;
									bool flag8 = pointerEventInternal == null || pointerEventInternal.recomputeTopElementUnderPointer;
									bool flag9 = flag8;
									if (flag9)
									{
										baseVisualElementPanel.RecomputeTopElementUnderPointer(pointerEvent.pointerId, pointerEvent.position, evt);
									}
								}
								evt.dispatch = true;
								evt.target = capturingElement;
								evt.currentTarget = capturingElement;
								evt.propagationPhase = PropagationPhase.AtTarget;
								evt.skipDisabledElements = false;
								capturingElement.HandleEvent(evt);
								evt.currentTarget = null;
								evt.propagationPhase = PropagationPhase.None;
								evt.dispatch = false;
								evt.stopDispatch = true;
								evt.propagateToIMGUI = false;
							}
						}
					}
				}
			}
		}
	}
}
