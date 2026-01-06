using System;

namespace Unity.VisualScripting
{
	// Token: 0x020000C4 RID: 196
	[UnitOrder(201)]
	public abstract class Absolute<TInput> : Unit
	{
		// Token: 0x17000236 RID: 566
		// (get) Token: 0x060005E6 RID: 1510 RVA: 0x0000C047 File Offset: 0x0000A247
		// (set) Token: 0x060005E7 RID: 1511 RVA: 0x0000C04F File Offset: 0x0000A24F
		[DoNotSerialize]
		[PortLabelHidden]
		public ValueInput input { get; private set; }

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x060005E8 RID: 1512 RVA: 0x0000C058 File Offset: 0x0000A258
		// (set) Token: 0x060005E9 RID: 1513 RVA: 0x0000C060 File Offset: 0x0000A260
		[DoNotSerialize]
		[PortLabelHidden]
		public ValueOutput output { get; private set; }

		// Token: 0x060005EA RID: 1514 RVA: 0x0000C06C File Offset: 0x0000A26C
		protected override void Definition()
		{
			this.input = base.ValueInput<TInput>("input");
			this.output = base.ValueOutput<TInput>("output", new Func<Flow, TInput>(this.Operation)).Predictable();
			base.Requirement(this.input, this.output);
		}

		// Token: 0x060005EB RID: 1515
		protected abstract TInput Operation(TInput input);

		// Token: 0x060005EC RID: 1516 RVA: 0x0000C0BE File Offset: 0x0000A2BE
		public TInput Operation(Flow flow)
		{
			return this.Operation(flow.GetValue<TInput>(this.input));
		}
	}
}
