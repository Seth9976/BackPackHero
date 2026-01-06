using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001D1 RID: 465
	internal class EventCallbackFunctor<TEventType> : EventCallbackFunctorBase where TEventType : EventBase<TEventType>, new()
	{
		// Token: 0x06000EA6 RID: 3750 RVA: 0x0003B43A File Offset: 0x0003963A
		public EventCallbackFunctor(EventCallback<TEventType> callback, CallbackPhase phase, InvokePolicy invokePolicy = InvokePolicy.Default)
			: base(phase, invokePolicy)
		{
			this.m_Callback = callback;
			this.m_EventTypeId = EventBase<TEventType>.TypeId();
		}

		// Token: 0x06000EA7 RID: 3751 RVA: 0x0003B458 File Offset: 0x00039658
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
						this.m_Callback(evt as TEventType);
					}
				}
			}
		}

		// Token: 0x06000EA8 RID: 3752 RVA: 0x0003B4E8 File Offset: 0x000396E8
		public override bool IsEquivalentTo(long eventTypeId, Delegate callback, CallbackPhase phase)
		{
			return this.m_EventTypeId == eventTypeId && this.m_Callback == callback && base.phase == phase;
		}

		// Token: 0x040006AD RID: 1709
		private readonly EventCallback<TEventType> m_Callback;

		// Token: 0x040006AE RID: 1710
		private readonly long m_EventTypeId;
	}
}
