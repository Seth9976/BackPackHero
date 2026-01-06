using System;

namespace Unity.VisualScripting
{
	// Token: 0x020000ED RID: 237
	[UnitOrder(102)]
	public abstract class Subtract<T> : Unit
	{
		// Token: 0x17000283 RID: 643
		// (get) Token: 0x060006ED RID: 1773 RVA: 0x0000D4C6 File Offset: 0x0000B6C6
		// (set) Token: 0x060006EE RID: 1774 RVA: 0x0000D4CE File Offset: 0x0000B6CE
		[DoNotSerialize]
		[PortLabel("A")]
		public ValueInput minuend { get; private set; }

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x060006EF RID: 1775 RVA: 0x0000D4D7 File Offset: 0x0000B6D7
		// (set) Token: 0x060006F0 RID: 1776 RVA: 0x0000D4DF File Offset: 0x0000B6DF
		[DoNotSerialize]
		[PortLabel("B")]
		public ValueInput subtrahend { get; private set; }

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x060006F1 RID: 1777 RVA: 0x0000D4E8 File Offset: 0x0000B6E8
		// (set) Token: 0x060006F2 RID: 1778 RVA: 0x0000D4F0 File Offset: 0x0000B6F0
		[DoNotSerialize]
		[PortLabel("A − B")]
		public ValueOutput difference { get; private set; }

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x060006F3 RID: 1779 RVA: 0x0000D4FC File Offset: 0x0000B6FC
		[DoNotSerialize]
		protected virtual T defaultMinuend
		{
			get
			{
				return default(T);
			}
		}

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x060006F4 RID: 1780 RVA: 0x0000D514 File Offset: 0x0000B714
		[DoNotSerialize]
		protected virtual T defaultSubtrahend
		{
			get
			{
				return default(T);
			}
		}

		// Token: 0x060006F5 RID: 1781 RVA: 0x0000D52C File Offset: 0x0000B72C
		protected override void Definition()
		{
			this.minuend = base.ValueInput<T>("minuend", this.defaultMinuend);
			this.subtrahend = base.ValueInput<T>("subtrahend", this.defaultSubtrahend);
			this.difference = base.ValueOutput<T>("difference", new Func<Flow, T>(this.Operation)).Predictable();
			base.Requirement(this.minuend, this.difference);
			base.Requirement(this.subtrahend, this.difference);
		}

		// Token: 0x060006F6 RID: 1782
		public abstract T Operation(T a, T b);

		// Token: 0x060006F7 RID: 1783 RVA: 0x0000D5AD File Offset: 0x0000B7AD
		public T Operation(Flow flow)
		{
			return this.Operation(flow.GetValue<T>(this.minuend), flow.GetValue<T>(this.subtrahend));
		}
	}
}
