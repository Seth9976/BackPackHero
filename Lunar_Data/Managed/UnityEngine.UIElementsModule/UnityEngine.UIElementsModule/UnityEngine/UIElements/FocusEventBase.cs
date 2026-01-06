using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001DC RID: 476
	public abstract class FocusEventBase<T> : EventBase<T>, IFocusEvent where T : FocusEventBase<T>, new()
	{
		// Token: 0x17000322 RID: 802
		// (get) Token: 0x06000EE7 RID: 3815 RVA: 0x0003C1A0 File Offset: 0x0003A3A0
		// (set) Token: 0x06000EE8 RID: 3816 RVA: 0x0003C1A8 File Offset: 0x0003A3A8
		public Focusable relatedTarget { get; private set; }

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x06000EE9 RID: 3817 RVA: 0x0003C1B1 File Offset: 0x0003A3B1
		// (set) Token: 0x06000EEA RID: 3818 RVA: 0x0003C1B9 File Offset: 0x0003A3B9
		public FocusChangeDirection direction { get; private set; }

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x06000EEB RID: 3819 RVA: 0x0003C1C2 File Offset: 0x0003A3C2
		// (set) Token: 0x06000EEC RID: 3820 RVA: 0x0003C1CA File Offset: 0x0003A3CA
		private protected FocusController focusController { protected get; private set; }

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x06000EED RID: 3821 RVA: 0x0003C1D3 File Offset: 0x0003A3D3
		// (set) Token: 0x06000EEE RID: 3822 RVA: 0x0003C1DB File Offset: 0x0003A3DB
		internal bool IsFocusDelegated { get; private set; }

		// Token: 0x06000EEF RID: 3823 RVA: 0x0003C1E4 File Offset: 0x0003A3E4
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x06000EF0 RID: 3824 RVA: 0x0003C1F5 File Offset: 0x0003A3F5
		private void LocalInit()
		{
			base.propagation = EventBase.EventPropagation.TricklesDown;
			this.relatedTarget = null;
			this.direction = FocusChangeDirection.unspecified;
			this.focusController = null;
		}

		// Token: 0x06000EF1 RID: 3825 RVA: 0x0003C21C File Offset: 0x0003A41C
		public static T GetPooled(IEventHandler target, Focusable relatedTarget, FocusChangeDirection direction, FocusController focusController, bool bIsFocusDelegated = false)
		{
			T pooled = EventBase<T>.GetPooled();
			pooled.target = target;
			pooled.relatedTarget = relatedTarget;
			pooled.direction = direction;
			pooled.focusController = focusController;
			pooled.IsFocusDelegated = bIsFocusDelegated;
			return pooled;
		}

		// Token: 0x06000EF2 RID: 3826 RVA: 0x0003C277 File Offset: 0x0003A477
		protected FocusEventBase()
		{
			this.LocalInit();
		}
	}
}
