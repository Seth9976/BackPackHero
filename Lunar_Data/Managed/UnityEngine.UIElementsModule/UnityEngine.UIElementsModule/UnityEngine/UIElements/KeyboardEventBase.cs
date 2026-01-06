using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001E8 RID: 488
	public abstract class KeyboardEventBase<T> : EventBase<T>, IKeyboardEvent where T : KeyboardEventBase<T>, new()
	{
		// Token: 0x17000330 RID: 816
		// (get) Token: 0x06000F18 RID: 3864 RVA: 0x0003C931 File Offset: 0x0003AB31
		// (set) Token: 0x06000F19 RID: 3865 RVA: 0x0003C939 File Offset: 0x0003AB39
		public EventModifiers modifiers { get; protected set; }

		// Token: 0x17000331 RID: 817
		// (get) Token: 0x06000F1A RID: 3866 RVA: 0x0003C942 File Offset: 0x0003AB42
		// (set) Token: 0x06000F1B RID: 3867 RVA: 0x0003C94A File Offset: 0x0003AB4A
		public char character { get; protected set; }

		// Token: 0x17000332 RID: 818
		// (get) Token: 0x06000F1C RID: 3868 RVA: 0x0003C953 File Offset: 0x0003AB53
		// (set) Token: 0x06000F1D RID: 3869 RVA: 0x0003C95B File Offset: 0x0003AB5B
		public KeyCode keyCode { get; protected set; }

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x06000F1E RID: 3870 RVA: 0x0003C964 File Offset: 0x0003AB64
		public bool shiftKey
		{
			get
			{
				return (this.modifiers & EventModifiers.Shift) > EventModifiers.None;
			}
		}

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x06000F1F RID: 3871 RVA: 0x0003C984 File Offset: 0x0003AB84
		public bool ctrlKey
		{
			get
			{
				return (this.modifiers & EventModifiers.Control) > EventModifiers.None;
			}
		}

		// Token: 0x17000335 RID: 821
		// (get) Token: 0x06000F20 RID: 3872 RVA: 0x0003C9A4 File Offset: 0x0003ABA4
		public bool commandKey
		{
			get
			{
				return (this.modifiers & EventModifiers.Command) > EventModifiers.None;
			}
		}

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x06000F21 RID: 3873 RVA: 0x0003C9C4 File Offset: 0x0003ABC4
		public bool altKey
		{
			get
			{
				return (this.modifiers & EventModifiers.Alt) > EventModifiers.None;
			}
		}

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x06000F22 RID: 3874 RVA: 0x0003C9E4 File Offset: 0x0003ABE4
		public bool actionKey
		{
			get
			{
				bool flag = Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.OSXPlayer;
				bool flag2;
				if (flag)
				{
					flag2 = this.commandKey;
				}
				else
				{
					flag2 = this.ctrlKey;
				}
				return flag2;
			}
		}

		// Token: 0x06000F23 RID: 3875 RVA: 0x0003CA1D File Offset: 0x0003AC1D
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x06000F24 RID: 3876 RVA: 0x0003CA2E File Offset: 0x0003AC2E
		private void LocalInit()
		{
			base.propagation = EventBase.EventPropagation.Bubbles | EventBase.EventPropagation.TricklesDown | EventBase.EventPropagation.Cancellable | EventBase.EventPropagation.SkipDisabledElements;
			this.modifiers = EventModifiers.None;
			this.character = '\0';
			this.keyCode = KeyCode.None;
		}

		// Token: 0x06000F25 RID: 3877 RVA: 0x0003CA54 File Offset: 0x0003AC54
		public static T GetPooled(char c, KeyCode keyCode, EventModifiers modifiers)
		{
			T pooled = EventBase<T>.GetPooled();
			pooled.modifiers = modifiers;
			pooled.character = c;
			pooled.keyCode = keyCode;
			return pooled;
		}

		// Token: 0x06000F26 RID: 3878 RVA: 0x0003CA94 File Offset: 0x0003AC94
		public static T GetPooled(Event systemEvent)
		{
			T pooled = EventBase<T>.GetPooled();
			pooled.imguiEvent = systemEvent;
			bool flag = systemEvent != null;
			if (flag)
			{
				pooled.modifiers = systemEvent.modifiers;
				pooled.character = systemEvent.character;
				pooled.keyCode = systemEvent.keyCode;
			}
			return pooled;
		}

		// Token: 0x06000F27 RID: 3879 RVA: 0x0003CAFA File Offset: 0x0003ACFA
		protected KeyboardEventBase()
		{
			this.LocalInit();
		}
	}
}
