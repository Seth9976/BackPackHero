using System;

namespace Unity.VisualScripting
{
	// Token: 0x020000C9 RID: 201
	[UnitOrder(402)]
	public abstract class Distance<T> : Unit
	{
		// Token: 0x17000243 RID: 579
		// (get) Token: 0x06000614 RID: 1556 RVA: 0x0000C465 File Offset: 0x0000A665
		// (set) Token: 0x06000615 RID: 1557 RVA: 0x0000C46D File Offset: 0x0000A66D
		[DoNotSerialize]
		public ValueInput a { get; private set; }

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x06000616 RID: 1558 RVA: 0x0000C476 File Offset: 0x0000A676
		// (set) Token: 0x06000617 RID: 1559 RVA: 0x0000C47E File Offset: 0x0000A67E
		[DoNotSerialize]
		public ValueInput b { get; private set; }

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x06000618 RID: 1560 RVA: 0x0000C487 File Offset: 0x0000A687
		// (set) Token: 0x06000619 RID: 1561 RVA: 0x0000C48F File Offset: 0x0000A68F
		[DoNotSerialize]
		[PortLabelHidden]
		public ValueOutput distance { get; private set; }

		// Token: 0x0600061A RID: 1562 RVA: 0x0000C498 File Offset: 0x0000A698
		protected override void Definition()
		{
			this.a = base.ValueInput<T>("a");
			this.b = base.ValueInput<T>("b");
			this.distance = base.ValueOutput<float>("distance", new Func<Flow, float>(this.Operation)).Predictable();
			base.Requirement(this.a, this.distance);
			base.Requirement(this.b, this.distance);
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x0000C50D File Offset: 0x0000A70D
		private float Operation(Flow flow)
		{
			return this.Operation(flow.GetValue<T>(this.a), flow.GetValue<T>(this.b));
		}

		// Token: 0x0600061C RID: 1564
		public abstract float Operation(T a, T b);
	}
}
