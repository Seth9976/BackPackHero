using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000012 RID: 18
	public abstract class ContextualMenuManager
	{
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000078 RID: 120 RVA: 0x00003BD5 File Offset: 0x00001DD5
		// (set) Token: 0x06000079 RID: 121 RVA: 0x00003BDD File Offset: 0x00001DDD
		internal bool displayMenuHandledOSX { get; set; }

		// Token: 0x0600007A RID: 122
		public abstract void DisplayMenuIfEventMatches(EventBase evt, IEventHandler eventHandler);

		// Token: 0x0600007B RID: 123 RVA: 0x00003BE8 File Offset: 0x00001DE8
		public void DisplayMenu(EventBase triggerEvent, IEventHandler target)
		{
			DropdownMenu dropdownMenu = new DropdownMenu();
			using (ContextualMenuPopulateEvent pooled = ContextualMenuPopulateEvent.GetPooled(triggerEvent, dropdownMenu, target, this))
			{
				if (target != null)
				{
					target.SendEvent(pooled);
				}
			}
			bool flag = Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.OSXPlayer;
			if (flag)
			{
				this.displayMenuHandledOSX = true;
			}
		}

		// Token: 0x0600007C RID: 124
		protected internal abstract void DoDisplayMenu(DropdownMenu menu, EventBase triggerEvent);
	}
}
