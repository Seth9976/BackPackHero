using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200021E RID: 542
	public sealed class PointerLeaveEvent : PointerEventBase<PointerLeaveEvent>
	{
		// Token: 0x06001067 RID: 4199 RVA: 0x0003FF2C File Offset: 0x0003E12C
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x06001068 RID: 4200 RVA: 0x0003FF0F File Offset: 0x0003E10F
		private void LocalInit()
		{
			base.propagation = EventBase.EventPropagation.TricklesDown | EventBase.EventPropagation.IgnoreCompositeRoots;
		}

		// Token: 0x06001069 RID: 4201 RVA: 0x0003FF3D File Offset: 0x0003E13D
		public PointerLeaveEvent()
		{
			this.LocalInit();
		}
	}
}
