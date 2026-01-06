using System;

namespace Unity.VisualScripting
{
	// Token: 0x020000D8 RID: 216
	[UnitOrder(401)]
	public abstract class Normalize<T> : Unit
	{
		// Token: 0x17000266 RID: 614
		// (get) Token: 0x06000684 RID: 1668 RVA: 0x0000CE77 File Offset: 0x0000B077
		// (set) Token: 0x06000685 RID: 1669 RVA: 0x0000CE7F File Offset: 0x0000B07F
		[DoNotSerialize]
		[PortLabelHidden]
		public ValueInput input { get; private set; }

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x06000686 RID: 1670 RVA: 0x0000CE88 File Offset: 0x0000B088
		// (set) Token: 0x06000687 RID: 1671 RVA: 0x0000CE90 File Offset: 0x0000B090
		[DoNotSerialize]
		[PortLabelHidden]
		public ValueOutput output { get; private set; }

		// Token: 0x06000688 RID: 1672 RVA: 0x0000CE9C File Offset: 0x0000B09C
		protected override void Definition()
		{
			this.input = base.ValueInput<T>("input");
			this.output = base.ValueOutput<T>("output", new Func<Flow, T>(this.Operation)).Predictable();
			base.Requirement(this.input, this.output);
		}

		// Token: 0x06000689 RID: 1673 RVA: 0x0000CEEE File Offset: 0x0000B0EE
		private T Operation(Flow flow)
		{
			return this.Operation(flow.GetValue<T>(this.input));
		}

		// Token: 0x0600068A RID: 1674
		public abstract T Operation(T input);
	}
}
