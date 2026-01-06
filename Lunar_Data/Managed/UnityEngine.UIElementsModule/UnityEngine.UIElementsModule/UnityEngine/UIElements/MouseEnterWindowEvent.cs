using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001F9 RID: 505
	public class MouseEnterWindowEvent : MouseEventBase<MouseEnterWindowEvent>
	{
		// Token: 0x06000F9C RID: 3996 RVA: 0x0003DB84 File Offset: 0x0003BD84
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x06000F9D RID: 3997 RVA: 0x0003DB95 File Offset: 0x0003BD95
		private void LocalInit()
		{
			base.propagation = EventBase.EventPropagation.Cancellable;
		}

		// Token: 0x06000F9E RID: 3998 RVA: 0x0003DBA0 File Offset: 0x0003BDA0
		public MouseEnterWindowEvent()
		{
			this.LocalInit();
		}

		// Token: 0x06000F9F RID: 3999 RVA: 0x0003DBB4 File Offset: 0x0003BDB4
		protected internal override void PostDispatch(IPanel panel)
		{
			EventBase eventBase = ((IMouseEventInternal)this).sourcePointerEvent as EventBase;
			bool flag = eventBase == null;
			if (flag)
			{
				BaseVisualElementPanel baseVisualElementPanel = panel as BaseVisualElementPanel;
				if (baseVisualElementPanel != null)
				{
					baseVisualElementPanel.CommitElementUnderPointers();
				}
			}
			base.PostDispatch(panel);
		}
	}
}
