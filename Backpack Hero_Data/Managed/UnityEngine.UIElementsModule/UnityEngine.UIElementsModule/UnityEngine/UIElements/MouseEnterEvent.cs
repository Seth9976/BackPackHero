using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001F7 RID: 503
	public class MouseEnterEvent : MouseEventBase<MouseEnterEvent>
	{
		// Token: 0x06000F96 RID: 3990 RVA: 0x0003DB34 File Offset: 0x0003BD34
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x06000F97 RID: 3991 RVA: 0x0003DB45 File Offset: 0x0003BD45
		private void LocalInit()
		{
			base.propagation = EventBase.EventPropagation.TricklesDown | EventBase.EventPropagation.Cancellable | EventBase.EventPropagation.IgnoreCompositeRoots;
		}

		// Token: 0x06000F98 RID: 3992 RVA: 0x0003DB51 File Offset: 0x0003BD51
		public MouseEnterEvent()
		{
			this.LocalInit();
		}
	}
}
