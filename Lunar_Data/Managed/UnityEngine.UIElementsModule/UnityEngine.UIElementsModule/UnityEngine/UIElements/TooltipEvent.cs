using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000224 RID: 548
	public class TooltipEvent : EventBase<TooltipEvent>
	{
		// Token: 0x1700039A RID: 922
		// (get) Token: 0x06001074 RID: 4212 RVA: 0x0004017E File Offset: 0x0003E37E
		// (set) Token: 0x06001075 RID: 4213 RVA: 0x00040186 File Offset: 0x0003E386
		public string tooltip { get; set; }

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x06001076 RID: 4214 RVA: 0x0004018F File Offset: 0x0003E38F
		// (set) Token: 0x06001077 RID: 4215 RVA: 0x00040197 File Offset: 0x0003E397
		public Rect rect { get; set; }

		// Token: 0x06001078 RID: 4216 RVA: 0x000401A0 File Offset: 0x0003E3A0
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x06001079 RID: 4217 RVA: 0x000401B4 File Offset: 0x0003E3B4
		private void LocalInit()
		{
			base.propagation = EventBase.EventPropagation.Bubbles | EventBase.EventPropagation.TricklesDown;
			this.rect = default(Rect);
			this.tooltip = string.Empty;
			base.ignoreCompositeRoots = true;
		}

		// Token: 0x0600107A RID: 4218 RVA: 0x000401F0 File Offset: 0x0003E3F0
		internal static TooltipEvent GetPooled(string tooltip, Rect rect)
		{
			TooltipEvent pooled = EventBase<TooltipEvent>.GetPooled();
			pooled.tooltip = tooltip;
			pooled.rect = rect;
			return pooled;
		}

		// Token: 0x0600107B RID: 4219 RVA: 0x00040219 File Offset: 0x0003E419
		public TooltipEvent()
		{
			this.LocalInit();
		}
	}
}
