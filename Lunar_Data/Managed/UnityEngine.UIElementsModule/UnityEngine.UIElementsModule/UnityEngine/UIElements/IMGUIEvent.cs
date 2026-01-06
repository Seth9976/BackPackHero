using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200022D RID: 557
	public class IMGUIEvent : EventBase<IMGUIEvent>
	{
		// Token: 0x06001095 RID: 4245 RVA: 0x0004043C File Offset: 0x0003E63C
		public static IMGUIEvent GetPooled(Event systemEvent)
		{
			IMGUIEvent pooled = EventBase<IMGUIEvent>.GetPooled();
			pooled.imguiEvent = systemEvent;
			return pooled;
		}

		// Token: 0x06001096 RID: 4246 RVA: 0x0004045D File Offset: 0x0003E65D
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x06001097 RID: 4247 RVA: 0x0003D9F1 File Offset: 0x0003BBF1
		private void LocalInit()
		{
			base.propagation = EventBase.EventPropagation.Bubbles | EventBase.EventPropagation.TricklesDown | EventBase.EventPropagation.Cancellable;
		}

		// Token: 0x06001098 RID: 4248 RVA: 0x0004046E File Offset: 0x0003E66E
		public IMGUIEvent()
		{
			this.LocalInit();
		}
	}
}
