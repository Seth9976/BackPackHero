using System;

namespace Unity.VisualScripting
{
	// Token: 0x020000BF RID: 191
	[UnitCategory("Logic")]
	[UnitOrder(3)]
	public sealed class Negate : Unit
	{
		// Token: 0x17000225 RID: 549
		// (get) Token: 0x060005B3 RID: 1459 RVA: 0x0000BB0A File Offset: 0x00009D0A
		// (set) Token: 0x060005B4 RID: 1460 RVA: 0x0000BB12 File Offset: 0x00009D12
		[DoNotSerialize]
		[PortLabel("X")]
		public ValueInput input { get; private set; }

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x060005B5 RID: 1461 RVA: 0x0000BB1B File Offset: 0x00009D1B
		// (set) Token: 0x060005B6 RID: 1462 RVA: 0x0000BB23 File Offset: 0x00009D23
		[DoNotSerialize]
		[PortLabel("~X")]
		public ValueOutput output { get; private set; }

		// Token: 0x060005B7 RID: 1463 RVA: 0x0000BB2C File Offset: 0x00009D2C
		protected override void Definition()
		{
			this.input = base.ValueInput<bool>("input");
			this.output = base.ValueOutput<bool>("output", new Func<Flow, bool>(this.Operation)).Predictable();
			base.Requirement(this.input, this.output);
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x0000BB7E File Offset: 0x00009D7E
		public bool Operation(Flow flow)
		{
			return !flow.GetValue<bool>(this.input);
		}
	}
}
