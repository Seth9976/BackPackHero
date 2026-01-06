using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001BD RID: 445
	public abstract class MouseCaptureEventBase<T> : PointerCaptureEventBase<T>, IMouseCaptureEvent where T : MouseCaptureEventBase<T>, new()
	{
		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x06000E1B RID: 3611 RVA: 0x00039EAF File Offset: 0x000380AF
		public new IEventHandler relatedTarget
		{
			get
			{
				return base.relatedTarget;
			}
		}

		// Token: 0x06000E1C RID: 3612 RVA: 0x00039EB8 File Offset: 0x000380B8
		public static T GetPooled(IEventHandler target, IEventHandler relatedTarget)
		{
			return PointerCaptureEventBase<T>.GetPooled(target, relatedTarget, 0);
		}

		// Token: 0x06000E1D RID: 3613 RVA: 0x00039ED4 File Offset: 0x000380D4
		protected override void Init()
		{
			base.Init();
		}
	}
}
