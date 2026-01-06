using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001B9 RID: 441
	public abstract class PointerCaptureEventBase<T> : EventBase<T>, IPointerCaptureEvent, IPointerCaptureEventInternal where T : PointerCaptureEventBase<T>, new()
	{
		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x06000E11 RID: 3601 RVA: 0x00039DF8 File Offset: 0x00037FF8
		// (set) Token: 0x06000E12 RID: 3602 RVA: 0x00039E00 File Offset: 0x00038000
		public IEventHandler relatedTarget { get; private set; }

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x06000E13 RID: 3603 RVA: 0x00039E09 File Offset: 0x00038009
		// (set) Token: 0x06000E14 RID: 3604 RVA: 0x00039E11 File Offset: 0x00038011
		public int pointerId { get; private set; }

		// Token: 0x06000E15 RID: 3605 RVA: 0x00039E1A File Offset: 0x0003801A
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x06000E16 RID: 3606 RVA: 0x00039E2B File Offset: 0x0003802B
		private void LocalInit()
		{
			base.propagation = EventBase.EventPropagation.Bubbles | EventBase.EventPropagation.TricklesDown;
			this.relatedTarget = null;
			this.pointerId = PointerId.invalidPointerId;
		}

		// Token: 0x06000E17 RID: 3607 RVA: 0x00039E4C File Offset: 0x0003804C
		public static T GetPooled(IEventHandler target, IEventHandler relatedTarget, int pointerId)
		{
			T pooled = EventBase<T>.GetPooled();
			pooled.target = target;
			pooled.relatedTarget = relatedTarget;
			pooled.pointerId = pointerId;
			return pooled;
		}

		// Token: 0x06000E18 RID: 3608 RVA: 0x00039E8C File Offset: 0x0003808C
		protected PointerCaptureEventBase()
		{
			this.LocalInit();
		}
	}
}
