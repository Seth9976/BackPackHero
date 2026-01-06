using System;

namespace Unity.VisualScripting
{
	// Token: 0x020000D5 RID: 213
	[UnitOrder(105)]
	public abstract class Modulo<T> : Unit
	{
		// Token: 0x17000256 RID: 598
		// (get) Token: 0x0600065D RID: 1629 RVA: 0x0000CAFA File Offset: 0x0000ACFA
		// (set) Token: 0x0600065E RID: 1630 RVA: 0x0000CB02 File Offset: 0x0000AD02
		[DoNotSerialize]
		[PortLabel("A")]
		public ValueInput dividend { get; private set; }

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x0600065F RID: 1631 RVA: 0x0000CB0B File Offset: 0x0000AD0B
		// (set) Token: 0x06000660 RID: 1632 RVA: 0x0000CB13 File Offset: 0x0000AD13
		[DoNotSerialize]
		[PortLabel("B")]
		public ValueInput divisor { get; private set; }

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x06000661 RID: 1633 RVA: 0x0000CB1C File Offset: 0x0000AD1C
		// (set) Token: 0x06000662 RID: 1634 RVA: 0x0000CB24 File Offset: 0x0000AD24
		[DoNotSerialize]
		[PortLabel("A % B")]
		public ValueOutput remainder { get; private set; }

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x06000663 RID: 1635 RVA: 0x0000CB30 File Offset: 0x0000AD30
		[DoNotSerialize]
		protected virtual T defaultDivisor
		{
			get
			{
				return default(T);
			}
		}

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x06000664 RID: 1636 RVA: 0x0000CB48 File Offset: 0x0000AD48
		[DoNotSerialize]
		protected virtual T defaultDividend
		{
			get
			{
				return default(T);
			}
		}

		// Token: 0x06000665 RID: 1637 RVA: 0x0000CB60 File Offset: 0x0000AD60
		protected override void Definition()
		{
			this.dividend = base.ValueInput<T>("dividend", this.defaultDividend);
			this.divisor = base.ValueInput<T>("divisor", this.defaultDivisor);
			this.remainder = base.ValueOutput<T>("remainder", new Func<Flow, T>(this.Operation)).Predictable();
			base.Requirement(this.dividend, this.remainder);
			base.Requirement(this.divisor, this.remainder);
		}

		// Token: 0x06000666 RID: 1638
		public abstract T Operation(T divident, T divisor);

		// Token: 0x06000667 RID: 1639 RVA: 0x0000CBE1 File Offset: 0x0000ADE1
		public T Operation(Flow flow)
		{
			return this.Operation(flow.GetValue<T>(this.dividend), flow.GetValue<T>(this.divisor));
		}
	}
}
