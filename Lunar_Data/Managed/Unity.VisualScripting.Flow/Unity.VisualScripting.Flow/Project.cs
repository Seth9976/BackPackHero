using System;

namespace Unity.VisualScripting
{
	// Token: 0x020000DA RID: 218
	[UnitOrder(406)]
	public abstract class Project<T> : Unit
	{
		// Token: 0x1700026A RID: 618
		// (get) Token: 0x06000694 RID: 1684 RVA: 0x0000CF9E File Offset: 0x0000B19E
		// (set) Token: 0x06000695 RID: 1685 RVA: 0x0000CFA6 File Offset: 0x0000B1A6
		[DoNotSerialize]
		public ValueInput a { get; private set; }

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x06000696 RID: 1686 RVA: 0x0000CFAF File Offset: 0x0000B1AF
		// (set) Token: 0x06000697 RID: 1687 RVA: 0x0000CFB7 File Offset: 0x0000B1B7
		[DoNotSerialize]
		public ValueInput b { get; private set; }

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x06000698 RID: 1688 RVA: 0x0000CFC0 File Offset: 0x0000B1C0
		// (set) Token: 0x06000699 RID: 1689 RVA: 0x0000CFC8 File Offset: 0x0000B1C8
		[DoNotSerialize]
		[PortLabelHidden]
		public ValueOutput projection { get; private set; }

		// Token: 0x0600069A RID: 1690 RVA: 0x0000CFD4 File Offset: 0x0000B1D4
		protected override void Definition()
		{
			this.a = base.ValueInput<T>("a");
			this.b = base.ValueInput<T>("b");
			this.projection = base.ValueOutput<T>("projection", new Func<Flow, T>(this.Operation)).Predictable();
			base.Requirement(this.a, this.projection);
			base.Requirement(this.b, this.projection);
		}

		// Token: 0x0600069B RID: 1691 RVA: 0x0000D049 File Offset: 0x0000B249
		private T Operation(Flow flow)
		{
			return this.Operation(flow.GetValue<T>(this.a), flow.GetValue<T>(this.b));
		}

		// Token: 0x0600069C RID: 1692
		public abstract T Operation(T a, T b);
	}
}
