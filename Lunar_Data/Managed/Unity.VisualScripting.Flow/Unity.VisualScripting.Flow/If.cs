using System;

namespace Unity.VisualScripting
{
	// Token: 0x02000039 RID: 57
	[UnitCategory("Control")]
	[UnitOrder(0)]
	[RenamedFrom("Bolt.Branch")]
	[RenamedFrom("Unity.VisualScripting.Branch")]
	public sealed class If : Unit, IBranchUnit, IUnit, IGraphElementWithDebugData, IGraphElement, IGraphItem, INotifiedCollectionItem, IDisposable, IPrewarmable, IAotStubbable, IIdentifiable, IAnalyticsIdentifiable
	{
		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000237 RID: 567 RVA: 0x00006CF6 File Offset: 0x00004EF6
		// (set) Token: 0x06000238 RID: 568 RVA: 0x00006CFE File Offset: 0x00004EFE
		[DoNotSerialize]
		[PortLabelHidden]
		public ControlInput enter { get; private set; }

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000239 RID: 569 RVA: 0x00006D07 File Offset: 0x00004F07
		// (set) Token: 0x0600023A RID: 570 RVA: 0x00006D0F File Offset: 0x00004F0F
		[DoNotSerialize]
		[PortLabelHidden]
		public ValueInput condition { get; private set; }

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x0600023B RID: 571 RVA: 0x00006D18 File Offset: 0x00004F18
		// (set) Token: 0x0600023C RID: 572 RVA: 0x00006D20 File Offset: 0x00004F20
		[DoNotSerialize]
		[PortLabel("True")]
		public ControlOutput ifTrue { get; private set; }

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x0600023D RID: 573 RVA: 0x00006D29 File Offset: 0x00004F29
		// (set) Token: 0x0600023E RID: 574 RVA: 0x00006D31 File Offset: 0x00004F31
		[DoNotSerialize]
		[PortLabel("False")]
		public ControlOutput ifFalse { get; private set; }

		// Token: 0x0600023F RID: 575 RVA: 0x00006D3C File Offset: 0x00004F3C
		protected override void Definition()
		{
			this.enter = base.ControlInput("enter", new Func<Flow, ControlOutput>(this.Enter));
			this.condition = base.ValueInput<bool>("condition");
			this.ifTrue = base.ControlOutput("ifTrue");
			this.ifFalse = base.ControlOutput("ifFalse");
			base.Requirement(this.condition, this.enter);
			base.Succession(this.enter, this.ifTrue);
			base.Succession(this.enter, this.ifFalse);
		}

		// Token: 0x06000240 RID: 576 RVA: 0x00006DCF File Offset: 0x00004FCF
		public ControlOutput Enter(Flow flow)
		{
			if (!flow.GetValue<bool>(this.condition))
			{
				return this.ifFalse;
			}
			return this.ifTrue;
		}

		// Token: 0x06000242 RID: 578 RVA: 0x00006DF4 File Offset: 0x00004FF4
		FlowGraph IUnit.get_graph()
		{
			return base.graph;
		}
	}
}
