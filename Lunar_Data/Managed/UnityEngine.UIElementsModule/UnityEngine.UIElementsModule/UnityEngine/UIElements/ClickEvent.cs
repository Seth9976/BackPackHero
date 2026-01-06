using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200021C RID: 540
	public sealed class ClickEvent : PointerEventBase<ClickEvent>
	{
		// Token: 0x06001060 RID: 4192 RVA: 0x0003FEB8 File Offset: 0x0003E0B8
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x06001061 RID: 4193 RVA: 0x0003D837 File Offset: 0x0003BA37
		private void LocalInit()
		{
			base.propagation = EventBase.EventPropagation.Bubbles | EventBase.EventPropagation.TricklesDown | EventBase.EventPropagation.Cancellable | EventBase.EventPropagation.SkipDisabledElements;
		}

		// Token: 0x06001062 RID: 4194 RVA: 0x0003FEC9 File Offset: 0x0003E0C9
		public ClickEvent()
		{
			this.LocalInit();
		}

		// Token: 0x06001063 RID: 4195 RVA: 0x0003FEDC File Offset: 0x0003E0DC
		internal static ClickEvent GetPooled(PointerUpEvent pointerEvent, int clickCount)
		{
			ClickEvent pooled = PointerEventBase<ClickEvent>.GetPooled(pointerEvent);
			pooled.clickCount = clickCount;
			return pooled;
		}
	}
}
