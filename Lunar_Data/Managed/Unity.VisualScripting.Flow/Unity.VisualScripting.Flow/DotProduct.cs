using System;

namespace Unity.VisualScripting
{
	// Token: 0x020000CB RID: 203
	[UnitOrder(404)]
	public abstract class DotProduct<T> : Unit
	{
		// Token: 0x1700024B RID: 587
		// (get) Token: 0x0600062A RID: 1578 RVA: 0x0000C641 File Offset: 0x0000A841
		// (set) Token: 0x0600062B RID: 1579 RVA: 0x0000C649 File Offset: 0x0000A849
		[DoNotSerialize]
		public ValueInput a { get; private set; }

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x0600062C RID: 1580 RVA: 0x0000C652 File Offset: 0x0000A852
		// (set) Token: 0x0600062D RID: 1581 RVA: 0x0000C65A File Offset: 0x0000A85A
		[DoNotSerialize]
		public ValueInput b { get; private set; }

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x0600062E RID: 1582 RVA: 0x0000C663 File Offset: 0x0000A863
		// (set) Token: 0x0600062F RID: 1583 RVA: 0x0000C66B File Offset: 0x0000A86B
		[DoNotSerialize]
		[PortLabel("A∙B")]
		public ValueOutput dotProduct { get; private set; }

		// Token: 0x06000630 RID: 1584 RVA: 0x0000C674 File Offset: 0x0000A874
		protected override void Definition()
		{
			this.a = base.ValueInput<T>("a");
			this.b = base.ValueInput<T>("b");
			this.dotProduct = base.ValueOutput<float>("dotProduct", new Func<Flow, float>(this.Operation)).Predictable();
			base.Requirement(this.a, this.dotProduct);
			base.Requirement(this.b, this.dotProduct);
		}

		// Token: 0x06000631 RID: 1585 RVA: 0x0000C6E9 File Offset: 0x0000A8E9
		private float Operation(Flow flow)
		{
			return this.Operation(flow.GetValue<T>(this.a), flow.GetValue<T>(this.b));
		}

		// Token: 0x06000632 RID: 1586
		public abstract float Operation(T a, T b);
	}
}
