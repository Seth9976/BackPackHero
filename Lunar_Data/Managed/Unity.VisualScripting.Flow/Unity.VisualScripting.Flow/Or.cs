using System;

namespace Unity.VisualScripting
{
	// Token: 0x020000C3 RID: 195
	[UnitCategory("Logic")]
	[UnitOrder(1)]
	public sealed class Or : Unit
	{
		// Token: 0x17000233 RID: 563
		// (get) Token: 0x060005DD RID: 1501 RVA: 0x0000BF78 File Offset: 0x0000A178
		// (set) Token: 0x060005DE RID: 1502 RVA: 0x0000BF80 File Offset: 0x0000A180
		[DoNotSerialize]
		public ValueInput a { get; private set; }

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x060005DF RID: 1503 RVA: 0x0000BF89 File Offset: 0x0000A189
		// (set) Token: 0x060005E0 RID: 1504 RVA: 0x0000BF91 File Offset: 0x0000A191
		[DoNotSerialize]
		public ValueInput b { get; private set; }

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x060005E1 RID: 1505 RVA: 0x0000BF9A File Offset: 0x0000A19A
		// (set) Token: 0x060005E2 RID: 1506 RVA: 0x0000BFA2 File Offset: 0x0000A1A2
		[DoNotSerialize]
		[PortLabel("A | B")]
		public ValueOutput result { get; private set; }

		// Token: 0x060005E3 RID: 1507 RVA: 0x0000BFAC File Offset: 0x0000A1AC
		protected override void Definition()
		{
			this.a = base.ValueInput<bool>("a");
			this.b = base.ValueInput<bool>("b");
			this.result = base.ValueOutput<bool>("result", new Func<Flow, bool>(this.Operation)).Predictable();
			base.Requirement(this.a, this.result);
			base.Requirement(this.b, this.result);
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x0000C021 File Offset: 0x0000A221
		public bool Operation(Flow flow)
		{
			return flow.GetValue<bool>(this.a) || flow.GetValue<bool>(this.b);
		}
	}
}
