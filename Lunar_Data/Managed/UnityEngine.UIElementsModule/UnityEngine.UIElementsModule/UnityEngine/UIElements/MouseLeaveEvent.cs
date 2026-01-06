using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001F8 RID: 504
	public class MouseLeaveEvent : MouseEventBase<MouseLeaveEvent>
	{
		// Token: 0x06000F99 RID: 3993 RVA: 0x0003DB62 File Offset: 0x0003BD62
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x06000F9A RID: 3994 RVA: 0x0003DB45 File Offset: 0x0003BD45
		private void LocalInit()
		{
			base.propagation = EventBase.EventPropagation.TricklesDown | EventBase.EventPropagation.Cancellable | EventBase.EventPropagation.IgnoreCompositeRoots;
		}

		// Token: 0x06000F9B RID: 3995 RVA: 0x0003DB73 File Offset: 0x0003BD73
		public MouseLeaveEvent()
		{
			this.LocalInit();
		}
	}
}
