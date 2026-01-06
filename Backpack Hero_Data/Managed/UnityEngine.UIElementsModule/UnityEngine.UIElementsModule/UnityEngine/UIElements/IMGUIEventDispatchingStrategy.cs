using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001E4 RID: 484
	internal class IMGUIEventDispatchingStrategy : IEventDispatchingStrategy
	{
		// Token: 0x06000F02 RID: 3842 RVA: 0x0003C770 File Offset: 0x0003A970
		public bool CanDispatchEvent(EventBase evt)
		{
			return evt is IMGUIEvent;
		}

		// Token: 0x06000F03 RID: 3843 RVA: 0x0003C78C File Offset: 0x0003A98C
		public void DispatchEvent(EventBase evt, IPanel panel)
		{
			bool flag = panel != null;
			if (flag)
			{
				EventDispatchUtilities.PropagateToIMGUIContainer(panel.visualTree, evt);
			}
			evt.propagateToIMGUI = false;
			evt.stopDispatch = true;
		}
	}
}
