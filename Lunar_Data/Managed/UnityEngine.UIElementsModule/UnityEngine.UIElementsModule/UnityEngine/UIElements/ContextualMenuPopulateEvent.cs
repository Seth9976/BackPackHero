using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001FD RID: 509
	public class ContextualMenuPopulateEvent : MouseEventBase<ContextualMenuPopulateEvent>
	{
		// Token: 0x1700035B RID: 859
		// (get) Token: 0x06000FA7 RID: 4007 RVA: 0x0003DCA4 File Offset: 0x0003BEA4
		// (set) Token: 0x06000FA8 RID: 4008 RVA: 0x0003DCAC File Offset: 0x0003BEAC
		public DropdownMenu menu { get; private set; }

		// Token: 0x1700035C RID: 860
		// (get) Token: 0x06000FA9 RID: 4009 RVA: 0x0003DCB5 File Offset: 0x0003BEB5
		// (set) Token: 0x06000FAA RID: 4010 RVA: 0x0003DCBD File Offset: 0x0003BEBD
		public EventBase triggerEvent { get; private set; }

		// Token: 0x06000FAB RID: 4011 RVA: 0x0003DCC8 File Offset: 0x0003BEC8
		public static ContextualMenuPopulateEvent GetPooled(EventBase triggerEvent, DropdownMenu menu, IEventHandler target, ContextualMenuManager menuManager)
		{
			ContextualMenuPopulateEvent pooled = EventBase<ContextualMenuPopulateEvent>.GetPooled(triggerEvent);
			bool flag = triggerEvent != null;
			if (flag)
			{
				triggerEvent.Acquire();
				pooled.triggerEvent = triggerEvent;
				IMouseEvent mouseEvent = triggerEvent as IMouseEvent;
				bool flag2 = mouseEvent != null;
				if (flag2)
				{
					pooled.modifiers = mouseEvent.modifiers;
					pooled.mousePosition = mouseEvent.mousePosition;
					pooled.localMousePosition = mouseEvent.mousePosition;
					pooled.mouseDelta = mouseEvent.mouseDelta;
					pooled.button = mouseEvent.button;
					pooled.clickCount = mouseEvent.clickCount;
				}
				else
				{
					IPointerEvent pointerEvent = triggerEvent as IPointerEvent;
					bool flag3 = pointerEvent != null;
					if (flag3)
					{
						pooled.modifiers = pointerEvent.modifiers;
						pooled.mousePosition = pointerEvent.position;
						pooled.localMousePosition = pointerEvent.position;
						pooled.mouseDelta = pointerEvent.deltaPosition;
						pooled.button = pointerEvent.button;
						pooled.clickCount = pointerEvent.clickCount;
					}
				}
				IMouseEventInternal mouseEventInternal = triggerEvent as IMouseEventInternal;
				bool flag4 = mouseEventInternal != null;
				if (flag4)
				{
					((IMouseEventInternal)pooled).triggeredByOS = mouseEventInternal.triggeredByOS;
				}
				else
				{
					IPointerEventInternal pointerEventInternal = triggerEvent as IPointerEventInternal;
					bool flag5 = pointerEventInternal != null;
					if (flag5)
					{
						((IMouseEventInternal)pooled).triggeredByOS = pointerEventInternal.triggeredByOS;
					}
				}
			}
			pooled.target = target;
			pooled.menu = menu;
			pooled.m_ContextualMenuManager = menuManager;
			return pooled;
		}

		// Token: 0x06000FAC RID: 4012 RVA: 0x0003DE3B File Offset: 0x0003C03B
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x06000FAD RID: 4013 RVA: 0x0003DE4C File Offset: 0x0003C04C
		private void LocalInit()
		{
			this.menu = null;
			this.m_ContextualMenuManager = null;
			bool flag = this.triggerEvent != null;
			if (flag)
			{
				this.triggerEvent.Dispose();
				this.triggerEvent = null;
			}
		}

		// Token: 0x06000FAE RID: 4014 RVA: 0x0003DE8C File Offset: 0x0003C08C
		public ContextualMenuPopulateEvent()
		{
			this.LocalInit();
		}

		// Token: 0x06000FAF RID: 4015 RVA: 0x0003DEA0 File Offset: 0x0003C0A0
		protected internal override void PostDispatch(IPanel panel)
		{
			bool flag = !base.isDefaultPrevented && this.m_ContextualMenuManager != null;
			if (flag)
			{
				this.menu.PrepareForDisplay(this.triggerEvent);
				this.m_ContextualMenuManager.DoDisplayMenu(this.menu, this.triggerEvent);
			}
			base.PostDispatch(panel);
		}

		// Token: 0x040006E8 RID: 1768
		private ContextualMenuManager m_ContextualMenuManager;
	}
}
