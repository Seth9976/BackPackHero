using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200021D RID: 541
	public sealed class PointerEnterEvent : PointerEventBase<PointerEnterEvent>
	{
		// Token: 0x06001064 RID: 4196 RVA: 0x0003FEFE File Offset: 0x0003E0FE
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x06001065 RID: 4197 RVA: 0x0003FF0F File Offset: 0x0003E10F
		private void LocalInit()
		{
			base.propagation = EventBase.EventPropagation.TricklesDown | EventBase.EventPropagation.IgnoreCompositeRoots;
		}

		// Token: 0x06001066 RID: 4198 RVA: 0x0003FF1B File Offset: 0x0003E11B
		public PointerEnterEvent()
		{
			this.LocalInit();
		}
	}
}
