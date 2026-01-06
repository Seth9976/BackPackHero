using System;

namespace Unity.VisualScripting
{
	// Token: 0x02000041 RID: 65
	[UnitCategory("Control")]
	[UnitTitle("Select")]
	[TypeIcon(typeof(ISelectUnit))]
	[UnitOrder(6)]
	public sealed class SelectUnit : Unit, ISelectUnit, IUnit, IGraphElementWithDebugData, IGraphElement, IGraphItem, INotifiedCollectionItem, IDisposable, IPrewarmable, IAotStubbable, IIdentifiable, IAnalyticsIdentifiable
	{
		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000279 RID: 633 RVA: 0x0000731E File Offset: 0x0000551E
		// (set) Token: 0x0600027A RID: 634 RVA: 0x00007326 File Offset: 0x00005526
		[DoNotSerialize]
		[PortLabelHidden]
		public ValueInput condition { get; private set; }

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x0600027B RID: 635 RVA: 0x0000732F File Offset: 0x0000552F
		// (set) Token: 0x0600027C RID: 636 RVA: 0x00007337 File Offset: 0x00005537
		[DoNotSerialize]
		[PortLabel("True")]
		public ValueInput ifTrue { get; private set; }

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x0600027D RID: 637 RVA: 0x00007340 File Offset: 0x00005540
		// (set) Token: 0x0600027E RID: 638 RVA: 0x00007348 File Offset: 0x00005548
		[DoNotSerialize]
		[PortLabel("False")]
		public ValueInput ifFalse { get; private set; }

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x0600027F RID: 639 RVA: 0x00007351 File Offset: 0x00005551
		// (set) Token: 0x06000280 RID: 640 RVA: 0x00007359 File Offset: 0x00005559
		[DoNotSerialize]
		[PortLabelHidden]
		public ValueOutput selection { get; private set; }

		// Token: 0x06000281 RID: 641 RVA: 0x00007364 File Offset: 0x00005564
		protected override void Definition()
		{
			this.condition = base.ValueInput<bool>("condition");
			this.ifTrue = base.ValueInput<object>("ifTrue").AllowsNull();
			this.ifFalse = base.ValueInput<object>("ifFalse").AllowsNull();
			this.selection = base.ValueOutput<object>("selection", new Func<Flow, object>(this.Branch)).Predictable();
			base.Requirement(this.condition, this.selection);
			base.Requirement(this.ifTrue, this.selection);
			base.Requirement(this.ifFalse, this.selection);
		}

		// Token: 0x06000282 RID: 642 RVA: 0x00007406 File Offset: 0x00005606
		public object Branch(Flow flow)
		{
			return flow.GetValue(flow.GetValue<bool>(this.condition) ? this.ifTrue : this.ifFalse);
		}

		// Token: 0x06000284 RID: 644 RVA: 0x00007432 File Offset: 0x00005632
		FlowGraph IUnit.get_graph()
		{
			return base.graph;
		}
	}
}
