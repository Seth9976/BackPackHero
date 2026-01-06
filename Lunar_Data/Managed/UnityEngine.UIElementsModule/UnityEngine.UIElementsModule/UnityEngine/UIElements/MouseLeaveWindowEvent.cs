using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001FA RID: 506
	public class MouseLeaveWindowEvent : MouseEventBase<MouseLeaveWindowEvent>
	{
		// Token: 0x06000FA0 RID: 4000 RVA: 0x0003DBF2 File Offset: 0x0003BDF2
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x06000FA1 RID: 4001 RVA: 0x0003DC03 File Offset: 0x0003BE03
		private void LocalInit()
		{
			base.propagation = EventBase.EventPropagation.Cancellable;
			((IMouseEventInternal)this).recomputeTopElementUnderMouse = false;
		}

		// Token: 0x06000FA2 RID: 4002 RVA: 0x0003DC16 File Offset: 0x0003BE16
		public MouseLeaveWindowEvent()
		{
			this.LocalInit();
		}

		// Token: 0x06000FA3 RID: 4003 RVA: 0x0003DC28 File Offset: 0x0003BE28
		public new static MouseLeaveWindowEvent GetPooled(Event systemEvent)
		{
			bool flag = systemEvent != null;
			if (flag)
			{
				PointerDeviceState.ReleaseAllButtons(PointerId.mousePointerId);
			}
			return MouseEventBase<MouseLeaveWindowEvent>.GetPooled(systemEvent);
		}

		// Token: 0x06000FA4 RID: 4004 RVA: 0x0003DC54 File Offset: 0x0003BE54
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
