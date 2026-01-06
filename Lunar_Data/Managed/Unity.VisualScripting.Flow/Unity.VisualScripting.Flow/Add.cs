using System;

namespace Unity.VisualScripting
{
	// Token: 0x020000C5 RID: 197
	[UnitOrder(101)]
	public abstract class Add<T> : Unit
	{
		// Token: 0x17000238 RID: 568
		// (get) Token: 0x060005EE RID: 1518 RVA: 0x0000C0DA File Offset: 0x0000A2DA
		// (set) Token: 0x060005EF RID: 1519 RVA: 0x0000C0E2 File Offset: 0x0000A2E2
		[DoNotSerialize]
		public ValueInput a { get; private set; }

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x060005F0 RID: 1520 RVA: 0x0000C0EB File Offset: 0x0000A2EB
		// (set) Token: 0x060005F1 RID: 1521 RVA: 0x0000C0F3 File Offset: 0x0000A2F3
		[DoNotSerialize]
		public ValueInput b { get; private set; }

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x060005F2 RID: 1522 RVA: 0x0000C0FC File Offset: 0x0000A2FC
		// (set) Token: 0x060005F3 RID: 1523 RVA: 0x0000C104 File Offset: 0x0000A304
		[DoNotSerialize]
		[PortLabel("A + B")]
		public ValueOutput sum { get; private set; }

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x060005F4 RID: 1524 RVA: 0x0000C110 File Offset: 0x0000A310
		[DoNotSerialize]
		protected virtual T defaultB
		{
			get
			{
				return default(T);
			}
		}

		// Token: 0x060005F5 RID: 1525 RVA: 0x0000C128 File Offset: 0x0000A328
		protected override void Definition()
		{
			this.a = base.ValueInput<T>("a");
			this.b = base.ValueInput<T>("b", this.defaultB);
			this.sum = base.ValueOutput<T>("sum", new Func<Flow, T>(this.Operation)).Predictable();
			base.Requirement(this.a, this.sum);
			base.Requirement(this.b, this.sum);
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x0000C1A3 File Offset: 0x0000A3A3
		private T Operation(Flow flow)
		{
			return this.Operation(flow.GetValue<T>(this.a), flow.GetValue<T>(this.b));
		}

		// Token: 0x060005F7 RID: 1527
		public abstract T Operation(T a, T b);
	}
}
