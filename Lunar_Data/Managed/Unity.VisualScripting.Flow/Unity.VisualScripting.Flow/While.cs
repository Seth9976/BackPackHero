using System;
using System.Collections;

namespace Unity.VisualScripting
{
	// Token: 0x0200004C RID: 76
	[UnitTitle("While Loop")]
	[UnitCategory("Control")]
	[UnitOrder(11)]
	public class While : LoopUnit
	{
		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000316 RID: 790 RVA: 0x000084AC File Offset: 0x000066AC
		// (set) Token: 0x06000317 RID: 791 RVA: 0x000084B4 File Offset: 0x000066B4
		[DoNotSerialize]
		[PortLabelHidden]
		public ValueInput condition { get; private set; }

		// Token: 0x06000318 RID: 792 RVA: 0x000084BD File Offset: 0x000066BD
		protected override void Definition()
		{
			base.Definition();
			this.condition = base.ValueInput<bool>("condition");
			base.Requirement(this.condition, base.enter);
		}

		// Token: 0x06000319 RID: 793 RVA: 0x000084E8 File Offset: 0x000066E8
		private int Start(Flow flow)
		{
			return flow.EnterLoop();
		}

		// Token: 0x0600031A RID: 794 RVA: 0x000084F0 File Offset: 0x000066F0
		private bool CanMoveNext(Flow flow)
		{
			return flow.GetValue<bool>(this.condition);
		}

		// Token: 0x0600031B RID: 795 RVA: 0x00008500 File Offset: 0x00006700
		protected override ControlOutput Loop(Flow flow)
		{
			int num = this.Start(flow);
			GraphStack graphStack = flow.PreserveStack();
			while (flow.LoopIsNotBroken(num) && this.CanMoveNext(flow))
			{
				flow.Invoke(base.body);
				flow.RestoreStack(graphStack);
			}
			flow.DisposePreservedStack(graphStack);
			flow.ExitLoop(num);
			return base.exit;
		}

		// Token: 0x0600031C RID: 796 RVA: 0x00008557 File Offset: 0x00006757
		protected override IEnumerator LoopCoroutine(Flow flow)
		{
			int loop = this.Start(flow);
			GraphStack stack = flow.PreserveStack();
			while (flow.LoopIsNotBroken(loop) && this.CanMoveNext(flow))
			{
				yield return base.body;
				flow.RestoreStack(stack);
			}
			flow.DisposePreservedStack(stack);
			flow.ExitLoop(loop);
			yield return base.exit;
			yield break;
		}
	}
}
