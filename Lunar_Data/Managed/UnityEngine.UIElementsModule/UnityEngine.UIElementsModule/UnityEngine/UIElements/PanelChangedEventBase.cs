using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200020A RID: 522
	public abstract class PanelChangedEventBase<T> : EventBase<T>, IPanelChangedEvent where T : PanelChangedEventBase<T>, new()
	{
		// Token: 0x17000360 RID: 864
		// (get) Token: 0x06000FCA RID: 4042 RVA: 0x0003E6C3 File Offset: 0x0003C8C3
		// (set) Token: 0x06000FCB RID: 4043 RVA: 0x0003E6CB File Offset: 0x0003C8CB
		public IPanel originPanel { get; private set; }

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x06000FCC RID: 4044 RVA: 0x0003E6D4 File Offset: 0x0003C8D4
		// (set) Token: 0x06000FCD RID: 4045 RVA: 0x0003E6DC File Offset: 0x0003C8DC
		public IPanel destinationPanel { get; private set; }

		// Token: 0x06000FCE RID: 4046 RVA: 0x0003E6E5 File Offset: 0x0003C8E5
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x06000FCF RID: 4047 RVA: 0x0003E6F6 File Offset: 0x0003C8F6
		private void LocalInit()
		{
			this.originPanel = null;
			this.destinationPanel = null;
		}

		// Token: 0x06000FD0 RID: 4048 RVA: 0x0003E70C File Offset: 0x0003C90C
		public static T GetPooled(IPanel originPanel, IPanel destinationPanel)
		{
			T pooled = EventBase<T>.GetPooled();
			pooled.originPanel = originPanel;
			pooled.destinationPanel = destinationPanel;
			return pooled;
		}

		// Token: 0x06000FD1 RID: 4049 RVA: 0x0003E73F File Offset: 0x0003C93F
		protected PanelChangedEventBase()
		{
			this.LocalInit();
		}
	}
}
