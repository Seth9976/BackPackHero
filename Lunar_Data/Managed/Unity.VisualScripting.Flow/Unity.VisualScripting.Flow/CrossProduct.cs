using System;

namespace Unity.VisualScripting
{
	// Token: 0x020000C8 RID: 200
	[UnitOrder(405)]
	[TypeIcon(typeof(Multiply<>))]
	public abstract class CrossProduct<T> : Unit
	{
		// Token: 0x17000240 RID: 576
		// (get) Token: 0x0600060A RID: 1546 RVA: 0x0000C392 File Offset: 0x0000A592
		// (set) Token: 0x0600060B RID: 1547 RVA: 0x0000C39A File Offset: 0x0000A59A
		[DoNotSerialize]
		public ValueInput a { get; private set; }

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x0600060C RID: 1548 RVA: 0x0000C3A3 File Offset: 0x0000A5A3
		// (set) Token: 0x0600060D RID: 1549 RVA: 0x0000C3AB File Offset: 0x0000A5AB
		[DoNotSerialize]
		public ValueInput b { get; private set; }

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x0600060E RID: 1550 RVA: 0x0000C3B4 File Offset: 0x0000A5B4
		// (set) Token: 0x0600060F RID: 1551 RVA: 0x0000C3BC File Offset: 0x0000A5BC
		[DoNotSerialize]
		[PortLabel("A × B")]
		public ValueOutput crossProduct { get; private set; }

		// Token: 0x06000610 RID: 1552 RVA: 0x0000C3C8 File Offset: 0x0000A5C8
		protected override void Definition()
		{
			this.a = base.ValueInput<T>("a");
			this.b = base.ValueInput<T>("b");
			this.crossProduct = base.ValueOutput<T>("crossProduct", new Func<Flow, T>(this.Operation)).Predictable();
			base.Requirement(this.a, this.crossProduct);
			base.Requirement(this.b, this.crossProduct);
		}

		// Token: 0x06000611 RID: 1553 RVA: 0x0000C43D File Offset: 0x0000A63D
		private T Operation(Flow flow)
		{
			return this.Operation(flow.GetValue<T>(this.a), flow.GetValue<T>(this.b));
		}

		// Token: 0x06000612 RID: 1554
		public abstract T Operation(T a, T b);
	}
}
