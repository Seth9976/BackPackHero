using System;

namespace Unity.VisualScripting
{
	// Token: 0x0200001B RID: 27
	[UnitSurtitle("State")]
	[UnitCategory("Nesting")]
	[UnitShortTitle("Trigger Transition")]
	[TypeIcon(typeof(IStateTransition))]
	public sealed class TriggerStateTransition : Unit
	{
		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x0000348D File Offset: 0x0000168D
		// (set) Token: 0x060000B9 RID: 185 RVA: 0x00003495 File Offset: 0x00001695
		[DoNotSerialize]
		[PortLabelHidden]
		public ControlInput trigger { get; private set; }

		// Token: 0x060000BA RID: 186 RVA: 0x0000349E File Offset: 0x0000169E
		protected override void Definition()
		{
			this.trigger = base.ControlInput("trigger", new Func<Flow, ControlOutput>(this.Trigger));
		}

		// Token: 0x060000BB RID: 187 RVA: 0x000034BD File Offset: 0x000016BD
		private ControlOutput Trigger(Flow flow)
		{
			IStateTransition parent = flow.stack.GetParent<INesterStateTransition>();
			flow.stack.ExitParentElement();
			parent.Branch(flow);
			return null;
		}
	}
}
