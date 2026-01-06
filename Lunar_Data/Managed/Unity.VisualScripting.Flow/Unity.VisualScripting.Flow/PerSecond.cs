using System;

namespace Unity.VisualScripting
{
	// Token: 0x020000D9 RID: 217
	[UnitOrder(601)]
	public abstract class PerSecond<T> : Unit
	{
		// Token: 0x17000268 RID: 616
		// (get) Token: 0x0600068C RID: 1676 RVA: 0x0000CF0A File Offset: 0x0000B10A
		// (set) Token: 0x0600068D RID: 1677 RVA: 0x0000CF12 File Offset: 0x0000B112
		[DoNotSerialize]
		[PortLabelHidden]
		public ValueInput input { get; private set; }

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x0600068E RID: 1678 RVA: 0x0000CF1B File Offset: 0x0000B11B
		// (set) Token: 0x0600068F RID: 1679 RVA: 0x0000CF23 File Offset: 0x0000B123
		[DoNotSerialize]
		[PortLabelHidden]
		public ValueOutput output { get; private set; }

		// Token: 0x06000690 RID: 1680 RVA: 0x0000CF2C File Offset: 0x0000B12C
		protected override void Definition()
		{
			this.input = base.ValueInput<T>("input", default(T));
			this.output = base.ValueOutput<T>("output", new Func<Flow, T>(this.Operation));
			base.Requirement(this.input, this.output);
		}

		// Token: 0x06000691 RID: 1681
		public abstract T Operation(T input);

		// Token: 0x06000692 RID: 1682 RVA: 0x0000CF82 File Offset: 0x0000B182
		public T Operation(Flow flow)
		{
			return this.Operation(flow.GetValue<T>(this.input));
		}
	}
}
