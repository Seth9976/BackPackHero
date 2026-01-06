using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001C8 RID: 456
	internal class DefaultDispatchingStrategy : IEventDispatchingStrategy
	{
		// Token: 0x06000E3A RID: 3642 RVA: 0x0003A1C4 File Offset: 0x000383C4
		public bool CanDispatchEvent(EventBase evt)
		{
			return !(evt is IMGUIEvent);
		}

		// Token: 0x06000E3B RID: 3643 RVA: 0x0003A1E4 File Offset: 0x000383E4
		public void DispatchEvent(EventBase evt, IPanel panel)
		{
			bool flag = evt.target != null;
			if (flag)
			{
				evt.propagateToIMGUI = evt.target is IMGUIContainer;
				EventDispatchUtilities.PropagateEvent(evt);
			}
			else
			{
				bool flag2 = !evt.isPropagationStopped && panel != null;
				if (flag2)
				{
					bool flag3 = evt.propagateToIMGUI || evt.eventTypeId == EventBase<MouseEnterWindowEvent>.TypeId() || evt.eventTypeId == EventBase<MouseLeaveWindowEvent>.TypeId();
					if (flag3)
					{
						EventDispatchUtilities.PropagateToIMGUIContainer(panel.visualTree, evt);
					}
				}
			}
			evt.stopDispatch = true;
		}
	}
}
