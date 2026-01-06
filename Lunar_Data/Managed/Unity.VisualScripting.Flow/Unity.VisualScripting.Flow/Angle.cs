using System;

namespace Unity.VisualScripting
{
	// Token: 0x020000C6 RID: 198
	[UnitOrder(403)]
	public abstract class Angle<T> : Unit
	{
		// Token: 0x1700023C RID: 572
		// (get) Token: 0x060005F9 RID: 1529 RVA: 0x0000C1CB File Offset: 0x0000A3CB
		// (set) Token: 0x060005FA RID: 1530 RVA: 0x0000C1D3 File Offset: 0x0000A3D3
		[DoNotSerialize]
		public ValueInput a { get; private set; }

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x060005FB RID: 1531 RVA: 0x0000C1DC File Offset: 0x0000A3DC
		// (set) Token: 0x060005FC RID: 1532 RVA: 0x0000C1E4 File Offset: 0x0000A3E4
		[DoNotSerialize]
		public ValueInput b { get; private set; }

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x060005FD RID: 1533 RVA: 0x0000C1ED File Offset: 0x0000A3ED
		// (set) Token: 0x060005FE RID: 1534 RVA: 0x0000C1F5 File Offset: 0x0000A3F5
		[DoNotSerialize]
		[PortLabelHidden]
		public ValueOutput angle { get; private set; }

		// Token: 0x060005FF RID: 1535 RVA: 0x0000C200 File Offset: 0x0000A400
		protected override void Definition()
		{
			this.a = base.ValueInput<T>("a");
			this.b = base.ValueInput<T>("b");
			this.angle = base.ValueOutput<float>("angle", new Func<Flow, float>(this.Operation)).Predictable();
			base.Requirement(this.a, this.angle);
			base.Requirement(this.b, this.angle);
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x0000C275 File Offset: 0x0000A475
		private float Operation(Flow flow)
		{
			return this.Operation(flow.GetValue<T>(this.a), flow.GetValue<T>(this.b));
		}

		// Token: 0x06000601 RID: 1537
		public abstract float Operation(T a, T b);
	}
}
