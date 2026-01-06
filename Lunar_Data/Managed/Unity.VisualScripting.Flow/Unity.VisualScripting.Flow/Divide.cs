using System;

namespace Unity.VisualScripting
{
	// Token: 0x020000CA RID: 202
	[UnitOrder(104)]
	public abstract class Divide<T> : Unit
	{
		// Token: 0x17000246 RID: 582
		// (get) Token: 0x0600061E RID: 1566 RVA: 0x0000C535 File Offset: 0x0000A735
		// (set) Token: 0x0600061F RID: 1567 RVA: 0x0000C53D File Offset: 0x0000A73D
		[DoNotSerialize]
		[PortLabel("A")]
		public ValueInput dividend { get; private set; }

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x06000620 RID: 1568 RVA: 0x0000C546 File Offset: 0x0000A746
		// (set) Token: 0x06000621 RID: 1569 RVA: 0x0000C54E File Offset: 0x0000A74E
		[DoNotSerialize]
		[PortLabel("B")]
		public ValueInput divisor { get; private set; }

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x06000622 RID: 1570 RVA: 0x0000C557 File Offset: 0x0000A757
		// (set) Token: 0x06000623 RID: 1571 RVA: 0x0000C55F File Offset: 0x0000A75F
		[DoNotSerialize]
		[PortLabel("A ÷ B")]
		public ValueOutput quotient { get; private set; }

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x06000624 RID: 1572 RVA: 0x0000C568 File Offset: 0x0000A768
		[DoNotSerialize]
		protected virtual T defaultDivisor
		{
			get
			{
				return default(T);
			}
		}

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x06000625 RID: 1573 RVA: 0x0000C580 File Offset: 0x0000A780
		[DoNotSerialize]
		protected virtual T defaultDividend
		{
			get
			{
				return default(T);
			}
		}

		// Token: 0x06000626 RID: 1574 RVA: 0x0000C598 File Offset: 0x0000A798
		protected override void Definition()
		{
			this.dividend = base.ValueInput<T>("dividend", this.defaultDividend);
			this.divisor = base.ValueInput<T>("divisor", this.defaultDivisor);
			this.quotient = base.ValueOutput<T>("quotient", new Func<Flow, T>(this.Operation)).Predictable();
			base.Requirement(this.dividend, this.quotient);
			base.Requirement(this.divisor, this.quotient);
		}

		// Token: 0x06000627 RID: 1575
		public abstract T Operation(T divident, T divisor);

		// Token: 0x06000628 RID: 1576 RVA: 0x0000C619 File Offset: 0x0000A819
		public T Operation(Flow flow)
		{
			return this.Operation(flow.GetValue<T>(this.dividend), flow.GetValue<T>(this.divisor));
		}
	}
}
