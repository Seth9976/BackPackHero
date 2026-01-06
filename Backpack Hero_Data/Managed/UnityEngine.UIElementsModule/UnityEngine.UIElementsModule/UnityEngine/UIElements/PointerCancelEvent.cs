using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200021B RID: 539
	public sealed class PointerCancelEvent : PointerEventBase<PointerCancelEvent>
	{
		// Token: 0x0600105C RID: 4188 RVA: 0x0003FDD0 File Offset: 0x0003DFD0
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x0600105D RID: 4189 RVA: 0x0003FDE1 File Offset: 0x0003DFE1
		private void LocalInit()
		{
			base.propagation = EventBase.EventPropagation.Bubbles | EventBase.EventPropagation.TricklesDown | EventBase.EventPropagation.SkipDisabledElements;
			((IPointerEventInternal)this).triggeredByOS = true;
			((IPointerEventInternal)this).recomputeTopElementUnderPointer = true;
		}

		// Token: 0x0600105E RID: 4190 RVA: 0x0003FDFD File Offset: 0x0003DFFD
		public PointerCancelEvent()
		{
			this.LocalInit();
		}

		// Token: 0x0600105F RID: 4191 RVA: 0x0003FE10 File Offset: 0x0003E010
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
					base.target.SendEvent(pooled);
				}
			}
			base.PostDispatch(panel);
			panel.ActivateCompatibilityMouseEvents(base.pointerId);
		}
	}
}
