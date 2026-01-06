using System;

namespace Unity.VisualScripting
{
	// Token: 0x020000D7 RID: 215
	[UnitOrder(103)]
	public abstract class Multiply<T> : Unit
	{
		// Token: 0x17000262 RID: 610
		// (get) Token: 0x06000679 RID: 1657 RVA: 0x0000CD88 File Offset: 0x0000AF88
		// (set) Token: 0x0600067A RID: 1658 RVA: 0x0000CD90 File Offset: 0x0000AF90
		[DoNotSerialize]
		public ValueInput a { get; private set; }

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x0600067B RID: 1659 RVA: 0x0000CD99 File Offset: 0x0000AF99
		// (set) Token: 0x0600067C RID: 1660 RVA: 0x0000CDA1 File Offset: 0x0000AFA1
		[DoNotSerialize]
		public ValueInput b { get; private set; }

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x0600067D RID: 1661 RVA: 0x0000CDAA File Offset: 0x0000AFAA
		// (set) Token: 0x0600067E RID: 1662 RVA: 0x0000CDB2 File Offset: 0x0000AFB2
		[DoNotSerialize]
		[PortLabel("A × B")]
		public ValueOutput product { get; private set; }

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x0600067F RID: 1663 RVA: 0x0000CDBC File Offset: 0x0000AFBC
		[DoNotSerialize]
		protected virtual T defaultB
		{
			get
			{
				return default(T);
			}
		}

		// Token: 0x06000680 RID: 1664 RVA: 0x0000CDD4 File Offset: 0x0000AFD4
		protected override void Definition()
		{
			this.a = base.ValueInput<T>("a");
			this.b = base.ValueInput<T>("b", this.defaultB);
			this.product = base.ValueOutput<T>("product", new Func<Flow, T>(this.Operation)).Predictable();
			base.Requirement(this.a, this.product);
			base.Requirement(this.b, this.product);
		}

		// Token: 0x06000681 RID: 1665 RVA: 0x0000CE4F File Offset: 0x0000B04F
		private T Operation(Flow flow)
		{
			return this.Operation(flow.GetValue<T>(this.a), flow.GetValue<T>(this.b));
		}

		// Token: 0x06000682 RID: 1666
		public abstract T Operation(T a, T b);
	}
}
