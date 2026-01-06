using System;

namespace Unity.VisualScripting
{
	// Token: 0x020000DB RID: 219
	[UnitOrder(202)]
	public abstract class Round<TInput, TOutput> : Unit
	{
		// Token: 0x1700026D RID: 621
		// (get) Token: 0x0600069E RID: 1694 RVA: 0x0000D071 File Offset: 0x0000B271
		// (set) Token: 0x0600069F RID: 1695 RVA: 0x0000D079 File Offset: 0x0000B279
		[Inspectable]
		[UnitHeaderInspectable]
		[Serialize]
		public Round<TInput, TOutput>.Rounding rounding { get; set; } = Round<TInput, TOutput>.Rounding.AwayFromZero;

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x060006A0 RID: 1696 RVA: 0x0000D082 File Offset: 0x0000B282
		// (set) Token: 0x060006A1 RID: 1697 RVA: 0x0000D08A File Offset: 0x0000B28A
		[DoNotSerialize]
		[PortLabelHidden]
		public ValueInput input { get; private set; }

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x060006A2 RID: 1698 RVA: 0x0000D093 File Offset: 0x0000B293
		// (set) Token: 0x060006A3 RID: 1699 RVA: 0x0000D09B File Offset: 0x0000B29B
		[DoNotSerialize]
		[PortLabelHidden]
		public ValueOutput output { get; private set; }

		// Token: 0x060006A4 RID: 1700 RVA: 0x0000D0A4 File Offset: 0x0000B2A4
		protected override void Definition()
		{
			this.input = base.ValueInput<TInput>("input");
			this.output = base.ValueOutput<TOutput>("output", new Func<Flow, TOutput>(this.Operation)).Predictable();
			base.Requirement(this.input, this.output);
		}

		// Token: 0x060006A5 RID: 1701
		protected abstract TOutput Floor(TInput input);

		// Token: 0x060006A6 RID: 1702
		protected abstract TOutput AwayFromZero(TInput input);

		// Token: 0x060006A7 RID: 1703
		protected abstract TOutput Ceiling(TInput input);

		// Token: 0x060006A8 RID: 1704 RVA: 0x0000D0F8 File Offset: 0x0000B2F8
		public TOutput Operation(Flow flow)
		{
			switch (this.rounding)
			{
			case Round<TInput, TOutput>.Rounding.Floor:
				return this.Floor(flow.GetValue<TInput>(this.input));
			case Round<TInput, TOutput>.Rounding.Ceiling:
				return this.Ceiling(flow.GetValue<TInput>(this.input));
			case Round<TInput, TOutput>.Rounding.AwayFromZero:
				return this.AwayFromZero(flow.GetValue<TInput>(this.input));
			default:
				throw new UnexpectedEnumValueException<Round<TInput, TOutput>.Rounding>(this.rounding);
			}
		}

		// Token: 0x020001BC RID: 444
		public enum Rounding
		{
			// Token: 0x040003B2 RID: 946
			Floor,
			// Token: 0x040003B3 RID: 947
			Ceiling,
			// Token: 0x040003B4 RID: 948
			AwayFromZero
		}
	}
}
