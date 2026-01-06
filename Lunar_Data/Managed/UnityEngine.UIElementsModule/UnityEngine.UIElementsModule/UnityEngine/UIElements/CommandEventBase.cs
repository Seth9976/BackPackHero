using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001C4 RID: 452
	public abstract class CommandEventBase<T> : EventBase<T>, ICommandEvent where T : CommandEventBase<T>, new()
	{
		// Token: 0x170002FA RID: 762
		// (get) Token: 0x06000E2D RID: 3629 RVA: 0x0003A0CC File Offset: 0x000382CC
		// (set) Token: 0x06000E2E RID: 3630 RVA: 0x0003A10B File Offset: 0x0003830B
		public string commandName
		{
			get
			{
				bool flag = this.m_CommandName == null && base.imguiEvent != null;
				string text;
				if (flag)
				{
					text = base.imguiEvent.commandName;
				}
				else
				{
					text = this.m_CommandName;
				}
				return text;
			}
			protected set
			{
				this.m_CommandName = value;
			}
		}

		// Token: 0x06000E2F RID: 3631 RVA: 0x0003A115 File Offset: 0x00038315
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x06000E30 RID: 3632 RVA: 0x0003A126 File Offset: 0x00038326
		private void LocalInit()
		{
			base.propagation = EventBase.EventPropagation.Bubbles | EventBase.EventPropagation.TricklesDown | EventBase.EventPropagation.Cancellable;
			this.commandName = null;
		}

		// Token: 0x06000E31 RID: 3633 RVA: 0x0003A13C File Offset: 0x0003833C
		public static T GetPooled(Event systemEvent)
		{
			T pooled = EventBase<T>.GetPooled();
			pooled.imguiEvent = systemEvent;
			return pooled;
		}

		// Token: 0x06000E32 RID: 3634 RVA: 0x0003A164 File Offset: 0x00038364
		public static T GetPooled(string commandName)
		{
			T pooled = EventBase<T>.GetPooled();
			pooled.commandName = commandName;
			return pooled;
		}

		// Token: 0x06000E33 RID: 3635 RVA: 0x0003A18A File Offset: 0x0003838A
		protected CommandEventBase()
		{
			this.LocalInit();
		}

		// Token: 0x0400067E RID: 1662
		private string m_CommandName;
	}
}
