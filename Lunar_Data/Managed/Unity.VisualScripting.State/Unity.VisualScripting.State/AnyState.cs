using System;

namespace Unity.VisualScripting
{
	// Token: 0x02000002 RID: 2
	public sealed class AnyState : State
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		[DoNotSerialize]
		public override bool canBeDestination
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002053 File Offset: 0x00000253
		public AnyState()
		{
			base.isStart = true;
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002062 File Offset: 0x00000262
		public override void OnExit(Flow flow, StateExitReason reason)
		{
			if (reason == StateExitReason.Branch)
			{
				return;
			}
			base.OnExit(flow, reason);
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002074 File Offset: 0x00000274
		public override void OnBranchTo(Flow flow, IState destination)
		{
			foreach (IStateTransition stateTransition in base.outgoingTransitionsNoAlloc)
			{
				if (stateTransition.destination != destination)
				{
					stateTransition.destination.OnExit(flow, StateExitReason.AnyBranch);
				}
			}
		}
	}
}
