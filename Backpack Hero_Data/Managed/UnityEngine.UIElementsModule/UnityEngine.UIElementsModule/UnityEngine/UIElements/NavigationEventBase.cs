using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000202 RID: 514
	public abstract class NavigationEventBase<T> : EventBase<T>, INavigationEvent where T : NavigationEventBase<T>, new()
	{
		// Token: 0x06000FB7 RID: 4023 RVA: 0x0003E4FB File Offset: 0x0003C6FB
		protected NavigationEventBase()
		{
			this.LocalInit();
		}

		// Token: 0x06000FB8 RID: 4024 RVA: 0x0003E50C File Offset: 0x0003C70C
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x06000FB9 RID: 4025 RVA: 0x0003E51D File Offset: 0x0003C71D
		private void LocalInit()
		{
			base.propagation = EventBase.EventPropagation.Bubbles | EventBase.EventPropagation.TricklesDown | EventBase.EventPropagation.Cancellable | EventBase.EventPropagation.SkipDisabledElements;
			base.propagateToIMGUI = false;
		}
	}
}
