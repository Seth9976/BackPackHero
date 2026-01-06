using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200021A RID: 538
	public sealed class PointerUpEvent : PointerEventBase<PointerUpEvent>
	{
		// Token: 0x06001058 RID: 4184 RVA: 0x0003FD05 File Offset: 0x0003DF05
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x06001059 RID: 4185 RVA: 0x0003FA88 File Offset: 0x0003DC88
		private void LocalInit()
		{
			base.propagation = EventBase.EventPropagation.Bubbles | EventBase.EventPropagation.TricklesDown | EventBase.EventPropagation.Cancellable | EventBase.EventPropagation.SkipDisabledElements;
			((IPointerEventInternal)this).triggeredByOS = true;
			((IPointerEventInternal)this).recomputeTopElementUnderPointer = true;
		}

		// Token: 0x0600105A RID: 4186 RVA: 0x0003FD16 File Offset: 0x0003DF16
		public PointerUpEvent()
		{
			this.LocalInit();
		}

		// Token: 0x0600105B RID: 4187 RVA: 0x0003FD28 File Offset: 0x0003DF28
		protected internal override void PostDispatch(IPanel panel)
		{
			bool flag = PointerType.IsDirectManipulationDevice(base.pointerType);
			if (flag)
			{
				panel.ReleasePointer(base.pointerId);
				BaseVisualElementPanel baseVisualElementPanel = panel as BaseVisualElementPanel;
				if (baseVisualElementPanel != null)
				{
					baseVisualElementPanel.ClearCachedElementUnderPointer(base.pointerId, this);
				}
			}
			bool flag2 = panel.ShouldSendCompatibilityMouseEvents(this);
			if (flag2)
			{
				using (MouseUpEvent pooled = MouseUpEvent.GetPooled(this))
				{
					pooled.target = base.target;
					pooled.target.SendEvent(pooled);
				}
			}
			base.PostDispatch(panel);
			panel.ActivateCompatibilityMouseEvents(base.pointerId);
		}
	}
}
