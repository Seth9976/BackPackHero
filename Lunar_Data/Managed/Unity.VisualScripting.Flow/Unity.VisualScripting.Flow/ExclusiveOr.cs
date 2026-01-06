using System;

namespace Unity.VisualScripting
{
	// Token: 0x020000BA RID: 186
	[UnitCategory("Logic")]
	[UnitOrder(2)]
	public sealed class ExclusiveOr : Unit
	{
		// Token: 0x1700021E RID: 542
		// (get) Token: 0x0600059A RID: 1434 RVA: 0x0000B9AF File Offset: 0x00009BAF
		// (set) Token: 0x0600059B RID: 1435 RVA: 0x0000B9B7 File Offset: 0x00009BB7
		[DoNotSerialize]
		public ValueInput a { get; private set; }

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x0600059C RID: 1436 RVA: 0x0000B9C0 File Offset: 0x00009BC0
		// (set) Token: 0x0600059D RID: 1437 RVA: 0x0000B9C8 File Offset: 0x00009BC8
		[DoNotSerialize]
		public ValueInput b { get; private set; }

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x0600059E RID: 1438 RVA: 0x0000B9D1 File Offset: 0x00009BD1
		// (set) Token: 0x0600059F RID: 1439 RVA: 0x0000B9D9 File Offset: 0x00009BD9
		[DoNotSerialize]
		[PortLabel("A ⊕ B")]
		public ValueOutput result { get; private set; }

		// Token: 0x060005A0 RID: 1440 RVA: 0x0000B9E4 File Offset: 0x00009BE4
		protected override void Definition()
		{
			this.a = base.ValueInput<bool>("a");
			this.b = base.ValueInput<bool>("b");
			this.result = base.ValueOutput<bool>("result", new Func<Flow, bool>(this.Operation)).Predictable();
			base.Requirement(this.a, this.result);
			base.Requirement(this.b, this.result);
		}

		// Token: 0x060005A1 RID: 1441 RVA: 0x0000BA59 File Offset: 0x00009C59
		public bool Operation(Flow flow)
		{
			return flow.GetValue<bool>(this.a) ^ flow.GetValue<bool>(this.b);
		}
	}
}
