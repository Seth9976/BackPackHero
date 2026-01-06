using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001D0 RID: 464
	internal abstract class EventCallbackFunctorBase
	{
		// Token: 0x17000319 RID: 793
		// (get) Token: 0x06000EA0 RID: 3744 RVA: 0x0003B3B7 File Offset: 0x000395B7
		public CallbackPhase phase { get; }

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x06000EA1 RID: 3745 RVA: 0x0003B3BF File Offset: 0x000395BF
		public InvokePolicy invokePolicy { get; }

		// Token: 0x06000EA2 RID: 3746 RVA: 0x0003B3C7 File Offset: 0x000395C7
		protected EventCallbackFunctorBase(CallbackPhase phase, InvokePolicy invokePolicy)
		{
			this.phase = phase;
			this.invokePolicy = invokePolicy;
		}

		// Token: 0x06000EA3 RID: 3747
		public abstract void Invoke(EventBase evt, PropagationPhase propagationPhase);

		// Token: 0x06000EA4 RID: 3748
		public abstract bool IsEquivalentTo(long eventTypeId, Delegate callback, CallbackPhase phase);

		// Token: 0x06000EA5 RID: 3749 RVA: 0x0003B3E0 File Offset: 0x000395E0
		protected bool PhaseMatches(PropagationPhase propagationPhase)
		{
			CallbackPhase phase = this.phase;
			CallbackPhase callbackPhase = phase;
			if (callbackPhase != CallbackPhase.TargetAndBubbleUp)
			{
				if (callbackPhase == CallbackPhase.TrickleDownAndTarget)
				{
					bool flag = propagationPhase != PropagationPhase.TrickleDown && propagationPhase != PropagationPhase.AtTarget;
					if (flag)
					{
						return false;
					}
				}
			}
			else
			{
				bool flag2 = propagationPhase != PropagationPhase.AtTarget && propagationPhase != PropagationPhase.BubbleUp;
				if (flag2)
				{
					return false;
				}
			}
			return true;
		}
	}
}
