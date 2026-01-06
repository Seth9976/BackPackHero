using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001D2 RID: 466
	internal class EventCallbackFunctor<TEventType, TCallbackArgs> : EventCallbackFunctorBase where TEventType : EventBase<TEventType>, new()
	{
		// Token: 0x1700031B RID: 795
		// (get) Token: 0x06000EA9 RID: 3753 RVA: 0x0003B51D File Offset: 0x0003971D
		// (set) Token: 0x06000EAA RID: 3754 RVA: 0x0003B525 File Offset: 0x00039725
		internal TCallbackArgs userArgs { get; set; }

		// Token: 0x06000EAB RID: 3755 RVA: 0x0003B52E File Offset: 0x0003972E
		public EventCallbackFunctor(EventCallback<TEventType, TCallbackArgs> callback, TCallbackArgs userArgs, CallbackPhase phase, InvokePolicy invokePolicy)
			: base(phase, invokePolicy)
		{
			this.userArgs = userArgs;
			this.m_Callback = callback;
			this.m_EventTypeId = EventBase<TEventType>.TypeId();
		}

		// Token: 0x06000EAC RID: 3756 RVA: 0x0003B558 File Offset: 0x00039758
		public override void Invoke(EventBase evt, PropagationPhase propagationPhase)
		{
			bool flag = evt == null;
			if (flag)
			{
				throw new ArgumentNullException("evt");
			}
			bool flag2 = evt.eventTypeId != this.m_EventTypeId;
			if (!flag2)
			{
				bool flag3 = base.PhaseMatches(propagationPhase);
				if (flag3)
				{
					using (new EventDebuggerLogCall(this.m_Callback, evt))
					{
						this.m_Callback(evt as TEventType, this.userArgs);
					}
				}
			}
		}

		// Token: 0x06000EAD RID: 3757 RVA: 0x0003B5EC File Offset: 0x000397EC
		public override bool IsEquivalentTo(long eventTypeId, Delegate callback, CallbackPhase phase)
		{
			return this.m_EventTypeId == eventTypeId && this.m_Callback == callback && base.phase == phase;
		}

		// Token: 0x040006AF RID: 1711
		private readonly EventCallback<TEventType, TCallbackArgs> m_Callback;

		// Token: 0x040006B0 RID: 1712
		private readonly long m_EventTypeId;
	}
}
