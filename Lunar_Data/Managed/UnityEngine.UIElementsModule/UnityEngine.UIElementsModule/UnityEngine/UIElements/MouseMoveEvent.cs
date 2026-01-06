using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001F4 RID: 500
	public class MouseMoveEvent : MouseEventBase<MouseMoveEvent>
	{
		// Token: 0x06000F88 RID: 3976 RVA: 0x0003D9E0 File Offset: 0x0003BBE0
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x06000F89 RID: 3977 RVA: 0x0003D9F1 File Offset: 0x0003BBF1
		private void LocalInit()
		{
			base.propagation = EventBase.EventPropagation.Bubbles | EventBase.EventPropagation.TricklesDown | EventBase.EventPropagation.Cancellable;
		}

		// Token: 0x06000F8A RID: 3978 RVA: 0x0003D9FC File Offset: 0x0003BBFC
		public MouseMoveEvent()
		{
			this.LocalInit();
		}

		// Token: 0x06000F8B RID: 3979 RVA: 0x0003DA10 File Offset: 0x0003BC10
		public new static MouseMoveEvent GetPooled(Event systemEvent)
		{
			MouseMoveEvent pooled = MouseEventBase<MouseMoveEvent>.GetPooled(systemEvent);
			pooled.button = 0;
			return pooled;
		}

		// Token: 0x06000F8C RID: 3980 RVA: 0x0003DA34 File Offset: 0x0003BC34
		internal static MouseMoveEvent GetPooled(PointerMoveEvent pointerEvent)
		{
			return MouseEventBase<MouseMoveEvent>.GetPooled(pointerEvent);
		}
	}
}
