using System;
using UnityEngine.AI;

namespace Unity.VisualScripting
{
	// Token: 0x02000090 RID: 144
	[UnitCategory("Events/Navigation")]
	public sealed class OnDestinationReached : MachineEventUnit<EmptyEventArgs>
	{
		// Token: 0x1700019A RID: 410
		// (get) Token: 0x06000445 RID: 1093 RVA: 0x00009715 File Offset: 0x00007915
		protected override string hookName
		{
			get
			{
				return "Update";
			}
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x06000446 RID: 1094 RVA: 0x0000971C File Offset: 0x0000791C
		// (set) Token: 0x06000447 RID: 1095 RVA: 0x00009724 File Offset: 0x00007924
		[DoNotSerialize]
		public ValueInput threshold { get; private set; }

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x06000448 RID: 1096 RVA: 0x0000972D File Offset: 0x0000792D
		// (set) Token: 0x06000449 RID: 1097 RVA: 0x00009735 File Offset: 0x00007935
		[DoNotSerialize]
		public ValueInput requireSuccess { get; private set; }

		// Token: 0x0600044A RID: 1098 RVA: 0x0000973E File Offset: 0x0000793E
		protected override void Definition()
		{
			base.Definition();
			this.threshold = base.ValueInput<float>("threshold", 0.05f);
			this.requireSuccess = base.ValueInput<bool>("requireSuccess", true);
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x00009770 File Offset: 0x00007970
		protected override bool ShouldTrigger(Flow flow, EmptyEventArgs args)
		{
			NavMeshAgent component = flow.stack.gameObject.GetComponent<NavMeshAgent>();
			return component != null && component.remainingDistance <= flow.GetValue<float>(this.threshold) && (component.pathStatus == NavMeshPathStatus.PathComplete || !flow.GetValue<bool>(this.requireSuccess));
		}
	}
}
