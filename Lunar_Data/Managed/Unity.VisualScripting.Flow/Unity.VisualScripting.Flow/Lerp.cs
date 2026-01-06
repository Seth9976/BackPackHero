using System;

namespace Unity.VisualScripting
{
	// Token: 0x020000D2 RID: 210
	[UnitOrder(501)]
	public abstract class Lerp<T> : Unit
	{
		// Token: 0x1700024E RID: 590
		// (get) Token: 0x06000641 RID: 1601 RVA: 0x0000C7C0 File Offset: 0x0000A9C0
		// (set) Token: 0x06000642 RID: 1602 RVA: 0x0000C7C8 File Offset: 0x0000A9C8
		[DoNotSerialize]
		public ValueInput a { get; private set; }

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x06000643 RID: 1603 RVA: 0x0000C7D1 File Offset: 0x0000A9D1
		// (set) Token: 0x06000644 RID: 1604 RVA: 0x0000C7D9 File Offset: 0x0000A9D9
		[DoNotSerialize]
		public ValueInput b { get; private set; }

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x06000645 RID: 1605 RVA: 0x0000C7E2 File Offset: 0x0000A9E2
		// (set) Token: 0x06000646 RID: 1606 RVA: 0x0000C7EA File Offset: 0x0000A9EA
		[DoNotSerialize]
		public ValueInput t { get; private set; }

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x06000647 RID: 1607 RVA: 0x0000C7F3 File Offset: 0x0000A9F3
		// (set) Token: 0x06000648 RID: 1608 RVA: 0x0000C7FB File Offset: 0x0000A9FB
		[DoNotSerialize]
		[PortLabelHidden]
		public ValueOutput interpolation { get; private set; }

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x06000649 RID: 1609 RVA: 0x0000C804 File Offset: 0x0000AA04
		[DoNotSerialize]
		protected virtual T defaultA
		{
			get
			{
				return default(T);
			}
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x0600064A RID: 1610 RVA: 0x0000C81C File Offset: 0x0000AA1C
		[DoNotSerialize]
		protected virtual T defaultB
		{
			get
			{
				return default(T);
			}
		}

		// Token: 0x0600064B RID: 1611 RVA: 0x0000C834 File Offset: 0x0000AA34
		protected override void Definition()
		{
			this.a = base.ValueInput<T>("a", this.defaultA);
			this.b = base.ValueInput<T>("b", this.defaultB);
			this.t = base.ValueInput<float>("t", 0f);
			this.interpolation = base.ValueOutput<T>("interpolation", new Func<Flow, T>(this.Operation)).Predictable();
			base.Requirement(this.a, this.interpolation);
			base.Requirement(this.b, this.interpolation);
			base.Requirement(this.t, this.interpolation);
		}

		// Token: 0x0600064C RID: 1612 RVA: 0x0000C8DD File Offset: 0x0000AADD
		private T Operation(Flow flow)
		{
			return this.Operation(flow.GetValue<T>(this.a), flow.GetValue<T>(this.b), flow.GetValue<float>(this.t));
		}

		// Token: 0x0600064D RID: 1613
		public abstract T Operation(T a, T b, float t);
	}
}
