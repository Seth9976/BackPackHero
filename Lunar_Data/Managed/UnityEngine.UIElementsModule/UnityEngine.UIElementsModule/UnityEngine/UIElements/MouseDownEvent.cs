using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001F2 RID: 498
	public class MouseDownEvent : MouseEventBase<MouseDownEvent>
	{
		// Token: 0x06000F79 RID: 3961 RVA: 0x0003D826 File Offset: 0x0003BA26
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x06000F7A RID: 3962 RVA: 0x0003D837 File Offset: 0x0003BA37
		private void LocalInit()
		{
			base.propagation = EventBase.EventPropagation.Bubbles | EventBase.EventPropagation.TricklesDown | EventBase.EventPropagation.Cancellable | EventBase.EventPropagation.SkipDisabledElements;
		}

		// Token: 0x06000F7B RID: 3963 RVA: 0x0003D843 File Offset: 0x0003BA43
		public MouseDownEvent()
		{
			this.LocalInit();
		}

		// Token: 0x06000F7C RID: 3964 RVA: 0x0003D854 File Offset: 0x0003BA54
		public new static MouseDownEvent GetPooled(Event systemEvent)
		{
			bool flag = systemEvent != null;
			if (flag)
			{
				PointerDeviceState.PressButton(PointerId.mousePointerId, systemEvent.button);
			}
			return MouseEventBase<MouseDownEvent>.GetPooled(systemEvent);
		}

		// Token: 0x06000F7D RID: 3965 RVA: 0x0003D888 File Offset: 0x0003BA88
		private static MouseDownEvent MakeFromPointerEvent(IPointerEvent pointerEvent)
		{
			bool flag = pointerEvent != null && pointerEvent.button >= 0;
			if (flag)
			{
				PointerDeviceState.PressButton(PointerId.mousePointerId, pointerEvent.button);
			}
			return MouseEventBase<MouseDownEvent>.GetPooled(pointerEvent);
		}

		// Token: 0x06000F7E RID: 3966 RVA: 0x0003D8CC File Offset: 0x0003BACC
		internal static MouseDownEvent GetPooled(PointerDownEvent pointerEvent)
		{
			return MouseDownEvent.MakeFromPointerEvent(pointerEvent);
		}

		// Token: 0x06000F7F RID: 3967 RVA: 0x0003D8E4 File Offset: 0x0003BAE4
		internal static MouseDownEvent GetPooled(PointerMoveEvent pointerEvent)
		{
			return MouseDownEvent.MakeFromPointerEvent(pointerEvent);
		}
	}
}
